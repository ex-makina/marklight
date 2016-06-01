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
    /// Mask view.
    /// </summary>
    /// <d>A view that masks any content using the background image and color as mask.</d>
    [HideInPresenter]
    public class Mask : UIView
    {
        #region Fields

        /// <summary>
        /// Indicates if mask should be rendered.
        /// </summary>
        /// <d>Indicates if the mask, i.e. Background image sprite and color should be rendered.</d>
        [MapTo("MaskComponent.showMaskGraphic")]
        public _bool ShowMaskGraphic;

        /// <summary>
        /// A component for masking child elements.
        /// </summary>
        /// <d>The mask components mask the child elements using the BackgroundImage sprite and color.</d>
        public UnityEngine.UI.Mask MaskComponent;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            ImageComponent.color = new Color(1, 1, 1, 0.012f);
            MaskComponent.showMaskGraphic = true;
        }

        /// <summary>
        /// Called when fields affecting the background image/color of the view are changed.
        /// </summary>
        public override void BackgroundChanged()
        {
            base.BackgroundChanged();

            // disable mask if background is transparent
            MaskComponent.enabled = ImageComponent.color.a > 0;
        }

        #endregion
    }
}
