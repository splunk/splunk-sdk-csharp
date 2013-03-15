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
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="Resource"/> class represents the base of all Splunk
    /// entity and entity collection classes.
    /// </summary>
    public abstract class Resource
    {
        /// <summary>
        /// The dictionary of actions allowed on this resource.
        /// </summary>
        private Dictionary<string, string> actions;
        
        /// <summary>
        /// The resource title.
        /// </summary>
        private string title;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="path">The path of the resource.</param>
        public Resource(Service service, string path) 
        {
            this.Path = service.Fullpath(path);
            this.PartialPath = path;
            this.Service = service;
            this.RefreshArgs = new Args("count", "-1");
            this.MaybeValid = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class, 
        /// adding optional arguments for namespace and other endpoint 
        /// arguments.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="path">The path of this resource.</param>
        /// <param name="args">The variable arguments.</param>
        public Resource(Service service, string path, Args args) 
        {
            this.Service = service;
            /* Pull out namespace items (app, owner, sharing) from the args, and
             * then use to create the full path. 
             */
            Args clonedArgs = new Args(args);
            Args splunkNamespace = new Args();
            if (args.ContainsKey("app")) 
            {
                splunkNamespace.AlternateAdd("app", args["app"].ToString());
                clonedArgs.Remove("app");
            }
            if (args.ContainsKey("owner")) 
            {
                splunkNamespace.AlternateAdd("owner", args["owner"].ToString());
                clonedArgs.Remove("owner");
            }
            if (args.ContainsKey("sharing")) 
            {
                splunkNamespace.AlternateAdd(
                    "sharing", args["sharing"].ToString());
                clonedArgs.Remove("sharing");
            }
            if (!clonedArgs.ContainsKey("count")) 
            {
                clonedArgs.AlternateAdd("count", "-1");
            }

            this.RefreshArgs = clonedArgs;
            this.Path = service.Fullpath(
                path, splunkNamespace.Count == 0 ? null : splunkNamespace);
            this.MaybeValid = false;
        }

        /// <summary>
        /// Gets or sets the full path of this resource.
        /// </summary>
        public string Path 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the partial path of this resource.
        /// </summary>
        private string PartialPath 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the service this resource is found on.
        /// </summary>
        public Service Service 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the refresh args for this resources.
        /// </summary>
        public Args RefreshArgs 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this resource is clean
        /// or dirty. When dirty, we refresh the resource before returning 
        /// any data contained therein.
        /// </summary>
        public bool MaybeValid 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets an up-to-date list of actions available for this resource.
        /// </summary>
        /// <returns>Available actions on this endpoint.</returns>
        public Dictionary<string, string> Actions() 
        {
            return this.Validate().actions;
        }

        /// <summary>
        /// Gets the name of this resource 
        /// </summary>
        public virtual string Name 
        {
            get 
            {
                return this.Title;
            }
        }

        /// <summary>
        /// Gets or sets the  value of the title of this resource. Note that
        /// getting the property may refresh the local resource if dirty from
        /// the server.
        /// </summary>
        public string Title 
        {
            get 
            {
                return this.Validate().title;
            }

            set 
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Marks the local state of this resource as no longer current.
        /// </summary>
        /// <returns>The resource.</returns>
        public Resource Invalidate() 
        {
            this.MaybeValid = false;
            return this;
        }

        /// <summary>
        /// Loads the state of this resource from a given Atom object.
        /// </summary>
        /// <param name="value">The AtomObject to load.</param>
        /// <returns>The <see cref="Resource"/>.</returns>
        public Resource Load(AtomObject value) 
        {
            if (value == null) 
            {
                this.title = "title";
            }
            else 
            {
                this.actions = value.Links;
                this.title = value.Title;
            }
            this.MaybeValid = true;
            return this;
        }

        /// <summary>
        /// Refreshes the local state of this resource.
        /// </summary>
        /// <returns>The <see cref="Resource"/>.</returns>
        public abstract Resource Refresh();

        /// <summary>
        /// Ensures that the local state of the resource is current,
        /// calling object-specific <see cref="Refresh"/> method if necessary.
        /// </summary>
        /// <returns>The <see cref="Resource"/>.</returns>
        public virtual Resource Validate() 
        {
            if (!this.MaybeValid) 
            {
                this.Refresh();
            }
            return this;
        }
    }
}
