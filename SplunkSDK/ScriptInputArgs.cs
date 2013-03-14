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
    /// Extends Args for EventType creation setters
    /// </summary>
    public class ScriptInputArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether the input script is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the host for events from this input. The default is whatever
        /// host sent the event.
        /// </summary>
        public string Host
        {
            set
            {
                this["host"] = value;
            }
        }

        /// <summary>
        /// Sets the index for events from this input. The defaults is the
        /// main index.
        /// </summary>
        public string Index
        {
            set
            {
                this["index"] = value;
            }
        }

        /// <summary>
        /// Sets the frequency of script execution. Valid values are either an 
        /// integer, in seconds, or a cron schedule. If a cron schedule is
        /// specified, the script is not executed on start-up. The default is 
        /// 60 seconds.
        /// </summary>
        public string Interval
        {
            set
            {
                this["interval"] = value;
            }
        }

        /// <summary>
        /// Sets the user to run the script as. If you provide a username, 
        /// Splunk generates an auth token for that user and passes it to the 
        /// script.
        /// </summary>
        public string PassAuth
        {
            set
            {
                this["passAuth"] = value;
            }
        }

        /// <summary>
        /// Sets the new name for the source field for the script.
        /// </summary>
        public string RenameSource
        {
            set
            {
                this["rename-source"] = value;
            }
        }

        /// <summary>
        /// Sets the source key/field for events from this input. Defaults to 
        /// the input file path. Sets the source key's initial value. The key 
        /// is used during parsing/indexing, in particular to set the source 
        /// field during indexing. It is also the source field used at search 
        /// time. As a convenience, the chosen string is prepended with 
        /// 'source::'.
        /// Note: Overriding the source key is generally not recommended. 
        /// Typically, the input layer provides a more accurate string to aid 
        /// in problem analysis and investigation, accurately recording the file
        /// from which the data was retreived. Consider use of source types, 
        /// tagging, and search wildcards before overriding this value.
        /// </summary>
        public string Source
        {
            set
            {
                this["source"] = value;
            }
        }

        /// <summary>
        /// Sets the sourcetype key/field for events from this input. If unset, 
        /// Splunk picks a source type based on various aspects of the data. As 
        /// a convenience, the chosen string is prepended with 'sourcetype::'. 
        /// There is no default. Sets the sourcetype key's initial value. The 
        /// key is used during parsing/indexing, in particular to set the source
        /// type field during indexing. It is also the source type field used at
        /// search time. Primarily used to explicitly declare the source type 
        /// for this data, as opposed to allowing it to be determined via 
        /// automated methods. This is typically important both for 
        /// searchability and for applying the relevant configuration for this 
        /// type of data during parsing and indexing.
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