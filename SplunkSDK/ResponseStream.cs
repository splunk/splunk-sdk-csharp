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
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <summary>
    /// The <see cref="ResponseStream"/> class represents a stream
    /// constructed by <see cref="ResponseMessage"/>. Everything is
    /// redirected to the original stream.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.DocumentationRules",
        "SA1600:ElementsMustBeDocumented",
        Justification =
            "Internal class. Pure passthrough")]
    internal class ResponseStream : Stream
    {
        private ResponseMessage message;
        private readonly bool isExportResult;

        internal ResponseStream(ResponseMessage message)
        {
            this.message = message;
        }

        internal ResponseStream(ResponseMessage message, bool isExportResult)
        {
            this.message = message;
            this.isExportResult = isExportResult;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.message.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Gets the body content stream.
        /// </summary>
        /// <returns>The stream.</returns>
        public bool IsExportResult
        {
            get { return this.isExportResult; }
        }

        public override long Position
        {
            get { return this.message.Content.Position; }

            set { this.message.Content.Position = value; }
        }

        public override long Length
        {
            get { return this.message.Content.Length; }
        }

        public override bool CanRead
        {
            get { return this.message.Content.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.message.Content.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.message.Content.CanWrite; }
        }

        public override void Flush()
        {
            this.message.Content.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.message.Content.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.message.Content.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.message.Content.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.message.Content.Write(buffer, offset, count);
        }
    }
}
