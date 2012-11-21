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
    /// The LicenseStack class represents the License Stack Entity.
    /// </summary>
    public class LicenseStack : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseStack"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public LicenseStack(Service service, string path)
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
        /// Gets the byte quota of this license stack. This value is the sum of 
        /// the byte quota for all the licenses in the license stack.
        /// </summary>
        public int Quota
        {
            get
            {
                return this.GetInteger("quota", 0);
            }
        }

        /// <summary>
        /// Gets additional information about the type of this license stack.
        /// </summary>
        public string Type
        {
            get
            {
                return this.GetString("type", null);
            }
        }
    }
}
