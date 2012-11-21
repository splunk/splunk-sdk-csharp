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
    /// The ClusterConfig class represents a collection of Cluster configs.
    /// This is introduced in Splunk 5.0.
    /// </summary>
    public class ClusterConfig : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClusterConfig"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterConfig(Service service, string path)
            : base(service, path)
        {
        }
 
        /// <summary>
        /// Gets or sets the timeout, in seconds, for establishing a connection
        /// between cluster nodes.
        /// </summary>
        public int ConnectionTimeout
        {
            get
            {
                return this.GetInteger("cxn_timeout");
            }

            set
            {
                this.SetCacheValue("cxn_timeout", value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this node is disabled.
        /// </summary>
        public bool Disabled
        {
            get
            {
                return this.GetBoolean("disabled");
            }
        }

        /// <summary>
        /// Sets the port from which to receive data from a forwarder.
        /// </summary>
        public int ForwarderDataReceivePort
        {
            set
            {
                this.SetCacheValue("forwarderdata_rcv_port", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether to use SSL when receiving data from
        /// a forwarder.
        /// </summary>
        public bool ForwarderDataUseSsl
        {
            set
            {
                this.SetCacheValue("forwarderdata_use_ssl", value);
            }
        }

        /// <summary>
        /// Sets The time, in seconds, that a peer attempts to send a heartbeat
        /// to the master. This attribute is only valid for peer nodes in a 
        /// cluster.
        /// </summary>
        public int HeartbeatPeriod
        {
            set
            {
                this.SetCacheValue("heartbeat_period", value);
            }
        }

        /// <summary>
        /// Gets or sets the time, in seconds, before a master considers a peer
        /// down. Once a peer is down, the master initiates steps to replicate 
        /// buckets from the dead peer to its live peers. The default is 60
        /// secxonds. This attribute is valid only for the master node in a
        /// cluster configuration.
        /// </summary>
        public int HeartbeatTimeout
        {
            get
            {
                return this.GetInteger("heartbeat_timeout");
            }

            set
            {
                this.SetCacheValue("heartbeat_timeout", value);
            }
        }

        /// <summary>
        /// Gets or sets the URI of the cluster master. This attribute is valid
        /// only for non-master nodes in a cluster configuration.
        /// </summary>
        public string MasterUri
        {
            get
            {
                return this.GetString("master_uri");
            }

            set
            {
                this.SetCacheValue("master_uri", value);
            }
        }

        /// <summary>
        /// Gets the number of jobs the peer can have in progress at any
        /// time that make the bucket searchable.
        /// </summary>
        public int MaxPeerBuildLoad
        {
            get
            {
                return this.GetInteger("max_peer_build_load");
            }
        }

        /// <summary>
        /// Gets the number of replications that can be ongoing as a 
        /// target.
        /// </summary>
        public int MaxPeerReplicationsLoad
        {
            get
            {
                return this.GetInteger("max_peer_rep_load");
            }
        }

        /// <summary>
        /// Gets or sets the operational mode for this cluster node. Valid
        /// values are, "master", "slave", "searchhead", or "disabled". Note 
        /// that only one Master may exist per cluster.
        /// </summary>
        public string Mode
        {
            get
            {
                return this.GetString("mode");
            }

            set
            {
                this.SetCacheValue("mode", value);
            }
        }

        /// <summary>
        /// Gets or sets the time, in seconds, that a master waits for peers to
        /// add themselves to the cluster conifguration.
        /// </summary>
        public int QuietPeriod
        {
            get
            {
                return this.GetInteger("quiet_period");
            }

            set
            {
                this.SetCacheValue("quiet_period", value);
            }
        }

        /// <summary>
        /// Gets or sets the timeout, in seconds, for receiving data between 
        /// cluster nodes. The default is 60 seconds.
        /// </summary>
        public int ReceiveTimeout
        {
            get
            {
                return this.GetInteger("rcv_timeout");
            }

            set
            {
                this.SetCacheValue("rcv_timeout", value);
            }
        }

        /// <summary>
        /// Gets or sets the address on which a peer is available for accepting 
        /// replication data. This is useful in the cases where a peer host 
        /// machine has multiple interfaces and only one of them can be reached 
        /// by another splunkd instance.
        /// </summary>
        public string RegisterReplicationAddress
        {
            get
            {
                return this.GetString("register_replication_address");
            }

            set
            {
                this.SetCacheValue("register_replication_address", value);
            }
        }

        /// <summary>
        /// Gets or sets the IP address that advertises this indexer to search 
        /// heads.
        /// </summary>
        public string RegisterSearchAddress
        {
            get
            {
                return this.GetString("register_search_address");
            }

            set
            {
                this.SetCacheValue("register_search_address", value);
            }
        }

        /// <summary>
        /// Sets the timeout, in seconds, for establishing a connection for 
        /// replicating data. The default is 5 seconds.
        /// </summary>
        public int ReplicationConnectionTimeout 
        {
            set
            {
                this.SetCacheValue("rep_cxn_timeout", value);
            }
        }

        /// <summary>
        /// Gets how many copies of raw data are created in the cluster.
        /// This could be less than the number of cluster peers. The value must 
        /// be greater than 0 and greater than or equal to the search factor. 
        /// This attribute is only valid for the master node in a
        /// cluster configuration. The defaults is 3.
        /// </summary>
        public int ReplicationFactor
        {
            get
            {
                return this.GetInteger("replication_factor");
            }
        }

        /// <summary>
        /// Sets the maximum cumulative time, in seconds, for receiving 
        /// acknowledgement data from peers. On rep_rcv_timeout a source peer 
        /// determines if total receive timeout has exceeded 
        /// rep_max_rcv_timeout. If so, replication fails. The default is 600
        /// seconds.
        /// </summary>
        public int ReplicationMaxReceiveTimeout
        {
            set
            {
                this.SetCacheValue("rep_max_rcv_timeout", value);
            }
        }

        /// <summary>
        /// Sets the maximum time, in seconds, for sending replication slice 
        /// data between cluster nodes. On rep_send_timeout, a source peer 
        /// determines if the total send timeout has exceeded the 
        /// rep_max_send_timeout. If cumulative rep_send_timeout exceeds 
        /// rep_max_send_timeout, replication fails. The default is 600 seconds.
        /// </summary>
        public int ReplicationMaxSendTimeout
        {
            set
            {
                this.SetCacheValue("rep_max_send_timeout", value);
            }
        }

        /// <summary>
        /// Sets the timeout, in seconds, for receiving data between cluster 
        /// nodes. The default is 60 seconds.
        /// </summary>
        public int ReplicationReceiveTimeout
        {
            set
            {
                this.SetCacheValue("rep_rcv_timeout", value);
            }
        }

        /// <summary>
        /// Sets the timeout, in seconds, for sending replication data between 
        /// cluster nodes. The default is 5 seconds.
        /// </summary>
        public int ReplicationSendTimeout
        {
            set
            {
                this.SetCacheValue("rep_send_timeout", value);
            }
        }

        /// <summary>
        /// Sets the TCP port to listen for replicated data from another 
        /// cluster member. If mode=slave is set in the [clustering] stanza, at 
        /// least one replication_port must be configured and not disabled.
        /// </summary>
        public int ReplicationPort
        {
            set
            {
                this.SetCacheValue("replication_port", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether to use SSL when sending replication
        /// data.
        /// </summary>
        public bool ReplicationUseSsl
        {
            set
            {
                this.SetCacheValue("replication_use_ssl", value);
            }
        }

        /// <summary>
        /// Gets or sets the timeout, in seconds, the master waits for a peer to
        /// come back when the peer is restarted (to avoid the overhead of 
        /// trying to fix the buckets that were on the peer). The default is 
        /// 600 seconds. Note: This only applies if the peer is restarted from 
        /// Splunk Web.
        /// </summary>
        public int RestartTimeout
        {
            get
            {
                return this.GetInteger("restart_timeout");
            }

            set
            {
                this.SetCacheValue("restart_timeout", value);
            }
        }

        /// <summary>
        /// Gets or sets the how many searchable copies of each bucket to 
        /// maintain. Must be less than or equal to replication_factor and 
        /// greater than 0. Defaults to 2. This attribute is only valid for the
        /// master node in a cluster configuration.
        /// </summary>
        public int SearchFactor
        {
            get
            {
                return this.GetInteger("search_factor");
            }

            set
            {
                this.SetCacheValue("search_factor", value);
            }
        }

        /// <summary>
        /// Gets or sets the secret that is shared among the nodes in the 
        /// cluster. This mechanism is used to prevent any arbitrary node from 
        /// connecting to the cluster. If a peer or searchhead is not 
        /// configured with the same secret as the master, it is not able to 
        /// communicate with the master. Corresponds to pass4SymmKey setting in 
        /// server.conf.
        /// </summary>
        public string Secret
        {
            get
            {
                return this.GetString("secret");
            }

            set
            {
                this.SetCacheValue("secret", value);
            }
        }

        /// <summary>
        /// Gets or sets the timeout, in seconds, for sending data between 
        /// cluster nodes. The default is 60 seconds.
        /// </summary>
        public int SendTimeout
        {
            get
            {
                return this.GetInteger("send_timeout");
            }

            set
            {
                this.SetCacheValue("send_timeout", value);
            }
        }
    }
}
