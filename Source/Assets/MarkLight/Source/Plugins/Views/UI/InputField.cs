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
    /// Input field.
    /// </summary>
    /// <d>Interactable input field enabling user to type single or multi-line text.</d>
    [HideInPresenter]
    public class InputField : UIView
    {
        #region Fields

        #region InputFieldComponent

        /// <summary>
        /// Character used for password fields.
        /// </summary>
        /// <d>The character used for password fields.</d>
        [MapTo("InputFieldComponent.asteriskChar")]
        public _char AsteriskChar;

        /// <summary>
        /// Caret blinks per second.
        /// </summary>
        /// <d>The blinking rate of the input caret, defined as the number of times the blink cycle occurs per second.</d>
        [MapTo("InputFieldComponent.caretBlinkRate")]
        public _float CaretBlinkRate;

        /// <summary>
        /// Caret color.
        /// </summary>
        /// <d>The custom caret color used if customCaretColor is set.</d>
        [MapTo("InputFieldComponent.caretColor")]
        public _Color CaretColor;

        /// <summary>
        /// Current caret position.
        /// </summary>
        /// <d>Current InputField caret position (also selection tail).</d>
        [MapTo("InputFieldComponent.caretPosition")]
        public _int CaretPosition;

        /// <summary>
        /// Caret width.
        /// </summary>
        /// <d>The width of the caret in pixels.</d>
        [MapTo("InputFieldComponent.caretWidth")]
        public _int CaretWidth;

        /// <summary>
        /// Character limit.
        /// </summary>
        /// <d>How many characters the input field is limited to. 0 = infinite.</d>
        [MapTo("InputFieldComponent.characterLimit")]
        public _int CharacterLimit;

        /// <summary>
        /// Character input validation.
        /// </summary>
        /// <d>The type of validation to perform on the input.</d>
        [MapTo("InputFieldComponent.characterValidation")]
        public _InputFieldCharacterValidation CharacterValidation;

        /// <summary>
        /// Type of input content (standard, password, etc).
        /// </summary>
        /// <d>Type of input field content.</d>
        [MapTo("InputFieldComponent.contentType")]
        public _InputFieldContentType ContentType;

        /// <summary>
        /// Indicates if custom caret color should be used.
        /// </summary>
        /// <d>Boolean indicating if the custom caret color specified by CaretColor should be used.</d>
        [MapTo("InputFieldComponent.customCaretColor")]
        public _bool UseCustomCaretColor;

        /// <summary>
        /// Type of input expected.
        /// </summary>
        /// <d>Enum indicating what type of input is expected.</d>
        [MapTo("InputFieldComponent.inputType")]
        public _InputFieldInputType InputType;

        /// <summary>
        /// Type of touch screen keyboard.
        /// </summary>
        /// <d>The type of touch screen keyboard that will be used.</d>
        [MapTo("InputFieldComponent.keyboardType")]
        public _TouchScreenKeyboardType KeyboardType;

        /// <summary>
        /// Input field line type.
        /// </summary>
        /// <d>Enum indicating line type of the input field.</d>
        [MapTo("InputFieldComponent.lineType")]
        public _InputFieldLineType LineType;

        /// <summary>
        /// Indicates if input field is read-only.
        /// </summary>
        /// <d>Boolean indicating if input field is read-only.</d>
        [MapTo("InputFieldComponent.readOnly")]
        public _bool IsReadOnly;

        /// <summary>
        /// Selection start position.
        /// </summary>
        /// <d>Selection start position.</d>
        [MapTo("InputFieldComponent.selectionAnchorPosition")]
        public _int SelectionStart;

        /// <summary>
        /// Selection end position.
        /// </summary>
        /// <d>Selection end position.</d>
        [MapTo("InputFieldComponent.selectionFocusPosition")]
        public _int SelectionEnd;

        /// <summary>
        /// Selection color.
        /// </summary>
        /// <d>The color of the highlight showing which characters are selected.</d>
        [MapTo("InputFieldComponent.selectionColor")]
        public _Color SelectionColor;

        /// <summary>
        /// Indicates if mobile input should be hidden.
        /// </summary>
        /// <d>Boolean indicating if mobile input should be hidden.</d>
        [MapTo("InputFieldComponent.shouldHideMobileInput")]
        public _bool ShouldHideMobileInput;

        /// <summary>
        /// Input field component.
        /// </summary>
        /// <d>Component used to receive and display user text input.</d>
        public UnityEngine.UI.InputField InputFieldComponent;

        #endregion

        #region InputText

        /// <summary>
        /// Input field text font.
        /// </summary>
        /// <d>The font of the input field label.</d>
        [MapTo("InputText.Font")]
        public _Font Font;

        /// <summary>
        /// Input field text font size.
        /// </summary>
        /// <d>The font size of the input field label.</d>
        [MapTo("InputText.FontSize")]
        public _int FontSize;

        /// <summary>
        /// Input field text line spacing.
        /// </summary>
        /// <d>The line spacing of the input field label.</d>
        [MapTo("InputText.LineSpacing")]
        public _int LineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the input field label supports rich text.</d>
        [MapTo("InputText.SupportRichText")]
        public _bool SupportRichText;

        /// <summary>
        /// Input field text font color.
        /// </summary>
        /// <d>The font color of the input field label.</d>
        [MapTo("InputText.FontColor")]
        public _Color FontColor;

        /// <summary>
        /// Input field text font style.
        /// </summary>
        /// <d>The font style of the input field label.</d>
        [MapTo("InputText.FontStyle")]
        public _FontStyle FontStyle;

        /// <summary>
        /// Input field text margin.
        /// </summary>
        /// <d>The margin of the input field label. Can be used to adjust the text positioning.</d>
        [MapTo("InputText.Margin")]
        public _ElementMargin TextMargin;

        /// <summary>
        /// Input field text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the input field label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("InputText.TextAlignment")]
        public _ElementAlignment TextAlignment;

        /// <summary>
        /// Input field text offset.
        /// </summary>
        /// <d>The offset of the input field label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("InputText.Offset")]
        public _ElementMargin TextOffset;

        /// <summary>
        /// Input field text shadow color.
        /// </summary>
        /// <d>The shadow color of the input field label.</d>
        [MapTo("InputText.ShadowColor")]
        public _Color ShadowColor;

        /// <summary>
        /// Input field text shadow distance.
        /// </summary>
        /// <d>The distance of the input field label shadow.</d>
        [MapTo("InputText.ShadowDistance")]
        public _Vector2 ShadowDistance;

        /// <summary>
        /// Input field text outline color.
        /// </summary>
        /// <d>The outline color of the input field label.</d>
        [MapTo("InputText.OutlineColor")]
        public _Color OutlineColor;

        /// <summary>
        /// Input field text outline distance.
        /// </summary>
        /// <d>The distance of the input field label outline.</d>
        [MapTo("InputText.OutlineDistance")]
        public _Vector2 OutlineDistance;

        /// <summary>
        /// Adjusts the input field to the text.
        /// </summary>
        /// <d>An enum indiciating how the input field should adjust its size to the label text.</d>
        [MapTo("InputText.AdjustToText")]
        public _AdjustToText AdjustToText;

        /// <summary>
        /// Label used to display user text input.
        /// </summary>
        /// <d>Label used to display user text input.</d>
        public Label InputText;

        #endregion

        /// <summary>
        /// Input field text.
        /// </summary>
        /// <d>Text displayind in the input field. Set as text is typed or at the end of edit if SetValueOnEndEdit is set.</d>
        [ChangeHandler("TextChanged")]
        public _string Text;

        /// <summary>
        /// Indicates if text value should be updated at end edit.
        /// </summary>
        /// <d>Boolean indicating if text value should be set at end edit rather than while text is being typed.</d>
        public _bool SetValueOnEndEdit;

        /// <summary>
        /// Region displayed when input field is empty.
        /// </summary>
        /// <d>Region that is displayed when the input field has no text input. Any child content of the input field is placed inside this region.</d>
        public Region InputFieldPlaceholder;

        /// <summary>
        /// Input field end edit.
        /// </summary>
        /// <d>Triggered when the user stops editing the input field.</d>
        public ViewAction EndEdit;

        /// <summary>
        /// Input field value changed.
        /// </summary>
        /// <d>Triggered when the input field text changes. Triggered once at end edit if SetValueOnEndEdit is set.</d>
        public ViewAction ValueChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Width.DirectValue = new ElementSize(200);
            Height.DirectValue = new ElementSize(40);

            // input text
            InputText.Margin.DirectValue = new ElementMargin(9);
            InputText.TextAlignment.DirectValue = ElementAlignment.TopLeft;
            InputText.Width.DirectValue = ElementSize.FromPercents(1);
            InputText.Height.DirectValue = ElementSize.FromPercents(1);

            // inputfield component
            InputFieldComponent.textComponent = InputText.TextComponent;
            InputFieldComponent.placeholder = InputFieldPlaceholder.ImageComponent;
            InputFieldComponent.transition = Selectable.Transition.None;            
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // hook up input field event system triggers           
            InputFieldComponent.onEndEdit.RemoveAllListeners();
            InputFieldComponent.onEndEdit.AddListener(InputFieldEndEdit);

#if UNITY_4_6 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
            InputFieldComponent.onValueChange.RemoveAllListeners();
            InputFieldComponent.onValueChange.AddListener(InputFieldValueChanged);
#else
            InputFieldComponent.onValueChanged.RemoveAllListeners();
            InputFieldComponent.onValueChanged.AddListener(InputFieldValueChanged);            
#endif
            // set initial text
            TextChanged();
        }

        /// <summary>
        /// Called when the text is changed.
        /// </summary>
        public virtual void TextChanged()
        {
            InputFieldComponent.text = Text ?? String.Empty;
        }

        /// <summary>
        /// Called on input field end edit.
        /// </summary>
        public void InputFieldEndEdit(string value)
        {
            if (SetValueOnEndEdit)
            {
                Text.Value = InputFieldComponent.text;
            }

            UpdatePlaceholder();
            EndEdit.Trigger();
        }

        /// <summary>
        /// Called when input field value has been updated.
        /// </summary>
        public void InputFieldValueChanged(string value)
        {
            if (!SetValueOnEndEdit)
            {
                Text.Value = InputFieldComponent.text;
            }

            UpdatePlaceholder();
            ValueChanged.Trigger();
        }

        /// <summary>
        /// Shows or hides placeholder based on text.
        /// </summary>
        private void UpdatePlaceholder()
        {
            if (String.IsNullOrEmpty(InputFieldComponent.text))
            {
                InputFieldPlaceholder.IsActive.Value = true;
            }
            else
            {
                InputFieldPlaceholder.IsActive.Value = false;
            }
        }

        #endregion
    }
}
