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
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents the base class of all collections.
    /// </summary>
    /// <typeparam name="T">The generic parameter derived from Resource</typeparam>
    public class ResourceCollection<T> : Resource, IEnumerable<T> where T : Resource
    {
        /// <summary>
        /// The object Type of the class, a derived class of Resource.
        /// </summary>
        private Type itemClass;

        /// <summary>
        /// The item signature of the constructors.
        /// </summary>
        private static Type[] itemSig = new Type[] 
        { 
            typeof(Service), typeof(string) 
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path of the resource</param>
        /// <param name="itemClass">The object type</param>
        public ResourceCollection(Service service, string path, Type itemClass) 
            : base(service, path) 
        {
            this.itemClass = itemClass;
            this.Items = new Dictionary<string, List<T>>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceCollection"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path of the resource</param>
        /// <param name="args">The variable arguments</param>
        /// <param name="itemClass">The object type</param>
        public ResourceCollection(Service service, string path, Args args, Type itemClass) 
            : base(service, path, args) 
        {
            this.itemClass = itemClass;
            this.Items = new Dictionary<string, List<T>>();
        }

        /// <summary>
        /// Gets a value indicating whether the IDictionary has a fixed size.
        /// </summary>
        public bool IsFixedSize 
        { 
            get 
            { 
                return false; 
            } 
        }

        /// <summary>
        /// Gets a value indicating whether the IDictionary is read-only. 
        /// </summary>
        public bool IsReadOnly 
        { 
            get 
            { 
                return false; 
            } 
        }

        /// <summary>
        /// Gets or sets the items in the collection. Because of namespace rules, a key-name may result
        /// in multiple Entities, thus the key resolves to a list of one or more Entities.
        /// </summary>
        protected Dictionary<string, List<T>> Items 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets an ICollection of keys in the collection of resources. Note that if the 
        /// local resource collection is dirty, will refresh an up-to-date copy from the
        /// server.
        /// </summary>
        public ICollection<T> Keys 
        {
            get 
            {
                List<T> collection = new List<T>();
                this.Validate();
                Dictionary<string, List<T>>.KeyCollection keySet = this.Items.Keys;
                foreach (string key in keySet) 
                {
                    List<T> list = this.Items[key];
                    foreach (T item in list) 
                    {
                        collection.Add(item);
                    }
                }
                return collection;
            }
        }

        /// <summary>
        /// Gets the Number of elements in the collection.
        /// </summary>
        /// <returns>The number of elements in the collection.</returns>
        public int Size 
        {
            get
            {
                return this.Validate().Items.Count;
            }
        }

        /// <summary>
        /// Gets an ICollection of values in the collection of resources. Note that if the 
        /// local resource collection is dirty, will refresh an up-to-date copy from the
        /// server.
        /// </summary>
        public ICollection<T> Values 
        {
            get 
            {
                List<T> collection = new List<T>();
                this.Validate();
                Dictionary<string, List<T>>.KeyCollection keySet = this.Items.Keys;
                foreach (string key in keySet) 
                {
                    List<T> list = this.Items[key];
                    foreach (T item in list) 
                    {
                        collection.Add(item);
                    }
                }
                return collection;
            }
        }

        /// <summary>
        /// Unsupported. Add's a value to the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void Add(object key, T value) 
        {
            throw new Exception("Add unsupported");
        }

        /// <summary>
        /// Returns the typed enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator</returns>
        public IEnumerator<T> GetEnumerator() 
        {
            return this.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns the generic enumerator for this collection.
        /// </summary>
        /// <returns>The enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator() 
        {
            return this.GetEnumerator();
        } 

        /// <summary>
        /// Unsupported. Clear the collection.
        /// </summary>
        public void Clear() 
        {
            throw new Exception("Clear not supported");
        }

        /// <summary>
        /// Returns a value indicating whether or not the collection of 
        /// Resources contains the desired key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True or false</returns>
        public bool ContainsKey(object key) 
        {
            return this.Validate().Items.ContainsKey((string)key);
        }

        /// <summary>
        /// Returns a value indicating whether or not the collection of 
        /// Resources contains the desired key, qualified by namespace.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="splunkNamespace">The namespace</param>
        /// <returns>True or false</returns>
        public bool ContainsKey(object key, Args splunkNamespace) 
        {
            this.Validate();
            List<T> entities = this.Items[(string)key];
            if (entities.Count == 0) 
            {
                return false;
            }
            string pathMatcher = this.Service.Fullpath(string.Empty, splunkNamespace);
            foreach (T entity in entities) 
            {
                if (entity.Path.StartsWith(pathMatcher))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns a value indicating whether or not the value exists in the collection. 
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns>True or false</returns>
        public bool ContainsValue(T value) 
        {
            foreach (object key in this.Keys) 
            {
                if (this.Items[(string)key].Contains(value)) 
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Creates a collection member.
        /// </summary>
        /// <param name="itemClass">The object type being created</param>
        /// <param name="path">The path to the resource</param>
        /// <param name="splunkNamespace">The namespace</param>
        /// <returns>The new object, of type T</returns>
        public T CreateItem(Type itemClass, string path, Args splunkNamespace) 
        {
            ConstructorInfo ctor = itemClass.GetConstructor(itemSig);
            T item = (T)ctor.Invoke(new object[] { Service, Service.Fullpath(path, splunkNamespace) });
            return item;
        }

        /// <summary>
        /// Creates a collection member corresponding to a given
        /// Atom entry. This base implementation uses the class object that was
        /// passed in when the generic ResourceCollection was created.
        /// Subclasses may override this method to provide alternative means of
        /// instantiating collection items.
        /// </summary>
        /// <param name="entry">The AtomEntry</param>
        /// <returns>The new object, of type T</returns>
        protected virtual T CreateItem(AtomEntry entry) 
        {
            return this.CreateItem(this.itemClass, this.ItemPath(entry), this.SplunkNamespace(entry));
        }

        /// <summary>
        /// Returns a avlue indicating whether or not the collections are equal.
        /// </summary>
        /// <param name="o">The obect to compare against</param>
        /// <returns>True or false</returns>
        public override bool Equals(object o) 
        {
            return this.Validate().Items.Equals(o);
        }

        /// <summary>
        /// Gets the object in the collection, given the key. If there
        /// are more than one Resource identifed by the key, an AMBIGUOUS
        /// exception is thrown. In order to disambiguate Resources, a
        /// namespace must be supplied.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The object, or default(object) if not found.</returns>
        public T Get(object key) 
        {
            this.Validate();
            if (!this.Items.ContainsKey((string)key)) 
            {
                return default(T);
            }
            List<T> entities = this.Items[(string)key];
            if (entities.Count > 1) 
            {
                throw new SplunkException(SplunkException.AMBIGUOUS, "Key has multiple values, specify a namespace");
            }
            if (entities.Count == 0) 
            {
                return default(T);
            }
            return entities[0];
        }

        /// <summary>
        /// Gets the object in the collection, given the key, with a scoped
        /// name-spaced constraint.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="splunkNamespace">The namespace</param>
        /// <returns>The object, or default(object) if not found.</returns>
        public T Get(object key, Args splunkNamespace) 
        {
            this.Validate();
            if (!this.Items.ContainsKey((string)key)) 
            {
                return default(T);
            }
            List<T> entities = this.Items[(string)key];
            if (entities.Count == 0) 
            {
                return default(T);
            }
            string pathMatcher = Service.Fullpath(string.Empty, splunkNamespace);
            foreach (T entity in entities) 
            {
                if (entity.Path.StartsWith(pathMatcher)) 
                {
                    return entity;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Returns an up-to-date Hashcode.
        /// </summary>
        /// <returns>The hash code</returns>
        public override int GetHashCode() 
        {
            return this.Validate().Items.GetHashCode();
        }

        /// <summary>
        /// Gets a value indicating whether the up-to-date Resource count is 0.
        /// </summary>
        /// <returns>True or false</returns>
        public bool IsEmpty 
        {
            get
            {
                return this.Validate().Items.Count == 0;
            }
        }
    
        /// <summary>
        /// Returns the value to use as the item key from a given Atom entry.
        /// Subclasses may override this value for collections that use something
        /// other than title as the key.
        /// </summary>
        /// <param name="entry">The AtomEntry</param>
        /// <returns>The title</returns>
        protected virtual string ItemKey(AtomEntry entry) 
        {
            return entry.Title;
        }

        /// <summary>
        /// Returns the value to use as the item path from a given Atom entry.
        /// Subclasses may override this value to support alternative methods of
        /// determining a member's path.
        /// </summary>
        /// <param name="entry">The AtomEntry</param>
        /// <returns>The alternate path</returns>
        protected virtual string ItemPath(AtomEntry entry) 
        {
            return entry.Links["alternate"];
        }

        /// <summary>
        /// Returns the namesapce of an AtomEntry based on the eai::acl field.
        /// </summary>
        /// <param name="entry">The AtomEntry</param>
        /// <returns>The namespace</returns>
        private Args SplunkNamespace(AtomEntry entry) 
        {
            Args splunkNamespace = new Args();

            // no content? return an empty namespace.
            if (entry.Content == null) 
            {
                return splunkNamespace;
            }

            Dictionary<string, object> entityMetadata =
                (Dictionary<string, object>)entry.Content["eai:acl"];
            if (entityMetadata.ContainsKey("owner")) 
            {
                splunkNamespace.AlternateAdd("owner", entityMetadata["owner"]);
            }
            if (entityMetadata.ContainsKey("app")) 
            {
                splunkNamespace.AlternateAdd("app", entityMetadata["app"]);
            }
            if (entityMetadata.ContainsKey("sharing")) 
            {
                splunkNamespace.AlternateAdd("sharing", entityMetadata["sharing"]);
            }
            return splunkNamespace;
        }

        /// <summary>
        /// Issues an HTTP request to list the contents of the collection resource.
        /// </summary>
        /// <returns>The contents of the collection ResponseMessage format</returns>
        public virtual ResponseMessage List()
        {
            return this.Service.Get(this.Path, this.RefreshArgs);
        }

        /// <summary>
        /// Loads the collection resource from a given Atom feed.
        /// </summary>
        /// <param name="value">The AtomFeed</param>
        /// <returns>The resource collection</returns>
        private ResourceCollection<T> Load(AtomFeed value) 
        {
            base.Load(value);
            foreach (AtomEntry entry in value.Entries) 
            {
                string key = this.ItemKey(entry);
                T item = this.CreateItem(entry);
                if (this.Items.ContainsKey(key)) 
                {
                    List<T> list = this.Items[key];
                    list.Add(item);
                } 
                else 
                {
                    List<T> list = new List<T>();
                    list.Add(item);
                    this.Items.Add(key, list);
                }
            }
            return this;
        }

        /// <summary>
        /// Refresh the resource collection
        /// </summary>
        /// <returns>The resource</returns>
        public override Resource Refresh() 
        {
            this.Items.Clear();
            ResponseMessage response = this.List();
            /* assert(response.getStatus() == 200); */
            AtomFeed feed = AtomFeed.Parse(response.Content);
            this.Load(feed);
            return this;
        }

        /// <summary>
        /// Unsupported. Remove an element from the resource collection.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>Throws an exception</returns>
        public T Remove(object key) 
        {
            throw new Exception("Remove unsupported");
        }

        /// <summary>
        /// Validates the collection. If dirty, will refresh.
        /// </summary>
        /// <returns>The collection</returns>
        public new ResourceCollection<T> Validate() 
        {
            base.Validate();
            return this;
        }

        /// <summary>
        /// Returns the number of elements in the list of a
        /// specific key in the collection.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The number of elements</returns>
        public int ValueSize(object key) 
        {
            this.Validate();
            if (!this.Items.ContainsKey("key"))
            {
                return 0;
            }
            List<T> entities = this.Items[(string)key];
            return entities.Count;
        }
    }
}
