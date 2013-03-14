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
    /// Represents the Input subclass Tcp (raw) Input
    /// </summary>
    public class TcpInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpInput"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public TcpInput(Service service, string path)
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
        /// Sets a value indicating whether this input is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
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
        /// Gets the Input type of this object, Tcp (raw).
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.Tcp;
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
        /// Gets the incoming host restriction for this TCP input. When
        /// specified, this input only accepts data from the specified host. 
        /// </summary>
        public string RestrictToHost
        {
            get
            {
                return this.GetString("restrictToHost", null);
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
        /// Gets or sets a value indicating whether this TCP input is using 
        /// secure socket layer (SSL).
        /// </summary>
        /// <returns></returns>
        public bool SSL
        {
            get
            {
                return this.GetBoolean("SSL", false);
            }

            set
            {
                this.SetCacheValue("SSL", value);
            }
        }

        /// <summary>
        /// Sets the timeout value for adding a Done key. 
        /// If a connection over the input port specified by name remains 
        /// idle after receiving data for this specified number of seconds, it 
        /// adds a Done key, implying that the last event has been completely 
        /// received.
        /// </summary>
        public int RawTcpDoneTimeout
        {
            set
            {
                this.SetCacheValue("rawTcpDoneTimeout", value);
            }
        }

        /// <summary>
        /// Returns an object that contains the inbound raw TCP connections.
        /// </summary>
        /// <returns>The connections</returns>
        public TcpConnections Connections()
        {
            return new TcpConnections(this.Service, this.Path + "/connections");
        }
    }
}
