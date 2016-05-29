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
    /// Value observer.
    /// </summary>
    public class ValueObserver
    {
        #region Constructor

        /// <summary>
        /// Initializes static instance of the class.
        /// </summary>
        static ValueObserver()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Notifies the value observer.
        /// </summary>
        public virtual bool Notify(HashSet<ViewFieldData> callstack)
        {
            return true;
        }

        #endregion
    }
}
