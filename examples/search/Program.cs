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

namespace SplunkSearch
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// The search program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main program
        /// </summary>
        /// <param name="argv">The command line arguments</param>
        public static void Main(string[] argv) 
        {
            Service service;
            Args args = new Args();
            Command cli = Command.Splunk("search");
            cli.AddRule("search", typeof(string), "search string");
            cli.Parse(argv);
            if (!cli.Opts.ContainsKey("search"))
            {
                System.Console.WriteLine("Search query string required, use --search=\"query\"");
                Environment.Exit(1);
            }

            service = Service.Connect(cli.Opts);
            JobCollection jobs = service.GetJobs();
            Job job = jobs.Create((string)cli.Opts["search"]);
            while (!job.IsDone) 
            {
                Thread.Sleep(1000);
            }

            // hard-code output args to json (use reader) and count is 0.
            Args outArgs = new Args("output_mode", "json");
            outArgs.Add("count", "0");
            using (Stream stream = job.Results(outArgs))
            {
                using (ResultsReaderJson rr = new ResultsReaderJson(stream))
                {
                    foreach (var map in rr)
                    {
                        System.Console.WriteLine("EVENT:");
                        foreach (string key in map.Keys)
                        {
                            System.Console.WriteLine(
                                "   " + key + " -> " + map[key]);
                        }
                    }
                    job.Cancel();
                }
            }
        }
    }
}
