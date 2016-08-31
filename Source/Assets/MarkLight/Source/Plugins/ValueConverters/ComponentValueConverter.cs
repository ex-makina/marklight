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
    /// Value converter for component types.
    /// </summary>
    public class ComponentValueConverter : ValueConverter
    {
        #region Fields 

        private Type _componentType;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ComponentValueConverter()
        {
            _type = typeof(Component);
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ComponentValueConverter(Type componentType)
        {
            _type = typeof(Component);
            _componentType = componentType;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for String type.
        /// </summary>
        public override ConversionResult Convert(object value, ValueConverterContext context)
        {
            if (value == null)
            {
                return base.Convert(value, context);
            }

            Type valueType = value.GetType();
            if (valueType.IsSubclassOf(_type))
            {
                return base.Convert(value, context);
            }
            else if (valueType == _stringType)
            {
                var stringValue = (string)value;
                try
                {
                    var go = UnityEngine.GameObject.Find(stringValue);
                    var component = go.GetComponent(_componentType);
                    if (component == null)
                    {
                        return ConversionFailed(value);
                    }

                    return new ConversionResult(component);
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
            Component component = value as Component;
            return component.gameObject.name;
        }

        #endregion
    }
}
