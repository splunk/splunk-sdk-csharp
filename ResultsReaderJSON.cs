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
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Reads a results/event JSOMN stream one event at a time. 
    /// </summary>
    public class ResultsReaderJson : ResultsReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderJson"/> 
        /// class.
        /// </summary>
        /// <param name="stream">Stream to be parsed</param>
        public ResultsReaderJson(Stream stream) : base(stream) 
        {
            this.JsonReader = new JsonTextReader(new StreamReader(stream));

            // if stream is empty, return a null reader.
            try 
            {
                // Note: reading causes the side effect of setting the json node
                // information. 
                if (!this.JsonReader.Read())
                {
                    this.JsonReader = null;
                    return;
                }
                
                // Introduced in Splunk 5.0, the format of the JSON object 
                // changed. Prior to 5.0, the array of events were a top level 
                // JSON element. In 5.0, the results are ain an array under the
                // key "results".
                if (this.JsonReader.TokenType.Equals(JsonToken.StartObject))
                {
                    while (true)
                    {
                        if (!this.JsonReader.Read())
                        {
                            this.JsonReader = null;
                            return;
                        }

                        if (this
                            .JsonReader
                            .TokenType
                            .Equals(JsonToken.PropertyName))
                        {
                            if (this.JsonReader.Value.Equals("results")) 
                            {
                                this.JsonReader.Read();
                                return;
                            }
                        }
                    }
                }
                else
                {
                    // Pre Splunk 5.0
                }
            }
            catch (Exception) 
            {
                this.JsonReader = null;
                return;
            }
        }

        /// <summary>
        /// Gets or sets the json reader
        /// </summary>
        private JsonTextReader JsonReader 
        {
            get; 
            set;
        }

        /// <summary>
        /// Closes the reader
        /// </summary>
        public override void Close()
        {
            if (this.JsonReader != null)
            {
                this.JsonReader.Close();
            }

            this.JsonReader = null;
        }

        /// <summary>
        /// Gets the enumerator for data returned from Splunk.
        /// </summary>
        /// <returns>A enumerator</returns>
        public override IEnumerator<Dictionary<string, object>> GetEnumerator()
        {
            Dictionary<string, object> returnData = null;
            string name = string.Empty;

            if (this.JsonReader == null)
            {
                yield break;
            }

            while (true)
            {
                // Events are almost flat, so no need for a true general parser 
                // solution.
                while (this.JsonReader.Read())
                {
                    if (returnData == null)
                    {
                        returnData = new Dictionary<string, object>();
                    }

                    if (this.JsonReader.TokenType.Equals(JsonToken.StartObject))
                    {
                        // skip
                    }
                    else if (this.JsonReader.TokenType.Equals(JsonToken.StartArray))
                    {
                        List<string> data = new List<string>();
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

                        returnData.Add(name, data.ToArray());
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
                        returnData.Add(name, (string)this.JsonReader.Value);
                    }
                    else if (this.JsonReader.TokenType.Equals(JsonToken.EndObject))
                    {
                        break;
                    }
                    else if (this.JsonReader.TokenType.Equals(JsonToken.EndArray))
                    {
                        yield break; // this is the end of the event set.
                    }
                }
                yield return returnData;
            }
        }
    }
}
