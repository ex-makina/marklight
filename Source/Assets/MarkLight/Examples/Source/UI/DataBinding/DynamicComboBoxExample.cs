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
    /// Example demonstrating binding list data to a ComboBox view.
    /// </summary>
    [HideInPresenter]
    public class DynamicComboBoxExample : UIView
    {
        #region Fields

        public _bool CanRemove;
        public ObservableList<MenuItem> Menus;
        public ComboBox ComboBox;        

        public Label Label;
        private int _newMenuCount;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            _newMenuCount = 1;      
            Menus = new ObservableList<MenuItem>();
            Menus.Add(new MenuItem { Text = "Game", Description = "Game Settings", Color = Utils.GetRandomColor() });
            Menus.Add(new MenuItem { Text = "Graphics", Description = "Graphics Settings", Color = Utils.GetRandomColor() });
            Menus.Add(new MenuItem { Text = "Audio", Description = "Audio Settings", Color = Utils.GetRandomColor() });
            Menus.Add(new MenuItem { Text = "Controls", Description = "Control Settings", Color = Utils.GetRandomColor() });
        }

        /// <summary>
        /// Adds new item to the combo box.
        /// </summary>
        public void Add()
        {
            var newItem = new MenuItem { Text = "Menu " + _newMenuCount, Description = "Description", Color = Utils.GetRandomColor() };
            Menus.Add(newItem);

            // select the item we've added
            ComboBox.ComboBoxList.SelectItem(newItem);
            ++_newMenuCount;
        }

        /// <summary>
        /// Removes selected item(s) from the combo-box.
        /// </summary>
        public void Remove()
        {
            Menus.Remove(ComboBox.ComboBoxList.SelectedItems.Value);
            CanRemove.Value = Menus.Count > 1;

            // select last item in list
            ComboBox.ComboBoxList.SelectItem(Menus.LastOrDefault());
        }

        /// <summary>
        /// Called when an item in the combo box is selected.
        /// </summary>
        public void ItemSelected(ItemSelectionActionData actionData)
        {
            // activate remove button and label description
            CanRemove.Value =  Menus.Count > 1;
            Label.IsVisible.Value =  true;            
        }

        #endregion
    }
}

