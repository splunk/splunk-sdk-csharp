﻿/*
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

using System;
using System.Xml;

namespace Splunk.ModularInputs
{
    /// <summary>
    ///     Writes event to stdout using XML streaming mode.
    /// </summary>
    public class EventStreamWriter : IDisposable
    {
        /// <summary>
        /// Used to write XML to stdout.
        /// </summary>
        private XmlTextWriter xmlWriter = new XmlTextWriter(Console.Out);

        /// <summary>
        /// Initializes a new instance of the <see cref="EventStreamWriter" /> class.
        /// </summary>
        public EventStreamWriter()
        {
            xmlWriter.WriteStartElement("stream");
        }

        /// <summary>
        /// Write the last end tag and release resources.
        /// </summary>
        public void Dispose()
        {
            if (xmlWriter == null)
            {
                return;
            }

            xmlWriter.WriteEndElement();
            ((IDisposable) xmlWriter).Dispose();
            xmlWriter = null;
        }

        /// <summary>
        /// Write the event element 
        /// </summary>
        /// <param name="eventElement">A event element</param>
        public void Write(EventElement eventElement)
        {
            xmlWriter.WriteStartElement("event");

            var stanza = eventElement.Stanza;
            if (stanza != null)
            {
                xmlWriter.WriteAttributeString("stanza", stanza);
            }

            if (eventElement.Unbroken)
            {
                xmlWriter.WriteAttributeString("unbroken", "1");
            }

            WriteElementIfNotNull(eventElement.Index, "index");
            WriteElementIfNotNull(eventElement.SourceType, "sourcetype");
            WriteElementIfNotNull(eventElement.Source, "source");
            WriteElementIfNotNull(eventElement.Host, "host");

            WriteElementIfNotNull(eventElement.Data, "data");

            var time = eventElement.Time;
            if (time != null)
            {
                xmlWriter.WriteElementString(
                    "time",
                    ConvertTimeToUtcUnixTimestamp(time.Value));
            }

            if (eventElement.Done)
            {
                xmlWriter.WriteStartElement("done");
                xmlWriter.WriteEndElement();
                Console.Out.Flush();
            }

            xmlWriter.WriteEndElement();
        }

        /// <summary>
        /// Write the element if it's content is not null
        /// </summary>
        /// <param name="content">Content of the element</param>
        /// <param name="tag">The tag name</param>
        private void WriteElementIfNotNull(string content, string tag)
        {
            if (content != null)
            {
                xmlWriter.WriteElementString(tag, content);
            }
        }

        /// <summary>
        /// Convert to Unix UTC timestamp
        /// </summary>
        /// <param name="dateTime">A date time value</param>
        /// <returns>The unit timestamp</returns>
        private static string ConvertTimeToUtcUnixTimestamp(DateTime dateTime)
        {
            // Unit timestamp is seconds after a fixed date, known as 
            // "unix timestamp epoch".
            var unixUtcEpoch =
                new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var utcTime = TimeZoneInfo.ConvertTimeToUtc(dateTime);

            return (utcTime - unixUtcEpoch).TotalSeconds.ToString();
        }
    }
}