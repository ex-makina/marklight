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
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Text Mesh Pro label view.
    /// </summary>
    /// <d>Presents text using Text Mesh Pro.</d>
    [ExcludeComponent("TextComponent")]
    [ReplacesViewModel("Label")]
    [RemappedField("Text", "TextMeshProComponent.text", "TextChanged")]
    [RemappedField("FontColor", "TextMeshProComponent.color")]
    [RemappedField("FontSize", "TextMeshProComponent.fontSize", "TextStyleChanged")]
    [RemappedField("LineSpacing", "TextMeshProComponent.lineSpacing", "TextStyleChanged")]
    [HideInPresenter]
    public class TextMeshProLabel : Label
    {
        #region Fields

        #region CanvasRenderer

        /// <summary>
        /// Renders graphics on a canvas.
        /// </summary>
        /// <d>Renders graphics on a canvas.</d>
        public CanvasRenderer CanvasRendererComponent;

        #endregion

        #region TextMeshPro

        /// <summary>
        /// Indicates if text goes right to left.
        /// </summary>
        /// <d>Boolean indicating if text goes right to left.</d>
        [MapTo("TextMeshProComponent.isRightToLeftText")]
        public _bool IsRightToLeftText;

        /// <summary>
        /// Text font.
        /// </summary>
        /// <d>Text font.</d>
        [MapTo("TextMeshProComponent.font")]
        [ReplacesDependencyField("Font")]
        public TMP_FontAsset TMProFont;

        /// <summary>
        /// Text font shared material.
        /// </summary>
        /// <d>Text font shared material.</d>
        [MapTo("TextMeshProComponent.fontSharedMaterial")]
        public _Material FontSharedMaterial;

        /// <summary>
        /// Text font shared materials.
        /// </summary>
        /// <d>Text font shared materials.</d>
        [MapTo("TextMeshProComponent.fontSharedMaterials")]
        public _MaterialArray FontSharedMaterials;

        /// <summary>
        /// Text font material.
        /// </summary>
        /// <d>Text font material.</d>
        [MapTo("TextMeshProComponent.fontMaterial")]
        public _Material FontMaterial;

        /// <summary>
        /// Text font materials.
        /// </summary>
        /// <d>Text font materials.</d>
        [MapTo("TextMeshProComponent.fontMaterials")]
        public _MaterialArray FontMaterials;

        /// <summary>
        /// Default text vertex alpha value.
        /// </summary>
        /// <d>Default text vertex alpha value.</d>
        [MapTo("TextMeshProComponent.alpha")]
        public _float TextAlpha;

        /// <summary>
        /// Indicates if text vertex color gradient should be used.
        /// </summary>
        /// <d>Boolean indicating if text vertex color gradient should be used.</d>
        [MapTo("TextMeshProComponent.enableVertexGradient")]
        public _bool EnableVertexGradient;

        /// <summary>
        /// Specifies the colors for the four vertices of the character quads.
        /// </summary>
        /// <d>Specifies the colors for the four vertices of the character quads.</d>
        [MapTo("TextMeshProComponent.colorGradient")]
        public VertexGradient ColorGradient;

        /// <summary>
        /// Preset specifying the color gradient.
        /// </summary>
        /// <d>Preset specifying the color gradient.</d>
        [MapTo("TextMeshProComponent.colorGradientPreset")]
        public TMP_ColorGradient ColorGradientPreset;

        /// <summary>
        /// Sprite asset used by the text object.
        /// </summary>
        /// <d>Sprite asset used by the text object.</d>
        [MapTo("TextMeshProComponent.spriteAsset")]
        public TMP_SpriteAsset SpriteAsset;

        /// <summary>
        /// Indicates if sprite color is multiplies by the vertex color of the text.
        /// </summary>
        /// <d>Indicates if sprite color is multiplies by the vertex color of the text.</d>
        [MapTo("TextMeshProComponent.tintAllSprites")]
        public _bool TintAllSprites;

        /// <summary>
        /// Indicates if color tags should be ignored and default font color should be used.
        /// </summary>
        /// <d>Boolean indicating if color tags should be ignored and default font color should be used.</d>
        [MapTo("TextMeshProComponent.overrideColorTags")]
        public _bool OverrideColorTags;

        /// <summary>
        /// Face color property of the assigned material.
        /// </summary>
        /// <d>_FaceColor property of the assigned material. Changing face color will result in an instance of the material.</d>
        [MapTo("TextMeshProComponent.faceColor")]
        public _Color32 FaceColor;

        /// <summary>
        /// Outline color property of the assigned material.
        /// </summary>
        /// <d>_OutlineColor property of the assigned material. Changing outline color will result in an instance of the material.</d>
        [MapTo("TextMeshProComponent.outlineColor")]
        [ReplacesDependencyField("OutlineColor")]
        public _Color32 TextMeshProOutlineColor;

        /// <summary>
        /// Thickness of the font outline.
        /// </summary>
        /// <d>Thickness of the font outline. Setting this will result in an instance of the material.</d>
        [MapTo("TextMeshProComponent.outlineWidth")]
        public _float OutlineWidth;

        /// <summary>
        /// Weight of the font.
        /// </summary>
        /// <d>Controls the weight of the font if an alternative font asset is assigned for the given weight in the font asset editor.</d>
        [MapTo("TextMeshProComponent.fontWeight")]
        public _int FontWeight;

        /// <summary>
        /// Enables text auto-sizing.
        /// </summary>
        /// <d>Boolean indicating if text auto-sizing should be enabled.</d>
        [MapTo("TextMeshProComponent.enableAutoSizing")]
        [ReplacesDependencyField("ResizeTextForBestFit")]
        public _bool EnableAutoSizing;

        /// <summary>
        /// Minimum point size of font when auto-sizing is enabled.
        /// </summary>
        /// <d>Minimum point size of font when auto-sizing is enabled through the EnableAutoSizing field.</d>
        [MapTo("TextMeshProComponent.fontSizeMin")]
        public _float FontSizeMin;

        /// <summary>
        /// Maximum point size of font when auto-sizing is enabled.
        /// </summary>
        /// <d>Maximum point size of font when auto-sizing is enabled through the EnableAutoSizing field.</d>
        [MapTo("TextMeshProComponent.fontSizeMax")]
        public _float FontSizeMax;

        /// <summary>
        /// Text font style.
        /// </summary>
        /// <d>Text font style.</d>
        [MapTo("TextMeshProComponent.fontStyle", "TextStyleChanged")]
        [ReplacesDependencyField("FontStyle")]
        public _FontStyles TMProFontStyle;

        /// <summary>
        /// Text alignment options.
        /// </summary>
        /// <d>Text alignment options.</d>
        [MapTo("TextMeshProComponent.alignment")]
        public _TextAlignmentOptions TMProAlignment;

        /// <summary>
        /// Additional spacing between characters.
        /// </summary>
        /// <d>Additional spacing between characters.</d>
        [MapTo("TextMeshProComponent.characterSpacing")]
        public _float CharacterSpacing;

        /// <summary>
        /// Additional spacing between paragraphs.
        /// </summary>
        /// <d>Additional spacing between paragraphs.</d>
        [MapTo("TextMeshProComponent.paragraphSpacing")]
        public _float ParagraphSpacing;

        /// <summary>
        /// Percentage of character width can be adjusted before auto-sizing reduces point size.
        /// </summary>
        /// <d>Percentage of character width can be adjusted before auto-sizing reduces point size.</d>
        [MapTo("TextMeshProComponent.characterWidthAdjustment")]
        public _float CharacterWidthAdjustment;

        /// <summary>
        /// Indicates if words should be wrapped.
        /// </summary>
        /// <d>Boolean indicating if words should be wrapped.</d>
        [MapTo("TextMeshProComponent.enableWordWrapping")]
        public _bool EnableWordWrapping;

        /// <summary>
        /// Controls the ratio between character and word spacing to fill-in space for justified text.
        /// </summary>
        /// <d>Controls the ratio between character and word spacing to fill-in space for justified text.</d>
        [MapTo("TextMeshProComponent.wordWrappingRatios")]
        public _float WordWrappingRatios;

        /// <summary>
        /// Indicates if justification should be adaptive.
        /// </summary>
        /// <d>Boolean indicating if justification should be adaptive.</d>
        [MapTo("TextMeshProComponent.enableAdaptiveJustification")]
        public _bool EnableAdaptiveJustification;

        /// <summary>
        /// Specifies text overflow mode.
        /// </summary>
        /// <d>Enum specifying text overflow mode.</d>
        [MapTo("TextMeshProComponent.OverflowMode")]
        public _TextOverflowModes OverflowMode;

        /// <summary>
        /// Indicates if kerning is enabled.
        /// </summary>
        /// <d>Boolean indicating if kerning is enabled.</d>
        [MapTo("TextMeshProComponent.enableKerning")]
        public _bool EnableKerning;

        /// <summary>
        /// Indicates if extra padding around characters should be added.
        /// </summary>
        /// <d>Boolean indicating if extra padding around characters should be added. This may be necessary when the displayed text is very small to prevent clipping.</d>
        [MapTo("TextMeshProComponent.extraPadding")]
        public _bool ExtraPadding;

        /// <summary>
        /// Indicates if rich text tags are enabled.
        /// </summary>
        /// <d>Boolean indicating if rich text tags are enabled.</d>
        [MapTo("TextMeshProComponent.richText")]
        [ReplacesDependencyField("SupportRichText")]
        public _bool RichText;

        /// <summary>
        /// Indicates if CTRL characters should be parsed.
        /// </summary>
        /// <d>Boolean indiciating if CTRL characters should be parsed.</d>
        [MapTo("TextMeshProComponent.parseCtrlCharacters")]
        public _bool ParseCtrlCharacters;

        /// <summary>
        /// Indicates if text should be rendered last on top of scene elements.
        /// </summary>
        /// <d>Boolean indicating if text should be rendered last on top of scene elements.</d>
        [MapTo("TextMeshProComponent.isOverlay")]
        public _bool IsOverlay;

        /// <summary>
        /// Indicates if perspective correction should be disabled.
        /// </summary>
        /// <d>Boolean indicating if perspective correction should be disabled. Sets Perspective Correction to Zero for Orthographic Camera mode & 0.875f for Perspective Camera mode.</d>
        [MapTo("TextMeshProComponent.isOrthographic")]
        public _bool IsOrthographic;

        /// <summary>
        /// Enables culling on the shaders. 
        /// </summary>
        /// <d>Enables culling on the shaders. Note changing this value will result in an instance of the material.</d>
        [MapTo("TextMeshProComponent.enableCulling")]
        public _bool EnableCulling;

        /// <summary>
        /// Indicates if non-visible objects should be refreshed.
        /// </summary>
        /// <d>Boolean indicating if non-visible objects should be refreshed.</d>
        [MapTo("TextMeshProComponent.ignoreVisibility")]
        public _bool IgnoreVisibility;

        /// <summary>
        /// Controls how face and outline textures are applied to text object.
        /// </summary>
        /// <d>Controls how face and outline textures are applied to text object.</d>
        [MapTo("TextMeshProComponent.horizontalMapping")]
        public _TextureMappingOptions HorizontalMapping;

        /// <summary>
        /// Controls how face and outline textures are applied to text object.
        /// </summary>
        /// <d>Controls how face and outline textures are applied to text object.</d>
        [MapTo("TextMeshProComponent.verticalMapping")]
        public _TextureMappingOptions VerticalMapping;

        /// <summary>
        /// Indicates if the mesh will be rendered.
        /// </summary>
        /// <d>Boolean indicating if the mesh will be rendered.</d>
        [MapTo("TextMeshProComponent.renderMode")]
        public _TextRenderFlags RenderMode;

        /// <summary>
        /// Maximum number of characters visible.
        /// </summary>
        /// <d>Maximum number of characters visible.</d>
        [MapTo("TextMeshProComponent.maxVisibleCharacters")]
        public _int MaxVisibleCharacters;

        /// <summary>
        /// Maximum number of words visible.
        /// </summary>
        /// <d>Maximum number of words visible.</d>
        [MapTo("TextMeshProComponent.maxVisibleWords")]
        public _int MaxVisibleWords;

        /// <summary>
        /// Maximum number of lines visible.
        /// </summary>
        /// <d>Maximum number of lines visible.</d>
        [MapTo("TextMeshProComponent.maxVisibleLines")]
        public _int MaxVisibleLines;

        /// <summary>
        /// Indicates if vertical alignment of text is adjusted based on visible descender of text.
        /// </summary>
        /// <d>Boolean indicating if vertical alignment of text is adjusted based on visible descender of text.</d>
        [MapTo("TextMeshProComponent.useMaxVisibleDescender")]
        public _bool UseMaxVisibleDescender;

        /// <summary>
        /// Indicates which page of the text is displayed.
        /// </summary>
        /// <d>Indicates which page of the text is displayed (starting at 1).</d>
        [MapTo("TextMeshProComponent.pageToDisplay")]
        public _int PageToDisplay;

        /// <summary>
        /// Margin of text object.
        /// </summary>
        /// <d>Margin of text object.</d>
        [MapTo("TextMeshProComponent.margin")]
        public Vector4 TextMargin;

        /// <summary>
        /// Indicates if size of text container adjusts to match text object.
        /// </summary>
        /// <d>Indicates if size of text container adjusts to match text object.</d>
        [MapTo("TextMeshProComponent.autoSizeTextContainer")]
        public _bool AutoSizeTextContainer;

        /// <summary>
        /// Indicates if geometry of characters are volumetric (cubes) rather than quads.
        /// </summary>
        /// <d>Indicates if geometry of characters are volumetric (cubes) rather than quads.</d>
        [MapTo("TextMeshProComponent.isVolumetricText")]
        public _bool IsVolumetricText;

        /// <summary>
        /// Text Mesh Pro UGUI component.
        /// </summary>
        /// <d>Text Mesh Pro UGUI component.</d>
        public TextMeshProUGUI TextMeshProComponent;

        #endregion

        #region TextComponent Wrapper

        /// <summary>
        /// Wrapper for the unity text component.
        /// </summary>
        [ReplacesComponentField("TextComponent")]
        public TextComponentWrapper TextComponentWrapper;

        #endregion

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public TextMeshProLabel()
        {
            TextComponentWrapper = new TextComponentWrapper();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            if (TextMeshProComponent != null)
            {
                TextMeshProComponent.fontSize = 18;
                TextMeshProComponent.color = UnityEngine.Color.black;
            }
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Updates the layout of the view.
        /// </summary>
        public override void LayoutChanged()
        {
            base.LayoutChanged();
        }

        /// <summary>
        /// Called when text changes.
        /// </summary>
        public override void TextChanged()
        {
            // parse text
            Text.DirectValue = ParseText(Text.Value);
            if (AdjustToText == MarkLight.AdjustToText.None)
            {
                // size of view doesn't change with text, no need to notify parents
                QueueChangeHandler("LayoutChanged");
            }
            else
            {                
                LayoutChanged();

                // size of view changes with text so notify parents
                NotifyLayoutChanged();
            }
        }

        /// <summary>
        /// Called when fields affecting the behavior of the view are changed.
        /// </summary>
        public override void BehaviorChanged()
        {
            if (!TMProAlignment.IsSet)
            {
                // convert marklight text alignment to text mesh pro text alignment
                TMProAlignment.DirectValue = TextAlignment.Value.ToTextAlignmentOptions();
            }

            base.BehaviorChanged();
        }

        /// <summary>
        /// Called when fields changing the text styles are changed.
        /// </summary>
        public override void TextStyleChanged()
        {
            if (!TMProFontStyle.IsSet)
            {
                // convert marklight font style to text mesh pro font style
                TMProFontStyle.DirectValue = FontStyle.Value.ToFontStyles();
            }

            base.TextStyleChanged();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Preferred width of text.
        /// </summary>
        public override float PreferredWidth
        {
            get
            {
                var preferredValues = TextMeshProComponent.GetPreferredValues();
                return preferredValues.x;
            }
        }

        /// <summary>
        /// Preferred height of text.
        /// </summary>
        public override float PreferredHeight
        {
            get
            {
                var preferredValues = TextMeshProComponent.GetPreferredValues();
                return preferredValues.y;
            }
        }

        #endregion
    }

    /// <summary>
    /// Wraps the unity text component.
    /// </summary>
    [Serializable]
    public class TextComponentWrapper
    {
        #region Fields

        public string text { get { return default(string); } set { } }
        public Font font { get { return default(Font); } set { } }
        public int fontSize { get { return default(int); } set { } }
        public int lineSpacing { get { return default(int); } set { } }
        public bool supportRichText { get { return default(bool); } set { } }
        public Color color { get { return default(Color); } set { } }
        public FontStyle fontStyle { get { return default(FontStyle); } set { } }
        public bool alignByGeometry { get { return default(bool); } set { } }
        public bool resizeTextForBestFit { get { return default(bool); } set { } }
        public int resizeTextMaxSize { get { return default(int); } set { } }
        public int resizeTextMinSize { get { return default(int); } set { } }
        public float preferredWidth { get { return default(float); } set { } }
        public float preferredHeight { get { return default(float); } set { } }

#if !UNITY_4_6
        public HorizontalWrapMode horizontalOverflow { get { return default(HorizontalWrapMode); } set { } }
        public VerticalWrapMode verticalOverflow { get { return default(VerticalWrapMode); } set { } }
#endif
        #endregion
    }

    /// <summary>
    /// Extension methods for text mesh pro integration.
    /// </summary>
    public static class TextMeshProExtensionMethods
    {
        #region Methods

        /// <summary>
        /// Converts marklight text alignment to text mesh pro text alignment.
        /// </summary>
        public static TextAlignmentOptions ToTextAlignmentOptions(this ElementAlignment alignment)
        {
            switch (alignment)
            {
                case ElementAlignment.Center:
                    return TextAlignmentOptions.Center;
                case ElementAlignment.Left:
                    return TextAlignmentOptions.Left;
                case ElementAlignment.Top:
                    return TextAlignmentOptions.Top;
                case ElementAlignment.Right:
                    return TextAlignmentOptions.Right;
                case ElementAlignment.Bottom:
                    return TextAlignmentOptions.Bottom;
                case ElementAlignment.TopLeft:
                    return TextAlignmentOptions.TopLeft;
                case ElementAlignment.TopRight:
                    return TextAlignmentOptions.TopRight;
                case ElementAlignment.BottomLeft:
                    return TextAlignmentOptions.BottomLeft;
                case ElementAlignment.BottomRight:
                    return TextAlignmentOptions.BottomRight;
                default:
                    return TextAlignmentOptions.Center;
            }
        }

        /// <summary>
        /// Converts marklight font style to text mesh pro font style.
        /// </summary>
        public static FontStyles ToFontStyles(this FontStyle fontStyle)
        {
            switch (fontStyle)
            {
                case FontStyle.Normal:
                    return FontStyles.Normal;
                case FontStyle.Bold:
                    return FontStyles.Bold;
                case FontStyle.Italic:
                    return FontStyles.Italic;
                case FontStyle.BoldAndItalic:
                    return FontStyles.Bold | FontStyles.Italic;
                default:
                    return FontStyles.Normal;
            }
        }

        #endregion
    }

    /// <summary>
    /// Value converter for TMP_FontAsset type.
    /// </summary>
    public class TMProFontValueConverter : AssetValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public TMProFontValueConverter()
        {
            _type = typeof(TMP_FontAsset);
            _loadType = _type;
            IsUnityAssetType = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts asset path and sets bool indicating if asset not found errors should be suppressed.
        /// </summary>
        protected override string ConvertAssetPath(string loadAssetPath, bool inResourcesFolder, out bool suppressAssetNotFoundError)
        {
            suppressAssetNotFoundError = false;
            if (inResourcesFolder)
            {                
                return loadAssetPath;
            }

            // if the path refers to a unity font (TrueType or OpenType) then check if an equivalent Text Mesh Pro font asset exists
            string extension = System.IO.Path.GetExtension(loadAssetPath);
            if (String.Equals(extension, ".ttf", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(extension, ".otf", StringComparison.OrdinalIgnoreCase))
            {
                loadAssetPath = System.IO.Path.ChangeExtension(loadAssetPath, ".asset");
                suppressAssetNotFoundError = true;
            }

            return loadAssetPath;
        }

        /// <summary>
        /// Converts loaded asset to desired type.
        /// </summary>
        protected override ConversionResult ConvertAssetResult(UnityAsset loadedAsset)
        {
            if (loadedAsset.Asset is Font)
            {
                // if it's a unity font we return null
                return new ConversionResult(null);
            }

            // else we return the text mesh pro asset
            return new ConversionResult(loadedAsset.Asset);            
        }

        #endregion
    }

    /// <summary>
    /// Value converter for TMP_FontAsset type.
    /// </summary>
    public class TMProGradientValueConverter : AssetValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public TMProGradientValueConverter()
        {
            _type = typeof(TMP_ColorGradient);
            _loadType = _type;
            IsUnityAssetType = false;
        }

        #endregion
    }

    /// <summary>
    /// Value converter for VertexGradient type.
    /// </summary>
    public class VertexGradientValueConverter : ValueConverter
    {
        #region Fields

        private ColorValueConverter _colorValueConverter;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public VertexGradientValueConverter()
        {
            _type = typeof(VertexGradient);
            _colorValueConverter = new ColorValueConverter();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Value converter for Font type.
        /// </summary>
        public override ConversionResult Convert(object value, ValueConverterContext context)
        {
            if (value == null)
            {
                return base.Convert(value, context);
            }

            Type valueType = value.GetType();
            if (valueType == _type)
            {
                return base.Convert(value, context);
            }
            else if (valueType == _stringType)
            {
                var stringValue = (string)value;
                try
                {
                    // format: "colorTopLeft;colorTopRight;colorBottomLeft;colorBottomRight"
                    string[] colors = stringValue.Split(';');
                    if (colors.Length == 4)
                    {
                        var result1 = _colorValueConverter.Convert(colors[0], context);
                        var result2 = _colorValueConverter.Convert(colors[1], context);
                        var result3 = _colorValueConverter.Convert(colors[2], context);
                        var result4 = _colorValueConverter.Convert(colors[3], context);

                        return result1.Success && result2.Success && result3.Success && result4.Success ?
                            new ConversionResult(new VertexGradient((Color)result1.ConvertedValue, (Color)result2.ConvertedValue, (Color)result3.ConvertedValue, (Color)result4.ConvertedValue)) :
                            StringConversionFailed(value);
                    }
                    else if (colors.Length == 1)
                    {
                        var result1 = _colorValueConverter.Convert(colors[0], context);
                        return result1.Success ? new ConversionResult(new VertexGradient((Color)result1.ConvertedValue)) :
                            StringConversionFailed(value);
                    }
                    else
                    {
                        return StringConversionFailed(value);
                    }
                }
                catch (Exception e)
                {
                    return ConversionFailed(value, e);
                }
            }

            return ConversionFailed(value);
        }

        /// <summary>
        /// Converts value to string.
        /// </summary>
        public override string ConvertToString(object value)
        {
            return value != null ? ViewPresenter.Instance.GetAssetPath(value as UnityEngine.Object) : String.Empty;
        }

        #endregion
    }

    #region Dependency Fields

    [Serializable]
    public class _FontStyles : ViewField<FontStyles>
    {
        public static implicit operator FontStyles(_FontStyles value) { return value.Value; }
    }

    [Serializable]
    public class _TextAlignmentOptions : ViewField<TextAlignmentOptions>
    {
        public static implicit operator TextAlignmentOptions(_TextAlignmentOptions value) { return value.Value; }
    }

    [Serializable]
    public class _TextOverflowModes : ViewField<TextOverflowModes>
    {
        public static implicit operator TextOverflowModes(_TextOverflowModes value) { return value.Value; }
    }

    [Serializable]
    public class _TextureMappingOptions : ViewField<TextureMappingOptions>
    {
        public static implicit operator TextureMappingOptions(_TextureMappingOptions value) { return value.Value; }
    }

    [Serializable]
    public class _TextRenderFlags : ViewField<TextRenderFlags>
    {
        public static implicit operator TextRenderFlags(_TextRenderFlags value) { return value.Value; }
    }

    #endregion
}
#endif