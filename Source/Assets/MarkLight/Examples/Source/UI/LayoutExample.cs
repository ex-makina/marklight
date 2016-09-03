#region Using Statements
using MarkLight.Examples.Data;
using MarkLight.Views.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Examples.UI
{
    /// <summary>
    /// Example demonstrating how we can adjust layout to a region by changing Alignment, Margin and Offset.
    /// </summary>
    public class LayoutExample : View
    {
        #region Fields

        public Region LayoutRegion;

        #endregion

        #region Methods

        public void TopLeft()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.TopLeft;
        }

        public void Top()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.Top;
        }

        public void TopRight()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.TopRight;
        }

        public void Left()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.Left;
        }

        public void Center()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.Center;
        }

        public void Right()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.Right;
        }

        public void BottomLeft()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.BottomLeft;
        }

        public void Bottom()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.Bottom;
        }

        public void BottomRight()
        {
            LayoutRegion.Alignment.Value = ElementAlignment.BottomRight;
        }

        public void MarginLeft(bool toggle)
        {
            LayoutRegion.Margin.Value = new ElementMargin(
                    toggle ? ElementSize.FromPixels(100) : new ElementSize(),
                    LayoutRegion.Margin.Value.Top,
                    LayoutRegion.Margin.Value.Right,
                    LayoutRegion.Margin.Value.Bottom
                );
        }

        public void MarginTop(bool toggle)
        {
            LayoutRegion.Margin.Value = new ElementMargin(
                    LayoutRegion.Margin.Value.Left,
                    toggle ? ElementSize.FromPixels(100) : new ElementSize(),
                    LayoutRegion.Margin.Value.Right,
                    LayoutRegion.Margin.Value.Bottom
                );
        }

        public void MarginRight(bool toggle)
        {
            LayoutRegion.Margin.Value = new ElementMargin(
                    LayoutRegion.Margin.Value.Left,
                    LayoutRegion.Margin.Value.Top,
                    toggle ? ElementSize.FromPixels(100) : new ElementSize(),
                    LayoutRegion.Margin.Value.Bottom
                );
        }

        public void MarginBottom(bool toggle)
        {
            LayoutRegion.Margin.Value = new ElementMargin(
                    LayoutRegion.Margin.Value.Left,
                    LayoutRegion.Margin.Value.Top,
                    LayoutRegion.Margin.Value.Right,
                    toggle ? ElementSize.FromPixels(100) : new ElementSize()
                );
        }

        public void OffsetLeft(bool toggle)
        {
            LayoutRegion.Offset.Value = new ElementMargin(
                    toggle ? ElementSize.FromPixels(100) : new ElementSize(),
                    LayoutRegion.Offset.Value.Top,
                    LayoutRegion.Offset.Value.Right,
                    LayoutRegion.Offset.Value.Bottom
                );
        }

        public void OffsetTop(bool toggle)
        {
            LayoutRegion.Offset.Value = new ElementMargin(
                    LayoutRegion.Offset.Value.Left,
                    toggle ? ElementSize.FromPixels(100) : new ElementSize(),
                    LayoutRegion.Offset.Value.Right,
                    LayoutRegion.Offset.Value.Bottom
                );
        }

        public void OffsetRight(bool toggle)
        {
            LayoutRegion.Offset.Value = new ElementMargin(
                    LayoutRegion.Offset.Value.Left,
                    LayoutRegion.Offset.Value.Top,
                    toggle ? ElementSize.FromPixels(100) : new ElementSize(),
                    LayoutRegion.Offset.Value.Bottom
                );
        }

        public void OffsetBottom(bool toggle)
        {
            LayoutRegion.Offset.Value = new ElementMargin(
                    LayoutRegion.Offset.Value.Left,
                    LayoutRegion.Offset.Value.Top,
                    LayoutRegion.Offset.Value.Right,
                    toggle ? ElementSize.FromPixels(100) : new ElementSize()
                );
        }

        #endregion
    }
}

