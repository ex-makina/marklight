#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Attribute for replacing one component field with another.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class ReplacesComponentField : Attribute
    {
        #region Fields

        public string ReplacedComponentField;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ReplacesComponentField(string replacedComponentField)
        {
            ReplacedComponentField = replacedComponentField;
        }

        #endregion
    }
}
