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
    ///     Base class for input configuration
    /// </summary>
    [XmlRoot("input")]
    public class InputConfigurationBase
    {
        /// <summary>
        ///     Gets or sets the hostname for the splunk server
        /// </summary>
        [XmlElement("server_host")]
        public string ServerHost { get; set; }

        /// <summary>
        ///     Gets or sets he management uri for the splunk server, identified by host, port and protocol
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
    }
}