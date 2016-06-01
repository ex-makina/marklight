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
    /// RowHeader view.
    /// </summary>
    /// <d>The row header view displays the content of a header row in the data grid.</d>
    [HideInPresenter]
    public class RowHeader : UIView
    {
        #region Fields

        private DataGrid _parentDataGrid;

        #endregion

        #region Methods

        /// <summary>
        /// Called when the layout of the view has changed.
        /// </summary>
        public override void LayoutChanged()
        {
            if (ParentDataGrid == null)
            {
                return;
            }

            // arrange columns according to the settings in the parent datagrid
            var columns = this.GetChildren<ColumnHeader>(false);

            // adjust width of columns based on headers
            float columnSpacing = ((columns.Count - 1) * ParentDataGrid.ColumnSpacing.Value.Pixels) / columns.Count;
            List<Column> columnsToFill = new List<Column>();
            float totalWidth = 0;

            for (int i = 0; i < columns.Count; ++i)
            {
                var defWidth = columns[i].Width.Value;
                if (!columns[i].Width.IsSet || defWidth.Fill == true)
                {
                    columnsToFill.Add(columns[i]);
                    continue;
                }
                else if (defWidth.Unit == ElementSizeUnit.Percents)
                {
                    columns[i].OverrideWidth.DirectValue = new ElementSize((defWidth.Percent * ActualWidth) - columnSpacing, ElementSizeUnit.Pixels);
                }
                else
                {
                    columns[i].OverrideWidth.DirectValue = new ElementSize(defWidth.Pixels - columnSpacing, ElementSizeUnit.Pixels);
                }

                totalWidth += columns[i].OverrideWidth.Value.Pixels;
                columns[i].SetIsSet("OverrideWidth");
            }

            // adjust width of fill columns
            if (columnsToFill.Count > 0)
            {
                float columnWidth = Math.Max(columnSpacing, (ActualWidth - totalWidth) / columnsToFill.Count);
                foreach (var column in columnsToFill)
                {
                    column.OverrideWidth.DirectValue = new ElementSize(columnWidth - columnSpacing, ElementSizeUnit.Pixels);
                    column.SetIsSet("OverrideWidth");
                }
            }            

            // adjust column offsets and settings
            float offset = 0;
            foreach (var column in columns)
            {
                if (!column.TextAlignment.IsSet)
                {
                    var textAlignment = ParentDataGrid.ColumnHeaderTextAlignment.IsSet ? ParentDataGrid.ColumnHeaderTextAlignment :
                        ParentDataGrid.ColumnTextAlignment;
                    column.TextAlignment.Value = textAlignment.Value;
                }

                if (!column.TextMargin.IsSet)
                {
                    var textMargin = ParentDataGrid.ColumnHeaderTextMargin.IsSet ? ParentDataGrid.ColumnHeaderTextMargin :
                        ParentDataGrid.ColumnTextMargin;
                    column.TextMargin.Value = textMargin.Value;
                }

                column.OffsetFromParent.DirectValue = new ElementMargin(offset, 0, 0, 0);
                offset += (column.OverrideWidth.Value.Pixels + ParentDataGrid.ColumnSpacing.Value.Pixels);
                column.QueueChangeHandler("LayoutChanged");
            }

            base.LayoutChanged();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets parent datagrid.
        /// </summary>
        public DataGrid ParentDataGrid
        {
            get
            {
                if (_parentDataGrid == null)
                {
                    _parentDataGrid = this.FindParent<DataGrid>();
                }

                return _parentDataGrid;
            }
        }

        #endregion
    }
}
