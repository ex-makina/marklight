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
    public class FontValueConverter : ValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FontValueConverter()
        {
            _type = typeof(Font);
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
                    UnityEngine.Object asset = null;
                    if (String.IsNullOrEmpty(assetPath))
                    {
                        return new ConversionResult(null);
                    }

                    if (!String.IsNullOrEmpty(context.BaseDirectory))
                    {
                        assetPath = Path.Combine(context.BaseDirectory, assetPath);
                    }

                    // is asset pre-loaded?
                    asset = ViewPresenter.Instance.GetFont(assetPath);
                    if (asset != null)
                    {
                        // yes. return pre-loaded asset
                        return new ConversionResult(asset);
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

                    // load asset from asset database
                    if (!Application.isPlaying || inResourcesFolder)
                    {
                        // load font from asset database
#if UNITY_EDITOR 
                        asset = inResourcesFolder ? Resources.Load(loadAssetPath) : AssetDatabase.LoadAssetAtPath(loadAssetPath, _type);
#else
                        asset = Resources.Load(loadAssetPath);
#endif
                        if (asset == null)
                        {
                            return ConversionFailed(value, String.Format("Asset not found at path \"{0}\".", assetPath));
                        }

                        ViewPresenter.Instance.AddFont(assetPath, asset as Font);
                        return new ConversionResult(asset);
                    }

                    return ConversionFailed(value, String.Format("Pre-loaded asset not found for path \"{0}\".", assetPath));
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }

            return ConversionFailed(value);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            return value != null ? ViewPresenter.Instance.GetFontAssetPath(value as Font) : String.Empty;
        }

#endregion
    }
}
