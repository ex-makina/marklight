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
    public class ElementSizeValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ElementSizeValueInterpolator()
        {
            _type = typeof(ElementSize);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two element sizes based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            ElementSize a = from as ElementSize;
            ElementSize b = to as ElementSize;

            if (a == null || b == null)
                return base.Interpolate(from, to, weight);

            if (a.Unit == ElementSizeUnit.Percents || b.Unit == ElementSizeUnit.Percents)
            {
                if (a.Unit != b.Unit)
                {
                    // can't interpolate between percent and another unit type
                    return from;
                }
                else
                {
                    return new ElementSize(Lerp(a.Percent, b.Percent, weight), ElementSizeUnit.Percents);
                }
            }
            
            return new ElementSize(Lerp(a.Pixels, b.Pixels, weight), ElementSizeUnit.Pixels);
        }

        #endregion
    }
}
