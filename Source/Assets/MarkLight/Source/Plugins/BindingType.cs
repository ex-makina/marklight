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
    /// Type of binding used.
    /// </summary>
    public enum BindingType
    {
        /// <summary>
        /// Binding to a single view field.
        /// </summary>
        SingleBinding = 0,

        /// <summary>
        /// Binding to multiple view fields combined through a transformation method.
        /// </summary>
        MultiBindingTransform = 1,

        /// <summary>
        /// Binding to a single or multiple view fields through a format string.
        /// </summary>
        MultiBindingFormatString = 2
    }
}
