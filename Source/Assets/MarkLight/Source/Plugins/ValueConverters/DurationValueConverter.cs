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
    /// Value converter for duration.
    /// </summary>
    public class DurationValueConverter : ValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public DurationValueConverter()
        {
            _type = typeof(DurationValueConverter);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for duration type.
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
                    float duration = 0;
                    string trimmedValue = stringValue.Trim();
                    if (trimmedValue.EndsWith("ms", StringComparison.OrdinalIgnoreCase))
                    {
                        int lastIndex = trimmedValue.LastIndexOf("ms", StringComparison.OrdinalIgnoreCase);
                        duration = System.Convert.ToSingle(trimmedValue.Substring(0, lastIndex), CultureInfo.InvariantCulture) / 1000f;
                    }
                    else if (trimmedValue.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                    {
                        int lastIndex = trimmedValue.LastIndexOf("s", StringComparison.OrdinalIgnoreCase);
                        duration = System.Convert.ToSingle(trimmedValue.Substring(0, lastIndex), CultureInfo.InvariantCulture);
                    }
                    else if (trimmedValue.EndsWith("min", StringComparison.OrdinalIgnoreCase))
                    {
                        int lastIndex = trimmedValue.LastIndexOf("min", StringComparison.OrdinalIgnoreCase);
                        duration = System.Convert.ToSingle(trimmedValue.Substring(0, lastIndex), CultureInfo.InvariantCulture) * 60f;
                    }
                    else
                    {
                        duration = System.Convert.ToSingle(trimmedValue, CultureInfo.InvariantCulture);
                    }

                    return new ConversionResult(duration);
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
            return value.ToString() + "s";
        }

        #endregion
    }
}
