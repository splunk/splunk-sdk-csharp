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
    /// This represents the Application class
    /// </summary>
    public class Application : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Application"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public Application(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the name of the app's author. For Splunkbase
        /// apps, this value is the username of the Splunk.com account. For internal
        /// apps, this value is the full name.
        /// </summary>
        public string Author
        {
            get
            {
                return this.GetString("author", null);
            }

            set
            {
                this.SetCacheValue("author", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Splunk checks Splunkbase for updates.
        /// </summary>
        public bool CheckForUpdates
        {
            get
            {
                return this.GetBoolean("check_for_updates", false);
            }

            set
            {
                this.SetCacheValue("check_for_updates", value);
            }
        }

        /// <summary>
        /// Gets or sets the short description of the app.
        /// </summary>
        public string Description
        {
            get
            {
                return this.GetString("description", null);
            }

            set
            {
                this.SetCacheValue("description", value);
            }
        }

        /// <summary>
        /// Gets or sets the app's label (its name)
        /// </summary>
        /// <returns></returns>
        public string Label
        {
            get
            {
                return this.GetString("label", null);
            }

            set
            {
                this.SetCacheValue("label", value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether to reload objects contained in the locally-installed app.
        /// </summary>
        public bool Refreshes
        {
            get
            {
                return this.GetBoolean("refresh", false);
            }
        }

        /// <summary>
        /// Gets or sets the version of the app.
        /// </summary>
        public string Version
        {
            get
            {
                return this.GetString("version", null);
            }

            set
            {
                this.SetCacheValue("version", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the app's custom setup has been
        /// performed. This field is available in Splunk version 4.2.4 and later.
        /// </summary>
        public bool IsConfigured
        {
            get
            {
                return this.GetBoolean("configured", false);
            }

            set
            {
                this.SetCacheValue("configured", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the app can be managed by Splunk Manager.
        /// </summary>
        public bool IsManageable
        {
            get
            {
                return this.GetBoolean("manageable", false);
            }

            set
            {
                this.SetCacheValue("manageable", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the app is visible and navigable from Splunk Web.
        /// </summary>
        public bool IsVisible
        {
            get
            {
                return this.GetBoolean("visible", false);
            }

            set
            {
                this.SetCacheValue("visible", value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether a state change requires the app to be restarted.
        /// </summary>
        public bool StateChangeRequiresRestart
        {
            get
            {
                return this.GetBoolean("state_change_requires_restart", false);
            }
        }

        /// <summary>
        /// Returns any update information that is available for the app.
        /// </summary>
        /// <returns>The update information</returns>
        public ApplicationUpdate AppUpdate()
        {
            return new ApplicationUpdate(this.Service, this.Path);
        }

        /// <summary>
        /// Archives the app on the server file system. 
        /// </summary>
        /// <returns>The archive information</returns>
        public ApplicationArchive Archive()
        {
            return new ApplicationArchive(this.Service, this.Path);
        }

        /// <summary>
        /// Returns the app's setup information.
        /// </summary>
        /// <returns>The setup information</returns>
        public ApplicationSetup Setup()
        {
            return new ApplicationSetup(this.Service, this.Path);
        }
    }
}
