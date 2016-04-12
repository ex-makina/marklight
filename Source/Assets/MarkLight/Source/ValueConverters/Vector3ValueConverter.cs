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
    /// Value converter for Vector3 type.
    /// </summary>
    public class Vector3ValueConverter : ValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Vector3ValueConverter()
        {
            _type = typeof(Vector3);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Vector3 type.
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
                float[] valueList;
                try
                {
                    valueList = stringValue.Split(',').Select(x => System.Convert.ToSingle(x, CultureInfo.InvariantCulture)).ToArray();
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }

                if (valueList.Length == 1)
                {
                    return new ConversionResult(new Vector3(valueList[0], valueList[0], valueList[0]));
                }
                else if (valueList.Length == 2)
                {
                    return new ConversionResult(new Vector3(valueList[0], valueList[1]));
                }
                else if (valueList.Length == 3)
                {
                    return new ConversionResult(new Vector3(valueList[0], valueList[1], valueList[2]));
                }
                else
                {
                    return StringConversionFailed(value);
                }
            }

            return ConversionFailed(value);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            Vector3 v = (Vector3)value;
            return String.Format("{0},{1},{2}", v.x, v.y, v.z);
        }

        #endregion
    }
}
