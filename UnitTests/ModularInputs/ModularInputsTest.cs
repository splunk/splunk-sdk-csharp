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
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk.ModularInputs;
    using System.Text;

    /// <summary>
    /// Test classes in Splunk.ModularInputs namespace
    /// </summary>
    [TestClass]
    public class ModularInputsTest
    {
        /// <summary>
        /// Input file folder
        /// </summary>
        private const string TestDataFolder = @"ModularInputs\Data";

        /// <summary>
        /// Input file containing input configuration
        /// </summary>
        private const string InputConfigurationFilePath = "InputConfiguration.xml";

        /// <summary>
        /// Input file containing validation items
        /// </summary>
        private const string ValidationItemsFilePath = "ValidationItems.xml";

        /// <summary>
        /// Input file containing scheme
        /// </summary>
        private const string SchemeFilePath = "Scheme.xml";

        /// <summary>
        /// Input file containing events
        /// </summary>
        private const string EventsFilePath = "Events.xml";

        private readonly static Scheme scheme = new Scheme
                {
                    Title = "Test Example",
                    Description = "This is a test modular input that handles all the appropriate functionality",
                    StreamingMode = StreamingMode.Xml,
                    Endpoint =
                    {
                        Arguments = new List<Argument>
                                        {
                                            new Argument
                                                {
                                                    Name = "interval",
                                                    Description = "Polling Interval",
                                                    DataType = DataType.Number,
                                                    Validation = "is_pos_int('interval')"
                                                },

                                            new Argument
                                                {
                                                    Name = "username",
                                                    Description = "Admin Username",
                                                    DataType = DataType.String,
                                                    RequiredOnCreate = false
                                                },

                                            new Argument
                                                {
                                                    Name = "password",
                                                    Description = "Admin Password",
                                                    DataType = DataType.String,
                                                    RequiredOnEdit = true
                                                }
                                        }
                    }
                };

        /// <summary>
        /// Test returning scheme through stdout
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            SchemeFilePath,
            TestDataFolder)]
        public void OutputScheme()
        {
            using (var consoleOut = new StringWriter())
            {
                Console.SetOut(consoleOut);
                Script.Run<TestScript>(new string[] { "--scheme" });
                AssertEqualWithExpectedFile(SchemeFilePath, consoleOut.ToString());
            }
        }

        /// <summary>
        /// Test getting validation info from stdin
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            ValidationItemsFilePath,
            TestDataFolder)]
        public void ExternalValidation()
        {
            using (var consoleIn = ReadFileFromDataFolderAsReaser(ValidationItemsFilePath))
            {
                SetConsoleIn(consoleIn);
                Script.Run<TestScript>(new string[] { "--validate-arguments" });
            }
        }

        /// <summary>
        /// Test getting validation info from stdin
        /// </summary>
        [TestMethod]
        [DeploymentItem(
            InputConfigurationFilePath,
            TestDataFolder)]
        [DeploymentItem(
            EventsFilePath,
            TestDataFolder)]
        public void StreamEvents()
        {
            using (var consoleIn = ReadFileFromDataFolderAsReaser(InputConfigurationFilePath))
            using (var consoleOut = new StringWriter())
            {
                SetConsoleIn(consoleIn);
                Console.SetOut(consoleOut);
                Script.Run<TestScript>(new string[] {});
                AssertEqualWithExpectedFile(EventsFilePath, consoleOut.ToString());
            }
        }

        /// <summary>
        /// Test error handling and logging
        /// </summary>
        [TestMethod]
        public void ErrorHandling()
        {
            using (var consoleIn = new StringReader(""))
            using (var consoleError = new StringWriter())
            {
                SetConsoleIn(consoleIn);
                Console.SetError(consoleError);
                var exitCode = Script.Run<TestScript>(new string[] { });

                // There will be an exception due to missing input configuration in 
                // (redirected) console stdin. 

                // Verify that an exception is logged with level FATAL.
                Assert.IsTrue(consoleError.ToString().Contains(
                    "FATAL Script.Run: Unhandled exception:"));

                // Verify that the exception is what we expect.
                Assert.IsTrue(consoleError.ToString().Contains("Root element is missing"));

                // Verify that an info level message is logged properly.
                Assert.IsTrue(consoleError.ToString().Contains("INFO Script.Run: Reading input definition"));

                Assert.AreNotEqual(0, exitCode);
            }
        }

        private static void AssertEqualWithExpectedFile(
              string expectedFilePath,
              string actual)
        {
            var expected = ReadFileFromDataFolderAsString(expectedFilePath);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Read file from data directory as a string
        /// </summary>
        /// <param name="relativePath">Relative path to the resource</param>
        /// <returns>Resource content</returns>
        private static string ReadFileFromDataFolderAsString(string relativePath)
        {
            return File.ReadAllText(GetDataFilePath(relativePath));
        }

        /// <summary>
        /// Read file from data directory as a test reader
        /// </summary>
        /// <param name="relativePath">Relative path to the resource</param>
        /// <returns>Resource content</returns>
        private static TextReader ReadFileFromDataFolderAsReaser(string relativePath)
        {
            return File.OpenText(GetDataFilePath(relativePath));
            //var utf8 = new UTF8Encoding();
            //return new StreamReader(
            //    GetDataFilePath(relativePath), 
            //    utf8, 
            //    false);
        }

        /// <summary>
        /// Get full path to the data file.
        /// </summary>
        /// <param name="relativePath">Relative path to the data folder.</param>
        /// <returns>A full path</returns>
        private static string GetDataFilePath(string relativePath)
        {
            return TestDataFolder + @"\" + relativePath;
        }
        
        /// <summary>
        /// Write events using EventStreamWriter
        /// </summary>
        // This method can be used by manual testing thus is public 
        public static void WriteEvents()
        {
            using (var writer = new EventStreamWriter())
            {
                var eventTemplate = new Event
                {
                    //Index = "sdk-tests2",
                    //Host = "test host",
                    //SourceType = "test sourcetype",
                    //Source = "test source",
                };

                WriteEventData(
                    writer,
                    eventTemplate,
                    "Event with all default fields set");

                WriteEventData(
                    writer,
                    eventTemplate,
                    "Letter O with double acute: \u0150");

                eventTemplate.Unbroken = true;

                WriteEventData(
                    writer,
                    eventTemplate,
                    "Part 1 of an unbroken event ");

                WriteEventData(
                    writer,
                    eventTemplate,
                    "Part 2 of an unbroken event ending with newline" + Environment.NewLine);

                WriteEventDone(
                      writer,
                      eventTemplate);

                eventTemplate.Unbroken = false;

                WriteEventData(
                    writer,
                    eventTemplate,
                    "Event after done key");

                var timedEvent = eventTemplate;
                timedEvent.Time = new DateTime(2013, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                timedEvent.Data = "Event with fixed time";
                writer.Write(timedEvent);

                WriteMultiplex(writer);
            }
        }

        private static void WriteMultiplex(EventStreamWriter writer)
        {
            var eventTemplate1 = new Event
            {
                Stanza = "modular_input://UnitTest1",
                Unbroken = true,
            };


            var eventTemplate2 = new Event
            {
                Stanza = "modular_input://UnitTest2",
                Unbroken = true,
            };

            WriteEventDataLine(writer, eventTemplate1, "Part 1 of channel 1 with a newline");
            WriteEventData(writer, eventTemplate2, "Part 1 of channel 2 without a newline ");

            // Mark the first channel done.
            WriteEventDone(writer, eventTemplate1);

            WriteEventDataLine(writer, eventTemplate1, "Part 2 of channel 1 with a newline");
            WriteEventDataLine(writer, eventTemplate2, "Part 2 of channel 2 with a newline");

            // Mark the second channel done.
            WriteEventDone(writer, eventTemplate2);
        }

        private static void WriteEventDone(EventStreamWriter writer, Event eventTemplate)
        {
            var @event = eventTemplate;
            @event.Unbroken = false;
            @event.Done = true;
            writer.Write(@event);
        }

        private static void WriteEventDataLine(
            EventStreamWriter writer,
            Event eventTemplate,
            string eventData)
        {
            WriteEventData(
                writer,
                eventTemplate,
                eventData + Environment.NewLine);
        }

        private static void WriteEventData(EventStreamWriter writer, Event eventTemplate, string eventData)
        {
            var @event = eventTemplate;
            @event.Data = eventData;
            writer.Write(@event);
        }
        
        private static void SetConsoleIn(TextReader consoleIn)
        {
            // Must set Console encoding to be UTF8. Otherwise, Script.Run
            // will call the setter of OutputEncoding which results in
            // resetting Console.In (which should be a System.Console bug).
            var utf8 = new System.Text.UTF8Encoding();
            Console.InputEncoding = utf8;
            Console.SetIn(consoleIn);
        }

        private class TestScript : Script
        {
            public override Scheme Scheme
            {
                get { return scheme; }
            }

            public override void StreamEvents(InputConfiguration inputConfiguration)
            {
                // Verify input configuration is received and parsed correctly.
                var reconstructed = inputConfiguration.Serialize();
                AssertEqualWithExpectedFile(InputConfigurationFilePath, reconstructed);
               
                // Write events through EventStreamWriter.
                ModularInputsTest.WriteEvents();
            }

            public override void Validate(ValidationItems validationItems)
            {
                var reconstructed = validationItems.Serialize();
                AssertEqualWithExpectedFile(ValidationItemsFilePath, reconstructed);
            }
        }
    }
}