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
        #region Methods

        /// <summary>
        /// Gets all types derived from specified base type.
        /// </summary>
        public static IEnumerable<Type> FindDerivedTypes(Type baseType)
        {
            var derivedTypes = new List<Type>();
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
        
        #endregion
    }
}