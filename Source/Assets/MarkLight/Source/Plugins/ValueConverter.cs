#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Value converter for view fields.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ValueConverter : Attribute
    {
        #region Fields

        protected Type _type;
        protected Type _stringType;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ValueConverter()
        {
            _type = typeof(object);
            _stringType = typeof(string);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts value to view field value.
        /// </summary>
        public ConversionResult Convert(object value)
        {
            return Convert(value, ValueConverterContext.Default);
        }

        /// <summary>
        /// Converts XUML attribute to a view value.
        /// </summary>
        public virtual ConversionResult Convert(object value, ValueConverterContext context)
        {
            return new ConversionResult(value);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public virtual string ConvertToString(object value)
        {
            return String.Empty;
        }

        /// <summary>
        /// Gets conversion failed result with formatted error message.
        /// </summary>
        protected ConversionResult ConversionFailed(object value, string reason)
        {
            var result = new ConversionResult();
            result.Success = false;
            result.ErrorMessage = String.Format("{0}: Unable to convert the value \"{1}\" to type: {2}. {3}", GetType().Name, value, _type.Name, reason);
            return result;
        }

        /// <summary>
        /// Gets conversion failed result with formatted error message.
        /// </summary>
        protected ConversionResult ConversionFailed(object value, Exception e)
        {
            var result = new ConversionResult();
            result.Success = false;
            result.ErrorMessage = String.Format("{0}: Unable to convert the value \"{1}\" to type: {2}. Exception thrown: {3}", GetType().Name, value, _type.Name, Utils.GetError(e));
            return result;
        }

        /// <summary>
        /// Gets conversion failed result with formatted error message.
        /// </summary>
        protected ConversionResult ConversionFailed(object value)
        {
            var result = new ConversionResult();
            result.Success = false;
            result.ErrorMessage = String.Format("{0}: Unable to convert the value \"{1}\" to type: {2}. No conversion implemented for value type: {3}.", GetType().Name, value, _type.Name, value != null ? value.GetType().Name : "unknown");
            return result;
        }

        /// <summary>
        /// Gets conversion failed result of improperly formatted string.
        /// </summary>
        protected ConversionResult StringConversionFailed(object value)
        {
            var result = new ConversionResult();
            result.Success = false;
            result.ErrorMessage = String.Format("{0}: Unable to convert the value \"{1}\" to type: {2}. Improperly formatted string.", GetType().Name, value, _type.Name);
            return result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets type of values converted.
        /// </summary>
        public Type Type
        {
            get 
            {
                return _type;
            }
        }

        #endregion
    }
}
