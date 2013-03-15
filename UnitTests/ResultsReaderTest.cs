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
    using System;
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
        /// Input file for the JSON test on Splunk Version 5
        /// </summary>
        private const string Splunk5JsonInputFilePath = "results5.json";

        /// <summary>
        /// Input file for the JSON test on Splunk Version 4
        /// </summary>
        private const string Splunk4JsonInputFilePath = "results4.json";

        /// <summary>
        /// Input file for the xml test
        /// </summary>
        private const string SplunkXmlInputFilePath = "results.xml";

        /// <summary>
        /// Input file for the xml test
        /// </summary>
        private const string SplunkExportResultsXmlInputFilePath =
            "resultsExport.xml";

        /// <summary>
        /// Input file for the JSON test
        /// </summary>
        private const string SplunkExportResultsJsonInputFilePath =
            "resultsExport.json";

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
        /// Input file for the xml test on Splunk Version 5.0.2
        /// </summary>
        private const string Splunk502XmlEmptyInputFilePath
            = @"5.0.2\results-empty.xml";

        /// <summary>
        /// Test JSON format using an input file representing 
        /// Splunk Version 5, with simple data common to all readers.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            Splunk5JsonInputFilePath,
            TestDataFolder)]
        public void TestReaderJsonOnSplunk5()
        {
            this.TestReadJson(Splunk5JsonInputFilePath);
        }

        /// <summary>
        /// Test JSON format using an input file representing
        /// Splunk Version 4, with simple data common to all readers.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            Splunk4JsonInputFilePath,
            TestDataFolder)]
        public void TestReaderJsonOnSplunk4()
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
        /// a stream from the export endpoint,
        /// with multiple 'results' elements.
        /// Also refer to 
        /// http://splunk-base.splunk.com/answers/34106/invalid-xml-returned-from-rest-api
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            SplunkExportResultsXmlInputFilePath,
            TestDataFolder)]
        public void TestExportMultiReaderXml()
        {
            var input = this.OpenResourceFileFromDataFolder(
                SplunkExportResultsXmlInputFilePath);

            this.TestExportMultiReader(
                new MultiResultsReaderXml(input),
                expectedCountResultSet: 18);
        }

        /// <summary>
        /// Test JSON format using an input file representing
        /// a stream from the export endpoint,
        /// with multiple 'results' elements.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            SplunkExportResultsJsonInputFilePath,
            TestDataFolder)]
        public void TestExportMultiReaderJson()
        {
            var input = this.OpenResourceFileFromDataFolder(
                SplunkExportResultsJsonInputFilePath);

            this.TestExportMultiReader(
                new MultiResultsReaderJson(input), 
                expectedCountResultSet: 15);
        }
       
        /// <summary>
        /// Test multi reader over an export stream.
        /// </summary>
        /// <typeparam name="T">Type of matching single reader</typeparam>
        /// <param name="multiReader">A multi reader</param>
        /// <param name="expectedCountResultSet">
        /// Expected count of result set in the steam
        /// </param>
        private void TestExportMultiReader<T>(
            MultiResultsReader<T> multiReader,
            int expectedCountResultSet)
            where T : ResultsReader, System.IDisposable
        {
            using (multiReader)
            {
                ISearchResults firstResults = null;
                int indexResultSet = 0;
  
                foreach (var results in multiReader)
                {
                    if (firstResults == null)
                    {
                        firstResults = results;
                    }

                    if (indexResultSet == expectedCountResultSet - 1) 
                    {
                        Assert.IsFalse(results.IsPreview);
                    }

                    var indexEvent = 0;
                    foreach (var ret in results)
                    {
                        if (indexResultSet == 1 && indexEvent == 1) 
                        {
                            Assert.AreEqual("andy-pc", ret["host"]);
                            Assert.AreEqual("3", ret["count"]);
                        }

                        if (indexResultSet == expectedCountResultSet - 2 && indexEvent == 3) 
                        {
                            Assert.AreEqual("andy-pc", ret["host"]);
                            Assert.AreEqual("135", ret["count"]);
                        }

                        indexEvent++;
                    }

                    switch (indexResultSet) 
                    {
                        case 0:
                            Assert.AreEqual(indexEvent, 1);
                            break;
                        case 1:
                            Assert.AreEqual(indexEvent, 3);
                            break;
                        default:
                            Assert.AreEqual(indexEvent, 5);
                            break;
                    }
                
                    indexResultSet++;
                }

                Assert.AreEqual(indexResultSet, expectedCountResultSet);
              
                // firstResults should be empty since the multi-reader has passed it
                // and there should be no exception.
                Assert.AreEqual(0, firstResults.Count());
            }
        }

        /// <summary>
        /// Test XML format using an input file representing
        /// a stream from the export endpoint,
        /// with multiple 'results' elements.
        /// Also refer to 
        /// http://splunk-base.splunk.com/answers/34106/invalid-xml-returned-from-rest-api
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            SplunkExportResultsXmlInputFilePath,
            TestDataFolder)]
        public void TestExportSingleReaderXml()
        {
            var stream = this.GetExportResultsStream(
                SplunkExportResultsXmlInputFilePath);

            this.TestExportSingleReader(
                new ResultsReaderXml(stream));
        }

        /// <summary>
        /// Test JSON format using an input file representing
        /// a stream from the export endpoint,
        /// with multiple 'results' elements.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            SplunkExportResultsJsonInputFilePath,
            TestDataFolder)]
        public void TestExportSingleReaderJson()
        {
            var stream = this.GetExportResultsStream(
                SplunkExportResultsJsonInputFilePath);

            this.TestExportSingleReader(
                new ResultsReaderJson(stream));
        }

        /// <summary>
        /// Get an export stream over a file.
        /// </summary>
        /// <param name="fileName">A file name</param>
        /// <returns>A export stream</returns>
        private ExportResultsStream GetExportResultsStream(string fileName)
        {
            var stream = this.OpenResourceFileFromDataFolder(
                          fileName);

            return new ExportResultsStream(stream);
        }

        /// <summary>
        /// Test a single result reader, used by testing export stream.
        /// </summary>
        /// <param name="reader">A single result reader</param>
        private void TestExportSingleReader(
            ResultsReader reader)
        {
            var indexEvent = 0;

            using (reader)
            {
                foreach (var ret in reader)
                {
                    if (indexEvent == 0)
                    {
                        Assert.AreEqual("172.16.35.130", ret["host"]);
                        Assert.AreEqual("16", ret["count"]);
                    }

                    if (indexEvent == 4)
                    {
                        Assert.AreEqual("three.four.com", ret["host"]);
                        Assert.AreEqual("35994", ret["count"]);
                    }

                    indexEvent++;
                }

                Assert.AreEqual(5, indexEvent);
            }
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

            var expectedRawFieldValue =
                @"12-19-2012 11:50:14.351 -0800 INFO  Metrics - group=search_concurrency, system total, active_hist_searches=0, active_realtime_searches=0";

            var expectedSegmentedRaw =
                "<v xml:space=\"preserve\" trunc=\"0\">12-19-2012 11:50:14.351 -0800 INFO  Metrics - group=<sg h=\"1\">search</sg>_concurrency, system total, active_hist_searches=0, active_realtime_searches=0</v>";
            
            Assert.AreEqual(expectedRawFieldValue, event9["_raw"]);

            Assert.AreEqual(expectedSegmentedRaw, event9.SegmentedRaw);
        }

        /// <summary>
        /// Test XML format using an input file representing
        /// Splunk, covering empty results.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            Splunk502XmlEmptyInputFilePath,
            TestDataFolder)]
        public void TestReadXmlEmpty()
        {
            var input = this.OpenResourceFileFromDataFolder(
                Splunk502XmlEmptyInputFilePath);

            var reader = new ResultsReaderXml(input);

            Assert.IsFalse(reader.IsPreview);

            var fields = reader.Fields.ToArray();

            Assert.AreEqual(0, fields.Length);
     
            var events = reader.ToArray();

            Assert.AreEqual(0, events.Length);
        }

        /// <summary>
        /// Test XML format using an input file representing
        /// Splunk, covering Splunk Version 5, '_raw' field
        /// and XML charactor escaping.
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

            // Verify handling of XML charactor escaping. 
            Assert.AreEqual(
                @"..._-__[//:::._-]_""_/-///_/.""___""://:/-//?=.&=""_""/",
                events[1]["punct"]);
        }

        /// <summary>
        /// Test XML format with a Oneshot search.
        /// </summary>
        [TestMethod]
        public void TestReaderEndToEndOneshotXml()
        {
            this.TestReaderEndToEnd(
                this.Connect(),
                "xml", 
                (service, query, args) => service.Oneshot(query, args),
                (input) => new ResultsReaderXml(input));
        }

        /// <summary>
        /// Test Json format with a Oneshot search.
        /// </summary>
        [TestMethod]
        public void TestReaderEndToEndOneshotJson()
        {
            this.TestReaderEndToEnd(
                this.Connect(),
                "json",
                (service, query, args) => service.Oneshot(query, args),
                (input) => new ResultsReaderJson(input));
        }

        /// <summary>
        /// Test XML format with an Export search.
        /// </summary>
        [TestMethod]
        public void TestReaderEndToEndExportXml()
        {
            this.TestReaderEndToEnd(
                this.Connect(),
                "xml",
                (service, query, args) => service.Export(query, args),
                (input) => new ResultsReaderXml(input));
        }

        /// <summary>
        /// Test Json format with an Export search.
        /// </summary>
        [TestMethod]
        public void TestReaderEndToEndExportJson()
        {
            var service = this.Connect();
            if (service.VersionCompare("5.0") < 0)
            {
                return;
            }

            this.TestReaderEndToEnd(
                service,
                "json",
                (service2, query, args) => service2.Export(query, args),
                (input) => new ResultsReaderJson(input));
        }

        /// <summary>
        /// Test a result reader by running a search on Splunk.
        /// </summary>
        /// <param name="service">The service object to run the search.</param>
        /// <param name="format">The search output format</param>
        /// <param name="runSearch">
        /// Run a type of search using the supplied service object,
        /// search string, and arguments.
        /// </param>
        /// <param name="createReader">
        /// Create a reader matching the search output format.
        /// </param>
        private void TestReaderEndToEnd(
            Service service,
            string format, 
            Func<Service, string, Args, Stream> runSearch,
            Func<Stream, ResultsReader> createReader)
        {
            var input = runSearch(
                service,
                "search index=_internal | head 1 | stats count",
                Args.Create("output_mode", format));

            var reader = createReader(input);

            var count = (int)reader.ToArray()[0]["count"];

            Assert.AreEqual(count, 1);
        }

        /// <summary>
        /// Test JSON format using an input file
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
        private void TestRead(IEnumerable<Event> reader)
        {
            var expected = new Event();

            var iter = reader.GetEnumerator();

            AddToEvent(expected, "series", "twitter");
            AddToEvent(expected, "sum(kb)", "14372242.758775");
            this.AssertNextEventEqualsAndReset(expected, iter);

            AddToEvent(expected, "series", "splunkd");
            AddToEvent(expected, "sum(kb)", "267802.333926");
            this.AssertNextEventEqualsAndReset(expected, iter);

            AddToEvent(expected, "series", "splunkd_access");
            AddToEvent(expected, "sum(kb)", "5979.036338");
            this.AssertNextEventEqualsAndReset(expected, iter);

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
            myEvent.Add(key, new Event.FieldValue(value));
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
        /// <param name="iter">Iterator over events</param>
        private void AssertNextEventEqualsAndReset(
            Event expected,
            IEnumerator<Event> iter)
        {
            iter.MoveNext();
            var actual = iter.Current;

            CollectionAssert.AreEquivalent(
                expected.Select(x => x.Value.ToString()).ToArray(),
                actual.Select(x => x.Value.ToString()).ToArray());

            expected.Clear();
        }
    }
}
