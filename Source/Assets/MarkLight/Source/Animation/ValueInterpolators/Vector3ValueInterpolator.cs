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
    /// Vector3 value interpolator.
    /// </summary>
    public class Vector3ValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Vector3ValueInterpolator()
        {
            _type = typeof(Vector3);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two vector values based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            Vector3 v1 = (Vector3)from;
            Vector3 v2 = (Vector3)to;

            return new Vector3(
                Lerp(v1.x, v2.x, weight),
                Lerp(v1.y, v2.y, weight),
                Lerp(v1.z, v2.z, weight));
        }

        #endregion
    }
}
