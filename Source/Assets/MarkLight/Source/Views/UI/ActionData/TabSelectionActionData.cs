#region Using Statements
using MarkLight.Views.UI;
using System;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Tab selection action data.
    /// </summary>
    public class TabSelectionActionData : ActionData
    {
        #region Fields

        public Tab TabView;
        public object Item;
        public bool IsSelected;

        #endregion
    }
}
