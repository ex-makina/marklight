#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.Globalization;
#endregion

namespace MarkLight.ValueConverters
{
    /// <summary>
    /// Value converter for Color type.
    /// </summary>
    public class Color32ValueConverter : ValueConverter
    {
        #region Fields

        protected ColorValueConverter _colorValueConverter;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Color32ValueConverter()
        {
            _type = typeof(Color32);
            _colorValueConverter = new ColorValueConverter();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Color32 type.
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

            var result = _colorValueConverter.Convert(value, context);
            if (result.Success)
            {
                result.ConvertedValue = (Color32)((Color)result.ConvertedValue);
            }

            return result;
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            return _colorValueConverter.ConvertToString((Color)((Color32)value));
        }

        #endregion
    }
}
