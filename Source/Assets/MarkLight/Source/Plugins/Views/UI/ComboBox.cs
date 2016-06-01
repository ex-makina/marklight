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
    /// Combo box view.
    /// </summary>
    /// <d>Presents a static or dynamic list of items in a drop-down.</d>
    [MapViewField("ItemSelected", "ComboBoxList.ItemSelected")]
    [HideInPresenter]
    public class ComboBox : UIView
    {
        #region Fields

        #region ComboBoxButton

        /// <summary>
        /// Combo box button image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the combo box button.</d>
        [MapTo("ComboBoxButton.BackgroundImage")]
        public _Sprite ButtonImage;

        /// <summary>
        /// Combo box button image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the combo box button.</d>
        [MapTo("ComboBoxButton.BackgroundImageType")]
        public _ImageType ButtonImageType;

        /// <summary>
        /// Combo box button image material.
        /// </summary>
        /// <d>The material of the combo box button image.</d>
        [MapTo("ComboBoxButton.BackgroundMaterial")]
        public _Material ButtonMaterial;

        /// <summary>
        /// Combo box button image color.
        /// </summary>
        /// <d>The color of the combo box button image.</d>
        [MapTo("ComboBoxButton.BackgroundColor")]
        public _Color ButtonColor;

        /// <summary>
        /// Combo box button width.
        /// </summary>
        /// <d>Specifies the width of the combo box button either in pixels or percents.</d>
        [MapTo("ComboBoxButton.Width")]
        public _ElementSize ButtonWidth;

        /// <summary>
        /// Combo box button height.
        /// </summary>
        /// <d>Specifies the height of the combo box button either in pixels or percents.</d>
        [MapTo("ComboBoxButton.Height")]
        public _ElementSize ButtonHeight;

        /// <summary>
        /// Combo box button offset.
        /// </summary>
        /// <d>Specifies the offset of the combo box button.</d>
        [MapTo("ComboBoxButton.Offset")]
        public _ElementSize ButtonOffset;

        /// <summary>
        /// Combo box button margin.
        /// </summary>
        /// <d>Specifies the margin of the combo box button.</d>
        [MapTo("ComboBoxButton.Margin")]
        public _ElementMargin ButtonMargin;

        /// <summary>
        /// Combo box button alignment.
        /// </summary>
        /// <d>Specifies the alignment of the combo box button.</d>
        [MapTo("ComboBoxButton.Alignment")]
        public _ElementAlignment ButtonAlignment;

        /// <summary>
        /// Combo box button text.
        /// </summary>
        /// <d>The text of the combo box button label.</d>
        [MapTo("ComboBoxButton.Text")]
        public _string ButtonText;

        /// <summary>
        /// Combo box button text font.
        /// </summary>
        /// <d>The font of the combo box button label.</d>
        [MapTo("ComboBoxButton.Font")]
        public _Font ButtonFont;

        /// <summary>
        /// Combo box button text font size.
        /// </summary>
        /// <d>The font size of the combo box button label.</d>
        [MapTo("ComboBoxButton.FontSize")]
        public _int ButtonFontSize;

        /// <summary>
        /// Combo box button text line spacing.
        /// </summary>
        /// <d>The line spacing of the combo box button label.</d>
        [MapTo("ComboBoxButton.LineSpacing")]
        public _int ButtonLineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the combo box button label supports rich text.</d>
        [MapTo("ComboBoxButton.SupportRichText")]
        public _bool ButtonSupportRichText;

        /// <summary>
        /// Combo box button text padding.
        /// </summary>
        /// <d>The combo box button TextPadding is used when AdjustToText is set. It determines the additional padding to be added to the size of the button when it adjusts to the text.</d>
        [MapTo("ComboBoxButton.TextPadding")]
        public _ElementMargin ButtonTextPadding;

        /// <summary>
        /// Combo box button text font color.
        /// </summary>
        /// <d>The font color of the combo box button label.</d>
        [MapTo("ComboBoxButton.FontColor")]
        public _Color ButtonFontColor;

        /// <summary>
        /// Combo box button text font style.
        /// </summary>
        /// <d>The font style of the combo box button label.</d>
        [MapTo("ComboBoxButton.FontStyle")]
        public _FontStyle ButtonFontStyle;

        /// <summary>
        /// Combo box button text margin.
        /// </summary>
        /// <d>The margin of the combo box button label. Can be used to adjust the text positioning.</d>
        [MapTo("ComboBoxButton.TextMargin")]
        public _ElementMargin ButtonTextMargin;

        /// <summary>
        /// Combo box button text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the combo box button label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("ComboBoxButton.TextAlignment")]
        public _ElementAlignment ButtonTextAlignment;

        /// <summary>
        /// Combo box button text offset.
        /// </summary>
        /// <d>The offset of the combo box button label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("ComboBoxButton.Offset")]
        public _ElementMargin ButtonTextOffset;

        /// <summary>
        /// Combo box button text shadow color.
        /// </summary>
        /// <d>The shadow color of the combo box button label.</d>
        [MapTo("ComboBoxButton.ShadowColor")]
        public _Color ButtonShadowColor;

        /// <summary>
        /// Combo box button text shadow distance.
        /// </summary>
        /// <d>The distance of the combo box button label shadow.</d>
        [MapTo("ComboBoxButton.ShadowDistance")]
        public _Vector2 ButtonShadowDistance;

        /// <summary>
        /// Combo box button text outline color.
        /// </summary>
        /// <d>The outline color of the combo box button label.</d>
        [MapTo("ComboBoxButton.OutlineColor")]
        public _Color ButtonOutlineColor;

        /// <summary>
        /// Combo box button text outline distance.
        /// </summary>
        /// <d>The distance of the combo box button label outline.</d>
        [MapTo("ComboBoxButton.OutlineDistance")]
        public _Vector2 ButtonOutlineDistance;

        /// <summary>
        /// Adjusts the combo box button to the text.
        /// </summary>
        /// <d>An enum indiciating how the combo box button should adjust its size to the label text.</d>
        [MapTo("ComboBoxButton.AdjustToText")]
        public _AdjustToText ButtonAdjustToText;
        
        /// <summary>
        /// The combo box button.
        /// </summary>
        /// <d>The combo box button is a toggle button that displays the drop-down list when pressed.</d>        
        public Button ComboBoxButton;

        #endregion

        #region ComboBoxList

        /// <summary>
        /// Indicates if the combo box list is scrollable.
        /// </summary>
        /// <d>Boolean indicating if the combo box list is scrollable. The height of the scrollable list can be set by the ListHeight field.</d>
        [MapTo("ComboBoxList.IsScrollable")]
        public _bool IsScrollable;

        /// <summary>
        /// Scroll delta distance for disabling interaction.
        /// </summary>
        /// <d>If set any interaction with child views (clicks, etc) is disabled when the specified distance has been scrolled. This is used e.g. to disable clicks while scrolling a selectable list of items.</d>
        [MapTo("ComboBoxList.DisableInteractionScrollDelta")]
        public _float DisableInteractionScrollDelta;

        /// <summary>
        /// Indicates if items are selected on mouse up.
        /// </summary>
        /// <d>Boolean indicating if items are selected on mouse up rather than mouse down (default).</d>
        [MapTo("ComboBoxList.SelectOnMouseUp")]
        public _bool SelectOnMouseUp;

        /// <summary>
        /// User-defined list data.
        /// </summary>
        /// <d>Can be bound to an generic ObservableList to dynamically generate combo box items based on a template.</d>
        [MapTo("ComboBoxList.Items")]
        public _IObservableList Items;

        /// <summary>
        /// Selected list item.
        /// </summary>
        /// <d>Set when the selected combo box item changes and points to the user-defined item data.</d>
        [MapTo("ComboBoxList.SelectedItem")]
        public _object SelectedItem;
        
        /// <summary>
        /// Combo box list image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the combo box list.</d>
        [MapTo("ComboBoxList.BackgroundImage")]
        public _Sprite ListImage;

        /// <summary>
        /// Combo box list image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the combo box list.</d>
        [MapTo("ComboBoxList.BackgroundImageType")]
        public _ImageType ListImageType;

        /// <summary>
        /// Combo box list image material.
        /// </summary>
        /// <d>The material of the combo box list image.</d>
        [MapTo("ComboBoxList.BackgroundMaterial")]
        public _Material ListMaterial;

        /// <summary>
        /// Combo box list image color.
        /// </summary>
        /// <d>The color of the combo box list image.</d>
        [MapTo("ComboBoxList.BackgroundColor")]
        public _Color ListColor;

        /// <summary>
        /// Combo box list image width.
        /// </summary>
        /// <d>Specifies the width of the combo box list image either in pixels or percents.</d>
        [MapTo("ComboBoxList.Width")]
        public _ElementSize ListWidth;

        /// <summary>
        /// Combo box list image height.
        /// </summary>
        /// <d>Specifies the height of the combo box list image either in pixels or percents. Used when IsScrollable is True to control the height of the scrollable viewport.</d>
        [MapTo("ComboBoxList.Height")]
        public _ElementSize ListHeight;

        /// <summary>
        /// Combo box list image offset.
        /// </summary>
        /// <d>Specifies the offset of the combo box list image.</d>
        [MapTo("ComboBoxList.Offset")]
        public _ElementSize ListOffset;

        /// <summary>
        /// Combo box list image offset.
        /// </summary>
        /// <d>Specifies the offset of the combo box list image.</d>
        [MapTo("ComboBoxList.Margin")]
        public _ElementMargin ListMargin;

        /// <summary>
        /// Combo box list alignment.
        /// </summary>
        /// <d>Specifies the alignment of the combo box list.</d>
        [MapTo("ComboBoxList.Alignment")]
        public _ElementAlignment ListAlignment;

        /// <summary>
        /// Combo box list orientation.
        /// </summary>
        /// <d>Specifies the orientation of the combo box list.</d>
        [MapTo("ComboBoxList.Orientation")]
        public _ElementOrientation ListOrientation;

        /// <summary>
        /// Spacing between combo box list items.
        /// </summary>
        /// <d>The spacing between combo box list items.</d>
        [MapTo("ComboBoxList.Spacing")]
        public _ElementSize ListSpacing;

        /// <summary>
        /// The alignment of combo box list items.
        /// </summary>
        /// <d>If the combo box list items varies in size the content alignment specifies how the combo box list items should be arranged in relation to each other.</d>
        [MapTo("ComboBoxList.ContentAlignment")]
        public _ElementAlignment ListContentAlignment;

        /// <summary>
        /// Combo box list content margin.
        /// </summary>
        /// <d>Sets the margin of the combo box list mask view that contains the contents of the combo box list.</d>
        [MapTo("ComboBoxList.ContentMargin")]
        public _ElementMargin ListContentMargin;

        /// <summary>
        /// Sort direction.
        /// </summary>
        /// <d>If combo box list items has SortIndex set they can be sorted in the direction specified.</d>
        [MapTo("ComboBoxList.SortDirection")]
        public _ElementSortDirection ListSortDirection;

        #region ListMask

        /// <summary>
        /// Indicates if a list mask is to be used.
        /// </summary>
        /// <d>Boolean indicating if a list mask is to be used.</d>
        [MapTo("ComboBoxList.UseListMask")]
        public _bool UseListMask;

        /// <summary>
        /// The width of the list mask image.
        /// </summary>
        /// <d>Specifies the width of the list mask image either in pixels or percents.</d>
        [MapTo("ComboBoxList.ListMaskWidth")]
        public _ElementSize ListMaskWidth;

        /// <summary>
        /// The height of the list mask image.
        /// </summary>
        /// <d>Specifies the height of the list mask image either in pixels or percents.</d>
        [MapTo("ComboBoxList.ListMaskHeight")]
        public _ElementSize ListMaskHeight;

        /// <summary>
        /// The offset of the list mask image.
        /// </summary>
        /// <d>Specifies the offset of the list mask image.</d>
        [MapTo("ComboBoxList.ListMaskOffset")]
        public _ElementMargin ListMaskOffset;

        /// <summary>
        /// Combo box list mask image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the list max.</d>
        [MapTo("ComboBoxList.ListMaskImage")]
        public _Sprite ListMaskImage;

        /// <summary>
        /// Combo box list mask image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the list max.</d>
        [MapTo("ComboBoxList.ListMaskImageType")]
        public _ImageType ListMaskImageType;

        /// <summary>
        /// Combo box list mask image material.
        /// </summary>
        /// <d>The material of the list max image.</d>
        [MapTo("ComboBoxList.ListMaskMaterial")]
        public _Material ListMaskMaterial;

        /// <summary>
        /// Combo box list mask image color.
        /// </summary>
        /// <d>The color of the list max image.</d>
        [MapTo("ComboBoxList.ListMaskColor")]
        public _Color ListMaskColor;

        /// <summary>
        /// Combo box list mask alignment.
        /// </summary>
        /// <d>Specifies the alignment of the list mask.</d>
        [MapTo("ComboBoxList.ListMaskAlignment")]
        public _ElementAlignment ListMaskAlignment;

        /// <summary>
        /// Indicates if list mask should be rendered.
        /// </summary>
        /// <d>Indicates if the list mask, i.e. the list mask background image sprite and color should be rendered.</d>
        [MapTo("ComboBoxList.ListMaskShowGraphic")]
        public _bool ListMaskShowGraphic;

        #endregion

        /// <summary>
        /// Combo box drop-down list.
        /// </summary>
        /// <d>The combo box drop-down list that is displayed when the combo box button is pressed.</d>                
        public List ComboBoxList;

        #endregion

        #region ComboBoxListCanvas

        /// <summary>
        /// Combo box list canvas sorting order.
        /// </summary>
        /// <d>Combo box list canvas draw order within a sorting layer.</d>
        [MapTo("ComboBoxListCanvas.SortingOrder")]
        public _int CanvasSortingOrder;

        /// <summary>
        /// Override combo box list canvas sort order.
        /// </summary>
        /// <d>Boolean indicating if the sort order should be overriden (not inherited from parent canvas).</d>
        [MapTo("ComboBoxListCanvas.OverrideSorting")]
        public _bool CanvasOverrideSorting;
        
        /// <summary>
        /// Combo box list canvas.
        /// </summary>
        /// <d>The combo box list canvas is used to render the drop-down list on top of all child views in the scene.</d>
        public UICanvas ComboBoxListCanvas;

        #endregion

        /// <summary>
        /// Indicates if the combo box is a drop-up list.
        /// </summary>
        /// <d>Boolean indicating if the combo box should open above instead of below the combo box button.</d>
        [ChangeHandler("IsDropUpChanged")]
        public _bool IsDropUp;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Width.DirectValue = ElementSize.FromPixels(160);
            Height.DirectValue = ElementSize.FromPixels(40);
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // if list is scrollable we need to force select on mouse up and set scroll delta for the combo box to be usable
            if (IsScrollable)
            {
                if (!DisableInteractionScrollDelta.IsSet)
                {
                    DisableInteractionScrollDelta.Value = 1f;
                }
                SelectOnMouseUp.Value = true;
            }


#if UNITY_4_6_0
            Utils.LogError("[MarkLight] Due to a bug in Unity 4.6.0 (653443) the ComboBox will not work correctly. The bug has been resolved in Unity 4.6.1p1.");
#endif
        }

        /// <summary>
        /// Called each frame. Updates the view.
        /// </summary>
        public virtual void Update()
        {
            // if list is open check if user has clicked outside
            if (ComboBoxList.IsActive)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (!ComboBoxButton.ContainsMouse(Input.mousePosition) &&
                        !ComboBoxList.ContainsMouse(Input.mousePosition))
                    {
                        ComboBoxList.Deactivate();
                        ComboBoxButton.ToggleValue.Value = false;
                    }
                }
            }
        }

        /// <summary>
        /// Called when IsDropUp field changes.
        /// </summary>
        public virtual void IsDropUpChanged()
        {
            if (IsDropUp)
            {
                ComboBoxListCanvas.OffsetFromParent.Value = new ElementMargin(0, -ComboBoxList.ActualHeight, 0, 0);
                ComboBoxList.SortDirection.Value = ElementSortDirection.Descending;
            }
            else
            {
                ComboBoxListCanvas.OffsetFromParent.Value = new ElementMargin(0, ActualHeight, 0, 0);
                ComboBoxList.SortDirection.Value = ElementSortDirection.Ascending;
            }
        }

        /// <summary>
        /// Called when mouse is clicked.
        /// </summary>
        public void ComboBoxButtonClick(Button source)
        {
            // toggle combo box list
            if (source.ToggleValue)
            {
                ComboBoxList.Activate();
            }
            else
            {
                ComboBoxList.Deactivate();
            }
        }

        /// <summary>
        /// Called when combo box list selection changes.
        /// </summary>
        public void ComboBoxListSelectionChanged(ItemSelectionActionData actionData)
        {
            // close list and set selected item text
            ComboBoxButton.ToggleValue.Value = false;
            ComboBoxButton.Text.Value = actionData.ItemView != null? actionData.ItemView.Text.Value : String.Empty;
            ComboBoxList.Deactivate();
        }

        #endregion
    }
}
