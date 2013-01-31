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

namespace UnitTests
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Splunk;

    /// <summary>
    /// Common class of all ResultsReader tests.
    /// </summary>
    [TestClass]
    public class ResultsReaderTest : TestHelper
    {
        /// <summary>
        /// Input file folder
        /// </summary>
        private const string TestDataFolder = @"Data\Results";

        /// <summary>
        /// Input file for the json test on Splunk Version 5
        /// </summary>
        private const string Splunk5JsonInputFilePath = "results5.json";

        /// <summary>
        /// Input file for the json test on Splunk Version 4
        /// </summary>
        private const string Splunk4JsonInputFilePath = "results4.json";

        /// <summary>
        /// Input file for the xml test
        /// </summary>
        private const string SplunkXmlInputFilePath = "results.xml";

        /// <summary>
        /// Input file for the xml test
        /// </summary>
        private const string SplunkMultipleResultsXmlInputFilePath = 
            "resultsMultiple.xml";

        /// <summary>
        /// Input file for the xml test on Splunk Version 4.3.5 preview
        /// </summary>
        private const string Splunk435PreviewXmlInputFilePath
            = @"4.3.5\results-preview.xml";

        /// <summary>
        /// Input file for the xml test on Splunk Version 5.0.2
        /// </summary>
        private const string Splunk502XmlInputFilePath
            = @"5.0.2\results.xml";
        
        /// <summary>
        /// Test json format using an input file representing 
        /// Splunk Version 5, with simple data common to all readers.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            Splunk5JsonInputFilePath,
            TestDataFolder)]
        public void TestReadJsonOnSplunk5()
        {
            this.TestReadJson(Splunk5JsonInputFilePath);
        }

        /// <summary>
        /// Test json format using an input file representing
        /// Splunk Version 4, with simple data common to all readers.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            Splunk4JsonInputFilePath,
            TestDataFolder)]
        public void TestReadJsonOnSplunk4()
        {
            this.TestReadJson(Splunk4JsonInputFilePath);
        }

        /// <summary>
        /// Test XML format using an input file representing
        /// Splunk, with simple data common to all readers.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            SplunkXmlInputFilePath,
            TestDataFolder)]
        public void TestReadXml()
        {
            var input = this.OpenResourceFileFromDataFolder(
                SplunkXmlInputFilePath);

            var reader = new ResultsReaderXml(input);

            this.TestRead(reader);
        }

        /// <summary>
        /// Test XML format using an input file representing
        /// Splunk, with multiple 'results' elements.
        /// Refer to 
        /// http://splunk-base.splunk.com/answers/34106/invalid-xml-returned-from-rest-api
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            SplunkMultipleResultsXmlInputFilePath,
            TestDataFolder)]
        public void TestReadXmlMultiple()
        {
            var input = this.OpenResourceFileFromDataFolder(
                SplunkMultipleResultsXmlInputFilePath);

            var reader = new ResultsReaderXml(input);

            // There are two events in the first set,
            // and it is a preview.
            Assert.AreEqual(reader.Count(), 1);
            Assert.IsTrue(reader.IsPreview);

            // There are three events in the second set.
            Assert.AreEqual(reader.Count(), 3);

            // Skip previews.
            while (reader.IsPreview)
            {
                foreach (var result in reader) 
                { 
                }
            }

            // There are 5 events in the final set.
            Assert.AreEqual(reader.Count(), 5);
        }

        /// <summary>
        /// Test XML format using an input file representing
        /// Splunk, covering preview and field '_raw'.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            Splunk435PreviewXmlInputFilePath,
            TestDataFolder)]
        public void TestReadXmlPreviewAndFields()
        {
            var input = this.OpenResourceFileFromDataFolder(
                Splunk435PreviewXmlInputFilePath);

            var reader = new ResultsReaderXml(input);

            Assert.IsTrue(reader.IsPreview);

            var fields = reader.Fields.ToArray();

            Assert.AreEqual(fields[0], "_cd");
            Assert.AreEqual(fields[fields.Length - 1], "version");
            CollectionAssert.Contains(fields, "_raw");
            CollectionAssert.Contains(fields, "mean_preview_period");

            var events = reader.ToArray();

            var event0 = events[0];

            Assert.AreEqual(event0["_cd"], "54:8568");

            Assert.AreEqual(
                (int)event0["timestartpos"], 
                0);

            Assert.AreEqual(events.Length, 10);

            var event9 = events[9];

            Assert.IsTrue(event9["_raw"].ToString().Contains(
                @"<sg h=""1"">search</sg>"));

            Assert.IsFalse(event9["_raw"].ToString().Contains(
                 @"<v"));
        }

        /// <summary>
        /// Test XML format using an input file representing
        /// Splunk, covering Splunk Version 5 and '_raw' field.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            Splunk502XmlInputFilePath,
            TestDataFolder)]
        public void TestReadXmlSplunk5MVAndFieldRaw()
        {
            var input = this.OpenResourceFileFromDataFolder(
                Splunk502XmlInputFilePath);

            var reader = new ResultsReaderXml(input);

            Assert.IsFalse(reader.IsPreview);

            var events = reader.ToArray();

            var valuesSi = events[0]["_si"].GetArray();

            Assert.AreEqual(valuesSi.Length, 2);

            Assert.AreEqual(valuesSi[1], "_internal");

            Assert.IsTrue(events[2]["_raw"].ToString().Contains(
                @"""GET /services/messages HTTP/1.1"""));

            Assert.IsFalse(events[2]["_raw"].ToString().Contains(
                 @"<v"));
        }

        /// <summary>
        /// Test XML format with a Oneshot search.
        /// </summary>
        [TestMethod]
        public void TestOneshotXml()
        {
            var service = Connect();

            var input = service.Oneshot(
                "search index=_internal | head 1 | stats count",
                Args.Create("output_mode", "xml"));

            var reader = new ResultsReaderXml(input);

            var count = (int)reader.ToArray()[0]["count"];

            Assert.AreEqual(count, 1);
        }
      
        /// <summary>
        /// Test json format using an input file
        /// </summary>
        /// <param name="path">Path to the input file</param>
        private void TestReadJson(string path)
        {
            var input = this.OpenResourceFileFromDataFolder(
                path);

            var reader = new ResultsReaderJson(input);

            this.TestRead(reader);
        }

        /// <summary>
        /// Test result reader
        /// </summary>
        /// <param name="reader">The reader</param>
        private void TestRead(ResultsReader reader)
        {
            var expected = new Event();

            AddToEvent(expected, "series", "twitter");
            AddToEvent(expected, "sum(kb)", "14372242.758775");
            this.AssertNextEventEqualsAndReset(expected, reader);

            AddToEvent(expected, "series", "splunkd");
            AddToEvent(expected, "sum(kb)", "267802.333926");
            this.AssertNextEventEqualsAndReset(expected, reader);

            AddToEvent(expected, "series", "splunkd_access");
            AddToEvent(expected, "sum(kb)", "5979.036338");
            this.AssertNextEventEqualsAndReset(expected, reader);

            var iter = reader.GetEnumerator();
            Assert.IsFalse(iter.MoveNext());
        }

        /// <summary>
        /// Add a field to the event
        /// </summary>
        /// <param name="myEvent">Event to add the field to</param>
        /// <param name="key">Key of the field</param>
        /// <param name="value">String value of the field</param>
        private static void AddToEvent(
            Event myEvent,
            string key,
            string value)
        {
            myEvent.Add(key, new Event.Field(value));
        }

        /// <summary>
        /// Open resource file
        /// </summary>
        /// <param name="path">Relative path to the resource</param>
        /// <returns>Stream of resource content</returns>
        private Stream OpenResourceFile(string path)
        {
            return File.OpenRead(TestDataFolder + @"\" + path);
        }

        /// <summary>
        /// Open resource file from base directory
        /// </summary>
        /// <param name="relativePath">Relative path to the resource</param>
        /// <returns>Stream of resource content</returns>
        private Stream OpenResourceFileFromDataFolder(string relativePath)
        {
            return File.OpenRead(TestDataFolder + @"\" + relativePath);
        }

        /// <summary>
        /// Assert the next event in the reader is equal to the expected one.
        /// The expected event will be cleared when the method return. 
        /// </summary>
        /// <param name="expected">Expected event, which will be cleared when the method returns</param>
        /// <param name="reader">Results reader</param>
        private void AssertNextEventEqualsAndReset(
            Event expected,
            ResultsReader reader)
        {
            var iter = reader.GetEnumerator();
            iter.MoveNext();
            var actual = iter.Current;

            CollectionAssert.AreEquivalent(
                expected.Select(x => x.Value.ToString()).ToArray(),
                actual.Select(x => x.Value.ToString()).ToArray());

            expected.Clear();
        }
    }
}
