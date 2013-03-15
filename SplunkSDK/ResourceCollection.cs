/*
 * Copyright 2013 Splunk, Inc.
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
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// The <see cref="ResourceCollection{T}"/> class represents the base class
    /// of all collections.
    /// </summary>
    /// <typeparam name="T">The generic parameter derived from <see
    /// cref="Resource"/>.
    /// </typeparam>
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
        /// Initializes a new instance of the 
        /// <see cref="ResourceCollection{T}"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="path">The path of the resource.</param>
        /// <param name="itemClass">The object type.</param>
        public ResourceCollection(Service service, string path, Type itemClass) 
            : base(service, path) 
        {
            this.itemClass = itemClass;
            this.Items = new OrderedResourceDictionary();
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ResourceCollection{T}"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="path">The path of the resource.</param>
        /// <param name="args">The variable arguments.</param>
        /// <param name="itemClass">The object type.</param>
        public ResourceCollection(
            Service service, string path, Args args, Type itemClass) 
            : base(service, path, args) 
        {
            this.itemClass = itemClass;
            this.Items = new OrderedResourceDictionary();
        }

        /// <summary>
        /// Gets a value that indicates whether the IDictionary has a fixed size.
        /// </summary>
        public bool IsFixedSize 
        { 
            get 
            { 
                return false; 
            } 
        }

        /// <summary>
        /// Gets a value that indicates whether the IDictionary is read-only. 
        /// </summary>
        public bool IsReadOnly 
        { 
            get 
            { 
                return false; 
            } 
        }

        /// <summary>
        /// Gets the items in the collection. Because of namespace 
        /// rules, a key-name may result in multiple Entities, thus the key 
        /// resolves to a list of one or more Entities.
        /// </summary>
        protected IDictionary<string, List<T>> Items 
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable"/> of keys in the collection of
        /// resources. Note that if the local resource collection is dirty, this
        /// will refresh an up-to-date copy from the server.
        /// </summary>
        public ICollection<string> Keys 
        {
            get 
            {
                return this.Validate().Items.Keys;
            }
        }

        /// <summary>
        /// Gets the number of elements in the collection.
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
        /// Gets an <see cref="IEnumerable"/> of values in the collection of
        /// resources. Note that if the local resource collection is dirty, this
        /// will refresh an up-to-date copy from the server.
        /// </summary>
        public ICollection<T> Values 
        {
            get 
            {
                var collection = new List<T>();
                foreach (var value in this.Validate().Items.Values) 
                {
                    foreach (var item in value) 
                    {
                        collection.Add(item);
                    }
                }
                return collection;
            }
        }

        /// <summary>
        /// Not supported. Adds a value to the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        public void Add(object key, T value) 
        {
            throw new NotSupportedException("Add unsupported");
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
        /// Returns a value that indicates whether or not the collection of 
        /// Resources contains the desired key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>True or false</returns>
        public virtual bool ContainsKey(object key) 
        {
            return this.Validate().Items.ContainsKey((string)key);
        }

        /// <summary>
        /// Returns a value that indicates whether or not the collection of 
        /// Resources contains the desired key, qualified by namespace.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="splunkNamespace">The namespace.</param>
        /// <returns>True or false.</returns>
        public virtual bool ContainsKey(object key, Args splunkNamespace) 
        {
            this.Validate();
            List<T> entities = this.Items[(string)key];
            if (entities.Count == 0) 
            {
                return false;
            }
            string pathMatcher = 
                this.Service.Fullpath(string.Empty, splunkNamespace);
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
        /// Returns a value that indicates whether or not the value exists in
        /// the collection. 
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>True or false.</returns>
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
        /// <param name="itemClass">The object type being created.</param>
        /// <param name="path">The path to the resource.</param>
        /// <param name="splunkNamespace">The namespace.</param>
        /// <returns>The new object, of type T.</returns>
        public T CreateItem(Type itemClass, string path, Args splunkNamespace) 
        {
            ConstructorInfo ctor = itemClass.GetConstructor(itemSig);
            T item = (T)ctor.Invoke(new object[] 
                { 
                    Service, Service.Fullpath(path, splunkNamespace) 
                });
            return item;
        }

        /// <summary>
        /// Creates a collection member corresponding to a given
        /// Atom entry. 
        /// </summary>
        /// <remarks>
        /// This base implementation uses the class object that was
        /// passed in when the generic <see cref="ResourceCollection"/> was
        /// created. Subclasses may override this method to provide alternative
        /// means of instantiating collection items.
        /// </remarks>
        /// <param name="entry">The AtomEntry</param>
        /// <returns>The new object, of type T</returns>
        protected virtual T CreateItem(AtomEntry entry) 
        {
            return this.CreateItem(
             this.itemClass, this.ItemPath(entry), this.SplunkNamespace(entry));
        }

        /// <summary>
        /// Returns a value that indicates whether the collections are equal.
        /// </summary>
        /// <param name="o">The object to compare against.</param>
        /// <returns>True or false.</returns>
        public override bool Equals(object o) 
        {
            return this.Validate().Items.Equals(o);
        }

        /// <summary>
        /// Gets the object in the collection, given the key. If there
        /// are more than one Resource identified by the key, an AMBIGUOUS
        /// exception is thrown. In order to disambiguate Resources, a
        /// namespace must be supplied.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The object, or default(object) if not found.</returns>
        public virtual T Get(object key) 
        {
            this.Validate();
            if (!this.Items.ContainsKey((string)key)) 
            {
                return default(T);
            }
            List<T> entities = this.Items[(string)key];
            if (entities.Count > 1) 
            {
                throw new SplunkException(
                    SplunkException.AMBIGUOUS, 
                    "Key has multiple values, specify a namespace");
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
        /// <param name="key">The key.</param>
        /// <param name="splunkNamespace">The namespace.</param>
        /// <returns>The object, or default(object) if not found.</returns>
        public virtual T Get(object key, Args splunkNamespace) 
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
            string pathMatcher;
            pathMatcher = Service.Fullpath(string.Empty, splunkNamespace);
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
        /// <returns>The hash code.</returns>
        public override int GetHashCode() 
        {
            return this.Validate().Items.GetHashCode();
        }

        /// <summary>
        /// Gets a value that indicates whether the up-to-date Resource count
        /// is 0.
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
        /// Subclasses may override this value for collections that use 
        /// something other than title as the key.
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
        /// <param name="entry">The AtomEntry.</param>
        /// <returns>The alternate path.</returns>
        protected virtual string ItemPath(AtomEntry entry) 
        {
            return entry.Links["alternate"];
        }

        /// <summary>
        /// Returns the namesapce of an AtomEntry based on the eai::acl field.
        /// </summary>
        /// <param name="entry">The AtomEntry.</param>
        /// <returns>The namespace.</returns>
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
                splunkNamespace.AlternateAdd(
                    "owner", entityMetadata["owner"]);
            }
            if (entityMetadata.ContainsKey("app")) 
            {
                splunkNamespace.AlternateAdd(
                    "app", entityMetadata["app"]);
            }
            if (entityMetadata.ContainsKey("sharing")) 
            {
                splunkNamespace.AlternateAdd(
                    "sharing", entityMetadata["sharing"]);
            }
            return splunkNamespace;
        }

        /// <summary>
        /// Issues an HTTP request to list the contents of the collection 
        /// resource.
        /// </summary>
        /// <returns>The contents of the collection <see
        /// cref="ResponseMessage"/> format.
        /// </returns>
        public virtual ResponseMessage List()
        {
            return this.Service.Get(this.Path, this.RefreshArgs);
        }

        /// <summary>
        /// Loads the collection resource from a given Atom feed.
        /// </summary>
        /// <param name="value">The AtomFeed.</param>
        /// <returns>The resource collection.</returns>
        protected ResourceCollection<T> Load(AtomFeed value) 
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
        /// Refreshes the resource collection.
        /// </summary>
        /// <returns>The resource.</returns>
        public override Resource Refresh() 
        {
            this.Items.Clear();
            ResponseMessage response = this.List();
            AtomFeed feed = AtomFeed.Parse(response.Content);
            this.Load(feed);
            return this;
        }

        /// <summary>
        /// Not supported. Removes an element from the resource collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Throws an exception.</returns>
        public T Remove(object key) 
        {
            throw new NotSupportedException("Remove unsupported");
        }

        /// <summary>
        /// Validates the collection. If dirty, will refresh.
        /// </summary>
        /// <returns>The collection.</returns>
        public new ResourceCollection<T> Validate() 
        {
            base.Validate();
            return this;
        }

        /// <summary>
        /// Returns the number of elements in the list of a
        /// specific key in the collection.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The number of elements.</returns>
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

        /// <summary>
        /// Wrapper to preserve order with an unordered base dictionary.
        /// We can't use System.Collections.Specialized.OrderedDictionary
        /// directly since we want a typed interface from IDictionary generics.
        /// </summary>
        internal class OrderedResourceDictionary : Dictionary<string, List<T>>,
            IDictionary<string, List<T>>,
            IEnumerable<KeyValuePair<string, List<T>>>,
            IEnumerable,
            ICollection<KeyValuePair<string, List<T>>>
        {
            /// <summary>
            /// A linked list for ordering.
            /// </summary>
            private LinkedList<KeyValuePair<string, List<T>>> linkedList = 
                new LinkedList<KeyValuePair<string, List<T>>>();

            /// <summary>
            /// Initializes a new instance of the 
            /// <see cref="OrderedResourceDictionary"/> class.
            /// </summary>
            public OrderedResourceDictionary()
            {
            }

            /// <summary>
            /// Gets ordered Keys collection.
            /// </summary>
            ICollection<string> IDictionary<string, List<T>>.Keys
            {
                get
                {
                    return this.linkedList.Select(x => x.Key).ToList();
                }
            }

            /// <summary>
            /// Gets ordered Values collection.
            /// </summary>
            ICollection<List<T>> IDictionary<string, List<T>>.Values
            {
                get
                {
                    var collection = new List<List<T>>();
                    var keySet = this.Keys;
                    foreach (string key in keySet)
                    {
                        collection.Add(this[key]);
                    }
                    return collection;
                }
            }
            
            /// <summary>
            /// Adds to the dictionary and the end of enumeration.
            /// </summary>
            /// <param name="key">Key of the element to add.</param>
            /// <param name="value">Value of the element to add.</param>
            void IDictionary<string, List<T>>.Add(string key, List<T> value)
            {
                base.Add(key, value);

                this.linkedList.AddLast(
                    new KeyValuePair<string, List<T>>(key, value));
            }

            /// <summary>
            /// Not supported. Use 'IDictionary.Add' instead.
            /// </summary>
            /// <param name="item">The element to add.</param>
            void ICollection<KeyValuePair<string, List<T>>>.Add(
                KeyValuePair<string, List<T>> item)
            {
                throw new NotSupportedException(
                    "ICollection.Add unsupported. Use IDictionary.Add instead.");
            }
            
            /// <summary>
            /// Not supported. Removes an element using a key.
            /// </summary>
            /// <param name="key">The key of the element to remove.</param>
            /// <returns>Not supported.</returns>
            bool IDictionary<string, List<T>>.Remove(string key)
            {
                throw new NotSupportedException("Remove unsupported.");
            }

            /// <summary>
            /// Not supported. Removes and element using an item. 
            /// </summary>
            /// <param name="item">The element to remove.</param>
            /// <returns>Not supported.</returns>
            bool ICollection<KeyValuePair<string, List<T>>>.Remove(
                KeyValuePair<string, List<T>> item)
            {
                throw new NotSupportedException("Remove unsupported.");
            }

            /// <summary>
            /// Returns an enumerator that iterates through 
            /// the collection in order.
            /// </summary>
            /// <returns>An enumerator.</returns>
            IEnumerator<KeyValuePair<string, List<T>>>
                IEnumerable<KeyValuePair<string, List<T>>>.GetEnumerator()
            {
                return this.linkedList.GetEnumerator();
            }

            /// <summary>
            /// Returns an enumerator that iterates through 
            /// the collection in order.
            /// </summary>
            /// <returns>An enumerator</returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.linkedList.GetEnumerator();
            }

            /// <summary>
            /// Clears the collection.
            /// </summary>
            void ICollection<KeyValuePair<string, List<T>>>.Clear()
            {
                base.Clear();
                this.linkedList.Clear();
            }
        }
    }
}
