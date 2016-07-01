#region Using Statements
using MarkLight.ValueConverters;
using MarkLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using MarkLight.Views.UI;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Holds a list of sprites.
    /// </summary>
    /// <d>Base class for all view models in the framework. All view models must be a subclass of this class to be processed and managed the framework. </d>
    public class SpriteBundle : MonoBehaviour
    {
        #region Fields

        public List<Sprite> Sprites;

        #endregion
    }
}
