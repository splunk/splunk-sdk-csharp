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
        /// matches th desired value.
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="value">The desired event count value</param>
        /// <param name="seconds">The number seconds to poll</param>
        private void Wait_event_count(Index index, int value, int seconds)
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
        /// Tests the basic index functionality
        /// </summary>
        [TestMethod]
        public void Index()
        {
            Service service = Connect();
            DateTimeOffset offset = new DateTimeOffset(DateTime.Now);
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd'T'HH:mm:ss") +
                string.Format("{0}{1}", offset.Offset.Hours.ToString("D2"), offset.Offset.Minutes.ToString("D2"));

            ServiceInfo info = service.GetInfo();

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
                dummyString = idx.LastInitTime;
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

            if (!indexes.ContainsKey("sdk-tests"))
            {
                indexes.Create("sdk-tests");
                indexes.Refresh();
            }

            Assert.IsTrue(indexes.ContainsKey("sdk-tests"), assertRoot + "#1");

            Index index = indexes.Get("sdk-tests");

            // get old values, skip saving paths and things we cannot write
            Args restore = new Args();
            restore.Add("blockSignSize", index.BlockSignSize);
            restore.Add("frozenTimePeriodInSecs", index.FrozenTimePeriodInSecs);
            restore.Add("maxConcurrentOptimizes", index.MaxConcurrentOptimizes);
            restore.Add("maxDataSize", index.MaxDataSize);
            restore.Add("maxHotBuckets", index.MaxHotBuckets);
            restore.Add("maxHotIdleSecs", index.MaxHotIdleSecs);
            restore.Add("maxHotSpanSecs", index.MaxHotSpanSecs);
            restore.Add("maxMemMB", index.MaxMemMB);
            restore.Add("maxMetaEntries", index.MaxMetaEntries);
            restore.Add("maxTotalDataSizeMB", index.MaxTotalDataSizeMB);
            restore.Add("maxWarmDBCount", index.MaxWarmDBCount);
            restore.Add("minRawFileSyncSecs", index.MinRawFileSyncSecs);
            restore.Add("partialServiceMetaPeriod", index.PartialServiceMetaPeriod);
            restore.Add("quarantineFutureSecs", index.QuarantineFutureSecs);
            restore.Add("quarantinePastSecs", index.QuarantinePastSecs);
            restore.Add("rawChunkSizeBytes", index.RawChunkSizeBytes);
            restore.Add("rotatePeriodInSecs", index.RotatePeriodInSecs);
            restore.Add("serviceMetaPeriod", index.ServiceMetaPeriod);
            restore.Add("syncMeta", index.SyncMeta);
            restore.Add("throttleCheckPeriod", index.ThrottleCheckPeriod);

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
            index.Update(restore);
            index.Refresh();

            index.Clean(180);
            Assert.AreEqual(0, index.TotalEventCount, assertRoot + "#2");

            index.Disable();
            Assert.IsTrue(index.IsDisabled);

            this.SplunkRestart();

            service = this.Connect();
            index = service.GetIndexes().Get("sdk-tests");

            index.Enable();
            Assert.IsFalse(index.IsDisabled);

            // submit events to index
            index.Submit(now + " Hello World. \u0150");
            index.Submit(now + " Goodbye world. \u0150");
            this.Wait_event_count(index, 2, 30);
            Assert.AreEqual(2, index.TotalEventCount, assertRoot + "#3");

            // clean
            index.Clean(180);
            Assert.AreEqual(0, index.TotalEventCount, assertRoot + "#4");

            /*
            // stream events to index
            Socket socket = index.Attach();
            NetworkStream stream = new NetworkStream(socket);
            StreamWriter writer = new StreamWriter(stream);

            writer.Write(Encoding.UTF8.GetBytes(now + " Hello World again. \u0150\r\n"));
            writer.Write(Encoding.UTF8.GetBytes(now + " Goodbye World again.\u0150\r\n"));
            writer.Flush();
            writer.Close();
            socket.Close();

            wait_event_count(index, 2, 30);
            Assert.AreEqual(2, index.TotalEventCount, assertRoot + "#5");
            
            // clean
            index.Clean(180);
            Assert.AreEqual(0, index.TotalEventCount, assertRoot + "#6");
            */

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
        }
    }
}
