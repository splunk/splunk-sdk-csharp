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
    ///     Base class for input definition.
    /// </summary>
    public class ConfigurationItemBase
    {
        /// <summary>
        /// Configuration parameters provided by Splunk at runtime
        /// </summary>
        private List<Parameter> @params;


        /// <summary>
        /// Configuration parameters provided by Splunk at runtime
        /// </summary>
        private List<MultiParameter> multiParams;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ConfigurationItemBase" /> class,
        /// </summary>
        internal ConfigurationItemBase()
        {
            @params = new List<Parameter>();
            multiParams = new List<MultiParameter>();
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
        /// Gets or sets the list of multi value parameters for defining this stanza.
        /// </summary>
        [XmlElement("param_list")]
        public List<MultiParameter> MultiValueParameters
        {
            get { return multiParams; }
            set { multiParams = value; }
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
}
