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

        public List<ViewTypeData> ViewTypeData;
        public List<ThemeData> ThemeData;
        public List<ResourceDictionary> ResourceDictionaries;
        public string MainView;
        public string DefaultTheme;
        public string DefaultLanguage;
        public string DefaultPlatform;
        public List<string> Views;
        public List<string> Themes;
        public GameObject RootView;
        public List<Sprite> Sprites;
        public List<string> SpritePaths;
        public List<Font> Fonts;
        public List<string> FontPaths;
        public List<Material> Materials;
        public List<string> MaterialPaths;
        public bool DisableAutomaticReload;
        public bool UpdateXsdSchema;

        private static ViewPresenter _instance;
        private Dictionary<string, Type> _viewTypes;
        private Dictionary<string, ViewTypeData> _viewTypeDataDictionary;
        private Dictionary<string, ThemeData> _themeDataDictionary;
        private Dictionary<string, ResourceDictionary> _resourceDictionaries;
        private Dictionary<string, ValueConverter> _valueConvertersForType;
        private Dictionary<string, ValueConverter> _valueConverters;
        private Dictionary<string, ValueInterpolator> _valueInterpolatorsForType;
        private Dictionary<string, Sprite> _spriteDictionary;
        private Dictionary<string, Font> _fontDictionary;
        private Dictionary<string, Material> _materialDictionary;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewPresenter()
        {
            ViewTypeData = new List<ViewTypeData>();
            ThemeData = new List<ThemeData>();
            ResourceDictionaries = new List<ResourceDictionary>();
            Views = new List<string>();
            Themes = new List<string>();
            Sprites = new List<Sprite>();
            SpritePaths = new List<string>();
            Fonts = new List<Font>();
            FontPaths = new List<string>();
            Materials = new List<Material>();
            MaterialPaths = new List<string>();
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
        public void InitializeViews(View rootView)
        {
            InitializeViews(rootView.gameObject);
        }

        /// <summary>
        /// Initializes the views. Called once on root view at the start of the scene. Need to be called on any views created dynamically.
        /// </summary>
        public void InitializeViews(GameObject rootView)
        {
            if (rootView == null)
            {
                return;                
            }

            rootView.ForThisAndEachChild<View>(x => x.TryInitializeInternalDefaultValues());
            rootView.ForThisAndEachChild<View>(x => x.TryInitializeInternal());
            rootView.ForThisAndEachChild<View>(x => x.TryInitialize(), true, null, TraversalAlgorithm.ReverseBreadthFirst);
            rootView.ForThisAndEachChild<View>(x => x.TryPropagateBindings(), true, null, TraversalAlgorithm.BreadthFirst);
            rootView.ForThisAndEachChild<View>(x => x.TryQueueAllChangeHandlers(), true, null, TraversalAlgorithm.ReverseBreadthFirst);

            // notify dictionary observers
            ResourceDictionary.NotifyObservers();

            // trigger change handlers
            int pass = 0;
            while (rootView.Find<View>(x => x.HasQueuedChangeHandlers))
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
        }

        /// <summary>
        /// Prints triggered change handler overflow error message.
        /// </summary>
        private void PrintTriggeredChangeHandlerOverflowError(int pass, GameObject rootView)
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

            Utils.LogError("[MarkLight] Error initializing views. Stack overflow when triggering change handlers. Make sure your change handlers doesn't trigger each other in a loop. The following change handlers were still triggered after {0} passes:{1}{2}", pass, Environment.NewLine, sb.ToString());
        }

        /// <summary>
        /// Removes all view data from presenter and clears the scene.
        /// </summary>
        public void Clear()
        {
            ThemeData.Clear();
            ViewTypeData.Clear();
            ResourceDictionaries.Clear();
            Sprites.Clear();
            SpritePaths.Clear();
            Fonts.Clear();
            FontPaths.Clear();
            Materials.Clear();
            MaterialPaths.Clear();

            _viewTypeDataDictionary = null;
            _themeDataDictionary = null;
            _resourceDictionaries = null;
            _spriteDictionary = null;
            _fontDictionary = null;
            _materialDictionary = null;            

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
            if (_viewTypeDataDictionary == null || !_viewTypeDataDictionary.ContainsKey(viewTypeName))
            {
                LoadViewTypeDataDictionary();
            }

            if (!_viewTypeDataDictionary.ContainsKey(viewTypeName))
            {
                Utils.LogError("[MarkLight] Can't find view type \"{0}\".", viewTypeName);
                return null;
            }

            return _viewTypeDataDictionary[viewTypeName];
        }

        /// <summary>
        /// Loads the view type data dictionary.
        /// </summary>
        private void LoadViewTypeDataDictionary()
        {
            _viewTypeDataDictionary = new Dictionary<string, ViewTypeData>();
            foreach (var viewTypeData in ViewTypeData)
            {
                _viewTypeDataDictionary.Add(viewTypeData.ViewName, viewTypeData);
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
        /// Gets pre-loaded sprite from asset path.
        /// </summary>
        public Sprite GetSprite(string assetPath)
        {
            if (_spriteDictionary == null)
            {
                _spriteDictionary = new Dictionary<string, Sprite>();
                for (int i = 0; i < Sprites.Count; ++i)
                {
                    _spriteDictionary.Add(SpritePaths[i], Sprites[i]);
                }
            }

            return _spriteDictionary.Get(assetPath);            
        }

        /// <summary>
        /// Gets asset path from sprite.
        /// </summary>
        public string GetSpriteAssetPath(Sprite sprite)
        {
            int index = Sprites.IndexOf(sprite);
            return SpritePaths[index];
        }

        /// <summary>
        /// Gets pre-loaded font from asset path.
        /// </summary>
        public Font GetFont(string assetPath)
        {
            if (_fontDictionary == null)
            {
                _fontDictionary = new Dictionary<string, Font>();
                for (int i = 0; i < Fonts.Count; ++i)
                {
                    _fontDictionary.Add(FontPaths[i], Fonts[i]);
                }
            }

            return _fontDictionary.Get(assetPath);
        }

        /// <summary>
        /// Gets asset path from font.
        /// </summary>
        public string GetFontAssetPath(Font font)
        {
            int index = Fonts.IndexOf(font);
            return FontPaths[index];
        }

        /// <summary>
        /// Gets pre-loaded material from asset path.
        /// </summary>
        public Material GetMaterial(string assetPath)
        {
            if (_materialDictionary == null)
            {
                _materialDictionary = new Dictionary<string, Material>();
                for (int i = 0; i < Fonts.Count; ++i)
                {
                    _materialDictionary.Add(MaterialPaths[i], Materials[i]);
                }
            }

            return _materialDictionary.Get(assetPath);
        }

        /// <summary>
        /// Gets asset path from material.
        /// </summary>
        public string GetMaterialAssetPath(Material material)
        {
            int index = Materials.IndexOf(material);
            return MaterialPaths[index];
        }

        /// <summary>
        /// Adds sprite to list of loaded sprites.
        /// </summary>
        public void AddSprite(string assetPath, Sprite asset)
        {
            if (SpritePaths.Contains(assetPath))
                return;

            SpritePaths.Add(assetPath);
            Sprites.Add(asset);
            
            if (_spriteDictionary != null && 
                !_spriteDictionary.ContainsKey(assetPath))
            {
                _spriteDictionary.Add(assetPath, asset);
            }
        }

        /// <summary>
        /// Adds font to list of loaded fonts.
        /// </summary>
        public void AddFont(string assetPath, Font asset)
        {
            if (FontPaths.Contains(assetPath))
                return;

            FontPaths.Add(assetPath);
            Fonts.Add(asset);

            if (_fontDictionary != null &&
                !_fontDictionary.ContainsKey(assetPath))
            {
                _fontDictionary.Add(assetPath, asset);
            }
        }

        /// <summary>
        /// Adds material to list of loaded materials.
        /// </summary>
        public void AddMaterial(string assetPath, Material asset)
        {
            if (MaterialPaths.Contains(assetPath))
                return;

            MaterialPaths.Add(assetPath);
            Materials.Add(asset);

            if (_materialDictionary != null &&
                !_materialDictionary.ContainsKey(assetPath))
            {
                _materialDictionary.Add(assetPath, asset);
            }
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
                foreach (var valueConverterType in TypeHelper.FindDerivedTypes(typeof(ValueConverter)))
                {
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
                foreach (var valueConverterType in TypeHelper.FindDerivedTypes(typeof(ValueConverter)))
                {
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

        #endregion

        #region Properties

        /// <summary>
        /// Gets global presentation engine instance.
        /// </summary>
        public static ViewPresenter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = UnityEngine.Object.FindObjectOfType(typeof(ViewPresenter)) as ViewPresenter;
                }

                return _instance;
            }
        }

        #endregion
    }
}
