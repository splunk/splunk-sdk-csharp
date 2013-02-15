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

namespace SplunkSubmit
{
    using System;
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// An example program to submit events into Splunk.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main program
        /// </summary>
        /// <param name="argv">The command line arguments</param>
        public static void Main(string[] argv)
        {
            Command cli = Command.Splunk("submit");
            cli.Parse(argv);
            var service = Service.Connect(cli.Opts);

            var args = new Args 
            {
                { "source", "splunk-sdk-tests" },
                { "sourcetype", "splunk-sdk-test-event" }
            };

            Receiver receiver = new Receiver(service);

            // Submit to default index
            receiver.Submit(args, "Hello World.");
            receiver.Submit(args, "Goodbye world.");
        }
    }
}
