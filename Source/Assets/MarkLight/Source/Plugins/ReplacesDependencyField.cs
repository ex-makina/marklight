#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Attribute for replacing one dependency field with another.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReplacesDependencyField : Attribute
    {
        #region Fields

        public string ReplacedDependencyField;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ReplacesDependencyField(string replacedDependencyField)
        {
            ReplacedDependencyField = replacedDependencyField;
        }

        #endregion
    }
}
