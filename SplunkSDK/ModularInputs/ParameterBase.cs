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

using System.Xml.Serialization;

namespace Splunk.ModularInputs
{
    /// <summary>
    /// Base class for different types of parameters in an input configuration.
    /// </summary>
    public abstract class ParameterBase
    {
        /// <summary>
        /// The name of the parameter.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The value of the parameter.
        /// </summary>
        /// <remarks>
        /// This parameter is read-only.
        /// </remarks>
        // 'Value' or 'ValueBase' would be better names. However they conflict with 
        // name of nested types.
        internal abstract ValueBase ValueAsBaseType { get; }

        /// <summary>
        /// Base class for different types of parameter values.
        /// </summary>
        public abstract class ValueBase
        {
        }
    }
}