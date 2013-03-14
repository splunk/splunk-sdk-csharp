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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The Upload class represents an active file uploading to Splunk.
    /// </summary>
    public class Upload : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Upload"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path8</param>
        public Upload(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the number of bytes that are currently indexed.
        /// </summary>
        public int BytesIndexed
        {
            get
            {
                return this.GetInteger("Bytes Indexed");
            }
        }

        /// <summary>
        /// Gets the current offset
        /// </summary>
        public int Offset
        {
            get
            {
                return this.GetInteger("Offset");
            }
        }

        /// <summary>
        /// Gets the number of sources that are indexed.
        /// </summary>
        public int SourcesIndexed
        {
            get
            {
                return this.GetInteger("Sources Indexed");
            }
        }

        /// <summary>
        /// Gets the start time of the upload that is being indexed.
        /// </summary>
        /// <returns>The index start time for this upload.</returns>
        public DateTime SpoolTime
        {
            get
            {
                return this.GetDate("Spool Time", DateTime.MinValue);
            }
        }

        /// <summary>
        /// Gets the current size.
        /// </summary>
        public int UploadSize
        {
            get
            {
                return this.GetInteger("Size");
            }
        }
    }
}
