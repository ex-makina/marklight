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

        public static bool SuppressLogging = false;
        public static System.Diagnostics.Stopwatch Stopwatch;
        public static string ErrorMessage = string.Empty;
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

        /// <summary>
        /// Logs error. 
        /// </summary>
        public static void Log(string message)
        {
            if (SuppressLogging)
                return;

            Debug.Log(message);
        }

        /// <summary>
        /// Logs error with format string.
        /// </summary>
        public static void Log(string format, params object[] args)
        {
            if (SuppressLogging)
                return;

            try
            {
                Debug.Log(String.Format(format, args));
            }
            catch
            {
                Debug.Log(format);
            }
        }

        /// <summary>
        /// Logs error. 
        /// </summary>
        public static void LogError(string error)
        {
            if (SuppressLogging)
                return;

            Debug.LogError(error);
        }

        /// <summary>
        /// Logs error with format string.
        /// </summary>
        public static void LogError(string format, params object[] args)
        {
            if (SuppressLogging)
                return;

            try
            {
                Debug.LogError(String.Format(format, args));
            }
            catch
            {
                Debug.LogError(format);
            }
        }

        /// <summary>
        /// Logs warning. 
        /// </summary>
        public static void LogWarning(string warning)
        {
            if (SuppressLogging)
                return;

            Debug.LogWarning(warning);
        }

        /// <summary>
        /// Logs warning with format string.
        /// </summary>
        public static void LogWarning(string format, params object[] args)
        {
            if (SuppressLogging)
                return;

            try
            {
                Utils.LogWarning(String.Format(format, args));
            }
            catch
            {
                Debug.LogWarning(format);
            }
        }

        /// <summary>
        /// Starts a stopwatch timer. Used for logging performance.
        /// </summary>
        public static void StartTimer()
        {
            Stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        /// <summary>
        /// Logs time elapsed on the timer. Used for logging performance. 
        /// </summary>
        public static void LogTimer()
        {
            Log("Time elapsed: {0}", Stopwatch.ElapsedMilliseconds);
        }

        /// <summary>
        /// Gets bool inidcating if the number is odd.
        /// </summary>
        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }

        /// <summary>
        /// Gets bool inidcating if the number is even.
        /// </summary>
        public static bool IsEven(int value)
        {
            return value % 2 == 0;
        }

        #endregion
    }
}
