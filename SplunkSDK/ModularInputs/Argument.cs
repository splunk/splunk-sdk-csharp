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
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    ///     The Argument is the XML entity that describes the arguments
    ///     that can be placed in to the inputs.conf stanza for this modular
    ///     input.
    /// </summary>
    [XmlRoot("arg")]
    public class Argument
    {   
        /// <summary>
        /// Initializes a new instance of the <see cref="Argument" /> class,
        /// which is empty.
        /// </summary>
        public Argument()
        {
            this.DataType = DataTypeEnum.String;
            this.RequiredOnEdit = false;
            this.RequiredOnCreate = true;
        }

        /// <summary>
        ///     Enumeration of the valid values for the Endpoint Argument data type.
        /// </summary>
        public enum DataTypeEnum
        {
            /// <summary>
            ///     A boolean value - true or false
            /// </summary>
            [XmlEnum(Name = "boolean")]
            Boolean,

            /// <summary>
            ///     A numeric value - regexp = [0-9\.]+
            /// </summary>
            [XmlEnum(Name = "number")]
            Number,

            /// <summary>
            ///     A string - virtually everything else
            /// </summary>
            [XmlEnum(Name = "string")]
            String
        }

        /// <summary>
        ///     Gets or sets the unique Name for this parameter
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the label for the parameter
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        ///     Gets or sets the description of the parameter
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the validation rules for arguments passed to an endpoint
        ///     create or edit action.
        /// </summary>
        [XmlElement("validation")]
        public string Validation { get; set; }

        /// <summary>
        ///     Gets or sets the value for use with scripts that return data in JSON format.  Defines the
        ///     data type of the parameter.  Default data type is string.
        /// </summary>
        [XmlElement("data_type")]
        public DataTypeEnum DataType { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the parameter is required for edit.  Default behavior
        ///     is that arguments for edit are optional.  Set this to true to override
        ///     this behavior, and make the parameter required.
        /// </summary>
        [XmlElement("required_on_edit")]
        public bool RequiredOnEdit { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the parameter is required for create.  Default behavior
        ///     is that arguments for create are required.  Set this to false to override
        ///     this behavior, and make the parameter optional.
        /// </summary>
        [XmlElement("required_on_create")]
        public bool RequiredOnCreate { get; set; }

        /// <summary>
        ///     Serializes this object to XML output
        /// </summary>
        /// <returns>The XML String</returns>
        public string Serialize()
        {
            var x = new XmlSerializer(typeof(Argument));
            var sw = new StringWriter();
            x.Serialize(sw, this);
            return sw.ToString();
        }
    }
}
