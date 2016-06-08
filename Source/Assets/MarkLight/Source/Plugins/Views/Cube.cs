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

        #region MeshFilter

        /// <summary>
        /// Instantiated mesh.
        /// </summary>
        /// <d>Instantiated mesh assigned to the mesh filter.</d>
        [MapTo("MeshFilter.mesh")]
        public _Mesh Mesh;

        /// <summary>
        /// Shared mesh.
        /// </summary>
        /// <d>Shared mesh of the mesh filter.</d>
        [MapTo("MeshFilter.sharedMesh")]
        public _Mesh SharedMesh;

        /// <summary>
        /// Mesh filter component.
        /// </summary>
        /// <d>The mesh filter takes a mesh and passes it to the mesh renderer.</d>
        public MeshFilter MeshFilter;

        #endregion

        #region MeshRenderer

        /// <summary>
        /// Additional vertex streams.
        /// </summary>
        /// <d>Vertex attributes in this mesh will override or add attributes of the primary mesh in the MeshRenderer.</d>
        [MapTo("MeshRenderer.additionalVertexStreams")]
        public _Mesh AdditionalVertexStreams;

        /// <summary>
        /// Mesh renderer component.
        /// </summary>
        /// <d>Renders a mesh.</d>
        public MeshRenderer MeshRenderer;

        #endregion

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

        /// <summary>
        /// Initializes internal default values.
        /// </summary>
        public override void InitializeInternalDefaultValues()
        {
            base.InitializeInternalDefaultValues();

            // temporarily instantiate primitive to get mesh
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;

            if (Application.isEditor)
            {
                GameObject.DestroyImmediate(gameObject);
            }
            else
            {
                GameObject.Destroy(gameObject);
            }

            // set mesh
            SharedMesh.DirectValue = mesh;
        }

        #endregion
    }
}
