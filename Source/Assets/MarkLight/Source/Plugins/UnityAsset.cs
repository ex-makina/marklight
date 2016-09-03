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
    /// Contains information about a unity asset.
    /// </summary>
    [Serializable]
    public class UnityAsset
    {
        #region Fields

        public UnityEngine.Object Asset;
        public string Path;

        private Dictionary<int, WeakReference> _assetLoadObservers;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public UnityAsset(string path, UnityEngine.Object asset)
        {
            Asset = asset;
            Path = path;
        }
         
        #endregion

        #region Methods

        /// <summary>
        /// Adds view to be notified when load/unloading occurs.
        /// </summary>
        public void AddObserver(View view)
        {
            if (_assetLoadObservers == null)
            {
                _assetLoadObservers = new Dictionary<int, WeakReference>();
            }

            int hashCode = view.GetHashCode();
            if (_assetLoadObservers.ContainsKey(hashCode))
                return;

            _assetLoadObservers.Add(hashCode, new WeakReference(view));
        }

        /// <summary>
        /// Notifies observers that a asset has been loaded/unloaded.
        /// </summary>
        public void NotifyObservers()
        {
            if (_assetLoadObservers == null)
            {
                _assetLoadObservers = new Dictionary<int, WeakReference>();
            }

            List<int> observersToRemove = new List<int>();
            foreach (var keyValue in _assetLoadObservers)
            {
                if (!keyValue.Value.IsAlive)
                {
                    observersToRemove.Add(keyValue.Key);
                    continue;
                }

                View view = keyValue.Value.Target as View;
                view.OnAssetChanged(this);
            }

            // remove observers that are not alive anymore
            if (observersToRemove.Count > 0)
            {
                foreach (var key in observersToRemove)
                {
                    _assetLoadObservers.Remove(key);
                }
            }
        }

        /// <summary>
        /// Converts element size to string.
        /// </summary>
        public override string ToString()
        {
            return Path;
        }

        /// <summary>
        /// Unload sprite.
        /// </summary>
        public void Unload()
        {
            if (!IsLoaded)
                return;

            //var sprite = Sprite;
            Asset = null;
            NotifyObservers();

            // uncomment to ensure memory is released
            //if (asset != null)
            //{
            //    UnityEngine.Object.DestroyImmediate(asset, true);
            //}
        }

        /// <summary>
        /// Attempt to load asset.
        /// </summary>
        public bool Load(UnityEngine.Object asset)
        {
            Asset = asset;
            NotifyObservers();
            return IsLoaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets boolean indicating if the asset is loaded.
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return Asset != null;
            }
        }

        /// <summary>
        /// Gets asset converted to sprite.
        /// </summary>
        public Sprite Sprite
        {
            get
            {
                return Asset != null ? Asset as Sprite : null;
            }
        }

        #endregion
    }    
}
