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
                    return _instance;
                
                // check default directory
                Configuration configuration = AssetDatabase.LoadAssetAtPath("Assets/MarkLight/Configuration/Configuration.asset", typeof(Configuration)) as Configuration;
                if (configuration != null)
                {
                    _instance = configuration;
                    return _instance;
                }

                // search for configuration
                var guid = AssetDatabase.FindAssets("t:Configuration name:Configuration", null).FirstOrDefault();
                if (!String.IsNullOrEmpty(guid))
                {
                    configuration = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guid), typeof(Configuration)) as Configuration;
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
                return _instance;
            }
        }

        #endregion
    }
}
