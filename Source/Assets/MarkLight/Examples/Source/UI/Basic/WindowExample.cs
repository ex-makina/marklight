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
    public class WindowExample : UIView
    {
        #region Fields

        public Window Window;

        #endregion

        #region Methods

        /// <summary>
        /// Open/closes the window.
        /// </summary>
        public void ToggleWindow()
        {
            if (Window.IsOpen)
            {
                Window.Close();
            }
            else
            {
                Window.Open();
            }
        }

        #endregion
    }
}

