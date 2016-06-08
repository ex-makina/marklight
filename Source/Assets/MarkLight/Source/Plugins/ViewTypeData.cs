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
    /// Contains data about a specific view type.
    /// </summary>
    [Serializable]
    public class ViewTypeData
    {
        #region Fields

        public string ViewName;
        public string Xuml;
        public List<string> DependencyNames;
        public List<string> ViewActionFields;
        public List<string> DependencyFields;
        public List<string> ComponentFields;
        public List<string> ReferenceFields;
        public List<string> FieldsNotSetFromXuml;
        public List<string> ExcludedComponentFields;
        public List<string> ViewFields;
        public List<MapViewFieldData> MapViewFields;
        public List<ViewFieldConverterData> ViewFieldConverters;
        public List<ViewFieldChangeHandler> ViewFieldChangeHandlers;
        public bool PermanentMark;
        public bool TemporaryMark;
        public bool HideInPresenter;

        [NonSerialized]
        private XElement _xumlElement;

        [NonSerialized]
        private List<ViewTypeData> _dependencies;

        [NonSerialized]
        private Dictionary<string, MapViewFieldData> _mappedViewFields;

        [NonSerialized]
        private Dictionary<string, ViewFieldConverterData> _viewFieldConverters;

        [NonSerialized]
        private Dictionary<string, ViewFieldChangeHandler> _viewFieldChangeHandlers;

        [NonSerialized]
        private Dictionary<string, FieldInfo> _viewFields;

        [NonSerialized]
        private Dictionary<string, ViewFieldPathInfo> _viewFieldPathInfo;

        #endregion        

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class
        /// </summary>
        public ViewTypeData()
        {
            DependencyNames = new List<string>();
            ViewActionFields = new List<string>();
            DependencyFields = new List<string>();
            ComponentFields = new List<string>();
            ReferenceFields = new List<string>();
            FieldsNotSetFromXuml = new List<string>();
            ExcludedComponentFields = new List<string>();
            ViewFields = new List<string>();
            MapViewFields = new List<MapViewFieldData>();
            ViewFieldConverters = new List<ViewFieldConverterData>();
            ViewFieldChangeHandlers = new List<ViewFieldChangeHandler>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets mapped view field. Returns the same field if no mapped field exist.
        /// </summary>
        public string GetMappedViewField(string viewField)
        {
            if (_mappedViewFields == null)
            {
                _mappedViewFields = new Dictionary<string, MapViewFieldData>();
                foreach (var mapField in MapViewFields)
                {
                    try
                    {
                        _mappedViewFields.Add(mapField.From, mapField);
                    }
                    catch
                    {
                        Utils.LogError("[MarkLight] View type \"{0}\" contains duplicate mapped view field \"{1} -> {2}\".", ViewName, mapField.From, mapField.To);
                    }
                }
            }
            return _mappedViewFields.ContainsKey(viewField) ? _mappedViewFields[viewField].To : viewField;
        }

        /// <summary>
        /// Gets value converter for view field.
        /// </summary>
        public ValueConverter GetViewFieldValueConverter(string viewField)
        {
            if (_viewFieldConverters == null)
            {
                _viewFieldConverters = new Dictionary<string, ViewFieldConverterData>();
                foreach (var converter in ViewFieldConverters)
                {
                    _viewFieldConverters.Add(converter.ViewField, converter);
                }
            }

            if (_viewFieldConverters.ContainsKey(viewField))
            {
                return ViewData.GetValueConverter(_viewFieldConverters[viewField].ValueConverterType);
            }
            return null;
        }

        /// <summary>
        /// Gets view field change handler.
        /// </summary>
        public string GetViewFieldChangeHandler(string viewField)
        {
            if (_viewFieldChangeHandlers == null)
            {
                _viewFieldChangeHandlers = new Dictionary<string, ViewFieldChangeHandler>();
            }

            if (_viewFieldChangeHandlers.ContainsKey(viewField))
            {
                return _viewFieldChangeHandlers[viewField].ChangeHandlerName;
            }

            return null;
        }

        /// <summary>
        /// Returns view field.
        /// </summary>
        public FieldInfo GetViewField(string field)
        {
            if (_viewFields == null)
            {
                _viewFields = new Dictionary<string, FieldInfo>();
                var viewType = ViewData.GetViewType(ViewName);                
                foreach (var viewField in viewType.GetFields())
                {
                    _viewFields.Add(viewField.Name, viewField);                    
                }
            }

            return _viewFields.Get(field);
        }

        /// <summary>
        /// Gets view field path info for the field.
        /// </summary>
        public ViewFieldPathInfo GetViewFieldPathInfo(string viewFieldPath)
        {
            return _viewFieldPathInfo != null ? _viewFieldPathInfo.Get(viewFieldPath) : null;
        }

        /// <summary>
        /// Adds view field path info.
        /// </summary>
        public void AddViewFieldPathInfo(string viewFieldPath, ViewFieldPathInfo viewFieldPathInfo)
        {
            if (_viewFieldPathInfo == null)
            {
                _viewFieldPathInfo = new Dictionary<string, ViewFieldPathInfo>();
            }

            _viewFieldPathInfo.Add(viewFieldPath, viewFieldPathInfo);
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

        /// <summary>
        /// Gets or sets view dependencies.
        /// </summary>
        public List<ViewTypeData> Dependencies
        {
            get 
            { 
                if (_dependencies == null)
                {
                    _dependencies = new List<ViewTypeData>();
                }
                return _dependencies; 
            }
            set { _dependencies = value; }
        }

        #endregion
    }

    /// <summary>
    /// Contains information about a mapped view field.
    /// </summary>
    [Serializable]
    public class MapViewFieldData
    {
        public string From;
        public string To;
        public string ValueConverterType;
        public bool ValueConverterTypeSet;
        public string ChangeHandlerName;
        public bool ChangeHandlerNameSet;
        public bool TriggerChangeHandlerImmediately;
    }

    /// <summary>
    /// Contains information about a mapped view field.
    /// </summary>
    [Serializable]
    public class ViewFieldConverterData
    {
        public string ViewField;
        public string ValueConverterType;
    }

    /// <summary>
    /// Contains information about a change handler.
    /// </summary>
    [Serializable]
    public class ViewFieldChangeHandler
    {
        public string ViewField;
        public string ChangeHandlerName;
        public bool TriggerImmediately;
    }
}
