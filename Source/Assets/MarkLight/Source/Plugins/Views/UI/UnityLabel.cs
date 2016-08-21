#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Unity label view. 
    /// </summary>
    /// <d>View can be used when Text Mesh Pro integration is activated (replacing all Label views with Text Mesh Pro labels) but you still want to use the ordinary UGUI label.</d>
    [HideInPresenter]
    public class UnityLabel : Label
    {
    }
}
