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

using System;

namespace Splunk.ModularInputs
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    ///     When Splunk executes a modular input script 
    ///     to stream events into Splunk, 
    ///     it reads configuration information from
    ///     inputs.conf files in the system.  It then passes 
    ///     this configuration in XML format to
    ///     the script.  The modular input script reads
    ///     the configuration information from stdin.
    ///     This object is used to parse and access the XML data.
    /// </summary>
    [XmlRoot("input")]
    public class InputDefinition : InputDefinitionBase
    {
        //XML example:
        //<?xml version="1.0" encoding="utf-16"?>
        //<input xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
        //  <server_host>tiny</server_host>
        //  <server_uri>https://127.0.0.1:8089</server_uri>
        //  <checkpoint_dir>/opt/splunk/var/lib/splunk/modinputs</checkpoint_dir>
        //  <session_key>123102983109283019283</session_key>
        //  <configuration>
        //    <stanza name="foobar://aaa">
        //      <param name="param1">value1</param>
        //      <param name="param2">value2</param>
        //      <param name="disabled">0</param>
        //      <param name="index">default</param>
        //    </stanza>
        //    <stanza name="foobar://bbb">
        //      <param name="param1">value11</param>
        //      <param name="param2">value22</param>
        //      <param name="disabled">0</param>
        //      <param name="index">default</param>
        //      <param_list name="multiValue">
        //        <value>value1</value>
        //        <value>value2</value>
        //      </param_list>
        //      <param_list name="multiValue2">
        //        <value>value3</value>
        //        <value>value4</value>
        //      </param_list>
        //    </stanza>
        //  </configuration>
        //</input>

        /// <summary>
        /// A dictionary of Stanzas
        /// </summary>
        private Dictionary<string, Stanza> stanzas;

        /// <summary>
        ///     Gets or sets the child tags for &lt;configuration&gt; 
        ///     which are based on the schema you define in the
        ///     inputs.conf.spec file for your modular input.  
        ///     Splunk reads all the configurations in
        ///     the Splunk installation and passes them to 
        ///     the script in &lt;stanza&gt; tags.
        /// </summary>
        [XmlArray("configuration")]
        [XmlArrayItem("stanza")]
        public List<Stanza> StanzaXmlElements { get; set; }

        /// <summary>
        /// Gets a dictionary of Stanzas keyed by stanza's name.
        /// </summary>
        public IDictionary<string, Stanza> Stanzas
        {
            get
            {
                if (this.stanzas == null)
                {
                    this.stanzas = this.StanzaXmlElements
                        .ToDictionary(p => p.Name);
                }
                return this.stanzas;
            }
        }

        /// <summary>
        ///     Gets the stanza. 
        ///     If there are more than one stanza, this method will fail.
        /// </summary>
        // This method is provided since it is very common to have only one.
        // That is the case when UseSingleInstance is true.
        public Stanza Stanza
        {
            get
            {
                if (this.StanzaXmlElements.Count > 1)
                {
                    throw new InvalidOperationException(
                        "There are more than one stanza. Use Stanzas property instead.");
                }
                return this.StanzaXmlElements[0];
            }
        }
    }
}
