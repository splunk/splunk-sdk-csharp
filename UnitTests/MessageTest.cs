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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// This class tests Splunk Messages
    /// </summary>
    [TestClass]
    public class MessageTest : TestHelper
    {
        /// <summary>
        /// The base assert string
        /// </summary>
        private string assertRoot = "Messages assert: ";

        /// <summary>
        /// Tests basic messages
        /// </summary>
        [TestMethod]
        public void Message()
        {
            Service service = Connect();

            MessageCollection messageCollection = service.GetMessages();

            if (messageCollection.ContainsKey("sdk-test-message1"))
            {
                messageCollection.Remove("sdk-test-message1");
            }

            Assert.IsFalse(messageCollection.ContainsKey("sdk-test-message1"), this.assertRoot + "#1");

            if (messageCollection.ContainsKey("sdk-test-message2"))
            {
                messageCollection.Remove("sdk-test-message2");
            }

            Assert.IsFalse(messageCollection.ContainsKey("sdk-test-message2"), this.assertRoot + "#2");

            messageCollection.Create("sdk-test-message1", "hello.");
            Assert.IsTrue(messageCollection.ContainsKey("sdk-test-message1"), this.assertRoot + "#3");

            Message message = messageCollection.Get("sdk-test-message1");
            Assert.AreEqual("sdk-test-message1", message.Key, this.assertRoot + "#4");
            Assert.AreEqual("hello.", message.Value, this.assertRoot + "#5");

            Args args2 = new Args();
            args2.Add("value", "world.");
            messageCollection.Create("sdk-test-message2", args2);
            Assert.IsTrue(messageCollection.ContainsKey("sdk-test-message2"), this.assertRoot + "#6");

            message = messageCollection.Get("sdk-test-message2");
            Assert.AreEqual("sdk-test-message2", message.Key, this.assertRoot + "#7");
            Assert.AreEqual("world.", message.Value, this.assertRoot + "#8");

            messageCollection.Remove("sdk-test-message1");
            Assert.IsFalse(messageCollection.ContainsKey("sdk-test-message1"), this.assertRoot + "#9");

            messageCollection.Remove("sdk-test-message2");
            Assert.IsFalse(messageCollection.ContainsKey("sdk-test-message2"), this.assertRoot + "#10");
        }
    }
}
