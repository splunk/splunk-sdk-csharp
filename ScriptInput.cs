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
    using System;

    /// <summary>
    /// Represents the Input subclass Script Input
    /// </summary>
    public class ScriptInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptInput"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public ScriptInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the time when the scripted input stopped running.
        /// </summary>
        public DateTime EndTime
        {
            get
            {
                return this.GetDate("endtime", DateTime.MinValue);
            }
        }

        /// <summary>
        /// Gets the OS group of commands for this scripted input.
        /// </summary>
        public string Group
        {
            get
            {
                return this.GetString("group", null);
            }
        }

        /// <summary>
        /// Gets or sets the source host for this scripted input.
        /// </summary>
        public string Host
        {
            get
            {
                return this.GetString("host", null);
            }

            set
            {
                this.SetCacheValue("host", value);
            }
        }

        /// <summary>
        /// Gets or sets the index name for this scripted input.
        /// </summary>
        public string Index
        {
            get
            {
                return this.GetString("index");
            }

            set
            {
                this.SetCacheValue("index", value);
            }
        }

        /// <summary>
        /// Gets or sets the frequency for running this scripted input.
        /// </summary>
        public string Interval
        {
            get
            {
                return this.GetString("interval");
            }

            set
            {
                this.SetCacheValue("interval", value);
            }
        }

        /// <summary>
        /// Gets the Input type of this object, Script.
        /// </summary>
        public override InputKind Kind
        {
            get
            {
                return InputKind.Script;
            }
        }

        /// <summary>
        /// Gets or sets the username that this scripted input runs under.
        /// </summary>
        public string PassAuth
        {
            get
            {
                return this.GetString("passAuth", null);
            }

            set
            {
                this.SetCacheValue("passAuth", value);
            }
        }

        /// <summary>
        /// Gets or sets the source of events from this scripted input.
        /// </summary>
        public string Source
        {
            get
            {
                return this.GetString("source", null);
            }

            set 
            {
                this.SetCacheValue("source", value);
            }
        }

        /// <summary>
        /// Gets or sets the source type of events from this scripted input.
        /// </summary>
        public string SourceType
        {
            get
            {
                return this.GetString("sourcetype", null);
            }

            set
            {
                this.SetCacheValue("sourcetype", value);
            }
        }

        /// <summary>
        /// Gets the time when the script was started.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.GetDate("starttime", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Sets a value indicating whether the scripted input is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Sets the source name for events from this scripted input. The same source
        /// should not be used for multiple data inputs.
        /// </summary>
        public string RenameSource
        {
            set
            {
                this.SetCacheValue("rename-source", value);
            }
        }
    }
}
