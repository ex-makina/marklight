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
    /// Group view.
    /// </summary>
    /// <d>The group is used to spacially arrange child views next to each other either horizontally or vertically.</d>
    [HideInPresenter]
    public class Group : UIView
    {
        #region Fields

        /// <summary>
        /// Orientation of the group.
        /// </summary>
        /// <d>The orientation of the group that determines how the child views will be arranged.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementOrientation Orientation;

        /// <summary>
        /// Spacing between views.
        /// </summary>
        /// <d>Determines the spacing to be added between views in the group.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSize Spacing;

        /// <summary>
        /// Content alignment.
        /// </summary>
        /// <d>Determines how the children should be aligned in relation to each other.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementAlignment ContentAlignment;

        /// <summary>
        /// Sort direction.
        /// </summary>
        /// <d>If children has SortIndex set the sort direction determines which order the views should be arranged in the group.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSortDirection SortDirection;

        protected View _groupContentContainer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Group()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Spacing.DirectValue = new ElementSize();
            Orientation.DirectValue = ElementOrientation.Vertical;
            SortDirection.DirectValue = ElementSortDirection.Ascending;
        }

        /// <summary>
        /// Called when a child layout has been updated.
        /// </summary>
        public override void ChildLayoutChanged()
        {
            base.ChildLayoutChanged();
            QueueChangeHandler("LayoutChanged");
        }

        /// <summary>
        /// Updates the layout of the view.
        /// </summary>
        public override void LayoutChanged()
        {
            float maxWidth = 0f;
            float maxHeight = 0f;
            float totalWidth = 0f;
            float totalHeight = 0f;
            bool percentageWidth = false;
            bool percentageHeight = false;

            bool isHorizontal = Orientation == ElementOrientation.Horizontal;

            var children = new List<UIView>();
            var childrenToBeSorted = new List<UIView>();
            _groupContentContainer.ForEachChild<UIView>(x =>
            {
                // should this be sorted?
                if (x.SortIndex != 0)
                {
                    // yes. 
                    childrenToBeSorted.Add(x);
                    return;
                }

                children.Add(x);
            }, false);

            if (SortDirection == ElementSortDirection.Ascending)
            {
                children.AddRange(childrenToBeSorted.OrderBy(x => x.SortIndex.Value));
            }
            else
            {
                children.AddRange(childrenToBeSorted.OrderByDescending(x => x.SortIndex.Value));
            }

            // get size of content and set content offsets and alignment
            int childCount = children.Count;
            int childIndex = 0;
            for (int i = 0; i < childCount; ++i)
            {
                var view = children[i];

                // don't group disabled views
                if (!view.IsLive)
                    continue;

                if (view.Width.Value.Unit == ElementSizeUnit.Percents)
                {
                    if (isHorizontal)
                    {
                        Utils.LogWarning("[MarkLight] Unable to group view \"{0}\" horizontally as it doesn't specify its width in pixels or elements.", view.GameObjectName);
                        continue;
                    }
                    else
                    {
                        percentageWidth = true;
                    }
                }

                if (view.Height.Value.Unit == ElementSizeUnit.Percents)
                {
                    if (!isHorizontal)
                    {
                        Utils.LogWarning("[MarkLight] Unable to group view \"{0}\" vertically as it doesn't specify its height in pixels or elements.", view.GameObjectName);
                        continue;
                    }
                    else
                    {
                        percentageHeight = true;
                    }
                }

                // set offsets and alignment
                var offset = new ElementMargin(
                    new ElementSize(isHorizontal ? totalWidth + Spacing.Value.Pixels * childIndex : 0f, ElementSizeUnit.Pixels),
                    new ElementSize(!isHorizontal ? totalHeight + Spacing.Value.Pixels * childIndex : 0f, ElementSizeUnit.Pixels));
                view.OffsetFromParent.DirectValue = offset;

                // set desired alignment if it is valid for the orientation otherwise use defaults
                var alignment = isHorizontal ? ElementAlignment.Left : ElementAlignment.Top;
                var desiredAlignment = ContentAlignment.IsSet ? ContentAlignment : view.Alignment;
                if (isHorizontal && (desiredAlignment == ElementAlignment.Top || desiredAlignment == ElementAlignment.Bottom
                    || desiredAlignment == ElementAlignment.TopLeft || desiredAlignment == ElementAlignment.BottomLeft))
                {
                    view.Alignment.DirectValue = alignment | desiredAlignment;
                }
                else if (!isHorizontal && (desiredAlignment == ElementAlignment.Left || desiredAlignment == ElementAlignment.Right
                    || desiredAlignment == ElementAlignment.TopLeft || desiredAlignment == ElementAlignment.TopRight))
                {
                    view.Alignment.DirectValue = alignment | desiredAlignment;
                }
                else
                {
                    view.Alignment.DirectValue = alignment;
                }

                // get size of content
                if (!percentageWidth)
                {
                    totalWidth += view.Width.Value.Pixels;
                    maxWidth = view.Width.Value.Pixels > maxWidth ? view.Width.Value.Pixels : maxWidth;
                }

                if (!percentageHeight)
                {
                    totalHeight += view.Height.Value.Pixels;
                    maxHeight = view.Height.Value.Pixels > maxHeight ? view.Height.Value.Pixels : maxHeight;
                }

                // update child layout
                view.RectTransformChanged();
                ++childIndex;
            }

            // set width and height 
            float totalSpacing = childCount > 1 ? (childIndex - 1) * Spacing.Value.Pixels : 0f;

            if (!Width.IsSet)
            {
                // if width is not explicitly set then adjust to content
                if (!percentageWidth)
                {
                    // add margins
                    totalWidth += isHorizontal ? totalSpacing : 0f;
                    totalWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels;
                    maxWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels;

                    // adjust width to content
                    Width.DirectValue = new ElementSize(isHorizontal ? totalWidth : maxWidth, ElementSizeUnit.Pixels);
                }
                else
                {
                    Width.DirectValue = new ElementSize(1, ElementSizeUnit.Percents);
                }
            }

            if (!Height.IsSet)
            {
                // if height is not explicitly set then adjust to content
                if (!percentageHeight)
                {
                    // add margins
                    totalHeight += !isHorizontal ? totalSpacing : 0f;
                    totalHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels;
                    maxHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels;

                    // adjust height to content
                    Height.DirectValue = new ElementSize(!isHorizontal ? totalHeight : maxHeight, ElementSizeUnit.Pixels);
                }
                else
                {
                    Height.DirectValue = new ElementSize(1, ElementSizeUnit.Percents);
                }
            }

            base.LayoutChanged();
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            _groupContentContainer = this;

            base.Initialize();            
        }
        
        #endregion
    }
}
