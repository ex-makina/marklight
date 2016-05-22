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

namespace MarkLight.Examples.UI
{
    /// <summary>
    /// Examples demonstrating the view switcher.
    /// </summary>
    public class ViewSwitcherExample : View
    {
        public ViewSwitcher ContentViewSwitcher;

        public void Play()
        {
            ContentViewSwitcher.SwitchTo(1);
        }

        public void OnlinePlay()
        {
            ContentViewSwitcher.SwitchTo(2);
        }

        public void Back()
        {
            ContentViewSwitcher.Previous();
        }
    }
}

