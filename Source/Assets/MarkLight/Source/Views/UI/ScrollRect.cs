#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace MarkLight.Views.UI
{
    /// <summary>
    /// ScrollRect view.
    /// </summary>
    /// <d>Displays scrollable content.</d>
    [HideInPresenter]
    public class ScrollRect : UIView
    {
        #region Fields

        /// <summary>
        /// Indicates if the content can scroll horizontally.
        /// </summary>
        /// <d>Boolean indicating if the content can be scrolled horizontally.</d>
        [MapTo("ScrollRectComponent.horizontal")]
        public _bool CanScrollHorizontally;

        /// <summary>
        /// Indicates if the content can scroll vertically.
        /// </summary>
        /// <d>Boolean indicating if the content can be scrolled vertically.</d>
        [MapTo("ScrollRectComponent.vertically")]
        public _bool CanScrollVertically;

        /// <summary>
        /// Scroll deceleration rate.
        /// </summary>
        /// <d>Value indicating the rate of which the scroll stops moving.</d>
        [MapTo("ScrollRectComponent.decelerationRate")]
        public _float DecelerationRate;

        /// <summary>
        /// Scroll elasticity.
        /// </summary>
        /// <d>Value indicating how elastic the scrolling is when moved beyond the bounds of the scrollable content.</d>
        [MapTo("ScrollRectComponent.elasticity")]
        public _float Elasticity;

        /// <summary>
        /// Horizontal normalized position.
        /// </summary>
        /// <d>Value between 0-1 indicating the position of the scrollable content.</d>
        [MapTo("ScrollRectComponent.horizontalNormalizedPosition")]
        public _float HorizontalNormalizedPosition;

        /// <summary>
        /// Horizontal scrollbar component.
        /// </summary>
        /// <d>Optional scrollbar component linked to the horizontal scrolling of the content.</d>
        [MapTo("ScrollRectComponent.horizontalScrollbar")]
        public _ScrollbarComponent HorizontalScrollbar;

        /// <summary>
        /// Space between scrollbar and scrollable content.
        /// </summary>
        /// <d>Space between scrollbar and scrollable content.</d>
        [MapTo("ScrollRectComponent.horizontalScrollbarSpacing")]
        public _float HorizontalScrollbarSpacing;

#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
        /// <summary>
        /// Indicates horizontal scrollbar visiblity mode.
        /// </summary>
        /// <d>Indicates horizontal scrollbar visiblity mode.</d>
        [MapTo("ScrollRectComponent.horizontalScrollbarVisibility")]
        public _ScrollbarVisibility HorizontalScrollbarVisibility;
#endif

        /// <summary>
        /// Indicates if scroll has intertia.
        /// </summary>
        /// <d>Boolean indicating if the scroll has inertia.</d>
        [MapTo("ScrollRectComponent.inertia")]
        public _bool HasInertia;

        /// <summary>
        /// Behavior when scrolled beyond bounds.
        /// </summary>
        /// <d>Enum specifying the behavior to use when the content moves beyond the scroll rect.</d>
        [MapTo("ScrollRectComponent.movementType")]
        public _ScrollRectMovementType MovementType;

        /// <summary>
        /// Normalized position of the scroll.
        /// </summary>
        /// <d>The scroll position as a Vector2 between (0,0) and (1,1) with (0,0) being the lower left corner.</d>
        [MapTo("ScrollRectComponent.normalizedPosition")]
        public _Vector2 NormalizedPosition;

        /// <summary>
        /// Scrollable content.
        /// </summary>
        /// <d>The content that can be scrolled. It should be a child of the GameObject with ScrollRect on it.</d>
        [MapTo("ScrollRectComponent.content")]
        public _RectTransformComponent ScrollContent;

        /// <summary>
        /// Scroll sensitivity.
        /// </summary>
        /// <d>Value indicating how sensitive the scrolling is to scroll wheel and track pad movement.</d>
        [MapTo("ScrollRectComponent.scrollSensitivity")]
        public _float ScrollSensitivity;

        /// <summary>
        /// Current velocity of the content.
        /// </summary>
        /// <d>Indicates the current velocity of the scrolled content.</d>
        [MapTo("ScrollRectComponent.velocity")]
        public _Vector2 ScrollVelocity;

        /// <summary>
        /// Vertical normalized position.
        /// </summary>
        /// <d>Value between 0-1 indicating the position of the scrollable content.</d>
        [MapTo("ScrollRectComponent.verticalNormalizedPosition")]
        public _float VerticalNormalizedPosition;

        /// <summary>
        /// Vertical scrollbar component.
        /// </summary>
        /// <d>Optional scrollbar component linked to the vertical scrolling of the content.</d>
        [MapTo("ScrollRectComponent.verticalScrollbar")]
        public _ScrollbarComponent VerticalScrollbar;

        /// <summary>
        /// Space between scrollbar and scrollable content.
        /// </summary>
        /// <d>Space between scrollbar and scrollable content.</d>
        [MapTo("ScrollRectComponent.verticalScrollbarSpacing")]
        public _float VerticalScrollbarSpacing;

#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
        /// <summary>
        /// Indicates vertical scrollbar visiblity mode.
        /// </summary>
        /// <d>Indicates vertical scrollbar visiblity mode.</d>
        [MapTo("ScrollRectComponent.verticalScrollbarVisibility")]
        public _ScrollbarVisibility VerticalScrollbarVisibility;
#endif

        /// <summary>
        /// Scrollable viewport.
        /// </summary>
        /// <d>References the RectTransform parent to the content.</d>
        [MapTo("ScrollRectComponent.viewport")]
        public _RectTransformComponent Viewport;

        /// <summary>
        /// ScrollRect component.
        /// </summary>
        /// <d>Component responsible for handling scrollable content.</d>
        public UnityEngine.UI.ScrollRect ScrollRectComponent;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            UpdateBackground.DirectValue = false;
            ScrollRectComponent.vertical = true;
            ScrollRectComponent.horizontal = true;
            ImageComponent.color = Color.clear;
        }

        /// <summary>
        /// Called when a child layout has been updated.
        /// </summary>
        public override void ChildLayoutChanged()
        {
            base.ChildLayoutChanged();
            QueueChangeHandler("LayoutChanged");
        }

        /// <summary>
        /// Called when the layout of the view has been changed.
        /// </summary>
        public override void LayoutChanged()
        {            
            if (ScrollRectComponent.content == null)
            {
                // set scrollrect content to first child
                var child = this.Find<UIView>(false);
                if (child != null)
                {
                    ScrollRectComponent.content = child.RectTransform;
                }                
            }

            // workaround for panel blocking drag events in child views
            UnblockDragEvents();

            base.LayoutChanged();
        }

        /// <summary>
        /// Workaround for blocking of drag events in child views.
        /// </summary>
        private void UnblockDragEvents()
        {
            this.ForEachChild<View>(x =>
            {
                var eventTrigger = x.GetComponent<EventTrigger>();

                if (eventTrigger == null)
                    return;

#if UNITY_4_6 || UNITY_5_0
                var triggers = eventTrigger.delegates;
#else
                var triggers = eventTrigger.triggers;
#endif      

                if (triggers == null)
                    return;

                // check if view has drag event entries
                bool hasDragEntries = false;
                foreach (var entry in triggers)
                {
                    if (entry.eventID == EventTriggerType.BeginDrag ||
                        entry.eventID == EventTriggerType.EndDrag ||
                        entry.eventID == EventTriggerType.Drag ||
                        entry.eventID == EventTriggerType.InitializePotentialDrag)
                    {
                        hasDragEntries = true;
                    }
                }

                // unblock drag events if the view doesn't handle drag events
                if (!hasDragEntries)
                {
                    ScrollRect scrollRect = this;

                    // unblock initialize potential drag 
                    var initializePotentialDragEntry = new EventTrigger.Entry();
                    initializePotentialDragEntry.eventID = EventTriggerType.InitializePotentialDrag;
                    initializePotentialDragEntry.callback = new EventTrigger.TriggerEvent();
                    initializePotentialDragEntry.callback.AddListener(eventData =>
                    {
                        scrollRect.SendMessage("OnInitializePotentialDrag", eventData);
                    });
                    triggers.Add(initializePotentialDragEntry);

                    // unblock begin drag
                    var beginDragEntry = new EventTrigger.Entry();
                    beginDragEntry.eventID = EventTriggerType.BeginDrag;
                    beginDragEntry.callback = new EventTrigger.TriggerEvent();
                    beginDragEntry.callback.AddListener(eventData =>
                    {
                        scrollRect.SendMessage("OnBeginDrag", eventData);
                    });
                    triggers.Add(beginDragEntry);

                    // drag
                    var dragEntry = new EventTrigger.Entry();
                    dragEntry.eventID = EventTriggerType.Drag;
                    dragEntry.callback = new EventTrigger.TriggerEvent();
                    dragEntry.callback.AddListener(eventData =>
                    {
                        scrollRect.SendMessage("OnDrag", eventData);
                    });
                    triggers.Add(dragEntry);

                    // end drag
                    var endDragEntry = new EventTrigger.Entry();
                    endDragEntry.eventID = EventTriggerType.EndDrag;
                    endDragEntry.callback = new EventTrigger.TriggerEvent();
                    endDragEntry.callback.AddListener(eventData =>
                    {
                        scrollRect.SendMessage("OnEndDrag", eventData);
                    });
                    triggers.Add(endDragEntry);
                }
            });
        }

        #endregion
    }
}
