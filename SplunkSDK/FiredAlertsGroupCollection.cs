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
    /// <summary>
    /// The <see cref="FiredAlertsGroupCollection"/> represents a collection
    /// of alert groups of fired alerts.
    /// </summary>
    public class FiredAlertsGroupCollection : EntityCollection<FiredAlertGroup>
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="FiredAlertsGroupCollection"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        public FiredAlertsGroupCollection(Service service) 
            : base(
                service, 
                "alerts/fired_alerts", 
                typeof(EntityCollection<FiredAlertGroup>))
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="FiredAlertsGroupCollection"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="args">The arguments.</param>
        public FiredAlertsGroupCollection(Service service, Args args)
            : base(
                service,
                "alerts/fired_alerts",
                args,
                typeof(EntityCollection<FiredAlertGroup>))
        {
        }
    }
}
