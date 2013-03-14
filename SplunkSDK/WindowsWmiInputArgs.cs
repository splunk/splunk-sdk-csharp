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
    /// The <see cref="WindowsWmiInputArgs"/> class extends the 
    /// <see cref="Args"/> class for <see cref="WindowsWmiInput"/> creation 
    /// setters.
    /// </summary>
    public class WindowsWmiInputArgs : Args
    {
        /// <summary>
        /// Sets a valid WMI class name. This parameter is required.
        /// </summary>
        public string Classes
        {
            set
            {
                this["classes"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether this collection is disabled. 
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the fields that you want to gather from the given class.
        /// </summary>
        public string[] Fields
        {
            set
            {
                this["fields"] = value;
            }
        }

        /// <summary>
        /// Sets the index in which to store the gathered data.
        /// </summary>
        public string Index
        {
            set
            {
                this["index"] = value;
            }
        }

        /// <summary>
        /// Sets the instances of the given class for which data is gathered.
        /// </summary>
        public string[] Instances
        {
            set
            {
                this["instances"] = value;
            }
        }

        /// <summary>
        /// Sets the frequency, in seconds, at which the SMI provider(s) are
        /// queried. This parameter is required.
        /// </summary>
        public int Interval
        {
            set
            {
                this["interval"] = value;
            }
        }

        /// <summary>
        /// Sets the host from which WMI data is gathered. This parameter
        /// is required. To specify additional hosts to be monitored via WMI, 
        /// use the "server" parameter.
        /// </summary>
        public string LookupHost
        {
            set
            {
                this["lookup_host"] = value;
            }
        }

        /// <summary>
        /// Sets a comma-separated list of additional servers from which you 
        /// want to gather data. 
        /// </summary>
        public string Server
        {
            set
            {
                this["server"] = value;
            }
        }
    }
}
