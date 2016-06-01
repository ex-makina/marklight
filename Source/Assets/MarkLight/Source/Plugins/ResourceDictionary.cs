#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;
#endregion

namespace MarkLight
{
    /// <summary>
    /// A dictionary of values that can be bound to in XUML and changed programmatically.
    /// </summary>
    [Serializable]
    public class ResourceDictionary
    {
        #region Fields

        public string Name;
        public string Xuml;
        public List<Resource> Resources;

        public static string Language;
        public static string Platform;

        [NonSerialized]
        private XElement _xumlElement;
                
        private static bool _forceUpdateAllObservers;
        private static Dictionary<string, List<Resource>> _resourceLookupDictionary;
        private static Dictionary<string, List<WeakReference>> _resourceBindingObservers;
        private static HashSet<WeakReference> _observersToBeNotified;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ResourceDictionary()
        {
            Resources = new List<Resource>();
        }

        /// <summary>
        /// Initializes a static instance of the class.
        /// </summary>
        static ResourceDictionary()
        {
            _resourceBindingObservers = new Dictionary<string, List<WeakReference>>();
            _observersToBeNotified = new HashSet<WeakReference>();
            _resourceLookupDictionary = new Dictionary<string, List<Resource>>();
        }

        #endregion

        #region Methods
        
        /// <summary>
        /// Sets active dictionary configuration and notifies observers.
        /// </summary>
        public static void SetConfiguration(string language, string platform = null)
        {
            Language = language;
            Platform = platform;

            _forceUpdateAllObservers = true;
        }

        /// <summary>
        /// Notifies resource binding observers.
        /// </summary>
        public static void NotifyObservers(bool forceUpdateAllObservers = false)
        {
            // get all observers or just the ones tagged to be notified depending on forceUpdateAllObservers setting
            var observerRefs = forceUpdateAllObservers || _forceUpdateAllObservers ? _resourceBindingObservers.Values.SelectMany(x => x) : _observersToBeNotified;
            _forceUpdateAllObservers = false;

            // notify observers
            foreach (var observerRef in observerRefs)
            {
                if (!observerRef.IsAlive)
                    continue;
              
                var bindingObserver = observerRef.Target as BindingValueObserver;

                //Debug.Log("Notifying target view field: " + bindingObserver.Target.ViewFieldPath);
                bindingObserver.Notify(new HashSet<ViewFieldData>());
            }
            
            _observersToBeNotified.Clear();
        }

        /// <summary>
        /// Gets svalue from resource dictionary.
        /// </summary>
        public static string GetValue(string dictionaryName, string resourceKey, out bool hasValue)
        {
            hasValue = false;
            var fullResourceKey = GetFullResourceKey(dictionaryName, resourceKey);

            List<Resource> resources;
            if (!_resourceLookupDictionary.TryGetValue(fullResourceKey, out resources))
            {
                return null;
            }

            // resource  that best matches dictionary configuration
            int maxMatch = 0;
            Resource matchedResource = null;

            foreach (var resource in resources)
            {
                int match = GetResourceMatchValue(resource);
                if (match > maxMatch)
                {
                    matchedResource = resource;
                    maxMatch = match;
                    hasValue = true;
                }
            }

            // return matched resource value or empty string if none matched
            return matchedResource != null ? matchedResource.Value : String.Empty;            
        }

        /// <summary>
        /// Gets value indicating how close the resource matches the current dictionary settings.
        /// </summary>
        private static int GetResourceMatchValue(Resource resource)
        {
            int match = 0;

            // matches according to the following rules: 
            // a resource parameter (platform or language) can give half match or full match against the dictionary configuration
            // an empty value in the resource indicates wildcard which gives half match unless the configuration is also empty which means a full match
            // if a resource parameter is set and the configuration is the same it's a full match otherwise a mismatch

            // compare language 
            if (String.IsNullOrEmpty(resource.Language))
            {
                if (String.IsNullOrEmpty(Language))
                {
                    match += 2;
                }
                else
                {
                    match += 1; // wildcard match                    
                }
            }
            else if (String.Equals(resource.Language, Language, StringComparison.OrdinalIgnoreCase))
            {
                match += 2;
            }
            else
            {
                // mismatch. language set in resource but not in dictionary config
                return -1; 
            }

            // compare platform
            if (String.IsNullOrEmpty(resource.Platform))
            {
                if (String.IsNullOrEmpty(Platform))
                {
                    match += 2;
                }
                else
                {
                    match += 1; // wildcard match                    
                }
            }
            else if (String.Equals(resource.Platform, Platform, StringComparison.OrdinalIgnoreCase))
            {
                match += 2;
            }
            else
            {
                // mismatch. platform set in resource but not in dictionary config
                return -1;
            }

            return match;
        }

        /// <summary>
        /// Sets/adds a resource in the runtime resource dictionary.
        /// </summary>
        public static void SetResource(string dictionaryName, string resourceKey, string value, string language = null, string platform = null)
        {
            SetResource(dictionaryName, new Resource { Key = resourceKey, Value = value, Language = language, Platform = platform });
        }

        /// <summary>
        /// Sets/adds a resource in the runtime resource dictionary.
        /// </summary>
        public static void SetResource(string dictionaryName, Resource resource)
        {
            var fullResourceKey = GetFullResourceKey(dictionaryName, resource.Key);
            if (!_resourceLookupDictionary.ContainsKey(fullResourceKey))
            {
                _resourceLookupDictionary.Add(fullResourceKey, new List<Resource>());
            }

            // if resource is in dictionary only update its value
            var existingResource = _resourceLookupDictionary[fullResourceKey].FirstOrDefault(x => x.Equals(resource));
            if (existingResource != null)
            {
                existingResource.Value = resource.Value;
            }
            else
            {
                _resourceLookupDictionary[fullResourceKey].Add(resource);
            }

            // does this resource have observers? 
            if (_resourceBindingObservers.ContainsKey(fullResourceKey))
            {
                // yes. add them to list to be notified
                _observersToBeNotified.AddRange(_resourceBindingObservers[fullResourceKey]);
            }
        }

        /// <summary>
        /// Registers a resource binding observer. 
        /// </summary>
        public static void RegisterResourceBindingObserver(string dictionaryName, string resourceKey, BindingValueObserver bindingValueObserver)
        {
            var fullResourceKey = GetFullResourceKey(dictionaryName, resourceKey);
            if (!_resourceBindingObservers.ContainsKey(fullResourceKey))
            {
                _resourceBindingObservers.Add(fullResourceKey, new List<WeakReference>());
            }

            var observerRef = new WeakReference(bindingValueObserver);
            _resourceBindingObservers[fullResourceKey].Add(new WeakReference(bindingValueObserver));
            _observersToBeNotified.Add(observerRef);            
        }

        /// <summary>
        /// Adds resource to the compile-time dictionary. Called when resources are loaded from XUML.
        /// </summary>
        public void AddResources(List<Resource> resources)
        {
            Resources.AddRange(resources);

            if (Application.isPlaying)
            {
                // if XUML is loaded during run-time we need to update runtime dictionary as well
                foreach (var resource in resources)
                {
                    SetResource(Name, resource);
                }
            }
        }

        /// <summary>
        /// Called once at startup and initializes the runtime dictionary.
        /// </summary>
        public static void Initialize()
        {
            _resourceLookupDictionary = new Dictionary<string, List<Resource>>();
            foreach (var resourceDictionary in ViewPresenter.Instance.ResourceDictionaries)
            {
                foreach (var resource in resourceDictionary.Resources)
                {
                    SetResource(resourceDictionary.Name, resource);
                }
            }
        }

        /// <summary>
        /// Gets full resource key from dictionary name and resource key.
        /// </summary>
        public static string GetFullResourceKey(string dictionaryName, string resourceKey)
        {
            return String.IsNullOrEmpty(dictionaryName) ? resourceKey : String.Format("{0}.{1}", dictionaryName, resourceKey);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets XUML element.
        /// </summary>
        public XElement XumlElement
        {
            get
            {
                if (_xumlElement == null && !String.IsNullOrEmpty(Xuml))
                {
                    try
                    {
                        _xumlElement = XElement.Parse(Xuml);
                    }
                    catch
                    {
                    }
                }

                return _xumlElement;
            }
            set { _xumlElement = value; }
        }

        #endregion
    }
}
