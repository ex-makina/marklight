#region Using Statements
using MarkLight.Views.UI;
using System;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// Items changed action data.
    /// </summary>
    public class ListChangedActionData : ActionData
    {
        #region Fields

        public ListChangeAction ListChangeAction;
        public int StartIndex;
        public int EndIndex;
        public string FieldPath;

        #endregion
    }
}
