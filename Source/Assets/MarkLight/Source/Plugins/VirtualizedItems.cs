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
    public class VirtualizedItems
    {
        #region Fields

        public VirtualizedItemsContainer VirtualizedItemsContainer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public VirtualizedItems(VirtualizedItemsContainer virtualizedItemsContainer)
        {
            this.VirtualizedItemsContainer = virtualizedItemsContainer;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Inserts a view into the view pool.
        /// </summary>
        public void InsertView(View view)
        {
            view.MoveTo(VirtualizedItemsContainer);
        }

        /// <summary>
        /// Gets first available view in the pool.
        /// </summary>
        public View GetView()
        {
            return VirtualizedItemsContainer.GetChild(0);
        }

        #endregion

        #region Properties

        #endregion
    }
}
