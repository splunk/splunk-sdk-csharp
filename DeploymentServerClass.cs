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
    public class DeploymentServerClass : Entity
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="DeploymentServerClass"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public DeploymentServerClass(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the list of the hosts that are excluded from this server class.
        /// This is a comma-separated list.
        /// </summary>
        public string Blacklist
        {
            get
            {
                return this.GetString("blacklist", null);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether configuration lookups 
        /// continue matching server classes, beyond the first match.
        /// </summary>
        public bool ContinueMatching
        {
            get
            {
                return this.GetBoolean("continueMatching", false);
            }

            set
            {
                this.SetCacheValue("continueMatching", value);
            }
        }

        /// <summary>
        /// Gets or sets the template endpoint URL. This endpoint refers to 
        /// where content can be downloaded by a deployment client. The 
        /// deployment client knows how to substitute the values of the
        /// variables in the URL. Any custom URL can also be supplied here as 
        /// long as it uses the specified variables.
        /// This attribute does not need to be specified unless you have a very 
        /// specific need, for example: to acquire deployment application files 
        /// from a third-party httpd, for extremely large environments.
        /// The default is $deploymentServerUri$/services/streams/deployment?name=$serverClassName$:$appName$
        /// </summary>
        public string Endpoint
        {
            get
            {
                return this.GetString("endpoint", null);
            }

            set
            {
                this.SetCacheValue("endpoint", value);
            }
        }

        /// <summary>
        /// Gets or sets the filter type that is applied first. If set to 
        /// "whitelist", all whitelist filters are applied first, followed by 
        /// blacklist filters. If set to "blacklist", all blacklist filters are
        /// applied first, followed by whitelist filters.
        /// </summary>
        public string FilterType
        {
            get
            {
                return this.GetString("filterType", null);
            }

            set
            {
                this.SetCacheValue("filterType", value);
            }
        }

        /// <summary>
        /// Gets or sets the location on the deployment server to store the 
        /// content that is to be deployed for this server class. 
        /// Note: The path may contain macro expansions or substitutions.
        /// </summary>
        public string RepositoryLocation
        {
            get
            {
                return this.GetString("repositoryLocation", null);
            }

            set
            {
                this.SetCacheValue("repositoryLocation", value);
            }
        }

        /// <summary>
        /// Gets or sets the location on the deployment client where the content 
        /// to be deployed for this server class should be installed.
        /// Note: The path may contain macro expansions or substitutions.
        /// </summary>
        public string TargetRepositoryLocation
        {
            get
            {
                return this.GetString("targetRepositoryLocation", null);
            }

            set
            {
                this.SetCacheValue("targetRepositoryLocation", value);
            }
        }

        /// <summary>
        /// Gets or sets the location of the working folder used by the 
        /// deployment server.
        /// </summary>
        public string TempFolder
        {
            get
            {
                return this.GetString("tmpFolder", null);
            }

            set
            {
                this.SetCacheValue("tmpFolder", value);
            }
        }

        /// <summary>
        /// Returns the exluded server by index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The exluded server, or null if not present.</returns>
        public string GetBlacklistByIndex(int index)
        {
            return this.GetString(string.Format("blacklist.%d", index), null);
        }

        /// <summary>
        /// Returns the included server by index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>The included server, or null if not present.</returns>
        public string GetWhitelistByIndex(int index)
        {
            return this.GetString(string.Format("whitelist.%d", index), null);
        }

        /// <summary>
        /// Returns the exluded server by index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="blacklist">The blacklist server</param>
        public void SetBlacklistByIndex(int index, string blacklist)
        {
            this.SetCacheValue(string.Format("blacklist.%d", index), blacklist);
        }

        /// <summary>
        /// Sets the included server by index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="whitelist">The whitelist server</param>
        public void SetWhitelistByIndex(int index, string whitelist)
        {
            this.SetCacheValue(string.Format("whitelist.%d", index), whitelist);
        }
    }
}