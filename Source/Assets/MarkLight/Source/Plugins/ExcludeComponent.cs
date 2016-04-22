#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Attribute indicating that component isn't to be automatically added to the view.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class ExcludeComponent : Attribute
    {
        #region Fields

        public string ComponentFieldName;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ExcludeComponent(string componentFieldName)
        {
            ComponentFieldName = componentFieldName;
        }

        #endregion
    }
}
