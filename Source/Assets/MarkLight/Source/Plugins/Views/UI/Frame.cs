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
    /// Frame view.
    /// </summary>
    /// <d>The frame resizes itself to its content by default.</d>
    [HideInPresenter]
    public class Frame : UIView
    {
        #region Fields

        /// <summary>
        /// Indicates if the view should resize to content.
        /// </summary>
        /// <d>Resizes the view to the size of its children.</d>
        [ChangeHandler("LayoutChanged")]
        public _bool ResizeToContent;

        /// <summary>
        /// Content margin.
        /// </summary>
        /// <d>The margin of the content of this view.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementMargin ContentMargin;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            ContentMargin.DirectValue = new ElementMargin();
            ResizeToContent.DirectValue = true;
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
            if (ResizeToContent)
            {
                float maxWidth = 0f;
                float maxHeight = 0f;
                int childCount = ChildCount;

                // get size of content and set content offsets and alignment
                for (int i = 0; i < childCount; ++i)
                {
                    var go = transform.GetChild(i);
                    var view = go.GetComponent<UIView>();
                    if (view == null)
                        continue;

                    // get size of content
                    if (view.Width.Value.Unit != ElementSizeUnit.Percents)
                    {
                        maxWidth = view.Width.Value.Pixels > maxWidth ? view.Width.Value.Pixels : maxWidth;
                    }

                    if (view.Height.Value.Unit != ElementSizeUnit.Percents)
                    {
                        maxHeight = view.Height.Value.Pixels > maxHeight ? view.Height.Value.Pixels : maxHeight;
                    }
                }

                // add margins
                maxWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels + ContentMargin.Value.Left.Pixels + ContentMargin.Value.Right.Pixels;
                maxHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels + ContentMargin.Value.Bottom.Pixels + ContentMargin.Value.Top.Pixels;

                // adjust size to content unless it has been set
                if (!Width.IsSet)
                {
                    Width.DirectValue = new ElementSize(maxWidth);
                }

                if (!Height.IsSet)
                {
                    Height.DirectValue = new ElementSize(maxHeight);
                }
            }

            base.LayoutChanged();
        }

        #endregion
    }
}
