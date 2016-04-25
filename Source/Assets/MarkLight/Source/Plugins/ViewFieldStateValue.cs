#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Contains data about a view field state value.
    /// </summary>
    [Serializable]
    public class ViewFieldStateValue
    {
        #region Fields

        public string ViewFieldPath;
        public string State;
        public string Value;
        public ValueConverterContext ValueConverterContext;
        public bool DefaultValueNotSet;

        private object _objectValue;

        #endregion

        #region Methods

        /// <summary>
        /// Gets value of state field.
        /// </summary>
        public object GetValue()
        {
            return _objectValue != null ? _objectValue : Value;
        }

        /// <summary>
        /// Sets value of field.
        /// </summary>
        public void SetValue(object value, string stringValue)
        {
            _objectValue = value;
            Value = stringValue;
            DefaultValueNotSet = false;
        }

        /// <summary>
        /// Sets value of field.
        /// </summary>
        public void SetValue(object value)
        {
            _objectValue = value;
        }

        #endregion
    }
}
