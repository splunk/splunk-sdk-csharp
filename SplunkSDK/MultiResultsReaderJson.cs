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
    using System.IO;

    /// <summary>
    /// The <see cref="MultiResultsReaderJson" /> class represents a streaming
    /// JSON reader for Splunk search results. This reader supports streams
    /// from export searches, which might return one of more previews before
    /// returning final results.
    /// </summary>
    public class MultiResultsReaderJson : MultiResultsReader<ResultsReaderJson>
    {
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="MultiResultsReaderJson"/> class.
        /// </summary>
        /// <param name="stream">The JSON stream to parse.</param>
        public MultiResultsReaderJson(Stream stream)
            : base(new ResultsReaderJson(stream, true))
        {
        }
    }
}
