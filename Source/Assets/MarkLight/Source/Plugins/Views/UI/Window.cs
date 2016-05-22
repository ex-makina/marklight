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
    /// Window view.
    /// </summary>
    [MapViewField("CloseButtonClick", "CloseButton.Click")]
    [HideInPresenter]
    public class Window : UICanvas
    {
        #region Fields

        #region TitleRegion

        /// <summary>
        /// Title region image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the title region.</d>
        [MapTo("TitleRegion.BackgroundImage")]
        public _Sprite TitleRegionImage;

        /// <summary>
        /// Title region image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the title region.</d>
        [MapTo("TitleRegion.BackgroundImageType")]
        public _ImageType TitleRegionImageType;

        /// <summary>
        /// Title region image material.
        /// </summary>
        /// <d>The material of the title region image.</d>
        [MapTo("TitleRegion.BackgroundMaterial")]
        public _Material TitleRegionMaterial;

        /// <summary>
        /// Title region image color.
        /// </summary>
        /// <d>The color of the title region image.</d>
        [MapTo("TitleRegion.BackgroundColor")]
        public _Color TitleRegionColor;

        /// <summary>
        /// Title region image width.
        /// </summary>
        /// <d>Specifies the width of the title region image either in pixels or percents.</d>
        [MapTo("TitleRegion.Width")]
        public _ElementSize TitleRegionWidth;

        /// <summary>
        /// Title region image height.
        /// </summary>
        /// <d>Specifies the height of the title region image either in pixels or percents.</d>
        [MapTo("TitleRegion.Height")]
        public _ElementSize TitleRegionHeight;

        /// <summary>
        /// Title region image offset.
        /// </summary>
        /// <d>Specifies the offset of the title region image.</d>
        [MapTo("TitleRegion.Offset")]
        public _ElementSize TitleRegionOffset;

        /// <summary>
        /// Title region image offset.
        /// </summary>
        /// <d>Specifies the offset of the title region image.</d>
        [MapTo("TitleRegion.Margin")]
        public _ElementMargin TitleRegionMargin;

        /// <summary>
        /// Title region alignment.
        /// </summary>
        /// <d>Specifies the alignment of the title region.</d>
        [MapTo("TitleRegion.Alignment")]
        public _ElementAlignment TitleRegionAlignment;

        /// <summary>
        /// Title region.
        /// </summary>
        /// <d>Interractable title region that can be clicked and dragged to move the window. Contains the window title label.</d>
        public InteractableRegion TitleRegion;

        #endregion

        #region TitleLabel

        /// <summary>
        /// Window title text.
        /// </summary>
        /// <d>The text of the window title.</d>
        [MapTo("TitleLabel.Text")]
        public _string Title;

        /// <summary>
        /// Window title text font.
        /// </summary>
        /// <d>The font of the window title.</d>
        [MapTo("TitleLabel.Font")]
        public _Font TitleFont;

        /// <summary>
        /// Window title text font size.
        /// </summary>
        /// <d>The font size of the window title.</d>
        [MapTo("TitleLabel.FontSize")]
        public _int TitleFontSize;

        /// <summary>
        /// Window title text line spacing.
        /// </summary>
        /// <d>The line spacing of the window title.</d>
        [MapTo("TitleLabel.LineSpacing")]
        public _int TitleLineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the window title supports rich text.</d>
        [MapTo("TitleLabel.SupportRichText")]
        public _bool TitleSupportRichText;

        /// <summary>
        /// Window title text font color.
        /// </summary>
        /// <d>The font color of the window title.</d>
        [MapTo("TitleLabel.FontColor")]
        public _Color TitleFontColor;

        /// <summary>
        /// Window title text font style.
        /// </summary>
        /// <d>The font style of the window title.</d>
        [MapTo("TitleLabel.FontStyle")]
        public _FontStyle TitleFontStyle;

        /// <summary>
        /// Window title text margin.
        /// </summary>
        /// <d>The margin of the window title. Can be used to adjust the text positioning.</d>
        [MapTo("TitleLabel.Margin")]
        public _ElementMargin TitleMargin;

        /// <summary>
        /// Window title alignment.
        /// </summary>
        /// <d>The alignment of the window text label.</d>
        [MapTo("TitleLabel.Alignment")]
        public _ElementAlignment TitleAlignment;

        /// <summary>
        /// Window title text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the window title. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("TitleLabel.TextAlignment")]
        public _ElementAlignment TitleTextAlignment;

        /// <summary>
        /// Window title text offset.
        /// </summary>
        /// <d>The offset of the window title. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("TitleLabel.Offset")]
        public _ElementMargin TitleOffset;

        /// <summary>
        /// Window title text shadow color.
        /// </summary>
        /// <d>The shadow color of the window title.</d>
        [MapTo("TitleLabel.ShadowColor")]
        public _Color TitleShadowColor;

        /// <summary>
        /// Window title text shadow distance.
        /// </summary>
        /// <d>The distance of the window title shadow.</d>
        [MapTo("TitleLabel.ShadowDistance")]
        public _Vector2 TitleShadowDistance;

        /// <summary>
        /// Window title text outline color.
        /// </summary>
        /// <d>The outline color of the window title.</d>
        [MapTo("TitleLabel.OutlineColor")]
        public _Color TitleOutlineColor;

        /// <summary>
        /// Window title text outline distance.
        /// </summary>
        /// <d>The distance of the window title outline.</d>
        [MapTo("TitleLabel.OutlineDistance")]
        public _Vector2 TitleOutlineDistance;

        /// <summary>
        /// Adjusts the title to the text.
        /// </summary>
        /// <d>An enum indiciating how the title should adjust its size to the label text.</d>
        [MapTo("TitleLabel.AdjustToText")]
        public _AdjustToText TitleAdjustToText;

        /// <summary>
        /// Title label.
        /// </summary>
        /// <d>Presents the window title text.</d>
        public Label TitleLabel;

        #endregion

        #region ContentRegion

        /// <summary>
        /// Content region image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the content region.</d>
        [MapTo("ContentRegion.BackgroundImage")]
        public _Sprite ContentRegionImage;

        /// <summary>
        /// Content region image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the content region.</d>
        [MapTo("ContentRegion.BackgroundImageType")]
        public _ImageType ContentRegionImageType;

        /// <summary>
        /// Content region image material.
        /// </summary>
        /// <d>The material of the content region image.</d>
        [MapTo("ContentRegion.BackgroundMaterial")]
        public _Material ContentRegionMaterial;

        /// <summary>
        /// Content region image color.
        /// </summary>
        /// <d>The color of the content region image.</d>
        [MapTo("ContentRegion.BackgroundColor")]
        public _Color ContentRegionColor;

        /// <summary>
        /// Content region image width.
        /// </summary>
        /// <d>Specifies the width of the content region image either in pixels or percents.</d>
        [MapTo("ContentRegion.Width")]
        public _ElementSize ContentRegionWidth;

        /// <summary>
        /// Content region image height.
        /// </summary>
        /// <d>Specifies the height of the content region image either in pixels or percents.</d>
        [MapTo("ContentRegion.Height")]
        public _ElementSize ContentRegionHeight;

        /// <summary>
        /// Content region image offset.
        /// </summary>
        /// <d>Specifies the offset of the content region image.</d>
        [MapTo("ContentRegion.Offset")]
        public _ElementSize ContentRegionOffset;

        /// <summary>
        /// Content region image offset.
        /// </summary>
        /// <d>Specifies the offset of the content region image.</d>
        [MapTo("ContentRegion.Margin")]
        public _ElementMargin ContentRegionMargin;

        /// <summary>
        /// Content region alignment.
        /// </summary>
        /// <d>Specifies the alignment of the content region.</d>
        [MapTo("ContentRegion.Alignment")]
        public _ElementAlignment ContentRegionAlignment;

        /// <summary>
        /// Content region.
        /// </summary>
        /// <d>Displays the window content.</d>
        public Region ContentRegion;

        #endregion

        #region CloseButton

        /// <summary>
        /// Close button image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the close button.</d>
        [MapTo("CloseButton.BackgroundImage")]
        public _Sprite CloseButtonImage;

        /// <summary>
        /// Close button image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the close button.</d>
        [MapTo("CloseButton.BackgroundImageType")]
        public _ImageType CloseButtonImageType;

        /// <summary>
        /// Close button image material.
        /// </summary>
        /// <d>The material of the close button image.</d>
        [MapTo("CloseButton.BackgroundMaterial")]
        public _Material CloseButtonMaterial;

        /// <summary>
        /// Close button image color.
        /// </summary>
        /// <d>The color of the close button image.</d>
        [MapTo("CloseButton.BackgroundColor")]
        public _Color CloseButtonColor;

        /// <summary>
        /// Close button width.
        /// </summary>
        /// <d>Specifies the width of the close button either in pixels or percents.</d>
        [MapTo("CloseButton.Width")]
        public _ElementSize CloseButtonWidth;

        /// <summary>
        /// Close button height.
        /// </summary>
        /// <d>Specifies the height of the close button either in pixels or percents.</d>
        [MapTo("CloseButton.Height")]
        public _ElementSize CloseButtonHeight;

        /// <summary>
        /// Close button offset.
        /// </summary>
        /// <d>Specifies the offset of the close button.</d>
        [MapTo("CloseButton.Offset")]
        public _ElementSize CloseButtonOffset;

        /// <summary>
        /// Close button margin.
        /// </summary>
        /// <d>Specifies the margin of the close button.</d>
        [MapTo("CloseButton.Margin")]
        public _ElementMargin CloseButtonMargin;

        /// <summary>
        /// Close button alignment.
        /// </summary>
        /// <d>Specifies the alignment of the close button.</d>
        [MapTo("CloseButton.Alignment")]
        public _ElementAlignment CloseButtonAlignment;

        /// <summary>
        /// Close button text.
        /// </summary>
        /// <d>The text of the close button label.</d>
        [MapTo("CloseButton.Text")]
        public _string CloseButtonText;

        /// <summary>
        /// Close button text font.
        /// </summary>
        /// <d>The font of the close button label.</d>
        [MapTo("CloseButton.Font")]
        public _Font CloseButtonFont;

        /// <summary>
        /// Close button text font size.
        /// </summary>
        /// <d>The font size of the close button label.</d>
        [MapTo("CloseButton.FontSize")]
        public _int CloseButtonFontSize;

        /// <summary>
        /// Close button text line spacing.
        /// </summary>
        /// <d>The line spacing of the close button label.</d>
        [MapTo("CloseButton.LineSpacing")]
        public _int CloseButtonLineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the close button label supports rich text.</d>
        [MapTo("CloseButton.SupportRichText")]
        public _bool CloseButtonSupportRichText;

        /// <summary>
        /// Close button text font color.
        /// </summary>
        /// <d>The font color of the close button label.</d>
        [MapTo("CloseButton.FontColor")]
        public _Color CloseButtonFontColor;

        /// <summary>
        /// Close button text font style.
        /// </summary>
        /// <d>The font style of the close button label.</d>
        [MapTo("CloseButton.FontStyle")]
        public _FontStyle CloseButtonFontStyle;

        /// <summary>
        /// Close button text margin.
        /// </summary>
        /// <d>The margin of the close button label. Can be used to adjust the text positioning.</d>
        [MapTo("CloseButton.TextMargin")]
        public _ElementMargin CloseButtonTextMargin;

        /// <summary>
        /// Close button text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the close button label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("CloseButton.TextAlignment")]
        public _ElementAlignment CloseButtonTextAlignment;

        /// <summary>
        /// Close button text offset.
        /// </summary>
        /// <d>The offset of the close button label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("CloseButton.Offset")]
        public _ElementMargin CloseButtonTextOffset;

        /// <summary>
        /// Close button text padding.
        /// </summary>
        /// <d>The close button TextPadding is used when AdjustToText is set. It determines the additional padding to be added to the size of the button when it adjusts to the text.</d>
        [MapTo("CloseButton.TextPadding")]
        public _ElementMargin CloseButtonTextPadding;

        /// <summary>
        /// Close button text shadow color.
        /// </summary>
        /// <d>The shadow color of the close button label.</d>
        [MapTo("CloseButton.ShadowColor")]
        public _Color CloseButtonShadowColor;

        /// <summary>
        /// Close button text shadow distance.
        /// </summary>
        /// <d>The distance of the close button label shadow.</d>
        [MapTo("CloseButton.ShadowDistance")]
        public _Vector2 CloseButtonShadowDistance;

        /// <summary>
        /// Close button text outline color.
        /// </summary>
        /// <d>The outline color of the close button label.</d>
        [MapTo("CloseButton.OutlineColor")]
        public _Color CloseButtonOutlineColor;

        /// <summary>
        /// Close button text outline distance.
        /// </summary>
        /// <d>The distance of the close button label outline.</d>
        [MapTo("CloseButton.OutlineDistance")]
        public _Vector2 CloseButtonOutlineDistance;

        /// <summary>
        /// Adjusts the close button to the text.
        /// </summary>
        /// <d>An enum indiciating how the close button should adjust its size to the label text.</d>
        [MapTo("CloseButton.AdjustToText")]
        public _AdjustToText CloseButtonAdjustToText;

        /// <summary>
        /// Active state of the window close button.
        /// </summary>
        /// <d>Boolean indicating if window button is active (visible and updated).</d>
        [MapTo("CloseButton.IsActive")]
        public _bool CloseButtonIsActive;

        /// <summary>
        /// Window close button.
        /// </summary>
        /// <d>The window close button.</d>
        public Button CloseButton;

        #endregion

        #region WindowRegion

        /// <summary>
        /// Window region image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the window region.</d>
        [MapTo("WindowRegion.BackgroundImage")]
        public _Sprite WindowImage;

        /// <summary>
        /// Window region image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the window region.</d>
        [MapTo("WindowRegion.BackgroundImageType")]
        public _ImageType WindowImageType;

        /// <summary>
        /// Window region image material.
        /// </summary>
        /// <d>The material of the window region image.</d>
        [MapTo("WindowRegion.BackgroundMaterial")]
        public _Material WindowMaterial;

        /// <summary>
        /// Window region image color.
        /// </summary>
        /// <d>The color of the window region image.</d>
        [MapTo("WindowRegion.BackgroundColor")]
        public _Color WindowColor;

        /// <summary>
        /// Window region image width.
        /// </summary>
        /// <d>Specifies the width of the window region image either in pixels or percents.</d>
        [MapTo("WindowRegion.Width")]
        public _ElementSize WindowRegionWidth;

        /// <summary>
        /// Window region image height.
        /// </summary>
        /// <d>Specifies the height of the window region image either in pixels or percents.</d>
        [MapTo("WindowRegion.Height")]
        public _ElementSize WindowRegionHeight;

        /// <summary>
        /// Window region image offset.
        /// </summary>
        /// <d>Specifies the offset of the window region image.</d>
        [MapTo("WindowRegion.Offset")]
        public _ElementSize WindowRegionOffset;

        /// <summary>
        /// Window region image offset.
        /// </summary>
        /// <d>Specifies the offset of the window region image.</d>
        [MapTo("WindowRegion.Margin")]
        public _ElementMargin WindowRegionMargin;

        /// <summary>
        /// Window region alignment.
        /// </summary>
        /// <d>Specifies the alignment of the window region.</d>
        [MapTo("WindowRegion.Alignment")]
        public _ElementAlignment WindowRegionAlignment;
        
        /// <summary>
        /// Window region.
        /// </summary>
        /// <d>Window region containing the title region, content region and close button.</d>
        public Region WindowRegion;

        #endregion

        /// <summary>
        /// Indicates if window is open.
        /// </summary>
        /// <d>Boolean indicating if the window is opened.</d>
        [ChangeHandler("IsOpenChanged", TriggerImmediately = true)]
        public _bool IsOpen;

        /// <summary>
        /// Override close button click.
        /// </summary>
        /// <d>Boolean indicating if the default behavior on close button click should be overridden.</d>
        public _bool OverrideCloseButtonClick;

        /// <summary>
        /// Indicates if window is movable.
        /// </summary>
        /// <d>Boolean indicating if the window can be moved by clicking and dragging the title region.</d>
        public _bool IsMovable;

        private Vector2 _initialWindowOffset;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {            
            base.SetDefaultValues();

            State.DirectValue = DefaultStateName;
            Canvas.overrideSorting = true;
            Canvas.sortingOrder = 5;
            TitleRegion.Alignment.DirectValue = ElementAlignment.Top;
            IsMovable.DirectValue = true;
            TitleLabel.AdjustToText.DirectValue = AdjustToText.Width;
            OverrideCloseButtonClick.DirectValue = false;
            IsOpen.DirectValue = true;

#if UNITY_4_6_0
            Utils.LogError("[MarkLight] Due to a bug in Unity 4.6.0 (653443) the Window will not work correctly. The bug has been resolved in Unity 4.6.1p1.");
#endif
        }

        /// <summary>
        /// Called on window drag begin.
        /// </summary>
        public void WindowBeginDrag(PointerEventData eventData)
        {
            if (!IsMovable)
            {
                return;
            }

            // calculate initial window offset
            Vector2 pos = GetLocalPoint(eventData.position);
            _initialWindowOffset.x = pos.x - WindowRegion.Offset.Value.Left.Pixels;
            _initialWindowOffset.y = -pos.y - WindowRegion.Offset.Value.Top.Pixels;
        }

        /// <summary>
        /// Called on window drag.
        /// </summary>
        public void WindowDrag(PointerEventData eventData)
        {
            if (!IsMovable)
            {
                return;
            }
            
            // calculate window offset
            Vector2 pos = GetLocalPoint(eventData.position);
            Vector2 offset = new Vector2(pos.x, -pos.y);
            
            // calculate the position of the window based on offset from initial click position
            WindowRegion.Offset.Value = new ElementMargin(offset.x - _initialWindowOffset.x, offset.y - _initialWindowOffset.y);
            
        }

        /// <summary>
        /// Called when window close button is clicked.
        /// </summary>
        public void CloseButtonClick()
        {
            if (OverrideCloseButtonClick)
                return;

            Close();
        }

        /// <summary>
        /// Called when IsOpen is changed.
        /// </summary>
        public void IsOpenChanged()
        {
            if (IsOpen)
            {
                SetState(DefaultStateName);
            }
            else
            {
                SetState("Closed");
            }
        }

        /// <summary>
        /// Opens the window.
        /// </summary>
        public void Open()
        {
            IsOpen.Value = true;
        }

        /// <summary>
        /// Closes the window.
        /// </summary>
        public void Close()
        {
            IsOpen.Value = false;
        }

        #endregion
    }
}
