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
    /// Represents the Input subclass Monitor Input
    /// </summary>
    public class MonitorInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonitorInput"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public MonitorInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a regular expression for a file path that when matched 
        /// is not indexed.
        /// </summary>
        public string Blacklist
        {
            get
            {
                return this.GetString("blacklist", null);
            }

            set
            {
                this.SetCacheValue("blacklist", value);
            }
        }

        /// <summary>
        /// Gets or sets a string that is used to force Splunk to index files 
        /// that have a matching cyclic redundancy check (CRC).
        /// </summary>
        public string CrcSalt
        {
            get
            {
                return this.GetString("crcSalt", null);
            }

            set
            {
                this.SetCacheValue("crc-salt", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether the index value is checked to ensure
        /// that it is the  name of a valid index.
        /// </summary>
        public bool CheckIndex
        {
            set
            {
                this.SetCacheValue("check-index", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether the name value is checked to ensure 
        /// that it exists.
        /// </summary>
        public bool CheckPath
        {
            set
            {
                this.SetCacheValue("check-path", value);
            }
        }

        /// <summary>
        /// Gets the file count of this monitor input.
        /// </summary>
        public int FileCount
        {
            get
            {
                return this.GetInteger("filecount", -1);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether files that are seen for the 
        /// first time will be read from the end.
        /// </summary>
        public bool FollowTail
        {
            get
            {
                return this.GetBoolean("followTail", false);
            }

            set
            {
                this.SetCacheValue("followTail", value);
            }
        }

        /// <summary>
        /// Gets or sets the host for this monitor input.
        /// </summary>
        public string Host
        {
            get
            {
                return this.GetString("host", null);
            }

            set
            {
                this.SetCacheValue("host", value);
            }
        }

        /// <summary>
        /// Gets or sets the regular expression for a file path to determine the
        /// host. If the path for a file matches this regular expression, the 
        /// captured value is used to populate the <b>host</b> field for events 
        /// from this monitor input. The regular expression must have one 
        /// capture group.
        /// </summary>
        public string HostRegex
        {
            get
            {
                return this.GetString("host_regex", null);
            }

            set
            {
                this.SetCacheValue("host_regex", value);
            }
        }

        /// <summary>
        /// Gets or sets a time value that defines a rolling time window for 
        /// monitoring files. If the modification time of a file being monitored
        /// falls outside of this rolling time window, the file is no longer 
        /// being monitored.
        /// </summary>
        public string IgnoreOlderThan
        {
            get
            {
                return this.GetString("ignoreOlderThan", null);
            }

            set
            {
                this.SetCacheValue("ignore-older-than", value);
            }
        }

        /// <summary>
        /// Gets or sets the index where events from this monitor input are 
        /// stored.
        /// </summary>
        public string Index
        {
            get
            {
                return this.GetString("index");
            }

            set
            {
                this.SetCacheValue("index", value);
            }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether  sub-directories are 
        /// monitored within this monitor input.
        /// </summary>
        public bool IsRecursive
        {
            get
            {
                return this.GetBoolean("recursive", false);
            }

            set
            {
                this.SetCacheValue("recursive", value);
            }
        }

        /// <summary>
        /// Gets the Input type of this object, Monitor.
        /// </summary>
        public override InputKind Kind
        {
            get
            {
                return InputKind.Monitor;
            }
        }

        /// <summary>
        /// Gets the queue for this monitor input. Valid values are 
        /// "parsingQueue" and "indexQueue".
        /// </summary>
        public string Queue
        {
            get
            {
                return this.GetString("queue", null);
            }
        }

        /// <summary>
        /// Gets the source of events from this monitor input.
        /// </summary>
        public string Source
        {
            get
            {
                return this.GetString("source", null);
            }
        }

        /// <summary>
        /// Gets or sets the source type of events from this monitor input.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the time period for keeping a file open.
        /// </summary>
        public int TimeBeforeClose
        {
            get
            {
                return this.GetInteger("time_before_close", -1);
            }

            set
            {
                this.SetCacheValue("time-before-close", value);
            }
        }

        /// <summary>
        /// Gets or sets a regular expression for a file path that, when 
        /// matched, is indexed. 
        /// </summary>
        public string Whitelist
        {
            get
            {
                return this.GetString("whitelist", null);
            }

            set
            {
                this.SetCacheValue("whitelist", value);
            }
        }

        /// <summary>
        /// Sets the specified slash-separate segment of the file path as the 
        /// field value
        /// </summary>
        public string HostSegment
        {
            set
            {
                this.SetCacheValue("host_segment", value);
            }
        }

        /// <summary>
        /// Sets the  name to populate in the <b>source</b> field for events
        /// from this monitor input. The same source name should not be used for 
        /// multiple data inputs.
        /// </summary>
        public string RenameSource
        {
            set
            {
                this.SetCacheValue("rename-source", value);
            }
        }
    }
}
