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
    /// <summary>
    /// The <see cref="WindowsPerfmonInput"/> class represents a Windows 
    /// Performance Monitor (Perfmon) data input.
    /// </summary>
    public class WindowsPerfmonInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPerfmonInput"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public WindowsPerfmonInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a list of monitored counters for this Windows Perfmon 
        /// input. An asterisk ("*") is equivalent to all counters.
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
        /// <remarks>
        /// This attribute is available starting in Splunk 4.3.
        /// </remarks>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Gets or sets the index name of this Windows Perfmon input.
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
        /// Gets or sets the counter instances of this Windows Perfmon input. 
        /// An asterisk ("*") is equivalent to all instances.
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
        /// Gets or sets the interval at which to poll the performance counters 
        /// for this Windows Perfmon input.
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
        /// Gets the input type of this object, Windows Perfmon input.
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.WindowsPerfmon;
            }
        }

        /// <summary>
        /// Gets or sets the performance monitor object for this Windows Perfmon
        /// input (for example, "Process", "Server", or "PhysicalDisk".)
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

        /// <summary>
        /// Gets or sets the source value to populate in he source field for 
        /// events from this data input. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// The same source should not be used for multiple events. 
        /// </para>
        /// <para>
        /// This property is available in Splunk 5.0 and later.
        /// </para>
        /// </remarks>
        public string Source
        {
            get
            {
                return this.GetString("source", null);
            }

            set
            {
                this.SetCacheValue("source", value);
            }
        }

        /// <summary>
        /// Gets or sets the sourcetype value to populate in the sourcetype 
        /// field for incoming events. 
        /// </summary>
        /// <remarks>
        /// This property is available in Splunk 5.0 and later.
        /// </remarks>
        public string SourceType
        {
            get
            {
                return this.GetString("sourcetype", null);
            }

            set
            {
                this.SetCacheValue("sourcetype", value);
            }
        }
    }
}
