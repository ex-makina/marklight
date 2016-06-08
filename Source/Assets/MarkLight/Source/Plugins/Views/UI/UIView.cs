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
    /// Base class for UI views.
    /// </summary>    
    /// <d>Base class for UI views. Has fields for doing layout such as Width, Height, Margin, Alignment, Offset, etc. and fields for rendering a background image.</d>
    [HideInPresenter]
    public class UIView : View
    {
        #region Fields

        /// <summary>
        /// The width of the view.
        /// </summary>
        /// <d>Specifies the width of the view either in pixels or percents.</d>
        [ChangeHandler("LayoutsChanged")]
        public _ElementSize Width;

        /// <summary>
        /// The height of the view.
        /// </summary>
        /// <d>Specifies the height of the view either in pixels or percents.</d>
        [ChangeHandler("LayoutsChanged")]
        public _ElementSize Height;

        /// <summary>
        /// Override width.
        /// </summary>
        /// <d>Used to override the layouting logic of inherited views and set the specified width.</d>
        [ChangeHandler("LayoutsChanged")]
        public _ElementSize OverrideWidth;

        /// <summary>
        /// Override height.
        /// </summary>
        /// <d>Used to override the layouting logic of inherited views and set the specified height.</d>
        [ChangeHandler("LayoutsChanged")]
        public _ElementSize OverrideHeight;

        /// <summary>
        /// View alignment.
        /// </summary>
        /// <d>Used to align the view relative to the layout parent region it resides in.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementAlignment Alignment;

        /// <summary>
        /// View margin
        /// </summary>
        /// <d>Determines the size of the content region relative the view's width and height. Adding margins to a view does not change its width or height.</d>
        [ChangeHandler("LayoutChanged")]
        public _ElementMargin Margin;

        /// <summary>
        /// View offset.
        /// </summary>
        /// <d>Determines the offset of the content region relative to the view's position.</d>
        [ChangeHandler("OffsetChanged")]
        public _ElementMargin Offset;

        /// <summary>
        /// View offset from parent.
        /// </summary>
        /// <d>Used by parent views to adjust the positioning of its children without affecting the internal offset of the children.</d>
        [ChangeHandler("OffsetChanged")]
        public _ElementMargin OffsetFromParent;

        /// <summary>
        /// View pivot position.
        /// </summary>
        /// <d>The normalized position that the view rect transform rotates around.</d>
        [MapTo("RectTransform.pivot")]
        public _Vector2 Pivot;

        /// <summary>
        /// Position, rotation and scale of the view.
        /// </summary>
        /// <d>The view rect transform is used to manipulate the position, rotation and scale of the view in relation to the layout parent view's transform or in world space. For most UIViews the transform manipulated indirectly through other view fields such as Width, Height, Margin, Offset, Alignment and through the UIView's internal layout logic.</d>
        public RectTransform RectTransform;
        
        /// <summary>
        /// Indicates when raycast should be blocked.
        /// </summary>
        /// <d>Enum indicating when raycasts should be blocked by the view.</d>
        [ChangeHandler("BackgroundChanged")]
        public _RaycastBlockMode RaycastBlockMode;

        /// <summary>
        /// Alpha value.
        /// </summary>
        /// <d>Can be used to adjust the alpha color of this view and all its children. E.g. used for fade in/out animations. Is separate from and different from the background color of the view as it affects the children as well.</d>
        [ChangeHandler("BackgroundChanged")]
        public _float Alpha;

        /// <summary>
        /// Indicate if the view is visible.
        /// </summary>
        /// <d>Can be used to adjust the visiblity of the view. If set to false the view is made invisible but unlike when deactivating the view, invisible views are still is active and takes up space.</d>
        [ChangeHandler("BackgroundChanged")]
        public _bool IsVisible;
       
        /// <summary>
        /// View sort index.
        /// </summary>
        /// <d>The sort index is used by views such as Group and List to sort its child views.</d>
        public _int SortIndex;

        /// <summary>
        /// Indicates if rect transform is updated.
        /// </summary>
        /// <d>If set to false the rect transform is not updated by the layout logic. It is used when layouting is done elsewhere.</d>
        public _bool UpdateRectTransform;

        /// <summary>
        /// Indicates if background is updated.
        /// </summary>
        /// <d>If set to false the background image and color is not updated by the view. Is used when the background updates is done elsewhere.</d>
        public _bool UpdateBackground;

        #region BackgroundImage

        /// <summary>
        /// Displays the background image.
        /// </summary>
        /// <d>The background image component is responsible for rendering the background image and sprite of the view.</d>
        public UnityEngine.UI.Image ImageComponent;

        /// <summary>
        /// Alpha threshold for letting through events.
        /// </summary>
        /// <d>The alpha threshold specifying the minimum alpha a pixel must have for the event to be passed through.</d>
        [MapTo("ImageComponent.eventAlphaThreshold")]
        public _float BackgroundImageEventAlphaThreshold;

        /// <summary>
        /// Background image fill amount.
        /// </summary>
        /// <d>Amount of the Image shown when the Image.type is set to Image.Type.Filled.</d>
        [MapTo("ImageComponent.fillAmount")]
        public _float BackgroundImageFillAmount;

        /// <summary>
        /// Indicates if center should be filled.
        /// </summary>
        /// <d>Boolean indicating whether or not to render the center of a Tiled or Sliced image.</d>
        [MapTo("ImageComponent.fillCenter")]
        public _bool BackgroundImageFillCenter;

        /// <summary>
        /// Indicates if the image should be filled clockwise.
        /// </summary>
        /// <d>Whether the Image should be filled clockwise (true) or counter-clockwise (false).</d>
        [MapTo("ImageComponent.fillClockwise")]
        public _bool BackgroundImageFillClockwise;

        /// <summary>
        /// Background image fill method.
        /// </summary>
        /// <d>Indicates what type of fill method should be used.</d>
        [MapTo("ImageComponent.fillMethod")]
        public _FillMethod BackgroundImageFillMethod;

        /// <summary>
        /// Background image fill origin.
        /// </summary>
        /// <d>Controls the origin point of the Fill process. Value means different things with each fill method.</d>
        [MapTo("ImageComponent.fillOrigin")]
        public _int BackgroundImageFillOrigin;

        /// <summary>
        /// Background image override sprite.
        /// </summary>
        /// <d>Set an override sprite to be used for rendering. If set the override sprite is used instead of the regular image sprite.</d>
        [MapTo("ImageComponent.overrideSprite")]
        public _Sprite BackgroundImageOverrideSprite;

        /// <summary>
        /// Preserve aspect ratio.
        /// </summary>
        /// <d>Indicates whether this image should preserve its Sprite aspect ratio.</d>
        [MapTo("ImageComponent.preserveAspect")]
        public _Sprite BackgroundImagePreserveAspect;

        /// <summary>
        /// Background image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered.</d>
        [MapTo("ImageComponent.sprite", "BackgroundChanged")]
        public _Sprite BackgroundImage;

        /// <summary>
        /// Type of background image.
        /// </summary>
        /// <d>The type of the image sprite that is to be rendered.</d>
        [MapTo("ImageComponent.type")]
        public _ImageType BackgroundImageType;

        /// <summary>
        /// Background image material.
        /// </summary>
        /// <d>Background image material.</d>
        [MapTo("ImageComponent.material")]
        public _Material BackgroundMaterial;

        /// <summary>
        /// Indicates if the image is maskable.
        /// </summary>
        /// <d>Indicates if the background image graphic is to be maskable.</d>
        [MapTo("ImageComponent.maskable")]
        public _bool BackgroundImageMaskable;

        /// <summary>
        /// Background color of the view.
        /// </summary>
        /// <d>Background color of the view.</d>
        [MapTo("ImageComponent.color", "BackgroundChanged")]
        public _Color BackgroundColor;

        #endregion

        /// <summary>
        /// Layout root.
        /// </summary>
        /// <d>A reference to the layout root of the UI views.</d>
        protected UserInterface _layoutRoot;
        
        protected CanvasGroup _canvasGroup;

        #endregion

        #region Methods

        /// <summary>
        /// Sets default values of the view.
        /// </summary>
        public override void SetDefaultValues()
        {
            base.SetDefaultValues();

            Alignment.DirectValue = MarkLight.ElementAlignment.Center;
            Width.DirectValue = new ElementSize(1.0f, ElementSizeUnit.Percents);
            Height.DirectValue = new ElementSize(1.0f, ElementSizeUnit.Percents);
            OverrideWidth.DirectValue = ElementSize.FromPixels(0);
            OverrideHeight.DirectValue = ElementSize.FromPixels(0);
            Margin.DirectValue = new ElementMargin();
            Offset.DirectValue = new ElementMargin();
            Alpha.DirectValue = 1;
            IsVisible.DirectValue = true;

            if (ImageComponent != null)
            {
                ImageComponent.color = Color.clear;
                ImageComponent.type = UnityEngine.UI.Image.Type.Simple;
            }

            IsActive.DirectValue = true;
            OffsetFromParent.DirectValue = new ElementMargin();
            SortIndex.DirectValue = 0;
            UpdateRectTransform.DirectValue = true;
            UpdateBackground.DirectValue = true;
        }

        /// <summary>
        /// Called when a field affecting the layout of the view (size and anchors) has changed.
        /// </summary>
        public override void LayoutChanged()
        {
            base.LayoutChanged();
            //Debug.Log(String.Format("{0}.LayoutChanged called", ViewTypeName));

            if (!UpdateRectTransform) 
                return; // rect transform is updated elsewhere

            RectTransformChanged();            
        }

        /// <summary>
        /// Called when the offset of the view has changed.
        /// </summary>
        public virtual void OffsetChanged()
        {
            if (!UpdateRectTransform)
                return; // rect transform is updated elsewhere

            RectTransformChanged();
        }

        /// <summary>
        /// Called when fields affecting the rect transform of the view has changed.
        /// </summary>
        public virtual void RectTransformChanged()
        {
            if (!UpdateRectTransform)
                return; // rect transform is updated elsewhere

            // update rectTransform
            // horizontal alignment and positioning
            var width = OverrideWidth.IsSet ? OverrideWidth : Width;
            var height = OverrideHeight.IsSet ? OverrideHeight : Height;

            float xMin = 0f;
            float xMax = 0f;
            float offsetMinX = 0f;
            float offsetMaxX = 0f;

            if (Alignment.Value.HasFlag(ElementAlignment.Left))
            {
                xMin = 0f;
                xMax = width.Value.Percent;
                offsetMinX = 0f;
                offsetMaxX = width.Value.Pixels;
            }
            else if (Alignment.Value.HasFlag(ElementAlignment.Right))
            {
                xMin = 1f - width.Value.Percent;
                xMax = 1f;
                offsetMinX = -width.Value.Pixels;
                offsetMaxX = 0f;
            }
            else
            {
                xMin = 0.5f - width.Value.Percent / 2f;
                xMax = 0.5f + width.Value.Percent / 2f;
                offsetMinX = -width.Value.Pixels / 2f;
                offsetMaxX = width.Value.Pixels / 2f;
            }

            // vertical alignment
            float yMin = 0f;
            float yMax = 0f;
            float offsetMinY = 0f;
            float offsetMaxY = 0f;

            if (Alignment.Value.HasFlag(ElementAlignment.Top))
            {
                yMin = 1f - height.Value.Percent;
                yMax = 1f;
                offsetMinY = -height.Value.Pixels;
                offsetMaxY = 0f;
            }
            else if (Alignment.Value.HasFlag(ElementAlignment.Bottom))
            {
                yMin = 0f;
                yMax = height.Value.Percent;
                offsetMinY = 0f;
                offsetMaxY = height.Value.Pixels;
            }
            else
            {
                yMin = 0.5f - height.Value.Percent / 2f;
                yMax = 0.5f + height.Value.Percent / 2f;
                offsetMinY = -height.Value.Pixels / 2f;
                offsetMaxY = height.Value.Pixels / 2f;
            }

            RectTransform.anchorMin = new Vector2(xMin, yMin);
            RectTransform.anchorMax = new Vector2(xMax, yMax);

            // positioning and margins
            RectTransform.offsetMin = new Vector2(
                offsetMinX + Margin.Value.Left.Pixels + Offset.Value.Left.Pixels - Offset.Value.Right.Pixels + OffsetFromParent.Value.Left.Pixels - OffsetFromParent.Value.Right.Pixels,
                offsetMinY + Margin.Value.Bottom.Pixels - Offset.Value.Top.Pixels + Offset.Value.Bottom.Pixels - OffsetFromParent.Value.Top.Pixels + OffsetFromParent.Value.Bottom.Pixels);
            RectTransform.offsetMax = new Vector2(
                offsetMaxX - Margin.Value.Right.Pixels + Offset.Value.Left.Pixels - Offset.Value.Right.Pixels + OffsetFromParent.Value.Left.Pixels - OffsetFromParent.Value.Right.Pixels,
                offsetMaxY - Margin.Value.Top.Pixels - Offset.Value.Top.Pixels + Offset.Value.Bottom.Pixels - OffsetFromParent.Value.Top.Pixels + OffsetFromParent.Value.Bottom.Pixels);

            RectTransform.anchoredPosition = new Vector2(
                RectTransform.offsetMin.x / 2.0f + RectTransform.offsetMax.x / 2.0f,
                RectTransform.offsetMin.y / 2.0f + RectTransform.offsetMax.y / 2.0f);
        }

        /// <summary>
        /// Called when a field affecting the behavior and visual appearance of the view has changed.
        /// </summary>
        public override void BehaviorChanged()
        {
            base.BehaviorChanged();
            //Debug.Log(String.Format("{0}.BehaviorChanged called", Name));
        }

        /// <summary>
        /// Called when fields affecting the background image/color of the view are changed.
        /// </summary>
        public virtual void BackgroundChanged()
        {
            if (!UpdateBackground)
                return; // background image is updated elsewhere

            if (Alpha.IsSet || IsVisible.IsSet || RaycastBlockMode.IsSet)
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = gameObject.GetComponent<CanvasGroup>();
                    if (_canvasGroup == null)
                    {
                        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                    }
                }

                _canvasGroup.alpha = IsVisible.Value ? Alpha.Value : 0;

                if (RaycastBlockMode == MarkLight.RaycastBlockMode.Always)
                {
                    _canvasGroup.blocksRaycasts = true;
                }
                else if (RaycastBlockMode == MarkLight.RaycastBlockMode.Never)
                {
                    _canvasGroup.blocksRaycasts = false;
                }
                else
                {
                    _canvasGroup.blocksRaycasts = (IsVisible && Alpha > 0);
                }

                _canvasGroup.interactable = IsVisible ?  Alpha > 0 : false;
            }
                        
            if (ImageComponent != null)
            {
                // set image color to white if sprite has been set but not color
                if (BackgroundImage.IsSet && !BackgroundColor.IsSet)
                {
                    ImageComponent.color = Color.white;
                }

                // if image color is clear disable image component
                ImageComponent.enabled = RaycastBlockMode == MarkLight.RaycastBlockMode.Always ? true : ImageComponent.color.a > 0;
            }
        }

        /// <summary>
        /// Gets local point in view from screen point (e.g. mouse position).
        /// </summary>
        public Vector2 GetLocalPoint(Vector2 screenPoint)
        {
            // get root canvas
            UnityEngine.Canvas canvas = LayoutRoot.Canvas;

            // for screen space overlay the camera should be null
            Camera worldCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

            // get local position of screen point
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(RectTransform, screenPoint, worldCamera, out pos);
            return pos;
        }

        /// <summary>
        /// Tests if mouse is over this view. 
        /// </summary>
        public bool ContainsMouse(Vector3 mousePosition, bool testChildren = false, bool ignoreFullScreenViews = false)
        {
            // get root canvas
            UnityEngine.Canvas canvas = LayoutRoot.Canvas;

            // for screen space overlay the camera should be null
            Camera worldCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            if (RectTransformUtility.RectangleContainsScreenPoint(this.RectTransform, mousePosition, worldCamera)
                && (!ignoreFullScreenViews || !IsFullScreen)
                && gameObject.activeInHierarchy
                && Alpha.Value > 0.99f)
            {
                return true;
            }

            if (testChildren)
            {
                foreach (var child in this)
                {
                    UIView view = child as UIView;
                    if (view == null)
                        continue;
                                            
                    if (view.ContainsMouse(mousePosition, testChildren, ignoreFullScreenViews))
                        return true;                    
                }
            }

            return false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets actual width of view in pixels. Useful when Width may be specified as percentage and you want actual pixel width.
        /// </summary>
        public float ActualWidth
        {
            get
            {
                return Mathf.Abs(RectTransform.rect.width);
            }
        }

        /// <summary>
        /// Gets actual height of view in pixels. Useful when Height may be specified as percentage and you want actual pixel height.
        /// </summary>
        public float ActualHeight
        {
            get
            {
                return Mathf.Abs(RectTransform.rect.height);
            }
        }

        /// <summary>
        /// Gets canvas group component.
        /// </summary>
        public CanvasGroup CanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = gameObject.GetComponent<CanvasGroup>();
                    if (_canvasGroup == null)
                    {
                        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                    }
                }

                return _canvasGroup;
            }
        }

        /// <summary>
        /// Gets boolean indicating if view takes up the entire screen.
        /// </summary>
        public bool IsFullScreen
        {
            get
            {
                return RectTransform.rect.width >= Screen.width && RectTransform.rect.height >= Screen.height;
            }
        }

        /// <summary>
        /// Gets layout root canvas.
        /// </summary>
        public UserInterface LayoutRoot
        {
            get
            {
                if (_layoutRoot == null)
                {
                    if (this is UserInterface)
                    {
                        _layoutRoot = this as UserInterface;
                    }
                    else
                    {
                        _layoutRoot = this.FindParent<UserInterface>();
                        if (_layoutRoot == null)
                        {
                            Utils.LogError("[MarkLight] {0}: LayoutRoot missing. All UIViews needs to be placed under a UserInterface root canvas.", GameObjectName);
                        }
                    }
                }

                return _layoutRoot;
            }
            set
            {
                _layoutRoot = value;
            }
        }

        #endregion
    }
}
