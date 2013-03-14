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
    /// <summary>
    /// The <see cref="ConfCollection"/> class represents the collection of 
    /// configuration collections.
    /// </summary>
    public class ConfCollection : ResourceCollection<EntityCollection<Entity>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        public ConfCollection(Service service)
            : base(service, "properties", typeof(EntityCollection<Entity>))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="args">The arguments.</param>
        public ConfCollection(Service service, Args args)
           : base(service, "properties", args, typeof(EntityCollection<Entity>))
        {
        }

        /// <summary>
        /// Creates a new stanza in the current configuration file.
        /// </summary>
        /// <param name="name">The stanza name.</param>
        /// <returns>The stanza.</returns>
        public EntityCollection<Entity> Create(string name)
        {
            return this.Create(name, (Args)null);
        }

        /// <summary>
        /// Creates a new stanza in the current configuration file with 
        /// attributes.
        /// </summary>
        /// <param name="name">The stanza name.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The stanza.</returns>
        public EntityCollection<Entity> Create(string name, Args args)
        {
            args = Args.Create(args);
            args.Add("__conf", name);
            this.Service.Post(this.Path, args);
            this.Invalidate();
            return this.Get(name);
        }

        /// <summary>
        /// Returns the endpoint path for this configuration stanza.
        /// </summary>
        /// <param name="entry">The Atom entry.</param>
        /// <returns>The item path.</returns>
        protected override string ItemPath(AtomEntry entry)
        {
            return string.Format("configs/conf-{0}", entry.Title);
        }
    }
}
