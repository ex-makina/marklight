#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Extension methods.
    /// </summary>
    public static class ExtensionMethods
    {
        #region Methods

        /// <summary>
        /// Traverses the view object tree and performs an action on each child until the action returns false.
        /// </summary>
        public static void DoUntil<T>(this View view, Func<T, bool> action, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            switch (traversalAlgorithm)
            {
                default:
                case TraversalAlgorithm.DepthFirst:
                    foreach (Transform child in view.gameObject.transform)
                    {
                        bool skipChild = false;
                        var childView = child.GetComponent<View>();
                        if (childView == null)
                        {
                            continue;
                        }

                        if (parent != null)
                        {
                            if (childView.Parent != parent)
                                skipChild = true;
                        }

                        if (!skipChild)
                        {
                            var component = child.GetComponent<T>();
                            if (component != null)
                            {
                                var result = action(component);
                                if (!result)
                                {
                                    // done traversing
                                    return;
                                }
                            }
                        }

                        if (recursive)
                        {
                            childView.DoUntil<T>(action, recursive, parent, traversalAlgorithm);
                        }
                    }
                    break;

                case TraversalAlgorithm.BreadthFirst:
                    Queue<View> queue = new Queue<View>();
                    foreach (Transform child in view.gameObject.transform)
                    {
                        bool skipChild = false;
                        var childView = child.GetComponent<View>();
                        if (childView == null)
                        {
                            continue;
                        }

                        if (parent != null)
                        {
                            if (childView.Parent != parent.gameObject)
                                skipChild = true;
                        }

                        if (!skipChild)
                        {
                            var component = child.GetComponent<T>();
                            if (component != null)
                            {
                                var result = action(component);
                                if (!result)
                                {
                                    // done traversing
                                    return;
                                }
                            }
                        }

                        if (recursive)
                        {
                            // add children to queue
                            queue.Enqueue(childView);
                        }
                    }

                    foreach (var queuedView in queue)
                    {
                        queuedView.DoUntil<T>(action, recursive, parent, traversalAlgorithm);
                    }
                    break;

                case TraversalAlgorithm.ReverseDepthFirst:
                    foreach (Transform child in view.gameObject.transform)
                    {
                        var childView = child.GetComponent<View>();
                        if (childView == null)
                        {
                            continue;
                        }

                        if (recursive)
                        {
                            childView.DoUntil<T>(action, recursive, parent, traversalAlgorithm);
                        }

                        if (parent != null)
                        {
                            if (childView.Parent != parent.gameObject)
                                continue;
                        }

                        var component = child.GetComponent<T>();
                        if (component != null)
                        {
                            var result = action(component);
                            if (!result)
                            {
                                // done traversing
                                return;
                            }
                        }
                    }
                    break;

                case TraversalAlgorithm.ReverseBreadthFirst:
                    Stack<T> componentStack = new Stack<T>();
                    Stack<View> childStack = new Stack<View>();
                    foreach (Transform child in view.gameObject.transform)
                    {
                        var childView = child.GetComponent<View>();
                        if (childView == null)
                        {
                            continue;
                        }

                        if (recursive)
                        {
                            childStack.Push(childView);
                        }

                        if (parent != null)
                        {
                            if (childView.Parent != parent.gameObject)
                                continue;
                        }

                        var component = child.GetComponent<T>();
                        if (component != null)
                        {
                            componentStack.Push(component);
                        }
                    }

                    foreach (var childStackView in childStack)
                    {
                        childStackView.DoUntil<T>(action, recursive, parent, traversalAlgorithm);
                    }

                    foreach (T component in componentStack)
                    {
                        var result = action(component);
                        if (!result)
                        {
                            // done traversing
                            return;
                        }
                    }

                    break;
            }
        }

        /// <summary>
        /// Traverses the view object tree and performs an action on each child until the action returns false.
        /// </summary>
        public static void ForEachChild<T>(this View view, Action<T> action, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            view.DoUntil<T>(x => { action(x); return true; }, recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Traverses the view object tree and performs an action on this view and its children until the action returns false.
        /// </summary>
        public static void ForThisAndEachChild<T>(this View view, Action<T> action, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            var thisView = view.gameObject.GetComponent<T>();
            if (thisView != null)
            {
                action(thisView);
            }
            view.ForEachChild<T>(action, recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Traverses the view object tree and performs an action on each child until the action returns false.
        /// </summary>
        public static void ForEachChild<T>(this GameObject gameObject, Action<T> action, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            var view = gameObject.GetComponent<View>();
            if (view != null)
            {
                view.ForEachChild<T>(action, recursive, parent, traversalAlgorithm);
            }
        }

        /// <summary>
        /// Traverses the view object tree and performs an action on each child until the action returns false.
        /// </summary>
        public static void ForThisAndEachChild<T>(this GameObject gameObject, Action<T> action, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            var view = gameObject.GetComponent<T>();
            if (view != null)
            {
                action(view);
                view.ForEachChild<T>(action, recursive, parent, traversalAlgorithm);
            }
        }

        /// <summary>
        /// Traverses the view object tree and returns the first view that matches the predicate.
        /// </summary>
        public static T Find<T>(this View view, Predicate<T> predicate, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            T result = null;
            view.DoUntil<T>(x =>
            {
                if (predicate(x))
                {
                    result = x;
                    return false;
                }
                return true;
            }, recursive, parent, traversalAlgorithm);
            return result;
        }

        /// <summary>
        /// Returns first view of type T found.
        /// </summary>
        public static T Find<T>(this View view, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            return view.Find<T>(x => true, recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Returns first view of type T found.
        /// </summary>
        public static T Find<T>(this GameObject gameObject, Predicate<T> predicate, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            var view = gameObject.GetComponent<View>();
            if (view == null)
            {
                return null;
            }

            return view.Find<T>(predicate, recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Returns first view of type T found.
        /// </summary>
        public static T Find<T>(this GameObject gameObject, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            var view = gameObject.GetComponent<View>();
            if (view == null)
            {
                return null;
            }

            return view.Find<T>(x => true, recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Returns first view of type T with the specified ID.
        /// </summary>
        public static T Find<T>(this View view, string id, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            return view.Find<T>(x => String.Equals(x.Id, id, StringComparison.OrdinalIgnoreCase), recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Returns first ascendant of type T found that matches the predicate.
        /// </summary>
        public static T FindParent<T>(this View view, Predicate<T> predicate) where T : View
        {
            var parent = view.LayoutParent;
            if (parent == null)
            {
                return null;
            }
            else if (parent is T && predicate(parent as T))
            {
                return parent as T;
            }
            else
            {
                return parent.FindParent(predicate);
            }
        }

        /// <summary>
        /// Returns first ascendant of type T found.
        /// </summary>
        public static T FindParent<T>(this View view) where T : View
        {
            return view.FindParent<T>(x => true);
        }

        /// <summary>
        /// Performs an action on all ascendants of a view.
        /// </summary>
        public static void ForEachParent<T>(this View view, Action<T> action) where T : View
        {
            var parent = view.transform.parent;
            if (parent == null)
                return;

            var component = parent.GetComponent<T>();
            if (component != null)
            {
                action(component);
            }

            parent.gameObject.ForEachParent(action);
        }

        /// <summary>
        /// Performs an action on all ascendants of a view.
        /// </summary>
        public static void ForEachParent<T>(this GameObject gameObject, Action<T> action) where T : View
        {
            var view = gameObject.GetComponent<View>();
            if (view != null)
            {
                view.ForEachParent<T>(action);
            }
        }

        /// <summary>
        /// Performs an action on this view and all its ascendants.
        /// </summary>
        public static void ForThisAndEachParent<T>(this GameObject gameObject, Action<T> action) where T : View
        {
            var view = gameObject.GetComponent<T>();
            if (view != null)
            {
                action(view);
            }

            gameObject.ForEachParent<T>(action);
        }

        /// <summary>
        /// Performs an action on this view and all its ascendants.
        /// </summary>
        public static void ForThisAndEachParent<T>(this View view, Action<T> action) where T : View
        {
            var thisView = view.gameObject.GetComponent<T>();
            if (thisView != null)
            {
                action(thisView);
            }
            view.ForEachParent<T>(action);
        }

        /// <summary>
        /// Gets a list of all descendants. 
        /// </summary>
        public static List<T> GetChildren<T>(this View view, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            return view.GetChildren<T>(x => true, recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Gets a list of all descendants matching the predicate. 
        /// </summary>
        public static List<T> GetChildren<T>(this View view, Func<T, bool> predicate = null, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            var children = new List<T>();
            if (predicate == null)
            {
                predicate = x => true;
            }

            view.ForEachChild<T>(x =>
            {
                if (predicate(x))
                {
                    children.Add(x);
                }
            }, recursive, parent, traversalAlgorithm);

            return children;
        }

        /// <summary>
        /// Gets a list of all descendants matching the predicate. 
        /// </summary>
        public static List<T> GetChildren<T>(this GameObject gameObject, Func<T, bool> predicate = null, bool recursive = true, View parent = null, TraversalAlgorithm traversalAlgorithm = TraversalAlgorithm.DepthFirst) where T : View
        {
            var view = gameObject.GetComponent<View>();
            if (view == null)
            {
                return new List<T>();
            }

            return view.GetChildren<T>(predicate, recursive, parent, traversalAlgorithm);
        }

        /// <summary>
        /// Gets child at index.
        /// </summary>
        public static View GetChild(this View view, int index, bool countOnlyActive = false)
        {
            if (!countOnlyActive)
            {
                var child = view.gameObject.transform.GetChild(index);
                return child.GetComponent<View>();
            }

            int i = 0;
            foreach (Transform child in view.gameObject.transform)
            {
                var childView = child.GetComponent<View>();
                if (childView == null || !childView.IsActive)
                {
                    continue;
                }

                if (i == index)
                {
                    return childView;
                }

                ++i;
            }

            return null;
        }

        /// <summary>
        /// Destroys a view.
        /// </summary>
        public static void Destroy(this View view, bool immediate = false)
        {
            view.IsDestroyed.DirectValue = true;
            if (Application.isPlaying && !immediate)
            {
                GameObject.Destroy(view.gameObject);
            }
            else
            {
                GameObject.DestroyImmediate(view.gameObject);
            }
        }

        /// <summary>
        /// Destroy a view or moves it back into view pool.
        /// </summary>
        public static void Destroy(this View view, ViewPool viewPool, bool immediate = false)
        {
            if (viewPool == null || viewPool.IsFull)
            {
                view.Destroy(immediate);
                return;
            }

            // move view into view pool
            viewPool.InsertView(view);
        }

        /// <summary>
        /// Destroys all children of a view.
        /// </summary>
        public static void DestroyChildren(this View view, bool immediate = false)
        {
            int childCount = view.transform.childCount;
            for (int i = childCount - 1; i >= 0; --i)
            {
                var go = view.transform.GetChild(i).gameObject;
                var childView = go.GetComponent<View>();
                if (childView != null)
                {
                    childView.IsDestroyed.DirectValue = true;
                }

                if (Application.isPlaying && !immediate)
                {
                    GameObject.Destroy(go);
                }
                else
                {
                    GameObject.DestroyImmediate(go);
                }
            }
        }

        /// <summary>
        /// Checks if a flag enum has a flag value set.
        /// </summary>
        public static bool HasFlag(this Enum variable, Enum value)
        {
            // check if from the same type
            if (variable.GetType() != value.GetType())
            {
                Debug.LogError("[MarkLight] The checked flag is not from the same type as the checked variable.");
                return false;
            }

            Convert.ToUInt64(value);
            ulong num = Convert.ToUInt64(value);
            ulong num2 = Convert.ToUInt64(variable);
            return (num2 & num) == num;
        }

        /// <summary>
        /// Clamps a value to specified range [min, max].
        /// </summary>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        /// <summary>
        /// Gets value from dictionary and returns null if it doesn't exist.
        /// </summary>
        public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
            {
                return default(TValue);
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Resets a rect transform.
        /// </summary>
        public static void Reset(this RectTransform rectTransform)
        {
            rectTransform.localScale = new Vector3(1f, 1f, 1f);
            rectTransform.localPosition = new Vector3(0f, 0f, 0f);
            rectTransform.anchorMin = new Vector2(0f, 0f);
            rectTransform.anchorMax = new Vector2(1f, 1f);
            rectTransform.pivot = new Vector2(0.5f, 0.5f);
            rectTransform.offsetMin = new Vector2(0.0f, 0.0f);
            rectTransform.offsetMax = new Vector2(0.0f, 0.0f);
        }

        /// <summary>
        /// Calculates mouse screen position.
        /// </summary>
        public static Vector2 GetMouseScreenPosition(this UnityEngine.Canvas canvas, Vector3 mousePosition)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out pos);
            Vector2 mouseScreenPosition = canvas.transform.TransformPoint(pos);
            return mouseScreenPosition;
        }

        /// <summary>
        /// Calculates mouse screen position.
        /// </summary>
        public static Vector2 GetMouseScreenPosition(this UnityEngine.Canvas canvas, Vector2 mousePosition)
        {
            Vector2 pos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out pos);
            Vector2 mouseScreenPosition = canvas.transform.TransformPoint(pos);
            return mouseScreenPosition;
        }

        /// <summary>
        /// Removes all whitespace from a string.
        /// </summary>
        public static string RemoveWhitespace(this string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Gets view field info from a type.
        /// </summary>
        public static MemberInfo GetFieldInfo(this Type type, string field, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance)
        {
            var fieldInfo = type.GetField(field, bindingFlags);
            if (fieldInfo != null)
                return fieldInfo;

            var propertyInfo = type.GetProperty(field, bindingFlags);
            return propertyInfo;
        }

        /// <summary>
        /// Gets view field type from view field info.
        /// </summary>
        public static Type GetFieldType(this MemberInfo memberInfo)
        {
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }

            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            return null;
        }

        /// <summary>
        /// Gets value from a view field.
        /// </summary>
        public static object GetFieldValue(this MemberInfo memberInfo, object typeObject)
        {
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
                return fieldInfo.GetValue(typeObject);

            var propertyInfo = memberInfo as PropertyInfo;
            return propertyInfo.GetValue(typeObject, null);
        }

        /// <summary>
        /// Sets view field value.
        /// </summary>
        public static void SetFieldValue(this MemberInfo memberInfo, object typeObject, object value)
        {
            var fieldInfo = memberInfo as FieldInfo;
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(typeObject, value);
                return;
            }

            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(typeObject, value, null);
            }
        }

        /// <summary>
        /// Adds range of items to a hashset.
        /// </summary>
        public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                hashSet.Add(item);
            }
        }

#if !UNITY_4_6 && !UNITY_5_0 && !UNITY_5_1
        /// <summary>
        /// Converts panel scrollbar visibility to unity scrollrect scrollbar visibility.
        /// </summary>
        public static UnityEngine.UI.ScrollRect.ScrollbarVisibility ToScrollRectVisibility(this PanelScrollbarVisibility visibility)
        {
            switch (visibility)
            {
                case PanelScrollbarVisibility.Permanent:
                    return UnityEngine.UI.ScrollRect.ScrollbarVisibility.Permanent;
                default:
                case PanelScrollbarVisibility.AutoHide:
                case PanelScrollbarVisibility.Hidden:
                case PanelScrollbarVisibility.Remove:
                    return UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHide;
                case PanelScrollbarVisibility.AutoHideAndExpandViewport:
                    return UnityEngine.UI.ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
            }
        }
#endif

        /// <summary>
        /// Converts content alignment to pivot.
        /// </summary>
        public static Vector2 ToPivot(this ElementAlignment alignment)
        {
            switch (alignment)
            {
                default:
                case ElementAlignment.Center:
                    return new Vector2(0.5f, 0.5f);
                case ElementAlignment.Left:
                    return new Vector2(0, 0.5f);
                case ElementAlignment.Top:
                    return new Vector2(0.5f, 1);
                case ElementAlignment.Right:
                    return new Vector2(1, 0.5f);
                case ElementAlignment.Bottom:
                    return new Vector2(0.5f, 0);
                case ElementAlignment.TopLeft:
                    return new Vector2(0, 1);
                case ElementAlignment.TopRight:
                    return new Vector2(1, 1);
                case ElementAlignment.BottomLeft:
                    return new Vector2(0, 0);
                case ElementAlignment.BottomRight:
                    return new Vector2(1, 0);
            }
        }

        /// <summary>
        /// Replaces the first occurance of a string.
        /// </summary>
        public static string ReplaceFirst(this string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }

        #endregion
    }
}
