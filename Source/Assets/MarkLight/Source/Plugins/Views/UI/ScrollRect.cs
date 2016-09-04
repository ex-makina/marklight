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
        [MapTo("ScrollRectComponent.vertical")]
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
        [ChangeHandler("NormalizedPositionChanged", TriggerImmediately = true)]
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
        [ChangeHandler("NormalizedPositionChanged", TriggerImmediately = true)]
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
        [ChangeHandler("NormalizedPositionChanged", TriggerImmediately = true)]
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
        /// Scroll delta distance for disabling interaction.
        /// </summary>
        /// <d>If set any interaction with child views (clicks, etc) is disabled when the specified amount of pixels has been scrolled. This is used e.g. to disable clicks while scrolling a selectable list of items.</d>
        public _float DisableInteractionScrollDelta;

        /// <summary>
        /// Scrollable viewport.
        /// </summary>
        /// <d>References the RectTransform parent to the content.</d>
        [MapTo("ScrollRectComponent.viewport")]
        public _RectTransformComponent Viewport;

        /// <summary>
        /// Scrollable content alignment.
        /// </summary>
        /// <d>Scrollable content alignment. Also controls the initial position of the scrollbars.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementAlignment ContentAlignment;

        /// <summary>
        /// Indicates if normalized position should be updated from NormalizedPosition field.
        /// </summary>
        /// <d>When NormalizedPosition is changed from the outside UpdateNormalizedPosition is set to true so that the scroll rect updates from the field instead of the other way around.</d>
        [ChangeHandler("NormalizedPositionChanged", TriggerImmediately = true)]
        public _bool UpdateNormalizedPosition;

        /// <summary>
        /// ScrollRect component.
        /// </summary>
        /// <d>Component responsible for handling scrollable content.</d>
        public UnityEngine.UI.ScrollRect ScrollRectComponent;     

        private bool _hasDisabledInteraction;
        private int _updateNormalizedPositionCount;

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
            ScrollRectComponent.scrollSensitivity = 60f;
            ImageComponent.color = Color.clear;
        }
        
        /// <summary>
        /// Called when a child view has been added or removed.
        /// </summary>
        public override void ChildrenChanged()
        {
            base.ChildrenChanged();
            QueueChangeHandler("LayoutChanged");
        }

        /// <summary>
        /// Called when the layout of the view has been changed.
        /// </summary>
        public override void LayoutChanged()
        {
            var child = this.Find<UIView>(false);
            if (child == null)
                return;

            // set scrollrect content to first child
            if (ScrollRectComponent.content == null)
            {
                ScrollRectComponent.content = child.RectTransform;
            }

            if (ContentAlignment.IsSet)
            {
                child.Alignment.DirectValue = ContentAlignment.Value;
                child.Pivot.DirectValue = ContentAlignment.Value.ToPivot();
            }

            // workaround for panel blocking drag events in child views
            UnblockDragEvents();
            base.LayoutChanged();
        }

        /// <summary>
        /// Called each frame and updates the scroll rect.
        /// </summary>
        public virtual void Update()
        {
            // keep track of current normalized position
            if (!UpdateNormalizedPosition)
            {
                // set normalized position
                NormalizedPosition.DirectValue = ScrollRectComponent.normalizedPosition;
                HorizontalNormalizedPosition.DirectValue = ScrollRectComponent.normalizedPosition.x;
                VerticalNormalizedPosition.DirectValue = ScrollRectComponent.normalizedPosition.y;
                return;
            }

            // we get here if we want to change the normalized position from outside           
            if (ScrollRectComponent.normalizedPosition != NormalizedPosition.Value)
            {
                ScrollRectComponent.normalizedPosition = NormalizedPosition.Value;
            }

            // workaround for issue where scroll rect resets its normalized position when the content
            // updates its rect transform, keep updating for a few frames to restore reset position
            ++_updateNormalizedPositionCount;
            if (_updateNormalizedPositionCount > 3)
            {                
                UpdateNormalizedPosition.DirectValue = false;
            }            
        }

        /// <summary>
        /// Called when the normalized position of the scroll rect has changed.
        /// </summary>
        public void NormalizedPositionChanged()
        {
            Vector2 normalizedPosition = Vector2.zero;
            if (NormalizedPosition.IsSet)
            {
                normalizedPosition = NormalizedPosition.Value;
                UpdateNormalizedPosition.DirectValue = true;
            }

            if (HorizontalNormalizedPosition.IsSet || VerticalNormalizedPosition.IsSet)
            {
                if (HorizontalNormalizedPosition.IsSet)
                {
                    normalizedPosition.x = HorizontalNormalizedPosition.Value;
                }

                if (VerticalNormalizedPosition.IsSet)
                {
                    normalizedPosition.y = VerticalNormalizedPosition.Value;
                }

                NormalizedPosition.DirectValue = normalizedPosition;
                UpdateNormalizedPosition.DirectValue = true;
            }

            if (UpdateNormalizedPosition)
            {
                ScrollRectComponent.normalizedPosition = NormalizedPosition.Value;
                _updateNormalizedPositionCount = 0;
            }
        }

        /// <summary>
        /// Workaround for draggable child views blocking drag events.
        /// </summary>
        public void UnblockDragEvents()
        {
            this.ForEachChild<View>(x =>
            {
                UnblockDragEvents(x);
            });
        }

        /// <summary>
        /// Unblocks drag events on view.
        /// </summary>
        public void UnblockDragEvents(View view)
        {
            var eventTrigger = view.GetComponent<EventTrigger>();

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
            bool hasScrollEntries = false;
            foreach (var entry in triggers)
            {
                if (entry.eventID == EventTriggerType.BeginDrag ||
                    entry.eventID == EventTriggerType.EndDrag ||
                    entry.eventID == EventTriggerType.Drag ||
                    entry.eventID == EventTriggerType.InitializePotentialDrag)
                {
                    hasDragEntries = true;
                }
                else if (entry.eventID == EventTriggerType.Scroll)
                {
                    hasScrollEntries = true;
                }
            }

            // unblock scroll events if the view doesn't handle scrolling
            if (!hasScrollEntries)
            {
                // unblock scroll
                var scrollEntry = new EventTrigger.Entry();
                scrollEntry.eventID = EventTriggerType.Scroll;
                scrollEntry.callback = new EventTrigger.TriggerEvent();
                scrollEntry.callback.AddListener(eventData =>
                {
                    ScrollRectComponent.SendMessage("OnScroll", eventData);
                });
                triggers.Add(scrollEntry);
            }

            // unblock drag events if the view doesn't handle drag events
            if (!hasDragEntries)
            {
                // unblock initialize potential drag 
                var initializePotentialDragEntry = new EventTrigger.Entry();
                initializePotentialDragEntry.eventID = EventTriggerType.InitializePotentialDrag;
                initializePotentialDragEntry.callback = new EventTrigger.TriggerEvent();
                initializePotentialDragEntry.callback.AddListener(eventData =>
                {
                    SendMessage("OnInitializePotentialDrag", eventData);
                });
                triggers.Add(initializePotentialDragEntry);

                // unblock begin drag
                var beginDragEntry = new EventTrigger.Entry();
                beginDragEntry.eventID = EventTriggerType.BeginDrag;
                beginDragEntry.callback = new EventTrigger.TriggerEvent();
                beginDragEntry.callback.AddListener(eventData =>
                {
                    SendMessage("OnBeginDrag", eventData);
                });
                triggers.Add(beginDragEntry);

                // drag
                var dragEntry = new EventTrigger.Entry();
                dragEntry.eventID = EventTriggerType.Drag;
                dragEntry.callback = new EventTrigger.TriggerEvent();
                dragEntry.callback.AddListener(eventData =>
                {
                    SendMessage("OnDrag", eventData);
                });
                triggers.Add(dragEntry);

                // end drag
                var endDragEntry = new EventTrigger.Entry();
                endDragEntry.eventID = EventTriggerType.EndDrag;
                endDragEntry.callback = new EventTrigger.TriggerEvent();
                endDragEntry.callback.AddListener(eventData =>
                {
                    SendMessage("OnEndDrag", eventData);
                });
                triggers.Add(endDragEntry);
            }
        }

        /// <summary>
        /// Called on scroll rect drag end.
        /// </summary>
        public void ScrollRectEndDrag(PointerEventData eventData)
        {
            if (!DisableInteractionScrollDelta.IsSet)
                return;

            // unblock raycasts
            if (_hasDisabledInteraction)
            {
                this.ForEachChild<UIView>(x => x.RaycastBlockMode.Value = MarkLight.RaycastBlockMode.Default, false);
                _hasDisabledInteraction = false;
            }
        }

        /// <summary>
        /// Called on scroll rect drag.
        /// </summary>
        public void ScrollRectDrag(PointerEventData eventData)
        {
            UpdateNormalizedPosition.DirectValue = false;
            if (!DisableInteractionScrollDelta.IsSet || _hasDisabledInteraction)
                return;

            // block raycasts if scrolled specified delta distance
            var pos = eventData.position - eventData.pressPosition;
            if (pos.magnitude > DisableInteractionScrollDelta)
            {
                this.ForEachChild<UIView>(x => x.RaycastBlockMode.Value = MarkLight.RaycastBlockMode.Never, false);
                _hasDisabledInteraction = true;
            }
        }

        #endregion
    }
}
