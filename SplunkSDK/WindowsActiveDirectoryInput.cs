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
    /// The <see cref="WindowsActiveDirectoryInput"/> class represents the 
    /// <see cref="Input"/> subclass Windows Active Directory Input.
    /// </summary>
    public class WindowsActiveDirectoryInput : Input
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="WindowsActiveDirectoryInput"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="path">The path.</param>
        public WindowsActiveDirectoryInput(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Sets a value that indicates whether this input is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Gets or sets the index name of this Windows Active Directory input.
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
        /// Gets the input type of this object, Windows Active Directory.
        /// </summary>
        public InputKind Kind
        {
            get
            {
                return InputKind.WindowsActiveDirectory;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the directory path 
        /// subtree is being monitored for this Windows Active Directory input.
        /// </summary>
        public bool MonitorSubtree
        {
            get
            {
                return this.GetBoolean("monitorSubtree");
            }

            set
            {
                this.SetCacheValue("monitorSubtree", value);
            }
        }

        /// <summary>
        /// Gets or sets the starting location in the directory path for this 
        /// Windows Active Directory input. If not specified, the the root of 
        /// the directory tree is used.
        /// </summary>
        public string StartingNode
        {
            get
            {
                return this.GetString("startingNode", null);
            }

            set
            {
                this.SetCacheValue("startingNode", value);
            }
        }

        /// <summary>
        /// Gets or sets the fully-qualified domain name of a valid, 
        /// network-accessible domain controller. If not specified, the local 
        /// machine is used.
        /// </summary>
        public string TargetDc
        {
            get
            {
                return this.GetString("targetDc", null);
            }

            set
            {
                this.SetCacheValue("targetDc", value);
            }
        }

        /// <summary>
        /// Updates the entity with the values you previously set using the 
        /// setter methods, and any additional specified arguments. The 
        /// specified arguments take precedent over the values that were set 
        /// using the setter methods.
        /// </summary>
        /// <param name="args">The key/value pairs to update.</param>
        public override void Update(Dictionary<string, object> args)
        {
            // Add required arguments if not already present
            if (!args.ContainsKey("monitorSubtree"))
            {
                args = Args.Create(args);
                args.Add("monitorSubtree", this.MonitorSubtree);
            }

            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the accumulated arguments, established by 
        /// the individual setter methods for each specific entity class.
        /// </summary>
        public override void Update()
        {
            // If not present in the update keys, add required attribute as long
            // as one pre-existing update pair exists
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("monitorSubtree"))
            {
                this.SetCacheValue("monitorSubtree", this.MonitorSubtree);
            }

            base.Update();
        }
    }
}
