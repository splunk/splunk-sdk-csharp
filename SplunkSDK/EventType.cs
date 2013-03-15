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
    using System.Collections.Generic;

    /// <summary>
    /// The <see cref="EventType"/> class represents an event type.
    /// </summary>
    public class EventType : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventType"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="path">The path.</param>
        public EventType(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the description of this event type.
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
        /// Gets or sets the priority of this event type. The range is 1 to 10, 
        /// with 1 being the highest priority.
        /// </summary>
        public int Priority
        {
            get
            {
                return this.GetInteger("priority", -1);
            }

            set
            {
                this.SetCacheValue("priority", value);
            }
        }

        /// <summary>
        /// Gets or sets the search terms for this event type.
        /// </summary>
        public string Search
        {
            get
            {
                return this.GetString("search", null);
            }

            set
            {
                this.SetCacheValue("search", value);
            }
        }

        /// <summary>
        /// Sets a value that indicates whether the event type is disabled.
        /// Note that changing the setting does not take effect until Splunk is
        /// restarted.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Updates the event type with the values you previously set using the 
        /// setter methods, and any additional specified arguments. The 
        /// specified arguments take precedent over the values that were set 
        /// using the setter methods.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public override void Update(Dictionary<string, object> args)
        {
            // Add required arguments if not already present
            if (!args.ContainsKey("search"))
            {
                args = Args.Create(args);
                args.Add("search", this.Search);
            }
            base.Update(args);
        }

        /// <summary>
        /// Updates the event type with the accumulated arguments, established 
        /// by the individual setter methods for each specific entity class.
        /// </summary>
        public override void Update()
        {
            // If not present in the update keys, add required attribute as long
            // as one pre-existing update pair exists.
            if (toUpdate.Count > 0 && !this.toUpdate.ContainsKey("search"))
            {
                this.SetCacheValue("search", this.Search);
            }
            base.Update();
        }
    }
}
