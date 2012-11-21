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
    /// Extends Args for Application creation setters
    /// </summary>
    public class OutputServerArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether the forwarder is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the data distribution method used when two or more servers 
        /// exist in the same forwarder group. Valid values are: clone, or 
        /// balance, or autobalance
        /// </summary>
        public string Method
        {
            set
            {
                this["method"] = value;
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
                this["sslAltNameToCheck"] = value;
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
                this["sslCertPath"] = value;
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
                this["sslCipher"] = value;
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
                this["sslCommonNameToCheck"] = value;
            }
        }

        /// <summary>
        /// Sets the password associated with the CAcert. The default Splunk 
        /// CAcert uses the password "password."
        /// </summary>
        public string SslPassword
        {
            set
            {
                this["sslPassword"] = value;
            }
        }

        /// <summary>
        /// Sets the path to the root certificate authority file.
        /// </summary>
        public string SslRootCAPath
        {
            set
            {
                this["sslRootCAPath"] = value;
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
                this["sslVerifyServerCert"] = value;
            }
        }
    }
}