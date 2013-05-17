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
    using System.IO;
    using System.Xml;

    /// <summary>
    /// The <see cref="AtomFeed"/> class represents the Atom feed data.
    /// </summary>
    public class AtomFeed : AtomObject
    {
        /// <summary>
        /// Gets or sets the list of Atom entries contained in this  
        /// <see cref="AtomFeed"/> object.
        /// </summary>
        public List<AtomEntry> Entries 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="AtomFeed"/>'s <b>itemsPerPage</b> 
        /// element value.
        /// </summary>
        public string ItemsPerPage 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="AtomFeed"/>'s <b>startIndex</b> 
        /// element value.
        /// </summary>
        public string StartIndex 
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <see cref="AtomFeed"/>'s <b>totalResults</b> element 
        /// value.
        /// </summary>
        public string TotalResults 
        {
            get;
            set;
        }

        /// <summary>
        /// Creates a new <see cref="AtomFeed"/> instance.
        /// </summary>
        /// <returns>An empty AtomFeed object</returns>
        public static AtomFeed Create() 
        {
            AtomFeed atomFeed = new AtomFeed();
            atomFeed.Links = new Dictionary<string, string>();
            atomFeed.Entries = new List<AtomEntry>();
            return atomFeed;
        }

        /// <summary>
        /// Creates a new <see cref="AtomFeed"/> instance based on the given 
        /// stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <returns>The AtomFeed.</returns>
        public static AtomFeed Parse(Stream input) 
        {
            XmlElement root = Xml.Parse(input).DocumentElement;
            string rname = root.Name;
            if (!rname.Equals("feed") && 
                !root.NamespaceURI.Equals("http://www.w3.org/2005/Atom")) 
            {
                throw new Exception("Unrecognized XML format");
            }
            return AtomFeed.Parse(root);
        }

        /// <summary>
        /// Creates a new <see cref="AtomFeed"/> based on a given XML element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        /// <returns>The AtomFeed.</returns>
        private static AtomFeed Parse(XmlElement element) 
        {
            AtomFeed feed = AtomFeed.Create();
            feed.Load(element);
            return feed;
        }

        /// <summary>
        /// Initializes the current <see cref="AtomFeed"/> instance from a 
        /// given XML element.
        /// </summary>
        /// <param name="element">The XML element.</param>
        internal override void Init(XmlElement element) 
        {
            string name = element.Name;
            if (name.Equals("entry")) 
            {
                AtomEntry entry = AtomEntry.Parse(element);
                this.Entries.Add(entry);
            }
            else if (name.Equals("s:messages")) 
            {
                // Ignore
            }
            else if (name.Equals("opensearch:totalResults")) 
            {
                this.TotalResults = element.InnerText;
            }
            else if (name.Equals("opensearch:itemsPerPage")) 
            {
                this.ItemsPerPage = element.InnerText;
            }
            else if (name.Equals("opensearch:startIndex")) 
            {
                this.StartIndex = element.InnerText;
            }
            else 
            {
                base.Init(element);
            }
        }
    }
}
