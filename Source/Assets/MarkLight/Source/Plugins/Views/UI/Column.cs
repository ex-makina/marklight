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
    /// Column view.
    /// </summary>
    /// <d>The column view displays the content of a cell in the data grid.</d>
    [HideInPresenter]
    public class Column : UIView
    {
        #region Fields

        #region ColumnLabel

        /// <summary>
        /// Column label text.
        /// </summary>
        /// <d>The text of the column label.</d>
        [MapTo("ColumnLabel.Text")]
        public _string Text;

        /// <summary>
        /// Column label text font.
        /// </summary>
        /// <d>The font of the column label.</d>
        [MapTo("ColumnLabel.Font")]
        public _Font Font;

        /// <summary>
        /// Column label text font size.
        /// </summary>
        /// <d>The font size of the column label.</d>
        [MapTo("ColumnLabel.FontSize")]
        public _int FontSize;

        /// <summary>
        /// Column label text line spacing.
        /// </summary>
        /// <d>The line spacing of the column label.</d>
        [MapTo("ColumnLabel.LineSpacing")]
        public _int LineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the column label supports rich text.</d>
        [MapTo("ColumnLabel.SupportRichText")]
        public _bool SupportRichText;

        /// <summary>
        /// Column label text font color.
        /// </summary>
        /// <d>The font color of the column label.</d>
        [MapTo("ColumnLabel.FontColor")]
        public _Color FontColor;

        /// <summary>
        /// Column label text font style.
        /// </summary>
        /// <d>The font style of the column label.</d>
        [MapTo("ColumnLabel.FontStyle")]
        public _FontStyle FontStyle;

        /// <summary>
        /// Column label text margin.
        /// </summary>
        /// <d>The margin of the column label. Can be used to adjust the text positioning.</d>
        [MapTo("ColumnLabel.Margin")]
        public _ElementMargin TextMargin;

        /// <summary>
        /// Column label text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the column label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("ColumnLabel.TextAlignment")]
        public _ElementAlignment TextAlignment;

        /// <summary>
        /// Column label text offset.
        /// </summary>
        /// <d>The offset of the column label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("ColumnLabel.Offset")]
        public _ElementMargin TextOffset;

        /// <summary>
        /// Column label text shadow color.
        /// </summary>
        /// <d>The shadow color of the column label.</d>
        [MapTo("ColumnLabel.ShadowColor")]
        public _Color ShadowColor;

        /// <summary>
        /// Column label text shadow distance.
        /// </summary>
        /// <d>The distance of the column label shadow.</d>
        [MapTo("ColumnLabel.ShadowDistance")]
        public _Vector2 ShadowDistance;

        /// <summary>
        /// Column label text outline color.
        /// </summary>
        /// <d>The outline color of the column label.</d>
        [MapTo("ColumnLabel.OutlineColor")]
        public _Color OutlineColor;

        /// <summary>
        /// Column label text outline distance.
        /// </summary>
        /// <d>The distance of the column label outline.</d>
        [MapTo("ColumnLabel.OutlineDistance")]
        public _Vector2 OutlineDistance;

        /// <summary>
        /// Adjusts the column to the text.
        /// </summary>
        /// <d>An enum indiciating how the column should adjust its size to the label text.</d>
        [MapTo("ColumnLabel.AdjustToText")]
        public _AdjustToText AdjustToText;

        /// <summary>
        /// The column label.
        /// </summary>
        /// <d>The column label displays text in the column.</d>
        public Label ColumnLabel;

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Sets the state of the view.
        /// </summary>
        public override void SetState(string state)
        {
            base.SetState(state);
            ColumnLabel.SetState(state);
        }

        #endregion
    }
}
