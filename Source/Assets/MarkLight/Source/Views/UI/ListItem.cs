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
    /// List item view.
    /// </summary>
    /// <d>Displays a selectable list item. Has the states: Default, Disabled, Highlighted, Pressed and Selected.</d>
    [HideInPresenter]
    public class ListItem : UIView
    {
        #region Fields

        #region ItemLabel

        /// <summary>
        /// List item text.
        /// </summary>
        /// <d>The text of the list item label.</d>
        [MapTo("ItemLabel.Text", "TextChanged")]
        public _string Text;

        /// <summary>
        /// List item text font.
        /// </summary>
        /// <d>The font of the list item label.</d>
        [MapTo("ItemLabel.Font")]
        public _Font Font;

        /// <summary>
        /// List item text font size.
        /// </summary>
        /// <d>The font size of the list item label.</d>
        [MapTo("ItemLabel.FontSize")]
        public _int FontSize;

        /// <summary>
        /// List item text line spacing.
        /// </summary>
        /// <d>The line spacing of the list item label.</d>
        [MapTo("ItemLabel.LineSpacing")]
        public _int LineSpacing;

        /// <summary>
        /// Supports rich text.
        /// </summary>
        /// <d>Boolean indicating if the list item label supports rich text.</d>
        [MapTo("ItemLabel.SupportRichText")]
        public _bool SupportRichText;

        /// <summary>
        /// List item text font color.
        /// </summary>
        /// <d>The font color of the list item label.</d>
        [MapTo("ItemLabel.FontColor")]
        public _Color FontColor;

        /// <summary>
        /// List item text font style.
        /// </summary>
        /// <d>The font style of the list item label.</d>
        [MapTo("ItemLabel.FontStyle")]
        public _FontStyle FontStyle;

        /// <summary>
        /// List item text margin.
        /// </summary>
        /// <d>The margin of the list item label. Can be used to adjust the text positioning.</d>
        [MapTo("ItemLabel.Margin")]
        public _ElementMargin TextMargin;

        /// <summary>
        /// List item text alignment.
        /// </summary>
        /// <d>The alignment of the text inside the list item label. Can be used with TextMargin and TextOffset to get desired positioning of the text.</d>
        [MapTo("ItemLabel.TextAlignment")]
        public _ElementAlignment TextAlignment;

        /// <summary>
        /// List item text offset.
        /// </summary>
        /// <d>The offset of the list item label. Can be used with TextMargin and TextAlignment to get desired positioning of the text.</d>
        [MapTo("ItemLabel.Offset")]
        public _ElementMargin TextOffset;

        /// <summary>
        /// List item text shadow color.
        /// </summary>
        /// <d>The shadow color of the list item label.</d>
        [MapTo("ItemLabel.ShadowColor")]
        public _Color ShadowColor;

        /// <summary>
        /// List item text shadow distance.
        /// </summary>
        /// <d>The distance of the list item label shadow.</d>
        [MapTo("ItemLabel.ShadowDistance")]
        public _Vector2 ShadowDistance;

        /// <summary>
        /// List item text outline color.
        /// </summary>
        /// <d>The outline color of the list item label.</d>
        [MapTo("ItemLabel.OutlineColor")]
        public _Color OutlineColor;

        /// <summary>
        /// List item text outline distance.
        /// </summary>
        /// <d>The distance of the list item label outline.</d>
        [MapTo("ItemLabel.OutlineDistance")]
        public _Vector2 OutlineDistance;

        /// <summary>
        /// Adjusts the list item to the text.
        /// </summary>
        /// <d>An enum indiciating how the list item should adjust its size to the label text.</d>
        [MapTo("ItemLabel.AdjustToText")]
        public _AdjustToText AdjustToText;

        /// <summary>
        /// The list item label.
        /// </summary>
        /// <d>The list item label displays text next to the list item.</d>
        public Label ItemLabel;

        #endregion

        /// <summary>
        /// List item text padding.
        /// </summary>
        /// <d>Padding added to list item text when AdjustToText is set.</d>
        [ChangeHandler("TextChanged")]
        public _ElementMargin TextPadding;

        /// <summary>
        /// List item length.
        /// </summary>
        /// <d>Specifies the list item width.</d>
        [ChangeHandler("LayoutsChanged")]
        public _ElementSize Length;

        /// <summary>
        /// List item breadth.
        /// </summary>
        /// <d>Specifies the list item breadth.</d>
        [ChangeHandler("LayoutsChanged")]
        public _ElementSize Breadth;

        /// <summary>
        /// Indicates if item is selected on mouse down.
        /// </summary>
        /// <d>Boolean indicating if item is selected on mouse down (true by default). If set to false the item is selected on mouse up (click).</d>
        public _bool SelectOnMouseDown;

        [NotSetFromXml]
        [ChangeHandler("IsSelectedChanged", TriggerImmediately = true)]
        public _bool IsSelected;        

        [NotSetFromXml]
        public _bool IsPressed;

        [NotSetFromXml]
        public _bool IsMouseOver;

        /// <summary>
        /// List item click action.
        /// </summary>
        /// <d>The list item click action is triggered when the user clicks on the list item.</d>
        public ViewAction Click;

        /// <summary>
        /// List item mouse enter action.
        /// </summary>
        /// <d>The list item mouse enter action is triggered when the mouse enters the list item.</d>
        public ViewAction MouseEnter;

        /// <summary>
        /// List item mouse exit action.
        /// </summary>
        /// <d>The list item mouse exit action is triggered when the mouse exits the list item.</d>
        public ViewAction MouseExit;

        /// <summary>
        /// List item mouse down action.
        /// </summary>
        /// <d>The list item mouse down action is triggered when the mouse is pressed over the list item.</d>
        public ViewAction MouseDown;

        /// <summary>
        /// List item mouse down action.
        /// </summary>
        /// <d>The list item mouse up action is triggered when the mouse is pressed and then released over the list item.</d>
        public ViewAction MouseUp;

        private List _parentList;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            Breadth.DirectValue = new ElementSize(40);
            Length.DirectValue = new ElementSize(120);
            SelectOnMouseDown.DirectValue = true;
            TextPadding.DirectValue = new ElementMargin();

            // list item label
            if (ItemLabel != null)
            {
                ItemLabel.TextAlignment.DirectValue = ElementAlignment.Center;
                ItemLabel.Width.DirectValue = ElementSize.FromPercents(1);
                ItemLabel.Height.DirectValue = ElementSize.FromPercents(1);
            }
        }

        /// <summary>
        /// Called when the layout of the view has been changed. 
        /// </summary>
        public override void LayoutChanged()
        {
            // adjust width and height to ParentList
            if (ParentList == null || ParentList.Orientation == ElementOrientation.Horizontal)
            {
                Width.DirectValue = IsSet(() => Width) && Width.Value.Unit != ElementSizeUnit.Percents ? Width.Value : new ElementSize(Length.Value);

                if (!IsSet(() => Height))
                {
                    Height.DirectValue = IsSet(() => Breadth) ? new ElementSize(Breadth.Value) : ElementSize.FromPercents(1);
                }                
            }
            else
            {
                // if neither width nor length is set, use 100% width                
                if (!IsSet(() => Width))
                {
                    Width.DirectValue = IsSet(() => Length) ? new ElementSize(Length.Value) : ElementSize.FromPercents(1);
                }

                Height.DirectValue = IsSet(() => Height) && Height.Value.Unit != ElementSizeUnit.Percents ? Height.Value : new ElementSize(Breadth.Value);
            }

            base.LayoutChanged();
        }

        /// <summary>
        /// Called when the item text has been changed.
        /// </summary>
        public virtual void TextChanged()
        {
            if (ItemLabel == null || ItemLabel.AdjustToText.Value == MarkLight.AdjustToText.None)
                return;

            // adjust item size to text
            if (ItemLabel.AdjustToText.Value == MarkLight.AdjustToText.Width)
            {
                Width.Value = new ElementSize(ItemLabel.TextComponent.preferredWidth + TextPadding.Value.Left.Pixels + TextPadding.Value.Right.Pixels
                    + ItemLabel.Margin.Value.Left.Pixels + ItemLabel.Margin.Value.Right.Pixels);
            }
            else if (ItemLabel.AdjustToText.Value == MarkLight.AdjustToText.Height)
            {
                Height.Value = new ElementSize(ItemLabel.TextComponent.preferredHeight + TextPadding.Value.Top.Pixels + TextPadding.Value.Bottom.Pixels
                    + ItemLabel.Margin.Value.Top.Pixels + ItemLabel.Margin.Value.Bottom.Pixels);
            }
            else if (ItemLabel.AdjustToText.Value == MarkLight.AdjustToText.WidthAndHeight)
            {
                Width.Value = new ElementSize(ItemLabel.TextComponent.preferredWidth + TextPadding.Value.Left.Pixels + TextPadding.Value.Right.Pixels
                    + ItemLabel.Margin.Value.Left.Pixels + ItemLabel.Margin.Value.Right.Pixels);
                Height.Value = new ElementSize(ItemLabel.TextComponent.preferredHeight + TextPadding.Value.Top.Pixels + TextPadding.Value.Bottom.Pixels
                    + ItemLabel.Margin.Value.Top.Pixels + ItemLabel.Margin.Value.Bottom.Pixels);
            }

            LayoutsChanged();
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// Called when mouse is clicked.
        /// </summary>
        public void ListItemMouseClick()
        {
            if (ParentList == null || State == "Disabled")
                return;

            if (SelectOnMouseDown.Value)
                return;

            ParentList.SelectItem(this, true);
        }

        /// <summary>
        /// Called when mouse enters.
        /// </summary>
        public void ListItemMouseEnter()
        {
            if (State == "Disabled")
                return;

            IsMouseOver.DirectValue = true;
            if (IsSelected)
                return;
            
            if (IsPressed)
            {
                SetState("Pressed");
            }
            else
            {
                SetState("Highlighted");
            }
        }

        /// <summary>
        /// Called when mouse exits.
        /// </summary>
        public void ListItemMouseExit()
        {
            if (State == "Disabled")
                return;

            IsMouseOver.DirectValue = false;
            if (IsSelected)
                return;

            SetState(DefaultStateName);
        }

        /// <summary>
        /// Called when mouse down.
        /// </summary>
        public void ListItemMouseDown()
        {
            if (ParentList == null || State == "Disabled")
                return;
                        
            if (SelectOnMouseDown.Value)
            {
                ParentList.SelectItem(this, true);
            }
            else
            {
                IsPressed.DirectValue = true;
                if (IsSelected)
                    return;

                SetState("Pressed");
            }
        }

        /// <summary>
        /// Called when mouse up.
        /// </summary>
        public void ListItemMouseUp()
        {
            if (State == "Disabled")
                return;

            IsPressed.DirectValue = false;
            if (IsSelected)
                return;

            if (IsMouseOver)
            {
                SetState("Highlighted");
            }
            else
            {
                SetState(DefaultStateName);
            }
        }

        /// <summary>
        /// Called when the IsSelected field changes.
        /// </summary>
        public virtual void IsSelectedChanged()
        {
            if (State == "Disabled")
                return;

            if (IsSelected)
            {
                SetState("Selected");
            }
            else
            {
                SetState("Default");
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns parent list.
        /// </summary>
        public List ParentList
        {
            get
            {
                if (!_parentList)
                {
                    _parentList = this.FindParent<List>();
                }

                return _parentList;
            }
            set
            {
                _parentList = value;
            }
        }

        #endregion
    }
}
