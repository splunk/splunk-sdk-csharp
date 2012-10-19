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
    /// Extends Args for WindowsEventLogInput creation setters
    /// </summary>
    public class WindowsEventLogInputArgs : Args
    {
        /// <summary>
        /// Sets addtional hosts to be used for monitoring. The first host 
        /// should be specified with "lookup_host", and the additional ones
        /// using this parameter. This parameters is a comma-separated list.
        /// </summary>
        public string Hosts
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the host from which log events are monitored. This parameter
        /// is required. To specify additional hosts to be monitored via WMI, 
        /// use the "hosts" parameter.
        /// </summary>
        public string LookupHost
        {
            set
            {
                this["lookup_host"] = value;
            }
        }

        /// <summary>
        /// Sets the  index in which to store the gathered data.
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
