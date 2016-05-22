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
    /// Panel scrollbar visibility mode.
    /// </summary>
    public enum PanelScrollbarVisibility
    {
        /// <summary>
        /// Always show the scrollbar.
        /// </summary>
        Permanent = 0,

        /// <summary>
        /// Same as Permanent - shows the scrollbar
        /// </summary>
        Visible = 0,

        /// <summary>
        /// Show the scrollbar if the content exceeds the bounds.
        /// </summary>
        AutoHide = 1,

        /// <summary>
        /// Expands the viewport when the scrollbar is hidden to take up the space of the scrollbar.
        /// </summary>
        AutoHideAndExpandViewport = 2,

        /// <summary>
        /// Hide the scrollbar until visibility mode is manually changed.
        /// </summary>
        Hidden = 3,

        /// <summary>
        /// Removes the scrollbar from the hierarchy, used when the scrollbar isn't to be used at any point.
        /// </summary>
        Remove = 4
    }
}
