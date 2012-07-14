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
    /// Tests saved searches
    /// </summary>
    [TestClass]
    public class SavedSearchTest : TestHelper
    {
        /// <summary>
        /// The assert root string
        /// </summary>
        private string assertRoot = "Saved Search assert: ";

        /// <summary>
        /// Returns a value indicating whether a specific Job SID exists
        /// in the job history.
        /// </summary>
        /// <param name="history">The job history</param>
        /// <param name="sid">The SID</param>
        /// <returns>True or false</returns>
        private bool Contains(Job[] history, string sid)
        {
            for (int i = 0; i < history.Length; ++i)
            {
                if (history[i].Sid.Equals(sid))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Touch test properties
        /// </summary>
        [TestMethod]
        public void SavedSearches()
        {
            Service service = this.Connect();

            SavedSearchCollection savedSearches = service.GetSavedSearches();

            // Iterate saved searches and make sure we can read them.
            foreach (SavedSearch savedSearch in savedSearches.Values)
            {
                string dummyString;
                bool dummyBool;
                DateTime dummyDateTime;
                int dummyInt;

                // Resource properties
                dummyString = savedSearch.Name;
                dummyString = savedSearch.Title;
                dummyString = savedSearch.Path;

                // SavedSearch properties get
                dummyString = savedSearch.ActionEmailAuthPassword;
                dummyString = savedSearch.ActionEmailAuthUsername;
                dummyBool = savedSearch.ActionEmailSendResults;
                dummyString = savedSearch.ActionEmailBcc;
                dummyString = savedSearch.ActionEmailCc;
                dummyString = savedSearch.ActionEmailCommand;
                dummyString = savedSearch.ActionEmailFormat;
                dummyBool = savedSearch.ActionEmailInline;
                dummyString = savedSearch.ActionEmailMailServer;
                dummyInt = savedSearch.ActionEmailMaxResults;
                dummyString = savedSearch.ActionEmailMaxTime;
                dummyString = savedSearch.ActionEmailReportPaperOrientation;
                dummyString = savedSearch.ActionEmailReportPaperSize;
                dummyBool = savedSearch.ActionEmailReportServerEnabled;
                dummyString = savedSearch.ActionEmailReportServerUrl;
                dummyBool = savedSearch.ActionEmailSendPdf;
                dummyBool = savedSearch.ActionEmailSendResults;
                dummyString = savedSearch.ActionEmailSubject;
                dummyString = savedSearch.ActionEmailTo;
                dummyBool = savedSearch.ActionEmailTrackAlert;
                dummyString = savedSearch.ActionEmailTtl;
                dummyBool = savedSearch.ActionEmailUseSsl;
                dummyBool = savedSearch.ActionEmailUseTls;
                dummyBool = savedSearch.ActionEmailWidthSortColumns;
                dummyString = savedSearch.ActionPopulateLookupCommand;
                dummyString = savedSearch.ActionPopulateLookupDest;
                dummyString = savedSearch.ActionPopulateLookupHostname;
                dummyInt = savedSearch.ActionPopulateLookupMaxResults;
                dummyString = savedSearch.ActionPopulateLookupMaxTime;
                dummyBool = savedSearch.ActionPopulateLookupTrackAlert;
                dummyString = savedSearch.ActionPopulateLookupTtl;
                dummyString = savedSearch.ActionRssCommand;
                dummyString = savedSearch.ActionRssHostname;
                dummyInt = savedSearch.ActionRssMaxResults;
                dummyString = savedSearch.ActionRssMaxTime;
                dummyBool = savedSearch.ActionRssTrackAlert;
                dummyString = savedSearch.ActionRssTtl;
                dummyString = savedSearch.ActionScriptCommand;
                dummyString = savedSearch.ActionScriptFilename;
                dummyString = savedSearch.ActionScriptHostname;
                dummyInt = savedSearch.ActionScriptMaxResults;
                dummyString = savedSearch.ActionScriptMaxTime;
                dummyBool = savedSearch.ActionScriptTrackAlert;
                dummyString = savedSearch.ActionScriptTtl;
                dummyString = savedSearch.ActionSummaryIndexName;
                dummyString = savedSearch.ActionSummaryIndexCommand;
                dummyString = savedSearch.ActionSummaryIndexHostname;
                dummyBool = savedSearch.ActionSummaryIndexInline;
                dummyInt = savedSearch.ActionSummaryIndexMaxResults;
                dummyString = savedSearch.ActionSummaryIndexMaxTime;
                dummyBool = savedSearch.ActionSummaryIndexTrackAlert;
                dummyString = savedSearch.ActionSummaryIndexTtl;
                dummyBool = savedSearch.AlertDigestMode;
                dummyString = savedSearch.AlertExpires;
                dummyInt = savedSearch.AlertSeverity;
                dummyBool = savedSearch.AlertSuppress;
                dummyString = savedSearch.AlertSuppressFields;
                dummyString = savedSearch.AlertSuppressPeriod;
                dummyString = savedSearch.AlertTrack;
                dummyString = savedSearch.AlertComparator;
                dummyString = savedSearch.AlertCondition;
                dummyString = savedSearch.AlertThreshold;
                dummyString = savedSearch.AlertType;
                dummyString = savedSearch.CronSchedule;
                dummyString = savedSearch.Description;
                dummyInt = savedSearch.DispatchBuckets;
                dummyString = savedSearch.DispatchEarliestTime;
                dummyString = savedSearch.DispatchLatestTime;
                dummyBool = savedSearch.DispatchLookups;
                dummyInt = savedSearch.DispatchMaxCount;
                dummyString = savedSearch.DispatchMaxTime;
                dummyInt = savedSearch.DispatchReduceFreq;
                dummyBool = savedSearch.DispatchRtBackfill;
                dummyBool = savedSearch.DispatchSpawnProcess;
                dummyString = savedSearch.DispatchTimeFormat;
                dummyString = savedSearch.DispatchTtl;
                dummyString = savedSearch.DisplayView;
                dummyInt = savedSearch.MaxConcurrent;
                dummyDateTime = savedSearch.NextScheduledTime;
                dummyString = savedSearch.QualifiedSearch;
                dummyBool = savedSearch.RealtimeSchedule;
                dummyString = savedSearch.RequestUiDispatchApp;
                dummyString = savedSearch.RequestUiDispatchView;
                dummyBool = savedSearch.RestartOnSearchPeerAdd;
                dummyBool = savedSearch.RunOnStartup;
                dummyString = savedSearch.Search;
                dummyString = savedSearch.Vsid;
                dummyBool = savedSearch.IsActionEmail;
                dummyBool = savedSearch.IsActionPopulateLookup;
                dummyBool = savedSearch.IsActionRss;
                dummyBool = savedSearch.IsActionScript;
                dummyBool = savedSearch.IsActionSummaryIndex;
                dummyBool = savedSearch.IsDisabled;
                dummyBool = savedSearch.IsScheduled;
                dummyBool = savedSearch.IsVisible;
            }
        }

        /// <summary>
        /// Test Saved Search Create Read Update and Delete.
        /// </summary>
        [TestMethod]
        public void SavedSearchesCRUD()
        {
            Service service = this.Connect();

            SavedSearchCollection savedSearches = service.GetSavedSearches();

            // Ensure test starts in a known good state
            if (savedSearches.ContainsKey("sdk-test1"))
            {
                savedSearches.Remove("sdk-test1");
            }

            Assert.IsFalse(savedSearches.ContainsKey("sdk-test1"), this.assertRoot + "#1");

            SavedSearch savedSearch;
            string search = "search index=sdk-tests * earliest=-1m";

            // Create a saved search
            savedSearches.Create("sdk-test1", search);
            Assert.IsTrue(savedSearches.ContainsKey("sdk-test1"), this.assertRoot + "#2");

            // Read the saved search
            savedSearch = savedSearches.Get("sdk-test1");
            Assert.IsTrue(savedSearch.IsVisible, this.assertRoot + "#3");
            // CONSIDER: Test some additinal default property values.

            // Update search properties, but don't specify required args to test
            // pulling them from the existing object
            savedSearch.Update(new Args("is_visible", false));
            savedSearch.Refresh();
            Assert.IsFalse(savedSearch.IsVisible, this.assertRoot + "#4");

            // Delete the saved search
            savedSearches.Remove("sdk-test1");
            Assert.IsFalse(savedSearches.ContainsKey("sdk-test1"), this.assertRoot + "#5");

            // Create a saved search with some additional arguments
            savedSearch = savedSearches.Create("sdk-test1", search, new Args("is_visible", false));
            Assert.IsFalse(savedSearch.IsVisible, this.assertRoot + "#6");

            // set email params
            savedSearch.ActionEmailAuthPassword = "sdk-password";
            savedSearch.ActionEmailAuthUsername = "sdk-username";
            savedSearch.ActionEmailBcc = "sdk-bcc@splunk.com";
            savedSearch.ActionEmailCc = "sdk-cc@splunk.com";
            savedSearch.ActionEmailCommand = "$name1$";
            savedSearch.ActionEmailFormat = "text";
            savedSearch.ActionEmailFrom = "sdk@splunk.com";
            savedSearch.ActionEmailHostname = "dummy1.host.com";
            savedSearch.ActionEmailInline = true;
            savedSearch.ActionEmailMailServer = "splunk.com";
            savedSearch.ActionEmailMaxResults = 101;
            savedSearch.ActionEmailMaxTime = "10s";
            savedSearch.ActionEmailPdfView = "dummy";
            savedSearch.ActionEmailPreProcessResults = "*";
            savedSearch.ActionEmailReportPaperOrientation = "landscape";
            savedSearch.ActionEmailReportPaperSize = "letter";
            savedSearch.ActionEmailReportServerEnabled = false;
            savedSearch.ActionEmailReportServerUrl = "splunk.com";
            savedSearch.ActionEmailSendPdf = false;
            savedSearch.ActionEmailSendResults = false;
            savedSearch.ActionEmailSubject = "sdk-subject";
            savedSearch.ActionEmailTo = "sdk-to@splunk.com";
            savedSearch.ActionEmailTrackAlert = false;
            savedSearch.ActionEmailTtl = "61";
            savedSearch.ActionEmailUseSsl = false;
            savedSearch.ActionEmailUseTls = false;
            savedSearch.ActionEmailWidthSortColumns = false;
            savedSearch.ActionPopulateLookupCommand = "$name2$";
            savedSearch.ActionPopulateLookupDest = "dummypath";
            savedSearch.ActionPopulateLookupHostname = "dummy2.host.com";
            savedSearch.ActionPopulateLookupMaxResults = 102;
            savedSearch.ActionPopulateLookupMaxTime = "20s";
            savedSearch.ActionPopulateLookupTrackAlert = false;
            savedSearch.ActionPopulateLookupTtl = "62";
            savedSearch.ActionRssCommand = "$name3$";
            savedSearch.ActionRssHostname = "dummy3.host.com";
            savedSearch.ActionRssMaxResults = 103;
            savedSearch.ActionRssMaxTime = "30s";
            savedSearch.ActionRssTrackAlert = false;
            savedSearch.ActionRssTtl = "63";
            savedSearch.ActionScriptCommand = "$name4$";
            //savedSearch.ActionScriptFilename = String  filename;
            savedSearch.ActionScriptHostname = "dummy4.host.com";
            savedSearch.ActionScriptMaxResults = 104;
            savedSearch.ActionScriptMaxTime = "40s";
            savedSearch.ActionScriptTrackAlert = false;
            savedSearch.ActionScriptTtl = "64";
            savedSearch.ActionSummaryIndexName = "default";
            savedSearch.ActionSummaryIndexCommand = "$name5$";
            savedSearch.ActionSummaryIndexHostname = "dummy5.host.com";
            savedSearch.ActionSummaryIndexInline = false;
            savedSearch.ActionSummaryIndexMaxResults = 105;
            savedSearch.ActionSummaryIndexMaxTime = "50s";
            savedSearch.ActionSummaryIndexTrackAlert = false;
            savedSearch.ActionSummaryIndexTtl = "65";
            savedSearch.TriggerActions = "rss,email,populate_lookup,script,summary_index";
            savedSearch.Search = search;

            savedSearch.Update();

            // check
            Assert.IsTrue(savedSearch.IsActionEmail, this.assertRoot + "#7");
            Assert.IsTrue(savedSearch.IsActionPopulateLookup, this.assertRoot + "#8");
            Assert.IsTrue(savedSearch.IsActionRss, this.assertRoot + "#9");
            Assert.IsTrue(savedSearch.IsActionScript, this.assertRoot + "#10");
            Assert.IsTrue(savedSearch.IsActionSummaryIndex, this.assertRoot + "#11");

            Assert.AreEqual("sdk-password", savedSearch.ActionEmailAuthPassword, this.assertRoot + "#12");
            Assert.AreEqual("sdk-username", savedSearch.ActionEmailAuthUsername, this.assertRoot + "#13");
            Assert.AreEqual("sdk-bcc@splunk.com", savedSearch.ActionEmailBcc, this.assertRoot + "#14");
            Assert.AreEqual("sdk-cc@splunk.com", savedSearch.ActionEmailCc, this.assertRoot + "#15");
            Assert.AreEqual("$name1$", savedSearch.ActionEmailCommand, this.assertRoot + "#16");
            Assert.AreEqual("text", savedSearch.ActionEmailFormat, this.assertRoot + "#17");
            Assert.AreEqual("sdk@splunk.com", savedSearch.ActionEmailFrom, this.assertRoot + "#18");
            Assert.AreEqual("dummy1.host.com", savedSearch.ActionEmailHostname, this.assertRoot + "#19");
            Assert.IsTrue(savedSearch.ActionEmailInline, this.assertRoot + "#20");
            Assert.AreEqual("splunk.com", savedSearch.ActionEmailMailServer, this.assertRoot + "#21");
            Assert.AreEqual(101, savedSearch.ActionEmailMaxResults, this.assertRoot + "#22");
            Assert.AreEqual("10s", savedSearch.ActionEmailMaxTime, this.assertRoot + "#23");
            Assert.AreEqual("dummy", savedSearch.ActionEmailPdfView, this.assertRoot + "#24");
            Assert.AreEqual("*", savedSearch.ActionEmailPreProcessResults, this.assertRoot + "#25");
            Assert.AreEqual("landscape", savedSearch.ActionEmailReportPaperOrientation, this.assertRoot + "#26");
            Assert.AreEqual("letter", savedSearch.ActionEmailReportPaperSize, this.assertRoot + "#27");
            Assert.IsFalse(savedSearch.ActionEmailReportServerEnabled, this.assertRoot + "#28");
            Assert.AreEqual("splunk.com", savedSearch.ActionEmailReportServerUrl, this.assertRoot + "#29");
            Assert.IsFalse(savedSearch.ActionEmailSendPdf, this.assertRoot + "#30");
            Assert.IsFalse(savedSearch.ActionEmailSendResults, this.assertRoot + "#31");
            Assert.AreEqual("sdk-subject", savedSearch.ActionEmailSubject, this.assertRoot + "#32");
            Assert.AreEqual("sdk-to@splunk.com", savedSearch.ActionEmailTo, this.assertRoot + "#33");
            Assert.IsFalse(savedSearch.ActionEmailTrackAlert, this.assertRoot + "#34");
            Assert.AreEqual("61", savedSearch.ActionEmailTtl, this.assertRoot + "#35");
            Assert.IsFalse(savedSearch.ActionEmailUseSsl, this.assertRoot + "#36");
            Assert.IsFalse(savedSearch.ActionEmailUseTls, this.assertRoot + "#37");
            Assert.IsFalse(savedSearch.ActionEmailWidthSortColumns, this.assertRoot + "#38");
            Assert.AreEqual("$name2$", savedSearch.ActionPopulateLookupCommand, this.assertRoot + "#39");
            Assert.AreEqual("dummypath", savedSearch.ActionPopulateLookupDest, this.assertRoot + "#40");
            Assert.AreEqual("dummy2.host.com", savedSearch.ActionPopulateLookupHostname, this.assertRoot + "#41");
            Assert.AreEqual(102, savedSearch.ActionPopulateLookupMaxResults, this.assertRoot + "#42");
            Assert.AreEqual("20s", savedSearch.ActionPopulateLookupMaxTime, this.assertRoot + "#43");
            Assert.IsFalse(savedSearch.ActionPopulateLookupTrackAlert, this.assertRoot + "#44");
            Assert.AreEqual("62", savedSearch.ActionPopulateLookupTtl, this.assertRoot + "#45");
            Assert.AreEqual("$name3$", savedSearch.ActionRssCommand, this.assertRoot + "#46");
            Assert.AreEqual("dummy3.host.com", savedSearch.ActionRssHostname, this.assertRoot + "#47");
            Assert.AreEqual(103, savedSearch.ActionRssMaxResults, this.assertRoot + "#48");
            Assert.AreEqual("30s", savedSearch.ActionRssMaxTime, this.assertRoot + "#49");
            Assert.IsFalse(savedSearch.ActionRssTrackAlert, this.assertRoot + "#50");
            Assert.AreEqual("63", savedSearch.ActionRssTtl, this.assertRoot + "#51");
            Assert.AreEqual("$name4$", savedSearch.ActionScriptCommand, this.assertRoot + "#52");
            //savedSearch.ActionScriptFilename(String  filename);
            Assert.AreEqual("dummy4.host.com", savedSearch.ActionScriptHostname, this.assertRoot + "#53");
            Assert.AreEqual(104, savedSearch.ActionScriptMaxResults, this.assertRoot + "#54");
            Assert.AreEqual("40s", savedSearch.ActionScriptMaxTime, this.assertRoot + "#55");
            Assert.IsFalse(savedSearch.ActionScriptTrackAlert, this.assertRoot + "#56");
            Assert.AreEqual("64", savedSearch.ActionScriptTtl, this.assertRoot + "#57");
            Assert.AreEqual("default", savedSearch.ActionSummaryIndexName, this.assertRoot + "#58");
            Assert.AreEqual("$name5$", savedSearch.ActionSummaryIndexCommand, this.assertRoot + "#59");
            Assert.AreEqual("dummy5.host.com", savedSearch.ActionSummaryIndexHostname, this.assertRoot + "#60");
            Assert.IsFalse(savedSearch.ActionSummaryIndexInline, this.assertRoot + "#61");
            Assert.AreEqual(105, savedSearch.ActionSummaryIndexMaxResults, this.assertRoot + "#62");
            Assert.AreEqual("50s", savedSearch.ActionSummaryIndexMaxTime, this.assertRoot + "#63");
            Assert.IsFalse(savedSearch.ActionSummaryIndexTrackAlert, this.assertRoot + "#64");
            Assert.AreEqual("65", savedSearch.ActionSummaryIndexTtl, this.assertRoot + "#65");

            // Delete the saved search - using alternative method
            savedSearch.Remove();
            savedSearches.Refresh();
            Assert.IsFalse(savedSearches.ContainsKey("sdk-test1"), this.assertRoot + "#66");
        }
        
        /// <summary>
        /// Test saved search dispatch
        /// </summary>
        [TestMethod]
        public void SavedSearchDispatch()
        {
            Service service = this.Connect();

            SavedSearchCollection savedSearches = service.GetSavedSearches();

            // Ensure test starts in a known good state
            if (savedSearches.ContainsKey("sdk-test1"))
            {
                savedSearches.Remove("sdk-test1");
            }
            
            Assert.IsFalse(savedSearches.ContainsKey("sdk-test1"), this.assertRoot + "#67");

            // Create a saved search
            Job job;
            string search = "search index=sdk-tests * earliest=-1m";
            SavedSearch savedSearch = savedSearches.Create("sdk-test1", search);

            // Dispatch the saved search and wait for results.
            job = savedSearch.Dispatch();
            this.Wait(job);
            job.Results().Close();
            job.Cancel();

            // Dispatch with some additional search options
            job = savedSearch.Dispatch(new Args("dispatch.buckets", 100));
            this.Wait(job);
            job.Timeline().Close();
            job.Cancel();

            // Delete the saved search
            savedSearches.Remove("sdk-test1");
            Assert.IsFalse(savedSearches.ContainsKey("sdk-test1"), this.assertRoot + "#68");
        }

        /// <summary>
        /// Test saved search history
        /// </summary>
        [TestMethod]
        public void SavedSearchHistory()
        {
            Service service = this.Connect();

            SavedSearchCollection savedSearches = service.GetSavedSearches();

            // Ensure test starts in a known good state
            if (savedSearches.ContainsKey("sdk-test1"))
            {
                savedSearches.Remove("sdk-test1");
            }

            Assert.IsFalse(savedSearches.ContainsKey("sdk-test1"), this.assertRoot + "#69");

            string search = "search index=sdk-tests * earliest=-1m";

            // Create a saved search
            SavedSearch savedSearch = savedSearches.Create("sdk test1", search);

            // Clear the history - even though we have a newly create saved search
            // its possible there was a previous saved search with the same name
            // that had a matching history.
            Job[] history = savedSearch.History();
            foreach (Job job in history)
            {
                job.Cancel();
            }

            history = savedSearch.History();
            Assert.AreEqual(0, history.Length, this.assertRoot + "#70");

            Job job1 = savedSearch.Dispatch();
            this.Ready(job1);
            history = savedSearch.History();
            Assert.AreEqual(1, history.Length, this.assertRoot + "#71");
            Assert.IsTrue(this.Contains(history, job1.Sid));

            Job job2 = savedSearch.Dispatch();
            this.Ready(job2);
            history = savedSearch.History();
            Assert.AreEqual(2, history.Length, this.assertRoot + "#72");
            Assert.IsTrue(this.Contains(history, job1.Sid), this.assertRoot + "#73");
            Assert.IsTrue(this.Contains(history, job2.Sid), this.assertRoot + "#74");

            job1.Cancel();
            history = savedSearch.History();
            Assert.AreEqual(1, history.Length, this.assertRoot + "#75");
            Assert.IsTrue(this.Contains(history, job2.Sid), this.assertRoot + "#76");

            job2.Cancel();
            history = savedSearch.History();
            Assert.AreEqual(0, history.Length, this.assertRoot + "#77");

            // Delete the saved search
            savedSearches.Remove("sdk test1");
            Assert.IsFalse(savedSearches.ContainsKey("sdk test1"), this.assertRoot + "#78");
        }
    }
}