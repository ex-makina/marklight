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
    /// Margin value interpolator.
    /// </summary>
    public class MarginValueInterpolator : ValueInterpolator
    {
        #region Fields

        private ElementSizeValueInterpolator _elementSizeValueInterpolator;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MarginValueInterpolator()
        {
            _type = typeof(ElementMargin);
            _elementSizeValueInterpolator = new ElementSizeValueInterpolator();
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two colors based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            ElementMargin a = from as ElementMargin;
            ElementMargin b = to as ElementMargin;
            if (a == null || b == null)
                return base.Interpolate(from, to, weight);

            return new ElementMargin(
                _elementSizeValueInterpolator.Interpolate(a.Left, b.Left, weight) as ElementSize,
                _elementSizeValueInterpolator.Interpolate(a.Top, b.Top, weight) as ElementSize,
                _elementSizeValueInterpolator.Interpolate(a.Right, b.Right, weight) as ElementSize,
                _elementSizeValueInterpolator.Interpolate(a.Bottom, b.Bottom, weight) as ElementSize);
        }

        #endregion
    }
}
