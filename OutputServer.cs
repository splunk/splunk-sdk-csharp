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
    using System.Collections.Generic;

    /// <summary>
    /// The OutputServer class represents a collection of the Output Servers. 
    /// </summary>
    public class OutputServer : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputServer"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public OutputServer(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the DNS name of the desination server.
        /// </summary>
        public string DestinationHost
        {
            get
            {
                return this.GetString("destHost", null);
            }
        }

        /// <summary>
        /// Gets the IP address of the destination server.
        /// </summary>
        public string DestinationIP
        {
            get
            {
                return this.GetString("destIP", null);
            }
        }

        /// <summary>
        /// Gets the port on which the destination server is listening.
        /// </summary>
        public int DestinationPort
        {
            get
            {
                return this.GetInteger("destPort", 0);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the outputs to the 
        /// destination server is disabled.
        /// </summary>
        public bool Disabled
        {
            get
            {
                return this.GetBoolean("disabled", false);
            }

            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Gets or sets the data distribution method used when two or more 
        /// servers exist in the same forwarder group. Valid values are:
        /// clone, balance, or autobalance.
        /// </summary>
        public string Method
        {
            get
            {
                return this.GetString("method", null);
            }

            set
            {
                this.SetCacheValue("method", value);
            }
        }

        /// <summary>
        /// Gets the port on the destination server where data is forwarded.
        /// </summary>
        public int SourcePort
        {
            get
            {
                return this.GetInteger("sourcePort", 0);
            }
        }

        /// <summary>
        /// Sets the alternate name to match in the remote server's SSL 
        /// certificate.
        /// </summary>
        public string SslAltNameToCheck
        {
            set
            {
                this.SetCacheValue("sslAltNameToCheck", value);
            }
        }

        /// <summary>
        /// Sets the path to the client certificate. If specified, connection 
        /// uses SSL.
        /// </summary>
        public string SslCertPath
        {
            set
            {
                this.SetCacheValue("sslCertPath", value);
            }
        }

        /// <summary>
        /// Sets the SSL Cipher in the form 
        /// ALL:!aNULL:!eNULL:!LOW:!EXP:RC4+RSA:+HIGH:+MEDIUM
        /// </summary>
        public string SslCipher
        {
            set
            {
                this.SetCacheValue("sslCipher", value);
            }
        }

        /// <summary>
        /// Sets the value to check the common name of the server's certificate. 
        /// If there is no match, assume that Splunk is not authenticated 
        /// against this server. You must specify this setting if 
        /// SslVerifyServerCert is true.
        /// </summary>
        public string SslCommonNameToCheck
        {
            set
            {
                this.SetCacheValue("sslCommonNameToCheck", value);
            }
        }

        /// <summary>
        /// Sets The password associated with the CAcert.
        /// The default Splunk CAcert uses the password "password."
        /// </summary>
        public string SslPassword
        {
            set
            {
                this.SetCacheValue("sslPassword", value);
            }
        }

        /// <summary>
        /// Sets The path to the root certificate authority file.
        /// </summary>
        public string SslRootCAPath
        {
            set
            {
                this.SetCacheValue("sslRootCAPath", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether the server you are connecting to is 
        /// a valid one (authenticated). Both the common name and the alternate
        /// name of the server are then checked for a match.
        /// </summary>
        public bool SslVerifyServerCert
        {
            set
            {
                this.SetCacheValue("sslVerifyServerCert", value);
            }
        }
    }
}
