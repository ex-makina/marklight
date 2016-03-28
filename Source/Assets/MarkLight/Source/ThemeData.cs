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
    /// Contains data about a theme.
    /// </summary>
    [Serializable]
    public class ThemeData
    {
        #region Fields

        public string ThemeName;
        public string BaseDirectory;
        public string Xml;
        public List<ThemeElementData> ThemeElementData;

        [NonSerialized]
        private XElement _xmlElement;

        [NonSerialized]
        private Dictionary<string, List<ThemeElementData>> _themeElementData;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        public ThemeData()
        {
            ThemeElementData = new List<ThemeElementData>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets theme element data for the specified view type, id and style.
        /// </summary>
        public List<ThemeElementData> GetThemeElementData(string viewTypeName, string id, string style)
        {
            List<ThemeElementData> matchedThemeElements = new List<ThemeElementData>();
            if (_themeElementData == null)
            {
                _themeElementData = new Dictionary<string, List<ThemeElementData>>();
                foreach (var themeElement in ThemeElementData)
                {
                    if (!_themeElementData.ContainsKey(themeElement.ViewName))
                    {
                        _themeElementData.Add(themeElement.ViewName, new List<ThemeElementData>());
                    }

                    _themeElementData[themeElement.ViewName].Add(themeElement);
                }
            }

            if (!_themeElementData.ContainsKey(viewTypeName))
            {
                return matchedThemeElements;
            }

            foreach (var themeElement in _themeElementData[viewTypeName])
            {
                // filter by Id
                if (!String.IsNullOrEmpty(themeElement.Id) &&
                    !String.Equals(themeElement.Id, id, StringComparison.OrdinalIgnoreCase))
                    continue;

                // filter by style
                if (!String.IsNullOrEmpty(themeElement.Style) &&
                    !String.Equals(themeElement.Style, style, StringComparison.OrdinalIgnoreCase))
                    continue;

                // we have a match
                matchedThemeElements.Add(themeElement);                
            }

            return matchedThemeElements;
        }

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
