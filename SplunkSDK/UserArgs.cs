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
    /// user creation setters.
    /// </summary>
    public class UserArgs : Args
    {
        /// <summary>
        /// Sets the name of a role to create for the user. After creating the 
        /// role, you can edit that role to specify what access that user 
        /// has to Splunk.
        /// </summary>
        public string CreateRole
        {
            set
            {
                this["createrole"] = value;
            }
        }

        /// <summary>
        /// Sets a default app for this user. The default app specified here 
        /// overrides the default app inherited from the user's roles.
        /// </summary>
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
        /// Sets the password for the user. This parameter is required.
        /// </summary>
        public string Pssword
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
        /// Sets a value that indicates whether to restart background search 
        /// jobs when Splunk restarts. If true, a background search job for 
        /// this user that has not completed is restarted when Splunk restarts.
        /// </summary>
        public bool RestartBackgroundJobs
        {
            set
            {
                this["restart_background_jobs"] = value;
            }
        }

        /// <summary>
        /// Sets the roles to assign this user. At least one role is required.
        /// If this parameter is not set, you must create a role with the 
        /// <see cref="CreateRole"/> parameter.
        /// </summary>
        public string[] Roles
        {
            set
            {
                this["roles"] = value;
            }
        }

        /// <summary>
        /// Sets the time zone display modifier for this user. This parameter is
        /// only available in Splunk 4.3 and later.
        /// </summary>
        public string Timezone
        {
            set
            {
                this["tz"] = value;
            }
        }
    }
}