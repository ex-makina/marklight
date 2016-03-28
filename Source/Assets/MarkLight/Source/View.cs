#region Using Statements
using MarkLight.ValueConverters;
using MarkLight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Base class for view models.
    /// </summary>
    /// <d>Base class for all view models in the framework. All view models must be a subclass of this class to be processed and managed the framework. </d>
    public class View : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The ID of the view. 
        /// </summary>
        /// <d>Specifies a unique ID for the view. Used to map the view to reference fields on the parent view model. Provides a way to reference the view in data bindings. Is used as selectors in styles.</d>
        [ChangeHandler("IdChanged")]
        public string Id;

        /// <summary>
        /// The style of the view.
        /// </summary>
        /// <d>Used as selector by the styles. Specifies the name of the style that is to be applied to the view. The style is applied when the view is created (usually in the editor as the view XML is processed).</d>
        public string Style;

        /// <summary>
        /// The theme of the view.
        /// </summary>
        /// <d>Specifies the name of the theme that is applied to the view. The theme determines which set of styles are to be considered when applying matching styles to the view.</d>
        public string Theme;

        /// <summary>
        /// Layout parent view.
        /// </summary>
        /// <d>The layout parent view is the direct ascendant of the current view in the scene object hierarchy.</d>
        [NotSetFromXml]
        public View LayoutParent;

        /// <summary>
        /// Parent view.
        /// </summary>
        /// <d>The parent of the view is the logical parent to which this view belongs. In the view XML any view you can see has the current view as its logical parent.</d>
        [NotSetFromXml]
        public View Parent;

        /// <summary>
        /// Content view.        
        /// </summary>
        /// <d>View that is the parent to the content of this view. Usually it is the current view itself but when a ContentPlaceholder is used the Content points to the view that contains the ContentPlaceholder.</d>
        [NotSetFromXml]
        public View Content;

        /// <summary>
        /// View state.
        /// </summary>
        /// <d>View state name. Determines state values to be applied to the view. All views start out in the "Default" state and when the state changes the values associated with that state are applied to the view.</d>
        [ChangeHandler("StateChanged", TriggerImmediately = true)]
        [NotSetFromXml]
        public _string State;

        /// <summary>
        /// Indicates if the view is enabled.
        /// </summary>
        /// <d>Activates/deactivates the view. If set to false in this or in any parent view, all components are disabled, attached renderers are turned off, etc. Any components attached will no longer have Update() called.</d>
        [ChangeHandler("IsActiveChanged", TriggerImmediately = true)]
        public _bool IsActive;

        /// <summary>
        /// Hide flags for the game object.
        /// </summary>
        /// <d>Bit mask that controls object destruction, saving and visibility in editor.</d>
        [MapTo("GameObject.hideFlags")]
        public _HideFlags HideFlags;

        /// <summary>
        /// GameObject the view is attached to.
        /// </summary>
        /// <d>GameObject that the view is attached to.</d>
        public GameObject GameObject;

        #region Transform

        /// <summary>
        /// Position, rotation and scale of the view.
        /// </summary>
        /// <d>The view transform is used to manipulate the position, rotation and scale of the view in relation to the layout parent view's transform or in world space. The transform is sometimes manipulated indirectly through other view fields and through the view model's internal layout logic.</d>
        public Transform Transform;

        /// <summary>
        /// Position of the view.
        /// </summary>
        /// <d>The local position of the view in relation to the layout parent view transform.</d>
        [MapTo("Transform.localPosition")]
        public _Vector3 Position;

        /// <summary>
        /// Rotation of the view.
        /// </summary>
        /// <d>The local rotation of the view in relation to the layout parent view transform. Stored as a Quaternion but specified in the view XML as euler angles.</d>
        [MapTo("Transform.localRotation")]
        public _Quaternion Rotation;

        /// <summary>
        /// Scale of the view.
        /// </summary>
        /// <d>The scale of the view in relation to the layout parent view transform.</d>
        [MapTo("Transform.localScale")]
        public _Vector3 Scale;

        #endregion

        /// <summary>
        /// Item data.
        /// </summary>
        /// <d>Provides a mechanism to bind to dynamic list data. The item is set, e.g. by the List view on the child views it generates for its dynamic list data. The Item points to the list item data the view is associated with.</d>
        public _object Item;

        /// <summary>
        /// Indicates if this view is to be used as a template.
        /// </summary>
        /// <d>A template view is used to create dynamic instances of the view. Used by certain views such as the List and TabPanel.</d>
        public _bool IsTemplate;

        /// <summary>
        /// Indicates if the view has been destroyed by GameObject.Destroy().
        /// </summary>
        [NotSetFromXml]
        public _bool IsDestroyed;

        /// <summary>
        /// Indicates if the view has been created dynamically. 
        /// </summary>
        [NotSetFromXml]
        public _bool IsDynamic;

        /// <summary>
        /// The name of the view's type.
        /// </summary>
        [NotSetFromXml]
        public string ViewTypeName;

        /// <summary>
        /// Name of the view as given by the view XML.
        /// </summary>
        [NotSetFromXml]
        public string ViewXmlName;

        [NotSetFromXml]
        public List<ViewFieldBinding> ViewFieldBindings;

        [NotSetFromXml]
        public List<ViewActionEntry> ViewActionEntries;

        [NotSetFromXml]
        public List<ViewFieldStateValue> ViewFieldStateValues;

        [NotSetFromXml]
        public List<string> SetViewFieldNames;

        public static string DefaultStateName = "Default";
        public static string AnyStateName = "Any";

        private Dictionary<string, ViewFieldData> _viewFieldData;
        private Dictionary<string, Dictionary<string, ViewFieldStateValue>> _stateValues;
        private Dictionary<string, Dictionary<string, StateAnimation>> _stateAnimations;
        private HashSet<string> _setViewFields;
        private List<ValueObserver> _valueObservers;
        private HashSet<string> _changeHandlers;
        private Dictionary<string, MethodInfo> _changeHandlerMethods;
        private Dictionary<string, string> _expressionViewField;
        private List<ViewAction> _eventSystemViewActions;
        private bool _defaultState;
        private string _previousState;
        private StateAnimation _stateAnimation;

#if UNITY_4_6 || UNITY_5_0
        private bool _eventSystemTriggersInitialized;
#endif

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public View()
        {
            ViewTypeName = GetType().Name;
            ViewFieldBindings = new List<ViewFieldBinding>();
            ViewActionEntries = new List<ViewActionEntry>();
            ViewFieldStateValues = new List<ViewFieldStateValue>();
            SetViewFieldNames = new List<string>();

            // initalize private data (also done in InitializeInternalDefaultValues because of being set to null during deserialization)
            _viewFieldData = new Dictionary<string, ViewFieldData>();
            _stateValues = new Dictionary<string, Dictionary<string, ViewFieldStateValue>>();
            _stateAnimations = new Dictionary<string, Dictionary<string, StateAnimation>>();
            _setViewFields = new HashSet<string>();
            _valueObservers = new List<ValueObserver>();
            _changeHandlers = new HashSet<string>();
            _changeHandlerMethods = new Dictionary<string, MethodInfo>();
            _expressionViewField = new Dictionary<string, string>();
            _eventSystemViewActions = new List<ViewAction>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets view value.
        /// </summary>
        public object SetValue(string viewField, object value)
        {
            return SetValue(viewField, value, true, null, null, true);
        }

        /// <summary>
        /// Sets view field value. 
        /// </summary>
        /// <param name="viewField">View field path.</param>
        /// <param name="value">Value to be set.</param>
        /// <param name="updateDefaultState">Boolean indicating if the default state should be updated (if the view is in the default state).</param>
        /// <param name="callstack">Callstack used to prevent cyclical SetValue calls.</param>
        /// <param name="context">Value converter context.</param>
        /// <returns>Returns the converted value set.</returns>
        public object SetValue(string viewField, object value, bool updateDefaultState, HashSet<ViewFieldData> callstack, ValueConverterContext context, bool notifyObservers)
        {
            callstack = callstack ?? new HashSet<ViewFieldData>();

            // Debug.Log(String.Format("{0}: {1} = {2}", GameObjectName, viewField, value));

            // get view field data
            var viewFieldData = GetViewFieldData(viewField);
            if (viewFieldData == null)
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign value \"{1}\" to view field \"{2}\". View field not found.", GameObjectName, value, viewField));
                return null;
            }

            // if default state set default state value
            if (_defaultState && updateDefaultState)
            {
                var defaultStateValues = _stateValues.Get(DefaultStateName);
                if (defaultStateValues != null)
                {
                    // update default state value
                    if (defaultStateValues.ContainsKey(viewField))
                    {
                        defaultStateValues[viewField].SetValue(value);
                    }
                }
            }

            // set view field value
            try
            {
                return viewFieldData.SetValue(value, callstack, updateDefaultState, context, notifyObservers);
            }
            catch (Exception e)
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign value \"{1}\" to view field \"{2}\". Exception thrown: {3}", GameObjectName, value, viewField, Utils.GetError(e)));
                return null;
            }
        }

        /// <summary>
        /// Sets the value of a field utilizing the binding and change tracking system.
        /// </summary>
        protected object SetValue<TField>(Expression<Func<TField>> expression, object value, bool updateDefaultState, ValueConverterContext context, bool notifyObservers)
        {
            return SetValue(GetMappedViewField(expression), value, updateDefaultState, null, context, notifyObservers);
        }

        /// <summary>
        /// Sets the value of a field utilizing the binding and change tracking system.
        /// </summary>
        protected object SetValue<TField>(Expression<Func<TField>> expression, object value)
        {
            return SetValue(GetMappedViewField(expression), value, true, null, null, true);
        }

        /// <summary>
        /// Sets view field is-set indicator. 
        /// </summary>
        public void SetIsSet(string viewField)
        {
            // get view field data
            var viewFieldData = GetViewFieldData(viewField);
            if (viewFieldData == null)
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to set is-set indicator on view field \"{1}\". View field not found.", GameObjectName, viewField));
                return;
            }

            viewFieldData.SetIsSet();
        }

        /// <summary>
        /// Sets view field binding.
        /// </summary>
        public void SetBinding(string viewField, string viewFieldBinding)
        {
            // get view field data for binding target
            var viewFieldData = GetViewFieldData(viewField);
            if (viewFieldData == null)
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign binding \"{1}\" to view field \"{2}\". View field not found.", GameObjectName, viewFieldBinding, viewField));
                return;
            }

            // create BindingValueObserver and add it as observer to source view fields
            var bindingValueObserver = new BindingValueObserver();
            bindingValueObserver.Target = viewFieldData;

            // parse view field binding string
            char[] delimiterChars = { ' ', ',', '$', '(', ')', '{', '}' };
            string trimmedBinding = viewFieldBinding.Trim();

            if (trimmedBinding.StartsWith("$"))
            {
                // transformed multi-binding
                string[] bindings = trimmedBinding.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
                if (bindings.Length < 1)
                {
                    Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign binding \"{1}\" to view field \"{2}\". Improperly formatted binding string.", GameObjectName, viewFieldBinding, viewField));
                    return;
                }

                bindingValueObserver.BindingType = BindingType.MultiBindingTransform;
                bindingValueObserver.ParentView = Parent;
                bindingValueObserver.TransformMethod = Parent.GetType().GetMethod(bindings[0], BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                if (bindingValueObserver.TransformMethod == null)
                {
                    Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign binding \"{1}\" to view field \"{2}\". Transform method \"{3}\" not found in view type \"{4}\".", GameObjectName, viewFieldBinding, viewField, bindings[0], Parent.ViewTypeName));
                    return;
                }

                foreach (var binding in bindings.Skip(1))
                {
                    bool isLocalField, isNegatedField, isOneWay;
                    var sourceFieldName = ParseBindingString(binding, out isLocalField, out isNegatedField, out isOneWay);

                    // if the binding is defined as a local field (through the '#' notation) we are binding to a field on this view 
                    // otherwise we are binding to our parent view
                    var bindingView = isLocalField ? this : Parent;

                    // get view field data for binding
                    var sourceViewFieldData = bindingView.GetViewFieldData(sourceFieldName);
                    if (sourceViewFieldData == null)
                    {
                        Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign binding \"{1}\" to view field \"{2}\". Source binding view field \"{3}\" not found.", GameObjectName, viewFieldBinding, viewField, binding));
                        return;
                    }
                    //Debug.Log(String.Format("Creating binding {0} <-> {1}", sourceViewFieldData.ViewFieldPath, viewFieldData.ViewFieldPath));

                    bindingValueObserver.Sources.Add(new BindingSource(sourceViewFieldData, isNegatedField));
                    sourceViewFieldData.RegisterValueObserver(bindingValueObserver);
                }
            }
            else
            {
                // check for bindings in string
                string formatString = String.Empty;
                List<Match> matches = new List<Match>();
                foreach (Match match in ViewFieldBinding.BindingRegex.Matches(viewFieldBinding))
                {
                    matches.Add(match);
                }

                if (matches.Count <= 0)
                {
                    // no bindings found
                    Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign binding \"{1}\" to view field \"{2}\". String contains no binding.", GameObjectName, viewFieldBinding, viewField));
                    return;
                }

                // is the binding a format string?
                bool formatStringBinding = false;
                if (matches.Count > 1 || (matches[0].Value.Length != viewFieldBinding.Length) || !String.IsNullOrEmpty(matches[0].Groups["format"].Value))
                {
                    // yes. 
                    int matchCount = 0;
                    formatString = ViewFieldBinding.BindingRegex.Replace(viewFieldBinding, x =>
                    {
                        string matchCountString = matchCount.ToString();
                        ++matchCount;
                        return String.Format("{{{0}{1}}}", matchCountString, x.Groups["format"]);
                    });

                    formatStringBinding = true;
                    bindingValueObserver.BindingType = BindingType.MultiBindingFormatString;
                    bindingValueObserver.FormatString = formatString;
                }

                // parse view fields for binding source(s)
                foreach (var match in matches)
                {
                    var binding = match.Groups["field"].Value.Trim();
                    bool isLocalField, isNegatedField, isOneWay;
                    var sourceFieldName = ParseBindingString(binding, out isLocalField, out isNegatedField, out isOneWay);

                    // if the binding is defined as a local field (through the '#' notation) we are binding to a field on this view 
                    // otherwise we are binding to our parent view
                    var bindingView = isLocalField ? this : Parent;

                    // get view field data for binding
                    var sourceViewFieldData = bindingView.GetViewFieldData(sourceFieldName);
                    if (sourceViewFieldData == null)
                    {
                        Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign binding \"{1}\" to view field \"{2}\". Source binding view field \"{3}\" not found.", GameObjectName, viewFieldBinding, viewField, sourceFieldName));
                        return;
                    }
                    //Debug.Log(String.Format("Creating binding {0} <-> {1}", sourceViewFieldData.ViewFieldPath, viewFieldData.ViewFieldPath));

                    bindingValueObserver.Sources.Add(new BindingSource(sourceViewFieldData, isNegatedField));
                    sourceViewFieldData.RegisterValueObserver(bindingValueObserver);

                    if (!formatStringBinding && !isOneWay)
                    {
                        bindingValueObserver.BindingType = BindingType.SingleBinding;

                        // create value observer for target
                        var targetBindingValueObserver = new BindingValueObserver();
                        targetBindingValueObserver.BindingType = BindingType.SingleBinding;
                        targetBindingValueObserver.Target = sourceViewFieldData;
                        targetBindingValueObserver.Sources.Add(new BindingSource(viewFieldData, isNegatedField));

                        viewFieldData.RegisterValueObserver(targetBindingValueObserver);
                        AddValueObserver(targetBindingValueObserver);
                    }
                }
            }

            AddValueObserver(bindingValueObserver);
        }

        /// <summary>
        /// Parses binding string and returns view field path.
        /// </summary>
        private string ParseBindingString(string binding, out bool isLocalField, out bool isNegatedField, out bool isOneWay)
        {
            isLocalField = false;
            isNegatedField = false;
            isOneWay = false;

            var viewField = binding;
            while (viewField.Length > 0)
            {
                if (viewField.StartsWith("#"))
                {
                    isLocalField = true;
                    viewField = viewField.Substring(1);
                }
                else if (viewField.StartsWith("!"))
                {
                    isNegatedField = true;
                    viewField = viewField.Substring(1);
                }
                else if (viewField.StartsWith("="))
                {
                    isOneWay = true;
                    viewField = viewField.Substring(1);
                }
                else
                {
                    break;
                }
            }

            return viewField;
        }

        /// <summary>
        /// Sets view action entry.
        /// </summary>
        public void SetViewActionEntry(ViewActionEntry entry)
        {
            // get view field data for binding target
            var viewFieldData = GetViewFieldData(entry.ViewActionFieldName);
            if (viewFieldData == null)
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign view action handler \"{1}.{2}()\" to view action \"{3}\". View action not found.", GameObjectName, Parent.ViewTypeName, entry.ViewActionHandlerName, entry.ViewActionFieldName));
                return;
            }

            bool hasValue;
            entry.SourceView = viewFieldData.SourceView;
            ViewAction viewAction = viewFieldData.GetValue(out hasValue) as ViewAction;
            if (hasValue)
            {
                viewAction.AddEntry(entry);
            }
        }

        /// <summary>
        /// Sets state value.
        /// </summary>
        private void SetStateValue(ViewFieldStateValue stateValue)
        {
            if (!_stateValues.ContainsKey(stateValue.State))
            {
                _stateValues.Add(stateValue.State, new Dictionary<string, ViewFieldStateValue>());
            }

            var currentStateValues = _stateValues[stateValue.State];
            if (!currentStateValues.ContainsKey(stateValue.ViewFieldPath))
            {
                currentStateValues.Add(stateValue.ViewFieldPath, stateValue);
            }
            else
            {
                currentStateValues[stateValue.ViewFieldPath] = stateValue;
            }

            // if it's a non-default state value make sure we create a equivalent default state value for it
            bool isDefaultState = stateValue.State == DefaultStateName;
            if (!isDefaultState)
            {
                ViewFieldStateValue defaultStateValue = new ViewFieldStateValue();
                defaultStateValue.State = DefaultStateName;
                defaultStateValue.ValueConverterContext = stateValue.ValueConverterContext;
                defaultStateValue.ViewFieldPath = stateValue.ViewFieldPath;
                defaultStateValue.SetValue(GetValue(defaultStateValue.ViewFieldPath));
                SetStateValue(defaultStateValue);
            }
        }

        /// <summary>
        /// Sets field set-value to true.
        /// </summary>
        private void SetViewFieldSetValue(string viewFieldPath)
        {
            _setViewFields.Add(viewFieldPath);
        }

        /// <summary>
        /// Sets view field change handler.
        /// </summary>
        private void SetChangeHandler(ViewFieldChangeHandler changeHandler)
        {
            // get view field data for change handler
            var viewFieldData = GetViewFieldData(changeHandler.ViewField);
            if (viewFieldData == null)
            {
                return;
            }

            // create change handler observer
            var changeHandlerObserver = new ChangeHandlerValueObserver(this, changeHandler.ChangeHandlerName, changeHandler.TriggerImmediately);
            if (changeHandlerObserver.IsValid)
            {
                viewFieldData.RegisterValueObserver(changeHandlerObserver);
                AddValueObserver(changeHandlerObserver);
            }
            else
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to assign view change handler \"{1}()\" for view field \"{2}\". Change handler method not found.", GameObjectName, changeHandler.ChangeHandlerName, changeHandler.ViewField));
                return;
            }
        }

        /// <summary>
        /// Gets mapped view field from expression.
        /// </summary>
        protected string GetMappedViewField<TField>(Expression<Func<TField>> expression)
        {
            // get mapped view field           
            string expressionString = expression.ToString();
            string mappedViewField = _expressionViewField.Get(expressionString);
            if (mappedViewField == null)
            {
                // add expression
                mappedViewField = expressionString.Substring(expressionString.IndexOf(").") + 2);
                _expressionViewField.Add(expressionString, mappedViewField);
            }

            return mappedViewField;
        }

        /// <summary>
        /// Gets value from view field.
        /// </summary>
        protected object GetValue<TField>(Expression<Func<TField>> expression)
        {
            return GetValue(GetMappedViewField(expression));
        }

        /// <summary>
        /// Gets value from view field.
        /// </summary>
        public object GetValue(string viewField)
        {
            bool hasValue;
            return GetValue(viewField, out hasValue);
        }

        /// <summary>
        /// Gets value from view field.
        /// </summary>
        public object GetValue(string viewField, out bool hasValue)
        {
            // get view field data            
            var viewFieldData = GetViewFieldData(viewField);
            if (viewFieldData == null)
            {
                hasValue = false;
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to get value from view field \"{1}\". View field not found.", GameObjectName, viewField));
                return null;
            }

            try
            {
                return viewFieldData.GetValue(out hasValue);
            }
            catch (Exception e)
            {
                hasValue = false;
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to get value from view field \"{1}\". Exception thrown: {2}", GameObjectName, viewField, Utils.GetError(e)));
                return null;
            }
        }

        /// <summary>
        /// Gets boolean indicating if value has been set on view field.
        /// </summary>
        public bool IsSet<TField>(Expression<Func<TField>> expression)
        {
            return IsSet(GetMappedViewField(expression));
        }

        /// <summary>
        /// Gets boolean indicating if value has been set on view field.
        /// </summary>
        public bool IsSet(string viewField)
        {
            // get view field data
            var viewFieldData = GetViewFieldData(viewField);
            if (viewFieldData == null)
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to get set-value from view field \"{1}\". View field not found.", GameObjectName, viewField));
                return false;
            }

            return viewFieldData.IsSet();
        }

        /// <summary>
        /// Initializes internal values to default values. Called once before InitializeInternal(). Called in depth-first order.
        /// </summary>
        public virtual void InitializeInternalDefaultValues()
        {
            State.DirectValue = DefaultStateName;
            IsActive.DirectValue = true;
            _defaultState = true;

            // initialize lists and dictionaries
            _viewFieldData = new Dictionary<string, ViewFieldData>();
            _stateValues = new Dictionary<string, Dictionary<string, ViewFieldStateValue>>();
            _stateAnimations = new Dictionary<string, Dictionary<string, StateAnimation>>();
            _setViewFields = new HashSet<string>();
            _valueObservers = new List<ValueObserver>();
            _changeHandlers = new HashSet<string>();
            _changeHandlerMethods = new Dictionary<string, MethodInfo>();
            _expressionViewField = new Dictionary<string, string>();
            _eventSystemViewActions = new List<ViewAction>();
            _previousState = State;
            _defaultState = State == DefaultStateName;
        }

        /// <summary>
        /// Initializes the view internally. Called once before Initialize(). Called in depth-first order.
        /// </summary>
        public virtual void InitializeInternal()
        {
            // initialize bindings
            foreach (var binding in ViewFieldBindings)
            {
                SetBinding(binding.ViewField, binding.BindingString);
            }

            // initialize state values
            foreach (var stateValue in ViewFieldStateValues)
            {
                SetStateValue(stateValue);
            }

            // initialize set-fields
            foreach (var setField in SetViewFieldNames)
            {
                SetViewFieldSetValue(setField);
            }

            // initialize action handlers
            foreach (var actionEntry in ViewActionEntries)
            {
                SetViewActionEntry(actionEntry);
            }

            // initialize change handlers
            var viewTypeData = ViewData.GetViewTypeData(ViewTypeName);
            foreach (var changeHandler in viewTypeData.ViewFieldChangeHandlers)
            {
                SetChangeHandler(changeHandler);
            }

            // initialize system event triggers
            _eventSystemViewActions = new List<ViewAction>();
            foreach (var viewActionField in viewTypeData.ViewActionFields)
            {
                // get view action field data
                var viewFieldData = GetViewFieldData(viewActionField);

                bool hasValue;
                ViewAction viewAction = viewFieldData.GetValue(out hasValue) as ViewAction;
                if (viewAction != null && viewAction.TriggeredByEventSystem)
                {
                    _eventSystemViewActions.Add(viewAction);
                }
            }

            InitEventSystemTriggers();
        }

        /// <summary>
        /// Initializes unity event triggers.
        /// </summary>
        internal void InitEventSystemTriggers()
        {
            if (!_eventSystemViewActions.Any())
            {
#if UNITY_4_6 || UNITY_5_0
                _eventSystemTriggersInitialized = true;
#endif
                return;
            }

            EventTrigger eventTrigger = GetComponent<EventTrigger>();
            if (eventTrigger == null)
            {
                eventTrigger = gameObject.AddComponent<EventTrigger>();
            }

#if UNITY_4_6 || UNITY_5_0
            var triggers = eventTrigger.delegates;
#else
            var triggers = eventTrigger.triggers;
#endif
            if (triggers == null)
            {
#if UNITY_4_6 || UNITY_5_0
                eventTrigger.delegates = new List<EventTrigger.Entry>();
#endif
                return;
            }

            triggers.Clear();

            foreach (var viewAction in _eventSystemViewActions)
            {
                var entry = new EventTrigger.Entry();
                entry.eventID = viewAction.EventTriggerType;
                entry.callback = new EventTrigger.TriggerEvent();

                var eventViewAction = viewAction;
                var action = new UnityAction<BaseEventData>(eventData => eventViewAction.Trigger(eventData));
                entry.callback.AddListener(action);

                triggers.Add(entry);
            }

#if UNITY_4_6 || UNITY_5_0
            _eventSystemTriggersInitialized = true;
#endif
        }

        /// <summary>
        /// Called once to initialize the view. Called in reverse breadth-first order.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Gets view field data.
        /// </summary>
        public ViewFieldData GetViewFieldData(string viewField, int depth = int.MaxValue)
        {
            // get mapped view field
            var viewTypeData = ViewData.GetViewTypeData(ViewTypeName);
            var mappedViewField = viewTypeData.GetMappedViewField(viewField);

            if (_viewFieldData == null)
            {
                _viewFieldData = new Dictionary<string, ViewFieldData>();
            }

            // is there data for this field?
            var fieldData = _viewFieldData.Get(mappedViewField);
            if (fieldData == null)
            {
                // no. create new field data
                fieldData = ViewFieldData.FromViewFieldPath(this, mappedViewField);
                if (fieldData != null)
                {
                    _viewFieldData.Add(mappedViewField, fieldData);
                }
            }

            // are we the owner of this field?
            if (fieldData != null && !fieldData.IsOwner && depth > 0)
            {
                // no. go deeper if we can
                var targetView = fieldData.GetTargetView();
                if (targetView != null)
                {
                    fieldData = targetView.GetViewFieldData(fieldData.TargetViewFieldPath, --depth);
                }
            }

            return fieldData;
        }

        /// <summary>
        /// Called once at the end of a frame. Triggers queued change handlers.
        /// </summary>
        public void LateUpdate()
        {
            TriggerChangeHandlers();

#if UNITY_4_6 || UNITY_5_0
            if (!_eventSystemTriggersInitialized)
            {
                InitEventSystemTriggers();
            }
#endif
        }

        /// <summary>
        /// Triggers queued change handlers.
        /// </summary>
        public void TriggerChangeHandlers()
        {
            if (_changeHandlers.Count > 0)
            {
                var triggeredChangeHandlers = new List<string>(_changeHandlers);
                _changeHandlers.Clear();

                foreach (var changeHandler in triggeredChangeHandlers)
                {
                    try
                    {
                        _changeHandlerMethods[changeHandler].Invoke(this, null);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(String.Format("[MarkLight] {0}: Exception thrown in change handler \"{1}\": {2}", GameObjectName, changeHandler, Utils.GetError(e)));
                    }
                }
            }
        }

        /// <summary>
        /// Called once to propagate bound values. Called in breadth-first order.
        /// </summary>
        public void PropagateBindings()
        {
            foreach (var viewFieldData in _viewFieldData.Values)
            {
                viewFieldData.NotifyBindingValueObservers(new HashSet<ViewFieldData>());
            }
        }

        /// <summary>
        /// Called once to queue all change handlers. Called in reverse breadth-first order.
        /// </summary>
        public void QueueAllChangeHandlers()
        {
            foreach (var viewFieldData in _viewFieldData.Values)
            {
                viewFieldData.NotifyChangeHandlerValueObservers(new HashSet<ViewFieldData>());
            }
        }

        /// <summary>
        /// Adds a value observer to the view.
        /// </summary>
        internal void AddValueObserver(ValueObserver valueObserver)
        {
            _valueObservers.Add(valueObserver);
        }

        /// <summary>
        /// Adds a binding to the view field that will be processed when the view is initialized.
        /// </summary>
        internal void AddBinding(string viewField, string bindingString)
        {
            ViewFieldBindings.Add(new ViewFieldBinding { ViewField = viewField, BindingString = bindingString });
        }

        /// <summary>
        /// Adds field field state value.
        /// </summary>
        internal void AddStateValue(string state, string viewField, string value, ValueConverterContext context, bool isSubState)
        {
            // convert value to ensure that assets that can only be loaded in the editor is cached by the view-presenter, the caching is done by the value converter
            var viewFieldData = GetViewFieldData(viewField);
            if (viewFieldData == null)
            {
                Debug.LogError(String.Format("[MarkLight] {0}: Unable to set state value \"{1}-{2}\". View field \"{2}\" not found.", GameObjectName, state, viewField));
                return;
            }

            if (viewFieldData.ValueConverter != null)
            {
                viewFieldData.ValueConverter.Convert(value, context != null ? context : ValueConverterContext.Empty);
            }

            // set state value
            if (isSubState)
            {
                // only go down one level
                var subViewFieldData = GetViewFieldData(viewField, 1);
                subViewFieldData.SourceView.AddStateValue(state, subViewFieldData.ViewFieldPath, value, context, false);
            }
            else
            {
                // get mapped view-field path
                var viewTypeData = ViewData.GetViewTypeData(ViewTypeName);
                var mappedViewField = viewTypeData.GetMappedViewField(viewField);

                // overwrite state value if it exist otherwise create a new one 
                var stateValue = ViewFieldStateValues.FirstOrDefault(x => x.State == state && x.ViewFieldPath == mappedViewField);
                if (stateValue != null)
                {
                    stateValue.Value = value;
                    stateValue.ValueConverterContext = context;
                }
                else
                {
                    ViewFieldStateValues.Add(new ViewFieldStateValue
                    {
                        State = state,
                        ViewFieldPath = mappedViewField,
                        Value = value,
                        ValueConverterContext = context
                    });
                }
            }
        }

        /// <summary>
        /// Adds a view action handler for a certain view action.
        /// </summary>
        internal void AddViewActionEntry(string viewActionFieldName, string viewActionHandlerName, View parent)
        {
            ViewActionEntries.Add(new ViewActionEntry { ParentView = parent, ViewActionFieldName = viewActionFieldName, ViewActionHandlerName = viewActionHandlerName });
        }

        /// <summary>
        /// Adds a state animation to the view.
        /// </summary>
        internal void AddStateAnimation(StateAnimation stateAnimation)
        {
            if (String.IsNullOrEmpty(stateAnimation.From))
                return;

            if (!_stateAnimations.ContainsKey(stateAnimation.From))
            {
                _stateAnimations.Add(stateAnimation.From, new Dictionary<string, StateAnimation>());
            }

            _stateAnimations[stateAnimation.From].Add(stateAnimation.To, stateAnimation);
        }

        /// <summary>
        /// Gets state animation.
        /// </summary>
        internal StateAnimation GetStateAnimation(string from, string to)
        {
            // check if animation exist between states From -> To
            if (_stateAnimations.ContainsKey(from) &&
                _stateAnimations[from].ContainsKey(to))
            {
                return _stateAnimations[from][to];
            }

            // check if animation exist between states Any -> To
            if (_stateAnimations.ContainsKey(AnyStateName) &&
                _stateAnimations[AnyStateName].ContainsKey(to))
            {
                return _stateAnimations[AnyStateName][to];
            }

            return null;
        }

        /// <summary>
        /// Queues change handler to be called at the end of the frame.
        /// </summary>
        internal void QueueChangeHandler(string name)
        {
            _changeHandlers.Add(name);
            if (!_changeHandlerMethods.ContainsKey(name))
            {
                _changeHandlerMethods.Add(name, GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
            }
        }

        /// <summary>
        /// Called after the view is initialized but before any XML values are set. Used to set default values on the view.
        /// </summary>
        public virtual void SetDefaultValues()
        {
            ViewTypeName = GetType().Name;
            GameObject = gameObject;
            State.DirectValue = DefaultStateName;
            IsActive.DirectValue = true;
        }

        /// <summary>
        /// Called when a field affecting the layout of the view has changed and that change is to be propagated to parents.
        /// </summary>
        public virtual void LayoutsChanged()
        {
            QueueChangeHandler("LayoutChanged");

            // inform parents of update
            this.ForEachParent<View>(x => x.QueueChangeHandler("ChildLayoutChanged"));
        }

        /// <summary>
        /// Called when a child layout has been changed.
        /// </summary>
        public virtual void ChildLayoutChanged()
        {
        }

        /// <summary>
        /// Called when a field affecting the layout of the view has changed.
        /// </summary>
        public virtual void LayoutChanged()
        {            
            //Debug.Log(ViewTypeName + ": LayoutChanged called");
        }

        /// <summary>
        /// Called when a field affecting the behavior and visual appearance of the view has changed.
        /// </summary>
        public virtual void BehaviorChanged()
        {
        }

        /// <summary>
        /// Called when the Id of the view changes.
        /// </summary>
        public virtual void IdChanged()
        {
            // set gameObject name to Id if set
            gameObject.name = GameObjectName;
        }

        /// <summary>
        /// Called when IsActive field has been changed.
        /// </summary>
        public virtual void IsActiveChanged()
        {
            gameObject.SetActive(IsActive.Value);
        }

        /// <summary>
        /// Called when view state has been changed.
        /// </summary>
        public virtual void StateChanged()
        {
            // no state changes are made while generating UI in editor, this is to prevent default state-values from being overwritten
            if (!Application.isPlaying)
            {
                State.DirectValue = DefaultStateName;
                return;
            }

            // stop previous animation if active and get new state animation
            if (_stateAnimation != null && !_stateAnimation.IsAnimationCompleted)
            {
                _stateAnimation.StopAnimation();
            }
            _stateAnimation = GetStateAnimation(_previousState, State);

            _defaultState = State == DefaultStateName;
            _previousState = State;

            // get state values
            var currentStateValues = _stateValues.Get(State);
            if (currentStateValues == null)
            {
                return; // no state values set, nothing to do
            }

            // go through state values and update view values
            foreach (var stateValue in currentStateValues.Values)
            {
                if (_stateAnimation != null)
                {
                    // is this view field animated?
                    var animators = _stateAnimation.GetFieldAnimators(stateValue.ViewFieldPath);
                    if (animators != null)
                    {
                        var animateTo = stateValue.GetValue();

                        // yes. set target value of animations
                        foreach (var animator in animators)
                        {
                            if (animateTo is String)
                            {
                                animator.ToStringValue = (String)animateTo;
                            }
                            else
                            {
                                animator.To = animateTo;
                            }

                            animator.UpdateViewFieldAnimator();
                        }

                        continue;
                    }
                }

                // set view value to state value
                var value = SetValue(stateValue.ViewFieldPath, stateValue.GetValue(), false, null, stateValue.ValueConverterContext, true);
                stateValue.SetValue(value); // store converted value to save conversion time
            }

            if (_stateAnimation != null)
            {
                // start state animation
                _stateAnimation.StartAnimation();
            }
        }

        /// <summary>
        /// Activates the view.
        /// </summary>
        public void Activate()
        {
            IsActive.Value = true;
        }

        /// <summary>
        /// Deactivates the view.
        /// </summary>
        public void Deactivate()
        {
            IsActive.Value = false;
        }

        /// <summary>
        /// Changes the state of the view.
        /// </summary>
        public virtual void SetState(string state)
        {
            State.Value = state;
        }

        /// <summary>
        /// Creates a view from a template and adds it to a parent at specified index.
        /// </summary>
        public static T CreateView<T>(T template, View layoutParent, int siblingIndex = -1) where T : View
        {
            // instantiate template
            var go = Instantiate(template.gameObject) as GameObject;            
            go.hideFlags = UnityEngine.HideFlags.None;

            // set layout parent
            go.transform.SetParent(layoutParent.transform, false);

            // set view parent
            var view = go.GetComponent<T>();
            if (siblingIndex > 0)
            {
                go.transform.SetSiblingIndex(siblingIndex);
            }

            view.IsTemplate.DirectValue = false;
            view.IsDynamic.DirectValue = true;
            return view;
        }

        /// <summary>
        /// Adds a view from a template.
        /// </summary>
        public T CreateView<T>(T template, int siblingIndex = -1) where T : View
        {
            return CreateView(template, this, siblingIndex);
        }

        /// <summary>
        /// Notifies all value observers that are dependent on the specified field. E.g. when field "Name" changes, value observers on "Name.FirstName"
        /// and "Name.LastName" are notified in this method. 
        /// </summary>
        public void NotifyDependentValueObservers(string viewFieldPath)
        {
            foreach (var viewFieldData in _viewFieldData.Values)
            {
                if (!viewFieldData.IsOwner)
                    continue;

                if (viewFieldData.ViewFieldPathInfo.Dependencies.Count > 0 &&
                    viewFieldData.ViewFieldPathInfo.Dependencies.Contains(viewFieldPath))
                {
                    viewFieldData.NotifyValueObservers(new HashSet<ViewFieldData>());
                }
            }
        }

        /// <summary>
        /// Moves the view to another view.
        /// </summary>
        public void MoveTo(View target, int childIndex = -1)
        {
            transform.SetParent(target.transform, false);
            if (childIndex >= 0)
            {
                transform.SetSiblingIndex(childIndex);
            }

            SetValue(() => LayoutParent, target);
        }

        /// <summary>
        /// Adds view field path to list of set view fields.
        /// </summary>
        internal void AddIsSetField(string viewFieldPath)
        {
            if (!SetViewFieldNames.Contains(viewFieldPath))
            {
                SetViewFieldNames.Add(viewFieldPath);
            }

            if (_setViewFields != null)
            {
                _setViewFields.Add(viewFieldPath);
            }
        }

        /// <summary>
        /// Gets bool indicating if set field is in list of set fields.
        /// </summary>
        internal bool GetIsSetFieldValue(string viewFieldPath)
        {
            if (_setViewFields != null)
            {
                return _setViewFields.Contains(viewFieldPath);
            }

            return false;
        }

#endregion

#region Properties

        /// <summary>
        /// Gets boolean indicating if this view is live (enabled and not destroyed).
        /// </summary>
        public bool IsLive
        {
            get
            {
                return IsActive && !IsDestroyed;
            }
        }

        /// <summary>
        /// Gets boolean indicating if the view has any queued change handlers.
        /// </summary>
        public bool HasQueuedChangeHandlers
        {
            get
            {
                return _changeHandlers != null ? _changeHandlers.Count > 0 : false;
            }
        }

        /// <summary>
        /// Gets list of currently queued change handlers.
        /// </summary>
        public List<string> QueuedChangeHandlers
        {
            get
            {
                return _changeHandlers != null ? _changeHandlers.ToList() : new List<string>();
            }
        }

        /// <summary>
        /// Gets child count. 
        /// </summary>
        public int ChildCount
        {
            get
            {
                return transform.childCount;
            }
        }

        /// <summary>
        /// Gets GameObject name (usually view type + id). 
        /// </summary>
        public string GameObjectName
        {
            get
            {
                var viewName = ViewTypeName == "View" ? ViewXmlName : ViewTypeName;                
                return !String.IsNullOrEmpty(Id) ? String.Format("{0} ({1})", viewName, Id) : viewName;
            }
        }

        /// <summary>
        /// Gets view field data of the view.
        /// </summary>
        public Dictionary<string, ViewFieldData> ViewFieldDataDictionary
        {
            get
            {
                return _viewFieldData;
            }
        }

        #endregion
    }
}
