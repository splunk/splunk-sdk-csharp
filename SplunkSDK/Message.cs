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
    /// The <see cref="Message"/> class represents a Splunk message.
    /// </summary>
    public class Message : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public Message(Service service, string path) 
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the key of this message, which is also the title.
        /// </summary>
        public string Key
        {
            get
            {
                return this.Title;
            }
        }

        /// <summary>
        /// Gets the value of this message, based on the key.
        /// </summary>
        public string Value
        {
            get
            {
                return this.GetString(this.Key);
            }
        }
    }
}
