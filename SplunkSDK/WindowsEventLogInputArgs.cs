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
    /// The <see cref="WindowsEventLogInputArgs"/> class extends 
    /// <see cref="Args"/> for <see cref="WindowsEventLogInput"/> properties.
    /// </summary>
    public class WindowsEventLogInputArgs : Args
    {
        /// <summary>
        /// Sets additional hosts to be used for monitoring. 
        /// </summary>
        /// <remarks>
        /// The first host should be specified with the 
        /// <see cref="LookupHost"/> property, and the additional ones using 
        /// this property. This property accepts a comma-separated list.
        /// </remarks>
        public string Hosts
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Required. Sets the host from which log events are monitored. 
        /// </summary>
        /// <remarks>
        /// To specify additional hosts to be monitored via Windows Management 
        /// Instrumentation (WMI), use the <see cref="Hosts"/> property.
        /// </remarks>
        public string LookupHost
        {
            set
            {
                this["lookup_host"] = value;
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
        /// Sets a comma-separated list of event log names to gather data from.
        /// </summary>
        public string Logs
        {
            set
            {
                this["logs"] = value;
            }
        }
    }
}
