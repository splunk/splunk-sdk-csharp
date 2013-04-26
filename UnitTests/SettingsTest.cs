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
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// This is the settingstest class
    /// </summary>
    [TestClass]
    public class SettingsTest : TestHelper
    {
        /// <summary>
        /// The base assert string
        /// </summary>
        private string assertRoot = "Settings assert: ";
        
        /// <summary>
        /// This method tests geting the events and then sets most, 
        /// and then reverts back to the original
        /// </summary>
        [TestMethod]
        public void Settings()
        {
            Service service = Connect();

            Settings settings = service.GetSettings();
            string dummyString;
            bool dummBool;
            int dummyInt;
            dummyString = settings.SplunkDB;
            dummyString = settings.SplunkHome;
            dummBool = settings.EnableSplunkWebSSL;
            dummyString = settings.Host;
            dummyInt = settings.HttpPort;
            dummyInt = settings.MgmtPort;
            dummyInt = settings.MinFreeSpace;
            dummyString = settings.Pass4SymmKey;
            dummyString = settings.ServerName;
            dummyString = settings.SessionTimeout;
            dummBool = settings.StartWebServer;
            dummyString = settings.TrustedIP;

            // set aside original settings
            string originalTimeout = settings.SessionTimeout;
            bool originalSSL = settings.EnableSplunkWebSSL;
            string originalHost = settings.Host;
            int originalHttpPort = settings.HttpPort;
            int originalMinSpace = settings.MinFreeSpace;
            //int originalMgmtPort = settings.MgmtPort();
            string originalServerName = settings.ServerName;
            bool originalStartWeb = settings.StartWebServer;

            // test update
            settings.EnableSplunkWebSSL = !originalSSL;
            bool updatedSSL = settings.EnableSplunkWebSSL;
            settings.Host = "sdk-host";
            settings.HttpPort = 8001;
            settings.MinFreeSpace = originalMinSpace - 100;
            //settings.MgmtHostPort(originalMgmtPort+1);
            settings.ServerName = "sdk-test-name";
            settings.SessionTimeout = "2h";
            //settings.StartWebServer(!originalStartWeb);
            settings.Update();

            // changing ports require a restart
            this.SplunkRestart();
            service = this.Connect();
            settings = service.GetSettings();

            Assert.AreNotEqual(originalSSL, updatedSSL, this.assertRoot + "#1");
            Assert.AreEqual("sdk-host", settings.Host, this.assertRoot + "#2");
            Assert.AreEqual(8001, settings.HttpPort, this.assertRoot + "#3");
            Assert.AreEqual(originalMinSpace - 100, settings.MinFreeSpace, this.assertRoot + "#4");
            Assert.AreEqual("sdk-test-name", settings.ServerName, this.assertRoot + "#5");
            Assert.AreEqual("2h", settings.SessionTimeout, this.assertRoot + "#6");
            //assertEquals(settings.StartWebServer(), !originalStartWeb);

            // restore original
            settings.EnableSplunkWebSSL = originalSSL;
            settings.Host = originalHost;
            settings.HttpPort = originalHttpPort;
            settings.MinFreeSpace = originalMinSpace;
            settings.ServerName = originalServerName;
            settings.SessionTimeout = originalTimeout;
            settings.StartWebServer = originalStartWeb;
            settings.Update();

            // changing ports require a restart
            this.SplunkRestart();
            service = this.Connect();
            settings = service.GetSettings();

            Assert.AreEqual(originalSSL, settings.EnableSplunkWebSSL, this.assertRoot + "#7");
            Assert.AreEqual(originalHost, settings.Host, this.assertRoot + "#8");
            Assert.AreEqual(originalHttpPort, settings.HttpPort, this.assertRoot + "#9");
            Assert.AreEqual(originalMinSpace, settings.MinFreeSpace, this.assertRoot + "#10");
            Assert.AreEqual(originalServerName, settings.ServerName, this.assertRoot + "#11");
            Assert.AreEqual(originalTimeout, settings.SessionTimeout, this.assertRoot + "#12");
            Assert.AreEqual(originalStartWeb, settings.StartWebServer, this.assertRoot + "#13");
        }
    }
}
