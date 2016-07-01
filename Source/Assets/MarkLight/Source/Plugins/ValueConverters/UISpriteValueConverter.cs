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
    /// Value converter for UISprite type.
    /// </summary>
    public class UISpriteValueConverter : ValueConverter
    {
        #region Fields

        private Type _spriteType;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public UISpriteValueConverter()
        {
            _type = typeof(UISprite);
            _spriteType = typeof(Sprite);
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
            else if (valueType == _spriteType)
            {
                var spriteValue = value as Sprite;

                // check if view-presenter contains sprite
                var uiSprite = ViewPresenter.Instance.GetSprite(spriteValue);
                if (uiSprite == null)
                {
                    uiSprite = new UISprite(spriteValue, String.Empty);
                }

                return new ConversionResult(uiSprite);
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

                    bool isOnDemandLoaded = assetPath.StartsWith("?");
                    if (isOnDemandLoaded)
                    {
                        assetPath = assetPath.Substring(1);
                    }
                    else if (!String.IsNullOrEmpty(context.BaseDirectory))
                    {
                        assetPath = Path.Combine(context.BaseDirectory, assetPath);
                    }

                    // has sprite been loaded previously?
                    var uiSprite = ViewPresenter.Instance.GetSprite(assetPath);
                    if (uiSprite != null)
                    {
                        // yes. return sprite
                        return new ConversionResult(uiSprite);
                    }

                    // is the sprite to be loaded externally on-demand?
                    if (isOnDemandLoaded)
                    {
                        // yes. 
                        UISprite sprite = new UISprite(null, assetPath);
                        ViewPresenter.Instance.AddSprite(sprite);
                        return new ConversionResult(sprite);
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
                            UISprite loadedSprite = new UISprite(asset as Sprite, assetPath);
                            ViewPresenter.Instance.AddSprite(loadedSprite);
                            return new ConversionResult(loadedSprite);
                        }
                        else
                        {
                            // load sprite
#if UNITY_EDITOR
                            asset = inResourcesFolder ? Resources.Load(loadAssetPath, _spriteType) : AssetDatabase.LoadAssetAtPath(loadAssetPath, _spriteType);
#else
                            asset = Resources.Load(loadAssetPath, _spriteType);
#endif                            
                            if (asset == null)
                            {
                                return ConversionFailed(value, String.Format("Asset not found at path \"{0}\".", assetPath));
                            }

                            UISprite loadedSprite = new UISprite(asset as Sprite, assetPath);
                            ViewPresenter.Instance.AddSprite(loadedSprite);
                            return new ConversionResult(loadedSprite);
                        }
                    }
                    
                    return ConversionFailed(value, String.Format("Sprite asset not found at path \"{0}\".", assetPath));
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
            return value != null ? (value as UISprite).Path : String.Empty;
        }

        #endregion
    }
}
