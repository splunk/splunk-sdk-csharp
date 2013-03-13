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
    /// Represents the collection of Credentials.
    /// </summary>
    public class CredentialCollection : EntityCollection<Credential>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        public CredentialCollection(Service service)
            : base(service, service.PasswordEndPoint, typeof(Credential))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="args">The arguments</param>
        public CredentialCollection(Service service, Args args)
            : base(service, "service.PasswordEndPoint", args, typeof(Credential))
        {
        }

        /// <summary>
        /// Creates a credential.
        /// </summary>
        /// <param name="name">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The Password credentials</returns>
        public Credential Create(string name, string password)
        {
            return this.Create(name, password, null);
        }

        /// <summary>
        /// Creates a credential.
        /// </summary>
        /// <param name="name">The username</param>
        /// <param name="password">The password</param>
        /// <param name="realm">The realm</param>
        /// <returns>The Password credentials</returns>
        public Credential Create(string name, string password, string realm)
        {
            Args args = new Args();
            args.Add("password", password);
            args.Add("realm", realm);
            return base.Create(name, args);
        }
    }
}
