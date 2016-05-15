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
    /// Example demonstrating binding list data to a DataGrid view.
    /// </summary>
    [HideInPresenter]
    public class DynamicDataGridExample : UIView
    {
        #region Fields

        public ObservableList<Highscore> Highscores;
        public DataGrid DataGrid;
        private int _newPlayerCounter;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            Highscores = new ObservableList<Highscore>();
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Aldon", LastName = "Rusl" }, Score = 14953 });
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Harris", LastName = "Alin" }, Score = 7396 });
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Damari", LastName = "Arnaf" }, Score = 3593 });
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Chindler", LastName = "Larris" }, Score = 8593 });
        }

        /// <summary>
        /// Adds new item to the data grid.
        /// </summary>
        public void Add()
        {
            ++_newPlayerCounter;
            System.Random random = new System.Random();
            Highscores.Add(new Highscore { Player = new Player { FirstName = "New Player " + _newPlayerCounter, LastName = "" }, Score = random.Next(20000) });
        }

        /// <summary>
        /// Removes selected item(s) from the data grid.
        /// </summary>
        public void Remove()
        {
            Highscores.Remove(DataGrid.DataGridList.SelectedItems.Value);
        }

        /// <summary>
        /// Called when row is selected in the grid.
        /// </summary>
        public void ItemSelected()
        {
            // var selectedItem = Highscores.SelectedItem;
        }

        #endregion
    }    
}

