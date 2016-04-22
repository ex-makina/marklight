#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Contains data about a theme element.
    /// </summary>
    [Serializable]
    public class ThemeElementData
    {
        #region Fields

        public string ViewName;
        public string Id;
        public string Style;
        public string BasedOn;
        public string Xuml;

        [NonSerialized]
        private XElement _xumlElement;

        #endregion        

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        public ThemeElementData()
        {
        }

        #endregion

        #region Methods
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets XUML element.
        /// </summary>
        public XElement XumlElement
        {
            get
            {
                if (_xumlElement == null && !String.IsNullOrEmpty(Xuml))
                {
                    try
                    {
                        _xumlElement = XElement.Parse(Xuml);
                    }
                    catch
                    {
                    }
                }

                return _xumlElement; 
            }
            set { _xumlElement = value; }
        }

        #endregion
    }
}
