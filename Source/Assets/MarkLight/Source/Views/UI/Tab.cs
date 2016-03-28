#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Tab view.
    /// </summary>
    /// <d>Represents a tab in the tab panel.</d>
    [HideInPresenter]
    public class Tab : UIView
    {
        #region Fields

        /// <summary>
        /// Indicates if tab is selected.
        /// </summary>
        /// <d>Boolean indicating if the tab is selected.</d>
        public _bool IsSelected;

        /// <summary>
        /// Tab header text.
        /// </summary>
        /// <d>Tab header text.</d>
        public _string Text;

        #endregion
    }
}
