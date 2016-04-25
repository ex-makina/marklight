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
    /// Binding source.
    /// </summary>
    public class BindingSource
    {
        #region Fields

        public ViewFieldData ViewFieldData;
        public bool NegateValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public BindingSource(ViewFieldData viewFieldData, bool negateValue = false)
        {
            ViewFieldData = viewFieldData;
            NegateValue = negateValue;
        }

        #endregion
    }
}
