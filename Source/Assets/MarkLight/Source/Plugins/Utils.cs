#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Static class containing useful methods.
    /// </summary>
    public static class Utils
    {
        #region Fields

        private static System.Random _random;

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a static instance of the class.
        /// </summary>
        static Utils()
        {
            _random = new System.Random();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a random (named) color.
        /// </summary>
        public static Color GetRandomColor()
        {            
            return ColorValueConverter.ColorCodes.Values.ElementAt(_random.Next(ColorValueConverter.ColorCodes.Values.Count));
        }

        /// <summary>
        /// Extracts infromation from an exception and returns a readable error message.
        /// </summary>
        public static object GetError(Exception e)
        {
            var exception = e;
            if (e is TargetInvocationException)
            {
                if (e.InnerException != null)
                {
                    exception = e.InnerException;
                }
            }
                      
            return String.Format("{0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace);
        }

        #endregion
    }
}
