#region Using Statements
using MarkLight.Views.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Virtualized items to be presented on demand.
    /// </summary>
    [HideInPresenter]
    public class VirtualizedItemsContainer : UIView
    {
        #region Fields

        public View Owner;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();        
        }

        #endregion

        #region Properties
        #endregion
    }
}
