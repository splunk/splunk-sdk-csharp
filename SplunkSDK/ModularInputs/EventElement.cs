﻿/*
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

using System;

namespace Splunk.ModularInputs
{
    /// <summary>
    /// Event element for XML event streaming.
    /// </summary>
    public struct EventElement
    {
        /// <summary>
        /// Event data.
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// The event source.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The source type.
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// The index.
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// The host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The timestamp of the event.
        /// If null, Splunk will generate a timestamp according to current time,
        /// or in case of "unbroken" event, a timestamp supplied ealier for the
        /// event will be used.
        /// </summary>
        public DateTime? Time { get; set; }
     
        /// <summary>
        /// A Boolean value that indicates whether the event stream has
        /// completed a set of events and can be flushed.
        /// </summary>
        public bool Done { get; set; }

        /// <summary>
        /// A value that indicates whether the element contains only a
        /// part of an event or multiple events. 
        /// If false, the element represents a single and whole event.
        /// </summary>
        /// <remarks>
        /// If this property is false, the element represents a single, 
        /// whole event.
        /// </remarks>
        public bool Unbroken { get; set; }

        /// <summary>
        /// The name of the stanza of the input this event belongs to.
        /// </summary>
        public string Stanza { get; set; }
    }
}