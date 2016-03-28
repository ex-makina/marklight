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
        public string Xml;

        [NonSerialized]
        private XElement _xmlElement;

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
        /// Gets or sets XML element.
        /// </summary>
        public XElement XmlElement
        {
            get
            {
                if (_xmlElement == null && !String.IsNullOrEmpty(Xml))
                {
                    try
                    {
                        _xmlElement = XElement.Parse(Xml);
                    }
                    catch
                    {
                    }
                }

                return _xmlElement; 
            }
            set { _xmlElement = value; }
        }

        #endregion
    }
}
