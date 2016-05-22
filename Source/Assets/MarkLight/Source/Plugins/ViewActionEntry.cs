#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Contains data about a view action handler.
    /// </summary>
    [Serializable]
    public class ViewActionEntry
    {
        #region Fields

        public string ViewActionFieldName;
        public string ViewActionHandlerName;
        public View ParentView;
        public View SourceView;

        private bool _initialized;
        private MethodInfo _viewActionMethod;

        private object[] _parameters;
        private bool _hasActionDataParameter;
        private int _actionDataParameterIndex;
        private bool _hasEventDataParemeter;
        private int _eventDataParameterIndex;
        private bool _hasCustomDataParameter;
        private int _customDataParameterIndex;

        #endregion

        #region Methods

        /// <summary>
        /// Invokes the view action method.
        /// </summary>
        public void Invoke()
        {
            Invoke(null, null, null);
        }

        /// <summary>
        /// Invokes the view action method with action data.
        /// </summary>
        public void Invoke(ActionData actionData)
        {
            Invoke(actionData, null, null);
        }

        /// <summary>
        /// Invokes the view action method with base event data.
        /// </summary>
        public void Invoke(BaseEventData baseEventData)
        {
            Invoke(null, baseEventData, null);
        }

        /// <summary>
        /// Invokes the view action method with custom event data.
        /// </summary>
        public void Invoke(object customData)
        {
            Invoke(null, null, customData);
        }

        /// <summary>
        /// Invokes the view action method with parameters.
        /// </summary>
        internal void Invoke(ActionData actionData, BaseEventData baseEventData, object customData)
        {
            if (!_initialized)
            {
                Initialize();

                if (!_initialized)
                    return;
            }

            if (_hasActionDataParameter)
            {
                _parameters[_actionDataParameterIndex] = actionData;
            }

            if (_hasEventDataParemeter)
            {
                _parameters[_eventDataParameterIndex] = baseEventData;
            }

            if (_hasCustomDataParameter)
            {
                _parameters[_customDataParameterIndex] = customData;
            }

            // call action handler
            try
            {
                _viewActionMethod.Invoke(ParentView, _parameters);
            }
            catch (Exception e)
            {
                Utils.LogError("[MarkLight] {0}: Exception thrown when triggering view action handler \"{1}.{2}()\" for view action \"{3}\": {4}", SourceView.GameObjectName, ParentView.ViewTypeName, ViewActionHandlerName, ViewActionFieldName, Utils.GetError(e));
            }
        }

        /// <summary>
        /// Initializes the view action entry.
        /// </summary>
        private void Initialize()
        {
            // look for a method with the same name as the entry
            _viewActionMethod = ParentView.GetType().GetMethod(ViewActionHandlerName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (_viewActionMethod == null)
            {
                Utils.LogError("[MarkLight] {0}: Unable to initialize view action handler \"{1}.{2}()\" for view action \"{3}\". View action handler not found.", SourceView.GameObjectName, ParentView.ViewTypeName, ViewActionHandlerName, ViewActionFieldName);
                return;
            }

            ParameterInfo[] viewActionMethodParameters = _viewActionMethod.GetParameters();
            int parameterCount = viewActionMethodParameters.Length;

            Type viewType = typeof(View);
            Type actionDataType = typeof(ActionData);
            Type baseEventDataType = typeof(BaseEventData);

            _parameters = parameterCount > 0 ? new object[parameterCount] : null;
            for (int i = 0; i < parameterCount; ++i)
            {
                if (viewType.IsAssignableFrom(viewActionMethodParameters[i].ParameterType))
                {
                    if (!viewActionMethodParameters[i].ParameterType.IsAssignableFrom(SourceView.GetType()))
                    {
                        Utils.LogError("[MarkLight] View action \"{0}.{1}\" has parameter \"{2}\" with invalid type. Expected type (or baseclass of) \"{3}\".", ParentView.ViewTypeName, ViewActionHandlerName, viewActionMethodParameters[i].Name, SourceView.ViewTypeName);
                    }

                    _parameters[i] = SourceView;
                }
                else if (actionDataType.IsAssignableFrom(viewActionMethodParameters[i].ParameterType))
                {
                    _hasActionDataParameter = true;
                    _actionDataParameterIndex = i;
                }
                else if (baseEventDataType.IsAssignableFrom(viewActionMethodParameters[i].ParameterType))
                {
                    _hasEventDataParemeter = true;
                    _eventDataParameterIndex = i;
                }
                else
                {
                    _hasCustomDataParameter = true;
                    _customDataParameterIndex = i;
                }
            }

            _initialized = true;
        }


        #endregion
    }
}
