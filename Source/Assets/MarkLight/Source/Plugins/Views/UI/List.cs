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
    /// List view.
    /// </summary>
    /// <d>The list view presents a selectable list of items. It can either contain a static list of ListItem views or one ListItem with IsTemplate="True". If bound to list data through the Items field the list uses the template to generate a dynamic list of ListItems.</d>
    [HideInPresenter]
    public class List : UIView
    {
        #region Fields

        #region ListMask

        /// <summary>
        /// Indicates if a list mask is to be used.
        /// </summary>
        /// <d>Boolean indicating if a list mask is to be used.</d>
        public _bool UseListMask;

        /// <summary>
        /// The width of the list mask image.
        /// </summary>
        /// <d>Specifies the width of the list mask image either in pixels or percents.</d>
        [MapTo("ListMask.Width")]
        public _ElementSize ListMaskWidth;

        /// <summary>
        /// The height of the list mask image.
        /// </summary>
        /// <d>Specifies the height of the list mask image either in pixels or percents.</d>
        [MapTo("ListMask.Height")]
        public _ElementSize ListMaskHeight;

        /// <summary>
        /// The offset of the list mask image.
        /// </summary>
        /// <d>Specifies the offset of the list mask image.</d>
        [MapTo("ListMask.Offset")]
        public _ElementMargin ListMaskOffset;

        /// <summary>
        /// List max image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the list max.</d>
        [MapTo("ListMask.BackgroundImage")]
        public _Sprite ListMaskImage;

        /// <summary>
        /// List max image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the list max.</d>
        [MapTo("ListMask.BackgroundImageType")]
        public _ImageType ListMaskImageType;

        /// <summary>
        /// List max image material.
        /// </summary>
        /// <d>The material of the list max image.</d>
        [MapTo("ListMask.BackgroundMaterial")]
        public _Material ListMaskMaterial;

        /// <summary>
        /// List max image color.
        /// </summary>
        /// <d>The color of the list max image.</d>
        [MapTo("ListMask.BackgroundColor")]
        public _Color ListMaskColor;

        /// <summary>
        /// List mask alignment.
        /// </summary>
        /// <d>Specifies the alignment of the list mask.</d>
        [MapTo("ListMask.Alignment")]
        public _ElementAlignment ListMaskAlignment;

        /// <summary>
        /// Indicates if list mask should be rendered.
        /// </summary>
        /// <d>Indicates if the list mask, i.e. the list mask background image sprite and color should be rendered.</d>
        [MapTo("ListMask.ShowMaskGraphic")]
        public _bool ListMaskShowGraphic;

        /// <summary>
        /// Content margin of the list.
        /// </summary>
        /// <d>Sets the margin of the list mask view that contains the contents of the list.</d>
        [MapTo("ListMask.Margin")]
        public _ElementMargin ContentMargin;

        /// <summary>
        /// List mask.
        /// </summary>
        /// <d>The list mask can be used to mask the list and its items using a mask graphic.</d>
        public Mask ListMask;

        #endregion

        #region ListPanel

        #region HorizontalScrollbar

        /// <summary>
        /// Orientation of the horizontal scrollbar.
        /// </summary>
        /// <d>Orientation of the horizontal scrollbar.</d>
        [MapTo("ListPanel.HorizontalScrollbarOrientation")]
        public _ElementOrientation HorizontalScrollbarOrientation;

        /// <summary>
        /// Breadth of the horizontal scrollbar.
        /// </summary>
        /// <d>Breadth of the horizontal scrollbar.</d>
        [MapTo("ListPanel.HorizontalScrollbarBreadth")]
        public _ElementSize HorizontalScrollbarBreadth;

        /// <summary>
        /// Scrollbar scroll direction.
        /// </summary>
        /// <d>Scrollbar scroll direction.</d>
        [MapTo("ListPanel.HorizontalScrollbarScrollDirection")]
        public _ScrollbarDirection HorizontalScrollbarScrollDirection;

        /// <summary>
        /// Scroll steps.
        /// </summary>
        /// <d>The number of steps to use for the value. A value of 0 disables use of steps.</d>
        [MapTo("ListPanel.HorizontalScrollbarNumberOfSteps")]
        public _int HorizontalScrollbarNumberOfSteps;

        /// <summary>
        /// Handle size.
        /// </summary>
        /// <d> The size of the horizontal scrollbar handle where 1 means it fills the entire horizontal scrollbar.</d>
        [MapTo("ListPanel.HorizontalScrollbarHandleSize")]
        public _float HorizontalScrollbarHandleSize;

        /// <summary>
        /// Scrollbar value.
        /// </summary>
        /// <d>The current value of the horizontal scrollbar, between 0 and 1.</d>
        [MapTo("ListPanel.HorizontalScrollbarValue")]
        public _float HorizontalScrollbarValue;

        /// <summary>
        /// Horizontal scrollbar image.
        /// </summary>
        /// <d>Horizontal scrollbar image sprite.</d>
        [MapTo("ListPanel.HorizontalScrollbarImage")]
        public _Sprite HorizontalScrollbarImage;

        /// <summary>
        /// Horizontal scrollbar image type.
        /// </summary>
        /// <d>Horizontal scrollbar image sprite type.</d>
        [MapTo("ListPanel.HorizontalScrollbarImageType")]
        public _ImageType HorizontalScrollbarImageType;

        /// <summary>
        /// Horizontal scrollbar image material.
        /// </summary>
        /// <d>Horizontal scrollbar image material.</d>
        [MapTo("ListPanel.HorizontalScrollbarMaterial")]
        public _Material HorizontalScrollbarMaterial;

        /// <summary>
        /// Horizontal scrollbar image color.
        /// </summary>
        /// <d>Horizontal scrollbar image color.</d>
        [MapTo("ListPanel.HorizontalScrollbarColor")]
        public _Color HorizontalScrollbarColor;

        /// <summary>
        /// Horizontal scrollbar handle image.
        /// </summary>
        /// <d>Horizontal scrollbar handle image sprite.</d>
        [MapTo("ListPanel.HorizontalScrollbarHandleImage")]
        public _Sprite HorizontalScrollbarHandleImage;

        /// <summary>
        /// Horizontal scrollbar handle image type.
        /// </summary>
        /// <d>Horizontal scrollbar handle image sprite type.</d>
        [MapTo("ListPanel.HorizontalScrollbarHandleImageType")]
        public _ImageType HorizontalScrollbarHandleImageType;

        /// <summary>
        /// Horizontal scrollbar handle image material.
        /// </summary>
        /// <d>Horizontal scrollbar handle image material.</d>
        [MapTo("ListPanel.HorizontalScrollbarHandleMaterial")]
        public _Material HorizontalScrollbarHandleMaterial;

        /// <summary>
        /// Horizontal scrollbar handle image color.
        /// </summary>
        /// <d>Horizontal scrollbar handle image color.</d>
        [MapTo("ListPanel.HorizontalScrollbarHandleColor")]
        public _Color HorizontalScrollbarHandleColor;

        #endregion

        #region VerticalScrollbar

        /// <summary>
        /// Orientation of the vertical scrollbar.
        /// </summary>
        /// <d>Orientation of the vertical scrollbar.</d>
        [MapTo("ListPanel.VerticalScrollbarOrientation")]
        public _ElementOrientation VerticalScrollbarOrientation;

        /// <summary>
        /// Breadth of the vertical scrollbar.
        /// </summary>
        /// <d>Breadth of the vertical scrollbar.</d>
        [MapTo("ListPanel.VerticalScrollbarBreadth")]
        public _ElementSize VerticalScrollbarBreadth;

        /// <summary>
        /// Scrollbar scroll direction.
        /// </summary>
        /// <d>Scrollbar scroll direction.</d>
        [MapTo("ListPanel.VerticalScrollbarScrollDirection")]
        public _ScrollbarDirection VerticalScrollbarScrollDirection;

        /// <summary>
        /// Scroll steps.
        /// </summary>
        /// <d>The number of steps to use for the value. A value of 0 disables use of steps.</d>
        [MapTo("ListPanel.VerticalScrollbarNumberOfSteps")]
        public _int VerticalScrollbarNumberOfSteps;

        /// <summary>
        /// Vertical scrollbar handle size.
        /// </summary>
        /// <d> The size of the vertical scrollbar handle where 1 means it fills the entire vertical scrollbar.</d>
        [MapTo("ListPanel.VerticalScrollbarHandleSize")]
        public _float VerticalScrollbarHandleSize;

        /// <summary>
        /// Scrollbar value.
        /// </summary>
        /// <d>The current value of the vertical scrollbar, between 0 and 1.</d>
        [MapTo("ListPanel.VerticalScrollbarValue")]
        public _float VerticalScrollbarValue;

        /// <summary>
        /// Vertical scrollbar image.
        /// </summary>
        /// <d>Vertical scrollbar image sprite.</d>
        [MapTo("ListPanel.VerticalScrollbarImage")]
        public _Sprite VerticalScrollbarImage;

        /// <summary>
        /// Vertical scrollbar image type.
        /// </summary>
        /// <d>Vertical scrollbar image sprite type.</d>
        [MapTo("ListPanel.VerticalScrollbarImageType")]
        public _ImageType VerticalScrollbarImageType;

        /// <summary>
        /// Vertical scrollbar image material.
        /// </summary>
        /// <d>Vertical scrollbar image material.</d>
        [MapTo("ListPanel.VerticalScrollbarMaterial")]
        public _Material VerticalScrollbarMaterial;

        /// <summary>
        /// Vertical scrollbar image color.
        /// </summary>
        /// <d>Vertical scrollbar image color.</d>
        [MapTo("ListPanel.VerticalScrollbarColor")]
        public _Color VerticalScrollbarColor;

        /// <summary>
        /// Vertical scrollbar handle image.
        /// </summary>
        /// <d>Vertical scrollbar handle image sprite.</d>
        [MapTo("ListPanel.VerticalScrollbarHandleImage")]
        public _Sprite VerticalScrollbarHandleImage;

        /// <summary>
        /// Vertical scrollbar handle image type.
        /// </summary>
        /// <d>Vertical scrollbar handle image sprite type.</d>
        [MapTo("ListPanel.VerticalScrollbarHandleImageType")]
        public _ImageType VerticalScrollbarHandleImageType;

        /// <summary>
        /// Vertical scrollbar handle image material.
        /// </summary>
        /// <d>Vertical scrollbar handle image material.</d>
        [MapTo("ListPanel.VerticalScrollbarHandleMaterial")]
        public _Material VerticalScrollbarHandleMaterial;

        /// <summary>
        /// Vertical scrollbar handle image color.
        /// </summary>
        /// <d>Vertical scrollbar handle image color.</d>
        [MapTo("ListPanel.VerticalScrollbarHandleColor")]
        public _Color VerticalScrollbarHandleColor;

        #endregion

        #region ScrollRect

        /// <summary>
        /// Indicates if the content can scroll horizontally.
        /// </summary>
        /// <d>Boolean indicating if the content can be scrolled horizontally.</d>
        [MapTo("ListPanel.CanScrollHorizontally")]
        public _bool CanScrollHorizontally;

        /// <summary>
        /// Indicates if the content can scroll vertically.
        /// </summary>
        /// <d>Boolean indicating if the content can be scrolled vertically.</d>
        [MapTo("ListPanel.CanScrollVertically")]
        public _bool CanScrollVertically;

        /// <summary>
        /// Scroll deceleration rate.
        /// </summary>
        /// <d>Value indicating the rate of which the scroll stops moving.</d>
        [MapTo("ListPanel.DecelerationRate")]
        public _float DecelerationRate;

        /// <summary>
        /// Scroll elasticity.
        /// </summary>
        /// <d>Value indicating how elastic the scrolling is when moved beyond the bounds of the scrollable content.</d>
        [MapTo("ListPanel.Elasticity")]
        public _float Elasticity;

        /// <summary>
        /// Horizontal normalized position.
        /// </summary>
        /// <d>Value between 0-1 indicating the position of the scrollable content.</d>
        [MapTo("ListPanel.HorizontalNormalizedPosition")]
        public _float HorizontalNormalizedPosition;

        /// <summary>
        /// Space between scrollbar and scrollable content.
        /// </summary>
        /// <d>Space between scrollbar and scrollable content.</d>
        [MapTo("ListPanel.HorizontalScrollbarSpacing")]
        public _float HorizontalScrollbarSpacing;

        /// <summary>
        /// Indicates if scroll has intertia.
        /// </summary>
        /// <d>Boolean indicating if the scroll has inertia.</d>
        [MapTo("ListPanel.HasInertia")]
        public _bool HasInertia;

        /// <summary>
        /// Behavior when scrolled beyond bounds.
        /// </summary>
        /// <d>Enum specifying the behavior to use when the content moves beyond the scroll rect.</d>
        [MapTo("ListPanel.MovementType")]
        public _ScrollRectMovementType MovementType;

        /// <summary>
        /// Normalized position of the scroll.
        /// </summary>
        /// <d>The scroll position as a Vector2 between (0,0) and (1,1) with (0,0) being the lower left corner.</d>
        [MapTo("ListPanel.NormalizedPosition")]
        public _Vector2 NormalizedPosition;

        /// <summary>
        /// Scroll sensitivity.
        /// </summary>
        /// <d>Value indicating how sensitive the scrolling is to scroll wheel and track pad movement.</d>
        [MapTo("ListPanel.ScrollSensitivity")]
        public _float ScrollSensitivity;

        /// <summary>
        /// Current velocity of the content.
        /// </summary>
        /// <d>Indicates the current velocity of the scrolled content.</d>
        [MapTo("ListPanel.ScrollVelocity")]
        public _Vector2 ScrollVelocity;

        /// <summary>
        /// Vertical normalized position.
        /// </summary>
        /// <d>Value between 0-1 indicating the position of the scrollable content.</d>
        [MapTo("ListPanel.VerticalNormalizedPosition")]
        public _float VerticalNormalizedPosition;

        /// <summary>
        /// Space between scrollbar and scrollable content.
        /// </summary>
        /// <d>Space between scrollbar and scrollable content.</d>
        [MapTo("ListPanel.VerticalScrollbarSpacing")]
        public _float VerticalScrollbarSpacing;

        /// <summary>
        /// Scroll delta distance for disabling interaction.
        /// </summary>
        /// <d>If set any interaction with child views (clicks, etc) is disabled when the specified distance has been scrolled. This is used e.g. to disable clicks while scrolling a selectable list of items.</d>
        [MapTo("ListPanel.DisableInteractionScrollDelta")]
        public _float DisableInteractionScrollDelta;

        #endregion

        /// <summary>
        /// Indicates if mask margin should be added.
        /// </summary>
        /// <d>Boolean indicating if margin should be added to the content mask to make room for the scrollbars.</d>
        [MapTo("ListPanel.AddMaskMargin")]
        public _bool AddMaskMargin;

        /// <summary>
        /// Horizontal scrollbar visibility of scrollable list content.
        /// </summary>
        /// <d>Horizontal scrollbar visibility of scrollable list content.</d>
        [MapTo("ListPanel.HorizontalScrollbarVisibility")]
        public _PanelScrollbarVisibility HorizontalScrollbarVisibility;

        /// <summary>
        /// Vertical scrollbar visibility of scrollable list content.
        /// </summary>
        /// <d>Vertical scrollbar visibility of scrollable list content.</d>
        [MapTo("ListPanel.VerticalScrollbarVisibility")]
        public _PanelScrollbarVisibility VerticalScrollbarVisibility;

        /// <summary>
        /// Alignment of scrollable list content.
        /// </summary>
        /// <d>Sets the alignment of the scrollable list content.</d>
        [MapTo("ListPanel.ContentAlignment")]
        public _ElementAlignment ScrollableContentAlignment;

        /// <summary>
        /// Indicates if the items should alternate in style.
        /// </summary>
        /// <d>Boolean indicating if the ListItem style should alternate between "Default" and "Alternate".</d>
        public _bool AlternateItems;

        /// <summary>
        /// Indicates if the list is scrollable.
        /// </summary>
        /// <d>Boolean indicating if the list is to be scrollable.</d>
        public _bool IsScrollable;

        /// <summary>
        /// Scrollable region of the list that contains the list items.
        /// </summary>
        /// <d>Scrollable region of the list that contains the list items. Set to null if the list isn't scrollable.</d>
        public Region ScrollContent;

        /// <summary>
        /// Panel containing scrollable list content.
        /// </summary>
        /// <d>Panel containing scrollable list content. Will be null if IsScrollable is set to False.
        public Panel ListPanel;

        #endregion

        /// <summary>
        /// User-defined data list.
        /// </summary>
        /// <d>Can be bound to an generic ObservableList to dynamically generate ListItems based on a template.</d>
        [ChangeHandler("ItemsChanged")]
        public _IObservableList Items;

        /// <summary>
        /// Orientation of the list.
        /// </summary>
        /// <d>Defines how the list items should be arranged.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementOrientation Orientation;

        /// <summary>
        /// Boolean indicating if list item arrangement should be disabled.
        /// </summary>
        /// <d>If set to true the list doesn't automatically arrange one item after another. Used when item arrangement is done elsewhere.</d>
        public _bool DisableItemArrangement;

        /// <summary>
        /// Indicates if an item is selected.
        /// </summary>
        /// <d>Set to true when a list item is selected.</d>
        public _bool IsItemSelected;

        /// <summary>
        /// Indicates if items can be deselected by clicking.
        /// </summary>
        /// <d>A boolean indicating if items in the list can be deselected by clicking. Items can always be deselected programmatically.</d>
        public _bool CanDeselect;

        /// <summary>
        /// Indicates if more than one list item can be selected.
        /// </summary>
        /// <d>A boolean indicating if more than one list items can be selected by clicking or programmatically.</d>
        public _bool CanMultiSelect;

        /// <summary>
        /// Indicates if items can be selected by clicking.
        /// </summary>
        /// <d>A boolean indicating if items can be selected by clicking. Items can always be selected programmatically.</d>
        public _bool CanSelect;

        /// <summary>
        /// Indicates if item can be selected again if it's already selected.
        /// </summary>
        /// <d>Boolean indicating if the item can be selected again if it is already selected. This setting is ignored if CanDeselect is True.</d>
        public _bool CanReselect;

        /// <summary>
        /// Indicates if items are deselected immediately after being selected.
        /// </summary>
        /// <d>A boolean indicating if items are deselected immediately after being selected. Useful if you want to trigger selection action but don't want the item to remain selected.</d>
        public _bool DeselectAfterSelect;

        /// <summary>
        /// Indicates how items overflow.
        /// </summary>
        /// <d>Enum indicating how items should overflow as they reach the boundaries of the list.</d>
        public _OverflowMode Overflow;

        /// <summary>
        /// Spacing between list items.
        /// </summary>
        /// <d>The spacing between list items.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSize Spacing;

        /// <summary>
        /// Horizontal spacing between list items.
        /// </summary>
        /// <d>The horizontal spacing between list items.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSize HorizontalSpacing;

        /// <summary>
        /// Vertical spacing between list items.
        /// </summary>
        /// <d>The vertical spacing between list items.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSize VerticalSpacing;

        /// <summary>
        /// The alignment of list items.
        /// </summary>
        /// <d>If the list items varies in size the content alignment specifies how the list items should be arranged in relation to each other.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementAlignment ContentAlignment;

        /// <summary>
        /// Sort direction.
        /// </summary>
        /// <d>If list items has SortIndex set they can be sorted in the direction specified.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementSortDirection SortDirection;

        /// <summary>
        /// Indicates if items are selected on mouse up.
        /// </summary>
        /// <d>Boolean indicating if items are selected on mouse up rather than mouse down (default).</d>
        public _bool SelectOnMouseUp;

        /// <summary>
        /// Indicates if template is to be shown in the editor.
        /// </summary>
        /// <d>Boolean indicating if template should be shown in the editor.</d>
        public _bool ShowTemplateInEditor;

        /// <summary>
        /// List item pool size.
        /// </summary>
        /// <d>Indicates how many list items should be pooled. Pooled items are already created and ready to be used rather than being created and destroyed on demand. Can be used to increase the performance of dynamic lists.</d>
        public _int PoolSize;

        /// <summary>
        /// Max list item pool size.
        /// </summary>
        /// <d>Indicates maximum number of list items that should be pooled. If not set it uses initial PoolSize is used as max. Pooled items are already created and ready to be used rather than being created and destroyed on demand. Can be used to increase the performance of dynamic lists.</d>
        public _int MaxPoolSize;

        /// <summary>
        /// Indicates if list should use virtualization.
        /// </summary>
        /// <d>Boolean indicating if list should use virtualization where only visible list items are presented in the visual hierarchy.</d>
        public _bool UseVirtualization;

        /// <summary>
        /// Indicates how much margin should be added to the realization viewport.
        /// </summary>
        /// <d>Boolean indicating how much margin should be added to the realization viewport. If zero the realization viewport will be the same size as the scrollable viewport. Used when UseVirtualization is True.</d>
        public _float RealizationMargin;

        /// <summary>
        /// Indicates how many pixels should be scrolled before virtualization updates.
        /// </summary>
        /// <d>Boolean indicating how many pixels should be scrolled before virtualization updates.</d>
        public _float VirtualizationUpdateThreshold;

        /// <summary>
        /// List item padding.
        /// </summary>
        /// <d>Adds padding to the list.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementMargin Padding;

        /// <summary>
        /// Selected data list item.
        /// </summary>
        /// <d>Set when the selected list item changes and points to the user-defined data item.</d>
        [ChangeHandler("SelectedItemChanged")]
        public _object SelectedItem;

        /// <summary>
        /// Selected items in the data list.
        /// </summary>
        /// <d>Contains selected items in the user-defined list data. Can contain more than one item if IsMultiSelect is true.</d>
        public _GenericObservableList SelectedItems;

        /// <summary>
        /// Item selected view action.
        /// </summary>
        /// <d>Triggered when a list item is selected either by user interaction or programmatically.</d>
        /// <actionData>ItemSelectionActionData</actionData>
        public ViewAction ItemSelected;

        /// <summary>
        /// Item deselected view action.
        /// </summary>
        /// <d>Triggered when a list item is deselected either by user interaction or programmatically. An item is deselected if another item is selected and CanMultiSelect is false. If CanMultiSelect is true an item is deselected when the user clicks on an selected item.</d>
        /// <actionData>ItemSelectionActionData</actionData>
        public ViewAction ItemDeselected;

        /// <summary>
        /// List changed view action.
        /// </summary>
        /// <d>Triggered when the list changes (items added, removed or moved).</d>
        /// <actionData>ListChangedActionData</actionData>
        public ViewAction ListChanged;

        private IObservableList _oldItems;
        private List<ListItem> _presentedListItems;
        private List<ListItem> _listItemTemplates;
        private object _selectedItem;
        private bool _updateWidth;
        private bool _updateHeight;
        private Dictionary<View, ViewPool> _viewPools;
        private VirtualizedItems _virtualizedItems;
        private float _previousViewportMin;
        private bool _updateVirtualization;

        #endregion

        #region Methods

        /// <summary>
        /// Checks if ActualWidth changes and updates the layout.
        /// </summary>
        public virtual void Update()
        {
            // adjust virtualized/realized items
            if (UseVirtualization)
            {
                UpdateVirtualizedItems();
            }

            // adjust width if list is wrapped
            if (Overflow.Value != OverflowMode.Wrap)
                return;

            if (!_updateWidth && !_updateWidth)
                return;

            if (_updateWidth && ActualWidth > 0)
            {
                QueueChangeHandler("LayoutChanged");
                _updateWidth = false;
            }

            if (_updateHeight && ActualHeight > 0)
            {
                QueueChangeHandler("LayoutChanged");
                _updateHeight = false;
            }
        }

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            Spacing.DirectValue = new ElementSize();
            CanSelect.DirectValue = true;
            CanDeselect.DirectValue = false;
            CanMultiSelect.DirectValue = false;
            Padding.DirectValue = new ElementMargin();
            RealizationMargin.DirectValue = 50;
            VirtualizationUpdateThreshold.DirectValue = 25;
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
        /// Called whenever the list is scrolled or items are added, removed or rearranged.
        /// </summary>
        public void UpdateVirtualizedItems()
        {
            float vpMin = 0;
            float vpMax = 0;

            if (Orientation.Value == ElementOrientation.Vertical)
            {
                float viewportHeight = ListPanel.ScrollRect.ActualHeight;
                float scrollHeight = ScrollContent.ActualHeight - viewportHeight;
                vpMin = (1.0f - VerticalNormalizedPosition.Value) * scrollHeight - RealizationMargin.Value;
                vpMax = vpMin + viewportHeight + RealizationMargin.Value;                
            }
            else
            {
                float viewportWidth = ListPanel.ScrollRect.ActualWidth;
                float scrollWidth = ScrollContent.ActualWidth - viewportWidth;
                vpMin = (1.0f - HorizontalNormalizedPosition.Value) * scrollWidth - RealizationMargin.Value;
                vpMax = vpMin + viewportWidth + RealizationMargin.Value;
            }

            // only update when we have scrolled further than the threshold since last update
            if (Mathf.Abs(_previousViewportMin - vpMin) <= VirtualizationUpdateThreshold.Value && !_updateVirtualization)
                return;

            _updateVirtualization = false;
            _previousViewportMin = vpMin;
            var newItems = _virtualizedItems.GetItemsInRange(vpMin, vpMax);

            // remove any items not in new list from viewport to virtualized list
            var previousItems = Content.GetChildren<ListItem>(x => x.IsLive, false);
            foreach (var item in previousItems)
            {
                if (!_virtualizedItems.IsInRange(item, vpMin, vpMax))
                {
                    item.MoveTo(_virtualizedItems.VirtualizedItemsContainer);
                }
            }

            // add new items to viewport
            foreach (var item in newItems)
            {
                item.MoveTo(Content);
                ListPanel.ScrollRect.UnblockDragEvents(item);
            }
        }

        /// <summary>
        /// Updates the layout of the view.
        /// </summary>
        public override void LayoutChanged()
        {
            if (DisableItemArrangement)
            {
                return;
            }

            if (ListPanel != null)
            {
                AdjustScrollableLayout();
            }

            // arrange items like a group
            float horizontalSpacing = HorizontalSpacing.IsSet ? HorizontalSpacing.Value.Pixels : Spacing.Value.Pixels;
            float verticalSpacing = VerticalSpacing.IsSet ? VerticalSpacing.Value.Pixels : Spacing.Value.Pixels;
            float maxWidth = 0f;
            float maxHeight = 0f;
            float totalWidth = 0f;
            float totalHeight = 0f;
            bool percentageWidth = false;
            bool percentageHeight = false;

            bool isHorizontal = Orientation == ElementOrientation.Horizontal;

            var children = new List<ListItem>();
            var childrenToBeSorted = new List<ListItem>();

            _presentedListItems.ForEach(x =>
            {
                // should this be sorted?
                if (x.SortIndex != 0)
                {
                    // yes. 
                    childrenToBeSorted.Add(x);
                    return;
                }

                children.Add(x);
            });

            if (SortDirection == ElementSortDirection.Ascending)
            {
                children.AddRange(childrenToBeSorted.OrderBy(x => x.SortIndex.Value));
            }
            else
            {
                children.AddRange(childrenToBeSorted.OrderByDescending(x => x.SortIndex.Value));
            }

            // get size of content and set content offsets and alignment
            int childCount = children.Count;
            int childIndex = 0;
            bool firstItem = true;
            float xOffset = 0;
            float yOffset = 0;
            float maxColumnWidth = 0;
            float maxRowHeight = 0;

            for (int i = 0; i < childCount; ++i)
            {
                var view = children[i];

                // don't group disabled or destroyed views
                if (!view.IsActive || view.IsDestroyed)
                    continue;

                if (view.Width.Value.Unit == ElementSizeUnit.Percents)
                {
                    if (isHorizontal)
                    {
                        Utils.LogWarning("[MarkLight] Unable to group view \"{0}\" horizontally as it doesn't specify its width in pixels or elements.", view.GameObjectName);
                        continue;
                    }
                    else
                    {
                        percentageWidth = true;
                    }
                }

                if (view.Height.Value.Unit == ElementSizeUnit.Percents)
                {
                    if (!isHorizontal)
                    {
                        Utils.LogWarning("[MarkLight] Unable to group view \"{0}\" vertically as it doesn't specify its height in pixels or elements.", view.GameObjectName);
                        continue;
                    }
                    else
                    {
                        percentageHeight = true;
                    }
                }

                if (Overflow == OverflowMode.Overflow)
                {
                    // set offsets and alignment
                    var offset = new ElementMargin(
                        new ElementSize(isHorizontal ? totalWidth + horizontalSpacing * childIndex : 0f, ElementSizeUnit.Pixels),
                        new ElementSize(!isHorizontal ? totalHeight + verticalSpacing * childIndex : 0f, ElementSizeUnit.Pixels));
                    view.OffsetFromParent.DirectValue = offset;

                    // set desired alignment if it is valid for the orientation otherwise use defaults                
                    var alignment = isHorizontal ? ElementAlignment.Left : ElementAlignment.Top;
                    var desiredAlignment = ContentAlignment.IsSet ? ContentAlignment : view.Alignment;
                    if (isHorizontal && (desiredAlignment == ElementAlignment.Top || desiredAlignment == ElementAlignment.Bottom
                        || desiredAlignment == ElementAlignment.TopLeft || desiredAlignment == ElementAlignment.BottomLeft))
                    {
                        view.Alignment.DirectValue = alignment | desiredAlignment;
                    }
                    else if (!isHorizontal && (desiredAlignment == ElementAlignment.Left || desiredAlignment == ElementAlignment.Right
                        || desiredAlignment == ElementAlignment.TopLeft || desiredAlignment == ElementAlignment.TopRight))
                    {
                        view.Alignment.DirectValue = alignment | desiredAlignment;
                    }
                    else
                    {
                        view.Alignment.DirectValue = alignment;
                    }

                    // get size of content
                    if (!percentageWidth)
                    {
                        totalWidth += view.Width.Value.Pixels;
                        maxWidth = Mathf.Max(maxWidth, view.Width.Value.Pixels);
                    }

                    if (!percentageHeight)
                    {
                        totalHeight += view.Height.Value.Pixels;
                        maxHeight = Mathf.Max(maxHeight, view.Height.Value.Pixels);
                    }
                }
                else
                {
                    // overflow mode is set to wrap
                    // set alignment
                    view.Alignment.DirectValue = ElementAlignment.TopLeft;

                    // set offsets of item
                    if (isHorizontal)
                    {
                        if (firstItem)
                        {
                            // first item, don't check for overflow
                            xOffset = 0;
                            firstItem = false;
                        }
                        else if ((xOffset + view.Width.Value.Pixels + horizontalSpacing) > ActualWidth)
                        {
                            // item overflows to the next row
                            xOffset = 0;
                            yOffset += maxRowHeight + verticalSpacing;
                            maxRowHeight = 0;
                        }
                        else
                        {
                            // item continues on the same row
                            xOffset += horizontalSpacing;
                        }

                        // set offset
                        view.OffsetFromParent.DirectValue = new ElementMargin(ElementSize.FromPixels(xOffset), ElementSize.FromPixels(yOffset));
                        xOffset += view.Width.Value.Pixels;
                        maxRowHeight = Mathf.Max(maxRowHeight, view.Height.Value.Pixels);
                        maxWidth = Mathf.Max(maxWidth, xOffset);
                        maxHeight = Mathf.Max(maxHeight, yOffset + view.Height.Value.Pixels);
                    }
                    else
                    {
                        if (firstItem)
                        {
                            yOffset = 0;
                            firstItem = false;
                        }
                        else if ((yOffset + view.Height.Value.Pixels + verticalSpacing) > ActualHeight)
                        {
                            // overflow to next column                        
                            yOffset = 0;
                            xOffset += maxColumnWidth + horizontalSpacing;
                            maxColumnWidth = 0;
                        }
                        else
                        {
                            // add spacing
                            yOffset += verticalSpacing;
                        }

                        // set offset
                        view.OffsetFromParent.DirectValue = new ElementMargin(ElementSize.FromPixels(xOffset), ElementSize.FromPixels(yOffset));
                        yOffset += view.Height.Value.Pixels;
                        maxColumnWidth = Mathf.Max(maxColumnWidth, view.Width.Value.Pixels);
                        maxWidth = Mathf.Max(maxWidth, xOffset + view.Width.Value.Pixels);
                        maxHeight = Mathf.Max(maxHeight, yOffset);
                    }
                }

                // update child layout
                view.RectTransformChanged();
                ++childIndex;
            }

            bool updateScrollContent = false;
            ElementMargin ListMaskMargin = ListMask != null ? ListMask.Margin.Value : new ElementMargin();
            if (Overflow == OverflowMode.Overflow)
            {
                // add margins
                totalWidth += isHorizontal ? (childCount > 1 ? (childIndex - 1) * horizontalSpacing : 0f) : 0f;
                totalWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels + ListMaskMargin.Left.Pixels + ListMaskMargin.Right.Pixels
                    + Padding.Value.Left.Pixels + Padding.Value.Right.Pixels;
                maxWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels + ListMaskMargin.Left.Pixels + ListMaskMargin.Right.Pixels
                    + Padding.Value.Left.Pixels + Padding.Value.Right.Pixels;

                // set width and height of list            
                if (!Width.IsSet)
                {
                    // if width is not explicitly set then adjust to content
                    if (!percentageWidth)
                    {
                        // adjust width to content
                        Width.DirectValue = new ElementSize(isHorizontal ? totalWidth : maxWidth, ElementSizeUnit.Pixels);
                    }
                    else
                    {
                        Width.DirectValue = new ElementSize(1, ElementSizeUnit.Percents);
                    }
                }
                else if (ScrollContent != null)
                {
                    // adjust width of scrollable area to size
                    ScrollContent.Width.DirectValue = percentageWidth ? new ElementSize(1, ElementSizeUnit.Percents) :
                        new ElementSize(isHorizontal ? totalWidth : maxWidth, ElementSizeUnit.Pixels);
                    updateScrollContent = true;
                }

                // add margins
                totalHeight += !isHorizontal ? (childCount > 1 ? (childIndex - 1) * verticalSpacing : 0f) : 0f;
                totalHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels + ListMaskMargin.Top.Pixels + ListMaskMargin.Bottom.Pixels
                    + Padding.Value.Top.Pixels + Padding.Value.Bottom.Pixels;
                maxHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels + ListMaskMargin.Top.Pixels + ListMaskMargin.Bottom.Pixels
                    + Padding.Value.Top.Pixels + Padding.Value.Bottom.Pixels;

                if (!Height.IsSet)
                {
                    // if height is not explicitly set then adjust to content
                    if (!percentageHeight)
                    {
                        // adjust height to content
                        Height.DirectValue = new ElementSize(!isHorizontal ? totalHeight : maxHeight, ElementSizeUnit.Pixels);
                    }
                    else
                    {
                        Height.DirectValue = new ElementSize(1, ElementSizeUnit.Percents);
                    }
                }
                else if (ScrollContent != null)
                {
                    // adjust width of scrollable area to size
                    ScrollContent.Height.DirectValue = percentageHeight ? new ElementSize(1, ElementSizeUnit.Percents) :
                        new ElementSize(!isHorizontal ? totalHeight : maxHeight, ElementSizeUnit.Pixels);
                    updateScrollContent = true;
                }
            }
            else
            {
                // adjust size to content
                if (isHorizontal)
                {
                    maxHeight += Margin.Value.Top.Pixels + Margin.Value.Bottom.Pixels + ListMaskMargin.Top.Pixels +
                        ListMaskMargin.Bottom.Pixels + Padding.Value.Top.Pixels + Padding.Value.Bottom.Pixels;

                    if (ScrollContent != null)
                    {
                        ScrollContent.Height.DirectValue = ElementSize.FromPixels(maxHeight);
                        updateScrollContent = true;
                    }
                    else
                    {
                        Height.DirectValue = ElementSize.FromPixels(maxHeight);
                    }
                }
                else
                {
                    maxWidth += Margin.Value.Left.Pixels + Margin.Value.Right.Pixels + ListMaskMargin.Left.Pixels +
                        ListMaskMargin.Right.Pixels + Padding.Value.Left.Pixels + Padding.Value.Right.Pixels;

                    if (ScrollContent != null)
                    {
                        ScrollContent.Width.DirectValue = ElementSize.FromPixels(maxWidth);
                        updateScrollContent = true;
                    }
                    else
                    {
                        Width.DirectValue = ElementSize.FromPixels(maxWidth);
                    }
                }
            }

            if (updateScrollContent)
            {
                ScrollContent.RectTransformChanged();
            }

            if (UseVirtualization)
            {
                _updateVirtualization = true;
            }

            base.LayoutChanged();
        }

        /// <summary>
        /// Gets all list items (realized and virtualized) that are active in the list.
        /// </summary>
        public List<ListItem> GetActiveListItems()
        {
            List<ListItem> listItems = new List<ListItem>();
            if (UseVirtualization)
            {
                listItems.AddRange(_virtualizedItems.VirtualizedItemsContainer.GetChildren<ListItem>(x => x.IsLive, false));
            }

            listItems.AddRange(Content.GetChildren<ListItem>(x => x.IsLive, false));
            return listItems;
        }

        /// <summary>
        /// Adjusts scrollable layout.
        /// </summary>
        private void AdjustScrollableLayout()
        {
            // set default scrollable content alignment based on orientation
            if (!ScrollableContentAlignment.IsSet)
            {
                if (Overflow.Value == OverflowMode.Overflow)
                {
                    ScrollableContentAlignment.Value = Orientation.Value == ElementOrientation.Vertical ? ElementAlignment.Top : ElementAlignment.Left;
                }
                else
                {
                    ScrollableContentAlignment.Value = Orientation.Value == ElementOrientation.Vertical ? ElementAlignment.Left : ElementAlignment.Top;
                }
            }

            // set default scrollbar visibility based on orientation
            if (!HorizontalScrollbarVisibility.IsSet)
            {
                if (Overflow.Value == OverflowMode.Overflow)
                {
                    HorizontalScrollbarVisibility.Value = Orientation.Value == ElementOrientation.Horizontal ? PanelScrollbarVisibility.AutoHideAndExpandViewport : PanelScrollbarVisibility.Hidden;
                }
                else
                {
                    HorizontalScrollbarVisibility.Value = Orientation.Value == ElementOrientation.Vertical ? PanelScrollbarVisibility.AutoHideAndExpandViewport : PanelScrollbarVisibility.Hidden;
                }

            }

            if (!VerticalScrollbarVisibility.IsSet)
            {
                if (Overflow.Value == OverflowMode.Overflow)
                {
                    VerticalScrollbarVisibility.Value = Orientation.Value == ElementOrientation.Vertical ? PanelScrollbarVisibility.AutoHideAndExpandViewport : PanelScrollbarVisibility.Hidden;
                }
                else
                {
                    VerticalScrollbarVisibility.Value = Orientation.Value == ElementOrientation.Horizontal ? PanelScrollbarVisibility.AutoHideAndExpandViewport : PanelScrollbarVisibility.Hidden;
                }
            }

            // set default allowed scroll direction
            if (!CanScrollHorizontally.IsSet)
            {
                if (Overflow.Value == OverflowMode.Overflow)
                {
                    CanScrollHorizontally.Value = Orientation.Value == ElementOrientation.Horizontal ? true : false;
                }
                else
                {
                    CanScrollHorizontally.Value = Orientation.Value == ElementOrientation.Vertical ? true : false;
                }
            }

            if (!CanScrollVertically.IsSet)
            {
                if (Overflow.Value == OverflowMode.Overflow)
                {
                    CanScrollVertically.Value = Orientation.Value == ElementOrientation.Vertical ? true : false;
                }
                else
                {
                    CanScrollVertically.Value = Orientation.Value == ElementOrientation.Horizontal ? true : false;
                }
            }
        }

        /// <summary>
        /// Called when the selected item of the list has been changed.
        /// </summary>
        public virtual void SelectedItemChanged()
        {
            if (_selectedItem == SelectedItem.Value)
            {
                return;
            }

            SelectItem(SelectedItem.Value);
        }

        /// <summary>
        /// Selects item in the list.
        /// </summary>
        public void SelectItem(ListItem listItem, bool triggeredByClick = false)
        {
            if (listItem == null || (triggeredByClick && !CanSelect))
                return;

            // is item already selected?
            if (listItem.IsSelected)
            {
                // yes. can it be deselected?
                if (triggeredByClick && !CanDeselect)
                {
                    // no. should it be re-selected?
                    if (CanReselect)
                    {
                        // yes. select it again
                        SetSelected(listItem, true);
                    }

                    return; // no.
                }

                // deselect and trigger actions
                SetSelected(listItem, false);
            }
            else
            {
                // select
                SetSelected(listItem, true);

                // deselect other items if we can't multi-select
                if (!CanMultiSelect)
                {
                    foreach (var presentedListItem in _presentedListItems)
                    {
                        if (presentedListItem == listItem)
                            continue;

                        // deselect and trigger actions
                        SetSelected(presentedListItem as ListItem, false);
                    }
                }

                // should this item immediately be deselected?
                if (DeselectAfterSelect)
                {
                    // yes.
                    SetSelected(listItem, false);
                }
            }
        }

        /// <summary>
        /// Selects item in the list.
        /// </summary>
        public void SelectItem(int index)
        {
            if (index >= _presentedListItems.Count)
            {
                Utils.LogError("[MarkLight] {0}: Unable to select list item. Index out of bounds.", GameObjectName);
                return;
            }

            SelectItem(_presentedListItems[index] as ListItem, false);
        }

        /// <summary>
        /// Selects item in the list.
        /// </summary>
        public void SelectItem(object itemData)
        {
            var listItem = _presentedListItems.FirstOrDefault(x =>
            {
                var item = x as ListItem;
                return item != null ? item.Item.Value == itemData : false;
            });

            if (listItem == null)
            {
                Utils.LogError("[MarkLight] {0}: Unable to select list item. Item not found.", GameObjectName);
                return;
            }

            SelectItem(listItem as ListItem, false);
        }

        /// <summary>
        /// Selects or deselects a list item.
        /// </summary>
        private void SetSelected(ListItem listItem, bool selected)
        {
            if (listItem == null)
                return;

            listItem.IsSelected.Value = selected;
            if (selected)
            {
                // item selected
                _selectedItem = listItem.Item.Value;
                SelectedItem.Value = _selectedItem;
                IsItemSelected.Value = true;
                if (Items.Value != null)
                {
                    Items.Value.SetSelected(_selectedItem);
                }

                // add to list of selected items
                if (!SelectedItems.Value.Contains(listItem.Item.Value))
                {
                    SelectedItems.Value.Add(listItem.Item.Value);
                }

                // trigger item selected action
                if (ItemSelected.HasEntries)
                {
                    ItemSelected.Trigger(new ItemSelectionActionData { IsSelected = true, ItemView = listItem, Item = listItem.Item.Value });
                }
            }
            else
            {
                // remove from list of selected items
                SelectedItems.Value.Remove(listItem.Item.Value);

                // set selected item
                if (SelectedItem.Value == listItem.Item.Value)
                {
                    _selectedItem = SelectedItems.Value.LastOrDefault();
                    SelectedItem.Value = _selectedItem;

                    if (Items.Value != null)
                    {
                        Items.Value.SetSelected(_selectedItem);
                    }
                }
                IsItemSelected.Value = SelectedItems.Value.Count > 0;

                // trigger item deselected action
                if (ItemDeselected.HasEntries)
                {
                    ItemDeselected.Trigger(new ItemSelectionActionData { IsSelected = selected, ItemView = listItem, Item = listItem.Item.Value });
                }
            }
        }

        /// <summary>
        /// Called when the list of items has been changed.
        /// </summary>
        public virtual void ItemsChanged()
        {
            if (ListItemTemplates.Count <= 0)
                return; // static list 
                        
            Rebuild();
            LayoutsChanged();
        }

        /// <summary>
        /// Called when the list of items has been changed.
        /// </summary>
        private void OnListChanged(object sender, ListChangedEventArgs e)
        {
            bool layoutChanged = false;

            // update list of items
            if (e.ListChangeAction == ListChangeAction.Clear)
            {
                Clear();
                layoutChanged = true;
            }
            else if (e.ListChangeAction == ListChangeAction.Add)
            {
                AddRange(e.StartIndex, e.EndIndex);
                layoutChanged = true;
            }
            else if (e.ListChangeAction == ListChangeAction.Remove)
            {
                RemoveRange(e.StartIndex, e.EndIndex);
                layoutChanged = true;
            }
            else if (e.ListChangeAction == ListChangeAction.Modify)
            {
                ItemsModified(e.StartIndex, e.EndIndex, e.FieldPath);
            }
            else if (e.ListChangeAction == ListChangeAction.Select)
            {
                SelectItem(e.StartIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.Replace)
            {
                ItemsReplaced(e.StartIndex, e.EndIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.ScrollTo)
            {
                ScrollTo(e.StartIndex, e.Alignment, e.Offset);
            }
            else if (e.ListChangeAction == ListChangeAction.Move)
            {
            }

            if (ListChanged.HasEntries)
            {
                ListChanged.Trigger(new ListChangedActionData { ListChangeAction = e.ListChangeAction, StartIndex = e.StartIndex, EndIndex = e.EndIndex, FieldPath = e.FieldPath });
            }

            // update sort index
            UpdateSortIndex();

            if (layoutChanged)
            {
                if (ListPanel != null)
                {
                    ListPanel.ScrollRect.UpdateNormalizedPosition.Value = true; // set to retain scroll position as content updates
                }

                LayoutsChanged();
            }
        }

        /// <summary>
        /// Scrolls to item at index.
        /// </summary>
        private void ScrollTo(int index, ElementAlignment? alignment, ElementMargin offset)
        {
            if (ListPanel == null)
                return;

            if (index >= _presentedListItems.Count || index < 0)
                return;

            if (offset == null)
            {
                offset = new ElementMargin();
            }

            bool verticalScrollDirection = Overflow.Value == OverflowMode.Overflow && Orientation.Value == ElementOrientation.Vertical ||
                Overflow.Value == OverflowMode.Wrap && Orientation.Value == ElementOrientation.Horizontal;

            if (verticalScrollDirection)
            {
                // set vertical scroll distance
                float viewportHeight = ListPanel.ScrollRect.ActualHeight;
                float scrollRegionHeight = ScrollContent.ActualHeight;
                float scrollHeight = scrollRegionHeight - viewportHeight;
                if (scrollHeight <= 0)
                {
                    return;
                }

                // calculate the scroll position based on alignment and offset
                float itemPosition = _presentedListItems[index].OffsetFromParent.Value.Top.Pixels;
                float itemHeight = _presentedListItems[index].Height.Value.Pixels;

                if (alignment == null || alignment.Value.HasFlag(ElementAlignment.Bottom))
                {
                    // scroll so item is at bottom of viewport
                    float scrollOffset = itemPosition - (viewportHeight - itemHeight) + offset.Top.Pixels + offset.Bottom.Pixels;
                    VerticalNormalizedPosition.Value = (1 - scrollOffset / scrollHeight).Clamp(0, 1);
                }
                else if (alignment.Value.HasFlag(ElementAlignment.Left) || alignment.Value.HasFlag(ElementAlignment.Right) ||
                    alignment.Value == ElementAlignment.Center)
                {
                    // scroll so item is at center of viewport
                    float scrollOffset = itemPosition - viewportHeight / 2 + itemHeight / 2 + offset.Top.Pixels + offset.Bottom.Pixels;
                    VerticalNormalizedPosition.Value = (1 - scrollOffset / scrollHeight).Clamp(0, 1);
                }
                else
                {
                    // scroll so item is at top of viewport
                    float scrollOffset = itemPosition + offset.Top.Pixels + offset.Bottom.Pixels;
                    VerticalNormalizedPosition.Value = (1 - scrollOffset / scrollHeight).Clamp(0, 1);
                }
            }
            else
            {
                // set horizontal scroll distance
                float viewportWidth = ListPanel.ScrollRect.ActualWidth;
                float scrollRegionWidth = ScrollContent.ActualWidth;
                float scrollWidth = scrollRegionWidth - viewportWidth;
                if (scrollWidth <= 0)
                {
                    return;
                }

                // calculate the scroll position based on alignment and offset
                float itemPosition = _presentedListItems[index].OffsetFromParent.Value.Left.Pixels;
                float itemWidth = _presentedListItems[index].Width.Value.Pixels;

                if (alignment == null || alignment.Value.HasFlag(ElementAlignment.Right))
                {
                    // scroll so item is the right side of viewport
                    float scrollOffset = itemPosition - (viewportWidth - itemWidth) + offset.Left.Pixels + offset.Right.Pixels;
                    HorizontalNormalizedPosition.Value = (scrollOffset / scrollWidth).Clamp(0, 1);
                }
                else if (alignment.Value.HasFlag(ElementAlignment.Top) || alignment.Value.HasFlag(ElementAlignment.Bottom) ||
                    alignment.Value == ElementAlignment.Center)
                {
                    // scroll so item is at center of viewport
                    float scrollOffset = itemPosition - viewportWidth / 2 + itemWidth / 2 + offset.Left.Pixels + offset.Right.Pixels;
                    HorizontalNormalizedPosition.Value = (scrollOffset / scrollWidth).Clamp(0, 1);
                }
                else
                {
                    // scroll so item is at left side of viewport
                    float scrollOffset = itemPosition + offset.Left.Pixels + offset.Right.Pixels;
                    HorizontalNormalizedPosition.Value = (scrollOffset / scrollWidth).Clamp(0, 1);
                }
            }
        }

        /// <summary>
        /// Updates the sort index on the list items.
        /// </summary>
        public void UpdateSortIndex()
        {
            int index = 0;

            _presentedListItems.ForEach(x =>
            {
                if (!x.IsLive)
                    return;

                int itemIndex = Items.Value != null ? Items.Value.GetIndex(x.Item.Value) : index;
                x.SortIndex.DirectValue = itemIndex;
                x.IsAlternate.Value = AlternateItems.Value && Utils.IsOdd(itemIndex);
                ++index;
            });
        }

        /// <summary>
        /// Rebuilds the entire list.
        /// </summary>
        public void Rebuild()
        {
            // assume a completely new list has been set
            if (_oldItems != null)
            {
                // unsubscribe from change events in the old list
                _oldItems.ListChanged -= OnListChanged;
            }
            _oldItems = Items.Value;

            // clear list
            Clear();

            // add new list
            if (Items.Value != null)
            {
                // subscribe to change events in the new list
                Items.Value.ListChanged += OnListChanged;

                // add list items
                if (Items.Value.Count > 0)
                {
                    AddRange(0, Items.Value.Count - 1);
                }
            }

            // update sort index
            UpdateSortIndex();
        }

        /// <summary>
        /// Clears the list items.
        /// </summary>
        public void Clear()
        {
            foreach (var presentedItem in _presentedListItems)
            {
                DestroyListItem(presentedItem);
            }

            _presentedListItems.Clear();
        }

        /// <summary>
        /// Adds a range of list items.
        /// </summary>
        private void AddRange(int startIndex, int endIndex)
        {
            if (Items == null)
                return;

            // make sure we have a template
            if (ListItemTemplates.Count <= 0)
            {
                Utils.LogError("[MarkLight] {0}: Unable to generate list from items. Template missing. Add a template by adding a view with IsTemplate=\"True\" to the list.", GameObjectName);
                return;
            }

            // validate input
            int lastIndex = Items.Value.Count - 1;
            int insertCount = (endIndex - startIndex) + 1;
            bool listMatch = _presentedListItems.Count == (Items.Value.Count - insertCount);
            if (startIndex < 0 || startIndex > lastIndex ||
                endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Utils.LogWarning("[MarkLight] {0}: List mismatch. Rebuilding list.", GameObjectName);
                Rebuild();
                return;
            }

            // insert items
            //Utils.StartTimer();
            for (int i = startIndex; i <= endIndex; ++i)
            {
                CreateListItem(i);
            }
            //Utils.LogTimer();
        }

        /// <summary>
        /// Removes a range of list items.
        /// </summary>
        private void RemoveRange(int startIndex, int endIndex)
        {
            // validate input
            int lastIndex = _presentedListItems.Count - 1;
            int removeCount = (endIndex - startIndex) + 1;
            bool listMatch = _presentedListItems.Count == (Items.Value.Count + removeCount);
            if (startIndex < 0 || startIndex > lastIndex ||
                endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Utils.LogWarning("[MarkLight] {0}: List mismatch. Rebuilding list.", GameObjectName);
                Rebuild();
                return;
            }

            // remove items
            for (int i = endIndex; i >= startIndex; --i)
            {
                DestroyListItem(i);
            }
        }

        /// <summary>
        /// Called when item data in the list have been modified.
        /// </summary>
        private void ItemsModified(int startIndex, int endIndex, string fieldPath = "")
        {
            // validate input
            int lastIndex = _presentedListItems.Count - 1;
            bool listMatch = _presentedListItems.Count == Items.Value.Count;
            if (startIndex < 0 || startIndex > lastIndex || endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Utils.LogWarning("[MarkLight] {0}: List mismatch. Rebuilding list.", GameObjectName);
                Rebuild();
                return;
            }

            // notify observers that item has changed
            for (int i = startIndex; i <= endIndex; ++i)
            {
                ItemModified(i, fieldPath);
            }
        }

        /// <summary>
        /// Called when item data in list has been modified.
        /// </summary>
        private void ItemModified(int index, string fieldPath = "")
        {
            object itemData = Items.Value[index];
            var listItem = _presentedListItems[index];
            var path = String.IsNullOrEmpty(fieldPath) ? "Item" : "Item." + fieldPath;

            listItem.ForThisAndEachChild<UIView>(x =>
            {
                // TODO can be made faster if a HasItemBinding flag is implemented, also we can stop traversing the tree if another item is set
                if (x.Item.Value == itemData)
                {
                    x.NotifyDependentValueObservers(path, true);
                }
            });
        }

        /// <summary>
        /// Called when item data in the list have been replaced.
        /// </summary>
        private void ItemsReplaced(int startIndex, int endIndex)
        {
            // validate input
            int lastIndex = _presentedListItems.Count - 1;
            bool listMatch = _presentedListItems.Count == Items.Value.Count;
            if (startIndex < 0 || startIndex > lastIndex || endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Utils.LogWarning("[MarkLight] {0}: List mismatch. Rebuilding list.", GameObjectName);
                Rebuild();
                return;
            }

            // replace items
            for (int i = startIndex; i <= endIndex; ++i)
            {
                ItemReplaced(i);
            }
        }

        /// <summary>
        /// Called when item data in list has been replaced.
        /// </summary>
        private void ItemReplaced(int index)
        {
            object newItemData = Items.Value[index];
            var listItem = _presentedListItems[index];
            var oldItemData = listItem.Item.Value;

            listItem.ForThisAndEachChild<UIView>(x =>
            {
                // TODO can be made faster if a HasItemBinding flag is implemented, also we can stop traversing the tree if another item is set
                if (x.Item.Value == oldItemData)
                {
                    x.Item.Value = newItemData;
                    x.NotifyDependentValueObservers("Item", true);
                }
            });
        }

        /// <summary>
        /// Creates and initializes a new list item.
        /// </summary>
        private ListItem CreateListItem(int index)
        {
            object itemData = Items.Value[index];
            var template = GetListItemTemplate(itemData);

            View content = UseVirtualization ? _virtualizedItems.VirtualizedItemsContainer : Content;           
            var newItemView = content.CreateView(GetListItemTemplate(itemData), -1, _viewPools.Get(template));
            newItemView.Template = template;
            _presentedListItems.Insert(index, newItemView);

            // set item data
            newItemView.ForThisAndEachChild<UIView>(x =>
            {
                if (x.FindParent<List>() == this)
                {
                    x.Item.Value = itemData;
                }
            });
            newItemView.Activate();

            // initialize view
            newItemView.InitializeViews();
            return newItemView;
        }

        /// <summary>
        /// Gets template based on item data.
        /// </summary>
        private ListItem GetListItemTemplate(object itemData)
        {
            if (ListItemTemplates.Count <= 0)
            {
                Utils.LogError("[MarkLight] {0}: Unable to generate list from items. Template missing. Add a template by adding a view with IsTemplate=\"True\" to the list.", GameObjectName);
                return null;
            }

            if (ListItemTemplates.Count == 1 || itemData == null)
            {
                return ListItemTemplates[0];
            }

            // get method GetTemplateId from list item
            Type type = itemData.GetType();
            var method = type.GetMethod("GetTemplateId", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            if (method == null)
            {
                return ListItemTemplates[0];
            }

            string templateId = method.IsStatic ? method.Invoke(null, null) as string : method.Invoke(itemData, null) as string;
            return ListItemTemplates.FirstOrDefault(x => String.Equals(x.Id, templateId, StringComparison.OrdinalIgnoreCase)) ?? ListItemTemplates[0];
        }

        /// <summary>
        /// Destroys a list item.
        /// </summary>
        private void DestroyListItem(int index)
        {
            var itemView = _presentedListItems[index];
            DestroyListItem(itemView);
            _presentedListItems.RemoveAt(index);
        }

        /// <summary>
        /// Destroys a list item.
        /// </summary>
        private void DestroyListItem(ListItem presentedItem)
        {
            // deselect the item first
            SetSelected(presentedItem, false);

            var viewPool = presentedItem.Template != null ? _viewPools.Get(presentedItem.Template) : null;
            presentedItem.Destroy(viewPool);
        }

        /// <summary>
        /// Creates a container for virtualized items which will be presented on demand. Used to improve performance.
        /// </summary>
        public VirtualizedItems GetVirtualizedItems()
        {
            if (LayoutRoot == null)
                return null;

            // does a virtualized items container exist for this view?
            var virtualizedItemsContainer = LayoutRoot.Find<VirtualizedItemsContainer>(x => x.Owner == this, false);
            if (virtualizedItemsContainer == null)
            {
                // no. create a new one 
                virtualizedItemsContainer = LayoutRoot.CreateView<VirtualizedItemsContainer>();
                virtualizedItemsContainer.IsActive.DirectValue = false;
                virtualizedItemsContainer.Id = GameObjectName;
                virtualizedItemsContainer.Owner = this;
                virtualizedItemsContainer.HideFlags.Value = UnityEngine.HideFlags.HideAndDontSave;
                virtualizedItemsContainer.InitializeViews();
            }

            return new VirtualizedItems(virtualizedItemsContainer);
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            _updateWidth = Width.Value.Unit == ElementSizeUnit.Percents;
            _updateHeight = Height.Value.Unit == ElementSizeUnit.Percents;
            SelectedItems.DirectValue = new GenericObservableList();

            // remove panel if not used
            if (ListPanel != null && !IsScrollable)
            {
                Content = ListMask != null ? ListMask.Content : this;
                ListPanel.DestroyAndMoveContent(Content);
                ScrollContent.DestroyAndMoveContent(Content);
                ListPanel = null;
                ScrollContent = null;
            }

            // remove list mask if not used
            if (ListMask != null && !UseListMask)
            {
                if (Content == ListMask.Content)
                {
                    Content = this;
                }

                ListMask.DestroyAndMoveContent(this);
                ListMask = null;
            }

            _presentedListItems = new List<ListItem>();
            if (ListItemTemplates.Count > 0)
            {
                ListItemTemplates.ForEach(x => x.Deactivate());
            }

            // set up virtualization
            UseVirtualization.DirectValue = InitializeVirtualization();            
            UpdatePresentedListItems();

            if (ListItemTemplates.Count > 0)
            {
                //  get view pools for item templates
                _viewPools = new Dictionary<View, ViewPool>();
                foreach (var template in ListItemTemplates)
                {
                    // should pooling be used for this template?
                    if (!PoolSize.IsSet && !template.PoolSize.IsSet)
                        continue; // no.

                    int poolSize = template.PoolSize.IsSet ? template.PoolSize : PoolSize;
                    int maxPoolSize = template.MaxPoolSize.IsSet ? template.MaxPoolSize : MaxPoolSize;

                    var viewPool = LayoutRoot.GetViewPool(template.GameObjectName, template, poolSize, maxPoolSize);
                    _viewPools.Add(template, viewPool);
                }
            }
        }

        /// <summary>
        /// Called once at initialization to set the list up for virtualization.
        /// </summary>
        private bool InitializeVirtualization()
        {
            if (!UseVirtualization)
                return false;

            // verify things are correctly set up for virtualization
            if (Overflow.Value == OverflowMode.Wrap || IsScrollable == false)
            {
                Utils.LogWarning("[MarkLight] {0}: Can't virtualize list because IsScrollable is false or Overflow is set to Wrap.", GameObjectName);
                return false;
            }
            
            if (DisableItemArrangement)
            {
                Utils.LogWarning("[MarkLight] {0}: Can't virtualize list because DisableItemArrangement is set to True.", GameObjectName);
                return false;
            }

            // check if templates are set and that they have the same height/width
            if (ListItemTemplates.Count <= 0)
            {
                Utils.LogWarning("[MarkLight] {0}: Can't virtualize list because no item template found. Only dynamic lists can be virtualized.", GameObjectName);
                return false;
            }

            // get virtualized items container
            _virtualizedItems = GetVirtualizedItems();
            _virtualizedItems.Orientation = Orientation.Value;
            return true;
        }

        /// <summary>
        /// Updates list of presented list items. Needs to be called after list items are added manually to the list.
        /// </summary>
        public void UpdatePresentedListItems()
        {
            _presentedListItems.Clear();
            _presentedListItems.AddRange(GetActiveListItems());
            UpdateSortIndex();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns list item template.
        /// </summary>
        public List<ListItem> ListItemTemplates
        {
            get
            {
                if (_listItemTemplates == null)
                {
                    _listItemTemplates = Content.GetChildren<ListItem>(x => x.IsTemplate, false);
                }

                return _listItemTemplates;
            }
        }

        /// <summary>
        /// Returns list of presented list items.
        /// </summary>
        public List<ListItem> PresentedListItems
        {
            get
            {
                return _presentedListItems;
            }
        }

        #endregion
    }
}
