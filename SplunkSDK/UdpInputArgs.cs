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
    /// The <see cref="UdpInputArgs"/> class extends <see cref="Args"/> for 
    /// UdpInput creation properties.
    /// </summary>
    public class UdpInputArgs : Args
    {
        /// <summary>
        /// Sets the host of the remote server that sends data. 
        /// </summary>
        /// <remarks>
        /// This property's valid values are:
        /// <list type="bullet">
        /// <item><b>"ip"</b> sets the host to the IP address of the remote 
        /// server sending data.</item>
        /// <item><b>"dns"</b> sets the host to the 
        /// reverse DNS entry for the IP address of the remote server sending 
        /// data.</item>
        /// <item><b>"none"</b> leaves the host as specified in
        /// inputs.conf, which is typically the Splunk system hostname.</item>
        /// </list>
        /// <para>
        /// This property's default value is "ip".
        /// </para>
        /// </remarks>
        public string ConnectionHost
        {
            set
            {
                this["connection_host"] = value;
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
        /// Sets a value indicating whether Splunk prepends a timestamp 
        /// and host name to incoming events.
        /// </summary>
        public bool NoAppendingTimestamp
        {
            set
            {
                this["no_appending_timestamp"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether Splunk removes the priority
        /// field from incoming syslog events.
        /// </summary>
        public bool NoPriorityStripping
        {
            set
            {
                this["no_priority_stripping"] = value;
            }
        }

        /// <summary>
        /// Sets where the input processor should deposit the events it reads. 
        /// Valid values are "parsingQueue" or "indexQueue". The default value
        /// is "parsingQueue".
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's valid values are:
        /// <list type="bullet">
        /// <item><b>"parsingQueue"</b> applies props.conf and other parsing 
        /// rules to your data. For more information about 
        /// props.conf and rules for timestamping and linebreaking, refer to 
        /// the <see href="http://docs.splunk.com/Documentation/Splunk/latest/Data/Editinputs.conf">Edit inputs.conf</see>
        /// topic in the Getting Data In manual.</item>
        /// <item><b>"indexQueue"</b> sends your data directly into the
        /// index.</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property's default value is "parsingQueue".
        /// </para>
        /// </remarks>
        public string Queue
        {
            set
            {
                this["queue"] = value;
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
        /// convenience, the chosen string is prepended with 'source::'. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Overriding the source key is generally not recommended. 
        /// Typically, the input layer provides a more accurate string to aid 
        /// in problem analysis and investigation, accurately recording the 
        /// file from which the data was retrieved. Consider use of source 
        /// types, tagging, and search wildcards before overriding this value. 
        /// </para>
        /// <para>
        /// This property's default value is the input file path.
        /// </para>
        /// </remarks>
        public string Source
        {
            set
            {
                this["source"] = value;
            }
        }

        /// <summary>
        /// Sets the source type for events from this input. 
        /// </summary>
        /// <remarks>
        /// This property's default value is:
        /// <list type="bullet">
        /// <item>"audittrail" if signedaudit=true</item>
        /// <item>"fschange" if signedaudit=false</item>
        /// </list>
        /// </remarks>
        public string SourceType
        {
            set
            {
                this["sourcetype"] = value;
            }
        }
    }
}
