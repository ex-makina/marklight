#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using MarkLight.Views.UI;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// View action.
    /// </summary>
    [Serializable]
    public class ViewAction
    {
        #region Fields

        public string Name;
        public bool TriggeredByEventSystem;
        public EventTriggerType EventTriggerType;
        public bool IsDisabled; 

        private List<ViewActionEntry> _viewActionEntries;
        public static Dictionary<string, EventTriggerType> EventTriggerTypes;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewAction(string name = "")
        {
            Name = name;
            if (EventTriggerTypes.ContainsKey(Name))
            {
                TriggeredByEventSystem = true;
                EventTriggerType = EventTriggerTypes[Name];
            }

            _viewActionEntries = new List<ViewActionEntry>();
        }

        /// <summary>
        /// Initializes static instance of the class.
        /// </summary>
        static ViewAction()
        {
            EventTriggerTypes = new Dictionary<string, EventTriggerType>();
            EventTriggerTypes.Add("BeginDrag", EventTriggerType.BeginDrag);
            EventTriggerTypes.Add("Cancel", EventTriggerType.Cancel);
            EventTriggerTypes.Add("Deselect", EventTriggerType.Deselect);
            EventTriggerTypes.Add("Drag", EventTriggerType.Drag);
            EventTriggerTypes.Add("Drop", EventTriggerType.Drop);
            EventTriggerTypes.Add("EndDrag", EventTriggerType.EndDrag);
            EventTriggerTypes.Add("InitializePotentialDrag", EventTriggerType.InitializePotentialDrag);
            EventTriggerTypes.Add("Move", EventTriggerType.Move);
            EventTriggerTypes.Add("Click", EventTriggerType.PointerClick);
            EventTriggerTypes.Add("PointerClick", EventTriggerType.PointerClick);
            EventTriggerTypes.Add("MouseClick", EventTriggerType.PointerClick);
            EventTriggerTypes.Add("PointerDown", EventTriggerType.PointerDown);
            EventTriggerTypes.Add("MouseDown", EventTriggerType.PointerDown);
            EventTriggerTypes.Add("PointerEnter", EventTriggerType.PointerEnter);
            EventTriggerTypes.Add("MouseEnter", EventTriggerType.PointerEnter);
            EventTriggerTypes.Add("PointerExit", EventTriggerType.PointerExit);
            EventTriggerTypes.Add("MouseExit", EventTriggerType.PointerExit);
            EventTriggerTypes.Add("PointerUp", EventTriggerType.PointerUp);
            EventTriggerTypes.Add("MouseUp", EventTriggerType.PointerUp);
            EventTriggerTypes.Add("Scroll", EventTriggerType.Scroll);
            EventTriggerTypes.Add("Select", EventTriggerType.Select);
            EventTriggerTypes.Add("Submit", EventTriggerType.Submit);
            EventTriggerTypes.Add("UpdateSelected", EventTriggerType.UpdateSelected);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Triggers the view action with no data.
        /// </summary>
        public void Trigger()
        {
            if (IsDisabled)
                return;

            // go through the entries and call them
            if (_viewActionEntries != null)
            {
                foreach (var entry in _viewActionEntries)
                {
                    entry.Invoke();
                }
            }
        }

        /// <summary>
        /// Triggers the view action with action data.
        /// </summary>
        public void Trigger(ActionData actionData)
        {
            if (IsDisabled)
                return;

            // go through the entries and call them
            if (_viewActionEntries != null)
            {
                foreach (var entry in _viewActionEntries)
                {
                    entry.Invoke(actionData);
                }
            }
        }

        /// <summary>
        /// Triggers the view action with event data.
        /// </summary>
        public void Trigger(BaseEventData baseEventData)
        {
            if (IsDisabled)
                return;

            // go through the entries and call them
            if (_viewActionEntries != null)
            {
                foreach (var entry in _viewActionEntries)
                {
                    entry.Invoke(baseEventData);
                }
            }
        }

        /// <summary>
        /// Triggers the view action custom data.
        /// </summary>
        public void Trigger(object customData)
        {
            if (IsDisabled)
                return;

            // go through the entries and call them
            if (_viewActionEntries != null)
            {
                foreach (var entry in _viewActionEntries)
                {
                    entry.Invoke(customData);
                }
            }
        }

        /// <summary>
        /// Triggers the view action.
        /// </summary>
        public void Trigger(ActionData actionData, BaseEventData baseEventData, object customData)
        {
            if (IsDisabled)
                return;

            // go through the entries and call them
            if (_viewActionEntries != null)
            {
                foreach (var entry in _viewActionEntries)
                {
                    entry.Invoke(actionData, baseEventData, customData);
                }
            }
        }

        /// <summary>
        /// Adds view action entry. 
        /// </summary>
        public void AddEntry(ViewActionEntry viewActionEntry)
        {
            if (_viewActionEntries == null)
            {
                _viewActionEntries = new List<ViewActionEntry>();
            }

            _viewActionEntries.Add(viewActionEntry);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets number of view action entries attached to this view action.
        /// </summary>
        public int Entries
        {
            get
            {
                return _viewActionEntries != null ? _viewActionEntries.Count : 0;
            }
        }

        /// <summary>
        /// Gets boolean indicating if the view action has any entries attached to it. 
        /// </summary>
        public bool HasEntries
        {
            get
            {
                return _viewActionEntries != null ? _viewActionEntries.Count > 0 : false;
            }
        }

        #endregion
    }
}
