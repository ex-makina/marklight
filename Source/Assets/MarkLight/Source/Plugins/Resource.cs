#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// A resource that is defined in a resource dictionary and can be bound to in XUML.
    /// </summary>
    [Serializable]
    public class Resource
    {
        #region Fields

        public string Key;
        public string Value;
        public string Language;
        public string Platform;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Resource()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Compares two resources.
        /// </summary>
        public bool IsEqual(Resource resource)
        {
            if (resource == null)
            {
                return false;
            }

            return (Key == resource.Key && Language == resource.Language && Platform == resource.Platform);
        }

        #endregion
    }
}
