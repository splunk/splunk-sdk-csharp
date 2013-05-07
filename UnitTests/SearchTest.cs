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

using System.IO;

namespace UnitTests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// This is the search test class
    /// </summary>
    [TestClass]
    public class SearchTest : TestHelper
    {
        /// <summary>
        /// Run the given query.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="query">The search query</param>
        /// <returns>The job</returns>
        private Job Run(Service service, string query)
        {
            return this.Run(service, query, null);
        }

        /// <summary>
        /// Run the given query with the given query args.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="query">The search query</param>
        /// <param name="args">The args</param>
        /// <returns>The job</returns>
        private Job Run(Service service, string query, Args args)
        {
            return service.GetJobs().Create(query, args);
        }

        /// <summary>
        /// Run the given query and wait for the job to complete.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="query">The search query</param>
        /// <returns>The job</returns>
        private Job RunWait(Service service, string query)
        {
            return this.RunWait(service, query, null);
        }

        /// <summary>
        /// Run the given query with the given query args and wait for the job to
        /// complete.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="query">The search query</param>
        /// <param name="args">The args</param>
        /// <returns>The job</returns>
        private Job RunWait(Service service, string query, Args args)
        {
            Job job = service.GetJobs().Create(query, args);
            return this.Wait(job);
        }

        /// <summary>
        /// Tests the basic create job, wait for it to finish, close the stream
        /// and cancel (clean up) the job on the server. Try with optional args too.
        /// </summary>
        [TestMethod]
        public void Search()
        {
            Service service = Connect();
            string query = "search index=_internal * earliest=-1m | stats count";

            Job job;

            job = this.RunWait(service, query);
            job.Results().Close();
            job.Cancel();

            job = this.RunWait(service, query);
            job.Results(new Args("output_mode", "csv")).Close();
            job.Cancel();

            job = this.RunWait(service, query);
            job.Results(new Args("output_mode", "json")).Close();
            job.Cancel();
        }

        /// <summary>
        /// Verify that segmentation is default to 'none'.
        /// </summary>
        [TestMethod]
        public void SegmentationWithExport()
        {
            Segmentation(
                (service, query, args) => service.Export(query, args));
        }

        /// <summary>
        /// Verify that segmentation is default to 'none'.
        /// </summary>
        [TestMethod]
        public void SegmentationWithOneshot()
        {
            Segmentation(
                (service, query, args) => service.Oneshot(query, args));
        }

        /// <summary>
        /// Verify that segmentation is default to 'none'.
        /// </summary>
        [TestMethod]
        public void SegmentationWithJobResults()
        {
            SegmentationWithJob(
                (job, resultsArgs) => job.Results(resultsArgs));
        }

        /// <summary>
        /// Verify that segmentation is default to 'none'.
        /// </summary>
        [TestMethod]
        public void SegmentationWithJobResultsPreview()
        {
            SegmentationWithJob(
                (job, resultsArgs) => job.ResultsPreview(resultsArgs));
        }

        /// <summary>
        /// Verify that segmentation is default to 'none'.
        /// </summary>
        [TestMethod]
        public void SegmentationWithJobEvents()
        {
            SegmentationWithJob(
                (job, resultsArgs) => job.Events(resultsArgs));
        }

        /// <summary>
        /// Verify that segmentation is default to 'none'.
        /// </summary>
        /// <param name="results">
        /// Get results stream from a Job object.
        /// </param>
        private void SegmentationWithJob(
            Func<Job, Args, Stream> results)
        {
            Segmentation(
                (service, query, resultsArgs) =>
                {
                    var job = this.RunWait(service, query);
                    return results(job, resultsArgs);
                });
        }

        /// <summary>
        /// Verify that segmentation is default to 'none'.
        /// </summary>
        /// <param name="getResults">
        /// Function to get a results stream.
        /// </param>
        private void Segmentation(
            Func<Service, string, Args, Stream> getResults)
        {
            var service = Connect();

            // 'segmentation=none' has no impact on Splunk 4.3.5 (or earlier)
            if (service.VersionCompare("5.0") < 0)
            {
                return;
            }

            const string SgTag = "<sg";

            var query = "search index=_internal GET | head 3";

            var input = getResults(service, query, null);

            using (var reader = new StreamReader(input))
            {
                var data = reader.ReadToEnd();
                Assert.IsFalse(data.Contains(SgTag));
            }

            var args = new Args
                {
                    { "segmentation", "raw" }
                };

            input = getResults(service, query, args);

            using (var reader = new StreamReader(input))
            {
                var data = reader.ReadToEnd();
                Assert.IsTrue(data.Contains(SgTag));
            }
        }
    }
}
