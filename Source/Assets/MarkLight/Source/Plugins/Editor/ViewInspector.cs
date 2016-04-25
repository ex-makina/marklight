#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace MarkLight.Editor
{
    /// <summary>
    /// Custom inspector for View components.
    /// </summary>
    [CustomEditor(typeof(View), true)]
    public class ViewInspector : UnityEditor.Editor
    {
        #region Methods

        /// <summary>
        /// Called when inspector GUI is to be rendered.
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            // add button for updating view
            if (GUILayout.Button("Update View"))
            {
                var view = (View)target;
                view.QueueAllChangeHandlers();
                view.TriggerChangeHandlers();
            }
        }

        #endregion
    }
}
