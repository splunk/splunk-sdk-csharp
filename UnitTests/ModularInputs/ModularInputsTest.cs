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
    using System.Xml.Serialization;
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
        private const string SchemeFilePath = "Scheme.xml";
 
        /// <summary>
        /// Test initialization and serialization of InputDefinition object.
        /// </summary>
        [TestMethod]
        public void ConstructInputDefinition()
        {
            var id = new InputDefinition
                {
                    ServerHost = "splunk-1",
                    ServerUri = "https://127.0.0.1:8089/",
                    SessionKey = "12345",
                    CheckpointDirectory = "C:\\Temp"
                };
            var ss = new InputDefinition.Stanza { Name = "myScheme://aaa" };
            ss.Parameters.Add(new InputDefinition.Parameter { Name = "f1", Value = "v1" });
            ss.Parameters.Add(new InputDefinition.Parameter { Name = "f2", Value = "v2" });
            ss.Parameters.Add(new InputDefinition.Parameter { Name = "f3", Value = "v3" });
            id.Stanzas.Add(ss);

            var id2 = InputDefinition.ReadInputDefinition(
                new StringReader(id.Serialize()));

            Assert.AreEqual(id.ServerHost, id2.ServerHost);
            Assert.AreEqual(id.ServerUri, id2.ServerUri);
            Assert.AreEqual(id.SessionKey, id2.SessionKey);
            Assert.AreEqual(id.CheckpointDirectory, id2.CheckpointDirectory);
            Assert.AreEqual(id.Stanzas.Count, id2.Stanzas.Count);
            Assert.AreEqual(id.Stanzas[0].Name, id2.Stanzas[0].Name);
            Assert.AreEqual(id.Stanzas[0].Parameters.Count, id2.Stanzas[0].Parameters.Count);
        }
        
        /// <summary>
        /// Test initialization and serialization of Scheme object.
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
                    StreamingMode = Scheme.StreamingModeEnum.Xml,
                    Endpoint =
                        {
                            Arguments = new List<Argument>
                                {
                                    new Argument
                                        {
                                            Name = "interval",
                                            Description = "Polling Interval",
                                            DataType = Argument.DataTypeEnum.Number,
                                            Validation = "is_pos_int('interval')"
                                        },

                                    new Argument
                                        {
                                            Name = "username",
                                            Description = "Admin Username",
                                            DataType = Argument.DataTypeEnum.String,
                                            RequiredOnCreate = false
                                        },

                                    new Argument
                                        {
                                            Name = "password",
                                            Description = "Admin Password",
                                            DataType = Argument.DataTypeEnum.String,
                                            RequiredOnEdit = true
                                        }
                                }
                        }
                };

            using (var reader = new StringReader(s.Serialize()))
            {
                var actual = reader.ReadToEnd();
                var expected = ReadFileFromDataFolder(SchemeFilePath);
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        /// Read file from data directory
        /// </summary>
        /// <param name="relativePath">Relative path to the resource</param>
        /// <returns>Resource content</returns>
        private static string ReadFileFromDataFolder(string relativePath)
        {
            return File.ReadAllText(TestDataFolder + @"\" + relativePath);
        }
    }
}