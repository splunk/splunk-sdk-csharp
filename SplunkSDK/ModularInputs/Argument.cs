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

using System.Xml.Serialization;

namespace Splunk.ModularInputs
{
    /// <summary>
    /// Enumeration of the valid values for the Endpoint Argument data type.
    /// </summary>
    public enum DataType
    {
        /// <summary>
        ///     A Boolean value: true or false
        /// </summary>
        [XmlEnum(Name = "boolean")] Boolean,

        /// <summary>
        ///     A numeric value: regexp = [0-9\.]+
        /// </summary>
        [XmlEnum(Name = "number")] Number,

        /// <summary>
        ///     A string: virtually everything else
        /// </summary>
        [XmlEnum(Name = "string")] String
    }

    /// <summary>
    /// Represents the XML entity that describes the arguments that can
    /// be placed in to the inputs.conf stanza for this modular input.
    /// </summary>
    [XmlRoot("arg")]
    public class Argument
    {
        /// <summary>
        /// Initializes a new, empty instance of the <see cref="Argument" />
        /// class.
        /// </summary>
        public Argument()
        {
            this.DataType = DataType.String;
            this.RequiredOnEdit = false;
            this.RequiredOnCreate = true;
        }

        /// <summary>
        /// The unique name for the parameter.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// The label for the parameter.
        /// </summary>
        [XmlElement("title")]
        public string Title { get; set; }

        /// <summary>
        /// The description of the parameter.
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// The validation rules for arguments passed to an endpoint
        /// create or edit action.
        /// </summary>
        [XmlElement("validation")]
        public string Validation { get; set; }

        /// <summary>
        /// The value for use with scripts that return data in JSON format.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property defines the data type of the parameter.  
        /// </para>
        /// <para>
        /// The default data type is "string".
        /// </para>
        /// </remarks>
        [XmlElement("data_type")]
        public DataType DataType { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the parameter is required
        /// for edit.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Set this property to true to make the parameter required for edit.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        [XmlElement("required_on_edit")]
        public bool RequiredOnEdit { get; set; }

        /// <summary>
        /// A Boolean value that indicates whether the parameter is required
        /// for create.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Set this property to false to make the parameter optional.
        /// </para>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// </remarks>
        [XmlElement("required_on_create")]
        public bool RequiredOnCreate { get; set; }
    }
}