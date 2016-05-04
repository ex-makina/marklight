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
    /// TabHeader view.
    /// </summary>
    /// <d>Displays the content of a tab header in the tab panel. Has the states: Default, Disabled, Highlighted, Pressed and Selected.</d>
    [HideInPresenter]
    public class TabHeader : ListItem
    {
        #region Fields

        public Tab ParentTab;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes internal default values of the view.
        /// </summary>
        public override void InitializeInternalDefaultValues()
        {
            base.InitializeInternalDefaultValues();
            if (ParentTab == null)
            {
                ParentTab = this.FindParent<Tab>();
            }
        }

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
        }

        #endregion
    }
}
