#region Using Statements
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Menu item for creating views.
    /// </summary>
    public class MenuItems
    {
        #region Methods

        [MenuItem("Assets/Create/View")]
        private static void CreateViewMenuItem()
        {
            var configuration = Configuration.Instance;
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            string comparePath = path.EndsWith("/") ? path : path + "/";

            // check if path is under a views folder
            if (String.IsNullOrEmpty(path) || !configuration.ViewPaths.Any(x => comparePath.StartsWith(x, StringComparison.OrdinalIgnoreCase)))
            {
                // no. pick default folder 
                path = configuration.ViewPaths.FirstOrDefault();
                if (String.IsNullOrEmpty(path))
                {
                    Debug.LogError(String.Format("[MarkLight] Unable to create view. No view folders are configured.", path));
                }
                System.IO.Directory.CreateDirectory(path);
            }
            else
            {
                if (!Directory.Exists(path))
                {
                    // try removing filename from path
                    path = Path.GetDirectoryName(path);
                    if (!Directory.Exists(path))
                    {
                        Debug.LogError(String.Format("[MarkLight] Unable to create view at path \"{0}\". Directory not found.", path));
                        return; 
                    }
                }
            }

            // create new view asset
            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/NewView.xml");
            File.WriteAllText(assetPathAndName, "<NewView xmlns=\"MarkLight\">\n</NewView>");
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath(assetPathAndName, typeof(TextAsset));
        }

        #endregion
    }
}
