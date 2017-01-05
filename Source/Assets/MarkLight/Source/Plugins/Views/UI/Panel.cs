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
    /// Panel view.
    /// </summary>
    /// <d>Presents scrollable content. Content can be aligned using the ContentAlignment field.</d>
    [MapViewField("Drag", "ScrollRect.Drag")]
    [MapViewField("InitializePotentialDrag", "ScrollRect.InitializePotentialDrag")]
    [MapViewField("BeginDrag", "ScrollRect.BeginDrag")]
    [MapViewField("EndDrag", "ScrollRect.EndDrag")]
    [HideInPresenter]
    public class Panel : UIView
    {
        #region Fields

        #region HorizontalScrollbar

        /// <summary>
        /// Orientation of the horizontal scrollbar.
        /// </summary>
        /// <d>Orientation of the horizontal scrollbar.</d>
        [MapTo("HorizontalScrollbar.Orientation")]
        public _ElementOrientation HorizontalScrollbarOrientation;

        /// <summary>
        /// Breadth of the horizontal scrollbar.
        /// </summary>
        /// <d>Breadth of the horizontal scrollbar.</d>
        [MapTo("HorizontalScrollbar.Breadth")]
        public _ElementSize HorizontalScrollbarBreadth;

        /// <summary>
        /// Scrollbar scroll direction.
        /// </summary>
        /// <d>Scrollbar scroll direction.</d>
        [MapTo("HorizontalScrollbar.ScrollDirection")]
        public _ScrollbarDirection HorizontalScrollbarScrollDirection;

        /// <summary>
        /// Scroll steps.
        /// </summary>
        /// <d>The number of steps to use for the value. A value of 0 disables use of steps.</d>
        [MapTo("HorizontalScrollbar.NumberOfSteps")]
        public _int HorizontalScrollbarNumberOfSteps;

        /// <summary>
        /// Handle size.
        /// </summary>
        /// <d> The size of the horizontal scrollbar handle where 1 means it fills the entire horizontal scrollbar.</d>
        [MapTo("HorizontalScrollbar.HandleSize")]
        public _float HorizontalScrollbarHandleSize;

        /// <summary>
        /// Scrollbar value.
        /// </summary>
        /// <d>The current value of the horizontal scrollbar, between 0 and 1.</d>
        [MapTo("HorizontalScrollbar.Value")]
        public _float HorizontalScrollbarValue;

        /// <summary>
        /// Horizontal scrollbar image.
        /// </summary>
        /// <d>Horizontal scrollbar image sprite.</d>
        [MapTo("HorizontalScrollbar.BackgroundImage")]
        public _SpriteAsset HorizontalScrollbarImage;

        /// <summary>
        /// Horizontal scrollbar image type.
        /// </summary>
        /// <d>Horizontal scrollbar image sprite type.</d>
        [MapTo("HorizontalScrollbar.BackgroundImageType")]
        public _ImageType HorizontalScrollbarImageType;

        /// <summary>
        /// Horizontal scrollbar image material.
        /// </summary>
        /// <d>Horizontal scrollbar image material.</d>
        [MapTo("HorizontalScrollbar.BackgroundMaterial")]
        public _Material HorizontalScrollbarMaterial;

        /// <summary>
        /// Horizontal scrollbar image color.
        /// </summary>
        /// <d>Horizontal scrollbar image color.</d>
        [MapTo("HorizontalScrollbar.BackgroundColor")]
        public _Color HorizontalScrollbarColor;

        /// <summary>
        /// Horizontal scrollbar handle image.
        /// </summary>
        /// <d>Horizontal scrollbar handle image sprite.</d>
        [MapTo("HorizontalScrollbar.HandleImage")]
        public _SpriteAsset HorizontalScrollbarHandleImage;

        /// <summary>
        /// Horizontal scrollbar handle image type.
        /// </summary>
        /// <d>Horizontal scrollbar handle image sprite type.</d>
        [MapTo("HorizontalScrollbar.HandleImageType")]
        public _ImageType HorizontalScrollbarHandleImageType;

        /// <summary>
        /// Horizontal scrollbar handle image material.
        /// </summary>
        /// <d>Horizontal scrollbar handle image material.</d>
        [MapTo("HorizontalScrollbar.HandleMaterial")]
        public _Material HorizontalScrollbarHandleMaterial;

        /// <summary>
        /// Horizontal scrollbar handle image color.
        /// </summary>
        /// <d>Horizontal scrollbar handle image color.</d>
        [MapTo("HorizontalScrollbar.HandleColor")]
        public _Color HorizontalScrollbarHandleColor;

        /// <summary>
        /// Horizontal scrollbar.
        /// </summary>
        /// <d>Horizontal scrollbar.</d>
        public Scrollbar HorizontalScrollbar;

        #endregion

        #region VerticalScrollbar

        /// <summary>
        /// Orientation of the vertical scrollbar.
        /// </summary>
        /// <d>Orientation of the vertical scrollbar.</d>
        [MapTo("VerticalScrollbar.Orientation")]
        public _ElementOrientation VerticalScrollbarOrientation;

        /// <summary>
        /// Breadth of the vertical scrollbar.
        /// </summary>
        /// <d>Breadth of the vertical scrollbar.</d>
        [MapTo("VerticalScrollbar.Breadth")]
        public _ElementSize VerticalScrollbarBreadth;

        /// <summary>
        /// Scrollbar scroll direction.
        /// </summary>
        /// <d>Scrollbar scroll direction.</d>
        [MapTo("VerticalScrollbar.ScrollDirection")]
        public _ScrollbarDirection VerticalScrollbarScrollDirection;

        /// <summary>
        /// Scroll steps.
        /// </summary>
        /// <d>The number of steps to use for the value. A value of 0 disables use of steps.</d>
        [MapTo("VerticalScrollbar.NumberOfSteps")]
        public _int VerticalScrollbarNumberOfSteps;

        /// <summary>
        /// Vertical scrollbar handle size.
        /// </summary>
        /// <d> The size of the vertical scrollbar handle where 1 means it fills the entire vertical scrollbar.</d>
        [MapTo("VerticalScrollbar.HandleSize")]
        public _float VerticalScrollbarHandleSize;

        /// <summary>
        /// Scrollbar value.
        /// </summary>
        /// <d>The current value of the vertical scrollbar, between 0 and 1.</d>
        [MapTo("VerticalScrollbar.Value")]
        public _float VerticalScrollbarValue;

        /// <summary>
        /// Vertical scrollbar image.
        /// </summary>
        /// <d>Vertical scrollbar image sprite.</d>
        [MapTo("VerticalScrollbar.BackgroundImage")]
        public _SpriteAsset VerticalScrollbarImage;

        /// <summary>
        /// Vertical scrollbar image type.
        /// </summary>
        /// <d>Vertical scrollbar image sprite type.</d>
        [MapTo("VerticalScrollbar.BackgroundImageType")]
        public _ImageType VerticalScrollbarImageType;

        /// <summary>
        /// Vertical scrollbar image material.
        /// </summary>
        /// <d>Vertical scrollbar image material.</d>
        [MapTo("VerticalScrollbar.BackgroundMaterial")]
        public _Material VerticalScrollbarMaterial;

        /// <summary>
        /// Vertical scrollbar image color.
        /// </summary>
        /// <d>Vertical scrollbar image color.</d>
        [MapTo("VerticalScrollbar.BackgroundColor")]
        public _Color VerticalScrollbarColor;

        /// <summary>
        /// Vertical scrollbar handle image.
        /// </summary>
        /// <d>Vertical scrollbar handle image sprite.</d>
        [MapTo("VerticalScrollbar.HandleImage")]
        public _SpriteAsset VerticalScrollbarHandleImage;

        /// <summary>
        /// Vertical scrollbar handle image type.
        /// </summary>
        /// <d>Vertical scrollbar handle image sprite type.</d>
        [MapTo("VerticalScrollbar.HandleImageType")]
        public _ImageType VerticalScrollbarHandleImageType;

        /// <summary>
        /// Vertical scrollbar handle image material.
        /// </summary>
        /// <d>Vertical scrollbar handle image material.</d>
        [MapTo("VerticalScrollbar.HandleMaterial")]
        public _Material VerticalScrollbarHandleMaterial;

        /// <summary>
        /// Vertical scrollbar handle image color.
        /// </summary>
        /// <d>Vertical scrollbar handle image color.</d>
        [MapTo("VerticalScrollbar.HandleColor")]
        public _Color VerticalScrollbarHandleColor;

        /// <summary>
        /// Vertical scrollbar.
        /// </summary>
        /// <d>Vertical scrollbar.</d>
        public Scrollbar VerticalScrollbar;

        #endregion

        #region ScrollRect

        /// <summary>
        /// Indicates if the content can scroll horizontally.
        /// </summary>
        /// <d>Boolean indicating if the content can be scrolled horizontally.</d>
        [MapTo("ScrollRect.CanScrollHorizontally")]
        public _bool CanScrollHorizontally;

        /// <summary>
        /// Indicates if the content can scroll vertically.
        /// </summary>
        /// <d>Boolean indicating if the content can be scrolled vertically.</d>
        [MapTo("ScrollRect.CanScrollVertically")]
        public _bool CanScrollVertically;

        /// <summary>
        /// Scroll deceleration rate.
        /// </summary>
        /// <d>Value indicating the rate of which the scroll stops moving.</d>
        [MapTo("ScrollRect.DecelerationRate")]
        public _float DecelerationRate;

        /// <summary>
        /// Scroll elasticity.
        /// </summary>
        /// <d>Value indicating how elastic the scrolling is when moved beyond the bounds of the scrollable content.</d>
        [MapTo("ScrollRect.Elasticity")]
        public _float Elasticity;

        /// <summary>
        /// Horizontal normalized position.
        /// </summary>
        /// <d>Value between 0-1 indicating the position of the scrollable content.</d>
        [MapTo("ScrollRect.HorizontalNormalizedPosition")]
        public _float HorizontalNormalizedPosition;

        /// <summary>
        /// Space between scrollbar and scrollable content.
        /// </summary>
        /// <d>Space between scrollbar and scrollable content.</d>
        [MapTo("ScrollRect.HorizontalScrollbarSpacing")]
        public _float HorizontalScrollbarSpacing;

        /// <summary>
        /// Indicates if scroll has intertia.
        /// </summary>
        /// <d>Boolean indicating if the scroll has inertia.</d>
        [MapTo("ScrollRect.HasInertia")]
        public _bool HasInertia;

        /// <summary>
        /// Behavior when scrolled beyond bounds.
        /// </summary>
        /// <d>Enum specifying the behavior to use when the content moves beyond the scroll rect.</d>
        [MapTo("ScrollRect.MovementType")]
        public _ScrollRectMovementType MovementType;

        /// <summary>
        /// Normalized position of the scroll.
        /// </summary>
        /// <d>The scroll position as a Vector2 between (0,0) and (1,1) with (0,0) being the lower left corner.</d>
        [MapTo("ScrollRect.NormalizedPosition")]
        public _Vector2 NormalizedPosition;

        /// <summary>
        /// Scroll sensitivity.
        /// </summary>
        /// <d>Value indicating how sensitive the scrolling is to scroll wheel and track pad movement.</d>
        [MapTo("ScrollRect.ScrollSensitivity")]
        public _float ScrollSensitivity;

        /// <summary>
        /// Current velocity of the content.
        /// </summary>
        /// <d>Indicates the current velocity of the scrolled content.</d>
        [MapTo("ScrollRect.ScrollVelocity")]
        public _Vector2 ScrollVelocity;

        /// <summary>
        /// Vertical normalized position.
        /// </summary>
        /// <d>Value between 0-1 indicating the position of the scrollable content.</d>
        [MapTo("ScrollRect.VerticalNormalizedPosition")]
        public _float VerticalNormalizedPosition;

        /// <summary>
        /// Space between scrollbar and scrollable content.
        /// </summary>
        /// <d>Space between scrollbar and scrollable content.</d>
        [MapTo("ScrollRect.VerticalScrollbarSpacing")]
        public _float VerticalScrollbarSpacing;

        /// <summary>
        /// Scroll delta distance for disabling interaction.
        /// </summary>
        /// <d>If set any interaction with child views (clicks, etc) is disabled when the specified distance has been scrolled. This is used e.g. to disable clicks while scrolling a selectable list of items.</d>
        [MapTo("ScrollRect.DisableInteractionScrollDelta")]
        public _float DisableInteractionScrollDelta;

        /// <summary>
        /// Scrollable viewport.
        /// </summary>
        /// <d>References the RectTransform parent to the content.</d>
        [MapTo("ScrollRect.Viewport")]
        public _RectTransformComponent Viewport;

        /// <summary>
        /// Panel content alignment.
        /// </summary>
        /// <d>Panel content alignment. Also controls the initial position of the scrollbars.</d>
        [MapTo("ScrollRect.ContentAlignment")]
        public _ElementAlignment ContentAlignment;

        /// <summary>
        /// Scrollable content.
        /// </summary>
        /// <d>Contains the panel's scrollable content.</d>
        public ScrollRect ScrollRect;

        #endregion

        /// <summary>
        /// Indicates how the horizontal scrollbar should be shown.
        /// </summary>
        /// <d>Enum indicating how the horizontal scrollbar should be shown.</d>
        [ChangeHandler("ScrollbarVisibilityChanged")]
        public _PanelScrollbarVisibility HorizontalScrollbarVisibility;

        /// <summary>
        /// Indicates how the vertical scrollbar should be shown.
        /// </summary>
        /// <d>Enum indicating how the vertical scrollbar should be shown.</d>
        [ChangeHandler("ScrollbarVisibilityChanged")]
        public _PanelScrollbarVisibility VerticalScrollbarVisibility;

        /// <summary>
        /// Indicates if mask margin should be added.
        /// </summary>
        /// <d>Boolean indicating if margin should be added to the content mask to make room for the scrollbars.</d>
        public _bool AddMaskMargin;

        /// <summary>
        /// Mask containing the panel content.
        /// </summary>
        /// <d>Mask containing the panel content but not the scrollbars.</d>
        public Mask ScrollbarMask;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            ScrollRect.ScrollRectComponent.vertical = true;
            ScrollRect.ScrollRectComponent.horizontal = true;
            AddMaskMargin.DirectValue = true;
        }

        /// <summary>
        /// Called when the behavior of the view has been changed.
        /// </summary>
        public virtual void ScrollbarVisibilityChanged()
        {
            // should horizontal scrollbar be destroyed?
            if (HorizontalScrollbar != null && HorizontalScrollbarVisibility.Value == PanelScrollbarVisibility.Remove)
            {
                // yes.
                HorizontalScrollbar.Destroy();
                HorizontalScrollbar = null;
            }

            // should vertical scrollbar be destroyed?
            if (VerticalScrollbar != null && VerticalScrollbarVisibility.Value == PanelScrollbarVisibility.Remove)
            {
                // yes.
                VerticalScrollbar.Destroy();
                VerticalScrollbar = null;
            }

            bool horizontalShown = HorizontalScrollbarVisibility.Value != PanelScrollbarVisibility.Hidden && HorizontalScrollbar != null;
            bool verticalShown = VerticalScrollbarVisibility.Value != PanelScrollbarVisibility.Hidden && VerticalScrollbar != null;
            float horizontalScrollbarBreadth = horizontalShown ? HorizontalScrollbar.Breadth.Value.Pixels : 0;
            float verticalScrollbarBreadth = verticalShown ? VerticalScrollbar.Breadth.Value.Pixels : 0;

            // set margins of the mask and scrollbars based on the breadth of scrollbars
            if (AddMaskMargin)
            {
                ScrollbarMask.Margin.Value = new ElementMargin(0, 0, verticalScrollbarBreadth, horizontalScrollbarBreadth);
            }

            if (HorizontalScrollbar != null)
            {
                HorizontalScrollbar.Margin.Value = new ElementMargin(0, 0, verticalScrollbarBreadth, 0);
                HorizontalScrollbar.IsActive.Value = horizontalShown;
#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
                ScrollRect.HorizontalScrollbarVisibility.Value = HorizontalScrollbarVisibility.Value.ToScrollRectVisibility();
#endif
            }

            if (VerticalScrollbar != null)
            {
                VerticalScrollbar.Margin.Value = new ElementMargin(0, 0, 0, horizontalScrollbarBreadth);
                VerticalScrollbar.IsActive.Value = verticalShown;
#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
                ScrollRect.VerticalScrollbarVisibility.Value = VerticalScrollbarVisibility.Value.ToScrollRectVisibility();
#endif
            }
            
            ScrollRect.HorizontalScrollbar.Value = horizontalShown ? HorizontalScrollbar.ScrollbarComponent : null;
            ScrollRect.VerticalScrollbar.Value = verticalShown ? VerticalScrollbar.ScrollbarComponent : null;
        }
        
        #endregion
    }
}
