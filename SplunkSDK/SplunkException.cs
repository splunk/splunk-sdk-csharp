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
    using System.Runtime.Serialization;

    /// <summary>
    /// The <see cref="SplunkException"/> class represents a Splunk exception.
    /// Exceptions are thrown for Splunk responses that return an error status 
    /// code.
    /// </summary>
    public class SplunkException : Exception, ISerializable
    {
        /// <summary>
        /// Indicates a job has been submitted to Splunk, but has not
        /// yet been scheduled to run, so there is no job information 
        /// available.
        /// </summary>
        public static int JOBNOTREADY = 1;

        /// <summary>
        /// Indicates a timed operation has reached its timeout value.
        /// </summary>
        public static int TIMEOUT = 2;

        /// <summary>
        /// Indicates an operation was requested on an object that is 
        /// ambiguously defined due to namespace rules. 
        /// </summary>
        public static int AMBIGUOUS = 3;

        /// <summary>
        /// Initializes a new instance of the <see cref="SplunkException"/> 
        /// class.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="text">The text.</param>
        public SplunkException(int code, string text) 
        {
            this.Code = code;
            this.Text = text;
        }

        /// <summary>
        /// Gets or sets the exception code.
        /// </summary>
        public int Code 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the exception text.
        /// </summary>
        private string Text 
        {
            get; set;
        }
    }
}
