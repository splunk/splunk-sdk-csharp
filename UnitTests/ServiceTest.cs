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
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// This class tests all the Splunk Service methods.
    /// </summary>
    [TestClass]
    public class ServiceTest : TestHelper
    {
        /// <summary>
        /// The base assert string
        /// </summary>
        private string assertRoot = "Service tests: ";

        /// <summary>
        /// Tests the getting of service info (there are no set arguments)
        /// </summary>
        [TestMethod]
        public void ServiceInfoTest()
        {
            List<string> expected = new List<string> 
            {
                "build", "cpu_arch", "guid", "isFree", "isTrial", "licenseKeys",
                "licenseSignature", "licenseState", "master_guid", "mode",
                "os_build", "os_name", "os_version", "serverName", "version"
            };

            Service service = Connect();
            ServiceInfo info = service.GetInfo();

            // check for standard fields
            foreach (string name in expected) 
            {
                    Debug.Assert(info.ContainsKey(name), this.assertRoot + "#1");
            }

            bool dummyBool;
            int dummyInt;
            string[] dummyStrings;
            string dummyString;

            dummyInt = info.Build;
            dummyString = info.CpuArch;
            dummyString = info.Guid;
            dummyStrings = info.LicenseKeys;
            dummyStrings = info.LicenseLabels;
            dummyString = info.LicenseSignature;
            dummyString = info.LicenseState;
            dummyString = info.MasterGuid;
            dummyString = info.Mode;
            dummyString = info.OsBuild;
            dummyString = info.OsName;
            dummyString = info.OsVersion;
            dummyString = info.ServerName;
            dummyString = info.Version;
            dummyBool = info.IsFree;
            dummyBool = info.IsRtSearchEnabled;
            dummyBool = info.IsTrial;
        }
    }
}
