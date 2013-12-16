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
    using System.IO;
    using System.Net;

    /// <summary>
    /// The <see cref="ResponseMessage"/> class represents the basic HTTP
    /// response object.
    /// </summary>
    public class ResponseMessage : IDisposable
    {
        /// <summary>
        /// The status.
        /// </summary>
        private int status;

        /// <summary>
        /// The header.
        /// </summary>
        private Dictionary<string, string> header = null;

        /// <summary>
        /// The content, a stream.
        /// </summary>
        private Stream content;

        /// <summary>
        /// The parent response. Needed for finalizing cleanup.
        /// </summary>
        private HttpWebResponse response;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseMessage"/> 
        /// class.
        /// </summary>
        public ResponseMessage()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseMessage"/> 
        /// class with an initial status.
        /// </summary>
        /// <param name="status">The status</param>
        public ResponseMessage(int status)
        {
            this.status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseMessage"/> 
        /// class with an initial status and stream.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="content">The content stream.</param>
        /// <param name="response">The response.</param>
        public ResponseMessage(
            int status, Stream content, HttpWebResponse response)
        {
            this.status = status;
            this.content = content;
            this.response = response;
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ResponseMessage"/> 
        /// class.
        /// </summary>
        ~ResponseMessage()
        {
            this.Dispose();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.response != null)
            {
                try
                {
                    this.response.Close();
                }
                catch (Exception)
                {
                    // ignore
                }
            }
        }

        /// <summary>
        /// Gets the body content stream.
        /// </summary>
        /// <returns>The stream.</returns>
        public Stream Content
        {
            get { return this.content; }
        }

        /// <summary>
        /// Gets the dictionary of the response headers.
        /// </summary>
        /// <returns>The response headers.</returns>
        public Dictionary<string, string> Header
        {
            get
            {
                if (this.header == null)
                {
                    this.header = new Dictionary<string, string>();
                }
                return this.header;
            }
        }

        /// <summary>
        /// Gets the response HTTP status code.
        /// </summary>
        /// <returns>The status.</returns>
        public int Status
        {
            get { return this.status; }
        }
    }
}
