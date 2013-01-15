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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Splunk;

    /// <summary>
    /// Common class of all ResultsReader tests.
    /// </summary>
    [TestClass]
    public class ResultsReaderTest : TestHelper
    {
        /// <summary>
        /// Input file for the json test
        /// </summary>
        private const string JsonInputFilePath = "results5.json";
        
        /// <summary>
        /// Test json format using an input file (not splunk server)
        /// </summary>
        [TestMethod]
        [DeploymentItem(JsonInputFilePath)]
        public void TestReadJsonOnSplunk5()
        {
            var input = this.OpenResource(JsonInputFilePath);
            var reader = new ResultsReaderJson(input);
            var expected = new Dictionary<string, object>();

            expected.Add("series", "twitter");
            expected.Add("sum(kb)", "14372242.758775");
            this.AssertNextEventEquals(expected, reader);

            expected.Add("series", "splunkd");
            expected.Add("sum(kb)", "267802.333926");
            this.AssertNextEventEquals(expected, reader);

            expected.Add("series", "splunkd_access");
            expected.Add("sum(kb)", "5979.036338");
            this.AssertNextEventEquals(expected, reader);

            Assert.IsNull(reader.GetNextEvent());
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
            Dictionary<string, object> expected,
            ResultsReader reader)
        {
            var actual = reader.GetNextEvent();
            CollectionAssert.AreEquivalent(
                expected, 
                actual);

            expected.Clear();
        }
    }
}
