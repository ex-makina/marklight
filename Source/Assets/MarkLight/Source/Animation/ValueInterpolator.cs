#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
#endregion

namespace MarkLight.Animation
{
    /// <summary>
    /// Base class for value interpolators.
    /// </summary>
    public class ValueInterpolator
    {
        #region Fields

        protected Type _type;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ValueInterpolator()
        {
            _type = null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Interpolates between two values based on a weight.
        /// </summary>
        public virtual object Interpolate(object from, object to, float weight)
        {
            return weight < 1f ? from : to;
        }

        /// <summary>
        /// Linear interpolation between two float values.
        /// </summary>
        public static float Lerp(float from, float to, float weight)
        {
            return (1f - weight) * from + weight * to;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets type of values interpolated.
        /// </summary>
        public Type Type
        {
            get
            {
                return _type;
            }
        }

        #endregion        
    }
}
