#region Using Statements
using MarkLight.Views.UI;
using System;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Item selection action data.
    /// </summary>
    public class ItemSelectionActionData : ActionData
    {
        #region Fields

        public ListItem ItemView;
        public object Item;
        public bool IsSelected;

        #endregion
    }
}
