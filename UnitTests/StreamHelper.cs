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

namespace UnitTests
{
    using System.IO;

    /// <summary>
    /// For Extension method on Stream object
    /// </summary>
    internal static class StreamHelper
    {
        /// <summary>
        /// Extension method of stream object
        /// </summary>
        /// <param name="stream">Object the extension method is on.</param>
        /// <param name="bytes">Input of the method</param>
        public static void Write(this Stream stream, byte[] bytes)
        {
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
