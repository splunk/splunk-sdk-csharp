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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// This object is used to parse and access XML data.
    /// </summary>
    /// <remarks>
    /// When Splunk runs a modular input script to stream events into
    /// Splunk, it reads configuration information from inputs.conf files
    /// in the system. It then passes this configuration in XML format to
    /// the script. The modular input script reads the configuration
    /// information from stdin.
    /// </remarks>
    [XmlRoot("input")]
    public class InputConfiguration : InputConfigurationBase
    {
        /// <summary>
        /// A dictionary of stanzas.
        /// </summary>
        private Dictionary<string, Stanza> stanzaByName;

        /// <summary>
        /// The child tags for the configuration element.
        /// </summary>
        /// <remarks>
        /// The child tags are based on the schema you define in the
        /// inputs.conf.spec file for your modular input. Splunk reads all of
        /// the configurations in the Splunk installation and passes them to 
        /// the script in the <b>stanza</b> tags.
        /// </remarks>
        [XmlArray("configuration")]
        [XmlArrayItem("stanza")]
        public List<Stanza> Stanzas { get; set; }

        /// <summary>
        /// A dictionary of stanzas keyed by stanza name.
        /// </summary>
        /// <remarks>
        /// This property is read-only.
        /// </remarks>
        public IDictionary<string, Stanza> StanzaByName
        {
            get
            {
                if (this.stanzaByName == null)
                {
                    this.stanzaByName = this.Stanzas
                        .ToDictionary(p => p.Name);
                }
                return this.stanzaByName;
            }
        }

        /// <summary>
        /// Reads the input stream specified and returns the parsed XML input.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>An InputDefinition object.</returns>
        internal static InputConfiguration Read(TextReader input)
        {
            var x = new XmlSerializer(typeof(InputConfiguration));
            var id = (InputConfiguration)x.Deserialize(input);
            return id;
        }

        /// <summary>
        /// Serializes this object to XML output. Used by unit tests.
        /// </summary>
        /// <returns>The XML String.</returns>
        internal string Serialize()
        {
            var x = new XmlSerializer(typeof(InputConfiguration));
            var sw = new StringWriter();
            x.Serialize(sw, this);
            return sw.ToString();
        }
    }
}
