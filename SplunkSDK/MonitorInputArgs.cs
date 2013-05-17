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
    /// The <see cref="MonitorInputArgs"/> class extends <see cref="Args"/> for
    /// <see cref="MonitorInput"/> creation properties.
    /// </summary>
    public class MonitorInputArgs : Args
    {
        /// <summary>
        /// Sets a regular expression for a filepath of files not to be 
        /// indexed.
        /// </summary>
        public string Blacklist
        {
            set
            {
                this["blacklist"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to check if the index is valid.
        /// </summary>
        public bool CheckIndex
        {
            set
            {
                this["check-index"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether or not then name, be it a file
        /// or a directory, is valid.
        /// </summary>
        public bool CheckPath
        {
            set
            {
                this["check-path"] = value;
            }
        }

        /// <summary>
        /// Sets a string that modifies the file tracking identity for files in 
        /// this input. The magic value "&lt;SOURCE&gt;" invokes special 
        /// behavior (for more information, see Splunk admin documentation).
        /// </summary>
        public string CrcSalt
        {
            set
            {
                this["crc-salt"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether input monitoring is
        /// disabled. 
        /// </summary>
        /// <remarks>
        /// This property is available in Splunk 5.0 and later. 
        /// </remarks>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether a file, seen for the first time,
        /// is read from the end.
        /// </summary>
        public bool FollowTail
        {
            set
            {
                this["followTail"] = value;
            }
        }

        /// <summary>
        /// Sets a value to populate the host field for inputs from this data 
        /// input. 
        /// </summary>
        public string Host
        {
            set
            {
                this["host"] = value;
            }
        }

        /// <summary>
        /// Sets a regular expression or a file path. 
        /// </summary>
        /// <remarks>
        /// If the path for a file matches this regular expression, the 
        /// captured value is used to populate the host field for events from
        /// this data input. The regular expression must have one capture 
        /// group.
        /// </remarks>
        public string HostRegex
        {
            set
            {
                this["host_regex"] = value;
            }
        }

        /// <summary>
		/// Sets the specified slash-separate segment of the file path as the
		/// host field value.
        /// </summary>
        public int HostSegment
        {
            set
            {
                this["host_segment"] = value;
            }
        }

        /// <summary>
        /// Sets a time value for monitoring lifetime. If the modification time
        /// of a file being monitored falls outside of this rolling time window,
        /// the file is no longer being monitored.
        /// </summary>
        public string IgnoreOlderThan
        {
            set
            {
                this["ignore-older-than"] = value;
            }
        }

        /// <summary>
        /// Sets the index for the events genererated by this data input. 
        /// </summary>
        /// <remarks>
        /// This property's default value is "default".
        /// </remarks>
        public string Index
        {
            set
            {
                this["index"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether subdirectories are indexed.
        /// </summary>
        public bool Recursive
        {
            set
            {
                this["recursive"] = value;
            }
        }

        /// <summary>
        /// Sets a value to populate the source field for inputs from this data 
        /// input.
        /// </summary>
        public string RenameSource
        {
            set
            {
                this["rename-source"] = value;
            }
        }

        /// <summary>
        /// Sets a value to populate the source type for inputs from this data 
        /// input.
        /// </summary>
        public string SourceType
        {
            set
            {
                this["sourcetype"] = value;
            }
        }

        /// <summary>
        /// Sets the amount of time to wait before closing a file upon reading 
        /// EOF.
        /// </summary>
        public int TimeBeforeClose
        {
            set
            {
                this["time-before-close"] = value;
            }
        }

        /// <summary>
        /// Sets a regular expression file path for inclusion. Only file paths 
        /// that match this regular expression are indexed.
        /// </summary>
        public string Whitelist
        {
            set
            {
                this["whitelist"] = value;
            }
        }
    }
}
