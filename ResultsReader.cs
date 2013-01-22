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
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// The abstract class results reader to return events from a stream
    /// in key/value pairs.
    /// </summary>
    public abstract class ResultsReader : IEnumerable<Dictionary<string, object>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResultsReader"/> class.
        /// </summary>
        /// <param name="inputStream">The stream</param>
        public ResultsReader(Stream inputStream) 
        {
            this.StreamHandle = inputStream;
        }

        /// <summary>
        /// Gets or sets the stream handle
        /// </summary>
        protected Stream StreamHandle
        {
            get;
            set;
        }

        /// <summary>
        /// Closes the stream and clears the handle.
        /// </summary>
        public virtual void Close() 
        {
            if (this.StreamHandle != null)
            {
                this.StreamHandle.Close();
            }
            this.StreamHandle = null;
        }

        /// <summary>
        /// Gets the enumerator for data returned from Splunk.
        /// </summary>
        /// <returns>A enumertor</returns>
        public abstract IEnumerator<Dictionary<string, object>> GetEnumerator();

        /// <summary>
        /// Gets the next event, or returns null when no more events are
        /// present in the stream
        /// </summary>
        /// <returns>A enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
