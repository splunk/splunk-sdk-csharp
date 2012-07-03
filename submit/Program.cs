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
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Splunk;
    using SplunkSDKHelper;

    /// <summary>
    /// An example program to submit events into splunk
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
            Command cli = Command.Splunk("submit");
            cli.Parse(argv);
            service = Service.Connect(cli.opts);

            Receiver receiver = new Receiver(service);
            receiver.Submit("Hello World. \u0150");
            receiver.Submit("Goodbye world. \u0150");
        }
    }
}
