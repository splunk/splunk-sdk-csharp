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
    /// Represents the Input subclass Windows Event Log Input.
    /// </summary>
    public class WindowsEventLogInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="WindowsEventLogInput"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public WindowsEventLogInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a comma-separated list of secondary hosts used in 
        /// monitoring.
        /// </summary>
        public string Hosts
        {
            get
            {
                return this.GetString("hosts", null);
            }

            set
            {
                this.SetCacheValue("hosts", value);
            }
        }

        /// <summary>
        /// Gets or sets the index name of this Windows Event Log input.
        /// </summary>
        public string Index
        {
            get
            {
                return this.GetString("index", null);
            }

            set
            {
                this.SetCacheValue("index", value);
            }
        }

        /// <summary>
        /// Gets the Input type of this object,  Windows Event Log input.
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.WindowsEventLog;
            }
        }

        /// <summary>
        /// Gets or sets the list of event log names to gather data from for 
        /// this Windows Event Log input.
        /// </summary>
        public string[] Logs
        {
            get
            {
                return this.GetStringArray("logs", null);
            }

            set
            {
                this.SetCacheValue("logs", value);
            }
        }

        /// <summary>
        /// Gets the collection name of this Windows Event Log input. This name
        /// appears in the configuration file, the source, and the sourcetype of
        /// the indexed data. If the value is "localhost", it will use native 
        /// event log collection; otherwise, it will use WMI.
        /// </summary>
        public string LocalName
        {
            get
            {
                return this.GetString("name");
            }
        }

        /// <summary>
        /// Gets or sets the main host of this Windows Event Log input.
        /// Secondary hosts are specified in the hosts attribute.
        /// </summary>
        public string LookupHost
        {
            get
            {
                return this.GetString("lookup_host");
            }

            set
            {
                this.SetCacheValue("lookup_host", value);
            }
        }

        /// <summary>
        /// Updates the entity with the values you previously set using the 
        /// setter methods, and any additional specified arguments. The 
        /// specified arguments take precedent over the values that were set 
        /// using the setter methods.
        /// </summary>
        /// <param name="args">The key/value pairs to update</param>
        public override void Update(Dictionary<string, object> args)
        {
            // Add required arguments if not already present
            if (!args.ContainsKey("lookup_host"))
            {
                args = Args.Create(args);
                args.Add("lookup_host", this.LookupHost);
            }

            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the accumulated arguments, established by 
        /// the individual setter methods for each specific entity class.
        /// </summary>
        public override void Update()
        {
            // If not present in the update keys, add required attribute as long
            // as one pre-existing update pair exists
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("lookup_host"))
            {
                this.SetCacheValue("lookup_host", this.LookupHost);
            }

            base.Update();
        }
    }
}
