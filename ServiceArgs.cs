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
    /// The Service Arguments data structure that defines the context
    /// of the Splunk service.
    /// </summary>
    public class ServiceArgs
    {
        /// <summary>
        ///  Gets or sets the application context of the service.
        /// </summary>
        public string App 
        { 
            get; set; 
        }

        /// <summary>
        ///  Gets or sets the host name of the service
        /// </summary>
        public string Host 
        { 
            get; set; 
        }

        /// <summary>
        ///  Gets or sets the owner context of the service
        /// </summary>
        public string Owner 
        { 
            get; set; 
        }

        /// <summary>
        ///  Gets or sets the port number of the service
        /// </summary>
        public int Port 
        { 
            get; set; 
        }

        /// <summary>
        ///  Gets or sets the scheme to use for accessing the service
        /// </summary>
        public string Scheme 
        { 
            get; set; 
        }

        /// <summary>
        ///  Gets or sets the Splunk authentication token to use for the session
        /// </summary>
        public string Token 
        { 
            get; set; 
        }
    }
}
