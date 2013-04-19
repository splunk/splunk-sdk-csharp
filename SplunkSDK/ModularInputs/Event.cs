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

namespace Splunk.ModularInputs
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using System;

    /// <summary>
    ///     Each stanza in the inputs.conf has a set of parameters that are stored in a KV pair store.
    /// </summary>
    public struct Event
    {
        public string Data { get; set; }
        public string Source { get; set; }
        public string SourceType { get; set; }
        public string Index { get; set; }
        public string Host { get; set; }

        public DateTime? Time 
        { 
            get
            {
                return this.time;
            }

            set
            {
                if (value != null && this.Unbroken)
                {
                    throw new InvalidOperationException(
                        "Time property cannot be used for an unbroken event.");
                }

                this.time = value;
            }
        }
        private DateTime? time;

        public bool Done { get; set; }

        public bool Unbroken
        {
            get
            {
                return this.unbroken;
            }

            set
            {
                if (value && this.Time != null)
                {
                    throw new InvalidOperationException(
                        "An unbroken event cannot have a Time property that is not null.");
                }

                this.unbroken = value;
            }
        }
        private bool unbroken;

        public string Stanza { get; set; }

    }
}
