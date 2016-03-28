#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Xml.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;
#endregion

namespace MarkLight.Editor
{
    /// <summary>
    /// Processes view XML assets and generates scene objects.
    /// </summary>
    internal class ViewPostprocessor : AssetPostprocessor
    {
        #region Methods

        /// <summary>
        /// Processes the view XML assets that are added, deleted, updated, etc. and generates the scene objects.
        /// </summary>
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // don't process view XML assets while playing or when there is no view presenter in the scene
            if (Application.isPlaying || ViewPresenter.Instance == null)
            {
                return;
            }

            // check if any views have been added, moved, updated or deleted
            var configuration = Configuration.Instance;            
            bool viewAssetsUpdated = false;
            foreach (var path in importedAssets.Concat(deletedAssets).Concat(movedAssets).Concat(movedFromAssetPaths))
            {
                if (configuration.ViewPaths.Any(x => path.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0) &&
                    path.IndexOf(".xml", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    viewAssetsUpdated = true;
                    break;
                }
            }

            // any views updated? 
            if (!viewAssetsUpdated)
            {
                return; // no.
            }

            ProcessViewAssets();
        }

        /// <summary>
        /// Processes view XML assets.
        /// </summary>
        public static void ProcessViewAssets()
        {
            // don't process view XML assets while playing or when there is no view presenter in the scene
            if (Application.isPlaying || ViewPresenter.Instance == null)
            {
                return;
            }

            // get all view XML assets
            HashSet<TextAsset> viewAssets = new HashSet<TextAsset>();
            foreach (var path in Configuration.Instance.ViewPaths)
            {
                string localPath = path.StartsWith("Assets/") ? path.Substring(7) : path;
                foreach (var asset in GetXmlAssetsAtPath(localPath))
                {
                    viewAssets.Add(asset);
                }
            }

            // load view XML assets
            ViewData.LoadAllXml(viewAssets);

            Debug.Log("[MarkLight] Views processed. " + DateTime.Now.ToString());
        }             

        /// <summary>
        /// Gets all XML assets of a certain type at a path.
        /// </summary>
        private static List<TextAsset> GetXmlAssetsAtPath(string path)
        {
            var assets = new List<TextAsset>();
            string searchPath = Application.dataPath + "/" + path;

            if (Directory.Exists(searchPath))
            {
                string[] fileEntries = Directory.GetFiles(searchPath, "*.xml", SearchOption.AllDirectories);
                foreach (string fileName in fileEntries)
                {
                    string localPath = "Assets/" + path + fileName.Substring(searchPath.Length);
                    var textAsset = AssetDatabase.LoadAssetAtPath(localPath, typeof(TextAsset)) as TextAsset;
                    if (textAsset != null)
                    {
                        assets.Add(textAsset);
                    }
                }
            }

            return assets;
        }

        #endregion
    }
}

