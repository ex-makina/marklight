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
    /// Image view.
    /// </summary>
    /// <d>Used to display an image. Contains additional mappings to the image component.</d>
    [HideInPresenter]
    public class Image : UIView
    {
        #region Fields

        /// <summary>
        /// Alpha threshold for letting through events.
        /// </summary>
        /// <d>The alpha threshold specifying the minimum alpha a pixel must have for the event to be passed through.</d>
        [MapTo("ImageComponent.eventAlphaThreshold")]
        public _float EventAlphaThreshold;

        /// <summary>
        /// Image fill amount.
        /// </summary>
        /// <d>Amount of the Image shown when the Image.type is set to Image.Type.Filled.</d>
        [MapTo("ImageComponent.fillAmount")]
        public _float FillAmount;

        /// <summary>
        /// Indicates if center should be filled.
        /// </summary>
        /// <d>Boolean indicating whether or not to render the center of a Tiled or Sliced image.</d>
        [MapTo("ImageComponent.fillCenter")]
        public _bool FillCenter;

        /// <summary>
        /// Indicates if the image should be filled clockwise.
        /// </summary>
        /// <d>Boolean indicating whether the image should be filled clockwise (true) or counter-clockwise (false).</d>
        [MapTo("ImageComponent.fillClockwise")]
        public _bool FillClockwise;

        /// <summary>
        /// Image fill method.
        /// </summary>
        /// <d>Indicates what type of fill method should be used.</d>
        [MapTo("ImageComponent.fillMethod")]
        public _ImageFillMethod FillMethod;

        /// <summary>
        /// Image fill origin.
        /// </summary>
        /// <d>Controls the origin point of the Fill process. Value means different things with each fill method.</d>
        [MapTo("ImageComponent.fillOrigin")]
        public _int FillOrigin;

        /// <summary>
        /// Image override sprite.
        /// </summary>
        /// <d>Set an override sprite to be used for rendering. If set the override sprite is used instead of the regular image sprite.</d>
        [MapTo("ImageComponent.overrideSprite")]
        public _Sprite OverrideSprite;

        /// <summary>
        /// Preserve aspect ratio.
        /// </summary>
        /// <d>Indicates whether this image should preserve its Sprite aspect ratio.</d>
        [MapTo("ImageComponent.preserveAspect")]
        public _bool PreserveAspect;

        /// <summary>
        /// Image sprite.
        /// </summary>
        /// <d>The sprite that will be rendered.</d>
        [MapTo("ImageComponent.sprite", "BackgroundChanged")]
        public _Sprite Sprite;

        /// <summary>
        /// Image type.
        /// </summary>
        /// <d>Type of image sprite that is to be rendered.</d>
        [MapTo("ImageComponent.type")]
        public _ImageType Type;

        /// <summary>
        /// Image material.
        /// </summary>
        /// <d>Image material.</d>
        [MapTo("ImageComponent.material")]
        public _Material Material;

        /// <summary>
        /// Indicates if the image is maskable.
        /// </summary>
        /// <d>Indicates if the image graphic is to be maskable.</d>
        [MapTo("ImageComponent.maskable")]
        public _bool Maskable;

        /// <summary>
        /// Image color.
        /// </summary>
        /// <d>Image color.</d>
        [MapTo("ImageComponent.color", "BackgroundChanged")]
        public _Color Color;

        #endregion
    }
}
