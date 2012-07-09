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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// This class tests the Splunk roles
    /// </summary>
    [TestClass]
    public class RoleTest : TestHelper
    {
        /// <summary>
        /// Test the basic getting of the roles
        /// </summary>
        [TestMethod]
        public void TestRoles1()
        {
            Service service = Connect();
            EntityCollection<Role> roles = service.GetRoles();

            foreach (Role role in roles)
            {
                string[] dummyStrings;
                string dummyString;
                int dummyInt;
                dummyStrings = role.Capabilities;
                dummyString = role.DefaultApp;
                dummyStrings = role.ImportedIndexesAllowed;
                dummyStrings = role.ImportedIndexesDefault;
                dummyStrings = role.ImportedRoles;
                dummyInt = role.ImportedRtSearchJobsQuota;
                dummyInt = role.ImportedSearchDiskQuota;
                dummyInt = role.RtSearchJobsQuota;
                dummyInt = role.SearchDiskQuota;
                dummyString = role.SearchFilter;
                dummyStrings = role.SearchIndexesAllowed;
                dummyStrings = role.SearchIndexesDefault;
                dummyInt = role.SearchJobsQuota;
                dummyInt = role.SearchTimeWin;
            }
        }
    }
}
