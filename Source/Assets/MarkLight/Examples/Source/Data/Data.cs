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

namespace MarkLight.Examples.Data
{
    /// <summary>
    /// Highscore.
    /// </summary>
    public class Highscore
    {
        public int Score;
        public Player Player;
    }

    /// <summary>
    /// Player.
    /// </summary>
    public class Player
    {
        public string FirstName;
        public string LastName;
    }

    /// <summary>
    /// Menu items.
    /// </summary>
    public class MenuItem
    {
        public string Text;
        public string Description;
        public Color Color;
    }

    /// <summary>
    /// Card.
    /// </summary>
    public class Card
    {
        public Color Color;
        public string Name;
        public string ImagePath;
        public int CardRank;
        public CardSuit CardSuit;

        public string GetTemplateId()
        {
            // return template id for card
            int cardRank = CardRank - 10; // only support 4 cards in this example
            cardRank.Clamp(1, 4);

            return String.Format("Card{0}", cardRank);
        }
    }

    /// <summary>
    /// Defines card suit.
    /// </summary>
    public enum CardSuit
    {
        Undefined = 0,
        Clubs = 1,
        Diamonds = 2,
        Spades = 3,
        Hearts = 4
    }
}

