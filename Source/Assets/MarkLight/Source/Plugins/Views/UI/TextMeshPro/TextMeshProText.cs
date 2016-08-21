// Uncomment the line below to enable Text Mesh Pro integration. Make sure the Text Mesh Pro asset is imported in the project.
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
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// TextMesh Pro label view.
    /// </summary>
    /// <d>Presents text using TextMesh Pro.</d>
    [ExcludeComponent("ImageComponent")]
    [HideInPresenter]
    public class TextMeshProText : UIView
    {
        #region Fields

        #region MeshRenderer

        /// <summary>
        /// Boolean indicating if the render is enabled.
        /// </summary>
        /// <d>Boolean indicating if the render is enabled. Makes the rendered object visible if enabled.</d>
        [MapTo("MeshRenderer.enabled")]
        public _bool IsRendererEnabled;

        /// <summary>
        /// Index of baked lightmap applied to renderer.
        /// </summary>
        /// <d>Index of baked lightmap applied to renderer.</d>
        [MapTo("MeshRenderer.lightmapIndex")]
        public _int LightmapIndex;

        /// <summary>
        /// UV scale & offset used for lightmap.
        /// </summary>
        /// <d>UV scale & offset used for lightmap.</d>
        [MapTo("MeshRenderer.lightmapScaleOffset")]
        public _Vector4 LightmapScaleOffset;

        /// <summary>
        /// If set the renderer uses Light Probe Proxy Volume component on gameobject.
        /// </summary>
        /// <d>If set the renderer uses Light Probe Proxy Volume component attached to the gameobject.</d>
        [MapTo("MeshRenderer.lightProbeProxyVolumeOverride")]
        public _GameObject LightProbeProxyVolumeOverride;

        /// <summary>
        /// Light probe interpolation type.
        /// </summary>
        /// <d>Enum indicating light probe interpolation type.</d>
        [MapTo("MeshRenderer.lightProbeUsage")]
        public _LightProbeUsage LightProbeUsage;

        /// <summary>
        /// First instantiated material assigned to the renderer.
        /// </summary>
        /// <d>First instantiated material assigned to the renderer.</d>
        [MapTo("MeshRenderer.material")]
        public _Material Material;

        /// <summary>
        /// All instantiated materials assigned to the renderer.
        /// </summary>
        /// <d>All instantiated materials assigned to the renderer.</d>
        [MapTo("MeshRenderer.materials")]
        public _MaterialArray Materials;

        /// <summary>
        /// Indicates if renderer has per-object motion vector pass.
        /// </summary>
        /// <d>Boolean indicating if renderer has a per-object motion vector pass.</d>
        [MapTo("MeshRenderer.motionVectors")]
        public _bool MotionVectors;

        /// <summary>
        /// If set uses the transform position to find the light or reflection.
        /// </summary>
        /// <d>If set uses the transform position to find the light or reflection.</d>
        [MapTo("MeshRenderer.probeAnchor")]
        public _Transform ProbeAnchor;

        /// <summary>
        /// Index of realtime lightmap applied to renderer.
        /// </summary>
        /// <d>Index of realtime lightmap applied to renderer.</d>
        [MapTo("MeshRenderer.realtimeLightmapIndex")]
        public _int RealtimeLightmapIndex;

        /// <summary>
        /// UV scale & offset used for realtime lightmap.
        /// </summary>
        /// <d>UV scale & offset used for realtime lightmap.</d>
        [MapTo("MeshRenderer.realtimeLightmapScaleOffset")]
        public _Vector4 RealtimeLightmapScaleOffset;

        /// <summary>
        /// Indicates if object receives shadows.
        /// </summary>
        /// <d>Indicates if object receives shadows.</d>
        [MapTo("MeshRenderer.receiveShadows")]
        public _bool ReceiveShadows;

        /// <summary>
        /// Indicates how reflection probes should be used for renderer.
        /// </summary>
        /// <d>Indicates how reflection probes should be used for renderer.</d>
        [MapTo("MeshRenderer.reflectionProbeUsage")]
        public _ReflectionProbeUsage ReflectionProbeUsage;

        /// <summary>
        /// Indicates how object casts shadows.
        /// </summary>
        /// <d>Indicates how object casts shadows.</d>
        [MapTo("MeshRenderer.shadowCastingMode")]
        public _ShadowCastingMode ShadowCastingMode;

        /// <summary>
        /// Shared material of object.
        /// </summary>
        /// <d>Shared material of object.</d>
        [MapTo("MeshRenderer.sharedMaterial")]
        public _Material SharedMaterial;

        /// <summary>
        /// All shared materials of object.
        /// </summary>
        /// <d>All shared materials of object.</d>
        [MapTo("MeshRenderer.sharedMaterials")]
        public _MaterialArray SharedMaterials;

        /// <summary>
        /// Unique ID of renderer's sorting layer.
        /// </summary>
        /// <d>Unique ID of renderer's sorting layer.</d>
        [MapTo("MeshRenderer.sortingLayerID")]
        public _int SortingLayerId;

        /// <summary>
        /// Name of renderer's sorting layer.
        /// </summary>
        /// <d>Name of renderer's sorting layer.</d>
        [MapTo("MeshRenderer.sortingLayerName")]
        public _string SortingLayerName;

        /// <summary>
        /// Renderer's order within sorting layer.
        /// </summary>
        /// <d>Renderer's order within sorting layer.</d>
        [MapTo("MeshRenderer.sortingOrder")]
        public _int SortingOrder;

        /// <summary>
        /// Additional vertex streams.
        /// </summary>
        /// <d>Vertex attributes in this mesh will override or add attributes of the primary mesh in the MeshRenderer.</d>
        [MapTo("MeshRenderer.additionalVertexStreams")]
        public _Mesh AdditionalVertexStreams;

        /// <summary>
        /// Mesh renderer component.
        /// </summary>
        /// <d>Renders a mesh.</d>
        public MeshRenderer MeshRenderer;

        #endregion

        #region TextContainer

        /// <summary>
        /// Text Mesh Pro text container component.
        /// </summary>
        /// <d>Text Mesh Pro text container component.</d>
        public TextContainer TextContainerComponent;

        #endregion

        #region TextMeshPro

        public TextMeshPro TextMeshProComponent;

        #endregion


        #region TextComponent

        /// <summary>
        /// Unity UI component used to render text.
        /// </summary>
        /// <d>Unity UI component used to render text.</d>
        public Text TextComponent;

        /// <summary>
        /// Label text.
        /// </summary>
        /// <d>The text of the label. The label can be set to adjust its size to the text through the AdjustToText field.</d>
        [MapTo("TextComponent.text", "TextChanged")]
        public _string Text;

        /// <summary>
        /// Label text font.
        /// </summary>
        /// <d>The font of the label text.</d>
        [MapTo("TextComponent.font", "TextStyleChanged")]
        public _Font Font;

        /// <summary>
        /// Label text font size.
        /// </summary>
        /// <d>The size of the label text.</d>
        [MapTo("TextComponent.fontSize", "TextStyleChanged")]
        public _int FontSize;

        /// <summary>
        /// Label text line spacing.
        /// </summary>
        /// <d>The line spacing of the label text.</d>
        [MapTo("TextComponent.lineSpacing", "TextStyleChanged")]
        public _int LineSpacing;

        /// <summary>
        /// Support rich text.
        /// </summary>
        /// <d>Boolean indicating if the label supports rich text.</d>
        [MapTo("TextComponent.supportRichText")]
        public _bool SupportRichText;

        /// <summary>
        /// Label text font color.
        /// </summary>
        /// <d>The font color of the label text.</d>
        [MapTo("TextComponent.color")]
        public _Color FontColor;

        /// <summary>
        /// Label text font style.
        /// </summary>
        /// <d>The font style of the label text.</d>
        [MapTo("TextComponent.fontStyle", "TextStyleChanged")]
        public _FontStyle FontStyle;

        /// <summary>
        /// Align by glyph geometry.
        /// </summary>
        /// <d>Boolean indicating if the extents of glyph geometry should be used to perform horizontal alignment rather than glyph metrics.</d>
        [MapTo("TextComponent.alignByGeometry")]
        public _bool AlignByGeometry;

        /// <summary>
        /// Resize text for best fit.
        /// </summary>
        /// <d>Boolean indicating if the text is to be automatically resized to fill the size of the label.</d>
        [MapTo("TextComponent.resizeTextForBestFit")]
        public _bool ResizeTextForBestFit;

        /// <summary>
        /// Resize text max size.
        /// </summary>
        /// <d>If ResizeTextForBestFit is true this indicates the maximum size the text can be, 1 = infinity.</d>
        [MapTo("TextComponent.resizeTextMaxSize")]
        public _int ResizeTextMaxSize;

        /// <summary>
        /// Resize text min size.
        /// </summary>
        /// <d>If ResizeTextForBestFit is true this indicates the minimum size the text can be.<d>
        [MapTo("TextComponent.resizeTextMinSize")]
        public _int ResizeTextMinSize;

#if !UNITY_4_6
        /// <summary>
        /// Horizontal overflow mode.
        /// </summary>
        /// <d>Indicates what will happen if the text overflows the horizontal bounds of the label.<d>
        [MapTo("TextComponent.horizontalOverflow")]
        public _HorizontalWrapMode HorizontalOverflow;

        /// <summary>
        /// Vertical overflow mode.
        /// </summary>
        /// <d>Indicates what will happen if the text overflows the vertical bounds of the label.<d>
        [MapTo("TextComponent.verticalOverflow")]
        public _VerticalWrapMode VerticalOverflow;
#endif

        #endregion

        /// <summary>
        /// Label text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [ChangeHandler("BehaviorChanged")]
        public _ElementAlignment TextAlignment;

        /// <summary>
        /// Label text shadow color.
        /// </summary>
        /// <d>The shadow color of the label text.</d>
        [ChangeHandler("BehaviorChanged")]
        public _Color ShadowColor;

        /// <summary>
        /// Label text shadow distance.
        /// </summary>
        /// <d>The distance of the label text shadow.</d>
        [ChangeHandler("BehaviorChanged")]
        public _Vector2 ShadowDistance;

        /// <summary>
        /// Label text outline color.
        /// </summary>
        /// <d>The outline color of the label text.</d>
        [ChangeHandler("BehaviorChanged")]
        public _Color OutlineColor;

        /// <summary>
        /// Label text outline distance.
        /// </summary>
        /// <d>The distance of the label text outline.</d>
        [ChangeHandler("BehaviorChanged")]
        public _Vector2 OutlineDistance;

        /// <summary>
        /// Adjust label to text.
        /// </summary>
        /// <d>An enum indiciating how the label should adjust its size to the label text. By default the label does not adjust its size to the text.</d>
        [ChangeHandler("LayoutChanged")]
        public _AdjustToText AdjustToText;

        private static Regex _tagRegex = new Regex(@"\[(?<tag>[^\]]+)\]");

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Width.DirectValue = new ElementSize(120);
            Height.DirectValue = new ElementSize(40);

            TextAlignment.DirectValue = ElementAlignment.Left;
            if (TextComponent != null)
            {
                TextComponent.fontSize = 18;
                TextComponent.lineSpacing = 1;
                TextComponent.color = Color.black;

#if !UNITY_4_6_0 && !UNITY_4_6_1 && !UNITY_4_6_2
                TextComponent.horizontalOverflow = HorizontalWrapMode.Overflow;
                TextComponent.verticalOverflow = VerticalWrapMode.Overflow;
#endif
            }
        }

        /// <summary>
        /// Called when text changes.
        /// </summary>
        public virtual void TextChanged()
        {
            // parse text
            TextComponent.text = ParseText(TextComponent.text);
            if (AdjustToText == MarkLight.AdjustToText.None)
            {
                // size of view doesn't change with text, no need to notify parents
                QueueChangeHandler("LayoutChanged");
            }
            else
            {
                // size of view changes with text so notify parents                
                LayoutsChanged();
            }
        }

        /// <summary>
        /// Called when a field affecting the layout of the view has changed.
        /// </summary>
        public override void LayoutChanged()
        {
            if (TextComponent == null)
                return;

            AdjustLabelToText();
            base.LayoutChanged();
        }

        /// <summary>
        /// Called when the text style has changed.
        /// </summary>
        public virtual void TextStyleChanged()
        {
            if (AdjustToText == MarkLight.AdjustToText.None)
                return;

            LayoutsChanged();
        }

        /// <summary>
        /// Called when a field affecting the behavior and visual appearance of the view has changed.
        /// </summary>
        public override void BehaviorChanged()
        {
            base.BehaviorChanged();

            TextComponent.alignment = TextAnchor;
            if (ShadowColor.IsSet || ShadowDistance.IsSet)
            {
                var shadowComponent = GetComponent<Shadow>();
                if (shadowComponent == null)
                {
                    shadowComponent = gameObject.AddComponent<Shadow>();
                }

                shadowComponent.effectColor = ShadowColor.Value;
                shadowComponent.effectDistance = ShadowDistance.Value;
            }

            if (OutlineColor.IsSet || OutlineDistance.IsSet)
            {
                var outlineComponent = GetComponent<Outline>();
                if (outlineComponent == null)
                {
                    outlineComponent = gameObject.AddComponent<Outline>();
                }

                outlineComponent.effectColor = OutlineColor.Value;
                outlineComponent.effectDistance = OutlineDistance.Value;
            }
        }

        /// <summary>
        /// Adjusts the label to text.
        /// </summary>
        public void AdjustLabelToText()
        {
            if (AdjustToText == MarkLight.AdjustToText.Width)
            {
                Width.DirectValue = new ElementSize(TextComponent.preferredWidth);
            }
            else if (AdjustToText == MarkLight.AdjustToText.Height)
            {
                Height.DirectValue = new ElementSize(TextComponent.preferredHeight);
            }
            else if (AdjustToText == MarkLight.AdjustToText.WidthAndHeight)
            {
                Width.DirectValue = new ElementSize(TextComponent.preferredWidth);
                Height.DirectValue = new ElementSize(TextComponent.preferredHeight);
            }
        }

        /// <summary>
        /// Replaces BBCode style tags with unity rich text syntax and parses embedded views.
        /// </summary>
        public string ParseText(string text)
        {
            if (text == null)
                return String.Empty;

            if (!TextComponent.supportRichText)
                return text;

            string formattedText = string.Empty;
            string separatorString = "&sp;";

            // search for tokens and apply formatting and embedded views
            List<TextToken> tokens = new List<TextToken>();
            formattedText = _tagRegex.Replace(text, x =>
            {
                string tag = x.Groups["tag"].Value.Trim();
                string tagNoWs = tag.RemoveWhitespace();

                // check if tag matches default tokens
                if (String.Equals("B", tagNoWs, StringComparison.OrdinalIgnoreCase))
                {
                    // bold
                    tokens.Add(new TextToken { TextTokenType = TextTokenType.BoldStart });
                    return separatorString;
                }
                else if (String.Equals("/B", tagNoWs, StringComparison.OrdinalIgnoreCase))
                {
                    // bold end
                    tokens.Add(new TextToken { TextTokenType = TextTokenType.BoldEnd });
                    return separatorString;
                }
                else if (String.Equals("I", tagNoWs, StringComparison.OrdinalIgnoreCase))
                {
                    // italic
                    tokens.Add(new TextToken { TextTokenType = TextTokenType.ItalicStart });
                    return separatorString;
                }
                else if (String.Equals("/I", tagNoWs, StringComparison.OrdinalIgnoreCase))
                {
                    // italic end
                    tokens.Add(new TextToken { TextTokenType = TextTokenType.ItalicEnd });
                    return separatorString;
                }
                else if (tagNoWs.StartsWith("SIZE=", StringComparison.OrdinalIgnoreCase))
                {
                    // parse size value
                    var vc = new IntValueConverter();
                    var convertResult = vc.Convert(tagNoWs.Substring(5), ValueConverterContext.Empty);
                    if (!convertResult.Success)
                    {
                        // unable to parse token
                        Utils.LogError("[MarkLight] {0}: Unable to parse text embedded size tag \"[{1}]\". {2}", GameObjectName, tag, convertResult.ErrorMessage);
                        return String.Format("[{0}]", tag);
                    }

                    tokens.Add(new TextToken { TextTokenType = TextTokenType.SizeStart, FontSize = (int)convertResult.ConvertedValue });
                    return separatorString;
                }
                else if (String.Equals("/SIZE", tagNoWs, StringComparison.OrdinalIgnoreCase))
                {
                    // size end
                    tokens.Add(new TextToken { TextTokenType = TextTokenType.SizeEnd });
                    return separatorString;
                }
                else if (tagNoWs.StartsWith("COLOR=", StringComparison.OrdinalIgnoreCase))
                {
                    // parse color value
                    var vc = new ColorValueConverter();
                    var convertResult = vc.Convert(tagNoWs.Substring(6), ValueConverterContext.Empty);
                    if (!convertResult.Success)
                    {
                        // unable to parse token
                        Utils.LogError("[MarkLight] {0}: Unable to parse text embedded color tag \"[{1}]\". {2}", GameObjectName, tag, convertResult.ErrorMessage);
                        return String.Format("[{0}]", tag);
                    }

                    Color color = (Color)convertResult.ConvertedValue;
                    tokens.Add(new TextToken { TextTokenType = TextTokenType.ColorStart, FontColor = color });
                    return separatorString;
                }
                else if (String.Equals("/COLOR", tagNoWs, StringComparison.OrdinalIgnoreCase))
                {
                    // color end
                    tokens.Add(new TextToken { TextTokenType = TextTokenType.ColorEnd });
                    return separatorString;
                }

                return String.Format("[{0}]", tag);
            });

            // replace newline in string
            formattedText = formattedText.Replace("\\n", Environment.NewLine);

            // split the string up on each line
            StringBuilder result = new StringBuilder();
            var splitString = formattedText.Split(new string[] { separatorString }, StringSplitOptions.None);
            int splitIndex = 0;
            int fontBoldCount = 0;
            int fontItalicCount = 0;
            Stack<int> fontSizeStack = new Stack<int>();

            // loop through each split string and apply tokens (embedded views & font styles)
            foreach (var str in splitString)
            {
                int tokenIndex = splitIndex - 1;
                var token = tokenIndex >= 0 && tokenIndex < tokens.Count ? tokens[tokenIndex] : null;
                ++splitIndex;

                // do we have a token?
                if (token != null)
                {
                    // yes. parse token type
                    switch (token.TextTokenType)
                    {
                        case TextTokenType.BoldStart:
                            result.Append("<b>");
                            ++fontBoldCount;
                            break;

                        case TextTokenType.BoldEnd:
                            result.Append("</b>");
                            --fontBoldCount;
                            break;

                        case TextTokenType.ItalicStart:
                            result.Append("<i>");
                            ++fontItalicCount;
                            break;

                        case TextTokenType.ItalicEnd:
                            result.Append("</i>");
                            --fontItalicCount;
                            break;

                        case TextTokenType.SizeStart:
                            result.Append(String.Format("<size={0}>", token.FontSize));
                            fontSizeStack.Push(token.FontSize);
                            break;

                        case TextTokenType.SizeEnd:
                            result.Append("</size>");
                            fontSizeStack.Pop();
                            break;

                        case TextTokenType.ColorStart:
                            int r = (int)(token.FontColor.r * 255f);
                            int g = (int)(token.FontColor.g * 255f);
                            int b = (int)(token.FontColor.b * 255f);
                            int a = (int)(token.FontColor.a * 255f);
                            result.Append(String.Format("<color=#{0}{1}{2}{3}>", r.ToString("X2"), g.ToString("X2"), b.ToString("X2"), a.ToString("X2")));
                            break;

                        case TextTokenType.ColorEnd:
                            result.Append("</color>");
                            break;

                        case TextTokenType.Unknown:
                        default:
                            break;
                    }
                }

                result.Append(str);
            }

            return result.ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets text anchor.
        /// </summary>
        public TextAnchor TextAnchor
        {
            get
            {
                switch (TextAlignment.Value)
                {
                    case ElementAlignment.TopLeft:
                        return TextAnchor.UpperLeft;
                    case ElementAlignment.Top:
                        return TextAnchor.UpperCenter;
                    case ElementAlignment.TopRight:
                        return TextAnchor.UpperRight;
                    case ElementAlignment.Left:
                        return TextAnchor.MiddleLeft;
                    case ElementAlignment.Right:
                        return TextAnchor.MiddleRight;
                    case ElementAlignment.BottomLeft:
                        return TextAnchor.LowerLeft;
                    case ElementAlignment.Bottom:
                        return TextAnchor.LowerCenter;
                    case ElementAlignment.BottomRight:
                        return TextAnchor.LowerRight;
                    case ElementAlignment.Center:
                    default:
                        return TextAnchor.MiddleCenter;
                }
            }
        }

        #endregion
    }
}
#endif