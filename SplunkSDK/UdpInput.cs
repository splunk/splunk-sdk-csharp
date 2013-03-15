﻿/*
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The <see cref="UdpInput"/> class represents the <see cref="Input"/> 
    /// subclass Udp Input.
    /// </summary>
    public class UdpInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UdpInput"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public UdpInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the style of host connection. Valid values are: "ip", 
        /// "dns", and "none".
        /// </summary>
        public string ConnectionHost
        {
            get
            {
                return this.GetString("connection_host", null);
            }

            set
            {
                this.SetCacheValue("connection_host", value);
            }
        }

        /// <summary>
        /// Gets the group of this TCP input.
        /// </summary>
        public string Group
        {
            get
            {
                return this.GetString("group");
            }
        }

        /// <summary>
        /// Gets or sets the source host of this TCP input where this indexer 
        /// gets its data.
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
        /// Gets or sets the index name of this TCP input.
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
        /// Gets the input type of this object, Udp.
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.Udp;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether Splunk prepends a 
        /// timestamp and hostname to incoming events.
        /// </summary>
        public bool NoAppendingTimeStamp
        {
            get
            {
                return this.GetBoolean("no_appending_timestamp", false);
            }

            set
            {
                this.SetCacheValue("no_appending_timestamp", value);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether Splunk removes the 
        /// priority field from incoming events. 
        /// </summary>
        public bool NoPriorityStripping
        {
            get
            {
                return this.GetBoolean("no_priority_stripping", false);
            }

            set
            {
                this.SetCacheValue("no_priority_stripping", value);
            }
        }

        /// <summary>
        /// Gets or sets the  queue for this TCP input. Valid values are:
        /// "parsingQueue" and "indexQueue".
        /// </summary>
        public string Queue
        {
            get
            {
                return this.GetString("queue", null);
            }

            set
            {
                this.SetCacheValue("queue", value);
            }
        }

        /// <summary>
        /// Gets or sets the initial source key for this TCP input. Typically 
        /// this value is the input file path.
        /// </summary>
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
        /// Gets or sets the source type for events from this TCP input.
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
        /// Returns an object that contains the inbound raw TCP connections.
        /// </summary>
        /// <returns>The connections</returns>
        public UdpConnections Connections()
        {
            return new UdpConnections(this.Service, this.Path + "/connections");
        }
    }
}
