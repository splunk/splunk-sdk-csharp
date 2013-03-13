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

    /// <summary>
    /// Represents the Entity Collection class. 
    /// </summary>
    /// <typeparam name="T">The Generic parameter that matches The Entity class,
    /// or any class derived from Entity.</typeparam>
    public class EntityCollection<T> : ResourceCollection<T> where T : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The endpoint for this collection</param>
        public EntityCollection(Service service, string path) 
            : base(service, path, typeof(Entity)) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The endpoint for this collection</param>
        /// <param name="args">The variable argument list</param>
        public EntityCollection(Service service, string path, Args args) 
            : base(service, path, args, typeof(Entity)) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The endpoint for this collection</param>
        /// <param name="itemClass">The type of this Entity</param>
        public EntityCollection(Service service, string path, Type itemClass) 
            : base(service, path, itemClass) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The endpoint for this collection</param>
        /// <param name="args">The variable argument list</param>
        /// <param name="itemClass">The type of this Entity</param>
        public EntityCollection(
                Service service, string path, Args args, Type itemClass) 
            : base(service, path, args, itemClass) 
        {
        }

        /// <summary>
        /// Creates an Entity in this collection.
        /// </summary>
        /// <param name="name">The name of this Entity</param>
        /// <returns>The Entity</returns>
        public virtual T Create(string name) 
        {
            return this.Create(name, (Args)null);
        }

        /// <summary>
        /// Creates an Entity in this collection.
        /// </summary>
        /// <param name="name">The name of this Entity</param>
        /// <param name="args">The variable argument list</param>
        /// <returns>The Entity</returns>
        public virtual T Create(string name, Args args) 
        {
            args = Args.Create(args);
            args.Add("name", name);
            this.Service.Post(this.Path, args);
            this.Invalidate();
            return this.Get(name);
        }

        /// <summary>
        /// Removes an entity from this collection. Note that this method can 
        /// throw the SplunkException "AMBIGUOUS" if the collection contains 
        /// more than one entity with the specified key. Disambiguation is done 
        /// through the similar method Remove(object key, Dictionary namespace)
        /// which uses the namespace to perform the disambiguation.
        /// </summary>
        /// <param name="key">The name of this Entity</param>
        /// <returns>The removed Entity</returns>
        public virtual T Remove(string key) 
        {
            this.Validate();
            if (!this.ContainsKey(key)) 
            {
                return default(T);
            }
            List<T> entities = this.Items[key];
            if (entities != null && entities.Count > 1) 
            {
                throw new SplunkException(
                    SplunkException.AMBIGUOUS, 
                    "Key has multiple values, specify a namespace");
            }
            if (entities == null) 
            {
                return default(T);
            }
            T entity = entities[0];
            entity.Remove();
            this.Invalidate();
            return entity;
        }

        /// <summary>
        /// Removes an entity from this collection, with a namespace 
        /// restriction.
        /// </summary>
        /// <param name="key">The name of this Entity</param>
        /// <param name="splunkNamespace">The namespace</param>
        /// <returns>The removed Entity</returns>
        public virtual T Remove(string key, Args splunkNamespace) 
        {
            this.Validate();
            if (!this.ContainsKey(key)) 
            {
                return default(T);
            }
            List<T> entities = Items[key];
            string pathMatcher = 
                    Service.Fullpath(string.Empty, splunkNamespace);
            if (entities.Count == 0) 
            {
                return default(T);
            }
            foreach (T entity in entities) 
            {
                if (entity.Path.StartsWith(pathMatcher)) 
                {
                    entity.Remove();
                    this.Invalidate();
                    return entity;
                }
            }
            return default(T);
        }
    }
}
