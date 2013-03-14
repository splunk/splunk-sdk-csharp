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
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    /// <summary>
    /// Represents a stream constructed by Service.Export.
    /// Everything is redirected to the original stream.
    /// </summary>
    [SuppressMessage(
        "Microsoft.StyleCop.CSharp.DocumentationRules", 
        "SA1600:ElementsMustBeDocumented",
        Justification = 
            "Internal class. Pure passthrough")]
    internal class ExportResultsStream : Stream
    {
        private Stream inner;

        internal ExportResultsStream(Stream stream)
        {
            this.inner = stream;
        }

        public override long Position
        {
            get
            {
                return this.inner.Position;
            }

            set
            {
                this.inner.Position = value;
            }
        }

        public override long Length
        {
            get { return this.inner.Length; }
        }
        
        public override bool CanRead
        {
            get { return this.inner.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.inner.CanSeek; }
        }
        
        public override bool CanWrite
        {
            get { return this.inner.CanWrite; }
        }

        public override void Flush()
        {
            this.inner.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.inner.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.inner.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.inner.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.inner.Write(buffer, offset, count);
        }
    }
}
