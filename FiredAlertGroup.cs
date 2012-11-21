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
    /// This represents the FiredAlertGroup class
    /// </summary>
    public class FiredAlertGroup : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiredAlertGroup"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public FiredAlertGroup(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Returns a collection of fired alerts.
        /// </summary>
        /// <returns>The collection od fired alerts.</returns>
        public EntityCollection<FiredAlert> Alerts()
        {
            return new EntityCollection<FiredAlert>(
                this.Service, 
                this.Path, 
                typeof(FiredAlert));
        }
    }
}
