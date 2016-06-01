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
    /// CheckBox view.
    /// </summary>
    /// <d>A check box consisting of a box that can be ticked and a text label. Has the states: Default, Checked and Disabled.</d>
    [HideInPresenter]
    public class CheckBox : UIView
    {
        #region Fields

        /// <summary>
        /// Indicates if the check box is checked.
        /// </summary>
        /// <d>If true the check box changes state from Default to Checked.</d>
        [ChangeHandler("IsCheckedChanged", TriggerImmediately = true)]
        public _bool IsChecked;

        /// <summary>
        /// Indicates if the check box is disabled.
        /// </summary>
        /// <d>If true the check box changes state from Default to Disabled and no longer responds to user interaction.</d>
        [ChangeHandler("IsDisabledChanged")]
        public _bool IsDisabled;

        #region CheckBoxGroup

        /// <summary>
        /// Check box group spacing.
        /// </summary>
        /// <d>Spacing between the check box image and text label.</d>
        [MapTo("CheckBoxGroup.Spacing")]
        public _ElementSize Spacing;

        /// <summary>
        /// Group containing the check box image and label.
        /// </summary>
        /// <d>The the check box group arranges the check box image and text label horizontally.</d>
        public Group CheckBoxGroup;

        #endregion

        #region CheckBoxImageView

        /// <summary>
        /// Check box image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the check box.</d>
        [MapTo("CheckBoxImageView.Sprite")]
        public _Sprite CheckBoxImage;

        /// <summary>
        /// Check box image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the check box.</d>
        [MapTo("CheckBoxImageView.Type")]
        public _ImageType CheckBoxImageType;

        /// <summary>
        /// Check box image material.
        /// </summary>
        /// <d>The material of the check box image.</d>
        [MapTo("CheckBoxImageView.Material")]
        public _Material CheckBoxMaterial;

        /// <summary>
        /// Check box image color.
        /// </summary>
        /// <d>The color of the check box image.</d>
        [MapTo("CheckBoxImageView.Color")]
        public _Color CheckBoxColor;

        /// <summary>
        /// Check box image width.
        /// </summary>
        /// <d>Specifies the width of the check box image either in pixels or percents.</d>
        [MapTo("CheckBoxImageView.Width")]
        public _ElementSize CheckBoxWidth;

        /// <summary>
        /// Check box image height.
        /// </summary>
        /// <d>Specifies the height of the check box image either in pixels or percents.</d>
        [MapTo("CheckBoxImageView.Height")]
        public _ElementSize CheckBoxHeight;

        /// <summary>
        /// Check box image offset.
        /// </summary>
        /// <d>Specifies the offset of the check box image.</d>
        [MapTo("CheckBoxImageView.Offset")]
        public _ElementSize CheckBoxOffset;

        /// <summary>
        /// Image displaying the check box.
        /// </summary>
        /// <d>Image view that displays the check box. Different images are presented when the check box changes state between Default and Checked.</d>        
        public Image CheckBoxImageView;

        #endregion

        #region CheckBoxLabel

        /// <summary>
        /// Check box text.
        /// </summary>
        /// <d>The text of the check box label.</d>
        [MapTo("CheckBoxLabel.Text")]
        public _string Text;

        /// <summary>
        /// Check box text font.
        /// </summary>
        /// <d>The font of the check box label.</d>
        [MapTo("CheckBoxLabel.Font")]
        public _Font Font;

        /// <summary>
        /// Check box text font size.
        /// </summary>
        /// <d>The font size of the check box label.</d>
        [MapTo("CheckBoxLabel.FontSize")]
        public _int FontSize;

        /// <summary>
        /// Check box text line spacing.
        /// </summary>
        /// <d>The line spacing of the check box label.</d>
        [MapTo("CheckBoxLabel.LineSpacing")]
        public _int LineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the check box label supports rich text.</d>
        [MapTo("CheckBoxLabel.SupportRichText")]
        public _bool SupportRichText;

        /// <summary>
        /// Check box text font color.
        /// </summary>
        /// <d>The font color of the check box label.</d>
        [MapTo("CheckBoxLabel.FontColor")]
        public _Color FontColor;

        /// <summary>
        /// Check box text font style.
        /// </summary>
        /// <d>The font style of the check box label.</d>
        [MapTo("CheckBoxLabel.FontStyle")]
        public _FontStyle FontStyle;

        /// <summary>
        /// Check box text margin.
        /// </summary>
        /// <d>The margin of the check box label. Can be used to adjust the text positioning.</d>
        [MapTo("CheckBoxLabel.Margin")]
        public _ElementMargin TextMargin;

        /// <summary>
        /// Check box text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the check box label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("CheckBoxLabel.TextAlignment")]
        public _ElementAlignment TextAlignment;

        /// <summary>
        /// Check box text offset.
        /// </summary>
        /// <d>The offset of the check box label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("CheckBoxLabel.Offset")]
        public _ElementMargin TextOffset;

        /// <summary>
        /// Check box text shadow color.
        /// </summary>
        /// <d>The shadow color of the check box label.</d>
        [MapTo("CheckBoxLabel.ShadowColor")]
        public _Color ShadowColor;

        /// <summary>
        /// Check box text shadow distance.
        /// </summary>
        /// <d>The distance of the check box label shadow.</d>
        [MapTo("CheckBoxLabel.ShadowDistance")]
        public _Vector2 ShadowDistance;

        /// <summary>
        /// Check box text outline color.
        /// </summary>
        /// <d>The outline color of the check box label.</d>
        [MapTo("CheckBoxLabel.OutlineColor")]
        public _Color OutlineColor;

        /// <summary>
        /// Check box text outline distance.
        /// </summary>
        /// <d>The distance of the check box label outline.</d>
        [MapTo("CheckBoxLabel.OutlineDistance")]
        public _Vector2 OutlineDistance;

        /// <summary>
        /// Adjusts the check box to the text.
        /// </summary>
        /// <d>An enum indiciating how the check box should adjust its size to the label text. By default the check box does not adjust its size to the text. Is used in conjunction with the TextPadding field to get the desired size of the check box in relation to its text.</d>
        [MapTo("CheckBoxLabel.AdjustToText")]
        public _AdjustToText AdjustToText;

        /// <summary>
        /// The check box label.
        /// </summary>
        /// <d>The check box label displays text next to the check box.</d>
        public Label CheckBoxLabel;

        #endregion

        /// <summary>
        /// Check box click action.
        /// </summary>
        /// <d>The check box click action is triggered when the user clicks on the check box image or text label.</d>
        public ViewAction Click;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Height.DirectValue = new ElementSize(40);
            CheckBoxImageView.Width.DirectValue = new ElementSize(40);
            CheckBoxImageView.Height.DirectValue = new ElementSize(40);
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
            // adjust width to CheckBoxGroup
            Width.DirectValue = new ElementSize(CheckBoxGroup.ActualWidth, ElementSizeUnit.Pixels);
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
        /// Called when check box is clicked.
        /// </summary>
        public void CheckBoxClick()
        {
            IsChecked.Value = !IsChecked;
        }

        #endregion
    }
}
