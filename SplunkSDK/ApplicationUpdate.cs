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
    /// The <see cref="ApplicationUpdate"/> class represents information for 
    /// an update to a locally-installed Splunk app.
    /// </summary>
    public class ApplicationUpdate : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUpdate"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="path">The path.</param>
        public ApplicationUpdate(Service service, string path)
            : base(service, path + "/update")   
        {
        }

        /// <summary>
        /// Gets the fully-qualified URL to the app update.
        /// </summary>
        public string AppUrl
        {
            get
            {
                return this.GetString("update.appurl", null);
            }
        }

        /// <summary>
        /// Gets the checksum of the app.
        /// </summary>
        public string Checksum
        {
            get
            {
                return this.GetString("update.checksum", null);
            }
        }

        /// <summary>
        /// Gets the checksum type of the app.
        /// </summary>
        public string ChecksumType
        {
            get
            {
                return this.GetString("update.checksum.type", null);
            }
        }

        /// <summary>
        /// Gets the fully-qualified URL to the app's homepage.
        /// </summary>
        public string Homepage
        {
            get
            {
                return this.GetString("update.homepage", null);
            }
        }

        /// <summary>
        /// Gets the app's name.
        /// </summary>
        public string UpdateName
        {
            get
            {
                return this.GetString("update.name", null);
            }
        }
        
        /// <summary>
        /// Gets the app's update size.
        /// </summary>
        public int UpdateSize
        {
            get
            {
                return this.GetInteger("update.size", -1);
            }
        }

        /// <summary>
        /// Gets the app's version.
        /// </summary>
        public string Version
        {
            get
            {
                return this.GetString("update.version", null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether an implicit ID is required.
        /// </summary>
        public bool IsImplicitIdRequired
        {
            get
            {
                return this.GetBoolean("update.implicit_id_required", false);
            }
        }
    }
}
