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
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// Application tests
    /// </summary>
    [TestClass]
    public class ApplicationTest : TestHelper
    {
        /// <summary>
        /// The assert root string
        /// </summary>
        private static string assertRoot = "Application assert: ";

        /// <summary>
        /// Cleans an application from Splunk -- requires a restart
        /// </summary>
        /// <param name="appName">The app name</param>
        /// <param name="service">The connected service</param>
        /// <returns>The new connection</returns>
        private Service CleanApp(string appName, Service service)
        {
            this.SplunkRestart();
            service = this.Connect();
            EntityCollection<Application> apps = service.GetApplications();
            apps.Remove(appName);
            this.SplunkRestart();
            return this.Connect();
        }

        /// <summary>
        /// The app tests
        /// </summary>
        [TestMethod]
        public void ApplicationTest1()
        {
            string dummyString;
            bool dummyBool;
            int dummyInt;
            Service service = Connect();

            EntityCollection<Application> apps = service.GetApplications();
            foreach (Application app in apps.Values)
            {
                try
                {
                    ApplicationSetup applicationSetup = app.Setup();
                    string setupXml = applicationSetup.SetupXml;
                }
                catch (Exception)
                {
                    // silent exception, if setup doesn't exist, exception occurs
                }

                ApplicationArchive applicationArchive = app.Archive();
                dummyString = app.Author;
                dummyBool = app.CheckForUpdates;
                dummyString = app.Description;
                dummyString = app.Label;
                dummyBool = app.Refreshes;
                dummyString = app.Version;
                dummyBool = app.IsConfigured;
                dummyBool = app.IsManageable;
                dummyBool = app.IsVisible;
                dummyBool = app.StateChangeRequiresRestart;
                ApplicationUpdate applicationUpdate = app.AppUpdate();
                dummyString = applicationUpdate.Checksum;
                dummyString = applicationUpdate.ChecksumType;
                dummyString = applicationUpdate.Homepage;
                dummyInt = applicationUpdate.UpdateSize;
                dummyString = applicationUpdate.UpdateName;
                dummyString = applicationUpdate.AppUrl;
                dummyString = applicationUpdate.Version;
                dummyBool = applicationUpdate.IsImplicitIdRequired;
            }

            if (apps.ContainsKey("sdk-tests"))
            {
                service = this.CleanApp("sdk-tests", service);
            }

            apps = service.GetApplications();
            Assert.IsFalse(apps.ContainsKey("sdk-tests"), assertRoot + "#1");

            Args createArgs = new Args();
            createArgs.Add("author", "me");
            if (service.VersionCompare("4.2.4") >= 0)
            {
                createArgs.Add("configured", false);
            }
            createArgs.Add("description", "this is a description");
            createArgs.Add("label", "SDKTEST");
            createArgs.Add("manageable", false);
            createArgs.Add("template", "barebones");
            createArgs.Add("visible", false);
            apps.Create("sdk-tests", createArgs);
            Assert.IsTrue(apps.ContainsKey("sdk-tests"), assertRoot + "#2");
            Application app2 = apps.Get("sdk-tests");

            dummyBool = app2.CheckForUpdates;
            Assert.AreEqual("SDKTEST", app2.Label, assertRoot + "#3");
            Assert.AreEqual("me", app2.Author, assertRoot + "#4");
            Assert.IsFalse(app2.IsConfigured, assertRoot + "#5");
            Assert.IsFalse(app2.IsManageable, assertRoot + "#6");
            Assert.IsFalse(app2.IsVisible, assertRoot + "#7");

            // update the app
            app2.Author = "not me";
            app2.Description = "new description";
            app2.Label = "new label";
            app2.IsVisible = false;
            app2.IsManageable = false;
            app2.Version = "5.0.0";
            app2.Update();

            // check to see if args took.
            Assert.AreEqual("not me", app2.Author, assertRoot + "#8");
            Assert.AreEqual("new description", app2.Description, assertRoot + "#9");
            Assert.AreEqual("new label", app2.Label, assertRoot + "#10");
            Assert.IsFalse(app2.IsVisible, assertRoot + "#11");
            Assert.AreEqual("5.0.0", app2.Version, assertRoot + "#12");

            // archive (package) the application
            ApplicationArchive appArchive = app2.Archive();
            Assert.IsTrue(appArchive.AppName.Length > 0, assertRoot + "#13");
            Assert.IsTrue(appArchive.FilePath.Length > 0, assertRoot + "#14");
            Assert.IsTrue(appArchive.Url.Length > 0, assertRoot + "#15");

            ApplicationUpdate appUpdate = app2.AppUpdate();
            Assert.IsTrue(appUpdate.ContainsKey("eai:acl"), assertRoot + "#16");

            service = this.CleanApp("sdk-tests", service);
            apps = service.GetApplications();
            Assert.IsFalse(apps.ContainsKey("sdk-tests"), assertRoot + "#17");
        }
    }
}
