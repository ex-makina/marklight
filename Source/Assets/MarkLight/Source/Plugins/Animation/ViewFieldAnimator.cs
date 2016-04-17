#region Using Statements
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

namespace MarkLight.Animation
{
    /// <summary>
    /// Animates a field.
    /// </summary>
    public class ViewFieldAnimator
    {
        #region Fields

        public delegate void AnimationEventHandler(ViewFieldAnimator viewFieldAnimator);

        public event AnimationEventHandler Started = delegate { };
        public event AnimationEventHandler Completed = delegate { };
        public event AnimationEventHandler Paused = delegate { };
        public event AnimationEventHandler Reversed = delegate { };
        public event AnimationEventHandler Stopped = delegate { };
        public event AnimationEventHandler Resumed = delegate { };

        public View TargetView;
        public EasingFunctionType EasingFunction;
        public bool AutoReset;
        public bool AutoReverse;
        public string Field;
        public object From;
        public object To;
        public float ReverseSpeed;
        public float Duration; // duration in seconds
        public float StartOffset;
        public string FromStringValue;
        public string ToStringValue;

        private ValueInterpolator _valueInterpolator;
        private EasingFunctions.EasingFunction _easingFunction;

        // animation state
        private bool _isRunning;
        private bool _isReversing;
        private bool _isCompleted;
        private bool _isPaused;

        private float _elapsedTime;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ViewFieldAnimator()
        {
            EasingFunction = EasingFunctionType.Linear;
            AutoReset = false;
            AutoReverse = false;
            Duration = 0f;
            ReverseSpeed = 1.0f;

            // default animation state
            _isRunning = false;
            _isReversing = false;
            _isCompleted = true;
            _isPaused = true;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the animation each frame.
        /// </summary>
        public void Update()
        {
            if (_isRunning)
            {
                UpdateAnimation(Time.deltaTime);
            }
        }

        /// <summary>
        /// Starts the animation.
        /// </summary>
        public void StartAnimation()
        {           
            // do nothing if animation is already active
            if (_isRunning)
            {
                return;
            }

            if (TargetView == null)
            {
                Debug.LogWarning("[MarkLight] Animator started before setting target view.");
                return;
            }

            if (String.IsNullOrEmpty(Field))
            {
                Debug.LogWarning("[MarkLight] Animator started before setting animation view field.");
                return;
            }

            // get info regarding animated field
            var viewFieldData = TargetView.GetViewFieldData(Field);
            if (viewFieldData == null)
            {
                Debug.LogError(String.Format("[MarkLight] Unable to animate view field \"{0}\" on view \"{1}\"\". Field not found.", Field, TargetView.GameObjectName));
                return;
            }

            _valueInterpolator = ViewData.GetValueInterpolatorForType(viewFieldData.ViewFieldTypeName);
            _easingFunction = EasingFunctions.GetEasingFunction(EasingFunction);

            // set converted to and from values
            var converter = ViewData.GetValueConverterForType(viewFieldData.ViewFieldTypeName);
            if (From == null && !String.IsNullOrEmpty(FromStringValue))
            {
                var result = converter.Convert(FromStringValue, ValueConverterContext.Default);
                if (!result.Success)
                {
                    Debug.LogError(String.Format("[MarkLight] Unable to parse animation From value \"{0}\". {1}", From, result.ErrorMessage));
                    return;
                }

                From = result.ConvertedValue;
            }

            if (To == null && !String.IsNullOrEmpty(ToStringValue))
            {
                var result = converter.Convert(ToStringValue, ValueConverterContext.Default);
                if (!result.Success)
                {
                    Debug.LogError(String.Format("[MarkLight] Unable to parse animation To value \"{0}\". {1}", To, result.ErrorMessage));
                    return;
                }

                To = result.ConvertedValue;
            }

            // reset animation
            if (From == null)
            {
                // use current view field value as from value
                From = TargetView.GetValue(Field);
            }

            ResetAnimation();
            _isRunning = true;

            // call start event 
            Started(this);

            // if To hasn't been set - complete animation instantly
            if (To == null && String.IsNullOrEmpty(ToStringValue))
            {
                CompleteAnimation();
            }
        }

        /// <summary>
        /// Updates the animator.
        /// </summary>
        /// <param name="deltaTime">Time since last update in seconds.</param>
        internal void UpdateAnimation(float deltaTime)
        {            
            _elapsedTime = _isReversing ? _elapsedTime - deltaTime * ReverseSpeed :
                _elapsedTime + deltaTime;

            // start animation once passed startOffset
            if (!_isReversing && _elapsedTime < StartOffset)
                return;

            // clamp elapsed time to max duration
            float t = _isReversing ? _elapsedTime.Clamp(0, Duration) : (_elapsedTime - StartOffset).Clamp(0, Duration);            
            float weight = Duration > 0 ? _easingFunction(t / Duration) : (_isReversing ? 0f : 1f);

            object interpolatedValue = null;
            try
            {
                interpolatedValue = _valueInterpolator.Interpolate(From, To, weight);
            }
            catch (Exception e)
            {
                Debug.LogError(String.Format("[MarkLight] Unable to animate field \"{0}\" on view \"{1}\". Stopping animation. Interpolator {2} threw exception: {3}", Field, TargetView.GameObjectName, _valueInterpolator.GetType().Name, Utils.GetError(e)));
                _isRunning = false;
                return;
            }

            // set new value
            try
            {
                TargetView.SetValue(Field, interpolatedValue, false, null, null, true);
            }
            catch (Exception e)
            {
                Debug.LogError(String.Format("[MarkLight] Unable to animate field \"{0}\" on view \"{1}\". Stopping animation. Exception thrown when trying to set view field value to \"{2}\": {3}", Field, TargetView.GameObjectName, interpolatedValue, Utils.GetError(e)));
                _isRunning = false;
                return;
            }

            // is animation done?
            if ((_isReversing && t <= 0) || (!_isReversing && t >= Duration))
            {
                // yes. should animation auto-reverse?
                if (!_isReversing && AutoReverse)
                {
                    // yes. reverse the animation
                    _isReversing = true;

                    // call animation reversed event
                    Reversed(this); 
                    return;
                }

                // animation is complete
                CompleteAnimation();
            }
        }

        /// <summary>
        /// Called when the animation is completed.
        /// </summary>
        public void CompleteAnimation()
        {            
            if (AutoReset)
            {
                ResetAndStopAnimation();
            }
            else
            {
                PauseAnimation();
            }

            _isCompleted = true;

            // call animation completed event
            Completed(this);
        }

        /// <summary>
        /// Stops the animation.
        /// </summary>
        public void StopAnimation()
        {
            _isRunning = false;

            // call animation completed event
            Stopped(this); 
        }

        /// <summary>
        /// Resets and stops animation.
        /// </summary>
        public void ResetAndStopAnimation()
        {
            ResetAnimation();
            StopAnimation();
        }

        /// <summary>
        /// Reverses the animation. Resumes the animation if paused.
        /// </summary>
        public void ReverseAnimation()
        {
            // reverse animation if active
            if (_isRunning)
            {
                _isReversing = true;
            }
            else if (_isPaused)
            {
                _isCompleted = false;
                _isReversing = true;
                ResumeAnimation();
            }

            // call animation reversed event
            Reversed(this);
        }

        /// <summary>
        /// Pauses animation.
        /// </summary>
        public void PauseAnimation()
        {
            _isRunning = false;
            _isPaused = true;

            // call animation paused event
            Paused(this); 
        }

        /// <summary>
        /// Resumes paused animation.
        /// </summary>
        public void ResumeAnimation()
        {
            if (_isPaused)
            {                
                _isRunning = true;
                _isPaused = false;

                // call animation completed event
                Resumed(this); 
            }
        }

        /// <summary>
        /// Resets the animation to its initial state (doesn't stop it).
        /// </summary>
        public void ResetAnimation()
        {
            // resets the animation (but doesn't stop it)                        
            _elapsedTime = 0;
            _isReversing = false;
            _isPaused = false;
            _isCompleted = false;
            TargetView.SetValue(Field, From, false, null, null, true);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether this animation is running.
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this animation is reversing.
        /// </summary>
        public bool IsReversing
        {
            get
            {
                return _isReversing;
            }
        }

        /// <summary>
        /// Gets boolean indicating if animation is completed.
        /// </summary>
        public bool IsCompleted
        {
            get
            {
                return _isCompleted;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this animation is paused.
        /// </summary>
        public bool IsPaused
        {
            get
            {
                return _isPaused;
            }
        }

        /// <summary>
        /// View which field is being animated.
        /// </summary>
        public View View
        {
            get
            {
                return TargetView;
            }
        }

        #endregion
    }
}
