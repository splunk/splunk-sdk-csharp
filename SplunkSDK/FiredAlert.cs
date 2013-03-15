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

    /// <summary>
    /// The <see cref="FiredAlert"/> class represents a fired alert.
    /// </summary>
    public class FiredAlert : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FiredAlert"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public FiredAlert(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets this alert's actions (such as notifying by email, running a 
        /// script, adding to RSS, tracking in Alert Manager, and enabling 
        /// summary indexing). 
        /// </summary>
        public string[] AlertActions
        {
            get
            {
                return this.GetStringArray("actions", null);
            }
        }

        /// <summary>
        /// Gets this alert's type.
        /// </summary>
        public string AlertType
        {
            get
            {
                return this.GetString("alert_type", null);
            }
        }

        /// <summary>
        /// Gets the rendered expiration time for this alert. This was 
        /// introduced in Splunk 4.3.
        /// </summary>
        public string ExpirationTimeRendered
        {
            get 
            {
                return this.GetString("expiration_time_rendered", null);
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the result is a set of events 
        /// (digest) or a single event (per result). This was introduced in 
        /// Splunk 4.3.
        /// </summary>
        public bool IsDigestMode
        {
            get
            {
                return this.GetBoolean("digest_mode", false);
            }
        }

        /// <summary>
        /// Gets the saved search name for this alert.
        /// </summary>
        public string SavedSearchName
        {
            get
            {
                return this.GetString("savedsearch_name", null);
            }
        }

        /// <summary>
        /// Gets this alert's severity on a scale of 1 to 10, with 1 being the
        /// the most severe.
        /// </summary>
        public int Severity
        {
            get
            {
                return this.GetInteger("severity", -1);
            }
        }

        /// <summary>
        /// Gets this alert's search ID (SID).
        /// </summary>
        public string Sid
        {
            get
            {
                return this.GetString("sid", null);
            }
        }

        /// <summary>
        /// Gets the count of triggered alerts. This was introduced in Splunk
        /// 4.3.
        /// </summary>
        public int TriggeredAlertCount
        {
            get
            {
                return this.GetInteger("triggered_alerts", -1);
            }
        }

        /// <summary>
        /// Gets this alert's trigger time.
        /// </summary>
        public DateTime TriggerTime
        {
            get
            {
                return this.GetDate("trigger_time", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Gets this alert's rendered trigger time.
        /// </summary>
        public DateTime TriggerTimeRendered
        {
            get
            {
                return this.GetDate("trigger_time_rendered", DateTime.MaxValue);
            }
        }
    }
}
