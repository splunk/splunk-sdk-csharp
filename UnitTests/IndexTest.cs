﻿/*
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
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// Tests the Index class
    /// </summary>
    [TestClass]
    public class IndexTest : TestHelper
    {
        /// <summary>
        /// The assert root
        /// </summary>
        private static string assertRoot = "Index assert: ";

        /// <summary>
        /// Polls the index until wither time runs down, or the event count
        /// matches the desired value.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="value">The desired event count value</param>
        /// <param name="seconds">The number seconds to poll</param>
        private void WaitUntilEventCount(Index index, int value, int seconds)
        {
            while (seconds > 0)
            {
                try
                {
                    Thread.Sleep(1000); // 1000ms (1 second sleep)
                    seconds = seconds - 1;
                    if (index.TotalEventCount == value)
                    {
                        return;
                    }

                    index.Refresh();
                }
                catch (Exception)
                {
                    return;
                }
            }
        }

        /// <summary>
        /// Tests the basic getters and setters of index
        /// </summary>
        [TestMethod]
        public void IndexAccessors()
        {
            string indexName = "sdk-tests2";
            Service service = Connect();
            DateTimeOffset offset = new DateTimeOffset(DateTime.Now);
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss") +
                string.Format("{0}{1} ", offset.Offset.Hours.ToString("D2"), offset.Offset.Minutes.ToString("D2"));

            ServiceInfo info = service.GetInfo();

            // set can_delete if not set, so we can delete events from the index.
            User user = service.GetUsers().Get("admin");
            string[] roles = user.Roles;
            if (!this.Contains(roles, "can_delete"))
            {
                string[] newRoles = new string[roles.Length + 1];
                roles.CopyTo(newRoles, 0);
                newRoles[roles.Length] = "can_delete";
                user.Roles = newRoles;
                user.Update();
            }

            EntityCollection<Index> indexes = service.GetIndexes();
            foreach (Index idx in indexes.Values)
            {
                int dummyInt;
                string dummyString;
                bool dummyBool;
                DateTime dummyTime;
                dummyBool = idx.AssureUTF8;
                dummyString = idx.BlockSignatureDatabase;
                dummyInt = idx.BlockSignSize;
                dummyInt = idx.BloomfilterTotalSizeKB;
                dummyString = idx.ColdPath;
                dummyString = idx.ColdPathExpanded;
                dummyString = idx.ColdToFrozenDir;
                dummyString = idx.ColdToFrozenScript;
                dummyBool = idx.CompressRawdata;
                dummyInt = idx.CurrentDBSizeMB;
                dummyString = idx.DefaultDatabase;
                dummyBool = idx.EnableRealtimeSearch;
                dummyInt = idx.FrozenTimePeriodInSecs;
                dummyString = idx.HomePath;
                dummyString = idx.HomePathExpanded;
                dummyString = idx.IndexThreads;
                dummyTime = idx.LastInitTime;
                dummyString = idx.MaxBloomBackfillBucketAge;
                dummyInt = idx.MaxConcurrentOptimizes;
                dummyString = idx.MaxDataSize;
                dummyInt = idx.MaxHotBuckets;
                dummyInt = idx.MaxHotIdleSecs;
                dummyInt = idx.MaxHotSpanSecs;
                dummyInt = idx.MaxMemMB;
                dummyInt = idx.MaxMetaEntries;
                dummyInt = idx.MaxRunningProcessGroups;
                dummyTime = idx.MaxTime;
                dummyInt = idx.MaxTotalDataSizeMB;
                dummyInt = idx.MaxWarmDBCount;
                dummyString = idx.MemPoolMB;
                dummyString = idx.MinRawFileSyncSecs;
                dummyTime = idx.MinTime;
                dummyInt = idx.NumBloomfilters;
                dummyInt = idx.NumHotBuckets;
                dummyInt = idx.NumWarmBuckets;
                dummyInt = idx.PartialServiceMetaPeriod;
                dummyInt = idx.QuarantineFutureSecs;
                dummyInt = idx.QuarantinePastSecs;
                dummyInt = idx.RawChunkSizeBytes;
                dummyInt = idx.RotatePeriodInSecs;
                dummyInt = idx.ServiceMetaPeriod;
                dummyString = idx.SuppressBannerList;
                dummyInt = idx.Sync;
                dummyBool = idx.SyncMeta;
                dummyString = idx.ThawedPath;
                dummyString = idx.ThawedPathExpanded;
                dummyInt = idx.ThrottleCheckPeriod;
                dummyInt = idx.TotalEventCount;
                dummyBool = idx.IsDisabled;
                dummyBool = idx.IsInternal;
            }

            if (!indexes.ContainsKey(indexName))
            {
                indexes.Create(indexName);
                indexes.Refresh();
            }

            Assert.IsTrue(indexes.ContainsKey(indexName), assertRoot + "#1");

            Index index = indexes.Get(indexName);

            Args indexProperties = getIndexProperties(index);

            // use setters to update most
            index.BlockSignSize = index.BlockSignSize + 1;
            if (service.VersionCompare("4.3") > 0)
            {
                index.EnableOnlineBucketRepair = !index.EnableOnlineBucketRepair;
                index.MaxBloomBackfillBucketAge = "20d";
            }
            index.FrozenTimePeriodInSecs = index.FrozenTimePeriodInSecs + 1;
            index.MaxConcurrentOptimizes = index.MaxConcurrentOptimizes + 1;
            index.MaxDataSize = "auto";
            index.MaxHotBuckets = index.MaxHotBuckets + 1;
            index.MaxHotIdleSecs = index.MaxHotIdleSecs + 1;
            index.MaxMemMB = index.MaxMemMB + 1;
            index.MaxMetaEntries = index.MaxMetaEntries + 1;
            index.MaxTotalDataSizeMB = index.MaxTotalDataSizeMB + 1;
            index.MaxWarmDBCount = index.MaxWarmDBCount + 1;
            index.MinRawFileSyncSecs = "disable";
            index.PartialServiceMetaPeriod = index.PartialServiceMetaPeriod + 1;
            index.QuarantineFutureSecs = index.QuarantineFutureSecs + 1;
            index.QuarantinePastSecs = index.QuarantinePastSecs + 1;
            index.RawChunkSizeBytes = index.RawChunkSizeBytes + 1;
            index.RotatePeriodInSecs = index.RotatePeriodInSecs + 1;
            index.ServiceMetaPeriod = index.ServiceMetaPeriod + 1;
            index.SyncMeta = !index.SyncMeta;
            index.ThrottleCheckPeriod = index.ThrottleCheckPeriod + 1;
            index.Update();

            // check, then restore using map method
            index.Update(indexProperties);
            index.Refresh();

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            Assert.AreEqual(0, index.TotalEventCount, assertRoot + "#2");

            index.Disable();
            Assert.IsTrue(index.IsDisabled);

            // Restore original roles
            user.Roles = roles;
            user.Update();

            this.SplunkRestart();
        }

        /// <summary>
        /// Gets old values from given index, skip saving paths and things we cannot write
        /// </summary>
        /// <param name="index">The Index</param>
        /// <returns>The argument getIndexProperties</returns>
        private Args getIndexProperties(Index index)
        {
            Args indexProperties = new Args();

            indexProperties.Add("blockSignSize", index.BlockSignSize);
            indexProperties.Add("frozenTimePeriodInSecs", index.FrozenTimePeriodInSecs);
            indexProperties.Add("maxConcurrentOptimizes", index.MaxConcurrentOptimizes);
            indexProperties.Add("maxDataSize", index.MaxDataSize);
            indexProperties.Add("maxHotBuckets", index.MaxHotBuckets);
            indexProperties.Add("maxHotIdleSecs", index.MaxHotIdleSecs);
            indexProperties.Add("maxHotSpanSecs", index.MaxHotSpanSecs);
            indexProperties.Add("maxMemMB", index.MaxMemMB);
            indexProperties.Add("maxMetaEntries", index.MaxMetaEntries);
            indexProperties.Add("maxTotalDataSizeMB", index.MaxTotalDataSizeMB);
            indexProperties.Add("maxWarmDBCount", index.MaxWarmDBCount);
            indexProperties.Add("minRawFileSyncSecs", index.MinRawFileSyncSecs);
            indexProperties.Add("partialServiceMetaPeriod", index.PartialServiceMetaPeriod);
            indexProperties.Add("quarantineFutureSecs", index.QuarantineFutureSecs);
            indexProperties.Add("quarantinePastSecs", index.QuarantinePastSecs);
            indexProperties.Add("rawChunkSizeBytes", index.RawChunkSizeBytes);
            indexProperties.Add("rotatePeriodInSecs", index.RotatePeriodInSecs);
            indexProperties.Add("serviceMetaPeriod", index.ServiceMetaPeriod);
            indexProperties.Add("syncMeta", index.SyncMeta);
            indexProperties.Add("throttleCheckPeriod", index.ThrottleCheckPeriod);

            return indexProperties;
        }

        /// <summary>
        /// Tests submitting and streaming events to an index 
        /// and also removing all events from the index
        /// </summary>
        [TestMethod]
        public void IndexEvents()
        {
            string indexName = "sdk-tests2";
            DateTimeOffset offset = new DateTimeOffset(DateTime.Now);
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss") +
                string.Format("{0}{1} ", offset.Offset.Hours.ToString("D2"), offset.Offset.Minutes.ToString("D2"));

            Service service = this.Connect();
            ServiceInfo info = service.GetInfo();
            Index index = service.GetIndexes().Get(indexName);

            index.Enable();
            Assert.IsFalse(index.IsDisabled);

            // submit events to index
            index.Submit(now + " Hello World. \u0150");
            index.Submit(now + " Goodbye world. \u0150");
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, assertRoot + "#3");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            Assert.AreEqual(0, index.TotalEventCount, assertRoot + "#4");

            // stream events to index
            Stream stream = index.Attach();
            stream.Write(Encoding.UTF8.GetBytes(now + " Hello World again. \u0150\r\n"));
            stream.Write(Encoding.UTF8.GetBytes(now + " Goodbye World again.\u0150\r\n"));
            stream.Close();
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, assertRoot + "#5");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            index.Clean(180);
            Assert.AreEqual(0, index.TotalEventCount, assertRoot + "#6");

            string filename;
            if (info.OsName.Equals("Windows"))
            {
                filename = "C:\\Windows\\WindowsUpdate.log"; // normally here
            }
            else if (info.OsName.Equals("Linux"))
            {
                filename = "/var/log/syslog";
            }
            else if (info.OsName.Equals("Darwin"))
            {
                filename = "/var/log/system.log";
            }
            else
            {
                throw new Exception("OS: " + info.OsName + " not supported");
            }

            try
            {
                index.Upload(filename);
            }
            catch (Exception e)
            {
                throw new Exception("File " + filename + "failed to upload: Exception -> " + e.Message);
            }

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            Assert.AreEqual(0, index.TotalEventCount, assertRoot + "#7");
        }

        /// <summary>
        /// Tests submitting and streaming events to a default index 
        /// and also removing all events from the index
        /// </summary>
        [TestMethod]
        public void DefaultIndex()
        {
            string indexName = "main";
            DateTimeOffset offset = new DateTimeOffset(DateTime.Now);
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss") +
                string.Format("{0}{1} ", offset.Offset.Hours.ToString("D2"), offset.Offset.Minutes.ToString("D2"));

            Service service = this.Connect();
            Receiver receiver = service.GetReceiver();
            Index index = service.GetIndexes().Get(indexName);
            index.Enable();
            Assert.IsFalse(index.IsDisabled);

            // submit events to default index
            receiver.Log(now + " Hello World. \u0150");
            receiver.Log(now + " Goodbye World. \u0150");
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, "Expected the total event count to be 2");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            Assert.AreEqual(0, index.TotalEventCount, "Expcted the total event count to be 0");

            // stream event to default index
            Stream streamDefaultIndex = receiver.Attach();
            streamDefaultIndex.Write(Encoding.UTF8.GetBytes(now + " Hello World again. \u0150\r\n"));
            streamDefaultIndex.Write(Encoding.UTF8.GetBytes(now + " Goodbye World again.\u0150\r\n"));
            streamDefaultIndex.Close();
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, "Expected the total event count to be 2");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            index.Clean(180);
            Assert.AreEqual(0, index.TotalEventCount, "Expected the total event count to be 0");
        }

        /// <summary>
        /// Tests submitting and streaming events to an index given the indexProperties argument
        /// and also removing all events from the index
        /// </summary>
        [TestMethod]
        public void IndexArgs()
        {
            string indexName = "sdk-tests2";
            DateTimeOffset offset = new DateTimeOffset(DateTime.Now);
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss") +
                string.Format("{0}{1} ", offset.Offset.Hours.ToString("D2"), offset.Offset.Minutes.ToString("D2"));

            Service service = this.Connect();
            Index index = service.GetIndexes().Get(indexName);

            index.Enable();
            Assert.IsFalse(index.IsDisabled);

            Args indexProperties = getIndexProperties(index);

            // submit event to index using variable arguments
            index.Submit(indexProperties, now + " Hello World. \u0150");
            index.Submit(indexProperties, now + " Goodbye World. \u0150");
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, "Expected the total event count to be 2");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            Assert.AreEqual(0, index.TotalEventCount, "Expected the total event count to be 0");

            // stream event to index with variable arguments
            Stream streamArgs = index.Attach(indexProperties);
            streamArgs.Write(Encoding.UTF8.GetBytes(now + " Hello World again. \u0150\r\n"));
            streamArgs.Write(Encoding.UTF8.GetBytes(now + " Goodbye World again.\u0150\r\n"));
            streamArgs.Close();
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, "Expected the total event count to be 2");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            index.Clean(180);
            Assert.AreEqual(0, index.TotalEventCount, "Expected the total event count to be 0");
        }

        /// <summary>
        /// Test submitting and streaming to a default index given the indexProperties argument
        /// and also removing all events from the index
        /// </summary>
        [TestMethod]
        public void DefaultIndexArgs()
        {
            string indexName = "main";
            DateTimeOffset offset = new DateTimeOffset(DateTime.Now);
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss") +
                string.Format("{0}{1} ", offset.Offset.Hours.ToString("D2"), offset.Offset.Minutes.ToString("D2"));

            Service service = this.Connect();
            Receiver receiver = service.GetReceiver();
            Index index = service.GetIndexes().Get(indexName);

            index.Enable();
            Assert.IsFalse(index.IsDisabled);

            Args indexProperties = getIndexProperties(index);

            // submit event to default index using variable arguments
            receiver.Log(indexProperties, now + " Hello World. \u0150");
            receiver.Log(indexProperties, now + " Goodbye World. \u0150");
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, "Expected the total event count to be 2");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            Assert.AreEqual(0, index.TotalEventCount, "Expected the total event count to be 0");

            // stream event to default index with variable arguments
            Stream streamArgs = receiver.Attach(indexProperties);
            streamArgs.Write(Encoding.UTF8.GetBytes(now + " Hello World again. \u0150\r\n"));
            streamArgs.Write(Encoding.UTF8.GetBytes(now + " Goodbye World again.\u0150\r\n"));
            streamArgs.Close();
            this.WaitUntilEventCount(index, 2, 45);
            Assert.AreEqual(2, index.TotalEventCount, "Expected the total event count to be 2");

            service.Oneshot(string.Format("search index={0} * | delete", indexName));
            this.WaitUntilEventCount(index, 0, 45);
            index.Clean(180);
            Assert.AreEqual(0, index.TotalEventCount, "Expected the total event count to be 0");
        }
    }
}
