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
    /// <summary>
    /// Extends Args for OutputGroup creation setters
    /// </summary>
    public class OutputGroupArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether the forwarder sends compressed data.
        /// </summary>
        public bool Compressed
        {
            set
            {
                this["compressed"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether this group is disabled.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
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
                this["dropEventsOnQueueFull"] = value;
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
                this["heartbeatFrequency"] = value;
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
                this["maxQueueSize"] = value;
            }
        }

        /// <summary>
        /// Sets the type of output processor. Valid values are: tcpout,
        /// or syslog.
        /// </summary>
        public string Method
        {
            set
            {
                this["method"] = value;
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
                this["sendCookedData"] = value;
            }
        }

        /// <summary>
        /// Sets the list of servers to include in the group. This is required.
        /// wkcfix -- doc says comma separated list, but GET is an array.
        /// </summary>
        public string[] Servers
        {
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
                this["servers"] = new string[] { composite };
            }
        }
    }
}