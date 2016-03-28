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
    /// Example demonstrating binding list data to the List view.
    /// </summary>
    [HideInPresenter]
    public class DynamicListExample : UIView
    {
        #region Fields

        public ObservableList<Highscore> Highscores;
        public List HighscoresList;
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
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Aldon", LastName = "Rusl" }});
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Harris", LastName = "Alin" }});
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Damari", LastName = "Arnaf" }});
            Highscores.Add(new Highscore { Player = new Player { FirstName = "Chindler", LastName = "Larris" }});            
        }

        /// <summary>
        /// Adds new item to the list.
        /// </summary>
        public void Add()
        {
            ++_newPlayerCounter;
            Highscores.Add(new Highscore { Player = new Player { FirstName = "New Player " + _newPlayerCounter, LastName = "" }});            
        }

        /// <summary>
        /// Removes selected item(s) from the list.
        /// </summary>
        public void Remove()
        {
            Highscores.Remove(HighscoresList.SelectedItems.Value);
        }

        #endregion
    }    
}

