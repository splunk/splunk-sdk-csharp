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
    /// The ClusterMasterBuckets class represents a collection of the Cluster 
    /// Masters Buckets. This is introduced in Splunk 5.0.
    /// </summary>
    public class ClusterMasterBuckets : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ClusterMasterBuckets"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterMasterBuckets(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the size, in bytes, of the bucket.
        /// </summary>
        public int BucketSize
        {
            get
            {
                return this.GetInteger("bucket_size", -1);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the bucket is frozen.
        /// </summary>
        public bool Frozen
        {
            get
            {
                return this.GetBoolean("frozen", false);
            }
        }

        /// <summary>
        /// Gets the peer information about buckets on peers to this master.
        /// wkcfix -- maybe introduce a new type called peers?
        /// </summary>
        public Record Peers
        {
            get
            {
                return (Record)this.Validate().Get("peers");
            }
        }

        /// <summary>
        /// Gets the value of deferred service until after this time. (time?)
        /// </summary>
        public string ServiceAfterTime
        {
            get
            {
                return this.GetString("service_after_time", null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the bucket was created on the peer 
        /// before the peer entered into a cluster configuration with this 
        /// master.
        /// </summary>
        public bool Standalone
        {
            get
            {
                return this.GetBoolean("standalone", false);
            }
        }
    }
}
