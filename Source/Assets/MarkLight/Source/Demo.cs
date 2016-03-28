#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Region view.
    /// </summary>
    [HideInPresenter]
    public class DemoMessage : View
    {
        public Label Label;
        public void UpdateMessage()
        {
            Label.UpdateRectTransform.Value = false;
            Label.RectTransform.localPosition = new Vector3(-500,0);            
        }
    }
}
