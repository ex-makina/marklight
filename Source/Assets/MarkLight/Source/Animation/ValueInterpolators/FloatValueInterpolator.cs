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
    /// Float value interpolator.
    /// </summary>
    public class FloatValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FloatValueInterpolator()
        {
            _type = typeof(float);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two float values based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            return Lerp((float)from, (float)to, weight);
        }

        #endregion
    }
}
