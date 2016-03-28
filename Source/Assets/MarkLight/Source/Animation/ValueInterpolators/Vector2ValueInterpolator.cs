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
    /// Vector2 value interpolator.
    /// </summary>
    public class Vector2ValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Vector2ValueInterpolator()
        {
            _type = typeof(Vector2);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two vector values based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            Vector2 v1 = (Vector2)from;
            Vector2 v2 = (Vector2)to;

            return new Vector2(
                Lerp(v1.x, v2.x, weight),
                Lerp(v1.y, v2.y, weight));
        }

        #endregion
    }
}
