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
                if (!viewPresenter.DisableAutomaticReload)
                {
                    ViewData.GenerateViews();
                }
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
                if (!viewPresenter.DisableAutomaticReload)
                {
                    ViewData.GenerateViews();
                }
            }

            // unit size option
            GUIContent unitSizeContent = new GUIContent("Unit Size", "User-defined unit size that can be used in XUML, e.g. Offset=\"12ux, 4uy\"");
            Vector3 newUnitSize = EditorGUILayout.Vector3Field(unitSizeContent, viewPresenter.UnitSize);
            if (newUnitSize != viewPresenter.UnitSize)
            {
                viewPresenter.UnitSize = newUnitSize;
            }

            // base directory
            GUIContent baseDirectoryContent = new GUIContent("Base Directory", "Base directory that will be prepended to assets paths in XUML. E.g. \"Assets/Path/To/Folder/\"");
            string newBaseDirectory = EditorGUILayout.TextField(baseDirectoryContent, viewPresenter.BaseDirectory);
            if (newBaseDirectory != viewPresenter.BaseDirectory)
            {
                viewPresenter.BaseDirectory = newBaseDirectory;
            }

            // default resource dictionary language
            GUIContent defaultLanguageContent = new GUIContent("Default Language", "Default language to be set on the resource dictionary");
            string defaultLanguage = EditorGUILayout.TextField(defaultLanguageContent, viewPresenter.DefaultLanguage);
            if (defaultLanguage != viewPresenter.DefaultLanguage)
            {
                viewPresenter.DefaultLanguage = defaultLanguage;
            }

            // default resource dictionary platform
            GUIContent defaultPlatformContent = new GUIContent("Default Platform", "Default platform to be set on the resource dictionary");
            string defaultPlatform = EditorGUILayout.TextField(defaultPlatformContent, viewPresenter.DefaultPlatform);
            if (defaultPlatform != viewPresenter.DefaultPlatform)
            {
                viewPresenter.DefaultPlatform = defaultPlatform;
            }

            // disable automatic reload option
            GUIContent disableReloadContent = new GUIContent("Disable Automatic Reload", "When checked views are only reloaded when the \"Reload Views\" button is clicked.");
            viewPresenter.DisableAutomaticReload = EditorGUILayout.Toggle(disableReloadContent, viewPresenter.DisableAutomaticReload);

            // generate XSD schema
            GUIContent generateSchemaContent = new GUIContent("Generate Schema", "Generates a new XSD schema for all the views. Used to get intellisense when editing XUML in visual studio. Schema need to be generated when new views are added, renamed or new view fields are added.");
            if (GUILayout.Button(generateSchemaContent))
            {
                ViewPostprocessor.GenerateXsdSchema();
            }

            // reload button
            GUIContent reloadViewsContent = new GUIContent("Reload Views", "Reloads the views in the scene. Views are automatically reloaded when XUML changes. The views need to be manually reloaded when XUML has been edited while the editor was closed or when only code (view models) has been modified.");
            if (GUILayout.Button(reloadViewsContent))
            {
                // .. trigger reload of views
                ViewPostprocessor.ProcessViewAssets();
            }
        }

        #endregion
    }
}
