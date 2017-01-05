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
    /// Canvas view.
    /// </summary>
    /// <d>The canvas view is used to render UI components and controls things like draw sort order, scaling and render mode. In order for UIViews to be rendered and positioned correctly they must be put under a parent UICanvas or a subclass of (like UserInterface).</d>
    [ExcludeComponent("ImageComponent")]
    [HideInPresenter]
    public class UICanvas : UIView
    {
        #region Fields

        #region Canvas

        /// <summary>
        /// Canvas render mode.
        /// </summary>
        /// <d>Specifies the render mode to be used by the canvas.</d>
        [MapTo("Canvas.renderMode")]
        public _GraphicRenderMode RenderMode;

        /// <summary>
        /// Indicates if rendering should be pixel perfect.
        /// </summary>
        /// <d>Indicates if the canvas is to be aligned with pixels. Only applied if render mode is set to ScreenSpace.</d>
        [MapTo("Canvas.pixelPerfect")]
        public _bool PixelPerfect;

        /// <summary>
        /// Canvas sorting order.
        /// </summary>
        /// <d>Canvas draw order within a sorting layer.</d>
        [MapTo("Canvas.sortingOrder")]
        public _int SortingOrder;

        /// <summary>
        /// Render camera.
        /// </summary>
        /// <d>Camera used for sizing the canvas when in render mode set to: Screen Space - Camera. Also used as camera that events will be sent through in world space.</d>
        [MapTo("Canvas.worldCamera")]
        public _CameraComponent RenderCamera;

        /// <summary>
        /// Canvas distance from camera.
        /// </summary>
        /// <d>The plane distance determines how far away from the camera the canvas is generated.</d>
        [MapTo("Canvas.planeDistance")]
        public _float PlaneDistance;

        /// <summary>
        /// Unique ID of the canvas sorting layer.
        /// </summary>
        /// <d>Unique ID of the canvas sorting layer.</d>
        [MapTo("Canvas.sortingLayerID")]
        public _int SortingLayerId;

        /// <summary>
        /// Name of the canvas sorting layer.
        /// </summary>
        /// <d>Name of the canvas sorting layer.</d>
        [MapTo("Canvas.sortingLayerName")]
        public _string SortingLayerName;

        /// <summary>
        /// Override canvas sort order.
        /// </summary>
        /// <d>Boolean indicating if the sort order should be overriden (not inherited from parent canvas).</d>
        [MapTo("Canvas.overrideSorting")]
        public _bool OverrideSorting;

        /// <summary>
        /// Override pixel perfect.
        /// </summary>
        /// <d>Boolean indicating if the pixel perfect setting should be overriden (not inherited from parent canvas).</d>
        [MapTo("Canvas.overridePixelPerfect")]
        public _bool OverridePixelPerfect;

        /// <summary>
        /// Canvas component.
        /// </summary>
        /// <d>The canvas component renders the UI components using specified settings.</d>        
        public Canvas Canvas;

        #endregion

        #region CanvasScaler

        /// <summary>
        /// UI scale mode.
        /// </summary>
        /// <d>Determines how the children within the canvas are scaled.</d>
        [MapTo("CanvasScaler.uiScaleMode")]
        public _CanvasScaleMode UIScaleMode;

        /// <summary>
        /// Scale factor.
        /// </summary>
        /// <d>Scales all children within the canvas by this factor.</d>
        [MapTo("CanvasScaler.scaleFactor")]
        public _float ScaleFactor;

        /// <summary>
        /// Reference pixels per unit.
        /// </summary>
        /// <d>If a sprite has 'Pixels Per Unit' setting, one pixel in the sprite will cover one unit in the UI.</d>
        [MapTo("CanvasScaler.referencePixelsPerUnit")]
        public _float ReferencePixelsPerUnit;

        /// <summary>
        /// Dynamic pixels per unit.
        /// </summary>
        /// <d>Pixels per unit to use for dynamically generated bitmaps such as text.</d>
        [MapTo("CanvasScaler.dynamicPixelsPerUnit")]
        public _float DynamicPixelsPerUnit;
        
        /// <summary>
        /// Reference resolution.
        /// </summary>
        /// <d>The resolution the UI layout is designed for. If the screen resolution is larger, the UI will be scaled up, and if it’s smaller, the UI will be scaled down.</d>
        [MapTo("CanvasScaler.referenceResolution")]
        public _Vector2 ReferenceResolution;

        /// <summary>
        /// Match width or height or in between.
        /// </summary>
        /// <d>Scale the canvas area with the width as reference (0), the height as reference (1), or something in between (e.g. 0.75).</d>
        [MapTo("CanvasScaler.matchWidthOrHeight")]
        public _float MatchWidthOrHeight;

        /// <summary>
        /// Canvas scaler component.
        /// </summary>
        /// <d>The canvas scaler component is used for controlling the scale and pixel density of the children of the canvas.</d>
        public CanvasScaler CanvasScaler;

        #endregion

        #region GraphicRaycaster

        /// <summary>
        /// Indicates if reversed graphics should be ignored.
        /// </summary>
        /// <d>Tells the graphic raycaster to ignore graphical components facing away from the camera.</d>
        [MapTo("GraphicRaycaster.ignoreReversedGraphics")]
        public _bool IgnoreReversedGraphics;

        /// <summary>
        /// Blocking objects.
        /// </summary>
        /// <d>Indicates the types of objects that should block the raycasts.</d>
        [MapTo("GraphicRaycaster.blockingObjects")]
        public _BlockingObjects BlockingObjects;

        /// <summary>
        /// Graphic raycaster component.
        /// </summary>
        /// <d>The graphic raycaster components does raycasting against graphical components (such as images).</d>
        public GraphicRaycaster GraphicRaycaster;

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Canvas.renderMode = UnityEngine.RenderMode.ScreenSpaceOverlay;
            Canvas.pixelPerfect = false;
            Canvas.sortingOrder = 0;
            CanvasScaler.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ConstantPixelSize;
            CanvasScaler.scaleFactor = 1;
            CanvasScaler.referencePixelsPerUnit = 100;
            GraphicRaycaster.ignoreReversedGraphics = true;
            GraphicRaycaster.blockingObjects = UnityEngine.UI.GraphicRaycaster.BlockingObjects.None;
        }

        #endregion
    }
}
