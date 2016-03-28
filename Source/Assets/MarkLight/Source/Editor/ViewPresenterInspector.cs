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
    /// Custom inspector for ViewPresenter components.
    /// </summary>
    [CustomEditor(typeof(ViewPresenter))]
    public class ViewPresenterInspector : UnityEditor.Editor
    {
        #region Methods

        /// <summary>
        /// Called when inspector GUI is to be rendered.
        /// </summary>
        public override void OnInspectorGUI()
        {
            //DrawDefaultInspector();

            var viewPresenter = (ViewPresenter)target;

            // main view selection
            int selectedViewIndex = viewPresenter.Views.IndexOf(viewPresenter.MainView) + 1;

            // .. add empty selection
            var mainViewOptions = new List<string>(viewPresenter.Views);
            mainViewOptions.Insert(0, "-- none --");

            // .. add drop-down logic
            int newSelectedViewIndex = EditorGUILayout.Popup("Main View", selectedViewIndex, mainViewOptions.ToArray());
            viewPresenter.MainView = newSelectedViewIndex > 0 ? viewPresenter.Views[newSelectedViewIndex - 1] : String.Empty;
            if (newSelectedViewIndex != selectedViewIndex)
            {
                // .. trigger reload on view presenter
                ViewData.GenerateViews();
            }

            // default theme selection
            int selectedThemeIndex = viewPresenter.Themes.IndexOf(viewPresenter.DefaultTheme) + 1;

            // .. add empty selection
            var themeOptions = new List<string>(viewPresenter.Themes);
            themeOptions.Insert(0, "-- none --");

            // .. add drop-down logic
            int newSelectedThemeIndex = EditorGUILayout.Popup("Default Theme", selectedThemeIndex, themeOptions.ToArray());
            viewPresenter.DefaultTheme = newSelectedThemeIndex > 0 ? viewPresenter.Themes[newSelectedThemeIndex - 1] : String.Empty;
            if (newSelectedThemeIndex != selectedThemeIndex)
            {
                // .. trigger reload on view presenter
                ViewData.GenerateViews();
            }

            // reload button
            if (GUILayout.Button("Reload Views"))
            {
                // .. trigger reload of views
                ViewPostprocessor.ProcessViewAssets();
            }
        }

        #endregion
    }
}
