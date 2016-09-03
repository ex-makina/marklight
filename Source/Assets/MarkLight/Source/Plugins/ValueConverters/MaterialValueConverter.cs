#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MarkLight.ValueConverters
{
    /// <summary>
    /// Value converter for Material type.
    /// </summary>
    public class MaterialValueConverter : AssetValueConverter
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public MaterialValueConverter()
        {
            _type = typeof(Material);
            _loadType = _type;
            IsUnityAssetType = false;
        }

        #endregion
    }
}
