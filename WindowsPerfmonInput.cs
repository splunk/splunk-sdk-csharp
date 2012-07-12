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
    /// Represents the Input subclass Windows Performance Monitor Input.
    /// </summary>
    public class WindowsPerfmonInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPerfmonInput"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public WindowsPerfmonInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a list of monitored counters for this Windows Perfmon input. 
        /// An asterisk (*) is equivalent to all counters.
        /// </summary>
        public string[] Counters
        {
            get
            {
                return this.GetStringArray("counters", null);
            }

            set
            {
                this.SetCacheValue("counters", value);
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
        /// Gets or sets the index name of this Windows Perfmon input
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
        /// Gets or sets the counter instances of this Windows Perfmon input. An asterisk 
        /// (*) is equivalent to all instances.
        /// </summary>
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
        /// Gets or sets the interval at which to poll the performance counters for this
        /// Windows Perfmon input.
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
        /// Gets the Input type of this object,  Windows Perfmon input.
        /// </summary>
        public override InputKind Kind
        {
            get
            {
                return InputKind.WindowsPerfmon;
            }
        }

        /// <summary>
        /// Gets or sets the performance monitor object for this Windows Perfmon input
        /// (for example, "Process", "Server", or "PhysicalDisk".)
        /// </summary>
        public string Object
        {
            get
            {
                return this.GetString("object");
            }

            set
            {
                this.SetCacheValue("object", value);
            }
        }
    }
}
