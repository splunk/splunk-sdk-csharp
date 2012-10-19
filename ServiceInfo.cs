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
    /// The Service Info class. This class contains the basic service
    /// information.
    /// </summary>
    public class ServiceInfo : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInfo"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        public ServiceInfo(Service service) : base(service, "server/info")
        {
        }

        /// <summary>
        /// Gets the Splunk build number.
        /// </summary>
        public int Build 
        {
            get 
            {
            return this.GetInteger("build");
            }
        }

        /// <summary>
        /// Gets the CPU architecture.
        /// </summary>
        public string CpuArch 
        {
            get
            {
                return this.GetString("cpu_arch");
            }
        }

        /// <summary>
        /// Gets the GUID identifying this Splunk instance.
        /// </summary>
        public string Guid
        {
            get
            {
                return this.GetString("guid");
            }
        }

        /// <summary>
        /// Gets the array of license labels.
        /// </summary>
        public string[] LicenseLabels 
        {
            get
            {
                return this.GetStringArray("license_labels", null);
            }
        }
        
        /// <summary>
        /// Gets the array of the license keys for this Splunk instance.
        /// </summary>
        public string[] LicenseKeys 
        {
            get
            {
                return this.GetStringArray("licenseKeys", null);
            }
        }

        /// <summary>
        /// Gets the license signature for this Splunk instance.
        /// </summary>
        public string LicenseSignature 
        {
            get
            {
                return this.GetString("licenseSignature");
            }
        }

        /// <summary>
        /// Gets the current license state of this Splunk instance.
        /// </summary>
        public string LicenseState 
        {
            get
            {
                return this.GetString("licenseState");
            }
        }

        /// <summary>
        /// Gets a GUID identifying the license master for this Splunk instance.
        /// </summary>
        public string MasterGuid 
        {
            get
            {
                return this.GetString("master_guid");
            }
        }

        /// <summary>
        /// Gets the current mode of this Splunk instance.
        /// </summary>
        public string Mode 
        {
            get
            {
                return this.GetString("mode");
            }
        }

        /// <summary>
        /// Gets the OS build of this Splunk instance.
        /// </summary>
        public string OsBuild 
        {
            get
            {
                return this.GetString("os_build");
            }
        }
        
        /// <summary>
        /// Gets the service's OS name (type).
        /// </summary>
        public string OsName 
        {
            get
            {
                return this.GetString("os_name");
            }
        }

        /// <summary>
        /// Gets the OS version of this Splunk instance.
        /// </summary>
        public string OsVersion 
        {
            get
            {
                return this.GetString("os_version");
            }
        }

        /// <summary>
        /// Gets the server name of this Splunk instance.
        /// </summary>
        public string ServerName 
        {
            get
            {
                return this.GetString("serverName");
            }
        }

        /// <summary>
        /// Gets the version number of this Splunk instance.
        /// </summary>
        public string Version 
        {
            get
            {
                return this.GetString("version");
            }
        }

        /// <summary>
        ///  Gets a value indicating whether this Splunk instance is running 
        ///  under a free license.
        /// </summary>
        public bool IsFree 
        {
            get
            {
                return this.GetBoolean("isFree");
            }
        }

        /// <summary>
        /// Gets a value indicating whether real-time search is enabled for the
        /// service.
        /// </summary>
        public bool IsRtSearchEnabled 
        {
            get
            {
                return this.GetBoolean("rtsearch_enabled", false);
            }
        }

        /// <summary>
        /// Gets a value indicating whether this Splunk instance is running 
        /// under a trial license.
        /// </summary>
        public bool IsTrial 
        {
            get
            {
                return this.GetBoolean("isTrial");
            }
        }
    }
}
