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

namespace Splunk.Examples.ListApps
{
    using System;
    using System.Linq;
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// An example program to list apps installed on the server.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main program
        /// </summary>
        public static void Main()
        {
            Command cli = Command.Splunk("list_apps");
            var service = Service.Connect(cli.Opts);

            Console.WriteLine("List of Apps:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();

            foreach (var app in service.GetApplications().Values)
            {
                Console.WriteLine(app.Name);
                
                // Write a seperator between the name and the description of an app.
                Console.WriteLine(
                    Enumerable.Repeat<char>('-', app.Name.Length).ToArray());

                Console.WriteLine(app.Description);

                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}
