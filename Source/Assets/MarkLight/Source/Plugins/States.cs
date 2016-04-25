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
    /// Defines the states the view is designed to have.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class States : Attribute
    {
        #region Fields

        public List<string> StateNames;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public States(params string[] states)
        {
            StateNames = new List<string>(states);
        }

        #endregion
    }
}
