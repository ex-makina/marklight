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
    /// Value converter for Material type.
    /// </summary>
    public class MaterialValueConverter : ValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MaterialValueConverter()
        {
            _type = typeof(Material);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Material type.
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
                    asset = ViewPresenter.Instance.GetMaterial(assetPath);
                    if (asset != null)
                    {
                        // yes. return pre-loaded asset
                        return new ConversionResult(asset);
                    }
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        // load material from asset database
                        asset = AssetDatabase.LoadAssetAtPath(assetPath, _type);
                        if (asset == null)
                        {
                            return ConversionFailed(value, String.Format("Asset not found at path \"{0}\".", assetPath));
                        }

                        ViewPresenter.Instance.AddMaterial(assetPath, asset as Material);
                        return new ConversionResult(asset);
                    }
#endif
                    return ConversionFailed(value, String.Format("Pre-loaded asset not found for path \"{0}\".", assetPath));
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }

            return ConversionFailed(value);
        }

        #endregion
    }
}
