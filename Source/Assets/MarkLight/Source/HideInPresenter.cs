#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Attribute indicating that the view is not to be shown in the view presenter's main view selection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class HideInPresenter : Attribute
    {
    }
}
