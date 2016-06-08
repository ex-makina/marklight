#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Contains information about a view field path.
    /// </summary>
    public class ViewFieldPathInfo
    {
        #region Fields

        public string ViewFieldPath;
        public string TargetViewFieldPath;
        public List<MemberInfo> MemberInfo;
        public HashSet<string> Dependencies;
        public ValueConverter ValueConverter;
        public string ViewFieldTypeName;
        public Type ViewFieldType;
        public bool IsMapped;
        public bool IsPathParsed;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewFieldPathInfo()
        {
            MemberInfo = new List<MemberInfo>();
            Dependencies = new HashSet<string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets value at the end of the field path.
        /// </summary>
        public object GetValue(View sourceView, out bool hasValue)
        {
            hasValue = true;
            object viewFieldObject = sourceView;
            foreach (var memberInfo in MemberInfo)
            {
                viewFieldObject = memberInfo.GetFieldValue(viewFieldObject);
                if (viewFieldObject == null)
                {
                    hasValue = false;
                    return null; 
                }
            }

            return viewFieldObject;
        }

        /// <summary>
        /// Sets value and returns old value.
        /// </summary>
        public object SetValue(View sourceView, object value)
        {
            object oldValue = null;
            object viewFieldObject = sourceView;
            for (int i = 0; i < MemberInfo.Count; ++i)
            {
                bool lastMemberInfo = i == (MemberInfo.Count - 1);

                var memberInfo = MemberInfo[i];
                                
                if (lastMemberInfo)
                {
                    oldValue = memberInfo.GetFieldValue(viewFieldObject);

                    // set value
                    memberInfo.SetFieldValue(viewFieldObject, value);
                    return oldValue;
                }

                viewFieldObject = memberInfo.GetFieldValue(viewFieldObject);                
                if (viewFieldObject == null)
                    return null;
            }

            return null;
        }

        #endregion
    }
}
