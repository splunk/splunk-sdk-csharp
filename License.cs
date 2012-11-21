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
    /// The License class represents the License Entity.
    /// </summary>
    public class License : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="License"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public License(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the time and date the license was created.
        /// </summary>
        public DateTime CreationTime
        {
            get
            {
                return this.GetDate("creation_time");
            }
        }

        /// <summary>
        /// Gets the time and date this license expires.
        /// </summary>
        public DateTime ExpirationTime
        {
            get
            {
                return this.GetDate("expiration_time");
            }
        }

        /// <summary>
        /// Gets the list of enabled features of this license.
        /// </summary>
        public string[] Features
        {
            get
            {
                return this.GetStringArray("features");
            }
        }

        /// <summary>
        /// Gets the group ID of this license.
        /// </summary>
        public string GroupId
        {
            get
            {
                return this.GetString("group_id", null);
            }
        }

        /// <summary>
        /// Gets the label of this license.
        /// </summary>
        public string Label
        {
            get
            {
                return this.GetString("label", null);
            }
        }

        /// <summary>
        /// Gets the hash value of this license.
        /// </summary>
        public string Hash
        {
            get
            {
                return this.GetString("license_hash", null);
            }
        }

        /// <summary>
        /// Gets the maximum number of violations allowed for this license. 
        /// A violation occurs when you exceed the maximum indexing volume 
        /// allowed for your license. Exceeding the maximum violations will 
        /// disable search. 
        /// </summary>
        public int MaxViolations
        {
            get
            {
                return this.GetInteger("max_violations");
            }
        }

        /// <summary>
        /// Gets the daily indexing quota, which is the maximum bytes per day 
        /// of indexing volume for this license.
        /// </summary>
        public int Quota
        {
            get
            {
                return this.GetInteger("quota");
            }
        }

        /// <summary>
        /// Gets  the source types that, when indexed, count against the 
        /// indexing volume for this license. All source types are allowed if 
        /// none are explicitly specified.
        /// </summary>
        public string[] SourceTypes
        {
            get
            {
                return this.GetStringArray("sourcetypes");
            }
        }

        /// <summary>
        /// Gets the stack ID for this license.
        /// </summary>
        public string StackId
        {
            get
            {
                return this.GetString("stack_id");
            }
        }

        /// <summary>
        /// Gets the status of this license.
        /// </summary>
        public string Status
        {
            get
            {
                return this.GetString("status");
            }
        }

        /// <summary>
        /// Gets additional information about the type of this license stack.
        /// </summary>
        public string Type
        {
            get
            {
                return this.GetString("type", null);
            }
        }

        /// <summary>
        /// Gets the number of days remaining in the rolling time window 
        /// for this license. A license violation occurs when you have 
        /// exceeded the number of allowed warnings within this period of 
        /// time. 
        /// </summary>
        public int WindowPeriod
        {
            get
            {
                return this.GetInteger("window_period");
            }
        }
    }
}
