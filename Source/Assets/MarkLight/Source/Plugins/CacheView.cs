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
    /// Marks Declares a change handler that will be invoked whenever the field changes value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CacheView : Attribute
    {
        #region Fields
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public CacheView()
        {
        }

        #endregion
    }
}
