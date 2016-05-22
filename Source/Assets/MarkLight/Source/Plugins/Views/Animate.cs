#region Using Statements
using MarkLight.Animation;
using MarkLight.ValueConverters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// <d>Animates view fields.</d>
    [HideInPresenter]
    public class Animate : ViewAnimation
    {
        #region Fields

        /// <summary>
        /// Animation easing function
        /// </summary>
        /// <d>Easing function to be used when interpolating between From and To animation values.</d>
        [ChangeHandler("BehaviorChanged")]
        public EasingFunctionType EasingFunction;

        /// <summary>
        /// Auto reset animation.
        /// </summary>
        /// <d>Boolean indicating if the animation automatically should be reset when completed.</d>
        [ChangeHandler("BehaviorChanged")]
        public bool AutoReset;

        /// <summary>
        /// Auto reverse animation.
        /// </summary>
        /// <d>Boolean indicating if animation automatically should be reversed when completed.</d>
        [ChangeHandler("BehaviorChanged")]
        public bool AutoReverse;

        /// <summary>
        /// Animation view field.
        /// </summary>
        /// <d>Path to the view field that should be animated.</d>
        [ChangeHandler("BehaviorChanged")]
        public string Field;

        /// <summary>
        /// From animation value.
        /// </summary>
        /// <d>The starting value to be set when the animation starts.</d>
        [ChangeHandler("BehaviorChanged")]
        public object From;

        /// <summary>
        /// To animation value.
        /// </summary>
        /// <d>The end value to be interpolated to during animation.</d>
        [ChangeHandler("BehaviorChanged")]
        public object To;

        /// <summary>
        /// Animation reverse speed.
        /// </summary>
        /// <d>The speed the animation should have when run in reverse (percentage of original speed).</d>
        [ChangeHandler("BehaviorChanged")]
        public float ReverseSpeed;

        /// <summary>
        /// Duration of animation.
        /// </summary>
        /// <d>The duration of the animation.</d>
        [ChangeHandler("BehaviorChanged")]
        [DurationValueConverter]
        public float Duration; // duration in seconds

        /// <summary>
        /// Animation start offset.
        /// </summary>
        /// <d>Indicates a delay in starting the animation after it is triggered.</d>
        [ChangeHandler("BehaviorChanged")]
        [DurationValueConverter]
        public float StartOffset;

        [NotSetFromXuml]
        public string FromStringValue;

        [NotSetFromXuml]
        public string ToStringValue;

        private ViewFieldAnimator _viewFieldAnimator;

        #endregion

        #region Properties

        /// <summary>
        /// Gets a boolean indicating whether this animation is running.
        /// </summary>
        public override bool IsAnimationRunning
        {
            get
            {
                return _viewFieldAnimator.IsRunning;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether this animation is reversing.
        /// </summary>
        public override bool IsAnimationReversing
        {
            get
            {
                return _viewFieldAnimator.IsReversing;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether this animation is completed.
        /// </summary>
        public override bool IsAnimationCompleted
        {
            get
            {
                return _viewFieldAnimator.IsCompleted;
            }
        }

        /// <summary>
        /// Gets a boolean indicating whether this animation is paused.
        /// </summary>
        public override bool IsAnimationPaused
        {
            get
            {
                return _viewFieldAnimator.IsPaused;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            GameObject.hideFlags = UnityEngine.HideFlags.HideInHierarchy;
            EasingFunction = EasingFunctionType.Linear;
            AutoReset = false;
            AutoReverse = false;
            Duration = 0f;
            ReverseSpeed = 1.0f;
        }

        /// <summary>
        /// Updates the animation each frame.
        /// </summary>
        public virtual void Update()
        {
            if (Application.isPlaying && _viewFieldAnimator != null)
            {
                _viewFieldAnimator.Update();
            }
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public override void StartAnimation()
        {
            _viewFieldAnimator.StartAnimation();            
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public override void StopAnimation()
        {
            _viewFieldAnimator.StopAnimation();
        }

        /// <summary>
        /// Resets and stops animation.
        /// </summary>
        public override void ResetAndStopAnimation()
        {
            _viewFieldAnimator.ResetAndStopAnimation();
        }

        /// <summary>
        /// Reverses the animation. Resumes the animation if paused.
        /// </summary>
        public override void ReverseAnimation()
        {
            _viewFieldAnimator.ReverseAnimation();
        }

        /// <summary>
        /// Pauses animation.
        /// </summary>
        public override void PauseAnimation()
        {
            _viewFieldAnimator.PauseAnimation();
        }

        /// <summary>
        /// Resumes paused animation.
        /// </summary>
        public override void ResumeAnimation()
        {
            _viewFieldAnimator.ResumeAnimation();
        }

        /// <summary>
        /// Resets the animation to its initial state (doesn't stop it).
        /// </summary>
        public override void ResetAnimation()
        {
            _viewFieldAnimator.ResetAnimation();
        }

        /// <summary>
        /// Called once by view before animations are used.
        /// </summary>
        public override void BehaviorChanged()
        {
            base.BehaviorChanged();

            UpdateViewFieldAnimator();
        }

        /// <summary>
        /// Sets animation target.
        /// </summary>
        public override void SetAnimationTarget(View view)
        {
            Target = view;
            UpdateViewFieldAnimator();
        }

        /// <summary>
        /// Initializes the view.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            _viewFieldAnimator = new ViewFieldAnimator();
            UpdateViewFieldAnimator();

            _viewFieldAnimator.Started += _viewFieldAnimator_Started;
            _viewFieldAnimator.Completed += _viewFieldAnimator_Completed;
            _viewFieldAnimator.Paused += _viewFieldAnimator_Paused;
            _viewFieldAnimator.Resumed += _viewFieldAnimator_Resumed;
            _viewFieldAnimator.Stopped += _viewFieldAnimator_Stopped;
            _viewFieldAnimator.Reversed += _viewFieldAnimator_Reversed;
        }

        /// <summary>
        /// Called when animation started.
        /// </summary>
        void _viewFieldAnimator_Started(ViewFieldAnimator viewFieldAnimator)
        {
            AnimationStarted.Trigger();
        }

        /// <summary>
        /// Called when animation completed.
        /// </summary>
        void _viewFieldAnimator_Completed(ViewFieldAnimator viewFieldAnimator)
        {
            AnimationCompleted.Trigger();

            // notify parent
            if (LayoutParent is ViewAnimation)
            {
                (LayoutParent as ViewAnimation).ChildAnimationCompleted();
            }
        }

        /// <summary>
        /// Called when animation paused.
        /// </summary>
        void _viewFieldAnimator_Paused(ViewFieldAnimator viewFieldAnimator)
        {
            AnimationPaused.Trigger();
        }

        /// <summary>
        /// Called when animation reversed.
        /// </summary>
        void _viewFieldAnimator_Reversed(ViewFieldAnimator viewFieldAnimator)
        {
            AnimationReversed.Trigger();
        }

        /// <summary>
        /// Called when animation stopped.
        /// </summary>
        void _viewFieldAnimator_Stopped(ViewFieldAnimator viewFieldAnimator)
        {
            AnimationStopped.Trigger();
        }

        /// <summary>
        /// Called when animation resumed.
        /// </summary>
        void _viewFieldAnimator_Resumed(ViewFieldAnimator viewFieldAnimator)
        {
            AnimationResumed.Trigger();
        }

        /// <summary>
        /// Updates view field animator.
        /// </summary>
        public void UpdateViewFieldAnimator()
        {
            //Debug.Log(String.Format("Updating View Field Animator: {0}: {1}, {2}", Field, From, To));
            if (From != null && From is String)
            {
                FromStringValue = (String)From;
            }

            if (To != null && To is String)
            {
                ToStringValue = (String)To;
            }

            if (_viewFieldAnimator == null)
            {
                _viewFieldAnimator = new ViewFieldAnimator();
            }

            _viewFieldAnimator.EasingFunction = EasingFunction;
            _viewFieldAnimator.AutoReset = AutoReset;
            _viewFieldAnimator.AutoReverse = AutoReverse;
            _viewFieldAnimator.Field = Field;
            _viewFieldAnimator.From = From;
            _viewFieldAnimator.To = To;
            _viewFieldAnimator.FromStringValue = FromStringValue;
            _viewFieldAnimator.ToStringValue = ToStringValue;
            _viewFieldAnimator.ReverseSpeed = ReverseSpeed;
            _viewFieldAnimator.Duration = Duration;
            _viewFieldAnimator.StartOffset = StartOffset;

            var view = Target != null ? Target :
                (Parent != null ? Parent : null);
            _viewFieldAnimator.TargetView = view;
        }

        #endregion
    }
}
