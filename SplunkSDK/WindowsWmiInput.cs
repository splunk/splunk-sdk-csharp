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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The <see cref="WindowsWmiInput"/> class represents represents a Windows 
    /// Management Instrumentation (WMI) data input.
    /// </summary>
    public class WindowsWmiInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsWmiInput"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="path">The path.</param>
        public WindowsWmiInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the WMI class name of this WMI input.
        /// </summary>
        public string Classes
        {
            get
            {
                return this.GetString("classes");
            }

            set
            {
                this.SetCacheValue("classes", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether this input is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Gets or sets the the properties (fields) collected for this class 
        /// for this WMI input.
        /// </summary>
        public string[] Fields
        {
            get
            {
                return this.GetStringArray("fields", null);
            }

            set
            {
                this.SetCacheValue("fields", value);
            }
        }

        /// <summary>
        /// Gets or sets the index name of this Windows Registry input.
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
        /// Gets or sets the counter instances of this WMI input. 
        /// </summary>
        /// <remarks>
        /// An asterisk (*) is equivalent to all instances.
        /// </remarks>
        public string[] Instances
        {
            get
            {
                return this.GetStringArray("instances", null);
            }

            set
            {
                this.SetCacheValue("instances", value);
            }
        }

        /// <summary>
        /// Gets or sets the interval at which WMI input providers are queried 
        /// for this WMI input.
        /// </summary>
        public int Interval
        {
            get
            {
                return this.GetInteger("interval");
            }

            set
            {
                this.SetCacheValue("interval", value);
            }
        }

        /// <summary>
        /// Gets the input type of this object, WMI input.
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.WindowsWmi;
            }
        }

        /// <summary>
        /// Gets or sets the main host of this Windows Event log input. 
        /// Secondary hosts are specified in the <see cref="Hosts"/> attribute.
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
        /// Gets the collection name for this WMI input. This name appears in
        /// configuration file, the source, and the sourcetype of the indexed 
        /// data.
        /// </summary>
        public string LocalName
        {
            get
            {
                return this.GetString("name");
            }
        }

        /// <summary>
        /// Gets or sets a comma-separated list of the additional servers used 
        /// in monitoring.
        /// </summary>
        public string Servers
        {
            get
            {
                return this.GetString("server", null);
            }

            set
            {
                this.SetCacheValue("server", value);
            }
        }

        /// <summary>
        /// Gets the query string for this WMI input.
        /// </summary>
        public string Wql
        {
            get
            {
                return this.GetString("wql");
            }
        }

        /// <summary>
        /// Updates the entity with the values you previously set using the 
        /// class properties, and any additional specified arguments. The 
        /// specified arguments take precedence over the values that were set
        /// using the properties.
        /// </summary>
        /// <param name="args">The key/value pairs to update.</param>
        public override void Update(Dictionary<string, object> args)
        {
            // If not present in the update keys, add required attributes
            if (!args.ContainsKey("classes"))
            {
                args = Args.Create(args);
                args.Add("classes", this.Classes);
            }

            if (!args.ContainsKey("interval"))
            {
                args = Args.Create(args);
                args.Add("interval", this.Interval);
            }

            if (!args.ContainsKey("lookup_host"))
            {
                args = Args.Create(args);
                args.Add("lookup_host", this.LookupHost);
            }

            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the accumulated arguments, established by 
        /// the individual properties for each specific entity class.
        /// </summary>
        public override void Update()
        {
            // If not present in the update keys, add required attributes as 
            // long as one pre-existing update pair exists.
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("classes"))
            {
                this.SetCacheValue("classes", this.Classes);
            }
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("interval"))
            {
                this.SetCacheValue("interval", this.Interval);
            }
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("lookup_host"))
            {
                this.SetCacheValue("lookup_host", this.LookupHost);
            }
            base.Update();
        }
    }
}
