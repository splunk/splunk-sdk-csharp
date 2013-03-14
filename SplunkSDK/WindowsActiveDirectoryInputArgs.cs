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
    /// Extends Args for WindowsActiveDirectoryInput creation setters
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
        /// Sets a value indicating whether to monitor the subtree(s) of a 
        /// given directory tree path. The default is true.
        /// </summary>
        public bool MonitorSubtree
        {
            set
            {
                this["monitorSubtree"] = value;
            }
        }

        /// <summary>
        /// Sets the Active Directory directory tree to start monitoring. If not
        /// specified, Splunk attempts to start at the root of the directory
        /// tree.
        /// </summary>
        public string StartingNode
        {
            set
            {
                this["startingNode"] = value;
            }
        }

        /// <summary>
        /// Sets the fully qualified domain name of a valid, network-accessible
        /// DC. If not specified, Splunk will obtain the local computer's DC.
        /// </summary>
        public string TargetDc
        {
            set
            {
                this["targetDc"] = value;
            }
        }
    }
}