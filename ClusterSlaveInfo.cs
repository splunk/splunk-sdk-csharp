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
    /// The ClusterMasterInfo class represents a collection of the Cluster 
    /// Masters Information. This is introduced in Splunk 5.0.
    /// </summary>
    public class ClusterSlaveInfo : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ClusterSlaveInfo"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterSlaveInfo(Service service, string path)
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
        /// Gets the initial bundle generation ID recognized by this peer. Any 
        /// searches from previous generations fail. The initial bundle 
        /// generation ID is created when a peer first comes online, restarts, 
        /// or recontacts the master.
        /// </summary>
        public int BaseGenerationId
        {
            get
            {
                return this.GetInteger("base_generation_id", -1);
            }
        }

        /// <summary>
        /// Gets the path on the server where bundles for this peer are located.
        /// </summary>
        public string BundlePath
        {
            get
            {
                return this.GetString("bundle_path", null);
            }
        }

        /// <summary>
        /// Gets the port on which to receive data from a forwarder.
        /// </summary>
        public int ForwarderDataReceivePort
        {
            get
            {
                return this.GetInteger("forwarderdata_rcv_port", -1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether SSL is used when receiving data from
        /// a forwarder.
        /// </summary>
        public bool ForwarderDataUseSsl
        {
            get
            {
                return this.GetBoolean("forwarderdata_use_ssl", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this peer is registered with the
        /// master in the cluster.
        /// </summary>
        public bool IsRegistered
        {
            get
            {
                return this.GetBoolean("is_registered", false);
            }
        }

        /// <summary>
        /// Gets a list of information about the most recent bundle downloaded 
        /// from the master.
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
        /// Gets the URI to the master node for this peer. This value is in the 
        /// form, host:port.
        /// </summary>
        public string MasterUri
        {
            get
            {
                return this.GetString("master_uri", null);
            }
        }

        /// <summary>
        /// Gets the port used to stream data to and from other peers.
        /// </summary>
        public int RawDataReceivePort
        {
            get
            {
                return this.GetInteger("rawdata_rcv_port", -1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether SSL is used when communicating with
        /// other peers in a cluster.
        /// </summary>
        public bool RawDataUseSsl
        {
            get
            {
                return this.GetBoolean("rawdata_use_ssl", false);
            }
        }

        /// <summary>
        /// Gets the peers restart state. Valid values: [TBD
        /// </summary>
        public string RestartState
        {
            get
            {
                return this.GetString("restart_state", null);
            }
        }

        /// <summary>
        /// Gets a value indicating the status of the peer. Valid values: Up
        /// Down, Pending, Detention, Restarting, DecommAvaitingPeer, 
        /// DecommFixingBuckets, Decommissioned.
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