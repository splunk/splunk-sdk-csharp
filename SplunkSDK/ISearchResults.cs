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
    /// The <see cref="ISearchResults"/> interface represents Splunk search
    /// results.
    /// </summary>
    public interface ISearchResults : IEnumerable<Event>
    {
        /// <summary>
        /// Gets a value indicating whether the results are
        /// a preview from an unfinished search.
        /// </summary>
        bool IsPreview
        {
            get;
        }

        /// <summary>
        /// Gets all the fields that may appear in each result.
        /// </summary>
        /// <remarks>
        /// Be aware that any given result will contain a subset of these 
        /// fields.
        /// </remarks>
        ICollection<string> Fields
        {
            get;
        }
    }
}