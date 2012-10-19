/*
 * Copyright 2012 Splunk, Inc.
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
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Represents an Atom object.
    /// </summary>
    public class AtomObject
    {
        /// <summary>
        /// Gets or sets value of the Atom id element.
        /// </summary>
        public string Id 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the Atom link elements 
        /// </summary>
        public Dictionary<string, string> Links 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the Atom title element
        /// </summary>
        public string Title 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value of the Atom updated element.
        /// </summary>
        public string Updated 
        {
            get;
            set;
        }

        /// <summary>
        /// Initializes a particular property of the current instance based
        /// on the given XML.
        /// </summary>
        /// <param name="element">The XML element</param>
        public virtual void Init(XmlElement element) 
        {
            string name = element.Name;
            if (name.Equals("id")) 
            {
                this.Id = element.InnerText;
            }
            else if (name.Equals("link")) 
            {
                string rel = element.GetAttribute("rel");
                string href = element.GetAttribute("href");
                this.Links.Add(rel, href);
            }
            else if (name.Equals("title")) 
            {
                this.Title = element.InnerText;
            }
            else if (name.Equals("updated")) 
            {
                this.Updated = element.InnerText;
            }
            else if (name.Equals("author") || name.Equals("generator")) 
            {
                // Ignore
            }
            else 
            {
                // Ignore
            }
        }

        /// <summary>
        /// Initializes the current AtomObect instance from the given XML 
        /// element by invoking init() on each child of the XML element.
        /// </summary>
        /// <param name="element">The XML element</param>
        public void Load(XmlElement element) 
        {
            foreach (XmlNode child in element.ChildNodes) 
            {
                if (child.NodeType != XmlNodeType.Element) 
                {
                    continue;
                }
                this.Init((XmlElement)child);
            }
        }
    }
}
