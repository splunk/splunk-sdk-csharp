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
    /// The <see cref="TcpSplunkInputArgs"/> class extends <see cref="Args"/> 
    /// for cooked TcpInput creation setters.
    /// </summary>
    public class TcpSplunkInputArgs : Args
    {
        /// <summary>
        /// Sets a value that indicates whether SSL is used.
        /// </summary>
        public bool Ssl
        {
            set
            {
                this["SSL"] = value;
            }
        }

        /// <summary>
        /// Sets the host of the remote server that sends data. Valid 
        /// values are "ip", "dns", or "none". The default value is "ip".
        /// </summary>
        /// <remarks>
        /// A value of "ip" sets the host to the IP address of the remote 
        /// server sending data. <br/>A value of "dns" sets the host to the 
        /// reverse DNS entry for the IP address of the remote server sending 
        /// data. <br/>A value of "none" leaves the host as specified in 
        /// inputs.conf, which is typically the Splunk system hostname.
        /// </remarks>
        public string ConnectionHost
        {
            set
            {
                this["connection_host"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether this input is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the host from which the indexer gets data.
        /// </summary>
        public string Host
        {
            set
            {
                this["host"] = value;
            }
        }

        /// <summary>
        /// Sets an input restriction to only allow this host to post data.
        /// </summary>
        public string RestrictToHost
        {
            set
            {
                this["restrictToHost"] = value;
            }
        }
    }
}
