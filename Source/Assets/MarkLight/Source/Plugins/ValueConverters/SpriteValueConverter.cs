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
        #region Fields

        private Type _uiSpriteType;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SpriteValueConverter()
        {
            _type = typeof(Sprite);
            _uiSpriteType = typeof(UISprite);
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
            else if (valueType == _uiSpriteType)
            {
                var spriteValue = value as UISprite;
                return new ConversionResult(spriteValue.Sprite);
            }
            else if (valueType == _stringType)
            {
                try
                {
                    UISpriteValueConverter uiConverter = new UISpriteValueConverter();
                    var result = uiConverter.Convert(value);
                    if (result.ConvertedValue == null)
                        return result;

                    var uiSprite = result.ConvertedValue as UISprite;
                    return new ConversionResult(uiSprite.Sprite);
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