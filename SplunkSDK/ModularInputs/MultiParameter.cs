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
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    ///     Definition of a key-value pair in the context of an XML object
    /// </summary>
    [XmlRoot("param_list")]
    public class MultiParameter : ParameterBase
    {
        [XmlRoot("value")]
        public class Value
        {
            /// <summary>
            /// Gets or sets the value of the parameter
            /// </summary>
            [XmlText]
            public string Text { get; set; }

            /// <summary>
            /// Returns the string value
            /// </summary>
            /// <returns>The string value
             /// </returns>
            public override string ToString()
            {
                return Text;
            }

            /// <summary>
            /// Convert to a <c>string</c>.
            /// Same as <see cref="ToString"/>
            /// </summary>
            /// <param name="value">Field value</param>
            /// <returns>The string value 
            /// <see cref="DefaultDelimiter"/>.
            /// </returns>
            public static implicit operator string(Value value)
            {
                return value.ToString();
            }
        }

        /// <summary>
        /// Gets or sets the value of the parameter
        /// </summary>
        [XmlElement("value")]
        public List<Value> Values { get; set; }

        /// <summary>
        ///     Serializes this object to XML output
        /// </summary>
        /// <returns>The XML String</returns>
        public string Serialize()
        {
            var x = new XmlSerializer(typeof(MultiParameter));
            var sw = new StringWriter();
            x.Serialize(sw, this);
            return sw.ToString();
        }
    }
}
