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
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ResultsReaderJSON : ResultsReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReaderJSON"/> class.
        /// </summary>
        /// <param name="stream">Stream to be parsed</param>
        public ResultsReaderJSON(Stream stream) : base(stream) 
        {
            this.JsonReader = new JsonTextReader(new StreamReader(stream));

            // if stream is empty, return a null reader.
            try 
            {
                // Note: reading causes the side effect of setting the json node info
                if (!this.JsonReader.Read())
                {
                    this.JsonReader = null;
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
        /// Gets the next event, or returns null when no more events are
        /// present in the stream
        /// </summary>
        /// <returns>A dictionary of key/value pairs in the event</returns>
        public override Dictionary<string, string> GetNextEvent()
        {
            Dictionary<string, string> returnData = null;
            string name = string.Empty;

            if (this.JsonReader == null)
            {
                return null;
            }

            //Events are almost flat, so no need for a true general parser solution.
            while (this.JsonReader.Read())
            {
                if (returnData == null) 
                {
                    returnData = new Dictionary<string, string>();
                }

                if (this.JsonReader.TokenType.Equals(JsonToken.StartObject)) 
                {
                    // skip
                }
                else if (this.JsonReader.TokenType.Equals(JsonToken.StartArray)) 
                {
                    string data = string.Empty;
                    while (this.JsonReader.Read()) 
                    {
                        if (this.JsonReader.TokenType.Equals(JsonToken.EndArray)) 
                        {
                            break;
                        }

                        if (this.JsonReader.TokenType.Equals(JsonToken.PropertyName))
                        {
                            data = data + (data.Equals(string.Empty) ? string.Empty : ",") +
                                    this.JsonReader.Value;
                        }
                    }
                    returnData.Add(name, data);
                }
                else if (this.JsonReader.TokenType.Equals(JsonToken.PropertyName)) 
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
                    // skip 
                }
            }
            return returnData;
        }
    }
}
