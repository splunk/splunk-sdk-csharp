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
    /// The <see cref="BaseService"/> class contains functionality common to 
    /// Splunk Enterprise and Splunk Storm. This class is an implementation 
    /// detail and should not be extended outside of the SDK.
    /// </summary>
    public abstract class BaseService : HttpService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        /// <param name="host">The hostname.</param>
        public BaseService(string host)
            : base(host)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class,
        /// with host and port.
        /// </summary>
        /// <param name="host">The hostname.</param>
        /// <param name="port">The port.</param>
        public BaseService(string host, int port)
            : base(host, port)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class,
        /// with host, port, and scheme.
        /// </summary>
        /// <param name="host">The hostname.</param>
        /// <param name="port">The port.</param>
        /// <param name="scheme">The scheme, http or https.</param>
        public BaseService(string host, int port, string scheme)
            : base(host, port, scheme)
        {
        }
    }
}
