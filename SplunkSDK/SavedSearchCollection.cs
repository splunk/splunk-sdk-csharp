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

    /// <summary>
    /// The <see cref="SavedSearchCollection"/> class represents the 
    /// collection of SavedSearches.
    /// </summary>
    public class SavedSearchCollection : EntityCollection<SavedSearch>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="SavedSearchCollection"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        public SavedSearchCollection(Service service)
            : base(service, "saved/searches", typeof(SavedSearch))
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="SavedSearchCollection"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="args">The arguments.</param>
        public SavedSearchCollection(Service service, Args args)
            : base(service, "saved/searches", args, typeof(SavedSearch))
        {
        }

        /// <summary>
        /// Unsupported. Do not use. Creates a saved search without a search 
        /// string.
        /// </summary>
        /// <param name="name">Search name.</param>
        /// <returns>N/A. Throws an exception.</returns>
        public override SavedSearch Create(string name)
        {
            throw new Exception("Create unsuported");
        }

        /// <summary>
        /// Creates a saved search from a name and search expression.
        /// </summary>
        /// <param name="name">The name of the search.</param>
        /// <param name="search">The search string.</param>
        /// <returns>The saved search.</returns>
        public SavedSearch Create(string name, string search)
        {
            Args args = new Args("search", search);
            return base.Create(name, args);
        }

        /// <summary>
        /// Creates a saved search from a name, search expression, and
        /// additional arguments.
        /// /// </summary>
        /// <param name="name">The name of the saved search.</param>
        /// <param name="search">The search string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The saved search.</returns>
        public SavedSearch Create(string name, string search, Args args)
        {
            args = Args.Create(args);
            args.Add("search", search);
            return base.Create(name, args);
        }
    }
}