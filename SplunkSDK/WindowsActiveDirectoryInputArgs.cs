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
    /// The <see cref="WindowsActiveDirectoryInputArgs"/> class extends 
    /// <see cref="Args"/> for 
    /// <see cref="WindowsActiveDirectoryInput"/> creation properties.
    /// </summary>
    public class WindowsActiveDirectoryInputArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether monitoring is disabled.
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
        /// Sets a value indicating whether to monitor the subtree(s) of a 
        /// given directory tree path. 
        /// </summary>
        /// <remarks>
        /// This property's default value is true.
        /// </remarks>
        public bool MonitorSubtree
        {
            set
            {
                this["monitorSubtree"] = value;
            }
        }

        /// <summary>
        /// Sets the Active Directory directory tree to start monitoring. 
        /// </summary>
        /// <remarks>
        /// If not specified, Splunk attempts to start at the root of the 
        /// directory tree.
        /// </remarks>
        public string StartingNode
        {
            set
            {
                this["startingNode"] = value;
            }
        }

        /// <summary>
        /// Sets the fully qualified domain name of a valid, network-accessible
        /// domain controller. 
        /// </summary>
        /// <remarks>
        /// If not specified, Splunk will obtain the local computer's domain 
        /// controller.
        /// </remarks>
        public string TargetDc
        {
            set
            {
                this["targetDc"] = value;
            }
        }
    }
}