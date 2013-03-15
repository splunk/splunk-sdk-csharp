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
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Reads a results/event JSON stream one event at a time. 
    /// </summary>
    public class ResultsReaderJson : ResultsReader
    {
        /// <summary>
        /// Helper object which will only be constructed if the reader is handling
        /// JSON format used by export.
        /// </summary>
        private ExportHelper exportHelper;

        /// <summary>
        /// Whether the 'preview' flag is read.
        /// </summary>
        private bool previewFlagRead;
  
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderJson"/> class.
        /// class.
        /// </summary>
        /// <param name="stream">Json stream to be parsed</param>
        public ResultsReaderJson(Stream stream) :
             this(stream, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderJson"/> class.
        /// </summary>
        /// <param name="stream">The JSON stream to parse.</param>
        /// <param name="isInMultiReader">
        /// Whether or not is the underlying reader of a multi reader.
        /// </param>
        internal ResultsReaderJson(Stream stream, bool isInMultiReader) :
            base(stream, isInMultiReader)
        {
            StreamReader = new StreamReader(stream);
            if (this.IsExportStream || isInMultiReader)
            {
                this.exportHelper = new ExportHelper(this);
            }
            this.FinishInitialization();
        }

        /// <summary>
        /// Gets and sets the stream reader on the JSON stream to parse.
        /// </summary>
        internal StreamReader StreamReader { get; private set; }
     
        /// <summary>
        /// Gets or sets the JSON reader
        /// </summary>
        private JsonTextReader JsonReader
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating whether or not the results are
        /// a preview from an unfinished search.
        /// </summary>
        public override bool IsPreview
        {
            get
            {
                if (!this.previewFlagRead)
                {
                    throw new InvalidOperationException(
                        "isPreview() is not supported " +
                        "with a stream from a Splunk 4.x server by this class. " +
                        "Use the XML format and an XML result reader instead.");
                }

                return base.IsPreview;    
            }

            protected set
            {
                base.IsPreview = value;
            }
        }

        /// <summary>
        /// This method is not support.
        /// </summary>
        public override ICollection<string> Fields
        {
            get
            {
                throw new InvalidOperationException(
                    "Fields is not supported by this subclass.");
            }
        }

        /// <summary>
        /// Gets a value indicating whether or not the reader has been
        /// disposed.
        /// </summary>
        private bool IsDisposed
        {
            get { return this.StreamReader == null; }
        }

        /// <summary>
        /// Advance to the next set, skipping remaining event(s) 
        /// if any in the current set.
        /// </summary>
        /// <returns>Return false if the end is reached.</returns>
        internal override bool AdvanceStreamToNextSet()
        {
            return this.AdvanceIntoNextSetBeforeEvent();
        }

        /// <summary>
        /// Advance to the next set, skipping remaining event(s) 
        /// if any in the current set, read meta data before the 
        /// first event in the next result set.
        /// </summary>
        /// <returns>Return false if the end is reached.</returns>       
        internal bool AdvanceIntoNextSetBeforeEvent()
        {
            // If the end of stream has been reached, don't continue.
            if (this.IsDisposed)
            {
                return false;
            }

            // In Splunk 5.0 from the export endpoint,
            // each result is in its own top level object.
            // In Splunk 5.0 not from the export endpoint, the results are
            // an array at that object's key "results".
            // In Splunk 4.3, the
            // array was the top level returned. So if we find an object
            // at top level, we step into it until we find the right key,
            // then leave it in that state to iterate over.
            //
            // Json single-reader depends on 'isExport' flag to function.
            // It does not support a stream from a file saved from
            // a stream from an export endpoint.
            // Json multi-reader assumes export format thus does not support
            // a stream from none export endpoints.
            if (this.exportHelper != null)
            {
                /*
                    * We're on Splunk 5 with a single-reader not from
                    * an export endpoint
                    * Below is an example of an input stream.
                    *      {"preview":true,"offset":0,"lastrow":true,"result":{"host":"Andy-PC","count":"62"}}
                    *      {"preview":true,"offset":0,"result":{"host":"Andy-PC","count":"1682"}}
                    */

                // Read into first result object of the cachedElement set.
                while (true)
                {
                    bool endPassed = this.exportHelper.LastRow;
                    this.exportHelper.SkipRestOfRow();
                    if (!this.exportHelper.ReadIntoRow())
                    {
                        return false;
                    }
                    if (endPassed)
                    {
                        break;
                    }
                }
                return true;
            }

            // Introduced in Splunk 5.0, the format of the JSON object 
            // changed. Prior to 5.0, the array of events were a top level 
            // JSON element. In 5.0, the results are ain an array under the
            // key "results".
            // Note: reading causes the side effect of setting the JSON node
            // information. 
            this.JsonReader = new JsonTextReader(StreamReader);
            this.JsonReader.Read();
            if (this.JsonReader.TokenType.Equals(JsonToken.StartObject))
            {
                /*
                    * We're on Splunk 5 with a single-reader not from
                    * an export endpoint
                    * Below is an example of an input stream.
                    *     {"preview":false,"init_offset":0,"messages":[{"type":"DEBUG","text":"base lispy: [ AND index::_internal ]"},{"type":"DEBUG","text":"search context: user=\"admin\", app=\"search\", bs-pathname=\"/Users/fross/splunks/splunk-5.0/etc\""}],"results":[{"sum(kb)":"14372242.758775","series":"twitter"},{"sum(kb)":"267802.333926","series":"splunkd"},{"sum(kb)":"5979.036338","series":"splunkd_access"}]}
                    */
                while (true)
                {
                    if (!this.JsonReader.Read())
                    {
                        this.Dispose();
                        return false;
                    }

                    if (this
                        .JsonReader
                        .TokenType
                        .Equals(JsonToken.PropertyName))
                    {
                        if (this.JsonReader.Value.Equals("preview"))
                        {
                            this.ReadPreviewFlag();
                        }

                        if (this.JsonReader.Value.Equals("results"))
                        {
                            this.JsonReader.Read();
                            return true;
                        }
                    }
                }
            }
            else
            {
                /* Pre Splunk 5.0
                    * Below is an example of an input stream
                    *   [
                    *       {
                    *           "sum(kb)":"14372242.758775",
                    *               "series":"twitter"
                    *       },
                    *       {
                    *           "sum(kb)":"267802.333926",
                    *               "series":"splunkd"
                    *       },
                    *       {
                    *           "sum(kb)":"5979.036338",
                    *               "series":"splunkd_access"
                    *       }
                    *   ]
                    */
                return true;
            }
        }

        /// <summary>
        /// Release unmanaged resources
        /// </summary>
        public override void Dispose()
        {
            if (this.IsDisposed)
            {
                return;
            }

            // The Json reader is created after
            // constructor so the property could be
            // null.
            if (this.JsonReader != null)
            {
                ((IDisposable)this.JsonReader).Dispose();              
            }

            if (this.exportHelper != null)
            {
                this.exportHelper.Dispose();
            }

            this.StreamReader.Close();
          
            // Marking this reader as disposed.
            this.StreamReader = null;
        }

        /// <summary>
        /// Gets the enumerator for data returned from Splunk.
        /// </summary>
        /// <returns>A enumerator</returns>
        internal override IEnumerable<Event> GetEventsFromCurrentSet()
        {
            while (true)
            {
                if (this.IsDisposed)
                {
                    yield break;
                }
                
                if (this.exportHelper != null)
                {
                    // If the last row has been passed and 
                    // AdvanceStreamToNextSet
                    // has not been called, end the current set.
                    if (this.exportHelper.LastRow && !this.exportHelper.InRow)
                    {
                        yield break;
                    }
                    this.exportHelper.ReadIntoRow();
                }

                var returnData = this.ReadEvent();

                if (this.exportHelper != null)
                {
                    this.exportHelper.SkipRestOfRow();
                } 
                
                if (returnData == null)
                {
                    // End the result reader. This is needed for Splunk 4.x
                    this.Dispose();
                    yield break;
                }
               
                yield return returnData;
            }
        }

        /// <summary>
        /// Read an event from the JSON reader.
        /// </summary>
        /// <returns>
        /// The event. Null indicatiing end of stream, 
        /// which is used by none --export cases.
        /// </returns>
        private Event ReadEvent()
        {
            string name = null;
            Event returnData = null;

            // Events are almost flat, so no need for a true general parser 
            // solution.
            while (this.JsonReader.Read())
            {
                if (returnData == null)
                {
                    returnData = new Event();
                }

                if (this.JsonReader.TokenType.Equals(JsonToken.StartObject))
                {
                    // skip
                }
                else if (this.JsonReader.TokenType.Equals(JsonToken.StartArray))
                {
                    var data = new List<string>();
                    while (this.JsonReader.Read())
                    {
                        if (this
                            .JsonReader
                            .TokenType
                            .Equals(JsonToken.EndArray))
                        {
                            break;
                        }

                        if (this
                            .JsonReader
                            .TokenType
                            .Equals(JsonToken.PropertyName))
                        {
                            data.Add((string)this.JsonReader.Value);
                        }
                    }

                    Debug.Assert(name != null, "Event field name is not set."); 
                    returnData.Add(name, new Event.FieldValue(data.ToArray()));
                }
                else if (this
                    .JsonReader
                    .TokenType
                    .Equals(JsonToken.PropertyName))
                {
                    name = (string)this.JsonReader.Value;
                }
                else if (this.JsonReader.TokenType.Equals(JsonToken.String))
                {
                    Debug.Assert(name != null, "Event field name is not set.");
                    returnData.Add(name, new Event.FieldValue((string)this.JsonReader.Value));
                }
                else if (this.JsonReader.TokenType.Equals(JsonToken.EndObject))
                {
                    break;
                }
                else if (this.JsonReader.TokenType.Equals(JsonToken.EndArray))
                {
                    return null; // this is the end of the event set.
                }
            }
            return returnData;
        }

        /// <summary>
        /// Pead the preview flag value from the stream.
        /// </summary>
        private void ReadPreviewFlag()
        {
            this.JsonReader.Read();
            this.IsPreview = (bool)this.JsonReader.Value;
            this.previewFlagRead = true;
        }

        /// <summary>
        /// Contains code only used for streams from the export endpoint.
        /// </summary>
        private class ExportHelper : IDisposable
        {
            /// <summary>
            /// The JSON reader
            /// </summary>
            private readonly ResultsReaderJson resultsReader;

            /// <summary>
            /// The row being read.
            /// </summary>
            private StringReader currentRow;
            
            /// <summary>
            /// Initializes a new instance of the <see cref="ExportHelper" /> class.
            /// </summary>
            /// <param name="resultsReader">The result reader that is using this helper.</param>
            public ExportHelper(ResultsReaderJson resultsReader) 
            {
                this.resultsReader = resultsReader;
                // Initial value must be true so that 
                // the first row is treated as the start of a new set.
                this.LastRow = true;
            }

            /// <summary>
            /// Gets or sets the JSON reader which is also used by
            /// the result reader itself.
            /// </summary>
            private JsonTextReader JsonReader
            {
                get { return this.resultsReader.JsonReader; }
                set { this.resultsReader.JsonReader = value; }
            }

            /// <summary>
            /// Gets a value indicating whether or not the row 
            /// is the last in the current set.
            /// </summary>
            internal bool LastRow { get; private set; }

            /// <summary>
            /// Gets a value indicating whether or not the reader is in the middle or a row.
            /// </summary>
            public bool InRow { get; private set; }

            /// <summary>
            /// Read meta data in the current row before event data.
            /// </summary>
            /// <returns>Return false if end of stream is encountered.</returns>
            public bool ReadIntoRow()
            {
                if (this.InRow)
                {
                    return true;
                }

                //if (this.jsonReader.TokenType == JsonToken.end doc)
                //    return false;
                this.InRow = true;

                var line = this.resultsReader.StreamReader.ReadLine();

                if (line == null)
                {
                    this.resultsReader.Dispose();
                    return false;
                }

                var stringReader = new StringReader(line);

                this.currentRow = stringReader;
                this.JsonReader = new JsonTextReader(this.currentRow);

                this.JsonReader.Read();
                if (this.JsonReader.TokenType == JsonToken.StartArray)
                {
                    throw new InvalidOperationException(
                        "A stream from an export endpoint of " +
                        "a Splunk 4.x server in the JSON output format " +
                        "is not supported by this class. " +
                        "Use the XML search output format, " +
                        "and an XML result reader instead.");
                }
    
                // lastrow name and value pair does not appear if the row
                // is not the last in the set.
                this.LastRow = false;
                while (this.JsonReader.Read())
                {
                    if (this
                        .JsonReader
                        .TokenType
                        .Equals(JsonToken.PropertyName))
                    {
                        var name = (string)this.JsonReader.Value;
                        if (name == "preview")
                        {
                            this.resultsReader.ReadPreviewFlag();
                        }
                        else if (name == "lastrow")
                        {
                            this.JsonReader.Read();
                            this.LastRow = (bool)this.JsonReader.Value;
                        }
                        else if (name == "result")
                        {
                            return true;
                        }
                        else
                        {
                            this.JsonReader.Skip();
                        }
                    }
                }
                return false;
            }

            /// <summary>
            /// Skip the rest of the current row.
            /// </summary>
            public void SkipRestOfRow()
            {
                if (!this.InRow)
                {
                    return;
                }
                this.InRow = false;
                ((IDisposable)this.JsonReader).Dispose();
                this.currentRow.Dispose();
            }

            /// <summary>
            /// Release resources including unmanaged ones.
            ///  </summary>
            public void Dispose()
            {
                ((IDisposable)this.JsonReader).Dispose();
                this.currentRow.Dispose();
            }
        }
    }
}
