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
    /// The LicenseSlave class represents the License Slave Entity.
    /// </summary>
    public class LicenseSlave : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseSlave"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public LicenseSlave(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the label of this license slave.
        /// </summary>
        public string Label
        {
            get
            {
                return this.GetString("label", null);
            }
        }

        /// <summary>
        /// Gets the pool IDs of this license slave. This is only available
        /// pre-Splunk 5.0.
        /// </summary>
        public string[] PoolIds
        {
            get
            {
                return this.GetStringArray("pool_ids", null);
            }
        }

        /// <summary>
        /// Gets the stack IDs of this license slave. This is only available
        /// pre-Splunk 5.0.
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
