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
    using System;
    using System.Net;
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
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadArgument()
        {
            var service = Connect();
            var query = "search index=_internal * earliest=-1m | stats count";

            var job = this.RunWait(service, query);
            job.Results(new Args("output_mode", "invalid_arg")).Close();
            job.Cancel();
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        public void JobResultsOutputModeArgument()
        {
            var service = Connect();
            var query = "search index=_internal * earliest=-1m | stats count";

            var outputModes = Enum.GetNames(typeof(JobResultsArgs.OutputModeEnum));
            foreach (var modeString in outputModes)
            {
                var mode = (JobResultsArgs.OutputModeEnum)Enum.Parse(
                    typeof(JobResultsArgs.OutputModeEnum), modeString);

                var job = this.RunWait(service, query);

                job.Results(
                    new JobResultsArgs
                        {
                            OutputMode = mode
                        }).Close();

                job.Cancel();
            }     
        }
    }
}
