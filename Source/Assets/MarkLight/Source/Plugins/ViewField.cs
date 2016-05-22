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

        public T _value;

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

                return _value;
            }
            set
            {
                if (ParentView != null)
                {
                    ParentView.SetValue(ViewFieldPath, value);
                }
                else
                {
                    _value = value;
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
                    _value = value;
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

        #endregion
    }
}
