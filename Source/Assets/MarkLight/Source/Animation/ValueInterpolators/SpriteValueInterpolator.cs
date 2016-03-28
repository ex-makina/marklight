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
    /// Sprite value interpolator.
    /// </summary>
    public class SpriteValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SpriteValueInterpolator()
        {
            _type = typeof(Sprite);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two sprites based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            return weight < 1f ? from : to;
        }

        #endregion
    }
}
