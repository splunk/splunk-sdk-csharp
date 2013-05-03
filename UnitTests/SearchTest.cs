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
        /// Search query the tests will use
        /// </summary>
        private const string Query = 
            "search index=_internal * earliest=-1m | stats count";

        /// <summary>
        /// Invalid argument
        /// </summary>
        private readonly Args badOutputMode =
            new Args("output_mode", "invalid_arg_value");

        /// <summary>
        /// Invalid argument
        /// </summary>
        private readonly Args badSearchMode =
            new Args("search_mode", "invalid_arg_value");

        /// <summary>
        /// Invalid argument
        /// </summary>
        private readonly Args badTruncationMode =
            new Args("truncation_mode", "invalid_arg_value");

        /// <summary>
        /// Invalid argument
        /// </summary>
        private readonly Args badExecutionMode =
            new Args("exec_mode", "invalid_arg_value");

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
        private Job Run(Service service, string query, JobArgs args)
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

            Job job;

            job = this.RunWait(service, Query);
            job.Results().Close();
            job.Cancel();

            job = this.RunWait(service, Query);
            job.Results(new Args("output_mode", "csv")).Close();
            job.Cancel();

            job = this.RunWait(service, Query);
            job.Results(new Args("output_mode", "json")).Close();
            job.Cancel();
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadOutputMode()
        {
            var service = Connect();

            var job = this.RunWait(service, Query);
            job.Results(badOutputMode).Close();
            job.Cancel();
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadTruncateMode()
        {
            var service = Connect();

            var job = this.RunWait(service, Query);
            job.Events(badTruncationMode).Close();
            job.Cancel();
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadSearchMode()
        {
            var service = Connect();

            var job = this.RunWait(
                service,  
                Query, 
                badSearchMode);
            job.Cancel();
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadExecutionMode()
        {
            var service = Connect();

            var job = this.RunWait(
                service,
                Query,
                badExecutionMode);
            job.Cancel();
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadSearchModeExport()
        {
            var service = Connect();

            service.Export(Query, badSearchMode);
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadOutputModeExport()
        {
            var service = Connect();

            service.Export(Query, badOutputMode);
        }

        /// <summary>
        /// Tests the result from a bad search argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(WebException),
          "Bad argument should cause Splunk to return http 400: Bad Request")]
        public void BadTruncationModeExport()
        {
            var service = Connect();

            service.Export(Query, badTruncationMode);
        }

        /// <summary>
        /// Tests all output modes for Job.Results
        /// </summary>
        [TestMethod]
        public void JobResultsOutputModeArgument()
        {
            var type = typeof(JobResultsArgs.OutputModeEnum);

            RunJobFuntionForEachEnum(
                type,
                (job, mode) =>
                    job.Results(
                        new JobResultsArgs
                        {
                            OutputMode = 
                                (JobResultsArgs.OutputModeEnum) Enum.Parse(
                                    type, 
                                    mode)
                        }));
        }

        /// <summary>
        /// Tests all output modes for Job.ResultsPreview
        /// </summary>
        [TestMethod]
        public void JobResultsPreviewOutputModeArgument()
        {
            var type = typeof(JobResultsPreviewArgs.OutputModeEnum);

            RunJobFuntionForEachEnum(
                type,
                (job, mode) =>
                    job.ResultsPreview(
                        new JobResultsPreviewArgs
                        {
                            OutputMode =
                                (JobResultsPreviewArgs.OutputModeEnum) Enum.Parse(
                                    type,
                                    mode)
                        }));
        }

        /// <summary>
        /// Tests all output modes for Job.Events
        /// </summary>
        [TestMethod]
        public void JobEventsOutputModeArgument()
        {
            var type = typeof(JobEventsArgs.OutputModeEnum);

            RunJobFuntionForEachEnum(
                type,
                (job, mode) =>
                job.Events(
                    new JobEventsArgs
                        {
                            OutputMode =
                                (JobEventsArgs.OutputModeEnum) Enum.Parse(
                                    type,
                                    mode)
                        }));
        }

        /// <summary>
        /// Tests all output modes for Job.Events
        /// </summary>
        [TestMethod]
        public void JobEventsTruncationModeArgument()
        {
            var type = typeof(JobEventsArgs.TruncationModeEnum);

            RunJobFuntionForEachEnum(
                type,
                (job, mode) =>
                    job.Events(
                        new JobEventsArgs
                        {
                            TruncationMode = 
                                (JobEventsArgs.TruncationModeEnum) Enum.Parse(
                                    type,
                                    mode)
                        }));
        }

        /// <summary>
        /// Run a job and a function on the job 
        /// for each enum value in an enum type.
        /// </summary>
        /// <param name="enumType">The enum type</param>
        /// <param name="jobFunction">
        /// A function for a job and an enum value
        /// </param>
        private void RunJobFuntionForEachEnum(
            Type enumType,
            Func<Job, string, Stream> jobFunction)
        {
            var service = Connect();

            ForEachEnum(
                enumType,
                (@enum) =>
                    {
                        var job = this.RunWait(service, Query);

                        jobFunction(job, @enum).Close();

                        job.Cancel();
                    });
        }

        /// <summary>
        /// Tests all search modes
        /// </summary>
        [TestMethod]
        public void JobSearchModeArgument()
        {
            var type = typeof(JobArgs.SearchModeEnum);

            RunJobForEachEnum(
                type,
                (mode) => new JobArgs
                    {
                        SearchMode =
                            (JobArgs.SearchModeEnum) Enum.Parse(
                                    type,
                                    mode)
                    });
        }


        /// <summary>
        /// Tests all search modes for export
        /// </summary>
        [TestMethod]
        public void ExportSearchModeArgument()
        {
            var type = typeof(JobExportArgs.SearchModeEnum);

            RunExportForEachEnum(
                type,
                (mode) => new JobExportArgs
                {
                    SearchMode =
                        (JobExportArgs.SearchModeEnum)Enum.Parse(
                                type,
                                mode)
                });
        }

        /// <summary>
        /// Tests all search modes for export
        /// </summary>
        [TestMethod]
        public void ExportOutputModeArgument()
        {
            var type = typeof(JobExportArgs.OutputModeEnum);

            RunExportForEachEnum(
                type,
                (mode) => new JobExportArgs
                {
                    OutputMode = 
                        (JobExportArgs.OutputModeEnum)Enum.Parse(
                                type,
                                mode)
                });
        }

        /// <summary>
        /// Tests all search modes for export
        /// </summary>
        [TestMethod]
        public void ExportTruncationModeArgument()
        {
            var type = typeof(JobExportArgs.TruncationModeEnum);

            RunExportForEachEnum(
                type,
                (mode) => new JobExportArgs
                {
                    TruncationMode = 
                        (JobExportArgs.TruncationModeEnum)Enum.Parse(
                                type,
                                mode)
                });
        }

        /// <summary>
        /// Run export for each enum value in an enum type.
        /// </summary>
        /// <param name="enumType">The enum type</param>
        /// <param name="getJobExportArgs">
        /// The funtion to get arguments to run a job.
        /// </param>
        private void RunExportForEachEnum(
            Type enumType,
            Func<string, JobExportArgs> getJobExportArgs)
        {
            var service = Connect();

            ForEachEnum(
                enumType,
                (@enum) => service.Export(
                    Query, 
                    getJobExportArgs(@enum)));
        }

        /// <summary>
        /// Run a job for each enum value in an enum type.
        /// </summary>
        /// <param name="enumType">The enum type</param>
        /// <param name="getJobArgs">
        /// The funtion to get arguments to run a job.
        /// </param>
        private void RunJobForEachEnum(
            Type enumType,
            Func<string, JobArgs> getJobArgs)
        {
            var service = Connect();

            ForEachEnum(
                enumType,
                (@enum) =>
                    {
                        var job = this.Run(
                            service,
                            Query,
                            getJobArgs(@enum));

                        job.Cancel();
                    });
        }

        /// <summary>
        /// Perform an action for each enum value in an enum type.
        /// </summary>
        /// <param name="enumType">The enum type</param>
        /// <param name="action">
        /// The action to perform on an enum value
        /// </param>
        private static void ForEachEnum(
          Type enumType,
          Action<string> action)
        {
            var enums = Enum.GetNames(enumType);
            foreach (var @enum in enums)
            {
                action(@enum);
            }
        }
    }
}
