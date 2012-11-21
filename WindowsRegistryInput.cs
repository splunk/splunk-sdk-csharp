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
    using System.Collections.Generic;

    /// <summary>
    /// Represents the Input subclass Windows Registry Input.
    /// </summary>
    public class WindowsRegistryInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsRegistryInput"/>
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public WindowsRegistryInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this Windows Registry input 
        /// has an established baseline.
        /// </summary>
        public bool Baseline
        {
            get
            {
                return this.GetBoolean("baseline");
            }

            set
            {
                this.SetCacheValue("baseline", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether this input is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Gets or sets the hive name to monitor for this Windows Registry 
        /// input.
        /// </summary>
        public string Hive
        {
            get
            {
                return this.GetString("hive");
            }

            set
            {
                this.SetCacheValue("hive", value);
            }
        }

        /// <summary>
        /// Gets or sets the index name of this Windows Registry input.
        /// </summary>
        public string Index
        {
            get
            {
                return this.GetString("index", null);
            }

            set
            {
                this.SetCacheValue("index", value);
            }
        }

        /// <summary>
        /// Gets the Input type of this object,  Windows Registry input.
        /// </summary>
        public InputKind Kind
        {
            get 
            {
                return InputKind.WindowsRegistry;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this Windows Registry input 
        /// monitors all sub-nodes under a given hive.
        /// </summary>
        public bool MonitorSubnodes
        {
            get
            {
                return this.GetBoolean("monitorSubnodes", false);
            }

            set
            {
                this.SetCacheValue("monitorSubnodes", value);
            }
        }

        /// <summary>
        /// Gets or sets the regular expression (regex) that is compared to 
        /// process names when including or excluding events for this Windows
        /// Registry input. Changes are only collected if a process name matches
        /// this regex. 
        /// </summary>
        public string Proc
        {
            get
            {
                return this.GetString("proc");
            }

            set
            {
                this.SetCacheValue("proc", value);
            }
        }

        /// <summary>
        /// Gets or sets the regular expression (regex) that is compared to 
        /// registry event types for this Windows Registry input. Only types 
        /// that match this regex are monitored. 
        /// </summary>
        public string[] Type
        {
            get
            {
                // Return either the string array, OR if we have a temporary
                // concatenation (as per the setter), rebuild the array.
                string[] temp = this.GetStringArray("type", null);
                if ((temp != null) && (temp.Length == 1))
                {
                    return temp[0].Split('|');
                }
                return temp;
            }

            set
            {
                // Take string array and build into a single string separating 
                // the terms with a | symbol (expected by the endpoint) for 
                // updating.
                string composite = string.Empty;
                for (int i = 0; i < value.Length; i++)
                {
                    if (i != 0) 
                    {
                        composite = composite + "|";
                    }
                    composite = composite + value[i];
                }
                this.SetCacheValue("type", new string[] { composite });
            }
        }

        /// <summary>
        /// Updates the entity with the values you previously set using the 
        /// setter methods, and any additional specified arguments. The 
        /// specified arguments take precedent over the values that were set 
        /// using the setter methods.
        /// </summary>
        /// <param name="args">The key/value pairs to update</param>
        public override void Update(Dictionary<string, object> args)
        {
            // Add required arguments if not already present
            if (!args.ContainsKey("baseline"))
            {
                args = Args.Create(args);
                args.Add("baseline", this.Baseline);
            }

            if (!args.ContainsKey("hive"))
            {
                args = Args.Create(args);
                args.Add("hive", this.Hive);
            }

            if (!args.ContainsKey("proc"))
            {
                args = Args.Create(args);
                args.Add("proc", this.Proc);
            }

            if (!args.ContainsKey("type"))
            {
                args = Args.Create(args);
                args.Add("type", this.Type);
            }

            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the accumulated arguments, established by 
        /// the individual setter methods for each specific entity class.
        /// </summary>
        public override void Update()
        {
            // If not present in the update keys, add required attributes as 
            // long as one pre-existing update pair exists
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("baseline"))
            {
                this.SetCacheValue("baseline", this.Baseline);
            }
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("hive"))
            {
                this.SetCacheValue("hive", this.Hive);
            }
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("proc"))
            {
                this.SetCacheValue("proc", this.Proc);
            }
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("type"))
            {
                this.SetCacheValue("type", this.Type);
            }
            base.Update();
        }
    }
}
