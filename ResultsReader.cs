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
    using System.Diagnostics;
    using System.IO;

    public interface ISearchResults : IEnumerable<Event>
    {
        /// <summary>
        /// Gets or sets a value indicating whether or not the results  in this reader 
        /// a preview from an unfinished search.
        /// </summary>
        bool IsPreview
        {
            get;
        }

        /// <summary>
        /// Gets or sets all the fields that may appear in each result.
        /// </summary>
        /// <remarks>
        /// Note that any given result will contain a subset of these fields.
        /// </remarks>
        IEnumerable<string> Fields
        {
            get;
        }
    }

    /// <summary>
    /// The abstract class results reader to return events from a stream
    /// in key/value pairs.
    /// </summary>
    public abstract class ResultsReader<T> : ISearchResults, IDisposable
    {        
        private bool used;

        /// <summary>
        /// Gets or sets a value indicating whether or not the results  in this reader 
        /// a preview from an unfinished search.
        /// </summary>
        public bool IsPreview
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not there are results
        /// to be read.
        /// </summary>
        internal bool HasResults
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets all the fields that may appear in each result.
        /// </summary>
        /// <remarks>
        /// Note that any given result will contain a subset of these fields.
        /// </remarks>
        public IEnumerable<string> Fields
        {
            get;
            protected set;
        }

        public abstract void Initialize(Stream stream);

        /// <summary>
        /// Releasingresetting unmanaged resources.        
        /// </summary>
        public abstract void Dispose();
    
        /// <summary>
        /// Returns an enumerator over the events in the event stream.
        /// </summary>
        /// <returns>An enumerator of events</returns>
        public IEnumerator<Event> GetEnumerator()
        {
            if (used)
            {
                throw new InvalidOperationException(
                    "All results in this reader have already been read. " +
                    "Use MultiResultsReader to read multiple sets of results " +
                    "including one or more previews, or final results"); 
            }

            used = true;

            // Either the public constructor should have failed or
            // the MultiResultsReader should not have returned
            // this reader to the caller.
            Debug.Assert(
                this.HasResults,
                "This reader has no results and should not be used.");
            
            return this.GetEnumeratorInner();
        }

        /// <summary>
        /// Returns an enumerator over the events in the event stream.
        /// </summary>
        /// <returns>An enumerator of events</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public abstract IEnumerator<Event> GetEnumeratorInner();
       
        internal virtual void TakeOver(T reader)
        {
            //reader.Dispose();
        }
    }
}
