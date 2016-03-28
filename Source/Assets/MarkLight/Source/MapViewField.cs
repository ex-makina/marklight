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
    /// Maps one field to another.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class MapViewField : Attribute
    {
        #region Fields

        public MapViewFieldData MapFieldData;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MapViewField(string from, string to, string changeHandlerName = null, Type valueConverterType = null, bool triggerChangeHandlerImmediately = false)
        {
            MapFieldData = new MapViewFieldData();
            MapFieldData.From = from;
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

        #region Methods
        #endregion
    }
}
