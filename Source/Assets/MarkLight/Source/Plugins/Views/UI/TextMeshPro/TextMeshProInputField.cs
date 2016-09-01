// Text Mesh Pro v1.0.54 integration created for Unity 5.4
//
// Activate Text Mesh Pro integration by creating a file called "smcs.rsp" in your project root, containing the line:
// -define ENABLE_TEXTMESHPRO
//
// Alternatively you can uncomment the define below (and in the other Text Mesh Pro files) to manually enable it. 
// Note that this approach means you need to do it again if you upgrade the MarkLight asset.
//
// When enabled all InputFields and Labels are replaced by the TextMeshPro variant. Make sure the Text Mesh Pro asset
// is imported in your project and that you reload the views in the scene to have the changes take effect.
// 

//#define ENABLE_TEXTMESHPRO

#if ENABLE_TEXTMESHPRO

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
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Text Mesh Pro Input field.
    /// </summary>
    /// <d>Text input using Text Mesh Pro</d>    
    [ExcludeComponent("InputFieldComponent")]
    [ReplacesViewModel("InputField")]
    [RemappedField("ShouldHideMobileInput", "TextMeshProInputFieldComponent.shouldHideMobileInput")]
    [RemappedField("CaretBlinkRate", "TextMeshProInputFieldComponent.caretBlinkRate")]
    [RemappedField("CaretWidth", "TextMeshProInputFieldComponent.caretWidth")]
    [RemappedField("AsteriskChar", "TextMeshProInputFieldComponent.asteriskChar")]
    [RemappedField("CaretColor", "TextMeshProInputFieldComponent.caretColor")]
    [RemappedField("CaretPosition", "TextMeshProInputFieldComponent.caretPosition")]
    [RemappedField("CharacterLimit", "TextMeshProInputFieldComponent.characterLimit")]
    [RemappedField("CharacterValidation", "TextMeshProInputFieldComponent.characterValidation")]
    [RemappedField("ContentType", "TextMeshProInputFieldComponent.contentType")]
    [RemappedField("UseCustomCaretColor", "TextMeshProInputFieldComponent.customCaretColor")]
    [RemappedField("InputType", "TextMeshProInputFieldComponent.inputType")]
    [RemappedField("KeyboardType", "TextMeshProInputFieldComponent.keyboardType")]
    [RemappedField("LineType", "TextMeshProInputFieldComponent.lineType")]
    [RemappedField("IsReadOnly", "TextMeshProInputFieldComponent.readOnly")]
    [RemappedField("SelectionStart", "TextMeshProInputFieldComponent.selectionAnchorPosition")]
    [RemappedField("SelectionEnd", "TextMeshProInputFieldComponent.selectionFocusPosition")]
    [RemappedField("SelectionColor", "TextMeshProInputFieldComponent.selectionColor")]
    [HideInPresenter]
    public class TextMeshProInputField : InputField
    {
        #region Fields

        #region InputText
        
        /// <summary>
        /// Indicates if text goes right to left.
        /// </summary>
        /// <d>Boolean indicating if text goes right to left.</d>
        [MapTo("InputText.IsRightToLeftText")]
        public _bool IsRightToLeftText;

        /// <summary>
        /// Text font.
        /// </summary>
        /// <d>Text font.</d>
        [MapTo("InputText.TMProFont")]
        public TMP_FontAsset TMProFont;

        /// <summary>
        /// Text font shared material.
        /// </summary>
        /// <d>Text font shared material.</d>
        [MapTo("InputText.FontSharedMaterial")]
        public _Material FontSharedMaterial;

        /// <summary>
        /// Text font shared materials.
        /// </summary>
        /// <d>Text font shared materials.</d>
        [MapTo("InputText.FontSharedMaterials")]
        public _MaterialArray FontSharedMaterials;

        /// <summary>
        /// Text font material.
        /// </summary>
        /// <d>Text font material.</d>
        [MapTo("InputText.FontMaterial")]
        public _Material FontMaterial;

        /// <summary>
        /// Text font materials.
        /// </summary>
        /// <d>Text font materials.</d>
        [MapTo("InputText.FontMaterials")]
        public _MaterialArray FontMaterials;

        /// <summary>
        /// Default text vertex alpha value.
        /// </summary>
        /// <d>Default text vertex alpha value.</d>
        [MapTo("InputText.TextAlpha")]
        public _float TextAlpha;

        /// <summary>
        /// Indicates if text vertex color gradient should be used.
        /// </summary>
        /// <d>Boolean indicating if text vertex color gradient should be used.</d>
        [MapTo("InputText.EnableVertexGradient")]
        public _bool EnableVertexGradient;

        /// <summary>
        /// Specifies the colors for the four vertices of the character quads.
        /// </summary>
        /// <d>Specifies the colors for the four vertices of the character quads.</d>
        [MapTo("InputText.ColorGradient")]
        public VertexGradient ColorGradient;

        /// <summary>
        /// Preset specifying the color gradient.
        /// </summary>
        /// <d>Preset specifying the color gradient.</d>
        [MapTo("InputText.ColorGradientPreset")]
        public TMP_ColorGradient ColorGradientPreset;

        /// <summary>
        /// Sprite asset used by the text object.
        /// </summary>
        /// <d>Sprite asset used by the text object.</d>
        [MapTo("InputText.SpriteAsset")]
        public TMP_SpriteAsset SpriteAsset;

        /// <summary>
        /// Indicates if sprite color is multiplies by the vertex color of the text.
        /// </summary>
        /// <d>Indicates if sprite color is multiplies by the vertex color of the text.</d>
        [MapTo("InputText.TintAllSprites")]
        public _bool TintAllSprites;

        /// <summary>
        /// Indicates if color tags should be ignored and default font color should be used.
        /// </summary>
        /// <d>Boolean indicating if color tags should be ignored and default font color should be used.</d>
        [MapTo("InputText.OverrideColorTags")]
        public _bool OverrideColorTags;

        /// <summary>
        /// Face color property of the assigned material.
        /// </summary>
        /// <d>_FaceColor property of the assigned material. Changing face color will result in an instance of the material.</d>
        [MapTo("InputText.FaceColor")]
        public _Color32 FaceColor;

        /// <summary>
        /// Outline color property of the assigned material.
        /// </summary>
        /// <d>_OutlineColor property of the assigned material. Changing outline color will result in an instance of the material.</d>
        [MapTo("InputText.TextMeshProOutlineColor")]
        public _Color32 TextMeshProOutlineColor;

        /// <summary>
        /// Thickness of the font outline.
        /// </summary>
        /// <d>Thickness of the font outline. Setting this will result in an instance of the material.</d>
        [MapTo("InputText.OutlineWidth")]
        public _float OutlineWidth;

        /// <summary>
        /// Weight of the font.
        /// </summary>
        /// <d>Controls the weight of the font if an alternative font asset is assigned for the given weight in the font asset editor.</d>
        [MapTo("InputText.FontWeight")]
        public _int FontWeight;

        /// <summary>
        /// Enables text auto-sizing.
        /// </summary>
        /// <d>Boolean indicating if text auto-sizing should be enabled.</d>
        [MapTo("InputText.EnableAutoSizing")]
        public _bool EnableAutoSizing;

        /// <summary>
        /// Minimum point size of font when auto-sizing is enabled.
        /// </summary>
        /// <d>Minimum point size of font when auto-sizing is enabled through the EnableAutoSizing field.</d>
        [MapTo("InputText.FontSizeMin")]
        public _float FontSizeMin;

        /// <summary>
        /// Maximum point size of font when auto-sizing is enabled.
        /// </summary>
        /// <d>Maximum point size of font when auto-sizing is enabled through the EnableAutoSizing field.</d>
        [MapTo("InputText.FontSizeMax")]
        public _float FontSizeMax;

        /// <summary>
        /// Text font style.
        /// </summary>
        /// <d>Text font style.</d>
        [MapTo("InputText.TMProFontStyle")]
        public _FontStyles TMProFontStyle;

        /// <summary>
        /// Text alignment options.
        /// </summary>
        /// <d>Text alignment options.</d>
        [MapTo("InputText.TMProAlignment")]
        public _TextAlignmentOptions TMProAlignment;

        /// <summary>
        /// Additional spacing between characters.
        /// </summary>
        /// <d>Additional spacing between characters.</d>
        [MapTo("InputText.CharacterSpacing")]
        public _float CharacterSpacing;

        /// <summary>
        /// Additional spacing between paragraphs.
        /// </summary>
        /// <d>Additional spacing between paragraphs.</d>
        [MapTo("InputText.ParagraphSpacing")]
        public _float ParagraphSpacing;

        /// <summary>
        /// Percentage of character width can be adjusted before auto-sizing reduces point size.
        /// </summary>
        /// <d>Percentage of character width can be adjusted before auto-sizing reduces point size.</d>
        [MapTo("InputText.CharacterWidthAdjustment")]
        public _float CharacterWidthAdjustment;

        /// <summary>
        /// Indicates if words should be wrapped.
        /// </summary>
        /// <d>Boolean indicating if words should be wrapped.</d>
        [MapTo("InputText.EnableWordWrapping")]
        public _bool EnableWordWrapping;

        /// <summary>
        /// Controls the ratio between character and word spacing to fill-in space for justified text.
        /// </summary>
        /// <d>Controls the ratio between character and word spacing to fill-in space for justified text.</d>
        [MapTo("InputText.WordWrappingRatios")]
        public _float WordWrappingRatios;

        /// <summary>
        /// Indicates if justification should be adaptive.
        /// </summary>
        /// <d>Boolean indicating if justification should be adaptive.</d>
        [MapTo("InputText.EnableAdaptiveJustification")]
        public _bool EnableAdaptiveJustification;

        /// <summary>
        /// Specifies text overflow mode.
        /// </summary>
        /// <d>Enum specifying text overflow mode.</d>
        [MapTo("InputText.OverflowMode")]
        public _TextOverflowModes OverflowMode;

        /// <summary>
        /// Indicates if kerning is enabled.
        /// </summary>
        /// <d>Boolean indicating if kerning is enabled.</d>
        [MapTo("InputText.EnableKerning")]
        public _bool EnableKerning;

        /// <summary>
        /// Indicates if extra padding around characters should be added.
        /// </summary>
        /// <d>Boolean indicating if extra padding around characters should be added. This may be necessary when the displayed text is very small to prevent clipping.</d>
        [MapTo("InputText.ExtraPadding")]
        public _bool ExtraPadding;

        /// <summary>
        /// Indicates if rich text tags are enabled.
        /// </summary>
        /// <d>Boolean indicating if rich text tags are enabled.</d>
        [MapTo("InputText.RichText")]
        public _bool RichText;

        /// <summary>
        /// Indicates if CTRL characters should be parsed.
        /// </summary>
        /// <d>Boolean indiciating if CTRL characters should be parsed.</d>
        [MapTo("InputText.ParseCtrlCharacters")]
        public _bool ParseCtrlCharacters;

        /// <summary>
        /// Indicates if text should be rendered last on top of scene elements.
        /// </summary>
        /// <d>Boolean indicating if text should be rendered last on top of scene elements.</d>
        [MapTo("InputText.IsOverlay")]
        public _bool IsOverlay;

        /// <summary>
        /// Indicates if perspective correction should be disabled.
        /// </summary>
        /// <d>Boolean indicating if perspective correction should be disabled. Sets Perspective Correction to Zero for Orthographic Camera mode & 0.875f for Perspective Camera mode.</d>
        [MapTo("InputText.IsOrthographic")]
        public _bool IsOrthographic;

        /// <summary>
        /// Enables culling on the shaders. 
        /// </summary>
        /// <d>Enables culling on the shaders. Note changing this value will result in an instance of the material.</d>
        [MapTo("InputText.EnableCulling")]
        public _bool EnableCulling;

        /// <summary>
        /// Indicates if non-visible objects should be refreshed.
        /// </summary>
        /// <d>Boolean indicating if non-visible objects should be refreshed.</d>
        [MapTo("InputText.IgnoreVisibility")]
        public _bool IgnoreVisibility;

        /// <summary>
        /// Controls how face and outline textures are applied to text object.
        /// </summary>
        /// <d>Controls how face and outline textures are applied to text object.</d>
        [MapTo("InputText.HorizontalMapping")]
        public _TextureMappingOptions HorizontalMapping;

        /// <summary>
        /// Controls how face and outline textures are applied to text object.
        /// </summary>
        /// <d>Controls how face and outline textures are applied to text object.</d>
        [MapTo("InputText.VerticalMapping")]
        public _TextureMappingOptions VerticalMapping;

        /// <summary>
        /// Indicates if the mesh will be rendered.
        /// </summary>
        /// <d>Boolean indicating if the mesh will be rendered.</d>
        [MapTo("InputText.RenderMode")]
        public _TextRenderFlags RenderMode;

        /// <summary>
        /// Maximum number of characters visible.
        /// </summary>
        /// <d>Maximum number of characters visible.</d>
        [MapTo("InputText.MaxVisibleCharacters")]
        public _int MaxVisibleCharacters;

        /// <summary>
        /// Maximum number of words visible.
        /// </summary>
        /// <d>Maximum number of words visible.</d>
        [MapTo("InputText.MaxVisibleWords")]
        public _int MaxVisibleWords;

        /// <summary>
        /// Maximum number of lines visible.
        /// </summary>
        /// <d>Maximum number of lines visible.</d>
        [MapTo("InputText.MaxVisibleLines")]
        public _int MaxVisibleLines;

        /// <summary>
        /// Indicates if vertical alignment of text is adjusted based on visible descender of text.
        /// </summary>
        /// <d>Boolean indicating if vertical alignment of text is adjusted based on visible descender of text.</d>
        [MapTo("InputText.UseMaxVisibleDescender")]
        public _bool UseMaxVisibleDescender;

        /// <summary>
        /// Indicates which page of the text is displayed.
        /// </summary>
        /// <d>Indicates which page of the text is displayed (starting at 1).</d>
        [MapTo("InputText.PageToDisplay")]
        public _int PageToDisplay;

        /// <summary>
        /// Margin of text object.
        /// </summary>
        /// <d>Margin of text object.</d>
        [MapTo("InputText.TextMargin")]
        public Vector4 InputTextMargin;

        /// <summary>
        /// Indicates if size of text container adjusts to match text object.
        /// </summary>
        /// <d>Indicates if size of text container adjusts to match text object.</d>
        [MapTo("InputText.AutoSizeTextContainer")]
        public _bool AutoSizeTextContainer;

        /// <summary>
        /// Indicates if geometry of characters are volumetric (cubes) rather than quads.
        /// </summary>
        /// <d>Indicates if geometry of characters are volumetric (cubes) rather than quads.</d>
        [MapTo("InputText.IsVolumetricText")]
        public _bool IsVolumetricText;

        #endregion

        #region TextMeshPro InputField

        /// <summary>
        /// Text Mesh Pro InputField component.
        /// </summary>
        /// <d>Text Mesh Pro InputField component.</d>
        public TMP_InputField TextMeshProInputFieldComponent;
        
        #endregion

        #region InputFieldComponent Wrapper

        /// <summary>
        /// Wrapper for the unity text component.
        /// </summary>
        [ReplacesComponentField("InputFieldComponent")]
        public InputFieldComponentWrapper InputFieldComponentWrapper;

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            // inputfield component
            TextMeshProInputFieldComponent.textComponent = InputText.GetComponent<TextMeshProUGUI>();
            TextMeshProInputFieldComponent.textViewport = RectTransform;
            TextMeshProInputFieldComponent.placeholder = InputFieldPlaceholder.ImageComponent;
            TextMeshProInputFieldComponent.transition = Selectable.Transition.None;
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // hook up input field event system triggers
            TextMeshProInputFieldComponent.onEndEdit.RemoveAllListeners();
            TextMeshProInputFieldComponent.onEndEdit.AddListener(TMProInputFieldEndEdit);

            TextMeshProInputFieldComponent.onValueChanged.RemoveAllListeners();
            TextMeshProInputFieldComponent.onValueChanged.AddListener(TMProInputFieldValueChanged);

            // set initial text
            TextChanged();
        }

        /// <summary>
        /// Called when the input text is changed.
        /// </summary>
        public override void TextChanged()
        {
            if (OnlyTriggerValueChangedFromUI)
            {
                TextMeshProInputFieldComponent.onValueChanged.RemoveAllListeners();
            }

            TextMeshProInputFieldComponent.text = Text ?? String.Empty;

            if (OnlyTriggerValueChangedFromUI)
            {
                TextMeshProInputFieldComponent.onValueChanged.AddListener(TMProInputFieldValueChanged);
                TMProUpdatePlaceholder();
            }
        }

        /// <summary>
        /// Called on input field end edit.
        /// </summary>
        public void TMProInputFieldEndEdit(string value)
        {
            if (SetValueOnEndEdit)
            {
                Text.Value = TextMeshProInputFieldComponent.text;
            }

            TMProUpdatePlaceholder();
            EndEdit.Trigger();
        }

        /// <summary>
        /// Called when input field value has been updated.
        /// </summary>
        public void TMProInputFieldValueChanged(string value)
        {
            if (!SetValueOnEndEdit)
            {
                Text.Value = TextMeshProInputFieldComponent.text;
            }

            TMProUpdatePlaceholder();
            ValueChanged.Trigger();
        }

        /// <summary>
        /// Shows or hides placeholder based on text.
        /// </summary>
        private void TMProUpdatePlaceholder()
        {
            if (String.IsNullOrEmpty(TextMeshProInputFieldComponent.text))
            {
                InputFieldPlaceholder.IsActive.Value = true;
            }
            else
            {
                InputFieldPlaceholder.IsActive.Value = false;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets boolean indicating if the text field is in focus (allowing input). 
        /// </summary>
        public bool IsFocused
        {
            get
            {
                return TextMeshProInputFieldComponent.isFocused;
            }
        }

        #endregion
    }

    /// <summary>
    /// Wraps the unity input field component.
    /// </summary>
    [Serializable]
    public class InputFieldComponentWrapper
    {
        #region Fields

        public char asteriskChar { get { return default(char); } set { } }
        public float caretBlinkRate { get { return default(float); } set { } }
        public Color caretColor { get { return default(Color); } set { } }
        public int caretPosition { get { return default(int); } set { } }
        public int caretWidth { get { return default(int); } set { } }
        public int characterLimit { get { return default(int); } set { } }
        public UnityEngine.UI.InputField.CharacterValidation characterValidation { get { return default(UnityEngine.UI.InputField.CharacterValidation); } set { } }
        public UnityEngine.UI.InputField.ContentType contentType { get { return default(UnityEngine.UI.InputField.ContentType); } set { } }
        public bool customCaretColor { get { return default(bool); } set { } }
        public UnityEngine.UI.InputField.InputType inputType { get { return default(UnityEngine.UI.InputField.InputType); } set { } }
        public TouchScreenKeyboardType keyboardType { get { return default(TouchScreenKeyboardType); } set { } }
        public UnityEngine.UI.InputField.LineType lineType { get { return default(UnityEngine.UI.InputField.LineType); } set { } }
        public UnityEngine.UI.InputField.SubmitEvent onEndEdit { get { return default(UnityEngine.UI.InputField.SubmitEvent); } set { } }
        public UnityEngine.UI.InputField.OnValidateInput onValidateInput { get { return default(UnityEngine.UI.InputField.OnValidateInput); } set { } }
        public UnityEngine.UI.InputField.OnChangeEvent onValueChange { get { return default(UnityEngine.UI.InputField.OnChangeEvent); } set { } }
        public UnityEngine.UI.InputField.OnChangeEvent onValueChanged { get { return default(UnityEngine.UI.InputField.OnChangeEvent); } set { } }
        public Graphic placeholder { get { return default(Graphic); } set { } }
        public bool readOnly { get { return default(bool); } set { } }
        public int selectionAnchorPosition { get { return default(int); } set { } }
        public Color selectionColor { get { return default(Color); } set { } }
        public int selectionFocusPosition { get { return default(int); } set { } }
        public bool shouldHideMobileInput { get { return default(bool); } set { } }
        public string text { get { return default(string); } set { } }
        public Text textComponent { get { return default(Text); } set { } }
        protected int caretPositionInternal { get { return default(int); } set { } }
        protected int caretSelectPositionInternal { get { return default(int); } set { } }

        #endregion
    }
}

#endif