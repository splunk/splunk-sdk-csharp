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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserCollection : EntityCollection<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserCollection"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        public UserCollection(Service service)
            : base(service, "authentication/users", typeof(User)) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCollection"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="args">The args</param>
        public UserCollection(Service service, Args args)
            : base(service, "authentication/users", args, typeof(User))
        {
        }

        /// <summary>
        /// Creates a new user entity from a username, password, and role.
        /// Usernames must be unique on the system, and are used by the user to log
        /// in to Splunk.
        /// </summary>
        /// <param name="name">The username for the new user</param>
        /// <param name="password">The asswird fir this new user</param>
        /// <param name="role">A roles to assign this new user</param>
        /// <returns>The new user entity</returns>
        public User Create(string name, string password, string role)
        {
            return this.Create(name, password, role, null);
        }

        /// <summary>
        /// Creates a new user entity from a username, password, and role.
        /// Usernames must be unique on the system, and are used by the user to log
        /// in to Splunk.
        /// </summary>
        /// <param name="name">The username for the new user</param>
        /// <param name="password">The asswird fir this new user</param>
        /// <param name="roles">A list of roles to assign this new user</param>
        /// <returns>The new user entity</returns>
        public User Create(string name, string password, string[] roles)
        {
            return this.Create(name, password, roles, null);
        }

        /// <summary>
        /// Creates a new user entity from a username, password, and role.
        /// Usernames must be unique on the system, and are used by the user to log
        /// in to Splunk.
        /// </summary>
        /// <param name="name">The username for the new user</param>
        /// <param name="password">The asswird fir this new user</param>
        /// <param name="role">A role to assign this new user</param>
        /// <param name="args">The optional args</param>
        /// <returns>The new user entity</returns>
        public User Create(string name, string password, string role, Args args)
        {
            args = Args.Create(args);
            args.Add("password", password);
            args.Add("roles", role);
            return this.Create(name.ToLower(), args);
        }

        /// <summary>
        /// Creates a new user entity from a username, password, and role.
        /// Usernames must be unique on the system, and are used by the user to log
        /// in to Splunk.
        /// </summary>
        /// <param name="name">The username for the new user</param>
        /// <param name="password">The asswird fir this new user</param>
        /// <param name="roles">A list of roles to assign this new user</param>
        /// <param name="args">The optional args</param>
        /// <returns>The new user entity</returns>
        public User
        Create(string name, string password, string[] roles, Args args)
        {
            args = Args.Create(args);
            args.Add("password", password);
            args.Add("roles", roles);
            return this.Create(name.ToLower(), args);
        }
    }
}
