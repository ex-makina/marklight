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
        public Vector2 UnitSize;
        public static ValueConverterContext Empty = new ValueConverterContext();
        private static ValueConverterContext _defaultContext;

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
                if (_defaultContext == null)
                {
                    _defaultContext = new ValueConverterContext();
                    _defaultContext.BaseDirectory = ViewPresenter.Instance.BaseDirectory;
                    _defaultContext.UnitSize = ViewPresenter.Instance.UnitSize;
                }

                return _defaultContext;
            }
        }

        #endregion
    }
}
