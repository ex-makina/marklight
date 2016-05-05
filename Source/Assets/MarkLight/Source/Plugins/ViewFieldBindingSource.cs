#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// View field binding source.
    /// </summary>
    public class ViewFieldBindingSource : BindingSource
    {
        #region Fields

        public ViewFieldData ViewFieldData;
        public bool NegateValue;
        private static string BoolTypeName = typeof(bool).Name;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewFieldBindingSource(ViewFieldData viewFieldData, bool negateValue = false)
        {
            ViewFieldData = viewFieldData;
            NegateValue = negateValue;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets value from binding source.
        /// </summary>
        public override object GetValue(out bool hasValue)
        {
            var value = ViewFieldData.GetValue(out hasValue);

            // check if value is to be negated
            if (NegateValue && ViewFieldData.ViewFieldTypeName == BoolTypeName)
            {
                value = !((bool)value);
            }

            return value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets binding source string.
        /// </summary>
        public override string BindingSourceString
        {
            get
            {
                return String.Format("{0}.{1}", ViewFieldData.SourceView.ViewTypeName, ViewFieldData.ViewFieldPath);
            }
        }

        #endregion
    }
}
