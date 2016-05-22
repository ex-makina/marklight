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
    /// Examples demonstrating the view switcher.
    /// </summary>
    public class AdvancedLists : View
    {
        #region Fields

        public ObservableList<Card> Cards;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            Cards = new ObservableList<Card>();

            // generate 30 random cards
            for (int i = 1; i <= 32; ++i)
            {
                Color color = Utils.GetRandomColor();
                Cards.Add(new Card { Color = color, Name = "Card " + i });
            }
        }

        /// <summary>
        /// Adds new card to the list.
        /// </summary>
        public void Add()
        {
            var card = new Card { Color = Utils.GetRandomColor(), Name = "Card" };
            Cards.Add(card);            
        }

        /// <summary>
        /// Removes selected card from the list.
        /// </summary>
        public void Remove()
        {
            Cards.RemoveAt(Cards.SelectedIndex);
        }

        /// <summary>
        /// Scroll to card in list.
        /// </summary>
        public void ScrollTo()
        {
            Cards.ScrollTo(Cards.SelectedItem);
        }

        /// <summary>
        /// Scroll to card in list.
        /// </summary>
        public void ScrollToCenter()
        {
            Cards.ScrollTo(Cards.SelectedItem, ElementAlignment.Center);
        }

        /// <summary>
        /// Scroll to card in list.
        /// </summary>
        public void ScrollToTop()
        {
            Cards.ScrollTo(Cards.SelectedItem, ElementAlignment.Top);
        }

        #endregion
    }
}

