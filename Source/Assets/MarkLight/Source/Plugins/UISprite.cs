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
    /// Represents a sprite.
    /// </summary>
    [Serializable]
    public class UISprite
    {
        #region Fields

        public Sprite Sprite;
        public string Path;

        private Dictionary<int, WeakReference> SpriteLoadObservers;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public UISprite(Sprite sprite, string path)
        {
            Sprite = sprite;
            Path = path;
        }
         
        #endregion

        #region Methods

        /// <summary>
        /// Adds view to be notified when load/unloading occurs.
        /// </summary>
        public void AddObserver(View view)
        {
            if (SpriteLoadObservers == null)
            {
                SpriteLoadObservers = new Dictionary<int, WeakReference>();
            }

            int hashCode = view.GetHashCode();
            if (SpriteLoadObservers.ContainsKey(hashCode))
                return;

            SpriteLoadObservers.Add(hashCode, new WeakReference(view));
        }

        /// <summary>
        /// Notifies observers that a sprite has been loaded/unloaded.
        /// </summary>
        public void NotifyObservers()
        {
            if (SpriteLoadObservers == null)
            {
                SpriteLoadObservers = new Dictionary<int, WeakReference>();
            }

            List<int> observersToRemove = new List<int>();
            foreach (var keyValue in SpriteLoadObservers)
            {
                if (!keyValue.Value.IsAlive)
                {
                    observersToRemove.Add(keyValue.Key);
                    continue;
                }

                View view = keyValue.Value.Target as View;
                view.OnSpriteChanged(this);
            }

            // remove observers that are not alive anymore
            if (observersToRemove.Count > 0)
            {
                foreach (var key in observersToRemove)
                {
                    SpriteLoadObservers.Remove(key);
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
            Sprite = null;
            NotifyObservers();

            // uncomment to ensure memory is released
            //if (sprite != null)
            //{
            //    UnityEngine.Object.DestroyImmediate(sprite, true);
            //}
        }

        /// <summary>
        /// Attempt to load sprite.
        /// </summary>
        public bool Load(Sprite sprite)
        {
            Sprite = sprite;
            NotifyObservers();
            return IsLoaded;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets boolean indicating if the sprite is loaded.
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return Sprite != null;
            }
        }

        #endregion
    }
}
