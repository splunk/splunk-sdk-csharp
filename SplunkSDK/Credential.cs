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
    using System;

    /// <summary>
    /// The Password class represents a credential.
    /// </summary>
    public class Credential : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Credential"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public Credential(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the plain text version of password.
        /// </summary>
        public string ClearPassword
        {
            get
            {
                return this.GetString("clear_password", null);
            }
        }

        /// <summary>
        /// Gets the encrypted password, as it is stored. This is always 
        /// stored as "********".
        /// </summary>
        public string EncryptedPassword
        {
            get
            {
                return this.GetString("encr_password", null);
            }
        }

        /// <summary>
        /// Gets the username, which we overload name, when building the 
        /// collection.
        /// </summary>
        public override string Name
        {
            get
            {
                return this.Username;
            }
        }

        /// <summary>
        /// Gets or sets the plain text version of password.
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
        /// Gets or sets the realm in which the credentials are valid.
        /// </summary>
        public string Realm
        {
            get
            {
                return this.GetString("realm", null);
            }

            set
            {
                this.SetCacheValue("realm", value);
            }
        }

        /// <summary>
        /// Gets the Splunk username for the credentials.
        /// </summary>
        public string Username
        {
            get
            {
                return this.GetString("username", null);
            }
        }
    }
}
