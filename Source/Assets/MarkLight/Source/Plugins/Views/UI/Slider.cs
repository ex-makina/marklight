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
    /// Slider view.
    /// </summary>
    /// <d>Slider with a handle that can be moved with the mouse. Can be oriented horizontally or vertically.</d>
    [HideInPresenter]
    public class Slider : UIView
    {
        #region Fields

        #region SliderBackgroundImage

        /// <summary>
        /// Slider image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the slider.</d>
        [MapTo("SliderBackgroundImageView.Sprite")]
        public _Sprite SliderBackgroundImage;

        /// <summary>
        /// Slider image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the slider.</d>
        [MapTo("SliderBackgroundImageView.Type")]
        public _ImageType SliderBackgroundImageType;

        /// <summary>
        /// Slider image material.
        /// </summary>
        /// <d>The material of the slider image.</d>
        [MapTo("SliderBackgroundImageView.Material")]
        public _Material SliderBackgroundMaterial;

        /// <summary>
        /// Slider image color.
        /// </summary>
        /// <d>The color of the slider image.</d>
        [MapTo("SliderBackgroundImageView.Color")]
        public _Color SliderBackgroundColor;

        /// <summary>
        /// Slider background image.
        /// </summary>
        /// <d>Presents the slider background image.</d>
        public Image SliderBackgroundImageView;

        #endregion

        #region SliderFillImage

        /// <summary>
        /// Slider fill image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the slider fill.</d>
        [MapTo("SliderFillImageView.Sprite")]
        public _Sprite SliderFillImage;

        /// <summary>
        /// Slider fill image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the slider fill.</d>
        [MapTo("SliderFillImageView.Type")]
        public _ImageType SliderFillImageType;

        /// <summary>
        /// Slider fill image material.
        /// </summary>
        /// <d>The material of the slider fill image.</d>
        [MapTo("SliderFillImageView.Material")]
        public _Material SliderFillMaterial;

        /// <summary>
        /// Slider fill image color.
        /// </summary>
        /// <d>The color of the slider fill image.</d>
        [MapTo("SliderFillImageView.Color")]
        public _Color SliderFillColor;

        /// <summary>
        /// Slider fill image.
        /// </summary>
        /// <d>Presents the slider fill image.</d>
        public Image SliderFillImageView;

        #endregion

        #region SliderHandleImage

        /// <summary>
        /// Slider handle image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the slider handle.</d>
        [MapTo("SliderHandleImageView.Sprite")]
        public _Sprite SliderHandleImage;

        /// <summary>
        /// Slider handle image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the slider handle.</d>
        [MapTo("SliderHandleImageView.Type")]
        public _ImageType SliderHandleImageType;

        /// <summary>
        /// Slider handle image material.
        /// </summary>
        /// <d>The material of the slider handle image.</d>
        [MapTo("SliderHandleImageView.Material")]
        public _Material SliderHandleMaterial;

        /// <summary>
        /// Slider handle image color.
        /// </summary>
        /// <d>The color of the slider handle image.</d>
        [MapTo("SliderHandleImageView.Color")]
        public _Color SliderHandleColor;

        /// <summary>
        /// Slider handle length.
        /// </summary>
        /// <d>Length of the slider handle.</d>
        [MapTo("SliderHandleImageView.Width")]
        public _ElementSize SliderHandleLength;

        /// <summary>
        /// Slider handle breadth.
        /// </summary>
        /// <d>Breadth of the slider handle.</d>
        [MapTo("SliderHandleImageView.Height")]
        public _ElementSize SliderHandleBreadth;

        /// <summary>
        /// Slider handle image.
        /// </summary>
        /// <d>Presents the slider handle image.</d>
        public Image SliderHandleImageView;

        #endregion

        #region SliderFillRegion

        /// <summary>
        /// Slider fill margin.
        /// </summary>
        /// <d>Margin of the slider fill region.</d>
        [MapTo("SliderFillRegion.Margin")]
        public _ElementMargin SliderFillMargin;

        /// <summary>
        /// Region that contains the fill image.
        /// </summary>
        /// <d>Region that contains the fill image.</d>
        public Region SliderFillRegion;

        #endregion

        /// <summary>
        /// Length of the slider.
        /// </summary>
        /// <d>Specifies the length of the slider. Corresponds to the width or height depending on the orientation of the slider.</d>
        public _ElementSize Length;

        /// <summary>
        /// Breadth of the slider.
        /// </summary>
        /// <d>Specifies the breadth of the slider. Corresponds to the height or width depending on the orientation of the slider.</d>
        public _ElementSize Breadth;

        /// <summary>
        /// Orientation of the slider.
        /// </summary>
        /// <d>Enum specifying the orientation of the slider.</d>
        public _ElementOrientation Orientation;

        /// <summary>
        /// Minimum value.
        /// </summary>
        /// <d>Value of the slider when the handle is at the beginning of the slide area.</d>
        [ChangeHandler("SliderValueChanged")]
        public _float Min;

        /// <summary>
        /// Maximum value.
        /// </summary>
        /// <d>Value of the slider when the handle is at the end of the slide area.</d>
        [ChangeHandler("SliderValueChanged")]
        public _float Max;

        /// <summary>
        /// Current value.
        /// </summary>
        /// <d>Current value of the slider. Calculated from the current handle position and the Min/Max value of the slider.</d>
        [ChangeHandler("SliderValueChanged")]
        public _float Value;

        /// <summary>
        /// Indicates if user can drag the slider handle.
        /// </summary>
        /// <d>Boolean indicating if the user can interact with the slider and drag the handle.</d>
        public _bool CanSlide;

        /// <summary>
        /// Indicates if value set when handle is released.
        /// </summary>
        /// <d>Boolean indicating if the slider value should be set when the user releases the handle instead of continously while dragging.</d>
        public _bool SetValueOnDragEnded;

        /// <summary>
        /// Region containing slider content.
        /// </summary>
        /// <d>Region containing slider background, fill and handle image.</d>
        public Region SliderRegion;

        /// <summary>
        /// Slider value changed.
        /// </summary>
        /// <d>Triggered when the slider value changes. Triggered once when handle is released if SetValueOnDragEnded is set.</d>
        public ViewAction ValueChanged;

        /// <summary>
        /// Slider begin drag.
        /// </summary>
        /// <d>Triggered when the user presses mouse on and starts to drag over the slider.</d>
        public ViewAction BeginDrag;

        /// <summary>
        /// Slider end drag.
        /// </summary>
        /// <d>Triggered when the user stops dragging mouse over the slider.</d>
        public ViewAction EndDrag;

        /// <summary>
        /// Slider drag.
        /// </summary>
        /// <d>Triggered as the user drags the mouse over the slider.</d>
        public ViewAction Drag;

        /// <summary>
        /// Slider initialize potential drag.
        /// </summary>
        /// <d>Triggered as the user initiates a potential drag over the slider.</d>
        public ViewAction InitializePotentialDrag;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Length.DirectValue = new ElementSize(160);
            Breadth.DirectValue = new ElementSize(40);
            Orientation.DirectValue = ElementOrientation.Horizontal;
            Min.DirectValue = 0;
            Max.DirectValue = 100;
            CanSlide.DirectValue = true;
            SetValueOnDragEnded.DirectValue = false;
            SliderFillImageView.Alignment.DirectValue = ElementAlignment.Left;
            SliderHandleImageView.Alignment.DirectValue = ElementAlignment.Left;
            SliderHandleImageView.Width.DirectValue = new ElementSize(20);
            SliderHandleImageView.Height.DirectValue = ElementSize.FromPercents(1);
        }

        /// <summary>
        /// Updates the layout of the view.
        /// </summary>
        public override void LayoutChanged()
        {
            Width.DirectValue = Width.IsSet ? Width.Value : (Orientation == ElementOrientation.Horizontal ? Length.Value : Breadth.Value);
            Height.DirectValue = Height.IsSet ? Height.Value : (Orientation == ElementOrientation.Horizontal ? Breadth.Value : Length.Value);

            base.LayoutChanged();

            // if vertical slider rotate slide region 90 degrees            
            if (Orientation == ElementOrientation.Vertical)
            {
                SliderRegion.Width.DirectValue = new ElementSize(RectTransform.rect.height, ElementSizeUnit.Pixels);
                SliderRegion.Height.DirectValue = new ElementSize(RectTransform.rect.width, ElementSizeUnit.Pixels);
                SliderRegion.Rotation.Value = Quaternion.Euler(new Vector3(0, 0, 90));
                SliderRegion.LayoutChanged();
            }

            // update slider position
            UpdateSliderPosition(Value);
        }

        /// <summary>
        /// Called when the value of the slider changes (or any fields affecting the value).
        /// </summary>
        public virtual void SliderValueChanged()
        {
            // clamp value to min/max
            Value.DirectValue = Value.Value.Clamp(Min, Max);
            UpdateSliderPosition(Value);
        }

        /// <summary>
        /// Called on slider drag begin.
        /// </summary>
        public void SliderBeginDrag(PointerEventData eventData)
        {
            if (!CanSlide)
            {
                return;
            }

            SetSlideTo(eventData.position);
        }

        /// <summary>
        /// Called on slider drag end.
        /// </summary>
        public void SliderEndDrag(PointerEventData eventData)
        {            
            if (!CanSlide)
            {
                return;
            }

            SetSlideTo(eventData.position, true);
        }

        /// <summary>
        /// Called on slider drag.
        /// </summary>
        public void SliderDrag(PointerEventData eventData)
        {
            if (!CanSlide)
            {
                return;
            }

            SetSlideTo(eventData.position);
        }

        /// <summary>
        /// Called on potential drag begin (click).
        /// </summary>
        public void SliderInitializePotentialDrag(PointerEventData eventData)
        {
            if (!CanSlide)
            {
                return;
            }

            SetSlideTo(eventData.position, true);
        }

        /// <summary>
        /// Sets slider value.
        /// </summary>
        public void SlideTo(float value)
        {
            float clampedValue = value.Clamp(Min, Max);
            Value.Value = clampedValue;
        }

        /// <summary>
        /// Slides the slider to the given position.
        /// </summary>
        private void SetSlideTo(Vector2 mouseScreenPositionIn, bool isEndDrag = false)
        {
            var fillTransform = SliderFillRegion.RectTransform;

            var pos = GetLocalPoint(mouseScreenPositionIn);
            
            // calculate slide percentage (transform.position.x/y is center of fill area)
            float p = 0;
            float slideAreaLength = fillTransform.rect.width - SliderHandleImageView.Width.Value.Pixels;
            if (Orientation == ElementOrientation.Horizontal)
            {
                p = ((pos.x - fillTransform.localPosition.x + slideAreaLength / 2f) / slideAreaLength).Clamp(0, 1);
            }
            else
            {
                p = ((pos.y - fillTransform.localPosition.y + slideAreaLength / 2f) / slideAreaLength).Clamp(0, 1);
            }

            // set value
            float newValue = (Max - Min) * p + Min;
            if (!SetValueOnDragEnded || (SetValueOnDragEnded && isEndDrag))
            {
                Value.Value = newValue;
                ValueChanged.Trigger();
            }
            else
            {
                UpdateSliderPosition(newValue);
            }
        }

        /// <summary>
        /// Sets slider position based on value.
        /// </summary>
        private void UpdateSliderPosition(float value)
        {            
            float p = (value - Min) / (Max - Min);
            var fillTransform = SliderFillRegion.RectTransform;

            // set handle offset
            float fillWidth = fillTransform.rect.width;
            float slideAreaWidth = fillWidth - SliderHandleImageView.Width.Value.Pixels;
            float handleOffset = p * slideAreaWidth + SliderFillRegion.Margin.Value.Left.Pixels;

            SliderHandleImageView.OffsetFromParent.DirectValue = ElementMargin.FromLeft(new ElementSize(handleOffset, ElementSizeUnit.Pixels));
            SliderHandleImageView.LayoutChanged();

            // set fill percentage as to match the offset of the handle
            float fillP = (handleOffset + SliderHandleImageView.Width.Value.Pixels / 2f) / fillWidth;
            SliderFillImageView.Width.DirectValue = new ElementSize(fillP, ElementSizeUnit.Percents);
            SliderFillImageView.LayoutChanged();
        }

        #endregion
    }
}
