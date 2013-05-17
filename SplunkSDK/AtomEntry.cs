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

namespace Splunk
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// The <see cref="AtomEntry"/> class represents the Atom &lt;entry&gt; 
    /// element data.
    /// </summary>
    public class AtomEntry : AtomObject
    {
        /// <summary>
        /// Gets or sets the value of the Atom entry's &lt;published&gt; 
        /// element.
        /// </summary>
        public string Published 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the Atom entry's &lt;content&gt;
        /// element.
        /// </summary>
        public Record Content 
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AtomEntry"/> class.
        /// </summary>
        /// <returns>An Atom entry.</returns>
        private static AtomEntry Create() 
        {
            AtomEntry atomEntry = new AtomEntry();
            atomEntry.Links = new Dictionary<string, string>();
            return atomEntry;
        }

        /// <summary>
        /// Creates a new <see cref="AtomEntry"/> instance based on a given 
        /// stream.
        /// </summary>
        /// <param name="input">The I/O stream.</param>
        /// <returns>An Atom entry.</returns>
        /// <remarks>
        /// A few endpoints, such as search/jobs/{sid}
        /// return an Atom entry element as the root of the response.
        /// </remarks>
        public static AtomEntry Parse(Stream input) 
        {
            XmlElement root = Xml.Parse(input).DocumentElement;
            string rname = root.Name;
            string xmlns = root.GetAttribute("xmlns");
            if (!rname.Equals("entry") && 
                !xmlns.Equals("http://www.w3.org/2005/Atom")) 
            {
                throw new Exception("Unrecognized XML format");
            }
            return AtomEntry.Parse(root);
        }

        /// <summary>
        /// Creates a new <see cref="AtomEntry"/> instance based on a given XML
        /// element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <returns>An Atom entry.</returns>
        public static AtomEntry Parse(XmlElement element) 
        {
            AtomEntry entry = AtomEntry.Create();
            entry.Load(element);
            return entry;
        }

        /// <summary>
        /// Initializes the current <see cref="AtomEntry"/> instance with a
        /// given XML element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        public override void Init(XmlElement element) 
        {
            string name = element.Name;
            if (name.Equals("published")) 
            {
                this.Published = element.InnerText;
            }
            else if (name.Equals("content")) 
            {
                this.Content = this.ParseContent(element);
            }
            else 
            {
                base.Init(element);
            }
        }

        /// <summary>
        /// Returns a filtered list of child XML element nodes. This helper
        /// function makes it easier to retrieve only the element children
        /// of a given XML element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <returns>XML element list.</returns>
        public static List<XmlElement> GetChildElements(XmlElement element) 
        {
            List<XmlElement> result = new List<XmlElement>();
            foreach (XmlNode child in element.ChildNodes) 
            {
                if (child.NodeType == XmlNodeType.Element) 
                {
                    result.Add((XmlElement)child);
                }
            }
            return result;
        }

        /// <summary>
        /// Parses the <b>content</b> element of an Atom entry.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <returns>The content record.</returns>
        public Record ParseContent(XmlElement element) 
        {
            Trace.Assert(element.LocalName.Equals("content"));
            Record content = null;

            List<XmlElement> children = GetChildElements(element);

            int count = children.Count;

            // Expect content to be empty or a single <dict> element
            Trace.Assert(count == 0 || count == 1);
            if (count == 1) 
            {
                XmlElement child = children[0];
                content = this.ParseDict(child);
            }

            return content;
        }

        /// <summary>
        /// Parses a dictinary &lt;content&gt; element and returns a record
        /// object containing the parsed values.
        /// </summary>
        /// <param name="element">An XML element.</param>
        /// <returns>The record.</returns>
        public Record ParseDict(XmlElement element) 
        {
            Trace.Assert(element.Name.Equals("s:dict"));
            if (element.FirstChild == null) 
            {
                return null;
            }

            List<XmlElement> children = GetChildElements(element);

            int count = children.Count;
            if (count == 0) 
            {
                return null;
            }

            Record result = new Record();
            foreach (XmlElement child in children) 
            {
                Trace.Assert(child.Name.Equals("s:key"));
                string key = child.GetAttribute("name");
                object value = this.ParseValue(child);
                if (value != null) 
                {
                    result.Add(key, value);
                }
            }
            return result;
        }

        /// <summary>
        /// Parses a &lt;list&gt; element and return a list object
        /// containing the parsed values.
        /// </summary>
        /// <param name="element">An XML element.</param>
        /// <returns>The list of parsed values.</returns>
        public List<object> ParseList(XmlElement element) 
        {
            Trace.Assert(element.Name.Equals("s:list"));
            if (element.FirstChild == null) 
            {
                return null;
            }

            List<XmlElement> children = GetChildElements(element);

            int count = children.Count;
            if (count == 0) 
            {
                return null;
            }

            List<object> result = new List<object>(count);
            foreach (XmlElement child in children) 
            {
                Trace.Assert(child.Name.Equals("s:item"));
                object value = this.ParseValue(child);
                if (value != null) 
                {
                    result.Add(value);
                }
            }
            return result;
        }

        /// <summary>
        /// Parses the value content of a dictionary/key or a list/item 
        /// element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <returns>Either the dictionary or list of values.</returns>
        /// <remarks>
        /// The value is either text, a dictionary element, or a list element.
        /// </remarks>
        public object ParseValue(XmlElement element) 
        {
            string name = element.Name;

            Trace.Assert(name.Equals("s:key") || name.Equals("s:item"));
            if (element.FirstChild == null) 
            {
                return null;
            }

            List<XmlElement> children = GetChildElements(element);

            int count = children.Count;

            // If no element children, then it must be a text value
            if (count == 0) 
            {
                return element.InnerText;
            }

            // If its not a text value, then expect a single child element.
            Trace.Assert(children.Count == 1);
            XmlElement child = children[0];

            name = child.Name;

            if (name.Equals("s:dict")) 
            {
                return this.ParseDict(child);
            }

            if (name.Equals("s:list")) 
            {
                return this.ParseList(child);
            }

            Trace.Assert(false); // Unreached
            return null;
        }
    }
}
