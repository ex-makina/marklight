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
    /// ViewSwitcher view.
    /// </summary>
    /// <d>Provides functionality for switching between views (presenting one view at a time). Can apply animations to views being switched to/from.</d>
    [HideInPresenter]
    public class ViewSwitcher : UIView
    {
        #region Fields

        /// <summary>
        /// Id of view displayed by default.
        /// </summary>
        /// <d>Id of the view to be displayed by default.</d>
        public _string StartView;

        /// <summary>
        /// Display default view if no start view is set.
        /// </summary>
        /// <d>Boolean indicating if default view (first view) should be displayed if no start view is set.</d>
        public _bool SwitchToDefault;

        /// <summary>
        /// Transition in animation ID.
        /// </summary>
        /// <d>ID of view animation to apply on views transitioned to.</d>
        [ChangeHandler("BehaviorChanged")]
        public _string TransitionIn;

        /// <summary>
        /// Transition out animation ID.
        /// </summary>
        /// <d>ID of view animation to apply on views transitioned from.</d>
        [ChangeHandler("BehaviorChanged")]
        public _string TransitionOut;

        /// <summary>
        /// Transition in animation.
        /// </summary>
        /// <d>Reference to the animation applied to views transitioned to.</d>
        [ChangeHandler("BehaviorChanged")]
        public ViewAnimation TransitionInAnimation;

        /// <summary>
        /// Transition out animation.
        /// </summary>
        /// <d>Reference to the animation applied to views transitioned from.</d>
        [ChangeHandler("BehaviorChanged")]
        public ViewAnimation TransitionOutAnimation;

        /// <summary>
        /// Active view.
        /// </summary>
        /// <d>Reference to the view currently displayed.</d>
        public View ActiveView;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            SwitchToDefault.DirectValue = true;
        }

        /// <summary>
        /// Initializes the view switcher.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            if (!IsSet(() => StartView))
            {
                if (SwitchToDefault)
                {
                    SwitchTo(0, false);
                }
                else
                {
                    // deactive all views
                    this.ForEachChild<View>(x => x.Deactivate(), false);
                }
            }
            else
            {
                SwitchTo(StartView, false);
            }
        }

        /// <summary>
        /// Switches to another view.
        /// </summary>
        public void SwitchTo(View view, bool animate = true)
        {
            this.ForEachChild<View>(x => SwitchTo(x, x == view, animate), false);
        }

        /// <summary>
        /// Switches to another view.
        /// </summary>
        public void SwitchTo(string id, bool animate = true)
        {
            this.ForEachChild<View>(x => SwitchTo(x, String.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase), animate), false);
        }

        /// <summary>
        /// Switches to another view.
        /// </summary>
        public void SwitchTo(int index, bool animate = true)
        {
            int i = 0;
            this.ForEachChild<View>(x =>
            {
                SwitchTo(x, index == i, animate);
                ++i;
            }, false);
        }

        /// <summary>
        /// Switches to view.
        /// </summary>
        private void SwitchTo(View view, bool active, bool animate)
        {
            bool previouslyEnabled = view.IsActive;
            if (!active && previouslyEnabled && animate)
            {
                if (TransitionOutAnimation)
                {
                    TransitionOutAnimation.SetAnimationTarget(view);
                    TransitionOutAnimation.StartAnimation();
                }
                else
                {
                    view.Deactivate();
                }
            }

            if (!animate)
            {
                if (active)
                {
                    view.Activate();
                }
                else
                {
                    view.Deactivate();
                }
            }

            // set animation target and start transition-in animation
            if (active && animate && !previouslyEnabled)
            {
                if (TransitionInAnimation != null)
                {
                    TransitionInAnimation.SetAnimationTarget(view);
                    TransitionInAnimation.StartAnimation();
                }
                view.Activate();
            }

            if (active)
            {
                ActiveView = view;
            }
        }

        /// <summary>
        /// Updates the behavior of the view.
        /// </summary>
        public override void BehaviorChanged()
        {
            base.BehaviorChanged();

            if (!String.IsNullOrEmpty(TransitionIn))
            {
                TransitionInAnimation = LayoutRoot.Find<ViewAnimation>(TransitionIn);
            }

            if (!String.IsNullOrEmpty(TransitionOut))
            {
                TransitionOutAnimation = LayoutRoot.Find<ViewAnimation>(TransitionOut);
            }
        }

        /// <summary>
        /// Switches to next view.
        /// </summary>
        public void Next(bool animate = true, bool cycle = true)
        {
            var views = this.GetChildren<View>(false);
            int viewCount = views.Count;

            if (viewCount <= 0)
                return;

            int index = views.IndexOf(ActiveView);
            ++index;

            if (index >= viewCount)
            {
                if (cycle)
                {
                    SwitchTo(0, animate);
                }

                return;
            }

            SwitchTo(index, animate);
        }

        /// <summary>
        /// Switches to previous view.
        /// </summary>
        public void Previous(bool animate = true, bool cycle = true)
        {
            var views = this.GetChildren<View>(false);
            int viewCount = views.Count;

            if (viewCount <= 0)
                return;

            int index = views.IndexOf(ActiveView);
            --index;

            if (index < 0)
            {
                if (cycle)
                {
                    SwitchTo(viewCount - 1, animate);
                }

                return;
            }

            SwitchTo(index, animate);
        }

        #endregion
    }
}
