#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{   
    /// <summary>
    /// Generic base class for dependency view fields.
    /// </summary>
    public class ViewField<T> : ViewFieldBase
    {
        #region Fields

        public T _internalValue;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets view field notifying observers if the value has changed.
        /// </summary>
        public T Value
        {
            get
            {
                if (ParentView != null && IsMapped)
                {
                    return (T)ParentView.GetValue(ViewFieldPath);
                }

                return _internalValue;
            }
            set
            {
                if (ParentView != null)
                {
                    ParentView.SetValue(ViewFieldPath, value);
                }
                else
                {
                    InternalValue = value;
                    _isSet = true;
                }                
            }
        }

        /// <summary>
        /// Sets view field directly without notifying observers that the value has changed.
        /// </summary>
        public T DirectValue
        {
            set
            {
                if (ParentView != null && IsMapped)
                {
                    ParentView.SetValue(ViewFieldPath, value, true, null, null, false);
                }
                else
                {
                    _internalValue = value;
                    _isSet = true;
                }
            }
        }

        /// <summary>
        /// Gets boolean indicating if the value has been set. 
        /// </summary>
        public bool IsSet
        {
            get
            {
                if (ParentView != null)
                {
                    return ParentView.IsSet(ViewFieldPath);
                }
                else
                {
                    return _isSet;
                }
            }
        }

        /// <summary>
        /// Gets or sets internal value without considering mappings and without notifying observers.
        /// </summary>
        public T InternalValue
        {
            get
            {
                return _internalValue;
            }
            set
            {
                _internalValue = value;
                TriggerValueSet();
            }
        }

        #endregion
    }
}
