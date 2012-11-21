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
    public class ClusterMasterInfo : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ClusterMasterInfo"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterMasterInfo(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets information about the active bundle for this master.
        /// wkcfix -- maybe introduce a new type called bundle?
        /// </summary>
        public Record ActiveBundle
        {
            get
            {
                return (Record)this.Validate().Get("active_bundle");
            }
        }

        /// <summary>
        /// Gets the number of bickets to fix when a peer goes offline. These 
        /// are the buckets that were on the peer that went offline and need
        /// copies created or made searchable to satisfy the replication and
        /// search factor configured on the master. For more information, refer 
        /// to What happens when a peer node goes down in the Splunk Managing 
        /// Indexers and Clusters manual.
        /// </summary>
        public int BucketsToFix
        {
            get
            {
                return this.GetInteger("active_bundle", -1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the cluster is ready for indexing.
        /// </summary>
        public bool IndexingReady
        {
            get
            {
                return this.GetBoolean("indexing_ready_flag", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the cluster has been initialized.
        /// </summary>
        public bool Initialized
        {
            get
            {
                return this.GetBoolean("initialized_flag", false);
            }
        }

        /// <summary>
        /// Gets the name of the master that is displayed in the Splunk Manager 
        /// page.
        /// </summary>
        public string Label
        {
            get
            {
                return this.GetString("label", null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the master is restarting the peers 
        /// in a cluster.
        /// </summary>
        public bool RollingRestart
        {
            get
            {
                return this.GetBoolean("rolling_restart_flag", false);
            }
        }

        /// <summary>
        /// Gets the most recent information reflecting any changes made to the
        /// master-apps configuration bundle. In steady state, this is equal to 
        /// active_bundle. If it is not equal, then pushing the latest bundle to
        /// all peers is in process (or needs to be started).
        /// wkcfix -- maybe introduce a new type called bundle?
        /// </summary>
        public Record LatestBundle
        {
            get
            {
                return (Record)this.Validate().Get("latest_bundle");
            }
        }
        
        /// <summary>
        /// Gets the time of the Master's creation.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.GetDate("start_time", DateTime.MaxValue);
            }
        }
    }
}