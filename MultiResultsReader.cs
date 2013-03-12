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

    /// <summary>
    /// Base class for multi result readers
    /// </summary>
    /// <typeparam name="T">
    /// Matching type of a single reader which will be passed 
    /// into the constructor.
    /// </typeparam>
    public class MultiResultsReader<T> : 
        IEnumerable<ISearchResults>, 
        IDisposable
        // Using <T extends ResultsReader> is to allow specialization of T in
        // subclasses of MultiResultReader. For example, MultiResultsReaderXml
        // references ResultsReaderXml.
        where T : ResultsReader, IDisposable
    {
        /// <summary>
        /// The underlying single reader 
        /// </summary>
        private readonly T resultsReader;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiResultsReader{T}" /> class.
        /// This is a base contructor that should not be used directly.
        /// </summary>
        /// <param name="resultsReader">
        /// The underlying single reader
        /// </param>
        internal MultiResultsReader(T resultsReader) 
        {
            this.resultsReader = resultsReader;
        }

        /// <summary>
        /// Returns an enumerator over the sets of results from this reader.
        /// </summary>
        /// <returns>An enumerator of events</returns>
        public IEnumerator<ISearchResults> GetEnumerator()
        {
            while (this.resultsReader.AdvanceStreamToNextSet())
            {
                yield return this.resultsReader;
            } 
        }

        /// <summary>
        /// Returns an enumerator over the sets of results from this reader.
        /// </summary>
        /// <returns>An enumerator of events</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Release unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            this.resultsReader.Dispose();
        }
    }
}