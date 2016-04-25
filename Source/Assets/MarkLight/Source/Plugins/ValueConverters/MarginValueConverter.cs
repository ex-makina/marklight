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
    /// Value converter for int type.
    /// </summary>
    public class MarginValueConverter : ValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MarginValueConverter()
        {
            _type = typeof(ElementMargin);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Margin type.
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
                    string[] valueList;
                    valueList = stringValue.Split(',').ToArray();
                    ElementMargin convertedValue = null;
                    if (valueList.Length == 1)
                    {
                        convertedValue = new ElementMargin(ElementSize.Parse(valueList[0], context.UnitSize));
                    }
                    else if (valueList.Length == 2)
                    {
                        convertedValue = new ElementMargin(
                            ElementSize.Parse(valueList[0], context.UnitSize),
                            ElementSize.Parse(valueList[1], context.UnitSize));
                    }
                    else if (valueList.Length == 3)
                    {
                        convertedValue = new ElementMargin(
                            ElementSize.Parse(valueList[0], context.UnitSize),
                            ElementSize.Parse(valueList[1], context.UnitSize),
                            ElementSize.Parse(valueList[2], context.UnitSize));
                    }
                    else if (valueList.Length == 4)
                    {
                        convertedValue = new ElementMargin(
                            ElementSize.Parse(valueList[0], context.UnitSize),
                            ElementSize.Parse(valueList[1], context.UnitSize),
                            ElementSize.Parse(valueList[2], context.UnitSize),
                            ElementSize.Parse(valueList[3], context.UnitSize));
                    }
                    else
                    {
                        return StringConversionFailed(value);
                    }

                    return new ConversionResult(convertedValue);
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }
            else
            {
                // attempt to convert using system type converter
                try
                {
                    var convertedValue = System.Convert.ToInt32(value, CultureInfo.InvariantCulture);
                    return new ConversionResult(convertedValue);
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            ElementMargin margin = value as ElementMargin;
            return margin.ToString();
        }

        #endregion
    }
}
