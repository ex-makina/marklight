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
    /// Declares a change handler that will be invoked whenever the field changes value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class ChangeHandler : Attribute
    {
        #region Fields

        public string Name;
        public bool TriggerImmediately;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ChangeHandler(string name)
        {
            Name = name;
        }

        #endregion
    }
}
