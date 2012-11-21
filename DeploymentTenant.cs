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
    /// The DeploymentServer class represents a Splunk deployment client,
    /// providing access to the configurations of all deployment servers.
    /// </summary>
    public class DeploymentTenant : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentTenant"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public DeploymentTenant(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Sets a value indicating whether to disable the deployment server.
        /// Note: One must restart splunk in order for this to take effect. 
        /// However, to avoid restarting Splunk, you can use the 
        /// Entity.Disable() and Entity.Enable() methods instead, which 
        /// take effect immediately. 
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Returns the inclusive criteria for determining deployment client 
        /// access to this deployment server. If the whitelist index does not
        /// exist, a null is returned.
        /// </summary>
        /// <param name="index">The whitellist index</param>
        /// <returns>The whitelisted server</returns>
        public string GetWhitelistByIndex(int index)
        {
            return this.GetString(string.Format("whitelist.%d", index), null);
        }
    }
}
