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
    using System.Collections.Generic;

    /// <summary>
    /// The LicensePool class represents the License Pool Entity.
    /// </summary>
    public class LicensePool : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePool"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public LicensePool(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Sets a value indicating whether to append or overwrite slaves to 
        /// this license pool.
        /// </summary>
        public bool AppendSlaves
        {
            set
            {
                this.SetCacheValue("append_slaves", value);
            }
        }

        /// <summary>
        /// Gets or sets the description of this license pool.
        /// </summary>
        public string Description
        {
            get
            {
                return this.GetString("description", null);
            }

            set
            {
                this.SetCacheValue("description", value);
            }
        }

        /// <summary>
        /// Gets or sets the indexing quota for this license pool. Note: While
        /// this value is normally an integer byyte count, use "MAX" to
        /// indicate the maximum amount that is allowed.
        /// </summary>
        public string Quota
        {
            get
            {
                return this.GetString("quota", null);
            }

            set
            {
                this.SetCacheValue("quota", value);
            }
        }

        /// <summary>
        /// Gets or sets the slaves of this license pool.
        /// </summary>
        public string[] Slaves
        {
            get
            {
                // Return either the string array, OR if we have a temporary
                // concatenation (as per the setter), rebuild the array.
                string[] temp = this.GetStringArray("slaves", null);
                if ((temp != null) && (temp.Length == 1))
                {
                    return temp[0].Split(',');
                }
                return temp;
            }

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
                this.SetCacheValue("slaves", new string[] { composite });
            }
        }

        /// <summary>
        /// Gets the slaves usage in bytes.
        /// </summary>
        public int SlavesUsageBytes
        {
            get
            {
                return this.GetInteger("slaves_usage_bytes", 0);
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

        /// <summary>
        /// Gets the index usage, in bytes, against this license pool.
        /// </summary>
        public int UsedBytes
        {
            get
            {
                return this.GetInteger("used_bytes", 0);
            }
        }

        /// <summary>
        /// Updates the entity with the arguments. We need to override 
        /// update only because of the asymmetric getter/setter slaves.
        /// </summary>
        /// <param name="args">The arguments to update.</param>
        public override void Update(Dictionary<string, object> args)
        {
            // force canonicalization
            if (this.Slaves != null)
            {
                this.Slaves = this.Slaves;
            }

            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the arguments. We need to override 
        /// update only because of the asymmetric getter/setter "Slaves".
        /// </summary>
        public override void Update()
        {
            // force canonicalization
            if (this.Slaves != null)
            {
                this.Slaves = this.Slaves;
            }

            base.Update();
        }
    }
}
