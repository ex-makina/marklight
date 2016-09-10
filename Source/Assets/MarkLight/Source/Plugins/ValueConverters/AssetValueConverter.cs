#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MarkLight.ValueConverters
{
    /// <summary>
    /// Value converter for Font type.
    /// </summary>
    public class AssetValueConverter : ValueConverter
    {
        #region Fields

        public Type _loadType;
        public Type _unityAssetType;
        public bool IsUnityAssetType;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public AssetValueConverter()
        {
            _type = typeof(UnityAsset);
            _unityAssetType = typeof(UnityAsset);
            _loadType = _unityAssetType;
            IsUnityAssetType = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Font type.
        /// </summary>
        public override ConversionResult Convert(object value, ValueConverterContext context)
        {
            if (value == null)
            {
                return base.Convert(value, context);
            }

            Type valueType = value.GetType();
            if (valueType == _type)
            {
                return base.Convert(value, context);
            }
            else if (valueType == _stringType)
            {
                var stringValue = (string)value;
                try
                {
                    string assetPath = stringValue.Trim();
                    if (String.IsNullOrEmpty(assetPath))
                    {
                        return new ConversionResult(null);
                    }

                    bool isOnDemandLoaded = assetPath.StartsWith("?");
                    if (isOnDemandLoaded)
                    {
                        assetPath = assetPath.Substring(1);
                    }
                    else if (!String.IsNullOrEmpty(context.BaseDirectory))
                    {
                        assetPath = Path.Combine(context.BaseDirectory, assetPath);
                    }

                    // is asset pre-loaded?
                    var unityAsset = ViewPresenter.Instance.GetAsset(assetPath);
                    if (unityAsset != null)
                    {
                        // yes. return pre-loaded asset
                        return ConvertAssetResult(unityAsset);
                    }

                    // is asset to be loaded externally on-demand?
                    if (isOnDemandLoaded)
                    {
                        // yes.
                        unityAsset = ViewPresenter.Instance.AddAsset(assetPath, null);
                        return ConvertAssetResult(unityAsset);
                    }

                    // if the asset is in a resources folder the load path should be relative to the folder
                    bool inResourcesFolder = assetPath.Contains("Resources/");
                    string loadAssetPath = assetPath;
                    if (inResourcesFolder)
                    {
                        loadAssetPath = loadAssetPath.Substring(assetPath.IndexOf("Resources/") + 10);
                        string extension = System.IO.Path.GetExtension(assetPath);
                        if (extension.Length > 0)
                        {
                            loadAssetPath = loadAssetPath.Substring(0, loadAssetPath.Length - extension.Length);
                        }
                    }

                    bool suppressAssetNotFoundError = false;
                    loadAssetPath = ConvertAssetPath(loadAssetPath, inResourcesFolder, out suppressAssetNotFoundError);

                    // load asset from asset database
                    if (!Application.isPlaying || inResourcesFolder)
                    {
                        UnityEngine.Object asset = null;

                        // load font from asset database
#if UNITY_EDITOR 
                        asset = inResourcesFolder ? Resources.Load(loadAssetPath, _loadType) : AssetDatabase.LoadAssetAtPath(loadAssetPath, _loadType);
#else
                        asset = Resources.Load(loadAssetPath);
#endif
                        if (asset == null)
                        {
                            return suppressAssetNotFoundError ? new ConversionResult(null) : ConversionFailed(value, String.Format("Asset not found at path \"{0}\".", assetPath));
                        }

                        var loadedAsset = ViewPresenter.Instance.AddAsset(assetPath, asset);
                        return ConvertAssetResult(loadedAsset);
                    }

                    return suppressAssetNotFoundError ? new ConversionResult(null) : ConversionFailed(value, String.Format("Pre-loaded asset not found for path \"{0}\".", assetPath));
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }
            else if (valueType == _loadType)
            {
                // is asset pre-loaded? 
                var unityAsset = ViewPresenter.Instance.GetAsset(value as UnityEngine.Object);
                if (unityAsset != null)
                    return ConvertAssetResult(unityAsset); // yes

                // no. return new unity asset
                return ConvertAssetResult(new UnityAsset(String.Empty, value as UnityEngine.Object));
            }
            else
            {
                return ConvertCustomType(value, valueType, context);
            }
        }

        /// <summary>
        /// Used to extend the asset value converter with custom types.
        /// </summary>
        protected virtual ConversionResult ConvertCustomType(object value, Type valueType, ValueConverterContext context)
        {
            return ConversionFailed(value);
        }

        /// <summary>
        /// Converts asset path and sets bool indicating if asset not found errors should be suppressed.
        /// </summary>
        protected virtual string ConvertAssetPath(string loadAssetPath, bool inResourcesFolder, out bool suppressAssetNotFoundError)
        {
            suppressAssetNotFoundError = false;
            return loadAssetPath;
        }

        /// <summary>
        /// Converts loaded asset to desired type.
        /// </summary>
        protected virtual ConversionResult ConvertAssetResult(UnityAsset loadedAsset)
        {
            if (IsUnityAssetType)
            {
                return new ConversionResult(loadedAsset);
            }
            else
            {
                return new ConversionResult(loadedAsset.Asset);
            }
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            if (IsUnityAssetType)
            {
                return value != null ? (value as UnityAsset).Path : String.Empty;
            }
            else
            {
                return ViewPresenter.Instance.GetAssetPath(value as UnityEngine.Object);
            }            
        }

        #endregion
    }
}
