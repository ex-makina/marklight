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
    /// RadioButton view.
    /// </summary>
    /// <d>Presents a one-of-many selection option. If multiple radio buttons shares the same parent only one is selected at a time.</d>
    [HideInPresenter]
    public class RadioButton : UIView
    {
        #region Fields

        /// <summary>
        /// Indicates if the radio button is checked.
        /// </summary>
        /// <d>If true the radio button changes state from Default to Checked.</d>
        [ChangeHandler("IsCheckedChanged", TriggerImmediately = true)]
        public _bool IsChecked;

        /// <summary>
        /// Indicates if the radio button is disabled.
        /// </summary>
        /// <d>If true the radio button changes state from Default to Disabled and no longer responds to user interaction.</d>
        [ChangeHandler("IsDisabledChanged")]
        public _bool IsDisabled;

        #region RadioButtonGroup

        /// <summary>
        /// Radio button group spacing.
        /// </summary>
        /// <d>Spacing between the radio button image and text label.</d>
        [MapTo("RadioButtonGroup.Spacing")]
        public _ElementSize Spacing;

        /// <summary>
        /// Group containing the radio button image and label.
        /// </summary>
        /// <d>The the radio button group arranges the radio button image and text label horizontally.</d>
        public Group RadioButtonGroup;

        #endregion

        #region RadioButtonImageView

        /// <summary>
        /// Radio button image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the radio button.</d>
        [MapTo("RadioButtonImageView.Sprite")]
        public _SpriteAsset RadioButtonImage;

        /// <summary>
        /// Radio button image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the radio button.</d>
        [MapTo("RadioButtonImageView.Type")]
        public _ImageType RadioButtonImageType;

        /// <summary>
        /// Radio button image material.
        /// </summary>
        /// <d>The material of the radio button image.</d>
        [MapTo("RadioButtonImageView.Material")]
        public _Material RadioButtonMaterial;

        /// <summary>
        /// Radio button image color.
        /// </summary>
        /// <d>The color of the radio button image.</d>
        [MapTo("RadioButtonImageView.Color")]
        public _Color RadioButtonColor;

        /// <summary>
        /// Radio button image width.
        /// </summary>
        /// <d>Specifies the width of the radio button image either in pixels or percents.</d>
        [MapTo("RadioButtonImageView.Width")]
        public _ElementSize RadioButtonWidth;

        /// <summary>
        /// Radio button image height.
        /// </summary>
        /// <d>Specifies the height of the radio button image either in pixels or percents.</d>
        [MapTo("RadioButtonImageView.Height")]
        public _ElementSize RadioButtonHeight;

        /// <summary>
        /// Radio button image offset.
        /// </summary>
        /// <d>Specifies the offset of the radio button image.</d>
        [MapTo("RadioButtonImageView.Offset")]
        public _ElementSize RadioButtonOffset;

        /// <summary>
        /// Image displaying the radio button.
        /// </summary>
        /// <d>Image view that displays the radio button. Different images are presented when the radio button changes state between Default and Checked.</d>        
        public Image RadioButtonImageView;

        #endregion

        #region RadioButtonLabel

        /// <summary>
        /// Radio button text.
        /// </summary>
        /// <d>The text of the radio button label.</d>
        [MapTo("RadioButtonLabel.Text")]
        public _string Text;

        /// <summary>
        /// Radio button text font.
        /// </summary>
        /// <d>The font of the radio button label.</d>
        [MapTo("RadioButtonLabel.Font")]
        public _Font Font;

        /// <summary>
        /// Radio button text font size.
        /// </summary>
        /// <d>The font size of the radio button label.</d>
        [MapTo("RadioButtonLabel.FontSize")]
        public _int FontSize;

        /// <summary>
        /// Radio button text line spacing.
        /// </summary>
        /// <d>The line spacing of the radio button label.</d>
        [MapTo("RadioButtonLabel.LineSpacing")]
        public _int LineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the radio button label supports rich text.</d>
        [MapTo("RadioButtonLabel.SupportRichText")]
        public _bool SupportRichText;

        /// <summary>
        /// Radio button text font color.
        /// </summary>
        /// <d>The font color of the radio button label.</d>
        [MapTo("RadioButtonLabel.FontColor")]
        public _Color FontColor;

        /// <summary>
        /// Radio button text font style.
        /// </summary>
        /// <d>The font style of the radio button label.</d>
        [MapTo("RadioButtonLabel.FontStyle")]
        public _FontStyle FontStyle;

        /// <summary>
        /// Radio button text margin.
        /// </summary>
        /// <d>The margin of the radio button label. Can be used to adjust the text positioning.</d>
        [MapTo("RadioButtonLabel.Margin")]
        public _ElementMargin TextMargin;

        /// <summary>
        /// Radio button text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the radio button label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("RadioButtonLabel.TextAlignment")]
        public _ElementAlignment TextAlignment;

        /// <summary>
        /// Radio button text offset.
        /// </summary>
        /// <d>The offset of the radio button label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("RadioButtonLabel.Offset")]
        public _ElementMargin TextOffset;

        /// <summary>
        /// Radio button text shadow color.
        /// </summary>
        /// <d>The shadow color of the radio button label.</d>
        [MapTo("RadioButtonLabel.ShadowColor")]
        public _Color ShadowColor;

        /// <summary>
        /// Radio button text shadow distance.
        /// </summary>
        /// <d>The distance of the radio button label shadow.</d>
        [MapTo("RadioButtonLabel.ShadowDistance")]
        public _Vector2 ShadowDistance;

        /// <summary>
        /// Radio button text outline color.
        /// </summary>
        /// <d>The outline color of the radio button label.</d>
        [MapTo("RadioButtonLabel.OutlineColor")]
        public _Color OutlineColor;

        /// <summary>
        /// Radio button text outline distance.
        /// </summary>
        /// <d>The distance of the radio button label outline.</d>
        [MapTo("RadioButtonLabel.OutlineDistance")]
        public _Vector2 OutlineDistance;

        /// <summary>
        /// Adjusts the radio button to the text.
        /// </summary>
        /// <d>An enum indiciating how the radio button should adjust its size to the label text.</d>
        [MapTo("RadioButtonLabel.AdjustToText")]
        public _AdjustToText AdjustToText;

        /// <summary>
        /// The radio button label.
        /// </summary>
        /// <d>The radio button label displays text next to the radio button.</d>
        public Label RadioButtonLabel;

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Height.DirectValue = new ElementSize(40);
            RadioButtonImageView.Width.DirectValue = new ElementSize(40);
            RadioButtonImageView.Height.DirectValue = new ElementSize(40);
        }

        /// <summary>
        /// Called when a child layout has been updated.
        /// </summary>
        public override void ChildLayoutChanged()
        {
            base.ChildLayoutChanged();
            QueueChangeHandler("LayoutChanged");
        }

        /// <summary>
        /// Called when a field affecting the layout of the view has changed.
        /// </summary>
        public override void LayoutChanged()
        {
            // adjust width to RadioButtonGroup
            Width.DirectValue = new ElementSize(RadioButtonGroup.ActualWidth, ElementSizeUnit.Pixels);
            base.LayoutChanged();
        }

        /// <summary>
        /// Called when the field IsChecked is changed.
        /// </summary>
        public virtual void IsCheckedChanged()
        {
            if (IsDisabled)
                return;

            if (IsChecked)
            {
                SetState("Checked");
            }
            else
            {
                SetState(DefaultStateName);
            }
        }

        /// <summary>
        /// Called when IsDisabled field changes.
        /// </summary>
        public virtual void IsDisabledChanged()
        {
            if (IsDisabled)
            {
                SetState("Disabled");

                // disable click actions
                Click.IsDisabled = true;
            }
            else
            {
                SetState(IsChecked ? "Checked" : DefaultStateName);

                // enable button actions
                Click.IsDisabled = false;
            }
        }

        /// <summary>
        /// Called when radio button is clicked.
        /// </summary>
        public void RadioButtonClick()
        {
            if (IsChecked)
                return;

            // deselect all radio buttons
            if (LayoutParent != null)
            {
                LayoutParent.Content.ForEachChild<RadioButton>(x =>
                {
                    if (x.IsChecked)
                    {
                        x.IsChecked.Value = false;
                    }
                }, false);
            }

            // select this radio button
            IsChecked.Value = true;
        }

        #endregion
    }
}
