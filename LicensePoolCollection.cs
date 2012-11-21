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
    /// Represents the collection of EventTypes.
    /// </summary>
    public class LicensePoolCollection : EntityCollection<LicensePool>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="LicensePoolCollection"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        public LicensePoolCollection(Service service)
            : base(service, "licenser/pools", typeof(LicensePool))
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="LicensePoolCollection"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="args">The arguments</param>
        public LicensePoolCollection(Service service, Args args)
            : base(service, "licenser/pools", args, typeof(LicensePool))
        {
        }

        /// <summary>
        /// Creates a license pool.
        /// </summary>
        /// <param name="name">The name of the event</param>
        /// <param name="quota">The quota. This is an integer followed by 
        /// MB or GB.</param>
        /// <param name="stackId">The stack ID corresponding to this license 
        /// pool.</param>
        /// <returns>The License Pool.</returns>
        public LicensePool Create(string name, string quota, string stackId)
        {
            return this.Create(name, quota, stackId, null);
        }

        /// <summary>
        /// Creates an license pool.
        /// </summary>
        /// <param name="name">The name of the event</param>
        /// <param name="quota">The quota. This is an integer followed by 
        /// MB or GB.</param>
        /// <param name="stackId">The stack ID corresponding to this license 
        /// pool.</param>
        /// <param name="args">Optional arguments.</param>
        /// <returns>The License Pool.</returns>
        public LicensePool 
            Create(string name, string quota, string stackId, Args args)
        {
            args = Args.Create(args);
            args.Add("quota", quota);
            args.Add("stack_id", stackId);
            return base.Create(name, args);
        }
    }
}
