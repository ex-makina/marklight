#region Using Statements
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// Contains information about a unity sprite asset.
    /// </summary>
    [Serializable]
    public class SpriteAsset
    {
        #region Fields

        public UnityAsset UnityAsset;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public SpriteAsset(UnityAsset unityAsset)
        {
            UnityAsset = unityAsset;
        }

        #endregion

        #region Property

        /// <summary>
        /// Gets unity asset sprite.
        /// </summary>
        public Sprite Sprite
        {
            get
            {
                return UnityAsset != null ? UnityAsset.Sprite : null;
            }
        }

        /// <summary>
        /// Gets unity asset path.
        /// </summary>
        public string Path
        {
            get
            {
                return UnityAsset != null ? UnityAsset.Path : null;
            }
        }

        #endregion
    }    
}
