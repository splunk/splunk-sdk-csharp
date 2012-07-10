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
    using System;

    /// <summary>
    /// This represents the ApplicationSetup class.
    /// </summary>
    public class ApplicationSetup : Entity 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSetup"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public ApplicationSetup(Service service, string path)
            : base(service, path + "/setup")
        {
        }

        /// <summary>
        /// Gets a value indicating whether to load the objects contained in the 
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
        /// Gets the app's setup information in XML format.
        /// </summary>
        public string SetupXml
        {
            get
            {
                return this.GetString("eai:setup");
            }
        }

        // Because all other keys are dynamic and context specific, they must
        // be retrieved using Dictionary "object.Get(key)" access.
    }
}
