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

using System.Text;

namespace Splunk
{
    /// <summary>
    /// The <see cref="JobExportArgs"/> class contains arguments for getting results
    /// using the <see cref="Service.Export(string, JobExportArgs)" /> class.
    /// </summary>
    public class JobExportArgs : Args
    {
        /// <summary>
        /// Specifies the format for the returned output.
        /// </summary>
        // C# disallows nested class to have the same name as
        // a property. Use 'Enum' suffix to differentiate.
        public enum OutputModeEnum
        {
            /// <summary>
            /// Returns output in Atom format.
            /// </summary>
            [CustomString("atom")]
            Atom,

            /// <summary>
            /// Returns output in CSV format.
            /// </summary>
            [CustomString("csv")]
            Csv,

            /// <summary>
            /// Returns output in JSON format.
            /// </summary>
            [CustomString("json")]
            Json,

            /// <summary>
            /// Returns output in JSON_COLS format.
            /// </summary>
            [CustomString("json_cols")]
            JsonColumns,

            /// <summary>
            /// Returns output in JSON_ROWS format.
            /// </summary>
            [CustomString("json_rows")]
            JsonRows,

            /// <summary>
            /// Returns output in raw format.
            /// </summary>
            [CustomString("raw")]
            Raw,

            /// <summary>
            /// Returns output in XML format.
            /// </summary>
            [CustomString("xml")]
            Xml,
        }

        /// <summary>
        /// Specifies how to create a job using the <see cref="JobCollection.Create(string, JobArgs)"/>
        /// method.       
        /// </summary>
        // C# disallows nested class to have the same name as
        // a property. Use 'Enum' suffix to differentiate.
        public enum SearchModeEnum
        {
            /// <summary>
            /// Searches historical data.
            /// </summary>
            [CustomString("normal")]
            Normal,

            /// <summary>
            /// Searches live data. A real-time search may also be specified by 
            /// setting the "earliest_time" and "latest_time" parameters to begin 
            /// with "rt", even if the search_mode is set to "normal" or is not set. 
            /// <para>
            /// If both the "earliest_time" and "latest_time" parameters are set to 
            /// "rt", the search represents all appropriate live data that was 
            /// received since the start of the search.
            /// </para>
            /// <para>
            /// If both the "earliest_time" and "latest_time" parameters are set to 
            /// "rt" followed by a relative time specifier, a sliding window is used 
            /// where the time bounds of the window are determined by the relative 
            /// time specifiers and are continuously updated based on current time.   
            /// </para>         
            /// </summary>
            [CustomString("realtime")]
            Realtime,
        }

        /// <summary>
        /// Specifies how to truncate lines to achieve the value in 
        /// <see cref="MaximumLines"/>
        /// </summary>
        // C# disallows nested class to have the same name as
        // a property. Use 'Enum' suffix to differentiate.
        public enum TruncationModeEnum
        {
            /// <summary>
            /// Use the "abstract" truncation mode.
            /// </summary>
            [CustomString("abstract")]
            Abstract,

            /// <summary>
            /// Use the "truncate" truncation mode.
            /// </summary>
            [CustomString("truncate")]
            Truncate,
        }

            /* BEGIN AUTOGENERATED CODE WITH MANUAL FIXUP */

        /// <summary>
        /// Sets the earliest time in the time range to search, based on the index time. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value is a time string. The time string can be a UTC
        /// time (with fractional seconds), a relative time specifier (to now),
        /// or a formatted time string.
        /// </para>
        /// </remarks>
        public string IndexEarliest
        {
            set { this["index_earliest"] = value; }
        }

        /// <summary>
        /// Sets the latest time in the time range to search, based on the index time. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value is a time string. The time string can be a UTC
        /// time (with fractional seconds), a relative time specifier (to now),
        /// or a formatted time string.
        /// </para>
        /// </remarks>
        public string IndexLatest
        {
            set { this["index_latest"] = value; }
        }

        /// <summary>
        /// Sets the format of the output.
        /// </summary>
        public OutputModeEnum OutputMode
        {
            set { this["output_mode"] = value.GetCustomString(); }
        }

        /// <summary>
        /// Sets the number of seconds of inactivity after which to 
        /// automatically cancel a job. 
        /// </summary>
        public int AutoCancel
        {
            set { this["auto_cancel"] = value; }
        }

        /// <summary>
        /// Sets the number of events to process after which to auto-finalize the search. 
        /// </summary>
        /// <remarks>
        /// Setting this property to "0" indicates there is no limit.
        /// </remarks>
        public int AutoFinalizeEventCount
        {
            set { this["auto_finalize_ec"] = value; }
        }

        /// <summary>
        /// Sets the number of seconds of inactivity after which to automatically pause a job. 
        /// </summary>
        /// <remarks>
        /// Setting this property to "0" indicates never auto-pause.
        /// </remarks>
        public int AutoPause
        {
            set { this["auto_pause"] = value; }
        }

        /// <summary>
        /// Sets the earliest time in the time range to search. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value is a time string. The time string can be a UTC
        /// time (with fractional seconds), a relative time specifier (to now),
        /// or a formatted time string.
        /// </para>
        /// </remarks>
        public string EarliestTime
        {
            set { this["earliest_time"] = value; }
        }

        /// <summary>
        /// Sets a value indicating whether to enable lookups for this search. 
        /// </summary>
        /// <remarks>
        /// Enabling lookups might slow searches significantly depending
        /// on the nature of the lookups.
        /// </remarks>
        public bool EnableLookups
        {
            set { this["enable_lookups"] = value; }
        }

        /// <summary>
        /// Sets a value indicating whether this search should cause 
        /// (and wait depending on the value of <see cref="SynchronizeBundleReplication"/>)
        /// bundle synchronization with all search peers.
        /// </summary>
        public bool ForceBundleReplication
        {
            set { this["force_bundle_replication"] = value; }
        }

        /// <summary>
        /// Specifies the latest time in the time range to search. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value is a time string. The time string can be a UTC
        /// time (with fractional seconds), a relative time specifier (to now),
        /// or a formatted time string.
        /// </para>
        /// </remarks>
        public string LatestTime
        {
            set { this["latest_time"] = value; }
        }

        /// <summary>
        /// Sets the number of seconds to run this search before finalizing. 
        /// </summary>
        /// <remarks>
        /// Setting this property to "0" indicates never finalize.
        /// </remarks>
        public int MaximumTime
        {
            set { this["max_time"] = value; }
        }

        /// <summary>
        /// Sets the application namespace in which to restrict searches.
        /// </summary>
        public string Namespace
        {
            set { this["namespace"] = value; }
        }

        /// <summary>
        /// Specifies a time string that sets the absolute time used for any relative
        /// time specifier in the search. 
        /// </summary>
        /// <remarks>
        /// <para> 
        /// You can specify a relative time modifier for this property. 
        /// For example, specify +2d to specify the current time plus two days. 
        /// If you specify a relative time modifier both in this parameter and in 
        /// the search string, the search string modifier takes precedence.
        /// </para>
        /// <para>
        /// For information about relative time modifiers, see 
        /// <a href="http://docs.splunk.com/Documentation/Splunk/latest/SearchReference/SearchTimeModifiers" target="_blank">Time
        /// modifiers for search</a> in the Search Reference.
        /// </para>
        /// <para>
        /// This property's default value is the current system time.
        /// </para>
        /// </remarks>
        public string Now
        {
            set { this["now"] = value; }
        }

        /// <summary>
        /// Sets the time to wait between running the MapReduce phase on accumulated
        /// map values.
        /// </summary>
        public int ReduceFrequency
        {
            set { this["reduce_freq"] = value; }
        }

        /// <summary>
        /// Sets a value indicating whether to reload macro definitions from the
        /// macros.conf configuration file.
        /// </summary>
        public bool ReloadMacros
        {
            set { this["reload_macros"] = value; }
        }

        /// <summary>
        /// Sets a list of (possibly wildcarded) servers from which to pull raw events. 
        /// </summary>
        /// <remarks>
        /// This same server list is used in subsearches.
        /// </remarks>
        public string[] RemoteServerList
        {
            set
            {
                // string[] will be encoded as multiple occurances
                // of the same parameter of the value set. That is not 
                // what we want.
                this["remote_server_list"] = value.ToCsv();
            }
        }

        /// <summary>
        /// Sets one or more required fields to the search. 
        /// </summary>
        /// <remarks>
        /// These fields, even if not referenced or used directly by the search, 
        /// are still included by the events and summary endpoints. Splunk Web 
        /// uses these fields to prepopulate panels in the Search view.
        /// </remarks>
        public string[] RequiredFieldList
        {
            set { this["rf"] = value; }
        }

        /// <summary>
        /// Sets a value indicating whether the indexer blocks if the queue for
        /// this search is full. 
        /// </summary>
        /// <remarks>
        /// This property only applies to real-time searches.
        /// </remarks>
        public bool RealtimeBlocking
        {
            set { this["rt_blocking"] = value; }
        }

        /// <summary>
        /// Sets a value indicating whether the indexer pre-filters events. 
        /// </summary>
        /// <remarks>
        /// This property only applies to real-time searches.
        /// </remarks>
        public bool RealtimeIndexFilter
        {
            set { this["rt_indexfilter"] = value; }
        }

        /// <summary>
        /// Sets the number of seconds indicating the maximum time to block. 
        /// </summary>
        /// <remarks>
        /// Setting this property to "0" indicates no limit. 
        /// For real-time searches with the <see cref="RealtimeBlocking"/> 
        /// property set to true.
        /// </remarks>
        public int RealtimeMaximumBlockSeconds
        {
            set { this["rt_maxblocksecs"] = value; }
        }

        /// <summary>
        /// Sets the queue size (in events) that the indexer should use for this search. 
        /// </summary>
        /// <remarks>
        /// This property only applies to real-time searches.
        /// </remarks>
        public int RealtimeQueueSize
        {
            set { this["rt_queue_size"] = value; }
        }

        /// <summary>
        /// Sets a string that registers a search state listener with the search. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use the format: 
        /// <code>
        /// search_state;results_condition;http_method;uri;
        /// </code>
        /// </para>
        /// <para>
        /// For example:
        /// <code>
        /// search_listener=onResults;true;POST;/servicesNS/admin/search/saved/search/foobar/notify;
        /// </code> 
        /// </para>
        /// </remarks>
        public string SearchListener
        {
            set { this["search_listener"] = value; }
        }

        /// <summary>
        /// Sets the search mode ("normal" or "realtime").
        /// </summary>
        public SearchModeEnum SearchMode
        {
            set { this["search_mode"] = value.GetCustomString(); }
        }

        /// <summary>
        /// Sets a value indicating whether this search should wait for bundle 
        /// replication to complete.
        /// </summary>
        public bool SynchronizeBundleReplication
        {
            set { this["sync_bundle_replication"] = value; }
        }

        /// <summary>
        /// Sets the format for converting a formatted time string from {start,end}_time 
        /// into UTC seconds. 
        /// </summary>
        /// <remarks>
        /// This property's default value is "ISO-8601".
        /// </remarks>
        public string TimeFormat
        {
            set { this["time_format"] = value; }
        }

        /// <summary>
        /// Sets the number of seconds to keep this search after processing has stopped.
        /// </summary>
        public int Timeout
        {
            set { this["timeout"] = value; }
        }

        /// <summary>
        /// Sets the maximum number of lines that any single event's <b>_raw</b> field 
        /// should contain.
        /// </summary>
        public int MaximumLines
        {
            set { this["max_lines"] = value; }
        }

        /// <summary>
        /// Sets a UTC time format.
        /// </summary>
        public string OutputTimeFormat
        {
            set { this["output_time_format"] = value; }
        }

        /// <summary>
        /// Sets the type of segmentation to perform on the data, 
        /// including an option to perform key/value segmentation.
        /// </summary>
        public string Segmentation
        {
            set { this["segmentation"] = value; }
        }

        /// <summary>
        /// Sets a value indicating how to truncate lines to achieve the 
        /// value in <see cref="MaximumLines"/>
        /// </summary>
        public TruncationModeEnum TruncationMode
        {
            set { this["truncation_mode"] = value.GetCustomString(); }
        }

        /* END AUTOGENERATED CODE */
    }
}
