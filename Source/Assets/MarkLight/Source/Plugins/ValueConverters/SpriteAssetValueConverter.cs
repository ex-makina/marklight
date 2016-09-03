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
    public class SpriteAssetValueConverter : AssetValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SpriteAssetValueConverter()
        {
            _type = typeof(SpriteAsset);
            _loadType = typeof(Sprite);
            IsUnityAssetType = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts loaded asset to desired type.
        /// </summary>
        protected override ConversionResult ConvertAssetResult(UnityAsset loadedAsset)
        {
            return new ConversionResult(new SpriteAsset(loadedAsset));
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            return value != null ? (value as SpriteAsset).Path : String.Empty;            
        }

        #endregion
    }
}