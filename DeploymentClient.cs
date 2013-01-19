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
    public class DeploymentClient : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeploymentClient"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        public DeploymentClient(Service service) 
            : base(service, "deployment/client")
        {
        }

        /// <summary>
        /// Sets a value indicating whether to disable the deployment client.
        /// Note: One must restart splunk in order for this to take effect. 
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Gets the list of server classes.
        /// </summary>
        public string[] ServerClasses
        {
            get
            {
                return this.GetStringArray("serverClasses", null);
            }
        }

        /// <summary>
        /// Gets or sets he deployment server's target URI for this deployment 
        /// client. The form of this URI is "deployment_server_uri:port".
        /// </summary>
        public string TargetUri
        {
            get
            {
                return this.GetString("targetUri", null);
            }

            set
            {
                this.SetCacheValue("targetUri", value);
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
                return this.Path + "/deployment-client";
            }

            return base.ActionPath(action);
        }

        /// <summary>
        /// Disables the deployment client. Note: this is unsupported. 
        /// </summary>
        public new void Disable()
        {
            throw new NotSupportedException("Disable unsupported");
        }

        /// <summary>
        /// Enables the deployment client. Note: this is unsupported. 
        /// </summary>
        public new void Enable()
        {
            throw new NotSupportedException("Enable unsupported");
        }

        /// <summary>
        /// Relodes the deployment client. Note: this is unsupported. 
        /// </summary>
        public new void Reload()
        {
            Service.Get(this.Path + "/deployment-client/Reload");
            this.Invalidate();
        }
    }
}
