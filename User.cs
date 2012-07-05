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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The code User class represents a Splunk user who is registered on the
    /// current Splunk server.
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public User(Service service, string path) 
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the user's default app.
        /// </summary>
        public string DefaultApp
        {
            get
            {
                return this.GetString("defaultApp", null);
            }

            set
            {
                this.SetCacheValue("defaultApp", value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the user's default app was set 
        /// specficially by the user.
        /// </summary>
        public bool DefaultAppIsUserOverride
        {
            get
            {
                return this.GetBoolean("defaultAppIsUserOverride");
            }
        }

        /// <summary>
        /// Gets the name of the role that the default app was inherited from, or 
        /// 'system' if it was inherited from the default system setting. 
        /// </summary>
        public string DefaultAppSourceRole
        {
            get
            {
                return this.GetString("defaultAppSourceRole");
            }
        }

        /// <summary>
        /// Gets or sets the user's email address.
        /// </summary>
        public string Email
        {
            get 
            {
                return this.GetString("email", null);
            }

            set
            {
                this.SetCacheValue("email", value);
            }
        }

        /// <summary>
        /// Gets or sets the user's password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.GetString("password", null);
            }

            set 
            {
                this.SetCacheValue("password", value);
            }
        }

        /// <summary>
        /// Gets or sets the the full name associated with this user.
        /// </summary>
        public string RealName
        {
            get
            {
                return this.GetString("realname", null);
            }

            set
            {
                this.SetCacheValue("realname", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether to restart background search jobs when
        /// Splunk restarts. When true, a background search job for this user that has not
        /// completed is restarted when Splunk restarts. 
        /// </summary>
        public bool RestartBackgroundJobs
        {
            set
            {
                this.SetCacheValue("restart_background_jobs", value);
            }
        }

        /// <summary>
        /// Gets or sets the roles assigned to this user, as an array of strings.
        /// </summary>
        public string[] Roles
        {
            get
            {
                return this.GetStringArray("roles", null);
            }

            set
            {
                this.SetCacheValue("roles", value);
            }
        }

        /// <summary>
        /// Gets the authentication method responsible for this user.
        /// </summary>
        public string Type
        {
            get
            {
                return this.GetString("type", null);
            }
        }

        /// <summary>
        /// Gets or sets the time zone to use when displaying dates for this user.
        /// </summary>
        public string Tz
        {
            get
            {
                return this.GetString("tz", null);
            }

            set
            {
                this.SetCacheValue("tz", value);
            }
        }
    }
}
