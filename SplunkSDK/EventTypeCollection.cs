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
    /// The <see cref="EventTypeCollection"/> class represents the collection
    /// of <see cref="EventType"/>s.
    /// </summary>
    public class EventTypeCollection : EntityCollection<EventType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypeCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        public EventTypeCollection(Service service)
            : base(service, "saved/eventtypes", typeof(EventType))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventTypeCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="args">The arguments.</param>
        public EventTypeCollection(Service service, Args args)
            : base(service, "saved/eventtypes", args, typeof(EventType))
        {
        }

        /// <summary>
        /// Creates an event type.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="search">The search string.</param>
        /// <returns>The EventType.</returns>
        public EventType Create(string name, string search)
        {
            return this.Create(name, search, null);
        }

        /// <summary>
        /// Creates an event type.
        /// </summary>
        /// <param name="name">The name of the event.</param>
        /// <param name="search">The search string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The event type.</returns>
        public EventType Create(string name, string search, Args args)
        {
            args = Args.Create(args);
            args.Add("search", search);
            return base.Create(name, args);
        }
    }
}
