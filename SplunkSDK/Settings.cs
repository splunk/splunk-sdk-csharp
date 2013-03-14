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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The Settings class represents configuration information for an
    /// instance of Splunk.
    /// </summary>
    public class Settings : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        public Settings(Service service)
            : base(service, "server/settings")
        {
        }

        /// <summary>
        /// Gets or sets the fully-qualified path to the directory containing 
        /// the default index for this instance of Splunk.
        /// </summary>
        public string SplunkDB
        {
            get
            {
                return this.GetString("SPLUNK_DB");
            }

            set
            {
                this.SetCacheValue("SPLUNK_DB", value);
            }
        }

        /// <summary>
        /// Gets the fully-qualified path to the Splunk installation directory.
        /// </summary>
        public string SplunkHome
        {
            get
            {
                return this.GetString("SPLUNK_HOME");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether SSL is enabled on the Splunk
        /// management port.
        /// </summary>
        public bool EnableSplunkWebSSL
        {
            get
            {
                return this.GetBoolean("enableSplunkWebSSL");
            }

            set
            {
                this.SetCacheValue("enableSplunkWebSSL", value);
            }
        }

        /// <summary>
        /// Gets or sets the default host name to use for data inputs.
        /// </summary>
        public string Host
        {
            get
            {
                return this.GetString("host", null);
            }

            set
            {
                this.SetCacheValue("host", value);
            }
        }

        /// <summary>
        /// Gets or sets the port on which Splunk Web is listening for this 
        /// instance of Splunk. The port number defaults to 8000. Note
        /// The port must be present for Splunk Web to start. If this 
        /// value is omitted or set to 0, the server will not start an 
        /// HTTP listener.
        /// </summary>
        public int HttpPort
        {
            get
            {
                return this.GetInteger("httpport");
            }

            set
            {
                this.SetCacheValue("httpport", value);
            }
        }

        /// <summary>
        /// Gets or sets the IP address:port number for Splunkd.
        /// </summary>
        public int MgmtPort
        {
            get
            {
                return this.GetInteger("mgmtHostPort");
            }

            set
            {
                this.SetCacheValue("mgmtHostPort", value);
            }
        }

        /// <summary>
        /// Gets or sets the amount of free disk space that is required for 
        /// Splunk to continue searching and indexing.
        /// </summary>
        public int MinFreeSpace
        {
            get
            {
                return this.GetInteger("minFreeSpace");
            }

            set
            {
                this.SetCacheValue("minFreeSpace", value);
            }
        }

        /// <summary>
        /// Gets or sets the string that is prepended to the Splunk symmetric 
        /// key to generate the final key that used to sign all traffic between 
        /// master and slave licensers.
        /// </summary>
        public string Pass4SymmKey
        {
            get
            {
                return this.GetString("pass4SymmKey");
            }

            set
            {
                this.SetCacheValue("pass4SymmKey", value);
            }
        }

        /// <summary>
        /// Gets or sets the name that is used to identify this Splunk instance 
        /// for features such as distributed search.
        /// </summary>
        public string ServerName
        {
            get
            {
                return this.GetString("serverName");
            }

            set
            {
                this.SetCacheValue("serverName", value);
            }
        }

        /// <summary>
        /// Gets or sets the amount of time before a user session times out.
        /// A valid format is <i>number</i> followed by a time unit ("s", "h",
        /// or "d").
        /// </summary>
        public string SessionTimeout
        {
            get
            {
                return this.GetString("sessionTimeout");
            }

            set
            {
                this.SetCacheValue("sessionTimeout", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the instance is configured 
        /// to start Splunk Web.
        /// </summary>
        public bool StartWebServer
        {
            get
            {
                return this.GetBoolean("startwebserver");
            }

            set
            {
                this.SetCacheValue("startwebserver", value);
            }
        }

        /// <summary>
        /// Gets or sets the IP address of the authenticating proxy. 
        /// </summary>
        public string TrustedIP
        {
            get
            {
                return this.GetString("trustedIP", null);
            }

            set
            {
                this.SetCacheValue("trustedIP", value);
            }
        }

        /// <summary>
        /// Updates the settings with the values previously set using the setter
        /// methods, and any additional specified arguments. The specified
        /// arguments take precedent over the values that were set using the 
        /// setter methods. The post message is sent to a non-normal endpoint.
        /// </summary>
        /// <param name="args">The key/value pairs to update</param>
        public override void Update(Dictionary<string, object> args)
        {
            // Merge cached setters and live args together before updating; live
            // args get precedence over the cached setter args.
            Dictionary<string, object> mergedArgs = 
                new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> element in args)
            {
                mergedArgs.Add(element.Key, element.Value);
            }
            foreach (KeyValuePair<string, object> element in this.toUpdate)
            {
                if (!mergedArgs.ContainsKey(element.Key))
                {
                    mergedArgs.Add(element.Key, element.Value);
                }
            }
            Service.Post(this.Path + "/settings", mergedArgs);
            toUpdate.Clear();
            this.Invalidate();
        }

        /// <summary>
        /// Updates the entity with the accumulated arguments, established by 
        /// this class. The post message is sent to a non-normal endpoint.
        /// </summary>
        public override void Update()
        {
            Service.Post(this.Path + "/settings", this.toUpdate);
            this.Invalidate();
        }
    }
}
