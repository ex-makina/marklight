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
    /// Color value interpolator.
    /// </summary>
    public class ColorValueInterpolator : ValueInterpolator
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ColorValueInterpolator()
        {
            _type = typeof(Color);
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Interpolates between two colors based on a weight.
        /// </summary>
        public override object Interpolate(object from, object to, float weight)
        {
            Color aRgb = from != null ? (Color)from : Color.black;
            Color bRgb = to != null ? (Color)to : Color.black;

            // simple aRGB interpolation 
            Color result = new Color(
                Lerp(aRgb.r, bRgb.r, weight),
                Lerp(aRgb.g, bRgb.g, weight),
                Lerp(aRgb.b, bRgb.b, weight),
                Lerp(aRgb.a, bRgb.a, weight)
                );

            return result;

            //// convert colors to HSV
            //ColorHsv a = aRgb.ToHsv();
            //ColorHsv b = bRgb.ToHsv();

            //// interpolate values
            //ColorHsv resultHsv;
            //resultHsv.Hue = Lerp(a.Hue, b.Hue, weight);
            //resultHsv.Saturation = Lerp(a.Saturation, b.Saturation, weight);
            //resultHsv.Value = Lerp(a.Value, b.Value, weight);

            //// convert back to RGB
            //Color result = resultHsv.ToRgb();
            //result.a = Lerp(aRgb.a, bRgb.a, weight);
            //return result;
        }

        #endregion
    }
}
