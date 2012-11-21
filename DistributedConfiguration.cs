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
    /// The DeploymentClient class represents a Splunk deployment client,
    /// providing access to deployment client configuration and status.
    /// </summary>
    public class DistributedConfiguration : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DistributedConfiguration"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        public DistributedConfiguration(Service service)
            : base(service, "search/distributed/config")
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether Splunk automatically adds
        /// all discovered servers.
        /// </summary>
        public bool AutoAddServers
        {
            get
            {
                return this.GetBoolean("autoAddServers", false);
            }

            set
            {
                this.SetCacheValue("autoAddServers", value);
            }
        }

        /// <summary>
        /// Gets or sets a comma-separted list of server names that are excluded
        /// from being peers.
        /// </summary>
        public string BlacklistNames
        {
            get
            {
                return this.GetString("blacklistNames", null);
            }

            set
            {
                this.SetCacheValue("blacklistNames", value);
            }
        }

        /// <summary>
        /// Gets or sets a comma-separated list of server URLs that are excluded
        /// from being peers. The format is "ip:port"
        /// </summary>
        public string BlacklistUrls
        {
            get
            {
                return this.GetString("blacklistURLs", null);
            }

            set
            {
                this.SetCacheValue("blacklistURLs", value);
            }
        }

        /// <summary>
        /// Gets or sets the time-out period for connecting to search peers, in
        /// seconds.
        /// </summary>
        public int ConnectionTimeout
        {
            get
            {
                return this.GetInteger("connectionTimeout", -1);
            }

            set
            {
                this.SetCacheValue("connectionTimeout", value);
            }
        }

        /// <summary>
        /// Gets or sets he frequency, in seconds, at which servers that have 
        /// timed out are rechecked. If RemoveTimedOutServers is false, this 
        /// setting has no effect.
        /// </summary>
        public int CheckTimedOutServersFrequency
        {
            get
            {
                return this.GetInteger("checkTimedOutServersFrequency", -1);
            }

            set
            {
                this.SetCacheValue("checkTimedOutServersFrequency", value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether distributed search is enabled.
        /// </summary>
        public bool DistributedSearchEnabled
        {
            get
            {
                return this.GetBoolean("dist_search_enabled", false);
            }
        }

        /// <summary>
        /// Gets or sets the heartbeat frequency between peers, in seconds.
        /// </summary>
        public int HeartbeatFrequency
        {
            get
            {
                return this.GetInteger("heartbeatFrequency", -1);
            }

            set
            {
                this.SetCacheValue("heartbeatFrequency", value);
            }
        }

        /// <summary>
        /// Gets or sets the multicast address where each Splunk server sends 
        /// and listens for heartbeat messages. The default address is 
        /// "224.0.0.37".
        /// </summary>
        public string HeartbeatMcastAddr
        {
            get
            {
                return this.GetString("heartbeatMcastAddr", null);
            }

            set
            {
                this.SetCacheValue("heartbeatMcastAddr", value);
            }
        }

        /// <summary>
        /// Gets or sets the port where each Splunk server sends and listens for
        /// heartbeat messages.
        /// </summary>
        public int HeartbeatPort
        {
            get
            {
                return this.GetInteger("heartbeatPort", -1);
            }

            set
            {
                this.SetCacheValue("heartbeatPort", value);
            }
        }

        /// <summary>
        /// Gets or sets the time-out period for reading and receiving data from
        /// a search peer.
        /// </summary>
        public int ReceiveTimeout
        {
            get
            {
                return this.GetInteger("receiveTimeout", -1);
            }

            set
            {
                this.SetCacheValue("receiveTimeout", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the timed-out servers are 
        /// removed from the distributed configuration.
        /// </summary>
        public bool RemovedTimedOutServers
        {
            get
            {
                return this.GetBoolean("removedTimedOutServers", false);
            }

            set
            {
                this.SetCacheValue("removedTimedOutServers", value);
            }
        }

        /// <summary>
        /// Gets or sets the time-out period for trying to write or send data
        /// to a search peer.
        /// </summary>
        public int SendTimeout
        {
            get
            {
                return this.GetInteger("sendTimeout", -1);
            }

            set
            {
                this.SetCacheValue("sendTimeout", value);
            }
        }

        /// <summary>
        /// Gets or sets the peer server list. You don't need to set servers 
        /// here if you are operating completely in "autoAddServers" mode
        /// because discovered servers are automatically added.
        /// </summary>
        public string Servers
        {
            get
            {
                return this.GetString("servers", null);
            }

            set
            {
                this.SetCacheValue("servers", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this server uses bundle 
        /// replication to share search-time configuration with search peers. 
        /// Note: If set to false, the search head assumes that the
        /// search peers can access the correct bundles using an shared storage.
        /// </summary>
        public bool ShareBundles
        {
            get
            {
                return this.GetBoolean("shareBundles", false);
            }

            set
            {
                this.SetCacheValue("shareBundles", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this server participates in 
        /// a search or call. If set to true, this server is skipped and does 
        /// not participate. This setting is used for building a node that only 
        /// merges the results from other servers.
        /// </summary>
        public bool SkipOurselves
        {
            get
            {
                return this.GetBoolean("skipOurselves", false);
            }

            set
            {
                this.SetCacheValue("skipOurselves", value);
            }
        }

        /// <summary>
        /// Gets or sets the time-out period, in seconds, for connecting to a 
        /// search peer for getting its basic info.
        /// </summary>
        public int StatusTimeout
        {
            get
            {
                return this.GetInteger("statusTimeout", -1);
            }

            set
            {
                this.SetCacheValue("statusTimeout", value);
            }
        }

        /// <summary>
        /// Gets or sets the time-to-live (ttl) of heartbeat messages.
        /// </summary>
        public int Ttl
        {
            get
            {
                return this.GetInteger("ttl", -1);
            }

            set
            {
                this.SetCacheValue("ttl", value);
            }
        }

        /// <summary>
        /// Returns the action path. Only edit is overriden, all others are 
        /// generated through the base Entity class.
        /// </summary>
        /// <param name="action">The action requested</param>
        /// <returns>The action path</returns>
        public new string ActionPath(string action)
        {
            if (action.Equals("edit"))
            {
                return this.Path + "/distributedSearch";
            }

            return base.ActionPath(action);
        }
    }
}
