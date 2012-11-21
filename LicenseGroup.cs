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
    /// <summary>
    /// The LicenseGroup class represents the License Group Entity.
    /// </summary>
    public class LicenseGroup : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseGroup"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public LicenseGroup(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this license group is 
        /// active.
        /// </summary>
        public bool Active
        {
            get
            {
                return this.GetBoolean("is_active", false);
            }

            set
            {
                this.SetCacheValue("is_active", value);
            }
        }

        /// <summary>
        /// Gets the stack IDs of the license stacks in this license group.
        /// </summary>
        public string[] StackIds
        {
            get
            {
                return this.GetStringArray("stack_ids", null);
            }
        }
    }
}
