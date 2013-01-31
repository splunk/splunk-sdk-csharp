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

namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// Test ResourceCollection class.
    /// </summary>
    [TestClass]
    public class ResourceCollectionTest
    {
        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="ResourceCollectionTest"/> 
        /// class.
        /// </summary>
        public ResourceCollectionTest()
        {
        }

        /// <summary>
        /// Check that ordering of Add is preserved.
        /// </summary>
        [TestMethod]
        public void OrderedResourceDictionary()
        {
            var random = new Random();

            var input = Enumerable.Repeat<object>(null, 1000).Select(
                x => random.Next()).ToList();

            var resources = new 
                ResourceCollection<Entity>.OrderedResourceDictionary();
            
            foreach (var i in input)
            {
                var entity = new Entity(
                new Service("dummy", 0, HttpService.SchemeHttp),
                    // Encode i in EntityPath
                    i.ToString());
                
                var list = new List<Entity> { entity };
           
                resources.Add(i.ToString(), list);
            }

            // Get back what's encoded in Key string.
            CollectionAssert.AreEqual(
                input,
                resources.Keys.Select(x => Convert.ToInt32(x)).ToList());

            var output = resources.Values.Select(x => Convert.ToInt32(
                // Get back what's encoded in Entity.Path.
                x[0].Path.Substring(10))).ToList();

            CollectionAssert.AreEqual(input, output);
          }
    }
}
