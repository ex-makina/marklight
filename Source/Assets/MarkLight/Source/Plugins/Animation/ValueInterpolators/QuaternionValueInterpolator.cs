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
    /// Element size value interpolator.
    /// </summary>
    public class QuaternionValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public QuaternionValueInterpolator()
        {
            _type = typeof(Quaternion);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two element sizes based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            Quaternion q1 = (Quaternion)from;
            Quaternion q2 = (Quaternion)to;
                      
            return Quaternion.Lerp(q1, q2, weight);
        }

        #endregion
    }
}
