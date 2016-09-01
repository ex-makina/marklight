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
