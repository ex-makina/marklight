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
    /// Value converter for Sprite type.
    /// </summary>
    public class SpriteValueConverter : ValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SpriteValueConverter()
        {
            _type = typeof(Sprite);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Sprite type.
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
                    asset = ViewPresenter.Instance.GetSprite(assetPath);
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
                        // load sprite from asset database
                        // does the path refer to a sprite in a sprite atlas?
                        if (assetPath.Contains(":"))
                        {
                            // yes. load sprite from atlas
                            string[] parts = loadAssetPath.Split(':');
                            string filepath = parts[0];

#if UNITY_EDITOR
                            UnityEngine.Object[] subSprites = inResourcesFolder ? Resources.LoadAll(filepath) : AssetDatabase.LoadAllAssetRepresentationsAtPath(filepath);
#else
                            UnityEngine.Object[] subSprites = Resources.LoadAll(filepath);
#endif
                            foreach (var s in subSprites)
                            {
                                if (s.name == parts[1])
                                {
                                    asset = s;
                                    break;
                                }
                            }

                            if (asset == null)
                            {
                                return ConversionFailed(value, String.Format("Asset not found at path \"{0}\".", assetPath));
                            }

                            // add asset to view-presenter
                            ViewPresenter.Instance.AddSprite(assetPath, asset as Sprite);
                            return new ConversionResult(asset);
                        }
                        else
                        {
                            // load sprite
#if UNITY_EDITOR
                            asset = inResourcesFolder ? Resources.Load(loadAssetPath, _type) : AssetDatabase.LoadAssetAtPath(loadAssetPath, _type);
#else
                            asset = Resources.Load(loadAssetPath, _type);
#endif                            
                            if (asset == null)
                            {
                                return ConversionFailed(value, String.Format("Asset not found at path \"{0}\".", assetPath));
                            }

                            ViewPresenter.Instance.AddSprite(assetPath, asset as Sprite);
                            return new ConversionResult(asset);
                        }
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
            return value != null ? ViewPresenter.Instance.GetSpriteAssetPath(value as Sprite) : String.Empty;
        }

#endregion
    }
}
