#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Attribute indicating additional view name alias that will map to this view-model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ViewNameAlias : Attribute
    {
        #region Fields

        public string ViewAlias;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewNameAlias(string viewName)
        {
            ViewAlias = viewName;
        }

        #endregion
    }
}
