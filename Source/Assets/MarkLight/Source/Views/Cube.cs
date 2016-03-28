#region Using Statements
using MarkLight.ValueConverters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#endregion

namespace MarkLight.Views
{
    /// <summary>
    /// Cube.
    /// </summary>
    [HideInPresenter]
    public class Cube : View
    {
        #region Fields
        
        public MeshFilter MeshFilter;
        public MeshRenderer MeshRenderer;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Cube()
        {
        }

        #endregion

        #region Methods
        #endregion

        #region Properties
        #endregion
    }
}
