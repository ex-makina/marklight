#region Using Statements
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using MarkLight.Views.UI;
using MarkLight.Animation;
using MarkLight.ValueConverters;
#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2 && !UNITY_5_3_1 && !UNITY_5_3_2 && !UNITY_5_3_3
using UnityEngine.SceneManagement;
#endif
#endregion

namespace MarkLight
{
    /// <summary>
    /// MarkLight Presentation Engine.
    /// </summary>
    [AddComponentMenu("MarkLight/View Presenter")]
    public class ViewPresenter : View
    {
        #region Fields

        public List<ViewTypeData> ViewTypeDataList;
        public List<ThemeData> ThemeData;
        public List<ResourceDictionary> ResourceDictionaries;
        public string MainView;
        public string DefaultTheme;
        public string DefaultLanguage;
        public string DefaultPlatform;
        public List<string> Views;
        public List<string> Themes;
        public GameObject RootView;
        public bool DisableAutomaticReload;
        public bool UpdateXsdSchema;

        public AssetDictionary AssetDictionary;

        private static ViewPresenter _instance;
        private static string _currentScene;
        private Dictionary<string, ValueConverter> _cachedValueConverters;
        private Dictionary<string, Type> _viewTypes;
        private Dictionary<string, ViewTypeData> _viewTypeDataDictionary;
        private Dictionary<string, ThemeData> _themeDataDictionary;
        private Dictionary<string, ResourceDictionary> _resourceDictionaries;
        private Dictionary<string, ValueConverter> _valueConvertersForType;
        private Dictionary<string, ValueConverter> _valueConverters;
        private Dictionary<string, ValueInterpolator> _valueInterpolatorsForType;        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewPresenter()
        {
            AssetDictionary = new AssetDictionary();
            ViewTypeDataList = new List<ViewTypeData>();
            ThemeData = new List<ThemeData>();
            ResourceDictionaries = new List<ResourceDictionary>();
            Views = new List<string>();
            Themes = new List<string>();
            UnitSize = new Vector3(40, 40, 40);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Called once at startup.
        /// </summary>
        public void Awake()
        {
            Initialize();
        }

        /// <summary>
        /// Called once to initialize views and runtime data.
        /// </summary>
        public override void Initialize()
        {
            UpdateInstance();

            // initialize resource dictionary
            ResourceDictionary.Language = DefaultLanguage;
            ResourceDictionary.Platform = DefaultPlatform;
            ResourceDictionary.Initialize();

            // initialize all views in the scene
            InitializeViews(RootView);
        }

        /// <summary>
        /// Initializes the views. Called once on root view at the start of the scene. Need to be called on any views created dynamically.
        /// </summary>
        public void InitializeViews(GameObject rootView)
        {
            if (rootView == null)
                return;

            InitializeViews(rootView.GetComponent<View>());
        }

        /// <summary>
        /// Initializes the views. Called once on root view at the start of the scene. Need to be called on any views created dynamically.
        /// </summary>
        public void InitializeViews(View rootView)
        {
            if (rootView == null || rootView.IsInitialized)
            {
                return;
            }

            // uncomment to log initialization performance
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            rootView.ForThisAndEachChild<View>(x => x.TryInitializeInternalDefaultValues());
            rootView.ForThisAndEachChild<View>(x => x.TryInitializeInternal());
            rootView.ForThisAndEachChild<View>(x => x.TryInitialize(), true, null, TraversalAlgorithm.ReverseBreadthFirst);
            rootView.ForThisAndEachChild<View>(x => x.TryPropagateBindings(), true, null, TraversalAlgorithm.BreadthFirst);
            rootView.ForThisAndEachChild<View>(x => x.TryQueueAllChangeHandlers(), true, null, TraversalAlgorithm.ReverseBreadthFirst);

            // notify dictionary observers
            ResourceDictionary.NotifyObservers();

            // trigger change handlers
            int pass = 0;
            while (rootView.Find<View>(x => x.HasQueuedChangeHandlers) != null)
            {
                if (pass >= 1000)
                {
                    PrintTriggeredChangeHandlerOverflowError(pass, rootView);
                    break;
                }

                // as long as there are change handlers queued, go through all views and trigger them
                rootView.ForThisAndEachChild<View>(x => x.TryTriggerChangeHandlers(), true, null, TraversalAlgorithm.ReverseBreadthFirst);
                ++pass;
            }

            // uncomment to log initialization performance
            //sw.Stop();
            //if (rootView.gameObject == RootView)
            //{
            //    Utils.Log("Initialization time: {0}", sw.ElapsedMilliseconds);
            //}
        }

        /// <summary>
        /// Prints triggered change handler overflow error message.
        /// </summary>
        private void PrintTriggeredChangeHandlerOverflowError(int pass, View rootView)
        {
            var sb = new StringBuilder();
            var triggeredViews = rootView.GetChildren<View>(x => x.HasQueuedChangeHandlers);
            foreach (var triggeredView in triggeredViews)
            {
                sb.AppendFormat("{0}: ", triggeredView.GameObjectName);
                sb.AppendLine();
                foreach (var triggeredChangeHandler in triggeredView.QueuedChangeHandlers)
                {
                    sb.AppendFormat("\t{0}", triggeredChangeHandler);
                    sb.AppendLine();
                }
            }

            Debug.LogError(String.Format("[MarkLight] Error initializing views. Stack overflow when triggering change handlers. Make sure your change handlers doesn't trigger each other in a loop. The following change handlers were still triggered after {0} passes:{1}{2}", pass, Environment.NewLine, sb.ToString()));
        }

        /// <summary>
        /// Removes all view data from presenter and clears the scene.
        /// </summary>
        public void Clear()
        {
            ThemeData.Clear();
            ViewTypeDataList.Clear();
            ResourceDictionaries.Clear();
            AssetDictionary.Clear();

            _viewTypeDataDictionary = null;
            _themeDataDictionary = null;
            _resourceDictionaries = null;
            _viewTypes = null;

            if (RootView != null)
            {
                GameObject.DestroyImmediate(RootView);
            }
        }

        /// <summary>
        /// Gets view type data.
        /// </summary>
        public ViewTypeData GetViewTypeData(string viewTypeName)
        {
            if (_viewTypeDataDictionary == null)
            {
                LoadViewTypeDataDictionary();
            }

            ViewTypeData viewTypeData;
            if (!_viewTypeDataDictionary.TryGetValue(viewTypeName, out viewTypeData))
            {
                Debug.LogError(String.Format("[MarkLight] Can't find view type \"{0}\".", viewTypeName));
                return null;
            }

            return viewTypeData;
        }

        /// <summary>
        /// Loads the view type data dictionary.
        /// </summary>
        private void LoadViewTypeDataDictionary()
        {
            _viewTypeDataDictionary = new Dictionary<string, ViewTypeData>();
            foreach (var viewTypeData in ViewTypeDataList)
            {
                foreach (var viewName in viewTypeData.ViewNameAliases)
                {
                    if (_viewTypeDataDictionary.ContainsKey(viewName))
                    {
                        Debug.LogError(String.Format("[MarkLight] Can't map view-model \"{0}\" to view \"{1}\" because it is already mapped to view-model \"{2}\". If you want to replace another view-model use the ReplaceViewModel class attribute. Otherwise choose a different view name that is available.", viewTypeData.ViewTypeName, viewName, _viewTypeDataDictionary[viewName].ViewTypeName));
                        continue;
                    }

                    _viewTypeDataDictionary.Add(viewName, viewTypeData);
                }
            }

            // check if view-models should be replaced
            foreach (var viewTypeData in ViewTypeDataList.Where(x => !String.IsNullOrEmpty(x.ReplacesViewModel)))
            {
                // find the view type it replaces
                var replacedViewTypeData = ViewTypeDataList.FirstOrDefault(x => String.Equals(x.ViewTypeName, viewTypeData.ReplacesViewModel, StringComparison.OrdinalIgnoreCase));
                if (replacedViewTypeData == null)
                    continue;

                // replace the view-model
                foreach (var kv in _viewTypeDataDictionary.ToList())
                {
                    if (kv.Value == replacedViewTypeData)
                    {
                        _viewTypeDataDictionary[kv.Key] = viewTypeData;
                    }
                }
            }
        }

        /// <summary>
        /// Gets theme data.
        /// </summary>
        public ThemeData GetThemeData(string themeName)
        {
            if (_themeDataDictionary == null)
            {
                _themeDataDictionary = new Dictionary<string, ThemeData>();
                foreach (var themeData in ThemeData)
                {
                    _themeDataDictionary.Add(themeData.ThemeName, themeData);
                }
            }

            return _themeDataDictionary.Get(themeName);
        }

        /// <summary>
        /// Gets resource dictionary.
        /// </summary>
        public ResourceDictionary GetResourceDictionary(string dictionaryName)
        {
            if (_resourceDictionaries == null)
            {
                _resourceDictionaries = new Dictionary<string, ResourceDictionary>();
                foreach (var resourceDictionary in ResourceDictionaries)
                {
                    _resourceDictionaries.Add(resourceDictionary.Name, resourceDictionary);
                }
            }

            return _resourceDictionaries.Get(dictionaryName);
        }

        /// <summary>
        /// Gets pre-loaded asset from path.
        /// </summary>
        public UnityAsset GetAsset(string assetPath)
        {
            return AssetDictionary.Get(assetPath);
        }

        /// <summary>
        /// Gets pre-loaded asset from path.
        /// </summary>
        public UnityAsset GetAsset(UnityEngine.Object asset)
        {
            return AssetDictionary.Get(asset);
        }

        /// <summary>
        /// Gets asset path from asset.
        /// </summary>
        public string GetAssetPath(UnityEngine.Object asset)
        {
            var unityAsset = AssetDictionary.Get(asset);
            return unityAsset != null ? unityAsset.Path : String.Empty;
        }        

        /// <summary>
        /// Adds asset to list of loaded assets.
        /// </summary>
        public UnityAsset AddAsset(string path, UnityEngine.Object asset)
        {
            if (!AssetDictionary.ContainsKey(path))
            {
                var unityAsset = new UnityAsset(path, asset);
                AssetDictionary.Add(unityAsset);
                return unityAsset;
            }

            return AssetDictionary[path];
        }

        /// <summary>
        /// Gets view type from view type name.
        /// </summary>
        public Type GetViewType(string viewTypeName)
        {
            if (_viewTypes == null)
            {
                _viewTypes = new Dictionary<string, Type>();
                foreach (var viewType in TypeHelper.FindDerivedTypes(typeof(View)))
                {
                    _viewTypes.Add(viewType.Name, viewType);
                }
            }

            return _viewTypes.Get(viewTypeName);
        }

        /// <summary>
        /// Gets value converter for view field type.
        /// </summary>
        public ValueConverter GetValueConverterForType(string viewFieldType)
        {
            if (_valueConvertersForType == null)
            {
                _valueConvertersForType = new Dictionary<string, ValueConverter>();

                // cache standard converters to improve load performance
                _valueConvertersForType.Add("Object", new ValueConverter());
                _valueConvertersForType.Add("Single", new FloatValueConverter());
                _valueConvertersForType.Add("Int32", new IntValueConverter());
                _valueConvertersForType.Add("Boolean", new BoolValueConverter());
                _valueConvertersForType.Add("Color", new ColorValueConverter());
                _valueConvertersForType.Add("ElementSize", new ElementSizeValueConverter());
                _valueConvertersForType.Add("Enum", new EnumValueConverter());
                _valueConvertersForType.Add("Component", new ComponentValueConverter());
                _valueConvertersForType.Add("Font", new FontValueConverter());
                _valueConvertersForType.Add("ElementMargin", new MarginValueConverter());
                _valueConvertersForType.Add("Material", new MaterialValueConverter());
                _valueConvertersForType.Add("Quaternion", new QuaternionValueConverter());
                _valueConvertersForType.Add("Sprite", new SpriteValueConverter());
                _valueConvertersForType.Add("UnityAsset", new AssetValueConverter());
                _valueConvertersForType.Add("SpriteAsset", new SpriteAssetValueConverter());
                _valueConvertersForType.Add("String", new StringValueConverter());
                _valueConvertersForType.Add("Vector2", new Vector2ValueConverter());
                _valueConvertersForType.Add("Vector3", new Vector3ValueConverter());
                _valueConvertersForType.Add("Vector4", new Vector4ValueConverter());

                foreach (var valueConverterType in TypeHelper.FindDerivedTypes(typeof(ValueConverter)))
                {
                    if (CachedValueConverters.ContainsKey(valueConverterType.Name))
                        continue;

                    var valueConverter = TypeHelper.CreateInstance(valueConverterType) as ValueConverter;
                    if (valueConverter.Type != null)
                    {
                        var valueTypeName = valueConverter.Type.Name;
                        if (!_valueConvertersForType.ContainsKey(valueTypeName))
                        {
                            _valueConvertersForType.Add(valueTypeName, valueConverter);
                        }
                    }
                }
            }

            return _valueConvertersForType.Get(viewFieldType);
        }

        /// <summary>
        /// Gets value converter.
        /// </summary>
        public ValueConverter GetValueConverter(string valueConverterTypeName)
        {
            if (_valueConverters == null)
            {
                _valueConverters = new Dictionary<string, ValueConverter>();

                // cache standard converters to improve load performance
                foreach (var cachedConverter in CachedValueConverters)
                {
                    _valueConverters.Add(cachedConverter.Key, cachedConverter.Value);
                }

                foreach (var valueConverterType in TypeHelper.FindDerivedTypes(typeof(ValueConverter)))
                {
                    if (_valueConverters.ContainsKey(valueConverterType.Name))
                        continue;

                    var valueConverter = TypeHelper.CreateInstance(valueConverterType) as ValueConverter;
                    _valueConverters.Add(valueConverterType.Name, valueConverter);
                }
            }

            return _valueConverters.Get(valueConverterTypeName);
        }

        /// <summary>
        /// Gets value interpolator for view field type.
        /// </summary>
        public ValueInterpolator GetValueInterpolatorForType(string viewFieldType)
        {
            if (_valueInterpolatorsForType == null)
            {
                _valueInterpolatorsForType = new Dictionary<string, ValueInterpolator>();
                foreach (var valueInterpolatorType in TypeHelper.FindDerivedTypes(typeof(ValueInterpolator)))
                {
                    var valueInterpolator = TypeHelper.CreateInstance(valueInterpolatorType) as ValueInterpolator;
                    if (valueInterpolator.Type != null)
                    {
                        var valueTypeName = valueInterpolator.Type.Name;
                        if (!_valueInterpolatorsForType.ContainsKey(valueTypeName))
                        {
                            _valueInterpolatorsForType.Add(valueTypeName, valueInterpolator);
                        }
                    }
                }
            }

            return _valueInterpolatorsForType.Get(viewFieldType);
        }

        /// <summary>
        /// Loads asset at path.
        /// </summary>
        public UnityAsset LoadAsset(string path, UnityEngine.Object asset)
        {
            var unityAsset = GetAsset(path);
            if (unityAsset == null)
            {
                unityAsset = AddAsset(path, asset);
            }
            else
            {
                unityAsset.Asset = asset;
                unityAsset.NotifyObservers();
            }

            return unityAsset;
        }

        /// <summary>
        /// Unloads the asset at path.
        /// </summary>
        public void UnloadAsset(string path)
        {
            var unityAsset = GetAsset(path);
            if (unityAsset == null)
            {
                Debug.LogError(String.Format("[MarkLight] Unable to unload asset \"{0}\". Asset not found.", path));
                return;
            }

            unityAsset.Unload();
        }

        /// <summary>
        /// Refreshes and updates the view presenter instance.
        /// </summary>
        public static void UpdateInstance()
        {
#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1 && !UNITY_5_2 && !UNITY_5_3_1 && !UNITY_5_3_2 && !UNITY_5_3_3
            var sceneName = SceneManager.GetActiveScene().name;
#else
            var sceneName = Application.loadedLevelName;
#endif
            if (_instance == null || sceneName != _currentScene)
            {
                _instance = UnityEngine.Object.FindObjectOfType(typeof(ViewPresenter)) as ViewPresenter;
                _currentScene = sceneName;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets global presentation engine instance.
        /// </summary>
        public static ViewPresenter Instance
        {
            get
            {
                UpdateInstance();
                return _instance;
            }
        }

        /// <summary>
        /// Gets cached value converters.
        /// </summary>
        private Dictionary<string, ValueConverter> CachedValueConverters
        {
            get
            {
                if (_cachedValueConverters == null)
                {
                    _cachedValueConverters = new Dictionary<string, ValueConverter>();
                    _cachedValueConverters.Add("ValueConverter", new ValueConverter());
                    _cachedValueConverters.Add("FloatValueConverter", new FloatValueConverter());
                    _cachedValueConverters.Add("IntValueConverter", new IntValueConverter());
                    _cachedValueConverters.Add("BoolValueConverter", new BoolValueConverter());
                    _cachedValueConverters.Add("ColorValueConverter", new ColorValueConverter());
                    _cachedValueConverters.Add("ElementSizeValueConverter", new ElementSizeValueConverter());
                    _cachedValueConverters.Add("ComponentValueConverter", new ComponentValueConverter());
                    _cachedValueConverters.Add("EnumValueConverter", new EnumValueConverter());
                    _cachedValueConverters.Add("FontValueConverter", new FontValueConverter());
                    _cachedValueConverters.Add("MarginValueConverter", new MarginValueConverter());
                    _cachedValueConverters.Add("MaterialValueConverter", new MaterialValueConverter());
                    _cachedValueConverters.Add("QuaternionValueConverter", new QuaternionValueConverter());
                    _cachedValueConverters.Add("SpriteValueConverter", new SpriteValueConverter());
                    _cachedValueConverters.Add("SpriteAssetValueConverter", new SpriteAssetValueConverter());
                    _cachedValueConverters.Add("AssetValueConverter", new AssetValueConverter());
                    _cachedValueConverters.Add("StringValueConverter", new StringValueConverter());
                    _cachedValueConverters.Add("Vector2ValueConverter", new Vector2ValueConverter());
                    _cachedValueConverters.Add("Vector3ValueConverter", new Vector3ValueConverter());
                    _cachedValueConverters.Add("Vector4ValueConverter", new Vector4ValueConverter());
                }

                return _cachedValueConverters;
            }
        }

        #endregion
    }
}
