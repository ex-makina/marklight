#region Using Statements
using MarkLight.Views.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Examples.UI.Basic
{
    /// <summary>
    /// Example demonstrating the button view.
    /// </summary>
    [HideInPresenter]
    public class ButtonsExample : UIView
    {
        #region Fields

        public _int ButtonClickCount;

        #endregion

        #region Methods

        /// <summary>
        /// Called when the button is clicked.
        /// </summary>
        public void ButtonClick()
        {
            ButtonClickCount.Value += 1;
        }

        #endregion
    }
}

