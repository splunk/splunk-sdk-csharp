/*
 * Copyright 2013 Splunk, Inc.
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
    /// The <see cref="UserArgs"/> class extends <see cref="Args"/> for 
    /// <see cref="User"/> creation setters.
    /// </summary>
    public class UserArgs : Args
    {
        /// <summary>
        /// Sets the name of a role to create for the user. 
        /// </summary>
    	/// <remarks>
		/// After creating the role, you can edit that role to specify what
		/// access that user has to Splunk.
		/// </remarks>
        public string CreateRole
        {
            set
            {
                this["createrole"] = value;
            }
        }

        /// <summary>
        /// Sets a default app for this user. 
        /// </summary>
		/// <remarks>
		/// The default app specified here overrides the default app inherited 
		/// from the user's roles.
		/// </remarks>
        public string DefaultApp
        {
            set
            {
                this["defaultApp"] = value;
            }
        }

        /// <summary>
        /// Sets the email address for the user.
        /// </summary>
        public string Email
        {
            set
            {
                this["email"] = value;
            }
        }

        /// <summary>
        /// Required. Sets the password for the user. 
        /// </summary>
        public string Password
        {
            set
            {
                this["password"] = value;
            }
        }
        
        /// <summary>
        /// Sets the realname for the user.
        /// </summary>
        public string RealName
        {
            set
            {
                this["realname"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to restart background search 
        /// jobs when Splunk restarts. 
        /// </summary>
		/// <remarks>
		/// If set to true, a background search job for this user that has not
		/// completed is restarted when Splunk restarts.
		/// </remarks>
        public bool RestartBackgroundJobs
        {
            set
            {
                this["restart_background_jobs"] = value;
            }
        }

        /// <summary>
        /// Sets the roles to assign this user. 
        /// </summary>
		/// <remarks>
		/// At least one role is required.
        /// If this parameter is not set, you must create a role with the 
        /// <see cref="CreateRole"/> parameter.
		/// </remarks>
        public string[] Roles
        {
            set
            {
                this["roles"] = value;
            }
        }

        /// <summary>
        /// Sets the time zone display modifier for this user. 
        /// </summary>
		/// <remarks>
		/// This parameter is available starting in Splunk 4.3.
		/// </remarks>
        public string Timezone
        {
            set
            {
                this["tz"] = value;
            }
        }
    }
}
