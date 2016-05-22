#region Using Statements
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{   
    [Serializable]
    public class _float : ViewField<float>
    {
        public static implicit operator float (_float value) { return value.Value; }
    }

    [Serializable]
    public class _string : ViewField<string>
    {
        public static implicit operator string (_string value) { return value.Value; }
    }

    [Serializable]
    public class _int : ViewField<int>
    {
        public static implicit operator int (_int value) { return value.Value; }
    }

    [Serializable]
    public class _bool : ViewField<bool>
    {
        public static implicit operator bool(_bool value) { return value.Value; }
    }

    [Serializable]
    public class _char : ViewField<char>
    {
        public static implicit operator char(_char value) { return value.Value; }
    }

    [Serializable]
    public class _Color : ViewField<Color>
    {
        public static implicit operator Color(_Color value) { return value.Value; }
    }

    [Serializable]
    public class _ElementSize : ViewField<ElementSize> { }

    [Serializable]
    public class _Font : ViewField<Font> { }

    [Serializable]
    public class _ElementMargin : ViewField<ElementMargin> { }

    [Serializable]
    public class _Material : ViewField<Material> { }

    [Serializable]
    public class _Quaternion : ViewField<Quaternion> { }

    [Serializable]
    public class _Sprite : ViewField<Sprite> { }

    [Serializable]
    public class _Vector2 : ViewField<Vector2> { }

    [Serializable]
    public class _Vector3 : ViewField<Vector3> { }

    [Serializable]
    public class _Vector4 : ViewField<Vector4> { }

    [Serializable]
    public class _ElementAlignment : ViewField<ElementAlignment>
    {
        public static implicit operator ElementAlignment(_ElementAlignment value) { return value.Value; }
    }

    [Serializable]
    public class _ElementOrientation : ViewField<ElementOrientation>
    {
        public static implicit operator ElementOrientation(_ElementOrientation value) { return value.Value; }
    }

    [Serializable]
    public class _AdjustToText : ViewField<AdjustToText>
    {
        public static implicit operator AdjustToText(_AdjustToText value) { return value.Value; }
    }

    [Serializable]
    public class _FontStyle : ViewField<FontStyle>
    {
        public static implicit operator FontStyle(_FontStyle value) { return value.Value; }
    }

    [Serializable]
    public class _HorizontalWrapMode : ViewField<HorizontalWrapMode>
    {
        public static implicit operator HorizontalWrapMode(_HorizontalWrapMode value) { return value.Value; }
    }

    [Serializable]
    public class _VerticalWrapMode : ViewField<VerticalWrapMode>
    {
        public static implicit operator VerticalWrapMode(_VerticalWrapMode value) { return value.Value; }
    }

    [Serializable]
    public class _FillMethod : ViewField<UnityEngine.UI.Image.FillMethod>
    {
        public static implicit operator UnityEngine.UI.Image.FillMethod(_FillMethod value) { return value.Value; }
    }

    [Serializable]
    public class _ImageType : ViewField<UnityEngine.UI.Image.Type>
    {
        public static implicit operator UnityEngine.UI.Image.Type(_ImageType value) { return value.Value; }
    }

    [Serializable]
    public class _ElementSortDirection : ViewField<ElementSortDirection>
    {
        public static implicit operator ElementSortDirection(_ElementSortDirection value) { return value.Value; }
    }

    [Serializable]
    public class _ImageFillMethod : ViewField<UnityEngine.UI.Image.FillMethod>
    {
        public static implicit operator UnityEngine.UI.Image.FillMethod(_ImageFillMethod value) { return value.Value; }
    }

    [Serializable]
    public class _InputFieldCharacterValidation : ViewField<UnityEngine.UI.InputField.CharacterValidation>
    {
        public static implicit operator UnityEngine.UI.InputField.CharacterValidation(_InputFieldCharacterValidation value) { return value.Value; }
    }

    [Serializable]
    public class _InputFieldContentType : ViewField<UnityEngine.UI.InputField.ContentType>
    {
        public static implicit operator UnityEngine.UI.InputField.ContentType(_InputFieldContentType value) { return value.Value; }
    }

    [Serializable]
    public class _InputFieldInputType : ViewField<UnityEngine.UI.InputField.InputType>
    {
        public static implicit operator UnityEngine.UI.InputField.InputType(_InputFieldInputType value) { return value.Value; }
    }

    [Serializable]
    public class _TouchScreenKeyboardType : ViewField<UnityEngine.TouchScreenKeyboardType>
    {
        public static implicit operator UnityEngine.TouchScreenKeyboardType(_TouchScreenKeyboardType value) { return value.Value; }
    }

    [Serializable]
    public class _InputFieldLineType : ViewField<UnityEngine.UI.InputField.LineType>
    {
        public static implicit operator UnityEngine.UI.InputField.LineType(_InputFieldLineType value) { return value.Value; }
    }

    [Serializable]
    public class _ScrollbarDirection : ViewField<UnityEngine.UI.Scrollbar.Direction>
    {
        public static implicit operator UnityEngine.UI.Scrollbar.Direction(_ScrollbarDirection value) { return value.Value; }
    }

#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
    [Serializable]
    public class _ScrollbarVisibility : ViewField<UnityEngine.UI.ScrollRect.ScrollbarVisibility>
    {
        public static implicit operator UnityEngine.UI.ScrollRect.ScrollbarVisibility(_ScrollbarVisibility value) { return value.Value; }
    }
#endif

    [Serializable]
    public class _PanelScrollbarVisibility : ViewField<PanelScrollbarVisibility>
    {
        public static implicit operator PanelScrollbarVisibility(_PanelScrollbarVisibility value) { return value.Value; }
    }

    [Serializable]
    public class _ScrollRectMovementType : ViewField<UnityEngine.UI.ScrollRect.MovementType>
    {
        public static implicit operator UnityEngine.UI.ScrollRect.MovementType(_ScrollRectMovementType value) { return value.Value; }
    }

    [Serializable]
    public class _RectTransformComponent : ViewField<RectTransform>
    {
        public static implicit operator RectTransform(_RectTransformComponent value) { return value.Value; }
    }

    [Serializable]
    public class _ScrollbarComponent : ViewField<UnityEngine.UI.Scrollbar>
    {
        public static implicit operator UnityEngine.UI.Scrollbar(_ScrollbarComponent value) { return value.Value; }
    }

    [Serializable]
    public class _GraphicRenderMode : ViewField<UnityEngine.RenderMode>
    {
        public static implicit operator UnityEngine.RenderMode(_GraphicRenderMode value) { return value.Value; }
    }

    [Serializable]
    public class _CameraComponent : ViewField<UnityEngine.Camera>
    {
        public static implicit operator UnityEngine.Camera(_CameraComponent value) { return value.Value; }
    }

    [Serializable]
    public class _CanvasScaleMode : ViewField<UnityEngine.UI.CanvasScaler.ScaleMode>
    {
        public static implicit operator UnityEngine.UI.CanvasScaler.ScaleMode(_CanvasScaleMode value) { return value.Value; }
    }

    [Serializable]
    public class _BlockingObjects : ViewField<UnityEngine.UI.GraphicRaycaster.BlockingObjects>
    {
        public static implicit operator UnityEngine.UI.GraphicRaycaster.BlockingObjects(_BlockingObjects value) { return value.Value; }
    }

    [Serializable]
    public class _GameObject : ViewField<UnityEngine.GameObject>
    {
        public static implicit operator UnityEngine.GameObject(_GameObject value) { return value.Value; }
    }

    [Serializable]
    public class _HideFlags : ViewField<UnityEngine.HideFlags>
    {
        public static implicit operator UnityEngine.HideFlags(_HideFlags value) { return value.Value; }
    }

    [Serializable]
    public class _OverflowMode : ViewField<OverflowMode>
    {
        public static implicit operator OverflowMode(_OverflowMode value) { return value.Value; }
    }

    [Serializable]
    public class _RaycastBlockMode : ViewField<RaycastBlockMode>
    {
        public static implicit operator RaycastBlockMode(_RaycastBlockMode value) { return value.Value; }
    }

    [Serializable]
    public class _Mesh : ViewField<Mesh>
    {
        public static implicit operator Mesh(_Mesh value) { return value.Value; }
    }

    [Serializable]
    public class _object : ViewField<object> { }

    [Serializable]
    public class _IObservableList : ViewField<IObservableList> { }

    [Serializable]
    public class _GenericObservableList : ViewField<GenericObservableList> { }
}
