#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Collections;
using System.Reflection;
using System.Linq.Expressions;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Helper methods for finding and instantiating objects through reflection.
    /// </summary>
    public static class TypeHelper
    {
        #region Fields

        private static List<Type> _scriptAssemblyTypes;
        private static Dictionary<Type, Func<ViewFieldBase>> _viewFieldFactory;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a static instance of the class.
        /// </summary>
        static TypeHelper()
        {
            _viewFieldFactory = new Dictionary<Type, Func<ViewFieldBase>>
            {
                { typeof(_float), () => new _float() },
                { typeof(_string), () => new _string() },
                { typeof(_int), () => new _int() },
                { typeof(_bool), () => new _bool() },
                { typeof(_char), () => new _char() },
                { typeof(_Color), () => new _Color() },
                { typeof(_ElementSize), () => new _ElementSize() },
                { typeof(_Font), () => new _Font() },
                { typeof(_ElementMargin), () => new _ElementMargin() },
                { typeof(_Material), () => new _Material() },
                { typeof(_Quaternion), () => new _Quaternion() },
                { typeof(_Sprite), () => new _Sprite() },
                { typeof(_Vector2), () => new _Vector2() },
                { typeof(_Vector3), () => new _Vector3() },
                { typeof(_Vector4), () => new _Vector4() },
                { typeof(_ElementAlignment), () => new _ElementAlignment() },
                { typeof(_ElementOrientation), () => new _ElementOrientation() },
                { typeof(_AdjustToText), () => new _AdjustToText() },
                { typeof(_FontStyle), () => new _FontStyle() },
                { typeof(_HorizontalWrapMode), () => new _HorizontalWrapMode() },
                { typeof(_VerticalWrapMode), () => new _VerticalWrapMode() },
                { typeof(_FillMethod), () => new _FillMethod() },
                { typeof(_ImageType), () => new _ImageType() },
                { typeof(_ElementSortDirection), () => new _ElementSortDirection() },
                { typeof(_ImageFillMethod), () => new _ImageFillMethod() },
                { typeof(_InputFieldCharacterValidation), () => new _InputFieldCharacterValidation() },
                { typeof(_InputFieldContentType), () => new _InputFieldContentType() },
                { typeof(_InputFieldInputType), () => new _InputFieldInputType() },
                { typeof(_TouchScreenKeyboardType), () => new _TouchScreenKeyboardType() },
                { typeof(_InputFieldLineType), () => new _InputFieldLineType() },
                { typeof(_ScrollbarDirection), () => new _ScrollbarDirection() },
#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
                { typeof(_ScrollbarVisibility), () => new _ScrollbarVisibility() },
#endif
                { typeof(_PanelScrollbarVisibility), () => new _PanelScrollbarVisibility() },
                { typeof(_ScrollRectMovementType), () => new _ScrollRectMovementType() },
                { typeof(_RectTransformComponent), () => new _RectTransformComponent() },
                { typeof(_ScrollbarComponent), () => new _ScrollbarComponent() },
                { typeof(_GraphicRenderMode), () => new _GraphicRenderMode() },
                { typeof(_CameraComponent), () => new _CameraComponent() },
                { typeof(_CanvasScaleMode), () => new _CanvasScaleMode() },
                { typeof(_BlockingObjects), () => new _BlockingObjects() },
                { typeof(_GameObject), () => new _GameObject() },
                { typeof(_HideFlags), () => new _HideFlags() },
                { typeof(_OverflowMode), () => new _OverflowMode() },
                { typeof(_RaycastBlockMode), () => new _RaycastBlockMode() },
                { typeof(_Mesh), () => new _Mesh() },
                { typeof(_object), () => new _object() },
                { typeof(_IObservableList), () => new _IObservableList() },
                { typeof(_GenericObservableList), () => new _GenericObservableList() },
                { typeof(ViewFieldBase), () => new ViewFieldBase() }
            };
        }

        #endregion

        #region Methods                

        /// <summary>
        /// Gets all types derived from specified base type.
        /// </summary>
        public static IEnumerable<Type> FindDerivedTypes(Type baseType)
        {
            var derivedTypes = new List<Type>();
            if (_scriptAssemblyTypes != null)
            {
                foreach (var type in _scriptAssemblyTypes)
                {
                    if (baseType.IsAssignableFrom(type))
                    {
                        derivedTypes.Add(type);
                    }
                }
                return derivedTypes;
            }            

            // look in assembly csharp only
            var scriptAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.GetName().Name.StartsWith("Assembly-")).ToList();
            if (scriptAssemblies.Count > 0)
            {
                _scriptAssemblyTypes = new List<Type>();
                foreach (var assembly in scriptAssemblies)
                {
                    _scriptAssemblyTypes.AddRange(assembly.GetLoadableTypes().ToList());                    
                }

                foreach (var type in _scriptAssemblyTypes)
                {
                    if (baseType.IsAssignableFrom(type))
                    {
                        derivedTypes.Add(type);
                    }
                }
            }
            else
            {
                // look in all assemblies
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (var type in assembly.GetLoadableTypes())
                    {
                        try
                        {
                            if (baseType.IsAssignableFrom(type))
                            {
                                derivedTypes.Add(type);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return derivedTypes;
        }

        /// <summary>
        /// Extension method for getting loadable types from an assembly.
        /// </summary>
        public static IEnumerable<Type> GetLoadableTypes(this Assembly assembly)
        {
            if (assembly == null)
            {
                return Enumerable.Empty<Type>();
            }

            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types.Where(t => t != null);
            }
        }

        /// <summary>
        /// Instiantiates a type.
        /// </summary>
        public static object CreateInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Helper method for generating dependency field activators.
        /// </summary>
        public static void PrintDependencyFields()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var derivedType in FindDerivedTypes(typeof(ViewFieldBase)))
            {
                sb.AppendLine(String.Format("{{ typeof({0}), () => new {0}() }},", derivedType.Name));
            }

            Utils.Log(sb.ToString());
        }
        
        /// <summary>
        /// Creates a dependency field from type.
        /// </summary>
        public static ViewFieldBase CreateViewField(Type type)
        {
            Func<ViewFieldBase> activator;
            if (_viewFieldFactory.TryGetValue(type, out activator))
            {
                return activator();
            }
            else
            {
                return TypeHelper.CreateInstance(type) as ViewFieldBase;
            }
        }

        #endregion
    }
}