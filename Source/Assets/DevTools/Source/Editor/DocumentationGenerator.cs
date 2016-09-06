#region Using Statements
using MarkLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEditor;
using UnityEngine;
#endregion

namespace Marklight.DevTools.Source.Editor
{
    /// <summary>
    /// Generates HTML documentation for the Marklight framework.
    /// </summary>
    [CustomEditor(typeof(DevTools))]
    public class DocumentationGenerator : UnityEditor.Editor
    {
        /// <summary>
        /// Generates documentation from an XML file.
        /// </summary>
        public void GenerateDocumentation()
        {
            // Add the following to the Source.CSharp.csproj file in first PropertyGroup
            // <DocumentationFile>Assets\DevTools\Docs\Documentation.XML</DocumentationFile>
            // parse Assets\DevTools\Docs\Documentation.XML
            var documentationXml = File.ReadAllText("Assets/DevTools/Docs/Documentation.XML");
            var viewTemplate = File.ReadAllText("Assets/DevTools/Docs/ViewDocTemplate.html");

            XElement xmlElement = null;
            try
            {
                xmlElement = XElement.Parse(documentationXml);
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Error parsing documentation XML. Exception thrown: {0}", Utils.GetError(e));
                return;
            }

            var docData = new List<DocData>();

            Utils.SuppressLogging = true;

            // parse XML comments and create document data objects
            foreach (var element in xmlElement.Descendants("member").Where(x => x.Attribute("name").Value.StartsWith("T:")))
            {
                var data = new DocData();

                // ignore examples, editor, textmeshpro and dev-tools               
                data.FullTypeName = element.Attribute("name").Value.Substring(2);
                if (data.FullTypeName.StartsWith("MarkLight.Examples") || data.FullTypeName.StartsWith("MarkLight.DevTools") ||
                    data.FullTypeName.StartsWith("MarkLight.Editor") || data.FullTypeName.StartsWith("MarkLight.Views.UI.DemoMessage") ||
                    data.FullTypeName.StartsWith("TMPro"))
                {
                    continue;
                }

                data.TypeName = data.FullTypeName.Substring(data.FullTypeName.LastIndexOf(".") + 1);

                // rename `1 for generic types
                data.FileName = String.Format("{0}.html", data.FullTypeName.Replace("`1", "T"));
                data.HtmlTypeName = data.TypeName.Replace("`1", "<T>");

                data.IsView = ViewPresenter.Instance.ViewTypeDataList.Any(x => x.ViewTypeName == data.TypeName);
                data.IsType = true;

                // add summary
                data.Summary = element.Element("summary").Value.Trim();

                // add description
                var description = element.Element("d");
                if (description != null)
                {
                    data.Description = description.Value.Trim();
                }

                // find all view fields associated with this type                
                foreach (var fieldElement in xmlElement.Descendants("member").Where(x => x.Attribute("name").Value.StartsWith(String.Format("F:{0}.", data.FullTypeName)) ||
                    x.Attribute("name").Value.StartsWith(String.Format("P:{0}.", data.FullTypeName))))
                {
                    string fieldName = string.Empty;

                    try
                    {
                        // get field summaries and descriptions
                        fieldName = fieldElement.Attribute("name").Value.Substring(2 + data.FullTypeName.Length + 1);
                        if (fieldName.Count(x => x == '.') > 0)
                            continue;

                        data.FieldSummaries.Add(fieldName, fieldElement.Element("summary").Value.Trim());

                        var fieldDescription = fieldElement.Element("d");
                        if (fieldDescription != null)
                        {
                            data.FieldDescriptions.Add(fieldName, fieldDescription.Value.Trim());
                        }

                        var fieldActionData = fieldElement.Element("actionData");
                        if (fieldActionData != null)
                        {
                            data.FieldActionData.Add(fieldName, fieldActionData.Value.Trim());
                        }
                        
                        // get mapped view field summaries and descriptions
                        foreach (var mappedSummary in fieldElement.Elements("maps"))
                        {
                            var mapField = mappedSummary.Attribute("field").Value;
                            data.FieldSummaries.Add(mapField, mappedSummary.Value.Trim());
                            data.MappedFields.Add(mapField);
                        }

                        foreach (var mappedDescription in fieldElement.Elements("mapd"))
                        {
                            var mapField = mappedDescription.Attribute("field").Value;
                            data.FieldDescriptions.Add(mapField, mappedDescription.Value.Trim());
                        }
                    }
                    catch (Exception e)
                    {
                        Utils.SuppressLogging = false;
                        Utils.LogError("Error generating documentation for {0}, when processing field {1}. {2}{3}", data.HtmlTypeName, fieldName, e.Message, e.StackTrace);
                        Utils.SuppressLogging = true;
                    }
                }

                docData.Add(data);
                //Debug.LogFormat("{0}: {1}", data.FileName, data.HtmlTypeName);
            }

            System.IO.Directory.CreateDirectory("Assets/DevTools/Docs/API/");

            // sort by name
            docData = docData.OrderBy(x => x.TypeName).ToList();

            // generate TOC for views and types
            var viewDocs = docData.Where(x => x.IsView);            
            var viewsTocSb = new StringBuilder();
            foreach (var viewDoc in viewDocs)
            {
                viewsTocSb.AppendFormat("<h5><a href=\"{0}\">{1}</a></h5>", viewDoc.FileName, viewDoc.HtmlTypeName);
            }
            var viewsToc = viewsTocSb.ToString();

            var typesDocs = docData.Where(x => !x.IsView);
            foreach (var viewDoc in viewDocs)
            {
                viewsTocSb.AppendFormat("<h5><a href=\"{0}\">{1}</a></h5>", viewDoc.FileName, viewDoc.HtmlTypeName);
            }
            var typesToc = viewsTocSb.ToString();

            var layoutRoot = ViewPresenter.Instance.RootView.GetComponent<View>();
            var rootView = ViewData.CreateView<View>(layoutRoot, layoutRoot);

            // generate view content documentation
            foreach (var data in viewDocs)
            {
                var viewTypeData = ViewPresenter.Instance.ViewTypeDataList.First(x => x.ViewTypeName == data.TypeName);
                var type = ViewData.GetViewType(data.TypeName);
                if (type == null)
                    continue;

                // section: title
                var sb = new StringBuilder();
                sb.AppendFormat("<h1>{0}</h1>", data.HtmlTypeName);

                // section: inherits from                
                if (type.BaseType != null && type.BaseType.FullName.StartsWith("MarkLight"))
                {
                    var inheritsFromData = docData.FirstOrDefault(x => x.FullTypeName == type.BaseType.FullName);
                    if (inheritsFromData != null)
                    {
                        sb.AppendFormat("<h3 class=\"inherit-info\">Inherits from <a href=\"{0}\">{1}</a></h3>", inheritsFromData.FileName, inheritsFromData.HtmlTypeName);
                    }
                }

                // section: used by
                var usedByList = ViewPresenter.Instance.ViewTypeDataList.Where(x => x.DependencyNames.Contains(data.TypeName))
                    .Select(x => viewDocs.FirstOrDefault(y => y.TypeName == x.ViewTypeName)).Where(x => x != null).ToList();
                if (usedByList.Count > 0)
                {
                    sb.Append("<small class=\"inherit-info\">Used by:");
                    foreach (var usedBy in usedByList)
                    {
                        sb.AppendFormat(" <a href=\"{0}\">{1}</a>", usedBy.FileName, usedBy.HtmlTypeName);
                    }
                    sb.Append("</small><br>");
                }
                sb.Append("<br>");

                // section: description
                if (!String.IsNullOrEmpty(data.Summary))
                {
                    sb.AppendFormat("<h2>Description</h2>{0}<br><br>", !String.IsNullOrEmpty(data.Description) ? data.Description : data.Summary);
                }

                // section: fields
                var viewFields = new List<string>(viewTypeData.ViewFields);
                viewFields.AddRange(viewTypeData.DependencyFields);
                viewFields.AddRange(viewTypeData.MapViewFields.Select(x => x.From));
                viewFields = viewFields.Distinct().ToList();

                if (viewFields.Count > 0)
                {
                    sb.Append("<h2>View Fields</h2>");
                    sb.Append("<table class=\"table table-condensed table-hover viewTableViewFields\">");
                    sb.Append("<thead><tr><th>Name</th><th class=\"col-lg-2\">Type</th><th>Description</th><th class=\"tableExpandColumn\"></th></tr></thead>");
                    sb.Append("<tbody>");

                    // get information about view field
                    var view = ViewData.CreateView(data.TypeName, rootView, rootView);
                    view.InitializeViews();

                    int viewFieldIndex = 0;
                    foreach (var viewField in viewFields.OrderBy(x => x))
                    {
                        // get view field type information
                        var viewFieldData = view.GetViewFieldData(viewField);
                        var viewFieldTypeSummary = viewFieldData.ViewFieldTypeName;
                        var viewFieldTypeDoc = docData.FirstOrDefault(x => x.TypeName == viewFieldTypeSummary);

                        if (viewFieldTypeDoc != null)
                        {
                            viewFieldTypeSummary = String.Format("<a href=\"{0}\">{1}</a>", viewFieldTypeDoc.FileName, viewFieldTypeDoc.HtmlTypeName);
                        }                        
                        else
                        {
                            // check if the type is a Unity type
                            if (viewFieldData.ViewFieldType.FullName.StartsWith("UnityEngine"))
                            {
                                // reference the unity docs
                                viewFieldTypeSummary = String.Format("<a href=\"http://docs.unity3d.com/ScriptReference/{0}.html\">{1} <i class=\"fa fa-external-link fa-tiny\"></i></a>", viewFieldTypeSummary, viewFieldTypeSummary);
                            }
                            else
                            {
                                // replace Boolean, String, Single, Object, etc.
                                if (viewFieldTypeSummary == "Boolean")
                                    viewFieldTypeSummary = "bool";
                                else if (viewFieldTypeSummary == "Int32")
                                    viewFieldTypeSummary = "int";
                                else if (viewFieldTypeSummary == "Single")
                                    viewFieldTypeSummary = "float";
                                else if (viewFieldTypeSummary == "String")
                                    viewFieldTypeSummary = "string";
                                else if (viewFieldTypeSummary == "Object")
                                    viewFieldTypeSummary = "object";
                            }
                        }

                        // add summary row
                        var baseViewDoc = data;
                        if (String.IsNullOrEmpty(baseViewDoc.GetFieldSummary(viewField)))
                        {
                            // check if field summary exist in any of the base types
                            var baseType = view.GetType().BaseType;
                            while (baseType != null && baseType != typeof(object))
                            {
                                baseViewDoc = docData.FirstOrDefault(x => x.TypeName == baseType.Name);
                                if (baseViewDoc != null && !String.IsNullOrEmpty(baseViewDoc.GetFieldSummary(viewField)))
                                    break;

                                baseType = baseType.BaseType;
                            }                            
                        }

                        if (baseViewDoc == null)
                        {
                            baseViewDoc = data;
                        }

                        // find mapped field path                        
                        string mappedPath = String.Empty;
                        bool isMappedViewField = viewTypeData.MapViewFields.Any(x => x.From == viewField);
                        if (isMappedViewField)
                        {
                            ViewFieldData prevVp = null;
                            for (int i = 0; ; ++i)
                            {
                                var vp = view.GetViewFieldData(viewField, i);
                                if (vp == prevVp)
                                    break;

                                prevVp = vp;

                                // strip away the last view field which points to the new mapped field
                                int lastDot = mappedPath.LastIndexOf(".");
                                mappedPath = lastDot > 0 ? mappedPath.Substring(0, lastDot + 1) : String.Empty;
                                mappedPath += vp.ViewFieldPath;
                            }
                        }

                        sb.AppendFormat("<tr data-toggle=\"collapse\" data-target=\"#viewFieldDetails{0}\" class=\"accordion-toggle clickable\"><td class=\"{1}\">{2}{3}</td><td class=\"viewFieldType\">{4}</td><td class=\"viewTableSummary\">{5}</td><td><button class=\"btn btn-default btn-xs\"><span class=\"glyphicon glyphicon-plus\"></span></button></td></tr>", 
                            viewFieldIndex,
                            isMappedViewField ? "mappedViewFieldName" : "viewFieldName",
                            viewField,
                            isMappedViewField ? String.Format(" <i class=\"fa fa-mail-forward\" data-toggle=\"tooltip\" data-placement=\"right\" title=\"{0}\"></i>", mappedPath) : String.Empty,                            
                            viewFieldTypeSummary,
                            baseViewDoc.GetFieldSummary(viewField));

                        // add expandable details row
                        var fieldDescription = baseViewDoc.GetFieldDescription(viewField);
                        sb.AppendFormat("<tr><td colspan=\"4\" class=\"hiddenRow\"><div class=\"accordion-body collapse viewFieldDetails\" id=\"viewFieldDetails{0}\"><div class=\"viewFieldDetailsContent\">{1}", viewFieldIndex, baseViewDoc.GetFieldDescription(viewField));

                        // ... add additional information if it's an enum type
                        if (viewFieldData.ViewFieldType != null && viewFieldData.ViewFieldType.IsEnum)
                        {
                            if (!String.IsNullOrEmpty(fieldDescription))
                            {
                                sb.Append("<br><br>");
                            }
                            sb.Append("<b>Enum Values</b><table class=\"table table-condensed\"><thead><tr><th>Name</th><th>Description</th></tr></thead><tbody>");
                                                        
                            var enumData = docData.FirstOrDefault(x => x.FullTypeName == viewFieldData.ViewFieldType.FullName);
                            if (enumData != null)
                            {
                                foreach (var enumFieldName in enumData.FieldSummaries.Keys)
                                {
                                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td></tr>", enumFieldName, enumData.FieldSummaries[enumFieldName]);
                                }
                            }
                            else
                            {
                                var memberInfos = viewFieldData.ViewFieldType.GetMembers(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
                                for (int i = 0; i < memberInfos.Length; ++i)
                                {
                                    sb.AppendFormat("<tr><td>{0}</td><td></td></tr>", memberInfos[i].Name);
                                }
                            }
                            sb.Append("</tbody></table>");                        
                        }
                        sb.Append("</div></div></td></tr>");

                        ++viewFieldIndex;
                    }
                    sb.Append("</tbody>");
                    sb.Append("</table><br>");

                    // section: view actions
                    var viewActionFieldData = viewTypeData.ViewActionFields;
                    if (viewActionFieldData.Count > 0)
                    {
                        sb.Append("<h2>View Actions</h2>");
                        sb.Append("<table class=\"table table-condensed table-hover viewTableViewActions\">");
                        sb.Append("<thead><tr><th>Name</th><th>Action Data</th><th>Description</th><th class=\"tableExpandColumn\"></th></tr></thead>");
                        sb.Append("<tbody>");

                        int viewActionIndex = 0;
                        foreach (var viewFieldData in viewActionFieldData)
                        {      
                            var viewActionData = data.GetFieldActionData(viewFieldData);
                            if (!String.IsNullOrEmpty(viewActionData))
                            {
                                // find the type
                                var actionDataDoc = docData.FirstOrDefault(x => x.IsType && x.TypeName == viewActionData);
                                if (actionDataDoc != null)
                                {
                                    viewActionData = String.Format("<a href=\"{0}\">{1}</a>", actionDataDoc.FileName, actionDataDoc.HtmlTypeName);
                                }
                            }
                            else
                            {
                                viewActionData = "none";
                            }

                            // get view action field summary
                            var baseViewDoc = data;
                            if (String.IsNullOrEmpty(baseViewDoc.GetFieldSummary(viewFieldData)))
                            {
                                // check if field summary exist in any of the base types
                                var baseType = view.GetType().BaseType;
                                while (baseType != null && baseType != typeof(object))
                                {
                                    baseViewDoc = docData.FirstOrDefault(x => x.TypeName == baseType.Name);
                                    if (baseViewDoc != null && !String.IsNullOrEmpty(baseViewDoc.GetFieldSummary(viewFieldData)))
                                        break;

                                    baseType = baseType.BaseType;
                                }
                            }

                            if (baseViewDoc == null)
                            {
                                baseViewDoc = data;
                            }

                            // get action data
                            sb.AppendFormat("<tr data-toggle=\"collapse\" data-target=\"#viewActionDetails{0}\" class=\"accordion-toggle clickable\"><td class=\"viewFieldName\">{1}{2}</td><td class=\"viewFieldType\">{3}</td><td class=\"viewTableSummary\">{4}</td><td><button class=\"btn btn-default btn-xs\"><span class=\"glyphicon glyphicon-plus\"></span></button></td></tr>", viewActionIndex, viewFieldData,
                                String.Empty,
                                viewActionData,
                                baseViewDoc.GetFieldSummary(viewFieldData));

                            // add expandable details row
                            sb.AppendFormat("<tr><td colspan=\"4\" class=\"hiddenRow\"><div class=\"accordion-body collapse viewFieldDetails\" id=\"viewActionDetails{0}\"><div class=\"viewFieldDetailsContent\">{1}</div></div></td></tr>", viewActionIndex, baseViewDoc.GetFieldDescription(viewFieldData));
                            ++viewActionIndex;
                        }

                        sb.Append("</tbody>");
                        sb.Append("</table><br>");
                    }
                }
                
                data.DocContent = sb.ToString();
            }

            GameObject.DestroyImmediate(rootView.gameObject);


            // generate view content documentation
            foreach (var data in typesDocs)
            {
                // section: title
                var sb = new StringBuilder();
                sb.AppendFormat("<h1>{0}</h1>", data.HtmlTypeName);
                sb.Append("<br>");

                // section: description
                if (!String.IsNullOrEmpty(data.Summary))
                {
                    sb.AppendFormat("<h2>Description</h2>{0}<br><br>", !String.IsNullOrEmpty(data.Description) ? data.Description : data.Summary);
                }

                data.DocContent = sb.ToString();
            }

            // generate HTML files
            foreach (var doc in docData)
            {
                var viewToc = viewsToc.Replace(String.Format(">{0}<", doc.HtmlTypeName),
                    String.Format(" class=\"sectionSelected\">{0}<", doc.HtmlTypeName));

                var fileContent = viewTemplate.Replace("__TITLE__", doc.HtmlTypeName)
                    .Replace("__TOC__", doc.IsView ? viewToc : typesToc).Replace("__CONTENT__", doc.DocContent);

                // write text to file
                File.WriteAllText(String.Format("C:/Projects/Websites/marklightforunity.com/www/docs/api/{0}", doc.FileName),
                    fileContent);
            }

            // generate sitemap.xml file
            var sitemapSb = new StringBuilder();
            sitemapSb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            sitemapSb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"");
            sitemapSb.AppendLine("        xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"");
            sitemapSb.AppendLine("        xsi:schemaLocation=\"http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd\">");

            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/index.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/introduction.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/animations.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/data-binding.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/gettingstarted.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/how-to-get-intellisense.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/how-to-move-marklight.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/resource-dictionaries.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/state-management.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/themes-and-styles.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/view-model.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/tutorials/working-with-list-data.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/news/marklight-2.1.0-released.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/news/marklight-2.2.0-released.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/news/marklight-2.3.0-released.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/news/marklight-2.4.1-released.html</loc>");
            sitemapSb.AppendLine("    </url>");
            sitemapSb.AppendLine("    <url>");
            sitemapSb.AppendLine("        <loc>http://www.marklightforunity.com/docs/news/marklight-2.5.0-released.html</loc>");
            sitemapSb.AppendLine("    </url>");

            foreach (var doc in docData)
            {
                sitemapSb.AppendLine("    <url>");
                sitemapSb.AppendFormat("        <loc>http://www.marklightforunity.com/docs/api/{0}</loc>{1}", doc.FileName, Environment.NewLine);
                sitemapSb.AppendLine("    </url>");
            }

            sitemapSb.AppendLine("</urlset>");

            File.WriteAllText("C:/Projects/Websites/marklightforunity.com/www/sitemap.xml", sitemapSb.ToString());

            Utils.SuppressLogging = false;

            Utils.Log("Documentation generated");
        }

        /// <summary>
        /// Called when inspector GUI is to be rendered.
        /// </summary>
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            // add button for updating view
            if (GUILayout.Button("Generate Documentation"))
            {
                GenerateDocumentation();
            }
        }
    }
}
