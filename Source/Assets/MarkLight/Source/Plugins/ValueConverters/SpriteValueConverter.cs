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
    public class SpriteValueConverter : AssetValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SpriteValueConverter()
        {
            _type = typeof(Sprite);
            _loadType = _type;
            IsUnityAssetType = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Used to extend the asset value converter with custom types.
        /// </summary>
        protected override ConversionResult ConvertCustomType(object value, Type valueType, ValueConverterContext context)
        {
            var spriteAsset = value as SpriteAsset;
            return spriteAsset != null ? new ConversionResult(spriteAsset.Sprite) : ConversionFailed(value);
        }

        #endregion
    }
}