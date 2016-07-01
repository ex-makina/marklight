#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MarkLight.Views.UI;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Provides access to a view pool.
    /// </summary>
    public class ViewPool
    {
        #region Fields

        public ViewPoolContainer ViewPoolContainer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewPool(ViewPoolContainer viewPoolContainer)
        {
            this.ViewPoolContainer = viewPoolContainer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a view into the view pool.
        /// </summary>
        public void InsertView(View view)
        {
            view.MoveTo(ViewPoolContainer, -1, false);
        }

        /// <summary>
        /// Gets first available view in the pool.
        /// </summary>
        public View GetView()
        {
            return ViewPoolContainer.GetChild(0);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets boolean indicating if pool is full.
        /// </summary>
        public bool IsFull
        {
            get
            {
                return ViewPoolContainer.IsFull;
            }
        }

        /// <summary>
        /// Gets boolean indicating if pool is empty.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return ViewPoolContainer.ChildCount <= 0;
            }
        }

        #endregion
    }
}
