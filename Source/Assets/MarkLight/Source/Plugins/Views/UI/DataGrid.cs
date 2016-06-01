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
    /// DataGrid view.
    /// </summary>
    /// <d>The data grid is used to arrange dynamic or static content in a grid.</d>
    [MapViewField("ItemSelected", "DataGridList.ItemSelected")]
    [MapViewField("ItemDeselected", "DataGridList.ItemDeselected")]
    [MapViewField("ListChanged", "DataGridList.ListChanged")]
    [HideInPresenter]
    public class DataGrid : UIView
    {
        #region Fields

        /// <summary>
        /// Row header view.
        /// </summary>
        /// <d>The row view that is displayed as header for all the data grid rows.</d>
        public RowHeader RowHeader;

        #region DataGridList

        /// <summary>
        /// User-defined data grid data.
        /// </summary>
        /// <d>Can be bound to an generic ObservableList to dynamically generate data grid items based on a template.</d>
        [MapTo("DataGridList.Items")]
        public _IObservableList Items;

        /// <summary>
        /// Selected list item.
        /// </summary>
        /// <d>Set when the selected data grid item changes and points to the user-defined item data.</d>
        [MapTo("DataGridList.SelectedItem")]
        public _object SelectedItem;

        /// <summary>
        /// Indicates if the list of rows are scrollable.
        /// </summary>
        /// <d>Boolean indicating if the list of rows is to be scrollable.</d>
        [MapTo("DataGridList.IsScrollable")]
        public _bool IsScrollable;

        /// <summary>
        /// Indicates if items can be deselected by clicking.
        /// </summary>
        /// <d>A boolean indicating if items in the data grid can be deselected by clicking. Items can always be deselected programmatically.</d>
        [MapTo("DataGridList.CanDeselect")]
        public _bool CanDeselect;

        /// <summary>
        /// Indicates if more than one list item can be selected.
        /// </summary>
        /// <d>A boolean indicating if more than one data grid item can be selected by clicking or programmatically.</d>
        [MapTo("DataGridList.CanMultiSelect")]
        public _bool CanMultiSelect;

        /// <summary>
        /// Indicates if items can be selected by clicking.
        /// </summary>
        /// <d>A boolean indicating if items can be selected by clicking. Items can always be selected programmatically.</d>
        [MapTo("DataGridList.CanSelect")]
        public _bool CanSelect;

        /// <summary>
        /// Indicates if the rows should alternate in style.
        /// </summary>
        /// <d>Boolean indicating if the Row style should alternate between "Default" and "Alternate".</d>
        [MapTo("DataGridList.AlternateItems")]
        public _bool AlternateRows;

        /// <summary>
        /// Data grid list image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the data grid list.</d>
        [MapTo("DataGridList.BackgroundImage")]
        public _Sprite ListImage;

        /// <summary>
        /// Data grid list image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the data grid list.</d>
        [MapTo("DataGridList.BackgroundImageType")]
        public _ImageType ListImageType;

        /// <summary>
        /// Data grid list image material.
        /// </summary>
        /// <d>The material of the data grid list image.</d>
        [MapTo("DataGridList.BackgroundMaterial")]
        public _Material ListMaterial;

        /// <summary>
        /// Data grid list image color.
        /// </summary>
        /// <d>The color of the data grid list image.</d>
        [MapTo("DataGridList.BackgroundColor")]
        public _Color ListColor;

        /// <summary>
        /// Data grid list image width.
        /// </summary>
        /// <d>Specifies the width of the data grid list image either in pixels or percents.</d>
        [MapTo("DataGridList.Width")]
        public _ElementSize ListWidth;

        /// <summary>
        /// Data grid list image height.
        /// </summary>
        /// <d>Specifies the height of the data grid list image either in pixels or percents.</d>
        [MapTo("DataGridList.Height")]
        public _ElementSize ListHeight;

        /// <summary>
        /// Data grid list image offset.
        /// </summary>
        /// <d>Specifies the offset of the data grid list image.</d>
        [MapTo("DataGridList.Offset")]
        public _ElementSize ListOffset;

        /// <summary>
        /// Data grid list image offset.
        /// </summary>
        /// <d>Specifies the offset of the data grid list image.</d>
        [MapTo("DataGridList.Margin")]
        public _ElementMargin ListMargin;

        /// <summary>
        /// Data grid list alignment.
        /// </summary>
        /// <d>Specifies the alignment of the data grid list.</d>
        [MapTo("DataGridList.Alignment")]
        public _ElementAlignment ListAlignment;

        /// <summary>
        /// Data grid list orientation.
        /// </summary>
        /// <d>Specifies the orientation of the data grid list.</d>
        [MapTo("DataGridList.Alignment")]
        public _ElementOrientation ListOrientation;

        /// <summary>
        /// Spacing between data grid list items.
        /// </summary>
        /// <d>The spacing between data grid list items.</d>
        [MapTo("DataGridList.Spacing")]
        public _ElementSize ListSpacing;

        /// <summary>
        /// The alignment of data grid list items.
        /// </summary>
        /// <d>If the data grid list items varies in size the content alignment specifies how the data grid list items should be arranged in relation to each other.</d>
        [MapTo("DataGridList.ContentAlignment")]
        public _ElementAlignment ListContentAlignment;

        /// <summary>
        /// Data grid list content margin.
        /// </summary>
        /// <d>Sets the margin of the data grid list mask view that contains the contents of the data grid list.</d>
        [MapTo("DataGridList.ContentMargin")]
        public _ElementMargin ListContentMargin;

        /// <summary>
        /// Sort direction.
        /// </summary>
        /// <d>If data grid list items has SortIndex set they can be sorted in the direction specified.</d>
        [MapTo("DataGridList.SortDirection")]
        public _ElementSortDirection ListSortDirection;

        #region ListMask

        /// <summary>
        /// Indicates if a list mask is to be used.
        /// </summary>
        /// <d>Boolean indicating if a list mask is to be used.</d>
        [MapTo("DataGridList.UseListMask")]
        public _bool UseListMask;

        /// <summary>
        /// The width of the list mask image.
        /// </summary>
        /// <d>Specifies the width of the list mask image either in pixels or percents.</d>
        [MapTo("DataGridList.ListMaskWidth")]
        public _ElementSize ListMaskWidth;

        /// <summary>
        /// The height of the list mask image.
        /// </summary>
        /// <d>Specifies the height of the list mask image either in pixels or percents.</d>
        [MapTo("DataGridList.ListMaskHeight")]
        public _ElementSize ListMaskHeight;

        /// <summary>
        /// The offset of the list mask image.
        /// </summary>
        /// <d>Specifies the offset of the list mask image.</d>
        [MapTo("DataGridList.ListMaskOffset")]
        public _ElementMargin ListMaskOffset;

        /// <summary>
        /// List max image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the list max.</d>
        [MapTo("DataGridList.ListMaskImage")]
        public _Sprite ListMaskImage;

        /// <summary>
        /// List max image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the list max.</d>
        [MapTo("DataGridList.ListMaskImageType")]
        public _ImageType ListMaskImageType;

        /// <summary>
        /// List max image material.
        /// </summary>
        /// <d>The material of the list max image.</d>
        [MapTo("DataGridList.ListMaskMaterial")]
        public _Material ListMaskMaterial;

        /// <summary>
        /// List max image color.
        /// </summary>
        /// <d>The color of the list max image.</d>
        [MapTo("DataGridList.ListMaskColor")]
        public _Color ListMaskColor;

        /// <summary>
        /// List mask alignment.
        /// </summary>
        /// <d>Specifies the alignment of the list mask.</d>
        [MapTo("DataGridList.ListMaskAlignment")]
        public _ElementAlignment ListMaskAlignment;

        /// <summary>
        /// Indicates if list mask should be rendered.
        /// </summary>
        /// <d>Indicates if the list mask, i.e. the list mask background image sprite and color should be rendered.</d>
        [MapTo("DataGridList.ListMaskShowGraphic")]
        public _bool ListMaskShowGraphic;

        #endregion

        /// <summary>
        /// Data grid list.
        /// </summary>
        /// <d>The data grid list renders all the selectable data grid rows.</d>
        public List DataGridList;

        #endregion

        /// <summary>
        /// Column text margin.
        /// </summary>
        /// <d>The margin of the column text. If set the text margin is applied to all columns that doesn't have a custom text margin set.</d>
        public _ElementMargin ColumnTextMargin;

        /// <summary>
        /// Column text alignment.
        /// </summary>
        /// <d>The alignment of the column text. If set the alignment is applied to all columns that doesn't have a custom alignment set.</d>
        public _ElementAlignment ColumnTextAlignment;

        /// <summary>
        /// Column header text margin.
        /// </summary>
        /// <d>The margin of the column header text. If set the text margin is applied to all column headers that doesn't have a custom text margin set.</d>
        public _ElementMargin ColumnHeaderTextMargin;

        /// <summary>
        /// Column header text alignment.
        /// </summary>
        /// <d>The alignment of the column header text. If set the alignment is applied to all column header that doesn't have a custom alignment set.</d>
        public _ElementAlignment ColumnHeaderTextAlignment;

        /// <summary>
        /// Spacing between columns.
        /// </summary>
        /// <d>Specifies the spacing that should be between columns.</d>
        public _ElementSize ColumnSpacing;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            ColumnSpacing.DirectValue = new ElementSize();
            ColumnTextMargin.DirectValue = new ElementMargin();
            ColumnHeaderTextMargin.DirectValue = new ElementMargin();
        }

        /// <summary>
        /// Called when the layout of a child has changed.
        /// </summary>
        public override void ChildLayoutChanged()
        {
            base.ChildLayoutChanged();
            QueueChangeHandler("LayoutChanged");
        }

        /// <summary>
        /// Called when the layout of the view changes.
        /// </summary>
        public override void LayoutChanged()
        {
            // adjust height to group
            Height.DirectValue = new ElementSize(DataGridList.Height.Value.Pixels + DataGridList.OffsetFromParent.Value.Top.Pixels);
            base.LayoutChanged(); 
        }

        /// <summary>
        /// Initializes the view. 
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            // parse header columns
            var rowHeader = DataGridList.Content.Find<RowHeader>(false);
            if (rowHeader != null)
            {
                rowHeader.MoveTo(this, 0);
                rowHeader.Activate();
                RowHeader = rowHeader;
                RowHeader.Alignment.DirectValue = MarkLight.ElementAlignment.Top;

                // adjust list offset to row header
                DataGridList.OffsetFromParent.Value.Top = RowHeader.Height.Value;
            }
        }

        #endregion
    }
}
