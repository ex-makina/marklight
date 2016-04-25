#region Using Statements
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;
#endregion

namespace MarkLight.Editor
{
    /// <summary>
    /// Serializable system configuration used by the view processor.
    /// </summary>
    public class Configuration : ScriptableObject
    {
        #region Fields

        public List<string> ViewPaths;
        public string SchemaFile;
        private static Configuration _instance;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public Configuration()
        {
            ViewPaths = new List<string>();
            ViewPaths.Add("Assets/Views/");
            ViewPaths.Add("Assets/MarkLight/Views/");
            ViewPaths.Add("Assets/MarkLight/Examples/Views/");
            SchemaFile = "Assets/MarkLight/Views/Schemas/MarkLight.xsd";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validate paths.
        /// </summary>
        public Configuration GetValidated()
        {
            // make sure all paths ends with "/"
            for (int i = 0; i < ViewPaths.Count; ++i)
            {
                if (!ViewPaths[i].EndsWith("/"))
                {
                    ViewPaths[i] += "/";
                }
            }
                        
            return this;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets global configuration instance.
        /// </summary>
        public static Configuration Instance
        {
            get
            {
                if (_instance != null)
                    return _instance.GetValidated();
                
                // check default directory
                Configuration configuration = AssetDatabase.LoadAssetAtPath("Assets/MarkLight/Configuration/Configuration.asset", typeof(Configuration)) as Configuration;
                if (configuration != null)
                {
                    _instance = configuration;
                    return _instance.GetValidated();
                }

                // search for configuration asset
                var configFilePath = System.IO.Directory.GetFiles(Application.dataPath, "Configuration.asset", System.IO.SearchOption.AllDirectories).FirstOrDefault();
                if (!String.IsNullOrEmpty(configFilePath))
                {
                    string localPath = "Assets/" + configFilePath.Substring(Application.dataPath.Length + 1);
                    configuration = AssetDatabase.LoadAssetAtPath(localPath, typeof(Configuration)) as Configuration;
                    if (configuration != null)
                    {
                        _instance = configuration;
                        return _instance;
                    }
                }

                // no configuration found. create new at default location                 
                System.IO.Directory.CreateDirectory("Assets/MarkLight/Configuration/");
                configuration = ScriptableObject.CreateInstance<Configuration>();
                AssetDatabase.CreateAsset(configuration, "Assets/MarkLight/Configuration/Configuration.asset");
                AssetDatabase.Refresh();
                    
                _instance = configuration;              
                return _instance.GetValidated();
            }
        }

        #endregion
    }
}
