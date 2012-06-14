/*
 * Copyright 2012 Splunk, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"): you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

namespace Splunk
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the base class for all Splunk Entity resources
    /// </summary>
    public class Entity : Resource
    {
        /// <summary>
        /// The content that makes up the Entity.
        /// </summary>
        private Record content;

        /// <summary>
        /// The local cache of value to update when either update methods are called.
        /// </summary>
        private Dictionary<string, object> toUpdate = new Dictionary<string, object>();

        /// <summary>
        ///  Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The resource's path</param>
        public Entity(Service service, string path) 
            : base(service, path) 
        {
        }

        /// <summary>
        /// Gets a value indicating whether or not the content is empty. Note
        /// that if dirty, this has the side effect of retrieving refreshed
        /// data from the server.
        /// </summary>
        public bool IsEmpty 
        {
            get 
            {
                return this.GetContent().Count != 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this entity is disabled. This method is 
        /// available on almost every endpoint. Note
        /// that if dirty, this has the side effect of retrieving refreshed
        /// data from the server.
        /// </summary>
        public bool IsDisabled 
        {
            get 
            {
                return this.GetBoolean("disabled", false);
            }
        }

        /// <summary>
        /// Gets the keys of the content. Note
        /// that if dirty, this has the side effect of retrieving refreshed
        /// data from the server.
        /// </summary>
        /// <returns>The keys</returns>
        public ICollection<string> KeySet 
        {
            get 
            {
                return this.GetContent().Keys;
            }
        }

        /// <summary>
        /// Gets the number of elements in the content. Note
        /// that if dirty, this has the side effect of retrieving refreshed
        /// data from the server.
        /// </summary>
        public int Size 
        {
            get 
            {
                return this.GetContent().Count;
            }
        }

        /// <summary>
        /// Returns the path that corresponds to the requested action.
        /// </summary>
        /// <param name="action">The action</param>
        /// <returns>The path for the desired action</returns>
        protected virtual string ActionPath(string action) 
        {
            if (action.Equals("disable")) 
            {
                return this.Path + "/disable";
            }
            if (action.Equals("edit")) 
            {
                return this.Path;
            }
            if (action.Equals("enable")) 
            {
                return this.Path + "/enable";
            }
            if (action.Equals("reload")) 
            {
                return this.Path + "/_reload";
            }
            if (action.Equals("remove")) 
            {
                return this.Path;
            }
            throw new Exception("Invalid action: " + action);
        }

        /// <summary>
        /// Determines whether the key exists in the content data.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True or False.</returns>
        public bool ContainsKey(object key) 
        {
            return this.GetContent().ContainsKey((string)key);
        }

        /// <summary>
        /// Determines whether the value exists in the content data.
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>True or False.</returns>
        public bool ContainsValue(object value) 
        {
            return this.GetContent().ContainsValue(value);
        }

        /// <summary>
        /// Disables the entity that is named by this endpoint. This method
        /// is available an almost every endpoint.
        /// </summary>
        public void Disable() 
        {
            this.Service.Post(this.ActionPath("disable"));
            this.Invalidate();
        }

        /// <summary>
        /// Enables the entity that is named by this endpoint. This method
        /// is available an almost every endpoint.
        /// </summary>
        public void Enable() 
        {
            this.Service.Post(this.ActionPath("enable"));
            this.Invalidate();
        }

        /// <summary>
        /// Gets the value associated with the key out of the content of 
        /// this resource.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public object Get(object key) 
        {
            if (this.toUpdate.ContainsKey((string)key)) 
            {
                return this.toUpdate[(string)key];
            }
            return this.GetContent()[(string)key];
        }

        /// <summary>
        /// Returns the Boolean value associated with the specified key. Values
        /// can be converted from: 0, 1, true, false.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True or False</returns>
        public bool GetBoolean(string key) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToBoolean((string)this.toUpdate[key]);
            }
            return Value.ToBoolean((string)this.GetContent()[key]);
        }

        /// <summary>
        /// Returns the Boolean value associated with the specified key, or the
        /// default value if the key does not exist. Boolean values can be converted
        /// from: 0, 1, true, false.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default value if key not found</param>
        /// <returns>The value</returns>
        public bool GetBoolean(string key, bool defaultValue) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToBoolean((string)this.toUpdate[key]);
            }
            return Value.ToBoolean((string)this.GetContent()[key]);
        }

        /// <summary>
        /// Returns the long value associated with the specified key. Long values
        /// can be converted from: number, numberMB, numberGB.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public long GetByteCount(string key) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToByteCount((string)this.toUpdate[key]);
            }
            return Value.ToByteCount((string)this.GetContent()[key]);
        }

        /// <summary>
        ///  Returns the long value associated with the specified key, or the default
        /// value if the key does not exist. Long values can be converted from: 
        /// number, numberMB, numberGB.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The value if the key is not found</param>
        /// <returns>The value</returns>
        public long GetByteCount(string key, long defaultValue) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToByteCount((string)this.toUpdate[key]);
            }
            return this.GetContent().GetByteCount(key, defaultValue);
        }

        /// <summary>
        /// Returns the validated (e.g. refreshed if dirty) content.
        /// </summary>
        /// <returns>The content</returns>
        private Record GetContent() 
        {
            return this.Validate().content;
        }

        /**
         * Returns a date value associated with the specified key. Date values can
         * be converted from standard UTC time formats.
         *
         * @param key The key to look up.
         * @return The date value associated with the specified key.
         
        Date GetDate(string key) {
            if (toUpdate.containsKey(key))
                return Value.toDate(toUpdate.get(key).toString());
            return GetContent().getDate(key);
        }
        */

        /**
         * Returns a date value associated with the specified key, or the default
         * value if the key does not exist. Date values can be converted from
         * standard UTC time formats.
         *
         * @param key The key to look up.
         * @param defaultValue The default value.
         * @return The date value associated with the specified key.
        Date GetDate(string key, Date defaultValue) {
            if (toUpdate.containsKey(key))
                return Value.toDate(toUpdate.get(key).toString());
            return GetContent().getDate(key, defaultValue);
        }
         */

        /// <summary>
        /// Returns the double-floating point value associated with the specified key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public double GetFloat(string key) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToFloat((string)this.toUpdate[key]);
            }
            return this.GetContent().GetFloat(key);
        }

        /// <summary>
        /// Returns the integer point value associated with the specified key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public int GetInteger(string key) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToInteger((string)this.toUpdate[key]);
            }
            return this.GetContent().GetInteger(key);
        }

        /// <summary>
        /// Returns the integer value associated with the specified key or the default
        /// value if the key does not exist.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The value if key not present</param>
        /// <returns>The value</returns>
        public int GetInteger(string key, int defaultValue) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToInteger((string)this.toUpdate[key]);
            }
            return this.GetContent().GetInteger(key, defaultValue);
        }

        /// <summary>
        ///  Returns the long value associated with the specified key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public long GetLong(string key) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToLong((string)this.toUpdate[key]);
            }
            return this.GetContent().GetLong(key);
        }

        /// <summary>
        /// Returns the long value associated with the specified key or the default
        /// value if the key does not exist.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The value of key is not present</param>
        /// <returns>The value</returns>
        public long GetLong(string key, int defaultValue) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return Value.ToLong((string)this.toUpdate[key]);
            }
            return this.GetContent().GetLong(key, defaultValue);
        }

        /**
         * Returns the metadata (eai:acl) of this entity. This data includes
         * permissions for accessing the resource, and values that indicate 
         * which resource fields are wildcards, required, and optional.
         *
         * @return The metadata of this entity, or {@code null} if none exist.
        public EntityMetadata GetMetadata() {
            // CONSIDER: For entities that don't have an eai:acl field, which is
            // uncommon but does happen at least in the case of a DeploymentClient
            // that is not enabled, we return null. A slightly friendlier option
            // would be to return a metadata instance that defaults all values?
            if (!containsKey("eai:acl")) return null;
            return new EntityMetadata(this);
        }
         */

        /// <summary>
        /// Returns the string value associated with the specified key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public string GetString(string key) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return (string)this.toUpdate[key];
            }
            return this.GetContent().GetString(key);
        }

        /// <summary>
        /// Returns the string value associated with the specified key, or the
        /// default value if the key is not found.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The value if the key is not found</param>
        /// <returns>The value</returns>
        public string GetString(string key, string defaultValue) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return (string)this.toUpdate[key];
            }
            return this.GetContent().GetString(key, defaultValue);
        }

        /// <summary>
        ///  Returns the string array value associated with the specified key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public string[] GetStringArray(string key) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return this.GetStringArray((string)this.toUpdate[key]);
            }
            return this.GetContent().GetStringArray(key);
        }

        /// <summary>
        ///  Returns the string array value associated with the specified key, or the
        ///  default value if the key does not exist.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The value if key is not found</param>
        /// <returns>The value</returns>
        public string[] GetStringArray(string key, string[] defaultValue) 
        {
            if (this.toUpdate.ContainsKey(key)) 
            {
                return this.GetStringArray((string)this.toUpdate[key]);
            }
            return this.GetContent().GetStringArray(key, defaultValue);
        }

        /// <summary>
        /// Loads the Entity with the AtomObject data.
        /// </summary>
        /// <param name="value">The AtomObject</param>
        /// <returns>The filled Entity</returns>
        public new Entity Load(AtomObject value) 
        {
            base.Load(value);
            AtomEntry entry = (AtomEntry)value;
            if (entry == null) 
            {
                this.content = new Record();
            }
            else 
            {
                this.content = entry.Content;
            }
            return this;
        }

        /// <summary>
        /// Refresh the Entity by calling the servers GET method on the endpoint.
        /// </summary>
        /// <returns>The refreshed resource</returns>
        public override Resource Refresh() 
        {
            // Update any attribute values set by a setter method that has not
            // yet been written to the object.
            ResponseMessage response = Service.Get(Path);
            /* assert(response.getStatus() == 200); */
            AtomFeed feed = AtomFeed.Parse(response.Content);
            int count = feed.Entries.Count;
            /* assert(count == 0 || count == 1); */
            AtomEntry entry = (count == 0) ? null : feed.Entries[0];
            this.Load(entry);
            return this;
        }

        /// <summary>
        /// Performs this entity's reload action.
        /// </summary>
        public void Reload() 
        {
            this.Service.Get(this.ActionPath("reload"));
            this.Invalidate();
        }

        /// <summary>
        /// Removes an element from the content of this entity. Not supported.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>Always throws an exception</returns>
        public object Remove(object key) 
        {
            throw new Exception("Remove unsupported");
        }

        /// <summary>
        /// sets the local cache update value. Writing to the server is deferred until
        /// the Update() method is called.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void SetCacheValue(string key, object value) 
        {
            this.toUpdate.Add(key, value);
        }

        /// <summary>
        /// Updates the entity with the values you previously set using the setter
        /// methods, and any additional specified arguments. The specified arguments
        /// take precedent over the values that were set using the setter methods.
        /// </summary>
        /// <param name="args">The key/value pairs to update</param>
        public void Update(Dictionary<string, object> args) 
        {
            // Merge cached setters and live args together before updating.
            Dictionary<string, object> mergedArgs = new Dictionary<string, object>();
            mergedArgs.Concat(this.toUpdate);
            mergedArgs.Concat(args);
            this.Service.Post(this.ActionPath("edit"), mergedArgs);
            this.toUpdate.Clear();
            this.Invalidate();
        }

        /// <summary>
        /// Updates the entity with the accumulated arguments, established by the
        /// individual setter methods for each specific entity class.
        /// </summary>
        public void Update() 
        {
            if (this.toUpdate.Count > 0) 
            {
                this.Service.Post(this.ActionPath("edit"), this.toUpdate);
                this.toUpdate.Clear();
                this.Invalidate();
            }
        }

        /// <summary>
        /// Removes this entity from its corresponding collection.
        /// </summary>
        public virtual void Remove() 
        {
            this.Service.Delete(this.ActionPath("remove"));
        }

        /// <summary>
        /// Validates the current Entity.
        /// </summary>
        /// <returns>The validated Entity</returns>
        public new Entity Validate() 
        { 
            base.Validate(); 
            return this;
        }

        /// <summary>
        /// Returns all the values from the content of this Entity.
        /// </summary>
        /// <returns>The values</returns>
        public Dictionary<string, object>.ValueCollection Values() 
        {
            return this.GetContent().Values;
        }
    }
}
