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

namespace MarkLight.Views
{
    /// <summary>
    /// Handles input, raycasting and sending events.
    /// </summary>
    /// <d>Handles input, raycasting and sending events.</d>
    [HideInPresenter]
    public class EventSystem : View
    {
        #region Fields

        #region UnityEventSystem

        /// <summary>
        /// GameObject selected first.
        /// </summary>
        /// <d>The GameObject that was selected first.</d>
        [MapTo("UnityEventSystem.firstSelectedGameObject")]
        public _GameObject FirstSelectedGameObject;

        /// <summary>
        /// GameObject currently selected.
        /// </summary>
        /// <d>The GameObject that is currently selected.</d>
        [MapTo("UnityEventSystem.currentSelectedGameObject")]
        public _GameObject CurrentSelectedGameObject;

        /// <summary>
        /// Pixel drag threshold.
        /// </summary>
        /// <d>The soft area for dragging in pixels.</d>
        [MapTo("UnityEventSystem.pixelDragThreshold")]
        public _int PixelDragThreshold;

        /// <summary>
        /// Indicates if navigation events are sent.
        /// </summary>
        /// <d>Boolean indicating if navigation events (move, submit, cancel) are handled by the event system.</d>
        [MapTo("UnityEventSystem.sendNavigationEvents")]
        public _bool SendNavigationEvents;

        /// <summary>
        /// Unity event system.
        /// </summary>
        /// <d>Handles input, raycasting and sending events.</d>
        public UnityEngine.EventSystems.EventSystem UnityEventSystem;

        #endregion

        #region StandaloneInputModule

        /// <summary>
        /// Horizontal axis button name.
        /// </summary>
        /// <d>Input manager name for the horizontal axis button.</d>
        [MapTo("StandaloneInputModule.horizontalAxis")]
        public _string HorizontalAxis;

        /// <summary>
        /// Vertical axis button name.
        /// </summary>
        /// <d>Input manager name for the vertical axis button.</d>
        [MapTo("StandaloneInputModule.verticalAxis")]
        public _string VerticalAxis;

        /// <summary>
        /// Submit button name.
        /// </summary>
        /// <d>Input manager name for the submit button.</d>
        [MapTo("StandaloneInputModule.submitButton")]
        public _string SubmitButton;

        /// <summary>
        /// Cancel button name.
        /// </summary>
        /// <d>Input manager name for the cancel button.</d>
        [MapTo("StandaloneInputModule.cancelButton")]
        public _string CancelButton;

        /// <summary>
        /// Input actions allowed per second.
        /// </summary>
        /// <d>Number of keyboard / controller inputs allowed per second.</d>
        [MapTo("StandaloneInputModule.inputActionsPerSecond")]
        public _float InputActionsPerSecond;

        /// <summary>
        /// Indicates if input module is activated on mobile devices.
        /// </summary>
        /// <d>Boolean indicates if the standalone input module is to be activated on mobile devices.</d>
        [MapTo("StandaloneInputModule.allowActivationOnMobileDevice")]
        public _bool AllowActivationOnMobileDevice;

        /// <summary>
        /// Standalone input module.
        /// </summary>
        /// <d>Standalone input module for designed for mouse and keyboard controller input.</d>
        public StandaloneInputModule StandaloneInputModule;

        #endregion

        #region TouchInputModule

#if UNITY_4_6 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
        public TouchInputModule TouchInputModule;
#endif

        #endregion

        #endregion
    }
}
