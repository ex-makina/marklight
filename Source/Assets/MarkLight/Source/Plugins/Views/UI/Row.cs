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
    /// Row view.
    /// </summary>
    /// <d>The row view displays the content of a row in the data grid.</d>
    [HideInPresenter]
    public class Row : ListItem
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
            var columns = this.GetChildren<Column>(false);
            var columnHeaders = ParentDataGrid.RowHeader != null ? ParentDataGrid.RowHeader.GetChildren<ColumnHeader>(false) :
                new List<ColumnHeader>();

            if (columnHeaders.Count > 0 && columns.Count > columnHeaders.Count)
            {
                Utils.LogWarning("[MarkLight] {0}: Row contains more columns ({1}) than there are column headers ({2}).", name,
                    columns.Count, columnHeaders.Count);

                // remove columns outside the bounds
                columns = new List<Column>(columns.Take(columnHeaders.Count));
            }

            // if no headers exist arrange columns to fit row with equal widths
            if (columnHeaders.Count <= 0)
            {
                // adjust columns to row width
                float columnWidth = (ActualWidth - ((columns.Count - 1) * ParentDataGrid.ColumnSpacing.Value.Pixels)) / columns.Count;
                foreach (var column in columns)
                {
                    column.Width.DirectValue = ElementSize.FromPixels(columnWidth);
                }
            }
            else
            {
                // adjust width of columns based on headers
                float columnSpacing = ((columns.Count - 1) * ParentDataGrid.ColumnSpacing.Value.Pixels) / columns.Count;
                List<Column> columnsToFill = new List<Column>();
                float totalWidth = 0;

                for (int i = 0; i < columns.Count; ++i)
                {
                    var defWidth = columnHeaders[i].Width.Value;
                    if (!columnHeaders[i].Width.IsSet || defWidth.Fill == true)
                    {
                        columnsToFill.Add(columns[i]);
                        continue;
                    }
                    else if (defWidth.Unit == ElementSizeUnit.Percents)
                    {
                        columns[i].Width.DirectValue = new ElementSize((defWidth.Percent * ActualWidth) - columnSpacing, ElementSizeUnit.Pixels);
                    }
                    else
                    {
                        columns[i].Width.DirectValue = new ElementSize(defWidth.Pixels - columnSpacing, ElementSizeUnit.Pixels);
                    }

                    totalWidth += columns[i].Width.Value.Pixels;
                }

                // adjust width of fill columns
                if (columnsToFill.Count > 0)
                {
                    float columnWidth = Math.Max(columnSpacing, (ActualWidth - totalWidth) / columnsToFill.Count);
                    foreach (var column in columnsToFill)
                    {
                        column.Width.DirectValue = new ElementSize(columnWidth - columnSpacing, ElementSizeUnit.Pixels);
                    }
                }
            }

            // adjust column offsets and settings
            float offset = 0;
            foreach (var column in columns)
            {
                if (!column.TextAlignment.IsSet)
                {
                    column.TextAlignment.Value = ParentDataGrid.ColumnTextAlignment.Value;
                }

                if (!column.TextMargin.IsSet)
                {
                    column.TextMargin.Value = ParentDataGrid.ColumnTextMargin.Value;
                }
                
                column.OffsetFromParent.DirectValue = new ElementMargin(offset, 0, 0, 0);
                offset += (column.Width.Value.Pixels + ParentDataGrid.ColumnSpacing.Value.Pixels);
                column.QueueChangeHandler("LayoutChanged");
            }

            base.LayoutChanged();
        }

        /// <summary>
        /// Sets the state of the view.
        /// </summary>
        public override void SetState(string state)
        {
            base.SetState(state);
            this.ForEachChild<Column>(x => x.SetState(state), false);
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
