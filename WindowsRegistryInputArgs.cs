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
    /// Extends Args for WindowsRegistryInput creation setters
    /// </summary>
    public class WindowsRegistryInputArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether this Windows Registry input 
        /// has an established baseline. This parameter is required.
        /// </summary>
        public bool Baseline
        {
            set
            {
                this["baseline"] = value;
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
        /// Sets the registry hive under which to monitor for changes. This 
        /// parameter is required.
        /// </summary>
        public string Hive
        {
            set
            {
                this["hive"] = value;
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
        /// Sets a value indicating whether to monitor the subnodes of a 
        /// given registry hive.
        /// </summary>
        public bool MonitorSubnodes
        {
            set
            {
                this["monitorSubnodes"] = value;
            }
        }

        /// <summary>
        /// Sets a regular expression applied to registry events. If the regex
        /// pattern matches on a registry event, that event is collected. This
        /// parameter is required.
        /// </summary>
        public string Proc
        {
            set
            {
                this["proc"] = value;
            }
        }

        /// <summary>
        /// Sets the types of registry events to be monitored.
        /// </summary>
        public string[] Type
        {
            set
            {
                // Take string array and build into a single string separating 
                // the terms with a | symbol (expected by the endpoint) for 
                // updating.
                string composite = string.Empty;
                for (int i = 0; i < value.Length; i++)
                {
                    if (i != 0)
                    {
                        composite = composite + "|";
                    }
                    composite = composite + value[i];
                }
                this["type"] = new string[] { composite };
            }
        }
    }
}
