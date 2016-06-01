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
    /// Button view.
    /// </summary>
    /// <d>The button view is a clickable region with text. Has the states: Default, Highlighted, Pressed and Disabled. The button can be set to toggle through IsToggleButton and to adjust its size to its text through the AdjustToText field.</d>
    [HideInPresenter]
    public class Button : UIView
    {
        #region Fields

        #region ButtonLabel

        /// <summary>
        /// The button label.
        /// </summary>
        /// <d>The button label arranges and displays the button text.</d>
        public Label ButtonLabel;

        /// <summary>
        /// Button text.
        /// </summary>
        /// <d>The text of the button label. The button can be set to adjust its size to the text through the AdjustToText field.</d>
        [MapTo("ButtonLabel.Text", "TextChanged")]
        public _string Text;

        /// <summary>
        /// Button text font.
        /// </summary>
        /// <d>The font of the button label text.</d>
        [MapTo("ButtonLabel.Font")]
        public _Font Font;

        /// <summary>
        /// Button text font size.
        /// </summary>
        /// <d>The size of the button label text.</d>
        [MapTo("ButtonLabel.FontSize")]
        public _int FontSize;

        /// <summary>
        /// Button text line spacing.
        /// </summary>
        /// <d>The line spacing of the button label text.</d>
        [MapTo("ButtonLabel.LineSpacing")]
        public _int LineSpacing;

        /// <summary>
        /// Support rich text.
        /// </summary>
        /// <d>Boolean indicating if the button label supports rich text.</d>
        [MapTo("ButtonLabel.SupportRichText")]
        public _bool SupportRichText;

        /// <summary>
        /// Button text font color.
        /// </summary>
        /// <d>The font color of the button label text.</d>
        [MapTo("ButtonLabel.FontColor")]
        public _Color FontColor;

        /// <summary>
        /// Button text font style.
        /// </summary>
        /// <d>The font style of the button label text.</d>
        [MapTo("ButtonLabel.FontStyle")]
        public _FontStyle FontStyle;

        /// <summary>
        /// Button text margin.
        /// </summary>
        /// <d>The margin of the button label text. Can be used to adjust the text positioning. If AdjustToText is used the TextPadding field is used to add padding.</d>
        [MapTo("ButtonLabel.Margin")]
        public _ElementMargin TextMargin;

        /// <summary>
        /// Button text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the button label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("ButtonLabel.TextAlignment")]
        public _ElementAlignment TextAlignment;

        /// <summary>
        /// Button text offset.
        /// </summary>
        /// <d>The offset of the button label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("ButtonLabel.Offset")]
        public _ElementMargin TextOffset;

        /// <summary>
        /// Button text shadow color.
        /// </summary>
        /// <d>The shadow color of the button label text.</d>
        [MapTo("ButtonLabel.ShadowColor")]
        public _Color ShadowColor;

        /// <summary>
        /// Button text shadow distance.
        /// </summary>
        /// <d>The distance of the button label text shadow.</d>
        [MapTo("ButtonLabel.ShadowDistance")]
        public _Vector2 ShadowDistance;

        /// <summary>
        /// Button text outline color.
        /// </summary>
        /// <d>The outline color of the button label text.</d>
        [MapTo("ButtonLabel.OutlineColor")]
        public _Color OutlineColor;

        /// <summary>
        /// Button text outline distance.
        /// </summary>
        /// <d>The distance of the button label text outline.</d>
        [MapTo("ButtonLabel.OutlineDistance")]
        public _Vector2 OutlineDistance;

        /// <summary>
        /// Adjust button to text.
        /// </summary>
        /// <d>An enum indiciating how the button should adjust its size to the label text. By default the button does not adjust its size to the text. Is used in conjunction with the TextPadding field to get the desired size of the button in relation to its text.</d>
        [MapTo("ButtonLabel.AdjustToText")]
        public _AdjustToText AdjustToText;

        #endregion

        /// <summary>
        /// Indicates if this button is set to toggle.
        /// </summary>
        /// <d>If IsToggleButton is set to true the button will toggle between pressed and unpressed (default) state when clicked.</d>
        public _bool IsToggleButton;

        /// <summary>
        /// Indicates if the button is disabled.
        /// </summary>
        /// <d>If set to true the button enters the "Disabled" state and can't be interacted with.</d>
        [ChangeHandler("IsDisabledChanged")]
        public _bool IsDisabled;

        /// <summary>
        /// Button toggle value.
        /// </summary>
        /// <d>If the button is a toggle button (IsToggleButton is set to true) the toggle value indicates the toggle state of the button. If pressed the ToggleValue is true.</d>
        [ChangeHandler("ToggleValueChanged")]
        public _bool ToggleValue;        

        /// <summary>
        /// Button text padding.
        /// </summary>
        /// <d>The button TextPadding is used when AdjustToText is set. It determines the additional padding to be added to the size of the button when it adjusts to the text.</d>
        [ChangeHandler("TextChanged")]
        public _ElementMargin TextPadding;

        /// <summary>
        /// Indicates if user can toggle on the button.
        /// </summary>
        /// <d>Boolean indicating if the button can be toggled on by user interaction. If set to false the button can only be toggled on programmatically.</d>
        public _bool CanToggleOn;

        /// <summary>
        /// Indicates if user can toggle off the button.
        /// </summary>
        /// <d>Boolean indicating if the button can be toggled off by user interaction. If set to false the button can only be toggled on programmatically.</d>
        public _bool CanToggleOff;

        /// <summary>
        /// Button click action.
        /// </summary>
        /// <d>The button click action is triggered when the user clicks on the button.</d>
        public ViewAction Click;

        /// <summary>
        /// Button mouse enter action.
        /// </summary>
        /// <d>The button mouse enter action is triggered when the mouse enters the button.</d>
        public ViewAction MouseEnter;

        /// <summary>
        /// Button mouse exit action.
        /// </summary>
        /// <d>The button mouse exit action is triggered when the mouse exits the button.</d>
        public ViewAction MouseExit;

        /// <summary>
        /// Button mouse down action.
        /// </summary>
        /// <d>The button mouse down action is triggered when the mouse is pressed over the button.</d>
        public ViewAction MouseDown;

        /// <summary>
        /// Button mouse down action.
        /// </summary>
        /// <d>The button mouse up action is triggered when the mouse is pressed and then released over the button.</d>
        public ViewAction MouseUp;

        /// <summary>
        /// Indicates if the button is pressed.
        /// </summary>
        [NotSetFromXuml]
        public bool IsPressed;

        /// <summary>
        /// Indicates if the mouse is over the button.
        /// </summary>
        [NotSetFromXuml]
        public bool IsMouseOver;

        #endregion

        #region Methods

        /// <summary>
        /// Called when the button text has been changed.
        /// </summary>
        public virtual void TextChanged()
        {
            if (AdjustToText == MarkLight.AdjustToText.None)
                return;

            // adjust button size to text
            if (AdjustToText == MarkLight.AdjustToText.Width)
            {
                Width.DirectValue = new ElementSize(ButtonLabel.TextComponent.preferredWidth + TextPadding.Value.Left.Pixels + TextPadding.Value.Right.Pixels);
            }
            else if (AdjustToText == MarkLight.AdjustToText.Height)
            {
                Height.DirectValue = new ElementSize(ButtonLabel.TextComponent.preferredHeight + TextPadding.Value.Top.Pixels + TextPadding.Value.Bottom.Pixels);
            }
            else if (AdjustToText == MarkLight.AdjustToText.WidthAndHeight)
            {
                Width.DirectValue = new ElementSize(ButtonLabel.TextComponent.preferredWidth + TextPadding.Value.Left.Pixels + TextPadding.Value.Right.Pixels);
                Height.DirectValue = new ElementSize(ButtonLabel.TextComponent.preferredHeight + TextPadding.Value.Top.Pixels + TextPadding.Value.Bottom.Pixels);
            }

            LayoutsChanged();
        }

        /// <summary>
        /// Called when IsDisabled field changes.
        /// </summary>
        public virtual void IsDisabledChanged()
        {            
            if (IsDisabled)
            {
                SetState("Disabled");

                // disable button actions
                Click.IsDisabled = true;
                MouseEnter.IsDisabled = true;
                MouseExit.IsDisabled = true;
                MouseDown.IsDisabled = true;
                MouseUp.IsDisabled = true;
            }
            else
            {
                SetState(IsToggleButton && ToggleValue ? "Pressed" : DefaultStateName);

                // enable button actions
                Click.IsDisabled = false;
                MouseEnter.IsDisabled = false;
                MouseExit.IsDisabled = false;
                MouseDown.IsDisabled = false;
                MouseUp.IsDisabled = false;
            }
        }

        /// <summary>
        /// Called when ToggleValue field changes.
        /// </summary>
        public virtual void ToggleValueChanged()
        {
            // toggle state
            if (IsToggleButton)
            {
                if (ToggleValue)
                {
                    SetState("Pressed");
                }
                else
                {
                    SetState(DefaultStateName);
                }
            }
        }

        /// <summary>
        /// Called when mouse is clicked.
        /// </summary>
        public void ButtonMouseClick()
        {
            // if toggle-button change state
            if (IsToggleButton)
            {
                if (ToggleValue == true && !CanToggleOff)
                    return;
                if (ToggleValue == false && !CanToggleOn)
                    return;

                ToggleValue.Value = !ToggleValue;
            }
        }

        /// <summary>
        /// Called when mouse enters.
        /// </summary>
        public void ButtonMouseEnter()
        {
            IsMouseOver = true;
            if (TogglePressed)
                return;

            if (IsPressed)
            {
                SetState("Pressed");
            }
            else
            {
                SetState("Highlighted");
            }            
        }

        /// <summary>
        /// Called when mouse exits.
        /// </summary>
        public void ButtonMouseExit()
        {
            IsMouseOver = false;
            if (TogglePressed)
                return;

            SetState(DefaultStateName);
        }

        /// <summary>
        /// Called when mouse down.
        /// </summary>
        public void ButtonMouseDown()
        {
            IsPressed = true;
            if (TogglePressed)
                return;

            SetState("Pressed");
        }

        /// <summary>
        /// Called when mouse up.
        /// </summary>
        public void ButtonMouseUp()
        {
            IsPressed = false;
            if (TogglePressed)
                return;

            if (IsMouseOver)
            {
                SetState("Highlighted");                
            }
            else
            {
                SetState(DefaultStateName);
            }
        }

        /// <summary>
        /// Sets the state of the view.
        /// </summary>
        public override void SetState(string state)
        {
            base.SetState(state);
            ButtonLabel.SetState(state);
        }

        /// <summary>
        /// Called when the button is disabled.
        /// </summary>
        public void OnDisable()
        {
            if (!IsToggleButton && !IsDisabled)
            {
                // reset state to default if view is deactivated
                SetState(DefaultStateName);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets value indicating if button is a toggle button and is pressed.
        /// </summary>
        public bool TogglePressed
        {
            get
            {
                return IsToggleButton && ToggleValue;
            }
        }

        #endregion
    }
}
