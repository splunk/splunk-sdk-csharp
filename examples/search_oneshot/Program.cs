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

namespace Splunk.Examples.Search
{
    using System;
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// The search program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// An example program to perform a oneshot search.
        /// </summary>
        /// <param name="argv">The command line arguments</param>
        public static void Main(string[] argv)
        {
            var cli = Command.Splunk("search_oneshot");
            cli.AddRule("search", typeof(string), "search string");
            cli.Parse(argv);
            if (!cli.Opts.ContainsKey("search"))
            {
                System.Console.WriteLine(
                    "Search query string required, use --search=\"query\"");
                Environment.Exit(1);
            }

            var service = Service.Connect(cli.Opts);
       
            var outArgs = new Args
            {
                { "output_mode", "json" },

                // Return all entries.
                { "count", "0" }
            };

            using (var stream = service.Oneshot(
                (string)cli.Opts["search"], outArgs))
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
    }
}