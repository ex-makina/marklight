#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
#endregion

namespace MarkLight.ValueConverters
{
    /// <summary>
    /// Value converter for ElementSize type.
    /// </summary>
    public class ElementSizeValueConverter : ValueConverter
    {
        #region Fields

        private Type _intType;
        private Type _floatType;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ElementSizeValueConverter()
        {
            _type = typeof(ElementSize);
            _intType = typeof(int);
            _floatType = typeof(float);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for ElementSize type.
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
                    var convertedValue = ElementSize.Parse(stringValue, context.UnitSize);
                    return new ConversionResult(convertedValue);
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }
            else if (valueType == _intType)
            {
                return new ConversionResult(ElementSize.FromPixels((float)value));
            }
            else if (valueType == _floatType)
            {
                return new ConversionResult(ElementSize.FromPixels((float)value));
            }

            return ConversionFailed(value);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            ElementSize elementSize = value as ElementSize;
            return elementSize.ToString();
        }

        #endregion
    }
}
