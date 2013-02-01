﻿/*
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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Splunk;

    /// <summary>
    /// Common class of all ResultsReader tests.
    /// </summary>
    [TestClass]
    public class ResultsReaderTest : TestHelper
    {
        /// <summary>
        /// Input file for the json test on Splunk Version 5
        /// </summary>
        private const string Splunk5JsonInputFilePath = "results5.json";

        /// <summary>
        /// Input file for the json test on Splunk Version 4
        /// </summary>
        private const string Splunk4JsonInputFilePath = "results4.json";

        /// <summary>
        /// Test json format using an input file representing 
        /// Splunk Version 5
        /// </summary>
        [TestMethod]
        [DeploymentItem(Splunk5JsonInputFilePath)]
        public void TestReadJsonOnSplunk5()
        {
            this.TestReadJson(Splunk5JsonInputFilePath);
        }

        /// <summary>
        /// Test json format using an input file representing
        /// Splunk Version 4
        /// </summary>
        [TestMethod]
        [DeploymentItem(Splunk4JsonInputFilePath)]
        public void TestReadJsonOnSplunk4()
        {
            this.TestReadJson(Splunk4JsonInputFilePath);
        }

        /// <summary>
        /// Test json format using an input file
        /// </summary>
        /// <param name="path">Path to the input file</param>
        private void TestReadJson(string path)
        {
            var input = this.OpenResource(path);
            var reader = new ResultsReaderJson(input);
            var expected = new Event();

            AddToEvent(expected, "series", "twitter");
            AddToEvent(expected, "sum(kb)", "14372242.758775");
            this.AssertNextEventEquals(expected, reader);

            AddToEvent(expected, "series", "splunkd");
            AddToEvent(expected, "sum(kb)", "267802.333926");
            this.AssertNextEventEquals(expected, reader);

            AddToEvent(expected, "series", "splunkd_access");
            AddToEvent(expected, "sum(kb)", "5979.036338");
            this.AssertNextEventEquals(expected, reader);

            var iter = reader.GetEnumerator();
            Assert.IsFalse(iter.MoveNext());
        }

        private static void AddToEvent(
            Event expected,
            string key,
            string value)
        {
            expected.Add(key, new Event.FieldValue(value));
        }

  
        /// <summary>
        /// Open file resource from network or local disk
        /// </summary>
        /// <param name="path">Path to the resource</param>
        /// <returns>Stream of resource content</returns>
        private Stream OpenResource(string path)
        {
            // TODO: more from Java SDK here.
            return File.OpenRead(path);
        }

        /// <summary>
        /// Assert helper
        /// </summary>
        /// <param name="expected">Expected result</param>
        /// <param name="reader">Results reader</param>
        private void AssertNextEventEquals(
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
