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
    /// Vector4 value interpolator.
    /// </summary>
    public class Vector4ValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Vector4ValueInterpolator()
        {
            _type = typeof(Vector4);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two vector values based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            Vector4 v1 = (Vector4)from;
            Vector4 v2 = (Vector4)to;

            return new Vector4(
                Lerp(v1.x, v2.x, weight),
                Lerp(v1.y, v2.y, weight),
                Lerp(v1.z, v2.z, weight),
                Lerp(v1.w, v2.w, weight));
        }

        #endregion
    }
}
