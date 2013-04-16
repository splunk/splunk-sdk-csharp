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

namespace Splunk.Examples.ModularInputs
{
    using System;
    using System.Collections.Generic;
    using Splunk.ModularInputs;
    
    /// <summary>
    /// A Splunk modular input executable.
    /// To install it, do the following:
    /// 1. Copy the inputs.conf.spec file (included in this project) 
    ///     into $SPLUNK_HOME\etc\apps\<app_name>\README\
    /// 2. Copy modular_input.exe (built by this project), and SplunkSDK.dll 
    ///     into $SPLUNK_HOME\etc\apps\<app_name>\bin\
    /// 3. Restart Splunk. 
    /// 4. Goto Manager>>Data inputs. You should see "C# SDK Example" listed there.
    /// Additionally, you can add per modular input (i.e. per scheme)
    /// default configuration, such as default host name and default index,
    /// by adding an inputs.conf file into $SPLUNK_HOME\etc\apps\<app_name>\default\
    /// </summary>
    internal class Program : Module
    {
        /// <summary>
        /// The main program
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static int Main(string[] args)
        {
            var module = new Program();

            // Create the Modular Input Scheme
            var scheme = new Scheme
            {
                Title = "C# SDK Example",
                Description = "This is a modular input example built using the C# SDK.",
                StreamingMode = StreamingMode.Xml,
                Endpoint =
                {
                    Arguments = new List<Argument>
                            {
                                new Argument
                                    {
                                        Name = "duration",
                                        Description = "Duration in seconds",
                                        DataType = DataType.Number,
                                        // Must be a positive integer.
                                        Validation = "is_pos_int('duration')",
                                        RequiredOnCreate = false
                                    },

                                new Argument
                                    {
                                        Name = "username",
                                        DataType = DataType.String,
                                        RequiredOnEdit = true
                                    },

                                new Argument
                                    {
                                        Name = "password",
                                        DataType = DataType.String,
                                        RequiredOnEdit = true
                                    }
                            }
                }
            };

            SystemLogger.Write("Calling Module.Dispatch");
            return module.Dispatch(args, scheme);
        }

        public override void StreamEvents(InputConfiguration inputConfiguration)
        {
            SystemLogger.Write(string.Format("Server Host: {0}", inputConfiguration.ServerHost));
            SystemLogger.Write(string.Format("Server URI : {0}", inputConfiguration.ServerUri));
            SystemLogger.Write(string.Format("Session Key: {0}", inputConfiguration.SessionKey));
            SystemLogger.Write(string.Format("Check  Dir : {0}", inputConfiguration.CheckpointDirectory));
            SystemLogger.Write(string.Format("Stanzas: {0}", inputConfiguration.Stanzas.Count));
            for (var i = 0; i < inputConfiguration.Stanzas.Count; i++)
            {
                SystemLogger.Write(string.Format(
                    "--- Stanza#{0}: {1} ---",
                    i,
                    inputConfiguration.Stanzas[i].Name));

                foreach (var t in inputConfiguration.Stanzas[i].Parameters)
                {
                    SystemLogger.Write(string.Format(
                        "Param {0}={1}",
                        t.Name,
                        t.Value));
                }
            }
            SystemLogger.Write("End of Stanzas Dump");

            // Write event to Splunk.
            Console.WriteLine("SDK Modular Input example.");
        }
    }
}