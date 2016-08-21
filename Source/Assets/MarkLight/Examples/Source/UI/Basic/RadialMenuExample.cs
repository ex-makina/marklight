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
    /// Example demonstrating the radial menu.
    /// </summary>
    [HideInPresenter]
    public class RadialMenuExample : UIView
    {
        #region Fields

        public RadialMenu ContextRadialMenu;
        public _string MenuClickText;

        #endregion

        #region Methods

        public override void Initialize()
        {
            base.Initialize();

            MenuClickText.DirectValue = "Click here";
        }

        /// <summary>
        /// Called when the user clicks on the interactable surface.
        /// </summary>
        public void ToggleRadialMenu(PointerEventData eventData)
        {
            ContextRadialMenu.ToggleAt(eventData.position, true);
        }

        /// <summary>
        /// Called when a button in the radial menu is clicked.
        /// </summary>
        public void MenuButtonClicked(Button source)
        {
            ContextRadialMenu.Close(true);

            MenuClickText.Value = source.Text + " Clicked!";
        }

        #endregion
    }
}

