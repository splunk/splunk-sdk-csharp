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
    using System.Text;

    /// <summary>
    /// The <see cref="JobArgs"/> class extends <see cref="Args"/> for Job
    /// creation setters.
    /// </summary>
    public class JobArgs : Args
    {
        /// <summary>
        /// Specifies how to create a job using the 
        /// <see cref="JobCollection.Create(string, JobArgs)"/>
        /// method.       
        /// </summary>
        // C# disallows nested class to have the same name as
        // a property. Use 'Enum' suffix to differentiate.
        public enum ExecutionModeEnum
        {
            /// <summary>
            /// Runs a search asynchronously and returns a search job immediately.
            /// </summary>
            [CustomString("normal")]
            Normal,

            /// <summary>
            /// Runs a search synchronously and does not return a search job until 
            /// the search has finished.
            /// </summary>
            [CustomString("blocking")]
            Blocking,

            /// <summary>
            /// Runs a blocking search that is scheduled to run immediately, and then 
            /// returns the results of the search once completed. 
            /// </summary>
            [CustomString("oneshot")]
            Oneshot,
        }

        /// <summary>
        /// Specifies how to create a job using the 
        /// <see cref="JobCollection.Create(string, JobArgs)"/>
        /// method.       
        /// </summary>
        // C# disallow nested class to have the same name as
        // a property. Use 'Enum' suffix to differentiate.
        public enum SearchModeEnum
        {
            /// <summary>
            /// Searches historical data.
            /// </summary>
            [CustomString("normal")]
            Normal,

            /// <summary>
            /// <para>
            /// Searches live data. A realtime search may also be specified by 
            /// setting the "earliest_time" and "latest_time" parameters to begin 
            /// with "rt", even if the search_mode is set to "normal" or is not set. 
            /// </para>
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
        /// Sets the auto-cancel frequency check, in seconds. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this property to "0" indicates never auto-cancel.
        /// </para>
        /// <para>
        /// This property's default value is "0". 
        /// </para>
        /// </remarks>
        public int AutoCancel
        {
            set
            {
                this["auto_cancel"] = value;
            }
        }

        /// <summary>
        /// Sets the auto-finalize counter. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// When at least these many events have been processed, the
        /// job is finalized. 
        /// </para>
        /// <para>
        /// Setting this property to "0" indicates there is no limit.
        /// </para>
        /// <para>
        /// This property's default value is "0". 
        /// </para>
        /// </remarks>
        public int AutoFinalizeEventCount
        {
            set
            {
                this["auto_finalize_ec"] = value;
            }
        }

        /// <summary>
        /// Sets the auto-pause frequency check, in seconds. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this property to "0" indicates never auto-pause.
        /// </para>
        /// <para>
        /// This property's default value is "0". 
        /// </para>
        /// </remarks>
        public int AutoPause
        {
            set
            {
                this["auto_pause"] = value;
            }
        }

        /// <summary>
        /// Sets the inclusive earliest time bounds for the search. 
        /// </summary>
        /// <remarks>
        /// Be aware that although this property's value is a time stamp,
        /// it is left as a string to support Splunk relative time format--
        /// for instance, "+2d", which specifies two days from now.
        /// </remarks>
        public string EarliestTime
        {
            set
            {
                // convert date to string
                this["earliest_time"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether lookups should be applied to
        /// events.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this propery to true may slow searches
        /// significantly, depending on the nature of the lookups.
        /// </para>
        /// <para>
        /// This property's default value is true. 
        /// </para>
        /// </remarks>
        public bool EnableLookups
        {
            set
            {
                this["enable_lookups"] = value;
            }
        }

        /// <summary>
        /// Sets the search execution mode. 
        /// </summary>
        /// <remarks>
        /// This property's valid values are any of the following keywords: 
        /// "blocking", "oneshot", "normal".
        /// </remarks>
        public ExecutionModeEnum ExecutionMode
        {
            set
            {
                this["exec_mode"] = value.GetCustomString();
            }
        }

        /// <summary>
        /// Sets a value indicating whether this search should cause (and
        /// wait depending on the value of sync_bundle_replication) bundle 
        /// synchronization with all search peers. 
        /// </summary>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ForceBundleReplication
        {
            set
            {
                this["force_bundle_replication"] = value;
            }
        }

        /// <summary>
        /// Sets the search ID. 
        /// </summary>
        /// <remarks>
        /// If this property is not set, a random ID is generated.
        /// </remarks>
        public string Id
        {
            set
            {
                this["id"] = value;
            }
        }

        /// <summary>
        /// Sets the exclusive latest time bounds for the search. 
        /// </summary>
        /// <remarks>
        /// Be aware that although this property's value is a time stamp,
        /// it is left as a string to support Splunk relative time format--
        /// for instance, "+2d", which specifies two days from now.
        /// </remarks>
        public string LatestTime
        {
            set
            {
                this["latest_time"] = value;
            }
        }

        /// <summary>
        /// Sets the number of events that can be accessible in any given status 
        /// bucket. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// When a search is transformed, this property indicates the maximum number
        /// of events to store. Specifically, in all calls, <i>codeoffset</i> + <i>count</i>
        /// &lt;= max_count. 
        /// </para>
        /// <para>
        /// This property's default value is "1000". 
        /// </para>
        /// </remarks>
        public int MaxCount
        {
            set
            {
                this["max_count"] = value;
            }
        }

        // namespace??

        /// <summary>
        /// Sets the absolute time for any relative time specifier in the 
        /// search. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Be aware that although this property's value is a time stamp,
        /// it is left as a string to support Splunk relative time format--
        /// for instance, "+2d", which specifies two days from now.
        /// </para>
        /// <para>
        /// This property's default value is the current system time. 
        /// </para>
        /// </remarks>
        public string Now
        {
            set
            {
                this["now"] = value;
            }
        }

        /// <summary>
        /// Sets the MapReduce reduce phase on accumulated map values frequency,
        /// in seconds.
        /// </summary>
        public int ReduceFreq
        {
            set
            {
                this["reduce_freq"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to reload macro definitions from
        /// macros.conf. 
        /// </summary>
        /// <remarks>
        /// This property's default value is true.
        /// </remarks>
        public bool ReloadMacros
        {
            set
            {
                this["reload_macros"] = value;
            }
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
                this["remote_server_list"] = value.GetCsv();
            }
        }

        /// <summary>
        /// Sets the list of required fields returned in the search.
        /// </summary>
        public string[] Rf
        {
            set
            {
                this["rf"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the indexer blocks if the queue
        /// for this search is full. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property only applies to real-time searches.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool RtBlocking
        {
            set
            {
                this["rt_blocking"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the indexer prefilters the
        /// events. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property only applies to real-time searches.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool RtIndexfilter
        {
            set
            {
                this["rt_indexfilter"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum time to block, in seconds. This only applies to 
        /// real-time searches and when rt_blocking is true. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property only applies to real-time searches when the 
        /// <see cref="RtBlocking"/> property is set to true.
        /// </para>
        /// <para>
        /// This property's default value is "60".
        /// </para>
        /// </remarks>
        public int RtMaxblockSeconds
        {
            set
            {
                this["rt_maxblocksecs"] = value;
            }
        }

        /// <summary>
        /// Sets the queue size, in number of events, the indexer should use for
        /// this search. 
        /// </summary>
        /// <para>
        /// This property only applies to real-time searches.
        /// </para>
        /// <para>
        /// This property's default value is "10000".
        /// </para>
        public int RtQueueSize
        {
            set
            {
                this["rt_queue_size"] = value;
            }
        }

        /// <summary>
        /// Sets a search state listener with the search. 
        /// </summary>
        /// <remarks>
        /// The format of this string is:
        /// <para>
        /// search_state;results_condition;http_method;uri;
        /// </para>
        /// For example:
        /// <example>
        /// "onResults;true;POST;/servicesNS/admin/search/saved/search/foobar/notify;"
        /// </example>
        /// </remarks>
        public string SearchListener
        {
            set
            {
                this["search_listener"] = value;
            }
        }

        /// <summary>
        /// Sets the search mode.
        /// </summary>
        public SearchModeEnum SearchMode
        {
            set 
            {
                this["search_mode"] = value.GetCustomString();
            }
        }

        /// <summary>
        /// Sets a value indicating whether the search should run in a
        /// separate spawned process. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Be aware that searches against indexes must run in a separate process.
        /// </para>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// </remarks>
        public bool SpawnProcess
        {
            set
            {
                this["spawn_process"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of buckets to create.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Setting this property to "0" causes
        /// no timeline information to be generated.
        /// </para>
        /// <para>
        /// This property's default value is "0".
        /// </para>
        /// </remarks>
        /// </remarks>
        public int StatusBuckets
        {
            set
            {
                this["status_buckets"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether this search should wait for
        /// bundle replication to complete.
        /// </summary>
        public bool SyncBundleReplication
        {
            set
            {
                this["sync_bundle_replication"] = value;
            }
        }

        /// <summary>
        /// Sets the time format string, used to convert a formatted time string 
        /// from {start,end}_time into UTC seconds. 
        /// </summary>
        /// <remarks>
        /// This property's default value is "ISO-8601".
        /// </remarks>
        public string TimeFormat
        {
            set
            {
                this["time_format"] = value;
            }
        }

        /// <summary>
        /// Sets the number of seconds to keep this search after processing has 
        /// stopped. 
        /// </summary>
        /// <remarks>
        /// This property's default value is "86400" (24 hours).
        /// </remarks>
        public int Timeout
        {
            set
            {
                this["timeout"] = value;
            }
        }
    }
}
