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

namespace MarkLight.Examples.UI
{
    /// <summary>
    /// Example demonstrating layouting.
    /// </summary>
    public class LayoutExample : View
    {
        #region Fields

        public _bool IsChecked;
        public ObservableList<Highscore> Highscores;
        public _int GlobalScore;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            
            var random = new System.Random();
            Highscores = new ObservableList<Highscore>();
            for (int i = 1; i <= 100; ++i)
            {
                Highscores.Add(new Highscore { Player = new Player { FirstName = "Player " + i }, Score = random.Next(100, 10000) });
            }
        }

        public void ChangeScore()
        {
            GlobalScore.Value += 100;
        }

        public void Remove()
        {
            if (Highscores.SelectedItem != null)
            {
                Highscores.Remove(Highscores.SelectedItem);
            }
        }        

        #endregion
    }
}

