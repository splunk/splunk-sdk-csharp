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

namespace Splunk.ModularInputs
{
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;
    using System.Xml;
    using System;

    /// <summary>
    ///     Each stanza in the inputs.conf has a set of parameters that are stored in a KV pair store.
    /// </summary>
    public class EventStreamWriter: IDisposable
    {
        private XmlTextWriter xmlWriter = new XmlTextWriter(Console.Out);
        public EventStreamWriter()
        {
            this.xmlWriter.WriteStartElement("stream");
        }

        public void Write(Event @event)
        {
            this.xmlWriter.WriteStartElement("event");

            var stanza = @event.Stanza;
            if (stanza != null)
            {
                this.xmlWriter.WriteAttributeString("stanza", stanza);
            }

            if (@event.Unbroken)
            {
                this.xmlWriter.WriteAttributeString("unbroken", "1");
            }

            WriteElementIfNotNull(@event.Index, "index");
            WriteElementIfNotNull(@event.SourceType, "sourcetype");
            WriteElementIfNotNull(@event.Source, "source");
            WriteElementIfNotNull(@event.Host, "host");

            WriteElementIfNotNull(@event.Data, "data");

            var time = @event.Time;
            if (time != null)
            {
                this.xmlWriter.WriteElementString(
                    "time",
                    ConvertTimeToUtcUnixTimestamp(time.Value));
            }
         
            if (@event.Done)
            {
                this.xmlWriter.WriteStartElement("done");
                this.xmlWriter.WriteEndElement();
                Console.Out.Flush();
            }

            this.xmlWriter.WriteEndElement();
        }

        private void WriteElementIfNotNull(string content, string tag)
        {
            if (content != null)
            {
                this.xmlWriter.WriteElementString(tag, content);
            }
        }

        private static string ConvertTimeToUtcUnixTimestamp(DateTime dateTime)
        {
            var unixUtcEpoch =
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var utcTime = TimeZoneInfo.ConvertTimeToUtc(dateTime);

            return (utcTime - unixUtcEpoch).TotalSeconds.ToString();
        }

        public void Dispose()
        {
            if (this.xmlWriter == null)
            {
                return;
            }

            this.xmlWriter.WriteEndElement();
            ((IDisposable)this.xmlWriter).Dispose();
            this.xmlWriter = null;
        }
    }
}