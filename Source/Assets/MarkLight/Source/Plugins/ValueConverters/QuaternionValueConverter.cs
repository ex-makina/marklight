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
    /// Value converter for Quaternion type.
    /// </summary>
    public class QuaternionValueConverter : ValueConverter
    {
        #region Fields

        private Type _vector3Type;
        private Vector3ValueConverter _vector3ValueConverter;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public QuaternionValueConverter()
        {
            _type = typeof(Quaternion);
            _vector3Type = typeof(Vector3);
            _vector3ValueConverter = new Vector3ValueConverter();
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
                var result = _vector3ValueConverter.Convert(value, context);
                if (result.Success)
                {
                    return new ConversionResult(Quaternion.Euler((Vector3)result.ConvertedValue));
                }

                return StringConversionFailed(value);
            }
            else if (valueType == _vector3Type)
            {
                return new ConversionResult(Quaternion.Euler((Vector3)value));
            }

            return ConversionFailed(value);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            Quaternion quaternion = (Quaternion)value;
            Vector3 eulerAngles = quaternion.eulerAngles;
            return String.Format("{0},{1},{2}", eulerAngles.x, eulerAngles.y, eulerAngles.z);
        }

        #endregion
    }
}
