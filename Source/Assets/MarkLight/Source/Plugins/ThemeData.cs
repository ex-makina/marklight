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
        public Vector3 UnitSize;
        public string Xuml;
        public List<ThemeElementData> ThemeElementData;
        public bool BaseDirectorySet;
        public bool UnitSizeSet;

        [NonSerialized]
        private XElement _xumlElement;

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

                // add styles this style is based on
                try
                {
                    matchedThemeElements.AddRange(GetBasedOnThemeElementData(viewTypeName, themeElement.BasedOn));
                }
                catch (Exception e)
                {
                    Utils.LogError("[MarkLight] Unable to get theme data. Exception thrown: {0}", Utils.GetError(e));
                }
            }

            return matchedThemeElements;
        }

        /// <summary>
        /// Gets theme element data a style is based on.
        /// </summary>
        public List<ThemeElementData> GetBasedOnThemeElementData(string viewTypeName, string basedOnTheme)
        {
            var matchedThemeElements = new List<ThemeElementData>();

            if (string.IsNullOrEmpty(basedOnTheme))
                return matchedThemeElements;

            //Debug.Log(string.Format("GetThemeElementData started Type:{0} Based:{1}", viewTypeName, basedOnTheme));           
            foreach (var themeElement in _themeElementData[viewTypeName])
            {
                if (themeElement.Style == basedOnTheme)
                {
                    //Debug.Log(string.Format("foundMatch for {0}", basedOnTheme));
                    matchedThemeElements.Add(themeElement);
                    matchedThemeElements.AddRange(GetBasedOnThemeElementData(viewTypeName, themeElement.BasedOn));
                }
            }

            return matchedThemeElements;
        }

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
