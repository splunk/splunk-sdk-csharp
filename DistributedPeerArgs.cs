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
    /// Extends Args for Distributed Peer creation setters
    /// </summary>
    public class DistributedPeerArgs : Args
    {
        /// <summary>
        /// Sets the remote password. Note: Remote username and password
        /// need to be set at the same time.
        /// </summary>
        public string RemotePassword
        {
            set
            {
                this["remotePassword"] = value;
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
                this["remoteUsername"] = value;
            }
        }
    }
}