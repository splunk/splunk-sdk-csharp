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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// Test Event types.
    /// </summary>
    [TestClass]
    public class EventTypeTest : TestHelper
    {
        /// <summary>
        /// Assert string root
        /// </summary>
        private static string assertRoot = "Event Type assert: ";

        /// <summary>
        /// Touch all getters
        /// </summary>
        /// <param name="eventType">The event</param>
        private void CheckEventType(EventType eventType)
        {
            string dummystring = eventType.Description;
            int dummyInt = eventType.Priority;
            string dummystring2 = eventType.Search;
        }

        /// <summary>
        /// Check all events for getters.
        /// </summary>
        /// <param name="collection">The collection</param>
        private void CheckEventTypes(EventTypeCollection collection)
        {
            foreach (EventType eventType in collection.Values)
            {
                this.CheckEventType(eventType);
            }
        }

        /// <summary>
        /// Test the event types.
        /// </summary>
        [TestMethod]
        public void EventType()
        {
            Service service = Connect();

            EventTypeCollection eventTypeCollection = service.GetEventTypes();

            if (eventTypeCollection.ContainsKey("sdk-test"))
            {
                eventTypeCollection.Remove("sdk-test");
            }
            Assert.IsFalse(eventTypeCollection.ContainsKey("sdk-test"), assertRoot + "#1");

            this.CheckEventTypes(eventTypeCollection);

            string search = "index=_internal *";

            EventTypeArgs args = new EventTypeArgs();
            args.Description = "Dummy description";
            args.Disabled = true;
            args.Priority = 3;
            args.Priority = 2;
            EventType eventType = eventTypeCollection.Create("sdk-test", search, args);

            Assert.IsTrue(eventTypeCollection.ContainsKey("sdk-test"), assertRoot + "#2");

            Assert.AreEqual("sdk-test", eventType.Name, assertRoot + "#3");
            Assert.AreEqual(args["description"], eventType.Description, assertRoot + "#4");
            Assert.AreEqual(args["priority"], eventType.Priority, assertRoot + "#5");
            Assert.AreEqual(search, eventType.Search, assertRoot + "#6");
            Assert.IsTrue(eventType.IsDisabled, assertRoot + "#7");

            eventType.Description = "Dummy description a second time";
            eventType.Disabled = true;
            eventType.Priority = 3;
            int priority = eventType.Priority;
            eventType.Update();
            eventType.Enable();

            Assert.AreEqual("sdk-test", eventType.Name, assertRoot + "#8");
            Assert.AreEqual("Dummy description a second time", eventType.Description, assertRoot + "#9");
            Assert.AreEqual(3, priority, assertRoot + "#10");
            Assert.AreEqual("index=_internal *", eventType.Search, assertRoot + "#11");
            Assert.IsFalse(eventType.IsDisabled, assertRoot + "#12");

            eventTypeCollection.Remove("sdk-test");
            Assert.IsFalse(eventTypeCollection.ContainsKey("sdk-test"), assertRoot + "#13");
        }
    }
}