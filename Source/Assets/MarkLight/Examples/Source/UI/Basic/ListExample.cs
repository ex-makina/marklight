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

namespace MarkLight.Examples.UI.Basic
{
    /// <summary>
    /// Example demonstrating the list view.
    /// </summary>
    [HideInPresenter]
    public class ListExample : UIView
    {
        #region Fields

        public Label SelectedItemLabel;

        #endregion

        #region Methods

        /// <summary>
        /// Adds new item to the list.
        /// </summary>
        public void ListItemSelected(ItemSelectionActionData itemSelectData)
        {
            SelectedItemLabel.Text.Value = itemSelectData.ItemView.Text;
        }

        #endregion
    }
}

