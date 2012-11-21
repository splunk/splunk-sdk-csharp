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
    /// The OutputDefault class represents the Output Default Entity.
    /// </summary>
    public class OutputDefault : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputDefault"/> 
        /// class.
        /// </summary>
        /// <param name="service">The service</param>
        public OutputDefault(Service service)
            : base(service, "data/outputs/tcp/default")
        {
        }

        /// <summary>
        /// Gets a value indicating whether this forwarder performs automatic 
        /// load balancing.
        /// </summary>
        public bool AutoLB
        {
            get
            {
                return this.GetBoolean("autoLB", false);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether tcpout settings are 
        /// disabled.
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
        /// Gets the inclusive set of indexes (whitelist 0) for this forwarder.
        /// This is an ordered list of whitelists and blacklists, which together
        /// decide if events should be forwarded to an index.
        /// </summary>
        public string ForwardedIndex0Whitelist
        {
            get
            {
                return this.GetString("forwardedindex.0.whitelist", null);
            }
        }

        /// <summary>
        /// Gets the exclusive set of indexes (blacklist 1) for this forwarder.
        /// This is an ordered list of whitelists and blacklists, which together
        /// decide if events should be forwarded to an index.
        /// </summary>
        public string ForwardedIndex1Blacklist
        {
            get
            {
                return this.GetString("forwardedindex.1.blacklist", null);
            }
        }

        /// <summary>
        /// Gets the inclusive set of indexes (whitelist 2) for this forwarder.
        /// This is an ordered list of whitelists and blacklists, which together
        /// decide if events should be forwarded to an index.
        /// </summary>
        public string ForwardedIndex2Whitelist
        {
            get
            {
                return this.GetString("forwardedindex.2.whitelist", null);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the index filter for this forwarder 
        /// is disabled.
        /// </summary>
        public bool ForwardedIndexFilterDisable
        {
            get
            {
                return this.GetBoolean("forwardedindex.filter.disable", false);
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
        /// Gets a value indicating whether to index all data locally, in 
        /// addition to forwarding it.
        /// </summary>
        public bool IndexAndForward
        {
            get
            {
                return this.GetBoolean("indexAndForward", false);
            }
        }

        /// <summary>
        /// Gets the maximum queue size, in bytes. Note that this value is a
        /// string an is of the form integer[KB|MB|GB].
        /// </summary>
        public string MaxQueueSize
        {
            get
            {
                return this.GetString("maxQueueSize", null);
            }
        }

        /// <summary>
        /// Sets a value indicating whether events are processed by splunk 
        /// before forwarding. Set to false if you are sending to a third-party 
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
        /// Updates the entity with the arguments. Note that since name is 
        /// required, if name is not presen, add the only valid name, tcpout.
        /// </summary>
        /// <param name="args">The arguments to update.</param>
        public override void Update(Dictionary<string, object> args)
        {
            if (!args.ContainsKey("name"))
            {
                args = Args.Create("name", "tcpout");
            }

            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the cached arguments. Note that since name 
        /// is required, if name is not presen, add the only valid name, tcpout.
        /// </summary>
        public override void Update()
        {
            if (this.toUpdate.Count > 0 && this.toUpdate.ContainsKey("name"))
            {
                this.SetCacheValue("name", "tcpout");
            }

            base.Update();
        }
    }
}
