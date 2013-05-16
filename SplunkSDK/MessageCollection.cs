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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The <see cref="MessageCollection"/> class represents a collection of
    /// <see cref="Message"/>s.
    /// </summary>
    public class MessageCollection : EntityCollection<Message>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        public MessageCollection(Service service)
            : base(service, "messages", typeof(Message))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="args">The optional arguments</param>
        public MessageCollection(Service service, Args args)
            : base(service, "messages", args, typeof(Message))
        {
        }

        /// <summary>
        /// Creates a new message.
        /// </summary>
        /// <param name="name">The key name.</param>
        /// <param name="value">The value.</param>
        /// <returns>The new message.</returns>
        public Message Create(string name, string value)
        {
            Args args = new Args("value", value);
            return base.Create(name, args);
        }
    }
}
