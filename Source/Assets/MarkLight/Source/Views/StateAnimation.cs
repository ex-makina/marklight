#region Using Statements
using MarkLight.Animation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight.Views
{
    /// <summary>
    /// Animates views.
    /// </summary>
    [HideInPresenter]
    public class StateAnimation : ViewAnimation
    {
        #region Fields

        public string From;
        public string To;

        private Dictionary<string, List<Animate>> _animatedFields;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();
            GameObject.hideFlags = UnityEngine.HideFlags.HideInHierarchy;
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void InitializeInternal()
        {
            base.InitializeInternal();

            _animatedFields = new Dictionary<string, List<Animate>>();

            // register the state animation in the parent
            if (LayoutParent != null)
            {
                LayoutParent.AddStateAnimation(this);
                Target = LayoutParent;
            }

            // set animation target and update list of animated fields
            this.ForEachChild<Animate>(x =>
            {
                x.SetAnimationTarget(Target);

                if (String.IsNullOrEmpty(x.Field))
                    return;

                if (!_animatedFields.ContainsKey(x.Field))
                {
                    _animatedFields.Add(x.Field, new List<Animate>());
                }

                _animatedFields[x.Field].Add(x);
            }, false);
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public override void StartAnimation()
        {
            this.ForEachChild<ViewAnimation>(x => x.StartAnimation(), false);
            AnimationStarted.Trigger();
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public override void StopAnimation()
        {
            this.ForEachChild<ViewAnimation>(x => x.StopAnimation(), false);
            AnimationStopped.Trigger();
        }

        /// <summary>
        /// Resets the animation.
        /// </summary>
        public override void ResetAnimation()
        {
            this.ForEachChild<ViewAnimation>(x => x.ResetAnimation(), false);            
        }

        /// <summary>
        /// Resets and stops the animation.
        /// </summary>
        public override void ResetAndStopAnimation()
        {
            this.ForEachChild<ViewAnimation>(x => x.ResetAndStopAnimation(), false);
            AnimationStopped.Trigger();
        }

        /// <summary>
        /// Reverses the animation.
        /// </summary>
        public override void ReverseAnimation()
        {
            this.ForEachChild<ViewAnimation>(x => x.ReverseAnimation(), false);
            AnimationReversed.Trigger();
        }

        /// <summary>
        /// Pauses the animation.
        /// </summary>
        public override void PauseAnimation()
        {
            this.ForEachChild<ViewAnimation>(x => x.PauseAnimation(), false);
            AnimationPaused.Trigger();
        }

        /// <summary>
        /// Resumes the animation.
        /// </summary>
        public override void ResumeAnimation()
        {
            this.ForEachChild<ViewAnimation>(x => x.ResumeAnimation(), false);
            AnimationResumed.Trigger();
        }

        /// <summary>
        /// Called if a child animation has been completed. 
        /// </summary>
        public override void ChildAnimationCompleted()
        {            
            if (IsAnimationCompleted)
            {
                AnimationCompleted.Trigger();
            }
        }

        /// <summary>
        /// Gets list of animators for specified view field.
        /// </summary>
        public List<Animate> GetFieldAnimators(string viewFieldPath)
        {
            return _animatedFields.Get(viewFieldPath);
        }

        #endregion

    }
}
