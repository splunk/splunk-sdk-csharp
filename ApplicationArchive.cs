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
    /// This represents the ApplicationArchive class
    /// </summary>
    public class ApplicationArchive : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationArchive"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public ApplicationArchive(Service service, string path)
            : base(service, path + "/package")
        {
        }

        /// <summary>
        /// Gets the app name.
        /// </summary>
        public string AppName
        {
            get
            {
                return this.GetString("name");
            }
        }

        /// <summary>
        /// Gets the path indicating where the app archive file is stored on the
        /// server, for direct file access.
        /// </summary>
        public string FilePath
        {
            get
            {
                return this.GetString("path");
            }
        }

        /// <summary>
        /// Gets a value indicating whether to reload the objects contained in the 
        /// locally-installed app.
        /// </summary>
        public bool Refreshes
        {
            get
            {
                return this.GetBoolean("refresh", false);
            }
        }

        /// <summary>
        /// Gets a URL to the app archive file on the server, for web browser access.
        /// </summary>
        public string Url
        {
            get
            {
                return this.GetString("url");
            }
        }
    }
}
