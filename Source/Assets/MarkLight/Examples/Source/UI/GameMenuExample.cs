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
    /// Example demonstrating a game menu.
    /// </summary>
    public class GameMenuExample : View
    {
        public ViewSwitcher ContentViewSwitcher;
        public ObservableList<Level> Levels;

        // game settings
        public _bool EasyMode;
        public _float SoundEffectsVolume;
        public _float MusicVolume;
        public _string DisplayName;

        public void Play()
        {
            // switch to level select
            ContentViewSwitcher.SwitchTo(2);
        }

        public void Options()
        {
            // switch to options menu
            ContentViewSwitcher.SwitchTo(1);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void BackToMainMenu()
        {
            // get back to main menu
            ContentViewSwitcher.SwitchTo(0);
        }
        
        /// <summary>
        /// Called when level select button is clicked.
        /// </summary>
        public void StartLevel(Button levelSelectButton)
        {
            var level = levelSelectButton.Item.Value as Level;

            // switch to in-game, passing level data
            ContentViewSwitcher.SwitchTo(3, level, true);
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            System.Random random = new System.Random();
            Levels = new ObservableList<Level>();

            // generate levels
            for (int i = 0; i < 22; ++i)
            {
                Level newLevel = new Level();
                newLevel.Stars = random.Next(0, 4); // randomize number of stars (0 - 3)
                newLevel.Number = i + 1; // level number
                newLevel.IsLocked = i > 12; // lock all levels over 17
                Levels.Add(newLevel);
            }
        }
    }
}

