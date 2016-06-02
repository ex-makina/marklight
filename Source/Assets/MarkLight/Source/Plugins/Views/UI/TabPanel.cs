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
    /// TabPanel view.
    /// </summary>
    /// <d>Arranges content in a series of tabs that can be switched between. Tabs can be oriented horizontallt/vertically and aligned topleft/bottom/etc. Tabs can be generated from a template and bound list data.</d>
    [HideInPresenter]
    public class TabPanel : UIView
    {
        #region Fields

        #region TabContent

        /// <summary>
        /// Tab content image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the tab content.</d>
        [MapTo("TabContent.BackgroundImage")]
        public _Sprite TabContentImage;

        /// <summary>
        /// Tab content image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the tab content.</d>
        [MapTo("TabContent.BackgroundImageType")]
        public _ImageType TabContentImageType;

        /// <summary>
        /// Tab content image material.
        /// </summary>
        /// <d>The material of the tab content image.</d>
        [MapTo("TabContent.BackgroundMaterial")]
        public _Material TabContentMaterial;

        /// <summary>
        /// Tab content image color.
        /// </summary>
        /// <d>The color of the tab content image.</d>
        [MapTo("TabContent.BackgroundColor")]
        public _Color TabContentColor;

        /// <summary>
        /// Tab content image width.
        /// </summary>
        /// <d>Specifies the width of the tab content image either in pixels or percents.</d>
        [MapTo("TabContent.Width")]
        public _ElementSize TabContentWidth;

        /// <summary>
        /// Tab content image height.
        /// </summary>
        /// <d>Specifies the height of the tab content image either in pixels or percents.</d>
        [MapTo("TabContent.Height")]
        public _ElementSize TabContentHeight;

        /// <summary>
        /// Tab content image offset.
        /// </summary>
        /// <d>Specifies the offset of the tab content image.</d>
        [MapTo("TabContent.Offset")]
        public _ElementSize TabContentOffset;

        /// <summary>
        /// Tab content image offset.
        /// </summary>
        /// <d>Specifies the offset of the tab content image.</d>
        [MapTo("TabContent.Margin")]
        public _ElementMargin TabContentMargin;

        /// <summary>
        /// Tab content alignment.
        /// </summary>
        /// <d>Specifies the alignment of the tab content.</d>
        [MapTo("TabContent.Alignment")]
        public _ElementAlignment TabContentAlignment;

        /// <summary>
        /// Tab content region.
        /// </summary>
        /// <d>Region that contains the tab content.</d>
        public Region TabContent;

        #endregion

        #region TabHeaderList

        /// <summary>
        /// Tab header list image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the tab header list.</d>
        [MapTo("TabHeaderList.BackgroundImage")]
        public _Sprite TabListImage;

        /// <summary>
        /// Tab header list image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the tab header list.</d>
        [MapTo("TabHeaderList.BackgroundImageType")]
        public _ImageType TabListImageType;

        /// <summary>
        /// Tab header list image material.
        /// </summary>
        /// <d>The material of the tab header list image.</d>
        [MapTo("TabHeaderList.BackgroundMaterial")]
        public _Material TabListMaterial;

        /// <summary>
        /// Tab header list image color.
        /// </summary>
        /// <d>The color of the tab header list image.</d>
        [MapTo("TabHeaderList.BackgroundColor")]
        public _Color TabListColor;

        /// <summary>
        /// Tab header list image width.
        /// </summary>
        /// <d>Specifies the width of the tab header list image either in pixels or percents.</d>
        [MapTo("TabHeaderList.Width")]
        public _ElementSize TabListWidth;

        /// <summary>
        /// Tab header list image height.
        /// </summary>
        /// <d>Specifies the height of the tab header list image either in pixels or percents.</d>
        [MapTo("TabHeaderList.Height")]
        public _ElementSize TabListHeight;

        /// <summary>
        /// Tab header list image offset.
        /// </summary>
        /// <d>Specifies the offset of the tab header list image.</d>
        [MapTo("TabHeaderList.Offset")]
        public _ElementSize TabListOffset;

        /// <summary>
        /// Tab header list image offset.
        /// </summary>
        /// <d>Specifies the offset of the tab header list image.</d>
        [MapTo("TabHeaderList.Margin")]
        public _ElementMargin TabListMargin;

        /// <summary>
        /// Tab header list alignment.
        /// </summary>
        /// <d>Specifies the alignment of the tab header list.</d>
        [MapTo("TabHeaderList.Alignment", "TabHeaderListOrientationChanged")]
        public _ElementAlignment TabListAlignment;

        /// <summary>
        /// Tab header list orientation.
        /// </summary>
        /// <d>Specifies the orientation of the tab header list.</d>
        [MapTo("TabHeaderList.Orientation", "TabHeaderListOrientationChanged")]
        public _ElementOrientation TabListOrientation;

        /// <summary>
        /// Spacing between tab header list items.
        /// </summary>
        /// <d>The spacing between tab header list items.</d>
        [MapTo("TabHeaderList.Spacing")]
        public _ElementSize TabListSpacing;

        /// <summary>
        /// The alignment of tab header list items.
        /// </summary>
        /// <d>If the tab header list items varies in size the content alignment specifies how the tab header list items should be arranged in relation to each other.</d>
        [MapTo("TabHeaderList.ContentAlignment")]
        public _ElementAlignment TabListContentAlignment;

        /// <summary>
        /// Tab header list content margin.
        /// </summary>
        /// <d>Sets the margin of the tab header list mask view that contains the contents of the tab header list.</d>
        [MapTo("TabHeaderList.ContentMargin")]
        public _ElementMargin TabListContentMargin;

        /// <summary>
        /// Sort direction.
        /// </summary>
        /// <d>If tab header list items has SortIndex set they can be sorted in the direction specified.</d>
        [MapTo("TabHeaderList.SortDirection")]
        public _ElementSortDirection TabListSortDirection;

        #region TabListMask

        /// <summary>
        /// Indicates if a list mask is to be used.
        /// </summary>
        /// <d>Boolean indicating if a list mask is to be used.</d>
        [MapTo("TabHeaderList.UseListMask")]
        public _bool TabListUseListMask;

        /// <summary>
        /// The width of the list mask image.
        /// </summary>
        /// <d>Specifies the width of the list mask image either in pixels or percents.</d>
        [MapTo("TabHeaderList.ListMaskWidth")]
        public _ElementSize TabListMaskWidth;

        /// <summary>
        /// The height of the list mask image.
        /// </summary>
        /// <d>Specifies the height of the list mask image either in pixels or percents.</d>
        [MapTo("TabHeaderList.ListMaskHeight")]
        public _ElementSize TabListMaskHeight;

        /// <summary>
        /// The offset of the list mask image.
        /// </summary>
        /// <d>Specifies the offset of the list mask image.</d>
        [MapTo("TabHeaderList.ListMaskOffset")]
        public _ElementMargin TabListMaskOffset;

        /// <summary>
        /// Tab header list mask image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered as the list max.</d>
        [MapTo("TabHeaderList.ListMaskImage")]
        public _Sprite TabListMaskImage;

        /// <summary>
        /// Tab header list mask image type.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered as the list max.</d>
        [MapTo("TabHeaderList.ListMaskImageType")]
        public _ImageType TabListMaskImageType;

        /// <summary>
        /// Tab header list mask image material.
        /// </summary>
        /// <d>The material of the list max image.</d>
        [MapTo("TabHeaderList.ListMaskMaterial")]
        public _Material TabListMaskMaterial;

        /// <summary>
        /// Tab header list mask image color.
        /// </summary>
        /// <d>The color of the list max image.</d>
        [MapTo("TabHeaderList.ListMaskColor")]
        public _Color TabListMaskColor;

        /// <summary>
        /// Tab header list mask alignment.
        /// </summary>
        /// <d>Specifies the alignment of the list mask.</d>
        [MapTo("TabHeaderList.ListMaskAlignment")]
        public _ElementAlignment TabListMaskAlignment;

        /// <summary>
        /// Indicates if list mask should be rendered.
        /// </summary>
        /// <d>Indicates if the list mask, i.e. the list mask background image sprite and color should be rendered.</d>
        [MapTo("TabHeaderList.ListMaskShowGraphic")]
        public _bool TabListMaskShowGraphic;

        #endregion

        /// <summary>
        /// Tab header list.
        /// </summary>
        /// <d>Presents a selectable list of tab headers that switches the tab content when clicked.</d>
        public List TabHeaderList;

        #endregion

        /// <summary>
        /// User-defined list data.
        /// </summary>
        /// <d>Can be bound to an generic ObservableList to dynamically generate tab content and headers based on a template.</d>
        [ChangeHandler("ItemsChanged")]
        public IObservableList Items;

        /// <summary>
        /// Indicates if tabs can be selected by the user.
        /// </summary>
        /// <d>Boolean indicating if tabs in the tab panel can be selected by the user.</d>
        public _bool CanSelect;

        /// <summary>
        /// Indicates if tab content margin should be automatically adjusted.
        /// </summary>
        /// <d>Boolean indicating if the tab panel content margin should automatically be adjusted to make room for the tab headers.</d>
        public _bool AutoAdjustContentMargin;

        /// <summary>
        /// Indicates if tab list content should be automatically adjusted.
        /// </summary>
        /// <d>Boolean indicating if tab list content alignment should automatically be adjusted to the tab list orientation/alignment. E.g. if tab list is oriented horizontally to the top-left then the content alignment of the list is set to bottom so the tab headers hug the top border of the tab panel content region.</d>
        public _bool AutoAdjustTabListContentAlignment;
        
        /// <summary>
        /// Selected data list item.
        /// </summary>
        /// <d>Set when the selected list item changes and points to the user-defined data item.</d>
        [ChangeHandler("SelectedItemChanged")]
        public _object SelectedItem;

        /// <summary>
        /// Tab selected view action.
        /// </summary>
        /// <d>Triggered when a tab is selected either by user interaction or programmatically.</d>
        /// <actionData>TabSelectionActionData</actionData>
        public ViewAction TabSelected;

        /// <summary>
        /// List changed view action.
        /// </summary>
        /// <d>Triggered when the list changes (items added, removed or moved).</d>
        /// <actionData>ListChangedActionData</actionData>
        public ViewAction ListChanged;

        public Tab SelectedTab;
        public ViewSwitcher TabSwitcher;

        [NotSetFromXuml]
        public bool StaticTabsGenerated;

        private IObservableList _oldItems;
        private List<Tab> _presentedTabs;
        private Tab _tabItemTemplate;
        private object _selectedItem;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            CanSelect.DirectValue = true;
            AutoAdjustContentMargin.DirectValue = true;
            AutoAdjustTabListContentAlignment.DirectValue = true;
            StaticTabsGenerated = false;

            TabHeaderList.Orientation.DirectValue = ElementOrientation.Horizontal;
            TabHeaderList.Alignment.DirectValue = ElementAlignment.TopLeft;
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
        /// Updates the layout of the view.
        /// </summary>
        public override void LayoutChanged()
        {
            // set content margins based on tab list size and its orientation
            var contentMargin = new ElementMargin();
            var tabAlignment = ElementAlignment.Center;
            if (TabHeaderList.Orientation == ElementOrientation.Horizontal)
            {
                if (TabHeaderList.Alignment.Value.HasFlag(ElementAlignment.Bottom))
                {
                    contentMargin.Bottom = ElementSize.FromPixels(TabHeaderList.Height.Value.Pixels);
                    tabAlignment = ElementAlignment.Top;
                }
                else
                {                    
                    contentMargin.Top = ElementSize.FromPixels(TabHeaderList.Height.Value.Pixels);
                    tabAlignment = ElementAlignment.Bottom;
                }
            }
            else
            {
                if (TabHeaderList.Alignment.Value.HasFlag(ElementAlignment.Right))
                {
                    contentMargin.Right = ElementSize.FromPixels(TabHeaderList.Width.Value.Pixels);
                    tabAlignment = ElementAlignment.Left;
                }
                else
                {
                    contentMargin.Left = ElementSize.FromPixels(TabHeaderList.Width.Value.Pixels);
                    tabAlignment = ElementAlignment.Right;
                }
            }

            if (AutoAdjustContentMargin)
            {
                TabContent.Margin.Value = contentMargin;
            }

            if (AutoAdjustTabListContentAlignment)
            {
                TabHeaderList.ContentAlignment.Value = tabAlignment;
            }

            base.LayoutChanged();
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            _presentedTabs = new List<Tab>();
            if (TabItemTemplate != null)
            {
                TabItemTemplate.Deactivate();
            }

            _presentedTabs.AddRange(TabSwitcher.GetChildren<Tab>(x => !x.IsTemplate && !x.IsDestroyed, false));
            if (TabItemTemplate == null)
            {               
                // initialize (static) tabs
                if (_presentedTabs.Count > 0 && !StaticTabsGenerated)
                {
                    StaticTabsGenerated = true;
                    TabHeaderList.Content.DestroyChildren();

                    // go through static items and initialize tab headers
                    foreach (var tab in _presentedTabs)
                    {
                        CreateTabHeader(tab);
                    }

                    // select first tab by default
                    SelectTab(0, false);
                }
            }
        }

        /// <summary>
        /// Switches to next tab.
        /// </summary>
        public void NextTab(bool animate = true, bool cycle = true)
        {
            var tabs = TabSwitcher.GetChildren<Tab>(false);
            int tabCount = tabs.Count;

            if (tabCount <= 0)
                return;

            int index = tabs.IndexOf(SelectedTab);
            ++index;

            if (index >= tabCount)
            {
                if (cycle)
                {
                    SelectTab(0, animate);
                }

                return;
            }

            SelectTab(index, animate);
        }

        /// <summary>
        /// Switches to previous tab.
        /// </summary>
        public void PreviousTab(bool animate = true, bool cycle = true)
        {
            var tabs = this.GetChildren<Tab>(false);
            int tabCount = tabs.Count;

            if (tabCount <= 0)
                return;

            int index = tabs.IndexOf(SelectedTab);
            --index;

            if (index < 0)
            {
                if (cycle)
                {
                    SelectTab(tabCount - 1, animate);
                }

                return;
            }

            SelectTab(index, animate);
        }

        /// <summary>
        /// Selects tab.
        /// </summary>
        public void SelectTab(Tab tab, bool animate = true, bool triggeredByClick = false)
        {
            if (tab == null || (triggeredByClick && !CanSelect) || tab.IsSelected)
                return;

            // select
            SetSelected(tab, true);

            // deselect other items if we can't multi-select
            foreach (var presentedTabItem in _presentedTabs)
            {
                if (presentedTabItem == tab)
                    continue;

                // deselect and trigger actions
                SetSelected(presentedTabItem, false);
            }

            // switch to tab
            TabSwitcher.SwitchTo(tab, animate);
        }

        /// <summary>
        /// Selects tab.
        /// </summary>
        public void SelectTab(int index, bool animate = true)
        {
            if (index >= _presentedTabs.Count)
            {
                Utils.LogError("[MarkLight] {0}: Unable to select tab. Index out of bounds.", GameObjectName);
                return;
            }

            SelectTab(_presentedTabs[index], animate, false);
        }

        /// <summary>
        /// Selects tab.
        /// </summary>
        public void SelectTab(object itemData, bool animate = true)
        {
            var tabItem = _presentedTabs.FirstOrDefault(x =>
            {
                var item = x as Tab;
                return item != null ? item.Item.Value == itemData : false;
            });

            if (tabItem == null)
            {
                Utils.LogError("[MarkLight] {0}: Unable to select tab. Item not found.", GameObjectName);
                return;
            }

            SelectTab(tabItem, animate, false);
        }

        /// <summary>
        /// Called when the selected item has been changed.
        /// </summary>
        public virtual void SelectedItemChanged()
        {
            if (_selectedItem == SelectedItem.Value)
            {
                return;
            }

            SelectTab(SelectedItem.Value);
        }

        /// <summary>
        /// Selects or deselects a tab.
        /// </summary>
        private void SetSelected(Tab tab, bool selected)
        {
            if (tab == null)
                return;

            tab.IsSelected.Value = selected;
            if (selected)
            {
                // select tab header if it's not selected
                var tabHeader = TabHeaderList.Content.Find<TabHeader>(x => x.ParentTab == tab, false);
                if (tabHeader != null && !tabHeader.IsSelected)
                {
                    TabHeaderList.SelectItem(tabHeader, false);
                }

                // item selected
                _selectedItem = tab.Item.Value;
                SelectedItem.Value = tab.Item.Value;
                SelectedTab = tab;
                if (Items != null)
                {
                    Items.SetSelected(_selectedItem);
                }

                // trigger item selected action
                if (TabSelected.HasEntries)
                {
                    TabSelected.Trigger(new TabSelectionActionData { IsSelected = true, TabView = tab, Item = tab.Item.Value });
                }
            }
        }

        /// <summary>
        /// Called when the list of items has been changed.
        /// </summary>
        public virtual void ItemsChanged()
        {
            if (TabItemTemplate == null)
                return; // static tabs
            
            Rebuild();
        }

        /// <summary>
        /// Called when the list of items has been changed.
        /// </summary>
        private void OnListChanged(object sender, ListChangedEventArgs e)
        {
            // update list of tabs
            if (e.ListChangeAction == ListChangeAction.Clear)
            {
                Clear();
            }
            else if (e.ListChangeAction == ListChangeAction.Add)
            {
                AddRange(e.StartIndex, e.EndIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.Remove)
            {
                RemoveRange(e.StartIndex, e.EndIndex);
            }
            else if (e.ListChangeAction == ListChangeAction.Select)
            {                
                SelectTab(e.StartIndex, true);
            }

            if (ListChanged.HasEntries)
            {
                ListChanged.Trigger(new ListChangedActionData { ListChangeAction = e.ListChangeAction, StartIndex = e.StartIndex, EndIndex = e.EndIndex });
            }

            LayoutsChanged();
        }

        /// <summary>
        /// Rebuilds the list of tabs.
        /// </summary>
        public void Rebuild()
        {
            // assume a completely new list has been set
            if (_oldItems != null)
            {
                // unsubscribe from change events in the old list
                _oldItems.ListChanged -= OnListChanged;
            }
            _oldItems = Items;

            // clear tab and header list
            Clear();

            // add new list
            if (Items != null)
            {
                // subscribe to change events in the new list
                Items.ListChanged += OnListChanged;

                // add list items
                if (Items.Count > 0)
                {
                    AddRange(0, Items.Count - 1);
                }

                // select first tab by default
                SelectTab(0, false);
            }
        }

        /// <summary>
        /// Clears the tabs.
        /// </summary>
        private void Clear()
        {
            foreach (var presentedItem in _presentedTabs)
            {
                DestroyTab(presentedItem);
            }
            _presentedTabs.Clear();

            TabHeaderList.Clear();
        }

        /// <summary>
        /// Adds a range of tabs.
        /// </summary>
        private void AddRange(int startIndex, int endIndex)
        {
            if (Items == null)
                return;

            // make sure we have a template
            if (TabItemTemplate == null)
            {
                Utils.LogError("[MarkLight] {0}: Unable to generate tabs from items. Template missing. Add a template by adding a Tab view with IsTemplate=\"True\" to the TabPanel.", GameObjectName);
                return;
            }

            // validate input
            int lastIndex = Items.Count - 1;
            int insertCount = (endIndex - startIndex) + 1;
            bool listMatch = _presentedTabs.Count == (Items.Count - insertCount);
            if (startIndex < 0 || startIndex > lastIndex ||
                endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Utils.LogWarning("[MarkLight] {0}: Tab list mismatch. Rebuilding tabs.", ViewTypeName);
                Rebuild();
                return;
            }

            // insert tabs
            for (int i = startIndex; i <= endIndex; ++i)
            {
                CreateTab(i);
            }
        }

        /// <summary>
        /// Removes a range of tabs.
        /// </summary>
        private void RemoveRange(int startIndex, int endIndex)
        {
            // validate input
            int lastIndex = _presentedTabs.Count - 1;
            int removeCount = (endIndex - startIndex) + 1;
            bool listMatch = _presentedTabs.Count == (Items.Count + removeCount);
            if (startIndex < 0 || startIndex > lastIndex ||
                endIndex < startIndex || endIndex > lastIndex || !listMatch)
            {
                Utils.LogWarning("[MarkLight] {0}: Tab list mismatch. Rebuilding tabs.", GameObjectName);
                Rebuild();
                return;
            }

            // remove tabs
            for (int i = endIndex; i >= startIndex; --i)
            {
                DestroyTab(i);
            }
        }

        /// <summary>
        /// Creates and initializes a new tab.
        /// </summary>
        private Tab CreateTab(int index)
        {
            object itemData = Items[index];
            var newTabView = TabSwitcher.CreateView(TabItemTemplate, index + 1);            

            _presentedTabs.Insert(index, newTabView);

            // set item data
            SetItemData(newTabView, itemData);

            // initialize view
            newTabView.InitializeViews();
            newTabView.Deactivate();

            // create tab header
            CreateTabHeader(newTabView, index, itemData);                       

            return newTabView;
        }

        /// <summary>
        /// Sets item data.
        /// </summary>
        private void SetItemData(View view, object itemData)
        {
            view.ForThisAndEachChild<UIView>(x =>
            {
                if (x.FindParent<TabPanel>() == this)
                {
                    x.Item.Value = itemData;
                }
            });
        }

        /// <summary>
        /// Destroys a tab.
        /// </summary>
        private void DestroyTab(int index)
        {
            var itemView = _presentedTabs[index];
            DestroyTab(itemView);
            _presentedTabs.RemoveAt(index);

            // select previous tab
            if (index > 0)
            {
                SelectTab(index - 1, false);
            }
            else if (_presentedTabs.Count > 0)
            {
                SelectTab(0, false);
            }
        }

        /// <summary>
        /// Destroys a tab.
        /// </summary>
        private void DestroyTab(Tab presentedItem)
        {
            // deselect the tab
            SetSelected(presentedItem, false);

            // destroy the tab header
            DestroyTabHeader(presentedItem);
                        
            presentedItem.Destroy();
        }

        /// <summary>
        /// Destroys a tab header.
        /// </summary>
        private void DestroyTabHeader(Tab presentedItem)
        {
            var tabHeader = TabHeaderList.Content.Find<TabHeader>(x => x.ParentTab == presentedItem);
            tabHeader.Destroy();
            TabHeaderList.UpdatePresentedListItems();
            TabHeaderList.QueueChangeHandler("LayoutChanged");
        }

        /// <summary>
        /// Creates tab header from tab.
        /// </summary>
        private void CreateTabHeader(Tab tab, int index = -1, object itemData = null)
        {
            // check if the tab has a custom tab header
            var tabHeader = tab.Find<TabHeader>(false);
            if (tabHeader == null)
            {
                // create default TabHeader                
                tabHeader = ViewData.CreateView<TabHeader>(TabHeaderList.Content, tab.Parent, null, Theme, String.Empty, Style);
                tabHeader.ParentTab = tab;
                if (index >= 0)
                {
                    tabHeader.transform.SetSiblingIndex(index + 1);
                }

                if (itemData != null)
                {
                    SetItemData(tabHeader, itemData);
                }

                // initialize tab header
                tabHeader.InitializeViews();
            }
            else
            {
                // move tab header to list
                tabHeader.MoveTo(TabHeaderList.Content, index >= 0 ? index + 1 : -1);
                TabHeaderList.QueueChangeHandler("LayoutChanged");
            }

            // make sure tab bindings are propagated to header
            tab.PropagateBindings();
            TabHeaderList.UpdatePresentedListItems();
        }

        /// <summary>
        /// Called when a tab header gets selected.
        /// </summary>
        public void TabHeaderSelected(ItemSelectionActionData actionData)
        {
            var tabHeader = actionData.ItemView as TabHeader;
            SelectTab(tabHeader.ParentTab, true, false);
        }

        /// <summary>
        /// Called when the alignment or orientation of the tab header list is changed.
        /// </summary>
        public virtual void TabHeaderListOrientationChanged()
        {
            string state = String.Format("{0}{1}", TabHeaderList.Orientation.Value.ToString(), TabHeaderList.Alignment.Value.ToString());            
            SetState(state);
            TabHeaderList.SetState(state);

            QueueChangeHandler("LayoutChanged");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns list item template.
        /// </summary>
        public Tab TabItemTemplate
        {
            get
            {
                if (_tabItemTemplate == null)
                {
                    _tabItemTemplate = TabSwitcher.Find<Tab>(x => x.IsTemplate, false);
                }

                return _tabItemTemplate;
            }
        }

        #endregion
    }
}
