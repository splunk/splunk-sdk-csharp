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
    /// The <see cref="TcpInput"/> class represents a raw TCP data input.
    /// This differs from a cooked TCP input in that this TCP input is in raw
    /// form, and is not processed (or "cooked").
    /// </summary>
    public class TcpInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TcpInput"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="path">The path.</param>
        public TcpInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the host of the remote server that sends data. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's possible values are:
        /// <list type="bullet">
        /// <item><b>"ip"</b> sets the host to the IP address of the remote 
        /// server sending data.</item>
        /// <item><b>"dns"</b> sets the host to the reverse DNS entry for the
        /// IP address of the remote server sending data.</item>
        /// <item><b>"none"</b> leaves the host as specified in inputs.conf,
        /// which is typically the Splunk system hostname.</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property's default value is "ip".
        /// </para>
        /// </remarks>
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
        /// Gets the input type of this object, Tcp (raw).
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.Tcp;
            }
        }

        /// <summary>
        /// Gets or sets the queue for this TCP input. 
        /// </summary>
        /// <remarks>
        /// This property's valid values are
        /// "parsingQueue" and "indexQueue".
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
        /// Gets or sets the initial source key for this TCP input.  
        /// This property's value is typically the input file path.
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
        /// Sets the timeout value for adding a "Done" key. 
        /// </summary>
        /// <remarks>
        /// If a connection over the input port specified by name remains 
        /// idle after receiving data for this specified number of seconds, it 
        /// adds a "Done" key, implying that the last event has been completely 
        /// received.
        /// </remarks>
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
        /// <returns>The connections.</returns>
        public TcpConnections Connections()
        {
            return new TcpConnections(this.Service, this.Path + "/connections");
        }
    }
}
