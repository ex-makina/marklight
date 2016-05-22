#region Using Statements
using MarkLight.Animation;
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
    /// RadialMenu view.
    /// </summary>
    /// <d>Arranges content in a circle. Can be opened/closed with optional animation. Radius and angles can be adjusted.</d>
    [HideInPresenter]
    public class RadialMenu : UIView
    {
        #region Fields
               
        /// <summary>
        /// Radius of the menu.
        /// </summary>
        /// <d>Value determined the radial distance of the content from the center.</d>
        public _float Radius;

        /// <summary>
        /// Start angle.
        /// </summary>
        /// <d>The start angle of the radial menu.</d>
        public _float StartAngle;

        /// <summary>
        /// End angle.
        /// </summary>
        /// <d>The end angle of the radial menu.</d>
        public _float EndAngle;

        /// <summary>
        /// Animation duration.
        /// </summary>
        /// <d>The open/close animation duration.</d>
        [DurationValueConverter]
        public _float AnimationDuration;

        private bool _isOpen;
        private List<ViewFieldAnimator> _menuAnimators;        
        private List<UIView> _menuItems;
        private List<UIView> _deactivatedMenuItems;
        private Vector2 _menuOffset;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Radius.DirectValue = 100;
            StartAngle.DirectValue = 0;
            EndAngle.DirectValue = 360;
        }

        /// <summary>
        /// Updates view field animators.
        /// </summary>
        public virtual void Update()
        {
            _menuAnimators.ForEach(x =>
            {
                x.Update();
                if (x.IsCompleted && !_isOpen)
                {
                    x.View.Deactivate();
                }
            });
        }

        /// <summary>
        /// Toggles radial menu.
        /// </summary>
        public void Toggle(bool animate = true)
        {
            if (_isOpen)
            {
                Close(animate);
            }
            else
            {
                Open(animate);
            }
        }

        /// <summary>
        /// Toggles radial menu.
        /// </summary>
        public void ToggleAt(Vector2 position, bool animate = true)
        {
            if (_isOpen)
            {
                Close(animate);
            }
            else
            {
                OpenAt(position, animate);
            }
        }

        /// <summary>
        /// Opens radial menu at position.
        /// </summary>
        public void OpenAt(Vector2 mouseScreenPositionIn, bool animate = true)
        {
            // calculate menu offset
            Vector2 pos = GetLocalPoint(mouseScreenPositionIn);
            _menuOffset.x = pos.x;
            _menuOffset.y = -pos.y;
            UpdateMenu();

            Open(animate);
        }

        /// <summary>
        /// Opens the radial menu.
        /// </summary>
        public void Open(bool animate = true)
        {
            _isOpen = true;

            // activate views 
            if (!animate)
            {
                int childCount = _menuItems.Count() - _deactivatedMenuItems.Count();
                if (childCount > 0)
                {
                    float deltaAngle = Mathf.Deg2Rad * ((EndAngle - StartAngle) / childCount);
                    float angle = Mathf.Deg2Rad * StartAngle;

                    foreach (var child in _menuItems)
                    {
                        if (_deactivatedMenuItems.Contains(child))
                        {
                            continue;
                        }

                        // set offset
                        float xOffset = Radius * Mathf.Sin(angle);
                        float yOffset = Radius * Mathf.Cos(angle);

                        child.OffsetFromParent.DirectValue = new ElementMargin(xOffset + _menuOffset.x, -yOffset + _menuOffset.y, 0, 0);
                        child.Alignment.DirectValue = ElementAlignment.Center;
                        child.Activate();
                        child.LayoutChanged();
                        angle += deltaAngle;
                    }
                }
            }
            else
            {
                _menuItems.ForEach(x =>
                {
                    if (!_deactivatedMenuItems.Contains(x))
                    {
                        x.Activate();
                    }
                });

                _menuAnimators.ForEach(x => x.StartAnimation());
            }
        }

        /// <summary>
        /// Closes the radial menu.
        /// </summary>
        public void Close(bool animate = true)
        {
            _isOpen = false;

            // deactivate views
            if (animate)
            {
                _menuAnimators.ForEach(x => x.ReverseAnimation());
            }
            else
            {
                _menuItems.ForEach(x =>
                {
                    x.Deactivate();
                });
            }
        }

        /// <summary>
        /// Activates a view within the radial menu.
        /// </summary>
        public void ActivateMenuItem(string id)
        {
            var view = this.Find<UIView>(id, false);
            if (view == null)
            {
                Utils.LogError("[MarkLight] {0}: Unable to activate menu item. Menu item \"{1}\" not found.", GameObjectName, id);
                return;
            }

            ActivateMenuItem(view);
        }

        /// <summary>
        /// Activates a view within the radial menu.
        /// </summary>
        public void ActivateMenuItem(int index)
        {
            if (index >= _menuItems.Count() || index < 0)
            {
                Utils.LogError("[MarkLight] {0}: Unable to activate menu item. Index out of range.", GameObjectName);
                return;
            }

            DeactivateMenuItem(_menuItems[index]);
        }

        /// <summary>
        /// Activates a view within the radial menu.
        /// </summary>
        public void ActivateMenuItem(UIView view)
        {
            if (_deactivatedMenuItems.Contains(view))
            {
                _deactivatedMenuItems.Remove(view);
                UpdateMenu();
            }
        }

        /// <summary>
        /// Deactivates a view within the radial menu.
        /// </summary>
        public void DeactivateMenuItem(string id)
        {
            var view = this.Find<UIView>(id, false);
            if (view == null)
            {
                Utils.LogError("[MarkLight] {0}: Unable to deactivate menu item. Menu item \"{1}\" not found.", GameObjectName, id);
                return;
            }

            DeactivateMenuItem(view.GetComponent<UIView>());
        }

        /// <summary>
        /// Deactivates a view within the radial menu.
        /// </summary>
        public void DeactivateMenuItem(int index)
        {
            if (index >= _menuItems.Count() || index < 0)
            {
                Utils.LogError("[MarkLight] {0}: Unable to deactivate menu item. Index out of range.", GameObjectName);
                return;
            }

            DeactivateMenuItem(_menuItems[index]);
        }

        /// <summary>
        /// Deactivates a view within the radial menu.
        /// </summary>
        public void DeactivateMenuItem(UIView view)
        {
            if (!_deactivatedMenuItems.Contains(view))
            {
                _deactivatedMenuItems.Add(view);
                UpdateMenu();
            }
        }

        /// <summary>
        /// Updates menu views and offset animators.
        /// </summary>
        public void UpdateMenu()
        {
            _menuAnimators.Clear();
            int activeChildCount = _menuItems.Count() - _deactivatedMenuItems.Count();
            if (activeChildCount > 0)
            {
                float deltaAngle = Mathf.Deg2Rad * ((EndAngle - StartAngle) / activeChildCount);
                float angle = Mathf.Deg2Rad * StartAngle;

                foreach (var child in _menuItems)
                {
                    if (_deactivatedMenuItems.Contains(child))
                    {
                        continue;
                    }

                    // calculate offset
                    float xOffset = Radius * Mathf.Sin(angle);
                    float yOffset = Radius * Mathf.Cos(angle);

                    // set offset animator
                    var offsetAnimator = new ViewFieldAnimator();
                    offsetAnimator.EasingFunction = EasingFunctionType.Linear;
                    offsetAnimator.Field = "OffsetFromParent";
                    offsetAnimator.From = new ElementMargin(_menuOffset.x, _menuOffset.y);
                    offsetAnimator.To = new ElementMargin(xOffset + _menuOffset.x, -yOffset + _menuOffset.y, 0, 0);
                    offsetAnimator.Duration = AnimationDuration.IsSet ? AnimationDuration : 0.2f;
                    offsetAnimator.TargetView = child;
                    _menuAnimators.Add(offsetAnimator);                    

                    child.OffsetFromParent.DirectValue = new ElementMargin(_menuOffset.x, _menuOffset.y, 0, 0);
                    child.Alignment.DirectValue = ElementAlignment.Center;
                    child.Deactivate();
                    child.LayoutChanged();
                    angle += deltaAngle;
                }
            }

            foreach (var deactivatedItem in _deactivatedMenuItems)
            {
                deactivatedItem.Deactivate();
                deactivatedItem.LayoutChanged();
            }
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            _isOpen = false;
            _menuOffset = new Vector2();
            _menuAnimators = new List<ViewFieldAnimator>();            
            _deactivatedMenuItems = new List<UIView>();
            _menuItems = this.GetChildren<UIView>(false);

            UpdateMenu();
            Close(false);
        }

        #endregion
    }
}
