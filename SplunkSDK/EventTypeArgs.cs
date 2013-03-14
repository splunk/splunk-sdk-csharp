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
    /// Extends Args for EventType creation setters
    /// </summary>
    public class EventTypeArgs : Args
    {
        /// <summary>
        /// Sets the description of this event type
        /// </summary>
        public string Description
        {
            set
            {
                this["description"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the event type is disabled.
        /// Note that changing the setting does not take effect until splunk is
        /// restarted.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the priority of this event type. The range is 1 to 10, with 1
        /// being the highest priority.
        /// </summary>
        public int Priority
        {
            set
            {
                this["priority"] = value;
            }
        }
    }
}