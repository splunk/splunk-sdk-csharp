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
    /// Extends Args for raw TcpInput creation setters
    /// </summary>
    public class TcpInputArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether SSL is used.
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
        /// values are ip, dns or none. The default is ip.
        /// ip sets the host to the IP address of the remote server sending 
        /// data. dns sets the host to the reverse DNS entry for the IP address 
        /// of the remote server sending data. none leaves the host as specified
        /// in inputs.conf, which is typically the Splunk system hostname.
        /// </summary>
        public string ConnectionHost
        {
            set
            {
                this["connection_host"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether this input is disabled.
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
        /// Sets the index in which to store all event receieved by this input.
        /// The default is "default".
        /// </summary>
        public string Index
        {
            set
            {
                this["index"] = value;
            }
        }

        /// <summary>
        /// Sets where the input processor should deposit the events it reads. 
        /// Valid values are parsingQueue or indexQueue Default is parsingQueue.
        /// Set queue to parsingQueue to apply props.conf and other parsing 
        /// rules to your data. For more information about props.conf and rules
        /// for timestamping and linebreaking, refer to props.conf and the 
        /// online documentation at Edit inputs.conf. Set queue to indexQueue to
        /// send your data directly into the index.
        /// </summary>
        public string Queue
        {
            set
            {
                this["queue"] = value;
            }
        }

        /// <summary>
        /// Sets the timeout value for adding a Done-key, in seconds. The 
        /// default is 10. If a connection over the port specified by name 
        /// remains idle after receiving data for specified number of seconds, 
        /// it adds a Done-key. This implies the last event has been completely 
        /// received.
        /// </summary>
        public int RawTcpDoneTimeout
        {
            set
            {
                this["rawTcpDoneTimeout"] = value;
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

        /// <summary>
        /// Sets the source key's initial value. The key is used during 
        /// parsing/indexing, in particular to set the source field during 
        /// indexing. It is also the source field used at search time. As a 
        /// convenience, the chosen string is prepended with 'source::'. Note: 
        /// Overriding the source key is generally not recommended. Typically, 
        /// the input layer provides a more accurate string to aid in problem 
        /// analysis and investigation, accurately recording the file from which
        /// the data was retreived. Consider use of source types, tagging, and 
        /// search wildcards before overriding this value. The default 
        /// is the input file path.
        /// </summary>
        public string Source
        {
            set
            {
                this["source"] = value;
            }
        }

        /// <summary>
        /// Sets the source type for events from this input. The ddefault is
        /// "audittrail", if signedaudit=true, or "fschange" if 
        /// signedaudit=false.
        /// </summary>
        public string SourceType
        {
            set
            {
                this["sourcetype"] = value;
            }
        }
    }
}
