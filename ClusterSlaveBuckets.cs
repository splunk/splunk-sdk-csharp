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
    using System;

    /// <summary>
    /// The ClusterMasterInfo class represents a collection of the Cluster 
    /// Masters Information. This is introduced in Splunk 5.0.
    /// </summary>
    public class ClusterSlaveBuckets : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ClusterSlaveBuckets"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterSlaveBuckets(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the a value that indicates the time of the earliest event in 
        /// this bucket. 
        /// </summary>
        public DateTime EarliestTime 
        {
            get
            {
                return this.GetDate("earliest_time", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Gets the generation ID for this peer.
        /// </summary>
        public int GenerationId
        {
            get
            {
                return this.GetInteger("generation_id", -1);
            }
        }

        /// <summary>
        /// Gets the a value that indicates the time of the latest event in 
        /// this bucket. 
        /// </summary>
        public DateTime LatestTime
        {
            get
            {
                return this.GetDate("latest_time", DateTime.MinValue);
            }
        }

        /// <summary>
        /// Gets a value indicating if the bucket is searchable. Valid values:
        /// "Searchable", "Unsearchable".
        /// </summary>
        public string SearchState
        {
            get
            {
                return this.GetString("search_state", null);
            }
        }

        /// <summary>
        /// Gets a value indicating the status of the bucket. Valid values: 
        /// Complete, StreamingSource, StreamingTarget, NonStreamingTarget,
        /// StreamingError, PendingTruncate, PendingDiscard, Standalone.
        /// </summary>
        public string Status
        {
            get
            {
                return this.GetString("status", null);
            }
        }
    }
}