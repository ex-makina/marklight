#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Contains data about a view field.
    /// </summary>
    public class ViewFieldData
    {
        #region Fields

        public View SourceView;
        public View TargetView;        
        public bool TargetViewSet;
        public ViewFieldPathInfo ViewFieldPathInfo;

        public bool SevereParseError;
        public bool PropagateFirst;

        private HashSet<ValueObserver> _valueObservers;
        private bool _isSet;
        private bool _isSetInitialized;

        #endregion

        #region Methods

        /// <summary>
        /// Sets value of field.
        /// </summary>
        public object SetValue(object inValue, HashSet<ViewFieldData> callstack, bool updateDefaultState = true,
            ValueConverterContext context = null, bool notifyObservers = true)
        {
            if (callstack.Contains(this))
                return null;

            callstack.Add(this);

            if (!IsOwner)
            {
                var targetView = GetTargetView();
                if (targetView == null)
                {
                    Utils.LogError("[MarkLight] {0}: Unable to assign value \"{1}\" to view field \"{2}\". View along path is null.", SourceView.GameObjectName, inValue, ViewFieldPath);
                    return null;
                }

                return targetView.SetValue(TargetViewFieldPath, inValue, updateDefaultState, callstack, null, notifyObservers);
            }

            // check if path has been parsed
            if (!IsPathParsed)
            {
                // attempt to parse path
                if (!ParseViewFieldPath())
                {
                    // path can't be resolved at this point
                    if (SevereParseError)
                    {
                        // severe parse error means the path is incorrect
                        Utils.LogError("[MarkLight] {0}: Unable to assign value \"{1}\". {2}", SourceView.GameObjectName, inValue, Utils.ErrorMessage);
                    }

                    // unsevere parse errors can be expected, e.g. value along path is null
                    return null;
                }
            }

            object value = inValue;
            if (context == null)
            {
                if (SourceView.ValueConverterContext != null)
                {
                    context = SourceView.ValueConverterContext;
                }
                else
                {
                    context = ValueConverterContext.Default;
                }
            }

            // get converted value            
            if (ValueConverter != null)
            {
                var conversionResult = ValueConverter.Convert(value, context);
                if (!conversionResult.Success)
                {
                    Utils.LogError("[MarkLight] {0}: Unable to assign value \"{1}\" to view field \"{2}\". Value converion failed. {3}", SourceView.GameObjectName, value, ViewFieldPath, conversionResult.ErrorMessage);
                    return null;
                }
                value = conversionResult.ConvertedValue;
            }

            // set value
            object oldValue = ViewFieldPathInfo.SetValue(SourceView, value);

            // notify observers if the value has changed
            if (notifyObservers)
            {
                // set isSet-indicator
                SetIsSet();

                bool valueChanged = value != null ? !value.Equals(oldValue) : oldValue != null;
                if (valueChanged)
                {
                    NotifyValueObservers(callstack);

                    // find dependent view fields and notify their value observers
                    SourceView.NotifyDependentValueObservers(ViewFieldPath);
                }
            }

            return value;
        }

        /// <summary>
        /// Sets isSet-indicator.
        /// </summary>
        public void SetIsSet()
        {
            if (!_isSetInitialized)
            {
                SourceView.AddIsSetField(ViewFieldPath);
                _isSetInitialized = true;
            }
            _isSet = true;
        }

        /// <summary>
        /// Gets value of field.
        /// </summary>
        public object GetValue(out bool hasValue)
        {
            if (!IsOwner)
            {
                var targetView = GetTargetView();
                if (targetView == null)
                {
                    hasValue = false;
                    //Utils.LogError("[MarkLight] {0}: Unable to get value from view field \"{1}\". View along path is null.", SourceView.GameObjectName, ViewFieldPath));
                    return null;
                }

                return targetView.GetValue(TargetViewFieldPath, out hasValue);
            }
            else
            {
                // check if path has been parsed
                if (!IsPathParsed)
                {
                    // attempt to parse path
                    if (!ParseViewFieldPath())
                    {
                        hasValue = false;
                        return null;
                    }
                }

                return ViewFieldPathInfo.GetValue(SourceView, out hasValue);
            }
        }

        /// <summary>
        /// Registers a value observer.
        /// </summary>
        public void RegisterValueObserver(ValueObserver valueObserver)
        {
            if (_valueObservers == null)
            {
                _valueObservers = new HashSet<ValueObserver>();
            }

            _valueObservers.Add(valueObserver);
        }

        /// <summary>
        /// Notifies all value observers that value has been set.
        /// </summary>
        public void NotifyValueObservers(HashSet<ViewFieldData> callstack)
        {
            if (_valueObservers == null)
                return;

            List<ValueObserver> removedObservers = null;
            foreach (var valueObserver in _valueObservers)
            {
                // notify observer
                bool isRemoved = !valueObserver.Notify(callstack);
                if (isRemoved)
                {
                    if (removedObservers == null)
                    {
                        removedObservers = new List<ValueObserver>();
                    }

                    removedObservers.Add(valueObserver);
                }
            }

            if (removedObservers != null)
            {
                removedObservers.ForEach(x => _valueObservers.Remove(x));
            }
        }

        /// <summary>
        /// Notifies all binding value observers that value has been set.
        /// </summary>
        public void NotifyBindingValueObservers(HashSet<ViewFieldData> callstack)
        {
            if (_valueObservers == null)
                return;

            List<ValueObserver> removedObservers = null;
            foreach (var valueObserver in _valueObservers)
            {
                if (valueObserver is BindingValueObserver)
                {
                    bool isRemoved = !valueObserver.Notify(callstack);
                    if (isRemoved)
                    {
                        if (removedObservers == null)
                        {
                            removedObservers = new List<ValueObserver>();
                        }

                        removedObservers.Add(valueObserver);
                    }
                }
            }

            if (removedObservers != null)
            {
                removedObservers.ForEach(x => _valueObservers.Remove(x));
            }
        }

        /// <summary>
        /// Notifies all change handler value observers that value has been set.
        /// </summary>
        public void NotifyChangeHandlerValueObservers(HashSet<ViewFieldData> callstack)
        {
            if (_valueObservers == null)
                return;

            foreach (var valueObserver in _valueObservers)
            {
                if (valueObserver is ChangeHandlerValueObserver)
                {
                    valueObserver.Notify(callstack);
                }
            }
        }

        /// <summary>
        /// Gets field data from field path.
        /// </summary>
        public static ViewFieldData FromViewFieldPath(View sourceView, string viewFieldPath)
        {
            if (String.IsNullOrEmpty(viewFieldPath) || sourceView == null)
                return null;

            ViewFieldData fieldData = new ViewFieldData();            
            fieldData.TargetView = sourceView;
            fieldData.SourceView = sourceView;

            var viewTypeData = sourceView.ViewTypeData;
            var viewFieldPathInfo = viewTypeData.GetViewFieldPathInfo(viewFieldPath);
            if (viewFieldPathInfo != null)
            {
                fieldData.ViewFieldPathInfo = viewFieldPathInfo;
                return fieldData;
            }
            else
            {
                fieldData.ViewFieldPathInfo = new ViewFieldPathInfo();
                fieldData.ViewFieldPathInfo.ViewFieldTypeName = sourceView.ViewTypeName;
                fieldData.ViewFieldPathInfo.ViewFieldPath = viewFieldPath;
                fieldData.ViewFieldPathInfo.TargetViewFieldPath = viewFieldPath;
            }

            Type viewType = typeof(View);
            var viewFields = viewFieldPath.Split('.');

            // do we have a view field path consisting of multiple view fields?
            if (viewFields.Length > 1)
            {
                // yes. get first view field
                var firstViewField = viewFields[0];

                // is this a field that refers to another view?
                var fieldInfo = sourceView.GetType().GetField(firstViewField);
                if (fieldInfo != null && viewType.IsAssignableFrom(fieldInfo.FieldType))
                {
                    // yes. set target view and return
                    fieldData.ViewFieldPathInfo.TargetViewFieldPath = String.Join(".", viewFields.Skip(1).ToArray());
                    fieldData.ViewFieldPathInfo.MemberInfo.Add(fieldInfo);
                    fieldData.ViewFieldPathInfo.IsMapped = true;
                    fieldData.ViewFieldPathInfo.IsPathParsed = true;
                    fieldData.TargetViewSet = false;                    
                    viewTypeData.AddViewFieldPathInfo(viewFieldPath, fieldData.ViewFieldPathInfo);
                    return fieldData;
                }

                // is this a property that refers to a view?
                var propertyInfo = sourceView.GetType().GetProperty(firstViewField);
                if (propertyInfo != null && viewType.IsAssignableFrom(propertyInfo.PropertyType))
                {
                    // yes. set target view and return
                    fieldData.ViewFieldPathInfo.TargetViewFieldPath = String.Join(".", viewFields.Skip(1).ToArray());
                    fieldData.ViewFieldPathInfo.MemberInfo.Add(propertyInfo);
                    fieldData.ViewFieldPathInfo.IsMapped = true;
                    fieldData.ViewFieldPathInfo.IsPathParsed = true;
                    fieldData.TargetViewSet = false;                    
                    viewTypeData.AddViewFieldPathInfo(viewFieldPath, fieldData.ViewFieldPathInfo);
                    return fieldData;
                }

                // does first view field or property exist?
                if (fieldInfo == null && propertyInfo == null)
                {
                    // no. check if it refers to a view in the hierarchy
                    var result = fieldData.TargetView.Find<View>(x => x.Id == viewFields[0], true, fieldData.TargetView);
                    if (result == null)
                    {
                        // no. assume that it refers to this view (in cases like x.SetValue(() => x.Field, value))
                        return FromViewFieldPath(sourceView, String.Join(".", viewFields.Skip(1).ToArray()));
                    }

                    // view found
                    fieldData.ViewFieldPathInfo.TargetViewFieldPath = String.Join(".", viewFields.Skip(1).ToArray());
                    fieldData.ViewFieldPathInfo.IsMapped = true;
                    fieldData.TargetViewSet = true;
                    fieldData.TargetView = result;
                    return fieldData;
                }
            }

            // try parse the path
            fieldData.ParseViewFieldPath();
            return fieldData;
        }

        /// <summary>
        /// Tries to parse the view field path and get view field path info. Called only if we're the owner of the field.
        /// </summary>
        public bool ParseViewFieldPath()
        {
            SevereParseError = false;

            var viewTypeData = SourceView.ViewTypeData;
            var viewFieldPath = viewTypeData.GetViewFieldPathInfo(ViewFieldPath); 
            if (viewFieldPath != null)
            {
                ViewFieldPathInfo = viewFieldPath;
                return true;
            }

            ViewFieldPathInfo.IsPathParsed = false;
            ViewFieldPathInfo.MemberInfo.Clear();
            ViewFieldPathInfo.Dependencies.Clear();

            // if we get here we are the owner of the field and need to parse the path
            ViewFieldPathInfo.ValueConverter = viewTypeData.GetViewFieldValueConverter(ViewFieldPath);

            //Type viewFieldType = SourceView.GetType();
            var viewFields = ViewFieldPath.Split('.');
            object viewFieldObject = SourceView;
            var viewFieldBaseType = typeof(ViewFieldBase);

            // parse view field path
            bool parseSuccess = true;
            string dependencyPath = string.Empty;
            for (int i = 0; i < viewFields.Length; ++i)
            {
                bool isLastField = (i == viewFields.Length - 1);
                string viewField = viewFields[i];

                // add dependency
                if (!isLastField)
                {
                    dependencyPath += (i > 0 ? "." : "") + viewField;
                    ViewFieldPathInfo.Dependencies.Add(dependencyPath);
                }

                if (!parseSuccess)
                {
                    continue;
                }
                                
                var viewFieldType = viewFieldObject.GetType();
                var memberInfo = viewFieldType.GetFieldInfo(viewField);
                if (memberInfo == null)
                {
                    SevereParseError = true;
                    Utils.ErrorMessage = String.Format("Unable to parse view field path \"{0}\". Couldn't find member with the name \"{1}\".", ViewFieldPath, viewField);
                    return false;
                }

                ViewFieldPathInfo.MemberInfo.Add(memberInfo);
                ViewFieldPathInfo.ViewFieldType = memberInfo.GetFieldType();

                // handle special ViewFieldBase types
                if (viewFieldBaseType.IsAssignableFrom(ViewFieldPathInfo.ViewFieldType))
                {
                    viewFieldObject = memberInfo.GetFieldValue(viewFieldObject);
                    if (viewFieldObject == null)
                    {
                        Utils.ErrorMessage = String.Format("Unable to parse view field path \"{0}\". Field/property with the name \"{1}\" was null.", ViewFieldPath, viewField);
                        parseSuccess = false;
                        continue;
                    }

                    memberInfo = ViewFieldPathInfo.ViewFieldType.GetProperty("InternalValue"); // set internal dependency view field value
                    ViewFieldPathInfo.MemberInfo.Add(memberInfo);
                    ViewFieldPathInfo.ViewFieldType = memberInfo.GetFieldType();
                }

                if (isLastField)
                {
                    ViewFieldPathInfo.ViewFieldType = memberInfo.GetFieldType();
                    ViewFieldPathInfo.ViewFieldTypeName = ViewFieldPathInfo.ViewFieldType.Name;
                    ViewFieldPathInfo.ValueConverter = ValueConverter ?? ViewData.GetValueConverterForType(ViewFieldTypeName);

                    // handle special case if converter is null and field type is enum
                    if (ValueConverter == null && ViewFieldPathInfo.ViewFieldType.IsEnum())
                    {
                        ViewFieldPathInfo.ValueConverter = new EnumValueConverter(ViewFieldPathInfo.ViewFieldType);
                    }
                }
                else
                {
                    viewFieldObject = memberInfo.GetFieldValue(viewFieldObject);
                }

                if (viewFieldObject == null)
                {
                    Utils.ErrorMessage = String.Format("Unable to parse view field path \"{0}\". Field/property with the name \"{1}\" was null.", ViewFieldPath, viewField);
                    parseSuccess = false;
                    continue;
                }
            }

            ViewFieldPathInfo.IsPathParsed = parseSuccess;
            if (parseSuccess)
            {
                viewTypeData.AddViewFieldPathInfo(ViewFieldPath, ViewFieldPathInfo);
            }

            return parseSuccess;
        }

        /// <summary>
        /// Gets target view. Only called if this view isn't the owner.
        /// </summary>
        public View GetTargetView()
        {
            if (TargetViewSet)
            {
                return TargetView;
            }

            bool hasValue;
            return ViewFieldPathInfo.GetValue(SourceView, out hasValue) as View;
        }

        /// <summary>
        /// Gets bool indicating if the view field has been set.
        /// </summary>
        public bool IsSet()
        {
            if (_isSetInitialized || _isSet) 
                return _isSet;

            // check with source view if the view field has been set
            _isSetInitialized = true;
            _isSet = SourceView.GetIsSetFieldValue(ViewFieldPath);
            return _isSet;
        }

        #endregion


        #region Properties

        /// <summary>
        /// Gets view field value converter.
        /// </summary>
        public ValueConverter ValueConverter
        {
            get
            {
                return ViewFieldPathInfo.ValueConverter;
            }
        }

        /// <summary>
        /// Gets view field type name.
        /// </summary>
        public string ViewFieldTypeName
        {
            get
            {
                return ViewFieldPathInfo.ViewFieldTypeName;
            }
        }

        /// <summary>
        /// Gets view field type.
        /// </summary>
        public Type ViewFieldType
        {
            get
            {
                return ViewFieldPathInfo.ViewFieldType;
            }
        }

        /// <summary>
        /// Gets view field path.
        /// </summary>
        public string ViewFieldPath
        {
            get
            {
                return ViewFieldPathInfo.ViewFieldPath;
            }
        }

        /// <summary>
        /// Gets target view field path.
        /// </summary>
        public string TargetViewFieldPath
        {
            get
            {
                return ViewFieldPathInfo.TargetViewFieldPath;
            }
        }

        /// <summary>
        /// Gets boolean indicating if path has been parsed.
        /// </summary>
        public bool IsPathParsed
        {
            get
            {
                return ViewFieldPathInfo.IsPathParsed;
            }
        }

        /// <summary>
        /// Returns boolean indicating if this view field is the owner of the value (not mapped to another view).
        /// </summary>
        public bool IsOwner
        {
            get
            {
                return !ViewFieldPathInfo.IsMapped;
            }
        }

        #endregion
    }
}
