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
    public class ResultsReaderXml : ResultsReader<ResultsReaderXml>
    {
        /// <summary>
        /// Underlying reader of the XML stream
        /// </summary>
        internal XmlReader XmlReader
        {
            get;
            private set;
        }

        public ResultsReaderXml()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderXml" /> 
        /// class for the event stream. You should only
        /// attempt to parse an XML stream with the XML reader. 
        /// </summary>
        /// <param name="stream">The stream to parse.</param>
        public ResultsReaderXml(Stream stream)
        {
            Initialize(stream);
        }

        public override void Initialize(Stream stream)
        {
            var setting = new XmlReaderSettings
            {
                ConformanceLevel = ConformanceLevel.Fragment,
            };

            this.XmlReader = XmlReader.Create(stream, setting);

            this.ReadIntoNextResultsElement();

            // Fail if there's no 'results' element found
            this.Validate();
        }

        internal override void TakeOver(ResultsReaderXml reader)
        {
            var resultsXmlReader = reader;
            this.XmlReader = resultsXmlReader.XmlReader;
            resultsXmlReader.XmlReader = null;
            this.ReadIntoNextResultsElement();
            base.TakeOver(reader);
        }

        public override void Dispose()
        {
            if (this.XmlReader != null)
            {
                ((IDisposable) this.XmlReader).Dispose();
            }

            this.XmlReader = null;
        }

        /// <summary>
        /// Read to next 'results' element, parse out 
        /// <see cref="IsPreview"/> and <see cref="Fields"/>,
        /// and update <see cref="HasResults"/> flag.
        /// </summary>
        private void ReadIntoNextResultsElement()
        {
            if (this.XmlReader.ReadToFollowing("results"))
            {
                this.IsPreview = XmlConvert.ToBoolean(this.XmlReader["preview"]);

                this.ReadMetaElement();
                this.HasResults = true;
            }
        }

        /// <summary>
        /// Read 'meta' element to populate Fields property, 
        /// and move to its end tag.
        /// </summary>
        private void ReadMetaElement()
        {
            if (this.XmlReader.ReadToDescendant("meta"))
            {
                if (this.XmlReader.ReadToDescendant("fieldOrder"))
                {
                    var fields = new List<string>();

                    this.ReadEachDescendant(
                        "field",
                        () =>
                        {
                            fields.Add(this.XmlReader.ReadElementContentAsString());
                        });

                    this.Fields = fields;

                    this.XmlReader.Skip();
                }

                this.XmlReader.Skip();
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
        public override IEnumerator<Event> GetEnumeratorInner()
        {
            while (true)
            {
                // MultiResultsReader might have transfered
                // the underlying reader to the next ResultsReader.
                if (this.XmlReader == null)
                {
                    throw new InvalidOperationException(
                        "The reader is no longer valid.");
                }

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
                                values.Add(this.XmlReader.ReadInnerXml());
                            }
                        }

                        result.Add(key, new Event.FieldValue(values.ToArray()));
                    });

                yield return result;
            }
        }

        private void Validate()
        {
            if (this.XmlReader == null)
            {
                throw new InvalidOperationException(
                    "The reader is no longer valid.");
            }

            EnsureResultsAvailable();
        }

        private void EnsureResultsAvailable()
        {
            if (!this.HasResults)
            {
                throw new InvalidOperationException(
                     "No or no more 'results' element found.");
            }
        }
    }
}
