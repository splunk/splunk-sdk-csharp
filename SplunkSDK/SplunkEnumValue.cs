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

using System;

namespace Splunk
{
    /// <summary>
    /// The <see cref="SplunkEnumValue"/> class represents an <see cref="Attribute"/>
    /// for Enum types holding a string value used by Splunk REST API.
    /// </summary>
    internal class SplunkEnumValue : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplunkEnumValue" /> class.
        /// </summary>
        /// <param name="value">The value of the custom string.</param>
        public SplunkEnumValue(string value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets the value of the custom string.
        /// </summary>
        public string Value { get; private set; }
    }
}
