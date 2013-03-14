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
    /// Extends Args for WindowsPerfmonInput creation setters
    /// </summary>
    public class WindowsPerfmonInputArgs : Args
    {
        /// <summary>
        /// Sets the list of counters within the specified monitored object. 
        /// Note that "*" is equivalent to all counters.
        /// </summary>
        public string[] Counters
        {
            set
            {
                this["counters"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether a specific stanza is monitored. 
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
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
        /// Sets the counter instances to monitor. Note: a "*" is equivalent
        /// to all instances.
        /// </summary>
        public string[] Instances
        {
            set
            {
                this["instances"] = value;
            }
        }

        /// <summary>
        /// Sets the frequency, in seconds, to poll the performance counters.
        /// This parameter is required.
        /// </summary>
        public int Interval
        {
            set
            {
                this["interval"] = value;
            }
        }

        /// <summary>
        /// Sets the performance monitor object. For example "Process", or 
        /// "Server", or "PhysicalDisk", etc. This parameter is required.
        /// </summary>
        public string Object
        {
            set
            {
                this["object"] = value;
            }
        }
    }
}
