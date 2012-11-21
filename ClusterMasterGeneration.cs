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
    /// The ClusterMasterGeneration class represents a collection of the Cluster 
    /// Masters Generation. This is introduced in Splunk 5.0.
    /// </summary>
    public class ClusterMasterGeneration : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ClusterMasterGeneration"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterMasterGeneration(Service service, string path)
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
        /// Gets ID of the current generation for this master.
        /// </summary>
        public string GenerationId
        {
            get
            {
                return this.GetString("generation_id", null);
            }
        }

        /// <summary>
        /// Gets the peers for this generation of the cluster.
        /// wkcfix -- maybe introduce a new type called peers?
        /// </summary>
        public Record GenerationPeers
        {
            get
            {
                return (Record)this.Validate().Get("generation_peers");
            }
        }

        /// <summary>
        /// Gets the next generation ID used by the master when committing a 
        /// new generation.
        /// </summary>
        public int PendingGenerationId
        {
            get
            {
                return this.GetInteger("pending_generation_id", -1);
            }
        }

        /// <summary>
        /// Gets the timestamp of the last attempt to commit to the pending 
        /// generation ID (if ever).
        /// </summary>
        public DateTime PendingLastAttempt
        {
            get
            {
                return this.GetDate("pending_last_attempt", DateTime.MinValue);
            }
        }

        /// <summary>
        /// Gets the reason why this peer failed to commit to the pending 
        /// generation. This is NULL if no such attempt was made.
        /// </summary>
        public string PendingLastReason
        {
            get
            {
                return this.GetString("pending_last_reason", null);
            }
        }
    }
}
