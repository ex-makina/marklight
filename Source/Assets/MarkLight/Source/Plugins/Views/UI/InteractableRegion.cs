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
    /// Interactable region view.
    /// </summary>
    /// <d>Interactable region used to intercept and handle user interactions such as clicks and drags.</d>
    [HideInPresenter]
    public class InteractableRegion : UIView
    {
        #region Fields

        /// <summary>
        /// Interactable region click.
        /// </summary>
        /// <d>Triggered when the user clicks on the interactable region.</d>
        public ViewAction Click;

        /// <summary>
        /// Interactable region begin drag.
        /// </summary>
        /// <d>Triggered when the user presses mouse on and starts to drag over the interactable region.</d>
        public ViewAction BeginDrag;

        /// <summary>
        /// Interactable region end drag.
        /// </summary>
        /// <d>Triggered when the user stops dragging mouse over the interactable region.</d>
        public ViewAction EndDrag;

        /// <summary>
        /// Interactable region drag.
        /// </summary>
        /// <d>Triggered as the user drags the mouse over the interactable region.</d>
        public ViewAction Drag;

        /// <summary>
        /// Interactable region initialize potential drag.
        /// </summary>
        /// <d>Triggered as the user initiates a potential drag over the interactable region.</d>
        public ViewAction InitializePotentialDrag;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            RaycastBlockMode.DirectValue = MarkLight.RaycastBlockMode.Always;
        }

        #endregion
    }
}
