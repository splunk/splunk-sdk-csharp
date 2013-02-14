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
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// An example program to perform a real-time search.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main program
        /// </summary>
        /// <param name="argv">The command line arguments</param>
        public static void Main(string[] argv)
        {
            Command cli = Command.Splunk("search_realtime");
            cli.AddRule("search", typeof(string), "search string");
            cli.Parse(argv);
            if (!cli.Opts.ContainsKey("search"))
            {
                System.Console.WriteLine(
                    "Search query string required, use --search=\"query\"");
                Environment.Exit(1);
            }

            var service = Service.Connect(cli.Opts);

            // Realtime window is 5 minutes
            var queryArgs = new Args 
            { 
                { "search_mode", "realtime" }, 
                { "earliest_time", "rt-5m" }, 
                { "latest_time", "rt" }
            };

            var job = service.GetJobs().Create(
                (string)cli.Opts["search"], 
                queryArgs);

            var outputArgs = new Args
            {
                { "output_mode", "json" },

                // Return all entries.
                { "count", "0" }
            };

            for (var i = 0; i < 5; i++)
            {
                System.Console.WriteLine();
                System.Console.WriteLine();
                System.Console.WriteLine("Snapshot " + i + ":"); 
                
                using (var stream = job.ResultsPreview(outputArgs))
                {
                    var rr = new ResultsReaderJson(stream);

                    foreach (var map in rr)
                    {
                        System.Console.WriteLine("EVENT:");
                        foreach (string key in map.Keys)
                        {
                            System.Console.WriteLine(
                                "   " + key + " -> " + map[key]);
                        }
                    }

                    rr.Close();
                }
            }

            job.Cancel();
        }
    }
}