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
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Represents a streaming XML reader for 
    /// Splunk search results.
    /// </summary>
    public class ResultsReaderXml : ResultsReader
    {
        /// <summary>
        /// Underlying reader of the XML stream
        /// </summary>
        private XmlReader reader;

        /// <summary>
        /// Whether or not there are more 'results' element to read.
        /// </summary>
        private bool noMoreResults;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderXml" /> 
        /// class for the event stream. You should only
        /// attempt to parse an XML stream with the XML reader. 
        /// Unpredictable results may occur if you use a non-XML stream.
         /// </summary>
        /// <param name="stream">The stream to parse.</param>
        public ResultsReaderXml(Stream stream) : base(stream)
        {
            var setting = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
            };

            this.reader = XmlReader.Create(stream, setting);

            this.ReadToNextResultsElement();                  
        }

        /// <summary>
        /// Read to next 'results' element, parse out 
        /// <see cref="IsPreview"/> and <see cref="Fields"/>,
        /// and update <see cref="noMoreResults"/> flag.
        /// </summary>
        private void ReadToNextResultsElement()
        {
            if (this.reader.ReadToFollowing("results"))
            {
                this.IsPreview = XmlConvert.ToBoolean(this.reader["preview"]);
                
                this.ReadMetaElement();
            }
            else
            {
                this.noMoreResults = true;
            }
        }

        /// <summary>
        /// Read 'meta' element to populate Fields property, 
        /// and move to its end tag.
        /// </summary>
        private void ReadMetaElement()
        {
            if (this.reader.ReadToDescendant("meta"))
            {
                if (this.reader.ReadToDescendant("fieldOrder"))
                {
                    var fields = new List<string>();

                    this.ReadEachDescendant(
                        "field",
                        () =>
                        {
                            fields.Add(this.reader.ReadElementContentAsString());
                        });

                    this.Fields = fields;

                    this.reader.Skip();
                }

                this.reader.Skip();
            }
        }

        /// <summary>
        /// Read each descendant found, and position the reader on the end
        /// tag of the current node.
        /// </summary>
        /// <param name="name">Name of the descendant</param>
        /// <param name="readAction">
        /// The action that reads each descendant found, and 
        /// position the reader at the decendant's element depth
        /// (i.e. end tag or start tag).
        /// </param>
        private void ReadEachDescendant(string name, Action readAction)
        {
            if (this.reader.ReadToDescendant(name))
            {
                readAction();

                while (this.reader.ReadToNextSibling(name))
                {
                    readAction();
                }
            }
        }

        /// <summary>
        /// Returns an enumerator over a set of the events 
        /// in the event stream, and get ready for the next set.
        /// <remarks>
        /// <para>
        /// When using 'search/jobs/export endpoint', search results
        /// will be streamed back as they become available. It is possble
        /// for one or more previews to be received, before the final one.
        /// The enumerator returned will be over a single preview or 
        /// the final results. Each time this method is called, 
        /// the next preview or the final results will be enumerated if it is 
        /// available, otherwise, and an exception will be thrown.
        /// </para>
        /// <para>
        /// After all events in the set is enumerated, the metadata of the 
        /// next set (if available) is read, with <see cref="IsPreview"/> 
        /// and <see cref="Fields"/> being set accordingly.
        /// </para>
        /// </remarks>
        /// </summary>
        /// <returns>A enumerator</returns>
        public override IEnumerator<Event> GetEnumerator()
        {
            if (this.noMoreResults)
            {
                throw new XmlException(
                    "No more results found.");
            }
            
            while (this.reader.ReadToNextSibling("result"))
            {
                var result = new Event();

                this.ReadEachDescendant(
                    "field", 
                    () => 
                    {
                        var key = this.reader["k"];

                        if (key == null)
                        {
                            throw new XmlException(
                                "'field' attribute 'k' not found");
                        }

                        var values = new List<string>();

                        var xmlDepthField = this.reader.Depth;

                        while (this.reader.Read())
                        {
                            if (this.reader.Depth == xmlDepthField)
                            {
                                break;
                            }

                            Debug.Assert(
                                reader.Depth > xmlDepthField,
                                "The loop should have exited earlier.");

                            if (this.reader.IsStartElement("value"))
                            {
                                if (this.reader.ReadToDescendant("text"))
                                {
                                    values.Add(
                                        this.reader.ReadElementContentAsString());
                                }
                            }
                            else if (this.reader.IsStartElement("v"))
                            {
                                values.Add(this.reader.ReadInnerXml());
                            }
                        }

                        result.Add(key, new Event.FieldValue(values.ToArray()));
                    });

                yield return result;
            }

            this.ReadToNextResultsElement();
        }
    }
}
