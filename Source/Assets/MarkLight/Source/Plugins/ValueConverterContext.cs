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
    /// Contains information about the context in which a value conversion occurs.
    /// </summary>
    [Serializable]
    public class ValueConverterContext
    {
        #region Fields

        public string BaseDirectory;
        public Vector3 UnitSize;
        public static ValueConverterContext Empty = new ValueConverterContext();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ValueConverterContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ValueConverterContext(ValueConverterContext oldContext)
        {
            if (oldContext != null)
            {
                BaseDirectory = oldContext.BaseDirectory;
                UnitSize = oldContext.UnitSize;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets default value converter context.
        /// </summary>
        public static ValueConverterContext Default
        {
            get
            {
                return new ValueConverterContext
                {
                    BaseDirectory = ViewPresenter.Instance.BaseDirectory,
                    UnitSize = ViewPresenter.Instance.UnitSize
                };
            }
        }

        #endregion
    }
}
