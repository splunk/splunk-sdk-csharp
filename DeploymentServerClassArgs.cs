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
    /// Extends Args for Application creation setters
    /// </summary>
    public class DeploymentServerClassArgs : Args
    {
        /// <summary>
        /// Sets the blacklist hosts for this serverclass. This is a comma
        /// separated list; the maximum entries is 10.
        /// </summary>
        public string Blacklist
        {
            set
            {
                // Take string build into specific blacklist by index enteries. 
                string[] list = value.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    this[string.Format("blacklist.%d", i)] = list[i];
                }
            }
        }

        /// <summary>
        /// Sets a value indicating whether configuration lookups 
        /// continue matching server classes, beyond the first match.
        /// </summary>
        public bool ContinueMatching
        {
            set
            {
                this["continueMatching"] = value;
            }
        }

        /// <summary>
        /// Sets the URL template string which specifies the endpoint from 
        /// which content can be downloaded by a deployment client. The 
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
            set
            {
                this["endpoint"] = value;
            }
        }

        /// <summary>
        /// Sets the filter type that is applied first. If set to 
        /// "whitelist", all whitelist filters are applied first, followed by 
        /// blacklist filters. If set to "blacklist", all blacklist filters are
        /// applied first, followed by whitelist filters.
        /// </summary>
        public string FilterType
        {
            set
            {
                this["filterType"] = value;
            }
        }

        /// <summary>
        /// Sets the location on the deployment server to store the 
        /// content that is to be deployed for this server class. 
        /// Note: The path may contain macro expansions or substitutions.
        /// </summary>
        public string RepositoryLocation
        {
            set
            {
                this["repositoryLocation"] = value;
            }
        }

        /// <summary>
        /// Sets the location on the deployment client where the content 
        /// to be deployed for this server class should be installed.
        /// Note: The path may contain macro expansions or substitutions.
        /// </summary>
        public string TargetRepositoryLocation
        {
            set
            {
                this["targetRepositoryLocation"] = value;
            }
        }

        /// <summary>
        /// Sets the location of the working folder used by the 
        /// deployment server.
        /// </summary>
        public string TempFolder
        {
            set
            {
                this["tmpFolder"] = value;
            }
        }

        /// <summary>
        /// Sets the whitelist hosts for this serverclass. This is a comma
        /// separated list; the maximum entries is 10.
        /// </summary>
        public string Whitelist
        {
            set
            {
                // Take string build into specific whitelist by index enteries. 
                string[] list = value.Split(',');
                for (int i = 0; i < list.Length; i++)
                {
                    this[string.Format("whitelist.%d", i)] = list[i];
                }
            }
        }
    }
}