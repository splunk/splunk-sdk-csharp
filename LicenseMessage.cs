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
    /// The LicenseMessage class represents the License Message Entity.
    /// </summary>
    public class LicenseMessage : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseMessage"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public LicenseMessage(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the category of this license message.
        /// </summary>
        public string Category
        {
            get
            {
                return this.GetString("category", null);
            }
        }

        /// <summary>
        /// Gets the description of this license message.
        /// </summary>
        public string Description
        {
            get
            {
                return this.GetString("description", null);
            }
        }

        /// <summary>
        /// Gets the pool ID of this license message.
        /// </summary>
        public string PoolId
        {
            get
            {
                return this.GetString("pool_id", null);
            }
        }

        /// <summary>
        /// Gets the severity of this license message.
        /// </summary>
        public string Severity
        {
            get
            {
                return this.GetString("severity", null);
            }
        }

        /// <summary>
        /// Gets the slave ID of this license message.
        /// </summary>
        public string SlaveId
        {
            get
            {
                return this.GetString("slave_id", null);
            }
        }

        /// <summary>
        /// Gets the stack ID of this license message.
        /// </summary>
        public string StackId
        {
            get
            {
                return this.GetString("stack_id", null);
            }
        }
    }
}
