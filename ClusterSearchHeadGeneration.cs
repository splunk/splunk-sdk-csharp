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
    public class ClusterSearchHeadGeneration : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ClusterSearchHeadGeneration"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public ClusterSearchHeadGeneration(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the current generation ID for this searchhead, which is part 
        /// of a cluster configuration. The search head uses this information to
        /// determine which buckets to search across.
        /// </summary>
        public int GenerationId
        {
            get
            {
                return this.GetInteger("generation_id", -1);
            }
        }

        /// <summary>
        /// Gets a list of peer nodes for the current generation in the cluster 
        /// configuration for this searchhead. The peer information about 
        /// buckets on peers to this master.
        /// wkcfix -- maybe introduce a new type called peers?
        /// </summary>
        public Record Peers
        {
            get
            {
                return (Record)this.Validate().Get("generation_peers");
            }
        }
    }
}