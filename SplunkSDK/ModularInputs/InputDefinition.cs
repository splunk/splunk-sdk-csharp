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
    using System.Xml.Serialization;

    /// <summary>
    ///     When Splunk executes a modular input script, it reads configuration information from
    ///     inputs.conf files in the system.  It then passes this configuration in XML format to
    ///     the script.  The modular input script reads the configuration information from stdin.
    ///     This object is used to parse and access the XML data.
    /// </summary>
    [XmlRoot("input")]
    public class InputDefinition
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InputDefinition" /> class,
        ///     which is empty.
        /// </summary>
        internal InputDefinition()
        {
            this.Stanzas = new List<Stanza>();
        }

        /// <summary>
        ///     Gets or sets the hostname for the splunk server
        /// </summary>
        [XmlElement("server_host")]
        public string ServerHost { get; set; }

        /// <summary>
        ///     Gets or sets he management port for the splunk server, identified by host, port and protocol
        /// </summary>
        [XmlElement("server_uri")]
        public string ServerUri { get; set; }

        /// <summary>
        ///     Gets or sets the directory used for a script to save checkpoints.  This is where splunk tracks the
        ///     input state from sources from which it is reading.
        /// </summary>
        [XmlElement("checkpoint_dir")]
        public string CheckpointDirectory { get; set; }

        /// <summary>
        ///     Gets or sets the REST API session key for this modular input
        /// </summary>
        [XmlElement("session_key")]
        public string SessionKey { get; set; }

        /// <summary>
        ///     Gets or sets the child tags for &lt;configuration&gt; are based on the schema you define in the
        ///     inputs.conf.spec file for your modular input.  Splunk reads all the configurations in
        ///     the Splunk installation and passes them to the script in &lt;stanza&gt; tags.
        /// </summary>
        [XmlArray("configuration")]
        [XmlArrayItem("stanza")]
        public List<Stanza> Stanzas { get; set; }

        /// <summary>
        ///     Read the input stream specified and return the parsed XML input.
        /// </summary>
        /// <param name="input">The input stream</param>
        /// <returns>An InputDefinition object</returns>
        public static InputDefinition ReadInputDefinition(TextReader input)
        {
            var x = new XmlSerializer(typeof(InputDefinition));
            var id = (InputDefinition)x.Deserialize(input);
            return id;
        }

        /// <summary>
        ///     Serializes this object to XML output
        /// </summary>
        /// <returns>The XML String</returns>
        public string Serialize()
        {
            var x = new XmlSerializer(typeof(InputDefinition));
            var sw = new StringWriter();
            x.Serialize(sw, this);
            return sw.ToString();
        }

        /// <summary>
        ///     Each stanza in the inputs.conf has a set of parameters that are stored in a KV pair store.
        /// </summary>
        [XmlRoot("stanza")]
        public class Stanza
        {
            /// <summary>
            /// Configuration parameters provided by Splunk at runtime
            /// </summary>
            private List<Parameter> @params;

            /// <summary>
            ///     Initializes a new instance of the <see cref="Stanza" /> class,
            ///     
            /// </summary>
            internal Stanza()
            {
                @params = new List<Parameter>();
            }

            /// <summary>
            /// Gets or sets the name of this stanza.
            /// </summary>
            [XmlAttribute("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the list of parameters for defining this stanza.
            /// </summary>
            [XmlElement("param")]
            public List<Parameter> Parameters
            {
                get { return @params; }
                set { @params = value; }
            }

            /// <summary>
            ///     When accessing the parameters, normally you will want to access the
            ///     parameters by name.  This method translates the list into an associative
            ///     array for access purposes.
            /// </summary>
            /// <param name="name">The name of the parameter</param>
            /// <param name="defaultValue">If not found, what should be returned</param>
            /// <returns>The value of the parameter, or defaultValue if the parameter does not exist.</returns>
            public string GetParameterByName(string name, string defaultValue)
            {
                foreach (var t in @params)
                {
                    if (t.Name.Equals(name))
                    {
                        return t.Value;
                    }
                }
                return defaultValue;
            }

            /// <summary>
            ///     Serializes this object to XML output
            /// </summary>
            /// <returns>The XML String</returns>
            public string Serialize()
            {
                var x = new XmlSerializer(typeof(Stanza));
                var sw = new StringWriter();
                x.Serialize(sw, this);
                return sw.ToString();
            }
        }

        /// <summary>
        ///     Definition of a key-value pair in the context of an XML object
        /// </summary>
        [XmlRoot("param")]
        public class Parameter
        {
            /// <summary>
            /// Gets or sets the name of the parameter
            /// </summary>
            [XmlAttribute("name")]
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets the value of the parameter
            /// </summary>
            [XmlText]
            public string Value { get; set; }

            /// <summary>
            ///     Serializes this object to XML output
            /// </summary>
            /// <returns>The XML String</returns>
            public string Serialize()
            {
                var x = new XmlSerializer(typeof(Parameter));
                var sw = new StringWriter();
                x.Serialize(sw, this);
                return sw.ToString();
            }
        }
    }
}