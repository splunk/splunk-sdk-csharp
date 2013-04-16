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
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk.ModularInputs;

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
        /// Input file for the JSON test on Splunk Version 5
        /// </summary>
        private const string InputDefinitionFilePath = "InputDefinition.xml";

        /// <summary>
        /// Input file for the JSON test on Splunk Version 5
        /// </summary>
        private const string ValidationItemsFilePath = "ValidationItems.xml";

        /// <summary>
        /// Input file for the JSON test on Splunk Version 5
        /// </summary>
        private const string SchemeFilePath = "Scheme.xml";
 
        /// <summary>
        /// Test parsing of InputDefinition XML.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
                InputDefinitionFilePath,
                TestDataFolder)]
        public void ParseInputDefinition()
        {
            var reader = ReadFileFromDataFolderAsReaser(InputDefinitionFilePath);
            var inputDefinition = InputConfiguration.Read(
                reader);

            var original = ReadFileFromDataFolderAsString(InputDefinitionFilePath);
            var reconstructed = inputDefinition.Serialize();

            Assert.AreEqual(original, reconstructed);
        }

        /// <summary>
        /// Test parsing of InputDefinition XML.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
                ValidationItemsFilePath,
                TestDataFolder)]
        public void ParseValidationItems()
        {
            var reader = ReadFileFromDataFolderAsReaser(ValidationItemsFilePath);
            var validationItems = ValidationItems.Read(
                reader);

            var original = ReadFileFromDataFolderAsString(ValidationItemsFilePath);
            var reconstructed = validationItems.Serialize();

            Assert.AreEqual(original, reconstructed);
        }

        /// <summary>
        /// Test serialization of Scheme object.
        /// </summary>
        [TestMethod]
        [DeploymentItem(
                SchemeFilePath,
                TestDataFolder)]
        public void ConstructScheme()
        {
            var s = new Scheme
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

            using (var reader = new StringReader(s.Serialize()))
            {
                var actual = reader.ReadToEnd();
                var expected = ReadFileFromDataFolderAsString(SchemeFilePath);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        /// Test logging into Splunk diagnostics
        /// </summary>
        [TestMethod]
        public void SystemLogger()
        {
            var msg = "Test";
            var header = "FATAL ";
            var line = Splunk.ModularInputs.SystemLogger.Format(LogLevel.Fatal, msg);
            Assert.AreEqual(header + msg, line);

            // Test end to end without checking the result.
            Splunk.ModularInputs.SystemLogger.Write(msg);
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
    }
}