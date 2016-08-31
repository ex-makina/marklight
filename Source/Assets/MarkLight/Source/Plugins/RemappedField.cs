#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Maps a dependency field to another field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RemappedField : Attribute
    {
        #region Fields

        public MapViewFieldData MapFieldData;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public RemappedField(string field, string to, string changeHandlerName = null, Type valueConverterType = null, bool triggerChangeHandlerImmediately = false)
        {
            MapFieldData = new MapViewFieldData();
            MapFieldData.From = field;
            MapFieldData.To = to;
            MapFieldData.TriggerChangeHandlerImmediately = triggerChangeHandlerImmediately;
            if (valueConverterType != null)
            {
                MapFieldData.ValueConverterType = valueConverterType.Name;
                MapFieldData.ValueConverterTypeSet = true;
            }

            if (changeHandlerName != null)
            {
                MapFieldData.ChangeHandlerName = changeHandlerName;
                MapFieldData.ChangeHandlerNameSet = true;
            }
        }

        #endregion
    }
}
