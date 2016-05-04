#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Resource binding source.
    /// </summary>
    public class ResourceBindingSource : BindingSource
    {
        #region Fields

        public string DictionaryName;
        public string ResourceKey;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ResourceBindingSource(string dictionaryName, string resourceKey)
        {
            DictionaryName = dictionaryName;
            ResourceKey = resourceKey;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets value from binding source.
        /// </summary>
        public override object GetValue(out bool hasValue)
        {
            return ResourceDictionary.GetValue(DictionaryName, ResourceKey, out hasValue);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets binding source string.
        /// </summary>
        public override string BindingSourceString
        {
            get
            {
                if (String.IsNullOrEmpty(DictionaryName))
                {
                    return String.Format("@{0}", ResourceKey);
                }
                else
                {
                    return String.Format("@{0}.{1}", DictionaryName, ResourceKey);
                }
            }
        }

        #endregion
    }
}
