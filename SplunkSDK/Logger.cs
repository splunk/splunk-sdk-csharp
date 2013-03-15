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

namespace Splunk
{
    using System;

    /// <summary>
    /// The <see cref="Logger"/> class represents the Logger Entity.
    /// </summary>
    public class Logger : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The path</param>
        public Logger(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the logging level of this logger. Valid values are:
        /// FATAL, CRIT, WARN, INFO, and DEBUG. The value of CRIT is only 
        /// availble in versions of Splunk prior to version 4.3.4.
        /// </summary>
        public string Level
        {
            get
            {
                return this.GetString("level");
            }

            set
            {
                this.SetCacheValue("level", value);
            }
        }
    }
}
