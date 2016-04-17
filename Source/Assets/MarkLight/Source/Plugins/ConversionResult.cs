#region Using Statements
using System;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Contains the result of a value conversion.
    /// </summary>
    public class ConversionResult
    {
        #region Fields
                
        private object _convertedValue;
        private bool _success;
        private string _errorMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ConversionResult()
        {
            _success = true;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ConversionResult(object convertedValue)
        {
            _convertedValue = convertedValue;
            _success = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets converted value.
        /// </summary>
        public object ConvertedValue
        {
            get 
            {
                return _convertedValue;
            }
            set 
            {
                _convertedValue = value;
            }
        }

        /// <summary>
        /// Gets or sets boolean indicating if conversion was successful.
        /// </summary>
        public bool Success
        {
            get 
            {
                return _success;
            }
            set 
            {
                _success = value;
            }
        }

        /// <summary>
        /// Gets or sets string containing eventual error message if conversion was unsuccessful.
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
            }
        }

        #endregion
    }
}
