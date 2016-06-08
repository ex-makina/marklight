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
    /// Processes view XUML assets and generates scene objects.
    /// </summary>
    internal class ViewPostprocessor : AssetPostprocessor
    {
        #region Methods

        /// <summary>
        /// Processes the XUML assets that are added, deleted, updated, etc. and generates the scene objects.
        /// </summary>
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            // don't process XUML assets while playing or when there is no view presenter in the scene
            if (Application.isPlaying || ViewPresenter.Instance == null || ViewPresenter.Instance.DisableAutomaticReload)
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
        /// Processes XUML assets.
        /// </summary>
        public static void ProcessViewAssets()
        {
            ViewPresenter.UpdateInstance();

            // don't process XUML assets while playing or when there is no view presenter in the scene
            if (Application.isPlaying || ViewPresenter.Instance == null)
            {
                return;
            }

            // get all XUML assets
            HashSet<TextAsset> viewAssets = new HashSet<TextAsset>();
            foreach (var path in Configuration.Instance.ViewPaths)
            {
                string localPath = path.StartsWith("Assets/") ? path.Substring(7) : path;
                foreach (var asset in GetXumlAssetsAtPath(localPath))
                {
                    viewAssets.Add(asset);
                }
            }

            // uncomment to log load performance
            //var sw = System.Diagnostics.Stopwatch.StartNew();

            // load XUML assets
            ViewData.LoadAllXuml(viewAssets);

            // update xsd schema
            if (ViewPresenter.Instance.UpdateXsdSchema)
            {
                ViewPresenter.Instance.UpdateXsdSchema = false;
                GenerateXsdSchema();
            }

            // uncomment to log load performance
            //Utils.Log("Total view processing time: {0}", sw.ElapsedMilliseconds);
            Utils.Log("[MarkLight] Views processed. {0}", DateTime.Now);
        }

        /// <summary>
        /// Gets all XUML assets of a certain type at a path.
        /// </summary>
        private static List<TextAsset> GetXumlAssetsAtPath(string path)
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

        /// <summary>
        /// Generates XSD schema from view type data.
        /// </summary>
        public static void GenerateXsdSchema()
        {
            if (ViewPresenter.Instance == null)
            {
                Utils.LogError("[MarkLight] Unable to generate XSD schema. View presenter can't be found in scene. Make sure the view presenter is enabled.");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<xs:schema id=\"MarkLight\" xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" targetNamespace=\"MarkLight\" xmlns=\"MarkLight\" attributeFormDefault=\"unqualified\" elementFormDefault=\"qualified\">");

            // create temporary root view where instantiate each view to get info about view fields
            if (ViewPresenter.Instance.RootView == null)
            {
                ViewPresenter.Instance.RootView = ViewData.CreateView<View>(ViewPresenter.Instance, ViewPresenter.Instance).gameObject;
            }
            var layoutRoot = ViewPresenter.Instance.RootView.GetComponent<View>();
            var temporaryRootView = ViewData.CreateView<View>(layoutRoot, layoutRoot);
            var enums = new HashSet<Type>();

            Utils.SuppressLogging = true;

            // generate XSD schema based on view type data
            foreach (var viewType in ViewPresenter.Instance.ViewTypeDataList)
            {
                sb.AppendLine();
                sb.AppendFormat("  <xs:element name=\"{0}\" type=\"{0}\" />{1}", viewType.ViewName, Environment.NewLine);
                sb.AppendFormat("  <xs:complexType name=\"{0}\">{1}", viewType.ViewName, Environment.NewLine);
                sb.AppendFormat("    <xs:sequence>{0}", Environment.NewLine);
                sb.AppendFormat("      <xs:any processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\" />{0}", Environment.NewLine);
                sb.AppendFormat("    </xs:sequence>{0}", Environment.NewLine);

                // instantiate view to get detailed information about each view field
                var view = ViewData.CreateView(viewType.ViewName, temporaryRootView, temporaryRootView);
                view.InitializeViews();

                var viewFields = new List<string>(viewType.ViewFields);
                viewFields.AddRange(viewType.DependencyFields);
                viewFields.AddRange(viewType.MapViewFields.Select(x => x.From));
                viewFields.AddRange(viewType.ViewActionFields);
                viewFields = viewFields.Distinct().ToList();

                // create attributes
                foreach (var viewField in viewFields)
                {
                    bool isEnum = false;
                    var viewFieldData = view.GetViewFieldData(viewField);
                    if (viewFieldData.ViewFieldType != null && viewFieldData.ViewFieldType.IsEnum)
                    {
                        isEnum = true;
                        enums.Add(viewFieldData.ViewFieldType);
                    }

                    sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", viewField, isEnum ? "Enum" + viewFieldData.ViewFieldTypeName : "xs:string", Environment.NewLine);
                }

                sb.AppendFormat("    <xs:anyAttribute processContents=\"skip\" />{0}", Environment.NewLine);

                sb.AppendFormat("  </xs:complexType>{0}", Environment.NewLine);
            }

            Utils.SuppressLogging = false;

            // destroy temporary root view
            GameObject.DestroyImmediate(temporaryRootView.gameObject);

            // add enums
            foreach (var enumType in enums)
            {
                sb.AppendLine();
                sb.AppendFormat("  <xs:simpleType name=\"{0}\">{1}", "Enum" + enumType.Name, Environment.NewLine);
                sb.AppendFormat("    <xs:restriction base=\"xs:string\">{0}", Environment.NewLine);

                foreach (var enumTypeName in Enum.GetNames(enumType))
                {
                    sb.AppendFormat("      <xs:enumeration value=\"{0}\" />{1}", enumTypeName, Environment.NewLine);
                }

                sb.AppendFormat("    </xs:restriction>{0}", Environment.NewLine);
                sb.AppendFormat("  </xs:simpleType>{0}", Environment.NewLine);
            }

            // add theme element
            sb.AppendLine();
            sb.AppendFormat("  <xs:element name=\"{0}\" type=\"{0}\" />{1}", "Theme", Environment.NewLine);
            sb.AppendFormat("  <xs:complexType name=\"{0}\">{1}", "Theme", Environment.NewLine);
            sb.AppendFormat("    <xs:sequence>{0}", Environment.NewLine);
            sb.AppendFormat("      <xs:any processContents=\"lax\" minOccurs=\"0\" maxOccurs=\"unbounded\" />{0}", Environment.NewLine);
            sb.AppendFormat("    </xs:sequence>{0}", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "BaseDirectory", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Name", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "UnitSize", "xs:string", Environment.NewLine);
            sb.AppendFormat("  </xs:complexType>{0}", Environment.NewLine);

            // add resource dictionary element
            sb.AppendLine();
            sb.AppendFormat("  <xs:element name=\"{0}\" type=\"{0}\" />{1}", "ResourceDictionary", Environment.NewLine);
            sb.AppendFormat("  <xs:complexType name=\"{0}\">{1}", "ResourceDictionary", Environment.NewLine);
            sb.AppendFormat("    <xs:sequence minOccurs=\"0\" maxOccurs=\"unbounded\">{0}", Environment.NewLine);
            sb.AppendFormat("      <xs:element name=\"Resource\" type=\"Resource\" minOccurs=\"0\" maxOccurs=\"unbounded\" />{0}", Environment.NewLine);
            sb.AppendFormat("      <xs:element name=\"ResourceGroup\" type=\"ResourceGroup\" minOccurs=\"0\" maxOccurs=\"unbounded\" />{0}", Environment.NewLine);
            sb.AppendFormat("    </xs:sequence>{0}", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Name", "xs:string", Environment.NewLine);
            sb.AppendFormat("  </xs:complexType>{0}", Environment.NewLine);

            // add resource element
            sb.AppendLine();
            sb.AppendFormat("  <xs:element name=\"{0}\" type=\"{0}\" />{1}", "Resource", Environment.NewLine);
            sb.AppendFormat("  <xs:complexType name=\"{0}\">{1}", "Resource", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Key", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Value", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Language", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Platform", "xs:string", Environment.NewLine);
            sb.AppendFormat("  </xs:complexType>{0}", Environment.NewLine);

            // add resource group element
            sb.AppendLine();
            sb.AppendFormat("  <xs:element name=\"{0}\" type=\"{0}\" />{1}", "ResourceGroup", Environment.NewLine);
            sb.AppendFormat("  <xs:complexType name=\"{0}\">{1}", "ResourceGroup", Environment.NewLine);
            sb.AppendFormat("    <xs:sequence minOccurs=\"0\" maxOccurs=\"unbounded\">{0}", Environment.NewLine);
            sb.AppendFormat("      <xs:element name=\"Resource\" type=\"Resource\" minOccurs=\"0\" maxOccurs=\"unbounded\" />{0}", Environment.NewLine);
            sb.AppendFormat("      <xs:element name=\"ResourceGroup\" type=\"ResourceGroup\" minOccurs=\"0\" maxOccurs=\"unbounded\" />{0}", Environment.NewLine);
            sb.AppendFormat("    </xs:sequence>{0}", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Key", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Value", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Language", "xs:string", Environment.NewLine);
            sb.AppendFormat("    <xs:attribute name=\"{0}\" type=\"{1}\" />{2}", "Platform", "xs:string", Environment.NewLine);
            sb.AppendFormat("  </xs:complexType>{0}", Environment.NewLine);

            sb.AppendLine("</xs:schema>");

            // save file
            var path = Configuration.Instance.SchemaFile;
            string localPath = path.StartsWith("Assets/") ? path.Substring(7) : path;
            File.WriteAllText(String.Format("{0}/{1}", Application.dataPath, localPath), sb.ToString());

            // print result
            Debug.Log(String.Format("[MarkLight] Schema generated at \"{0}\"", Configuration.Instance.SchemaFile));
        }

        #endregion
    }
}

