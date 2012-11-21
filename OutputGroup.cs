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
    /// The OutputGroup class represents a collection of the Output Groups. 
    /// </summary>
    public class OutputGroup : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputGroup"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public OutputGroup(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether this forwarder performs
        /// automatic load balancing. This is not used in Splunk 5.0+.
        /// </summary>
        public bool AutoLB
        {
            get
            {
                return this.GetBoolean("autoLB", false);
            }

            set
            {
                this.SetCacheValue("autoLB", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether the forwarder sends compressed data.
        /// </summary>
        public bool Compressed
        {
            set
            {
                this.SetCacheValue("compressed", value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this group is disabled
        /// </summary>
        public bool Disabled
        {
            get
            {
                return this.GetBoolean("disabled", false);
            }

            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Sets the numberof seconds to wait before throwing events when the 
        /// queue is full. The default is -1, which means do not drop events.
        /// Setting this to -1 or 0 causes the output queue to block when it 
        /// gets full, which causes further blocking up the processing chain. If
        /// any target group's queue is blocked, no more data reaches any other 
        /// target group. Using auto load-balancing is the best way to minimize 
        /// this condition, because, in that case, multiple receivers must be 
        /// down (or jammed up) before queue blocking can occur.
        /// </summary>
        public int DropEventsOnQueueFull
        {
            set
            {
                this.SetCacheValue("dropEventsOnQueueFull", value);
            }
        }

        /// <summary>
        /// Sets the number of seconds to send a heartbeat packet to the 
        /// receiving server.Note that Heartbeats are only sent if 
        /// SendCookedData is true. The default is 30 seconds.
        /// </summary>
        public int HeartbeatFrequency
        {
            set
            {
                this.SetCacheValue("heartbeatFrequency", value);
            }
        }

        /// <summary>
        /// Sets the maximum queue size, in bytes. Note that this value is a
        /// string an is of the form integer[KB|MB|GB].
        /// </summary>
        public string MaxQueueSize
        {
            set
            {
                this.SetCacheValue("maxQueueSize", value);
            }
        }

        /// <summary>
        /// Gets or sets the type of output processor. Valid values are: tcpout,
        /// or syslog.
        /// </summary>
        public string Method
        {
            get
            {
                return this.GetString("method", null);
            }

            set
            {
                this.SetCacheValue("method", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether the data is processed by splunk 
        /// before sending. Set to false if you are sending to a third-party 
        /// system.
        /// </summary>
        public bool SendCookedData
        {
            set
            {
                this.SetCacheValue("sendCookedData", value);
            }
        }

        /// <summary>
        /// Gets or sets the servers of this forwarder group.
        /// </summary>
        public string[] Servers
        {
            get
            {
                // Return either the string array, OR if we have a temporary
                // concatenation (as per the setter), rebuild the array.
                string[] temp = this.GetStringArray("servers", null);
                if ((temp != null) && (temp.Length == 1))
                {
                    return temp[0].Split(',');
                }
                return temp;
            }

            set
            {
                // Take string array and build into a single string separating 
                // the terms with a , symbol (expected by the endpoint) for 
                // updating.
                string composite = string.Empty;
                for (int i = 0; i < value.Length; i++)
                {
                    if (i != 0)
                    {
                        composite = composite + ",";
                    }
                    composite = composite + value[i];
                }
                this.SetCacheValue("servers", new string[] { composite });
            }
        }

        /// <summary>
        /// Updates the entity with the arguments. Note that since servers is 
        /// required, if servers is not present, pull it from our
        /// existing structure.
        /// </summary>
        /// <param name="args">The arguments to update.</param>
        public override void Update(Dictionary<string, object> args)
        {
            if (!args.ContainsKey("servers"))
            {
                args = Args.Create("servers", this.Servers);
            }

            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the cached arguments. Note that since s
        /// ervers is required, if servers is not present, pull it from our
        /// existing structure.
        /// </summary>
        public override void Update()
        {
            if (this.toUpdate.Count > 0 && 
                !this.toUpdate.ContainsKey("servers"))
            {
                this.SetCacheValue("servers", this.Servers);
            }

            base.Update();
        }
    }
}
