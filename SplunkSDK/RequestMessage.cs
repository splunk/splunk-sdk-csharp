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

    /// <summary>
    /// The request Message is an abstraction of the HTTP/S web request message
    /// </summary>
    public class RequestMessage
    {
        /// <summary>
        /// The method, defaults to get.
        /// </summary>
        private string method = "GET";  // "GET" | "PUT" | "POST" | "DELETE"

        /// <summary>
        /// The header
        /// </summary>
        private Dictionary<string, string> header = null;

        /// <summary>
        /// The content
        /// </summary>
        private object content = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessage"/> 
        /// class.
        /// </summary>
        public RequestMessage() 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestMessage"/> 
        /// class,
        /// with specific method.
        /// </summary>
        /// <param name="method">The method</param>
        public RequestMessage(string method) 
        {
            this.method = method;
        }

        /// <summary>
        /// Gets the header dictionary.
        /// </summary>
        /// <returns>The header</returns>
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
        /// Gets or sets the HTTP/S method.
        /// </summary>
        /// <returns>The method</returns>
        public string Method
        {
            get
            {
                return this.method;
            }

            set
            {
                value = value.ToUpper();
                if (!this.CheckMethod(value))
                {
                    throw new Exception("Bad HTTP method");
                }
                this.method = value;
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <returns>The content</returns>
        public object Content
        {
            get
            {
                return this.content;
            }

            set
            {
                this.content = value;
            }
        }

        /// <summary>
        /// Checks if the method is supported.
        /// </summary>
        /// <param name="value">The method name</param>
        /// <returns>Whether or not the method is supported</returns>
        private bool CheckMethod(string value) 
        {
            return
                value.Equals("GET") ||
                value.Equals("PUT") ||
                value.Equals("POST") ||
                value.Equals("DELETE");
        }
    }
}
