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
    /// Extends Args for OutputSyslog creation setters
    /// </summary>
    public class OutputSyslogArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether the forwarder is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the syslog priority value. 
        /// </summary>
        public int Priority
        {
            set
            {
                this["priority"] = value;
            }
        }

        /// <summary>
        /// Sets the host:port of the server where the syslog is sent.
        /// </summary>
        public string Server
        {
            set
            {
                this["server"] = value;
            }
        }

        /// <summary>
        /// Sets a rule for handling data in addition to that provided by the 
        /// "syslog" sourcetype. By default, there is no value for 
        /// SyslogSourceType.
        /// This string is used as a substring match against the sourcetype key.
        /// For example, if the string is set to 'syslog', then all source types
        /// containing the string "syslog" receives this special treatment.
        /// To match a source type explicitly, use the pattern 
        /// "sourcetype::sourcetype_name." For example 
        /// syslogSourcetype = sourcetype::apache_common -- Data that is 
        /// "syslog" or matches this setting is assumed to already be in syslog 
        /// format. Data that does not match the rules has a header, potentially
        /// a timestamp, and a hostname added to the front of the event. This is 
        /// how Splunk causes arbitrary log data to match syslog expectations.
        /// </summary>
        public string SyslogSourceType
        {
            set
            {
                this["syslogSourceType"] = value;
            }
        }

        /// <summary>
        /// Sets Format of timestamp to add at start of the events to be 
        /// forwarded. The format is a strftime-style timestamp formatting 
        /// string. See $SPLUNK_HOME/etc/system/README/outputs.conf.spec for 
        /// details.
        /// </summary>
        public string TimestampFormat
        {
            set
            {
                this["timestampformat"] = value;
            }
        }

        /// <summary>
        /// Sets the protocol to use to send syslog data. Valid values: 
        /// tcp, or udp.
        /// </summary>
        public string Type
        {
            set
            {
                this["type"] = value;
            }
        }
    }
}