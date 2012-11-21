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
    /// The ClusterMasterBuckets class represents a collection of the Cluster 
    /// Masters Peers. This is introduced in Splunk 5.0.
    /// </summary>
    public class ClusterMasterPeers : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterMasterPeers"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterMasterPeers(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the ID of the configuration bundle currently being used by the 
        /// master.
        /// </summary>
        public string ActiveBundleId
        {
            get
            {
                return this.GetString("active_bundle_id", null);
            }
        }

        /// <summary>
        /// Gets the initial bundle generation ID recognized by this peer. Any 
        /// searches from previous generations fail. The initial bundle 
        /// generation ID is created when a peer first comes online, restarts, 
        /// or recontacts the master.
        /// </summary>
        public string BaseGenerationId
        {
            get
            {
                return this.GetString("base_generation_id", null);
            }
        }

        /// <summary>
        /// Gets the list of buckets for this peer.
        /// </summary>
        public string[] Buckets
        {
            get
            {
                return this.GetStringArray("buckets", null);
            }
        }

        /// <summary>
        /// Gets the status of the cluster bundle. Valid values are:
        /// "ebundleTypeActive": Indicates that this is the bundle the peers are 
        /// currently using. "ebundleTypeLatest": Indicates the most up to date 
        /// bundle from the master. In steady state, it should match the active 
        /// bundle. If unapplied changes have been recently made, it differs 
        /// from the active bundle.
        /// </summary>
        public string BundleStatus
        {
            get
            {
                return this.GetString("bundle_status", null);
            }
        }

        /// <summary>
        /// Gets the set of buckets that need repair once you take the peer 
        /// offline.
        /// </summary>
        public string[] FixupSet
        {
            get
            {
                return this.GetStringArray("fixup_set", null);
            }
        }

        /// <summary>
        /// Gets the host and port advertised to peers for the data replication 
        /// channel. This value can be either of the form IP:port or 
        /// hostname:port.
        /// </summary>
        public string HostPortPair
        {
            get
            {
                return this.GetString("host_port_pair", null);
            }
        }

        /// <summary>
        /// Gets the name  for the peer that is displayed in the Splunk Manager 
        /// UI page.
        /// </summary>
        public string Label
        {
            get
            {
                return this.GetString("label", null);
            }
        }

        /// <summary>
        /// Gets the timestamp for last heartbeat recieved from the peer.
        /// </summary>
        public DateTime LastHeartBeat
        {
            get
            {
                return this.GetDate("last_heartbeat", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Gets the ID of the configuration bundle this peer is using.
        /// </summary>
        public string LatestBundleId
        {
            get
            {
                return this.GetString("latest_bundle_id", null);
            }
        }

        /// <summary>
        /// Gets the pending jobs requested by the master to this peer. If the 
        /// number exceeds the max_peer_build_load, the master does not send a 
        /// job to this peer to make a bucket searchable.
        /// </summary>
        public int PendingJobCount
        {
            get 
            {
                return this.GetInteger("pending_job_count", -1);
            }
        }

        /// <summary>
        /// Gets the number of buckets for which this peer is the primary. When 
        /// a peer is the primary for a bucket, the peer returns the results 
        /// from a search of that bucket. 
        /// </summary>
        public int PrimaryCount
        {
            get
            {
                return this.GetInteger("primary_count", -1);
            }
        }

        /// <summary>
        /// Gets the TCP port to listen for replicated data from another 
        /// cluster member.
        /// </summary>
        public int ReplicationPort
        {
            get
            {
                return this.GetInteger("replication_port", -1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to use SSL when sending replication 
        /// data.
        /// </summary>
        public bool ReplicationsUseSsl
        {
            get
            {
                return this.GetBoolean("replication_use_ssl", false);
            }
        }

        /// <summary>
        /// Gets a List of the number of buckets on the peer for each search 
        /// state for the bucket.
        /// wkcfix -- maybe introduce a new type called counter?
        /// </summary>
        public Record SearchStateCounter
        {
            get
            {
                return (Record)this.Validate().Get("search_state_counter");
            }
        }

        /// <summary>
        /// Gets the status of the peer. Valid values are: Up, Down, Pending,
        /// Detention, Restarting, DecommAwaitPeer, DecommFixingBuckets, 
        /// Decommissioned. Valid values for bucket status:
        /// Complete: complete (warm/cold) bucket. NonStreamingTarget: target of
        /// replication for already completed (warm/cold) bucket. 
        /// PendingTruncate: bucket pending truncation. PendingDiscard: bucket 
        /// pending discard. Standalone: bucket that is not replicated. 
        /// StreamingError: copy of streaming bucket where some error was 
        /// encountered. StreamingSource: streaming hot bucket on source side
        /// StreamingTarget: streaming hot bucket copy on target side. Unset: 
        /// uninitialized
        /// </summary>
        public string Status
        {
            get
            {
                return this.GetString("status", null);
            }
        }

        /// <summary>
        /// Gets a List of the number of buckets  on the peer for each bucket 
        /// status.
        /// wkcfix -- maybe introduce a new type called counter?
        /// </summary>
        public Record StatusCounter
        {
            get
            {
                return (Record)this.Validate().Get("status_counter");
            }
        }
    }
}
