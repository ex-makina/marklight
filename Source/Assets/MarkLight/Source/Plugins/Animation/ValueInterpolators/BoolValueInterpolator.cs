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
    /// Bool value interpolator.
    /// </summary>
    public class BoolValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public BoolValueInterpolator()
        {
            _type = typeof(bool);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two float values based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            return weight < 1f ? from : to;
        }

        #endregion
    }
}
