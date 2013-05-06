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
        /// Specifies how to create a job using the <see cref="JobCollection.Create(string, JobArgs)"/>
        /// method.       
        /// </summary>
        // C# disallow nested class to have the same name as
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
            /// Runs a blocking search that is scheduled to run immediately, then 
            /// returns the results of the search once completed. 
            /// </summary>
            [CustomString("oneshot")]
            Oneshot,
        }

        /// <summary>
        /// Specifies how to create a job using the <see cref="JobCollection.Create(string, JobArgs)"/>
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
        /// Sets the auto-cancel frequency check, in seconds. The default
        /// value is 0. 
        /// A value of zero means never auto-cancel.
        /// </summary>
        public int AutoCancel
        {
            set
            {
                this["auto_cancel"] = value;
            }
        }

        /// <summary>
        /// Sets the auto-finalize counter. When at least these many events have
        /// been processed, the job is finalized. The default value is 0. A
        /// value of zero means no limit.
        /// </summary>
        public int AutoFinalizeEventCount
        {
            set
            {
                this["auto_finalize_ec"] = value;
            }
        }

        /// <summary>
        /// Sets the auto-pause frequency check, in seconds. The default is 0. 
        /// A value of zero means never auto-pause.
        /// </summary>
        public int AutoPause
        {
            set
            {
                this["auto_pause"] = value;
            }
        }

        /// <summary>
        /// Sets the inclusive earliest time bounds for the search. Note that 
        /// although this is a time stamp, it is left as a string in order to 
        /// support Splunk relative time format, such as "+2d" that specifies 
        /// two days from now.
        /// </summary>
        public string EarliestTime
        {
            set
            {
                // convert date to string
                this["earliest_time"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether lookups should be applied to
        /// events.
        /// The default value is true. Specifying true may slow searches
        /// significantly depending on the nature of the lookups.
        /// </summary>
        public bool EnableLookups
        {
            set
            {
                this["enable_lookups"] = value;
            }
        }

        /// <summary>
        /// Sets the search execution mode. Valid values are from the list, 
        /// "blocking", "oneshot", "normal".
        /// </summary>
        public ExecutionModeEnum ExecutionMode
        {
            set
            {
                this["exec_mode"] = value.GetCustomString();
            }
        }

        /// <summary>
        /// Sets a value that indicates whether this search should cause (and
        /// wait depending on the value of sync_bundle_replication) for bundle 
        /// synchronization with all search peers. The default value is false.
        /// </summary>
        public bool ForceBundleReplication
        {
            set
            {
                this["force_bundle_replication"] = value;
            }
        }

        /// <summary>
        /// Sets the search ID. If unset, a random ID is generated.
        /// </summary>
        public string Id
        {
            set
            {
                this["id"] = value;
            }
        }

        /// <summary>
        /// Sets the exclusive latest time bounds for the search. Note that 
        /// although this is a time stamp, it is left as a string in order to 
        /// support Splunk relative time format, such as "+2d" that specifies 
        /// two days from now.
        /// </summary>
        public string LatestTime
        {
            set
            {
                this["latest_time"] = value;
            }
        }

        /// <summary>
        /// Sets the number of events that can be accessible in any given status 
        /// bucket. When a search is transformed, the maximum number of events 
        /// to store. Specifically, in all calls, codeoffset+count
        /// &lt;= max_count. The default is 1000. 
        /// </summary>
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
        /// search. Defaults to the current system time. Note that although 
        /// this is a time stamp, it is left as a string in order to support 
        /// Splunk relative time format, such as "+2d" that specifies two days 
        /// from now.
        /// </summary>
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
        /// Sets a value that indicates whether to reload macro definitions from
        /// macros.conf. The default value is true.
        /// </summary>
        public bool ReloadMacros
        {
            set
            {
                this["reload_macros"] = value;
            }
        }

        /// <summary>
        /// Sets a list of (possibly wildcarded) servers from which to pull raw events. 
        /// This same server list is used in subsearches.
        /// </summary>
        public string[] RemoteServerList
        {
            set
            {
                // string[] will be encoded as multiple occurances
                // of the same parameter of the value set. That is not 
                // what we want.
                var csv = new StringBuilder();
                for (int i = 0, n = value.Length; i < n; i++)
                {
                    if (i != 0)
                    {
                        csv.Append(",");
                    }
                    csv.Append(value[i]);
                }

                this["remote_server_list"] = csv.ToString();
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
        /// Sets a value that indicates whether the indexer blocks if the queue
        /// for this search is full. This only applies to real-time searches.
        /// The default value is false.
        /// </summary>
        public bool RtBlocking
        {
            set
            {
                this["rt_blocking"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether the indexer prefilters the
        /// events. This only applies to real-time searches. The default value
        /// is true.
        /// </summary>
        public bool RtIndexfilter
        {
            set
            {
                this["rt_indexfilter"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum time to block, in seconds. This only applies to 
        /// real-time searches and when rt_blocking is true. The default value 
        /// is 60.
        /// </summary>
        public int RtMaxblockSeconds
        {
            set
            {
                this["rt_maxblocksecs"] = value;
            }
        }

        /// <summary>
        /// Sets the queue size, in number of events, the indexer should use for
        /// this search. This only applies for real-time searches. The default 
        /// value is 10,000.
        /// </summary>
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
        /// Sets a value that indicates whether the search should run in a
        /// separate spawned process. The default value is true. 
        /// Note: searches against indexes must run in a separate process.
        /// </summary>
        public bool SpawnProcess
        {
            set
            {
                this["spawn_process"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of buckets to create. A value of zero causes
        /// no timeline information to be generated. The default value is 0.
        /// </summary>
        public int StatusBuckets
        {
            set
            {
                this["status_buckets"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether this search should wait for
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
        /// from {start,end}_time into UTC seconds. It defaults to ISO-8601.
        /// </summary>
        public string TimeFormat
        {
            set
            {
                this["time_format"] = value;
            }
        }

        /// <summary>
        /// Sets the number of seconds to keep this search after processing has 
        /// stopped. The default is 86400 (24 hours).
        /// </summary>
        public int Timeout
        {
            set
            {
                this["timeout"] = value;
            }
        }
    }
}
