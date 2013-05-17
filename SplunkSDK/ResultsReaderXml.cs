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
    using System.Text;
    using System.Xml;

    /// <summary>
    /// The <see cref="ResultsReaderXml"/> class represents a streaming XML 
    /// reader for Splunk search results. When a stream from an export search
    /// is passed to this reader, it skips any preview events in the stream. 
    /// If you want to access the preview events, use the  
    /// <see cref="MultiResultsReaderXml"/> class.
    /// </summary>
    public class ResultsReaderXml : ResultsReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderXml"/> 
        /// class for the event stream. You should only attempt to parse an XML
        /// stream with the XML reader. 
        /// </summary>
        /// <param name="stream">The stream to parse.</param>
        public ResultsReaderXml(Stream stream) :
            this(stream, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderXml"/>
        /// class.
        /// </summary>
        /// <param name="stream">The XML stream to parse.</param>
        /// <param name="isInMultiReader">
        /// Whether the reader is the underlying reader of a multi
        /// reader.
        /// </param>
        internal ResultsReaderXml(Stream stream, bool isInMultiReader) :
            base(stream, isInMultiReader)
        {
            var setting = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
            };

            this.XmlReader = XmlReader.Create(stream, setting);

            this.FinishInitialization();
        }

        /// <summary>
        /// Gets the underlying reader of the XML stream.
        /// </summary>
        internal XmlReader XmlReader
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Advances to the next set, skipping any remaining event(s) 
        /// in the current set. Reads metadata before the first event in the
        /// next result set.
        /// </summary>
        /// <returns>Returns false if the end is reached.</returns>     
        internal override bool AdvanceStreamToNextSet()
        {
            return this.ReadIntoNextResultsElement();
        }

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        public override void Dispose()
        {
            ((IDisposable)this.XmlReader).Dispose();
        }

        /// <summary>
        /// Reads to next <b>results</b> element, parses out 
        /// <see cref="IsPreview"/> and <see cref="Fields"/>,
        /// and updates <see cref="HasResults"/> flag.
        /// </summary>
        /// <returns>Returns false if the end is reached.</returns>     
        private bool ReadIntoNextResultsElement()
        {
            // Below is an example of an input stream, with a single 'results'
            // element. With a stream from an export point, there can be
            // multiple ones.
            //
            //        <?xml version='1.0' encoding='UTF-8'?>
            //        <results preview='0'>
            //        <meta>
            //        <fieldOrder>
            //        <field>series</field>
            //        <field>sum(kb)</field>
            //        </fieldOrder>
            //        </meta>
            //        <messages>
            //        <msg type='DEBUG'>base lispy: [ AND ]</msg>
            //        <msg type='DEBUG'>search context: user='admin', app='search', bs-pathname='/some/path'</msg>
            //        </messages>
            //        <result offset='0'>
            //        <field k='series'>
            //        <value><text>twitter</text></value>
            //        </field>
            //        <field k='sum(kb)'>
            //        <value><text>14372242.758775</text></value>
            //        </field>
            //        </result>
            //        <result offset='1'>
            //        <field k='series'>
            //        <value><text>splunkd</text></value>
            //        </field>
            //        <field k='sum(kb)'>
            //        <value><text>267802.333926</text></value>
            //        </field>
            //        </result>
            //        </results>
            if (this.XmlReader.ReadToFollowing("results"))
            {
                this.IsPreview = XmlConvert.ToBoolean(this.XmlReader["preview"]);

                this.ReadMetaElement();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads the <b>meta</b> element to populate the <see cref="Fields"/>
        /// property, and moves to its end tag.
        /// </summary>
        private void ReadMetaElement()
        {
            if (this.XmlReader.ReadToDescendant("meta"))
            {
                if (this.XmlReader.ReadToDescendant("fieldOrder"))
                {
                    this.ReadEachDescendant(
                        "field",
                        () =>
                        {
                            this.Fields.Add(this.XmlReader.ReadElementContentAsString());
                        });

                    this.XmlReader.Skip();
                }

                this.XmlReader.Skip();
            }
        }

        /// <summary>
        /// Reads each descendant found and positions the reader on the end
        /// tag of the current node.
        /// </summary>
        /// <param name="name">Name of the descendant.</param>
        /// <param name="readAction">
        /// The action that reads each descendant found, and 
        /// positions the reader at the decendant's element depth
        /// (for instance, the end tag or the start tag).
        /// </param>
        private void ReadEachDescendant(string name, Action readAction)
        {
            if (this.XmlReader.ReadToDescendant(name))
            {
                readAction();

                while (this.XmlReader.ReadToNextSibling(name))
                {
                    readAction();
                }
            }
        }

        /// <summary>
        /// Returns an enumerator over a set of the events 
        /// in the event stream, and gets ready for the next set.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using 'search/jobs/export endpoint', search results
        /// will be streamed back as they become available. It is possible
        /// for one or more previews to be received before the final one.
        /// The enumerator returned will be over a single preview or 
        /// the final results. Each time this method is called, 
        /// the next preview or the final results are enumerated if they are
        /// available; otherwise, an exception is thrown.
        /// </para>
        /// <para>
        /// After all events in the set is enumerated, the metadata of the 
        /// next set (if available) is read, with <see cref="IsPreview"/> 
        /// and <see cref="Fields"/> being set accordingly.
        /// </para>
        /// </remarks>
        /// <returns>A enumerator.</returns>
        internal override IEnumerable<Event> GetEventsFromCurrentSet()
        {
            while (true)
            {
                if (!this.XmlReader.ReadToNextSibling("result"))
                {
                    yield break;
                }

                var result = new Event();

                this.ReadEachDescendant(
                    "field", 
                    () => 
                    {
                        var key = this.XmlReader["k"];

                        if (key == null)
                        {
                            throw new XmlException(
                                "'field' attribute 'k' not found");
                        }

                        var values = new List<string>();

                        var xmlDepthField = this.XmlReader.Depth;

                        while (this.XmlReader.Read())
                        {
                            if (this.XmlReader.Depth == xmlDepthField)
                            {
                                break;
                            }

                            Debug.Assert(
                                XmlReader.Depth > xmlDepthField,
                                "The loop should have exited earlier.");

                            if (this.XmlReader.IsStartElement("value"))
                            {
                                if (this.XmlReader.ReadToDescendant("text"))
                                {
                                    values.Add(
                                        this.XmlReader.ReadElementContentAsString());
                                }
                            }
                            else if (this.XmlReader.IsStartElement("v"))
                            {
                                result.SegmentedRaw = this.XmlReader.ReadOuterXml();
                                var value = ReadTextContentFromXml(
                                    result.SegmentedRaw);                    
                                values.Add(value);
                            }
                        }

                        result.Add(key, new Event.FieldValue(values.ToArray()));
                    });

                yield return result;
            }
        }

        /// <summary>
        /// Extracts and concatenate text, excluding any markup.
        /// </summary>
        /// <param name="xml">The XML fragment with markup.</param>
        /// <returns>Extracted and concatenated text.</returns>
        private static string ReadTextContentFromXml(string xml)
        {
            var ret = new StringBuilder();

            var stringReader = new StringReader(xml);

            var setting = new XmlReaderSettings
                {
                    ConformanceLevel = ConformanceLevel.Fragment,
                };

            var xmlReader = XmlReader.Create(stringReader, setting);

            while (xmlReader.Read())
            {
                ret.Append(xmlReader.ReadString());
            }

            return ret.ToString();
        }
    }
}
