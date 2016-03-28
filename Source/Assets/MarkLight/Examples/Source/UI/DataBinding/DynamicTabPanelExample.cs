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

namespace MarkLight.Examples.UI.DataBinding
{
    /// <summary>
    /// Example demonstrating binding list data to a TabPanel view.
    /// </summary>
    [HideInPresenter]
    public class DynamicTabPanelExample : UIView
    {
        #region Fields

        public TabPanel TabPanel;
        public ObservableList<MenuItem> Menus;
        public _bool CanAdd;
        public _bool CanRemove;

        private int _newTabCount;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            CanAdd.DirectValue = true;
            CanRemove.DirectValue = true;
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
                        
            Menus = new ObservableList<MenuItem>();
            Menus.Add(new MenuItem { Text = "Game", Description = "Game Settings", Color = Utils.GetRandomColor() });
            Menus.Add(new MenuItem { Text = "Graphics", Description = "Graphics Settings", Color = Utils.GetRandomColor() });
            Menus.Add(new MenuItem { Text = "Audio", Description = "Audio Settings", Color = Utils.GetRandomColor() });
            Menus.Add(new MenuItem { Text = "Controls", Description = "Control Settings", Color = Utils.GetRandomColor() });
        }

        /// <summary>
        /// Adds a new tab.
        /// </summary>
        public void AddTab()
        {
            _newTabCount = _newTabCount < 9 ? ++_newTabCount : 1;
            Menus.Add(new MenuItem { Text = "New Tab " + _newTabCount, Description = "Description", Color = Utils.GetRandomColor() });            

            CanAdd.Value = Menus.Count < 5;
            CanRemove.Value = true;
        }

        /// <summary>
        /// Removes selected tab.
        /// </summary>
        public void RemoveTab()
        {
            Menus.Remove(TabPanel.SelectedItem.Value);

            CanAdd.Value = Menus.Count < 5;
            CanRemove.Value = Menus.Count > 1;
        }

        #endregion
    }    
}

