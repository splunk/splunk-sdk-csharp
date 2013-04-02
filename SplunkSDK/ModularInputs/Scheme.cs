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
    ///     The Scheme class represents the XML output when a Modular Input is called
    ///     with the --scheme argument.
    /// </summary>
    [XmlRoot("scheme")]
    public class Scheme
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Scheme" /> class.
        ///     Sets up default values for this scheme
        /// </summary>
        public Scheme()
        {
            this.UseExternalValidation = false;
            this.UseSingleInstance = false;
            this.Endpoint = new EndpointElement();
        }

        /// <summary>
        ///     Enumeration of the valid values for the Scheme Streaming Mode
        /// </summary>
        public enum StreamingModeEnum
        {
            /// <summary>
            ///     A plain-text modular input
            /// </summary>
            [XmlEnum(Name = "simple")]
            Simple,

            /// <summary>
            ///     Data is streamed to splunk with XML objects
            /// </summary>
            [XmlEnum(Name = "xml")]
            Xml
        }

        /// <summary>
        ///     Gets or sets the Modular Input title.
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the Modular Input description.
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether external validation 
        ///     is enabled for this Modular Input.
        ///     Default is false.
        /// </summary>
        [XmlElement("use_external_validation")]
        public bool UseExternalValidation { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether to launch a single instance of the script or
        ///     one script instance for each input stanza.  Default is false.
        /// </summary>
        [XmlElement("use_single_instance")]
        public bool UseSingleInstance { get; set; }

        /// <summary>
        ///     Gets or sets Streaming Mode for this Modular Input (SIMPLE or XML)
        /// </summary>
        [XmlElement("streaming_mode")]
        public StreamingModeEnum StreamingMode { get; set; }

        /// <summary>
        ///     Gets or sets the endpoint description for this modular input
        /// </summary>
        [XmlElement("endpoint")]
        public EndpointElement Endpoint { get; set; }

        /// <summary>
        ///     Serializes this object to XML output
        /// </summary>
        /// <returns>The XML String</returns>
        public string Serialize()
        {
            var x = new XmlSerializer(typeof(Scheme));
            var sw = new StringWriter();
            x.Serialize(sw, this);
            return sw.ToString();
        }

        /// <summary>
        ///     The Endpoint is a collection of arguments that represent parameters
        ///     to the inputs.conf stanza
        /// </summary>
        [XmlRoot("endpoint")]
        public class EndpointElement
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="EndpointElement" /> class.
            ///     It will be empty.
            /// </summary>
            internal EndpointElement()
            {
                this.Arguments = new List<Argument>();
            }

            /// <summary>
            ///     Gets or sets the list of arguments to this endpoint.  Note that this represents
            ///     the parameters list for the InputDefinition as well (with some standard
            ///     exceptions).
            /// </summary>
            [XmlArray("args")]
            [XmlArrayItem("arg")]
            public List<Argument> Arguments { get; set; }

            /// <summary>
            ///     Serializes this object to XML output
            /// </summary>
            /// <returns>The XML String</returns>
            public string Serialize()
            {
                var x = new XmlSerializer(typeof(EndpointElement));
                var sw = new StringWriter();
                x.Serialize(sw, this);
                return sw.ToString();
            }
        }
    }
}
