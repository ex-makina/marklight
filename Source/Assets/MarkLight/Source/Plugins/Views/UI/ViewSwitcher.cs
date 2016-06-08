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
        /// Deactivate views not currently active in view switcher.
        /// </summary>
        /// <d>Boolean indicating if views not currently being switched to should be deactivated.</d>
        public _bool DeactiveViews;

        /// <summary>
        /// Transition in animation ID.
        /// </summary>
        /// <d>ID of view animation to apply on views transitioned to.</d>
        public _string TransitionIn;

        /// <summary>
        /// Transition out animation ID.
        /// </summary>
        /// <d>ID of view animation to apply on views transitioned from.</d>
        public _string TransitionOut;

        /// <summary>
        /// Transition in reverse animation ID.
        /// </summary>
        /// <d>ID of view animation to apply on views transitioned to when going from a higher indexed view to a lower.</d>
        public _string TransitionInReverse;

        /// <summary>
        /// Transition out reverse animation ID.
        /// </summary>
        /// <d>ID of view animation to apply on views transitioned from when going from a higher indexed view to a lower.</d>
        public _string TransitionOutReverse;

        /// <summary>
        /// Transition in animation.
        /// </summary>
        /// <d>Reference to the animation applied to views transitioned to.</d>
        public ViewAnimation TransitionInAnimation;

        /// <summary>
        /// Transition out animation.
        /// </summary>
        /// <d>Reference to the animation applied to views transitioned from.</d>
        public ViewAnimation TransitionOutAnimation;

        /// <summary>
        /// Transition in reverse animation.
        /// </summary>
        /// <d>Reference to the animation applied to views transitioned to when going from a higher indexed view to a lower.</d>
        public ViewAnimation TransitionInReverseAnimation;

        /// <summary>
        /// Transition out reverse animation.
        /// </summary>
        /// <d>Reference to the animation applied to views transitioned from when going from a higher indexed view to a lower.</d>
        public ViewAnimation TransitionOutReverseAnimation;

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
            DeactiveViews.DirectValue = true;
        }

        /// <summary>
        /// Initializes the view switcher.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            
            if (!String.IsNullOrEmpty(TransitionIn))
            {
                TransitionInAnimation = LayoutRoot.Find<ViewAnimation>(TransitionIn);
            }

            if (!String.IsNullOrEmpty(TransitionOut))
            {
                TransitionOutAnimation = LayoutRoot.Find<ViewAnimation>(TransitionOut);
                if (TransitionOutAnimation != null)
                {
                    TransitionOutAnimation.AnimationCompleted.AddEntry(new ViewActionEntry
                    {
                        ParentView = this,
                        SourceView = TransitionOutAnimation,
                        ViewActionFieldName = "AnimationCompleted",
                        ViewActionHandlerName = "TransitionOutCompleted"
                    });
                }
            }

            if (!String.IsNullOrEmpty(TransitionInReverse))
            {
                TransitionInReverseAnimation = LayoutRoot.Find<ViewAnimation>(TransitionInReverse);
            }

            if (!String.IsNullOrEmpty(TransitionOutReverse))
            {
                TransitionOutReverseAnimation = LayoutRoot.Find<ViewAnimation>(TransitionOutReverse);
                if (TransitionOutReverseAnimation != null)
                {
                    TransitionOutReverseAnimation.AnimationCompleted.AddEntry(new ViewActionEntry
                    {
                        ParentView = this,
                        SourceView = TransitionOutReverseAnimation,
                        ViewActionFieldName = "AnimationCompleted",
                        ViewActionHandlerName = "TransitionOutCompleted"
                    });
                }
            }

            if (!StartView.IsSet)
            {
                if (SwitchToDefault)
                {
                    if (ChildCount > 0)
                    {
                        SwitchTo(0, false);
                    }
                }
                else
                {
                    // deactive all views
                    if (DeactiveViews)
                    {
                        this.ForEachChild<View>(x => x.Deactivate(), false);
                    }
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
            SwitchTo(view, null, animate);
        }

        /// <summary>
        /// Switches to another view.
        /// </summary>
        public void SwitchTo(string id, bool animate = true)
        {
            SwitchTo(id, null, animate);
        }

        /// <summary>
        /// Switches to another view.
        /// </summary>
        public void SwitchTo(int index, bool animate = true)
        {
            SwitchTo(index, null, animate);
        }

        /// <summary>
        /// Switches to another view passing data to it.
        /// </summary>
        public void SwitchTo(string id, object data, bool animate)
        {
            var view = this.Find<View>(id, false);
            SwitchTo(view, data, animate);
        }

        /// <summary>
        /// Switches to another view passing data to it.
        /// </summary>
        public void SwitchTo(int index, object data, bool animate)
        {
            var view = this.GetChild(index);
            SwitchTo(view, data, animate);
        }

        /// <summary>
        /// Switches to another view passing data to it.
        /// </summary>
        public void SwitchTo(View view, object data, bool animate)
        {
            var oldActiveView = ActiveView;
            ActiveView = view;

            // activate new one
            if (data != null)
            {
                view.Activate(data);
            }
            else
            {
                view.Activate();
            }

            // animate transition in and out
            bool transitionOutOld = animate && oldActiveView != null && ActiveView != oldActiveView;
            bool transitionOutStarted = false;

            if (animate)
            {
                bool transitionReverse = false;

                // check if new view comes before old view
                if (transitionOutOld)
                {
                    foreach (var child in this)
                    {
                        if (child == oldActiveView)
                        {
                            break;
                        }

                        if (child == ActiveView)
                        {
                            transitionReverse = true;
                            break;
                        }
                    }
                }

                // set and stop any animations before starting
                var transitionInAnimation = TransitionInAnimation;
                if (transitionInAnimation != null)
                {
                    transitionInAnimation.StopAnimation();
                }

                if (transitionReverse && TransitionInReverseAnimation != null)
                {
                    transitionInAnimation = TransitionInReverseAnimation;
                    transitionInAnimation.StopAnimation();
                }

                var transitionOutAnimation = TransitionOutAnimation;
                if (transitionOutAnimation != null)
                {
                    transitionOutAnimation.StopAnimation();
                }

                if (transitionReverse && TransitionOutReverseAnimation != null)
                {
                    transitionOutAnimation = TransitionOutReverseAnimation;
                    transitionOutAnimation.StopAnimation();
                }

                // do we switch from an old view to a new one?
                if (transitionOutOld && transitionOutAnimation != null)
                {
                    // yes. transition out the old one
                    transitionOutAnimation.SetAnimationTarget(oldActiveView);
                    transitionOutAnimation.StartAnimation();
                    transitionOutStarted = true;
                }

                // start transition in animation
                if (transitionInAnimation != null)
                {
                    transitionInAnimation.SetAnimationTarget(view);
                    transitionInAnimation.StartAnimation();
                }
            }

            // disable the rest         
            if (DeactiveViews)
            {
                this.ForEachChild<View>(x =>
                {
                    if (x == ActiveView)
                        return;

                    if (transitionOutStarted && x == oldActiveView)
                        return;

                    x.Deactivate();
                }, false);
            }
        }

        /// <summary>
        /// Called when transition out animation is completed.
        /// </summary>
        public void TransitionOutCompleted(ViewAnimation animation)
        {            
            if (animation.Target != null && DeactiveViews)
            {
                if (animation.Target != ActiveView)
                {
                    animation.Target.Deactivate();
                }
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
