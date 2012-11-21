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
    /// The DistributedPeers class represents a Splunk distributed peer,
    /// providing distributed peer server management.
    /// </summary>
    public class DistributedPeer : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DistributedPeer"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public DistributedPeer(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets this peer's build number.
        /// </summary>
        public string Build
        {
            get
            {
                return this.GetString("build", null);
            }
        }

        /// <summary>
        /// Gets a list of bundle versions.
        /// </summary>
        public string[] BundleVersions
        {
            get
            {
                return this.GetStringArray("bundle_versions", null);
            }
        }

        /// <summary>
        /// Gets the GUID, or null if not specified.
        /// </summary>
        public string Guid
        {
            get
            {
                return this.GetString("guid", null);
            }
        }

        /// <summary>
        /// Gets this peer's license signature.
        /// </summary>
        public string LicenseSignature
        {
            get
            {
                return this.GetString("licenseSignature", null);
            }
        }

        /// <summary>
        /// Gets this peer's name.
        /// </summary>
        public string PeerName
        {
            get
            {
                return this.GetString("peerName", null);
            }
        }

        /// <summary>
        /// Gets this peer's type.
        /// </summary>
        public string PeerType
        {
            get
            {
                return this.GetString("peerType", null);
            }
        }

        /// <summary>
        /// Gets this peer's replication status.
        /// </summary>
        public string ReplicationStatus
        {
            get
            {
                return this.GetString("replicationStatus", null);
            }
        }

        /// <summary>
        /// Gets this peer's status.
        /// </summary>
        public string Status
        {
            get
            {
                return this.GetString("status", null);
            }
        }

        /// <summary>
        /// Gets this peer's version.
        /// </summary>
        public string Version
        {
            get
            {
                return this.GetString("version", null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this peer is using HTTPS.
        /// </summary>
        public bool IsHttps
        {
            get
            {
                return this.GetBoolean("is_https", false);
            }
        }

        /// <summary>
        /// Sets the remote password. Note: Remote username and password
        /// need to be set at the same time.
        /// </summary>
        public string RemotePassword
        {
            set
            {
                this.SetCacheValue("remotePassword", value);
            }
        }

        /// <summary>
        /// Sets the remote username. Note: Remote username and password
        /// need to be set at the same time.
        /// </summary>
        public string RemoteUsername
        {
            set
            {
                this.SetCacheValue("remoteUsername", value);
            }
        }
    }
}