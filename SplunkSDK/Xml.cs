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

namespace Splunk
{
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// The <see cref="Xml"/> class represents a collection of XML utilities.
    /// </summary>
    public class Xml
    {
        /// <summary>
        /// Parses the given input stream and returns it as an XML document 
        /// object model (DOM).
        /// </summary>
        /// <param name="input">The Stream</param>
        /// <returns>The XML document.</returns>
        public static XmlDocument Parse(Stream input) 
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(input);
            return xmlDoc;
        }
    }
}
