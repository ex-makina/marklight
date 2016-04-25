#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Attribute indicating that field is not to be set from XUML.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class NotSetFromXuml : Attribute
    {
    }
}
