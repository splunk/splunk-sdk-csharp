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
    /// Extends Args for LicensePool creation setters
    /// </summary>
    public class LicensePoolArgs : Args
    {
        /// <summary>
        /// Sets the byte quota of this pool. Normally this is an integer, or an 
        /// integer followed by MB or GB. "MAX" is also allowed, which specifies
        /// the maximum amount allowed by this license. This is required. 
        /// </summary>
        public string Quota
        {
            set
            {
                this["quota"] = value;
            }
        }

        /// <summary>
        /// Sets the stack ID of the stack corresponding to this pool. Valid
        /// values are: download-trial, enterprise, forwarder, or free. This
        /// is required.
        /// </summary>
        public string StackId
        {
            set
            {
                this["stack_id"] = value;
            }
        }

        /// <summary>
        /// Sets the description of this pool.
        /// </summary>
        public string Description
        {
            set
            {
                this["description"] = value;
            }
        }

        /// <summary>
        /// Sets the slave IDs that are members of this pool, or "*" to 
        /// accept all slaves. 
        /// </summary>
        public string[] Slaves
        {
            set
            {
                // Take string array and build into a single string separating 
                // the terms with a , symbol (expected by the endpoint) for 
                // updating.
                string composite = string.Empty;
                for (int i = 0; i < value.Length; i++)
                {
                    if (i != 0)
                    {
                        composite = composite + ",";
                    }
                    composite = composite + value[i];
                }
                this["slaves"] = new string[] { composite };
            }
        }
    }
}
