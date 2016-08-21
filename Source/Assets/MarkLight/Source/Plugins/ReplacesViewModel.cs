#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Attribute indicating that this view-model replaces another with the specified name.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ReplacesViewModel : Attribute
    {
        #region Fields

        public string ViewTypeName;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ReplacesViewModel(string viewTypeName)
        {
            ViewTypeName = viewTypeName;
        }

        #endregion
    }
}
