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

namespace Splunk
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// The Index class represents the Splunk DB/Index object.
    /// </summary>
    public class Index : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Index"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public Index(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the data retrieved from this
        /// index has been UTF8-encoded. Indexing performance degrades when this
        /// parameter is set to true.
        /// </summary>
        public bool AssureUTF8
        {
            get
            {
                return this.GetBoolean("assureUTF8");
            }

            set
            {
                this.SetCacheValue("assureUTF8", value);
            }
        }

        /// <summary>
        /// Gets the block signature database for this index.
        /// </summary>
        public string BlockSignatureDatabase
        {
            get
            {
                return this.GetString("blockSignatureDatabase");
            }
        }

        /// <summary>
        /// Gets or sets the block sign size for this index. This value defines 
        /// the number of events that make up a block for block signatures. A 
        /// value of 0 means block signing is disabled.
        /// </summary>
        public int BlockSignSize
        {
            get
            {
                return this.GetInteger("blockSignSize");
            }

            set
            {
                this.SetCacheValue("blockSignSize", value);
            }
        }

        /// <summary>
        /// Gets the total size of all bloom filter files.
        /// </summary>
        public int BloomfilterTotalSizeKB
        {
            get
            {
                return this.GetInteger("bloomfilterTotalSizeKB", 0);
            }
        }

        /// <summary>
        /// Gets or sets the suggestion Splunk bucket rebuild process for the 
        /// size of the time-series (tsidx) file to make.
        /// Caution: This is an advanced parameter. Inappropriate use of this 
        /// parameter causes splunkd to not start if rebuild is required. Do not
        /// set this parameter unless instructed by Splunk Support.
        /// This is introduced in Splunk 5.0. The default is 
        /// "auto".
        /// </summary>
        public string BucketRebuildMemoryHint
        {
            get
            {
                return this.GetString("bucketRebuildMemoryHint", null);
            }

            set
            {
                this.SetCacheValue("bucketRebuildMemoryHint", value);
            }
        }

        /// <summary>
        /// Gets the absolute file path to the cold database for this index. 
        /// This value may contain shell expansion terms.
        /// </summary>
        public string ColdPath
        {
            get
            {
                return this.GetString("coldPath", null);
            }
        }

        /// <summary>
        /// Gets the expanded absolute file path to the cold database for this
        /// index.
        /// </summary>
        public string ColdPathExpanded
        {
            get
            {
                return this.GetString("coldPath_expanded", null);
            }
        }

        /// <summary>
        /// Gets or sets the frozen archive destination path for this index.
        /// </summary>
        public string ColdToFrozenDir
        {
            get
            {
                return this.GetString("coldToFrozenDir", null);
            }

            set
            {
                this.SetCacheValue("coldToFrozenDir", value);
            }
        }

        /// <summary>
        /// Gets or sets the path to the archiving script. 
        /// </summary>
        public string ColdToFrozenScript
        {
            get
            {
                return this.GetString("coldToFrozenScript", null);
            }

            set
            {
                this.SetCacheValue("coldToFrozenScript", value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the raw data is compressed.
        /// </summary>
        public bool CompressRawdata
        {
            get
            {
                return this.GetBoolean("compressRawdata");
            }
        }

        /// <summary>
        /// Gets the current size of this index.
        /// </summary>
        public int CurrentDBSizeMB
        {
            get
            {
                return this.GetInteger("currentDBSizeMB");
            }
        }

        /// <summary>
        /// Gets the default index name of the Splunk instance.
        /// </summary>
        public string DefaultDatabase
        {
            get
            {
                return this.GetString("defaultDatabase");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether asnychronous "online fsck" 
        /// bucket repair is enabled. When this feature is enabled, you don't 
        /// have to wait for buckets to be repaired before starting Splunk, but 
        /// one might notice a slight degradation in performance as a result.
        /// </summary>
        public bool EnableOnlineBucketRepair
        {
            get
            {
                return this.GetBoolean("enableOnlineBucketRepair");
            }

            set
            {
                this.SetCacheValue("enableOnlineBucketRepair", value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether real-time search is enabled for this
        /// index.
        /// </summary>
        public bool EnableRealtimeSearch
        {
            get
            {
                return this.GetBoolean("enableRealtimeSearch");
            }
        }

        /// <summary>
        /// Gets or sets the maximum age for a bucket, after which the data in 
        /// this index rolls to frozen. If archiving is necessary for frozen 
        /// data, see the coldToFrozen attributes.
        /// </summary>
        public int FrozenTimePeriodInSecs
        {
            get
            {
                return this.GetInteger("frozenTimePeriodInSecs");
            }

            set
            {
                this.SetCacheValue("frozenTimePeriodInSecs", value);
            }
        }

        /// <summary>
        /// Gets the absolute path to both hot and warm buckets for this index.
        /// This value may contain shell expansion terms.
        /// </summary>
        public string HomePath
        {
            get
            {
                return this.GetString("homePath", null);
            }
        }

        /// <summary>
        /// Gets the expanded absolute path to both hot and warm buckets for 
        /// this index.
        /// </summary>
        public string HomePathExpanded
        {
            get
            {
                return this.GetString("homePath_expanded", null);
            }
        }

        /// <summary>
        /// Gets the number of threads for this index. Note can be "auto" 
        /// instead of a number.
        /// </summary>
        public string IndexThreads
        {
            get
            {
                return this.GetString("indexThreads");
            }
        }

        /// <summary>
        /// Gets the last initialization time for this index.
        /// </summary>
        public DateTime LastInitTime
        {
            get
            {
                return this.GetDate("lastInitTime", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Gets or sets if a warm or cold bucket is older than the specified 
        /// age, do not create or rebuild its bloomfilter. Specify 0 to never 
        /// rebuild bloomfilters.
        /// </summary>
        public string MaxBloomBackfillBucketAge
        {
            get
            {
                return this.GetString("maxBloomBackfillBucketAge");
            }

            set
            {
                this.SetCacheValue("maxBloomBackfillBucketAge", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of concurrent optimize processes 
        /// that can run against a hot bucket for this index.
        /// </summary>
        public int MaxConcurrentOptimizes
        {
            get
            {
                return this.GetInteger("maxConcurrentOptimizes");
            }

            set
            {
                this.SetCacheValue("maxConcurrentOptimizes", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum data size before triggering a roll from 
        /// hot to warm buckets for this index. Note that the maximum data size,
        /// in MB, or "auto" (which means 750MB), or "auto_high_volume" (which 
        /// means 10GB on a 64-bit system, or 1GB on a 32-bit system).
        /// </summary>
        public string MaxDataSize
        {
            get
            {
                return this.GetString("maxDataSize");
            }

            set
            {
                this.SetCacheValue("maxDataSize", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of hot buckets that can exist for 
        /// this index.
        /// </summary>
        public int MaxHotBuckets
        {
            get
            {
                return this.GetInteger("maxHotBuckets");
            }

            set
            {
                this.SetCacheValue("maxHotBuckets", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum lifetime of a hot bucket for this index. 
        /// If a hot bucket exceeds this value, Splunk rolls it to warm. 
        /// A value of 0 means an infinite lifetime.
        /// </summary>
        public int MaxHotIdleSecs
        {
            get
            {
                return this.GetInteger("maxHotIdleSecs");
            }

            set
            {
                this.SetCacheValue("maxHotIdleSecs", value);
            }
        }

        /// <summary>
        /// Gets or sets the upper bound of the target maximum timespan of 
        /// hot and warm buckets for this index.
        /// </summary>
        public int MaxHotSpanSecs
        {
            get
            {
                return this.GetInteger("maxHotSpanSecs");
            }

            set
            {
                this.SetCacheValue("maxHotSpanSecs", value);
            }
        }

        /// <summary>
        /// Gets or sets the amount of memory to allocate for buffering 
        /// a single .tsidx file into memory before flushing to disk. 
        /// </summary>
        public int MaxMemMB
        {
            get
            {
                return this.GetInteger("maxMemMB");
            }

            set
            {
                this.SetCacheValue("maxMemMB", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of unique lines that are allowed 
        /// in a bucket's .data files for this index. A value of 0 means 
        /// infinite lines.
        /// </summary>
        public int MaxMetaEntries
        {
            get
            {
                return this.GetInteger("maxMetaEntries");
            }

            set
            {
                this.SetCacheValue("maxMetaEntries", value);
            }
        }

        /// <summary>
        /// Gets the  maximum number of concurrent helper processes for this 
        /// index.
        /// </summary>
        public int MaxRunningProcessGroups
        {
            get
            {
                return this.GetInteger("maxRunningProcessGroups", 0);
            }
        }

        /// <summary>
        /// Gets timestamp of the newest event time in the index. If not 
        /// available, the Systems concept of minimum time is returned.
        /// </summary>
        public DateTime MaxTime
        {
            get
            {
                return this.GetDate("maxTime", DateTime.MinValue);
            }
        }

        /// <summary>
        /// Gets or sets the Upper limit, in seconds, on how long an event can 
        /// sit in raw slice. Applies only if replication is enabled for this 
        /// index, otherwise is ignored. If there are any acknowledged events 
        /// sharing this raw slice, this paramater does not apply. In this case,
        /// maxTimeUnreplicatedWithAcks applies. Highest legal value is 
        /// 2147483647. To disable this parameter, set to 0.
        /// Note: this is an advanced parameter. Understand the consequences 
        /// before changing. This is introduced in Splunk 5.0.
        /// </summary>
        public int MaxTimeUnreplicatedNoAcks
        {
            get
            {
                return this.GetInteger("maxTimeUnreplicatedNoAcks", -1);
            }
         
            set
            {
                this.SetCacheValue("maxTimeUnreplicatedNoAcks", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum size for this index. If an index
        /// grows larger than this value, the oldest data is frozen.
        /// </summary>
        public int MaxTotalDataSizeMB
        {
            get
            {
                return this.GetInteger("maxTotalDataSizeMB");
            }

            set
            {
                this.SetCacheValue("maxTotalDataSizeMB", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of warm buckets for this index. If 
        /// this value is exceeded, the warm buckets with the lowest value for 
        /// their latest times are moved to cold.
        /// </summary>
        public int MaxWarmDBCount
        {
            get
            {
                return this.GetInteger("maxWarmDBCount");
            }

            set
            {
                this.SetCacheValue("maxWarmDBCount", value);
            }
        }

        /// <summary>
        /// Gets the memory pool size for this index. Note that the value can
        /// be a number or "auto".
        /// </summary>
        public string MemPoolMB
        {
            get
            {
                return this.GetString("memPoolMB");
            }
        }

        /// <summary>
        /// Gets or sets the frequency at which Splunkd forces a filesystem sync
        /// while compressing journal slices for this index. A value of 
        /// "disable" disables this feature completely, while a value of 0 
        /// forces a file-system sync after completing compression of every 
        /// journal slice.
        /// </summary>
        public string MinRawFileSyncSecs
        {
            get
            {
                return this.GetString("minRawFileSyncSecs");
            }

            set
            {
                this.SetCacheValue("minRawFileSyncSecs", value);
            }
        }

        /// <summary>
        /// Gets the timestamp of the oldest event time in the index. If not 
        /// available, the Systems concept of maximum time is returned.
        /// </summary>
        public DateTime MinTime
        {
            get
            {
                return this.GetDate("minTime", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Gets the number of hot buckets that were created for this index.
        /// </summary>
        public int NumHotBuckets
        {
            get
            {
                return this.GetInteger("numHotBuckets", 0);
            }
        }

        /// <summary>
        /// Gets the umber of warm buckets created for this index.
        /// </summary>
        public int NumWarmBuckets
        {
            get
            {
                return this.GetInteger("numWarmBuckets", 0);
            }
        }

        /// <summary>
        /// Gets the number of bloom filters created for this index.
        /// </summary>
        public int NumBloomfilters
        {
            get
            {
                return this.GetInteger("numBloomfilters", 0);
            }
        }

        /// <summary>
        /// Gets or sets the frequency, in seconds, at which metadata is for 
        /// partially synced (synced in-place) for this index. A value of 0 
        /// disables partial syncing, so metadata is only synced on the 
        /// ServiceMetaPeriod interval.
        /// </summary>
        public int PartialServiceMetaPeriod
        {
            get
            {
                return this.GetInteger("partialServiceMetaPeriod");
            }

            set
            {
                this.SetCacheValue("partialServiceMetaPeriod", value);
            }
        }

        /// <summary>
        /// Gets or sets the future event-time quarantine for this index. Events
        /// that are newer than now plus this value are quarantined.
        /// </summary>
        public int QuarantineFutureSecs
        {
            get
            {
                return this.GetInteger("quarantineFutureSecs");
            }

            set
            {
                this.SetCacheValue("quarantineFutureSecs", value);
            }
        }

        /// <summary>
        /// Gets or sets the past event-time quarantine for this index. Events
        /// that are older than now minus this value are quarantined.
        /// </summary>
        public int QuarantinePastSecs
        {
            get
            {
                return this.GetInteger("quarantinePastSecs");
            }

            set
            {
                this.SetCacheValue("quarantinePastSecs", value);
            }
        }

        /// <summary>
        /// Gets or sets the target uncompressed size of individual raw slices 
        /// in the raw data journal for this index. WARNING:This is an advanced 
        /// parameter. Only change it if you are instructed to do so by Splunk 
        /// Support.
        /// </summary>
        public int RawChunkSizeBytes
        {
            get
            {
                return this.GetInteger("rawChunkSizeBytes");
            }

            set
            {
                this.SetCacheValue("rawChunkSizeBytes", value);
            }
        }

        /// <summary>
        /// Gets or sets the replication factor. Valid Values are either a 
        /// non-negative number or "auto." This parameter only applies to Splunk
        /// clustering slaves. "auto": Use the value as configured with the 
        /// master. "0": Specify zero to turn off replication for this index. 
        /// This is introduced in Splunk 5.0.
        /// </summary>
        public string ReplicationFactor
        {
            get
            {
                return this.GetString("repFactor", null);
            }

            set
            {
                this.SetCacheValue("repFactor", value);
            }
        }

        /// <summary>
        /// Gets or sets the frequency to check for the need to create a new hot
        /// bucket and the need to roll or freeze any warm or cold buckets for 
        /// this index.
        /// </summary>
        public int RotatePeriodInSecs
        {
            get
            {
                return this.GetInteger("rotatePeriodInSecs");
            }

            set
            {
                this.SetCacheValue("rotatePeriodInSecs", value);
            }
        }

        /// <summary>
        /// Gets or sets the frequency at which metadata is synced to disk for 
        /// this index.
        /// </summary>
        public int ServiceMetaPeriod
        {
            get
            {
                return this.GetInteger("serviceMetaPeriod");
            }

            set
            {
                this.SetCacheValue("serviceMetaPeriod", value);
            }
        }

        /// <summary>
        /// Gets a comma separated list of indexes that suppress "index missing"
        /// messages.
        /// </summary>
        public string SuppressBannerList
        {
            get
            {
                return this.GetString("suppressBannerList", null);
            }
        }

        /// <summary>
        /// Gets a value that specifies the number of events that trigger the 
        /// indexer to sync events.
        /// </summary>
        public int Sync
        {
            get
            {
                return this.GetInteger("sync");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the sync operation is 
        /// invoked before the file descriptor is closed on metadata updates. 
        /// WARNING:This is an advanced parameter. Only change it if you are 
        /// instructed to do so by Splunk Support.
        /// </summary>
        public bool SyncMeta
        {
            get
            {
                return this.GetBoolean("syncMeta");
            }

            set
            {
                this.SetCacheValue("syncMeta", value);
            }
        }

        /// <summary>
        /// Gets the absolute path to the thawed index for this index. This 
        /// value may contain shell expansion terms.
        /// </summary>
        public string ThawedPath
        {
            get
            {
                return this.GetString("thawedPath", null);
            }
        }

        /// <summary>
        /// Gets the expanded absolute path to the thawed index for this index.
        /// </summary>
        public string ThawedPathExpanded
        {
            get
            {
                return this.GetString("thawedPath_expanded", null);
            }
        }

        /// <summary>
        /// Gets or sets the frequency at which Splunk checks for an index 
        /// throttling condition.
        /// </summary>
        public int ThrottleCheckPeriod
        {
            get
            {
                return this.GetInteger("throttleCheckPeriod");
            }

            set
            {
                this.SetCacheValue("throttleCheckPeriod", value);
            }
        }

        /// <summary>
        /// Gets the total event count for this index.
        /// </summary>
        public int TotalEventCount
        {
            get
            {
                return this.GetInteger("totalEventCount");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this index is an internal index.
        /// </summary>
        public bool IsInternal
        {
            get
            {
                return this.GetBoolean("isInternal");
            }
        }

        /// <summary>
        /// Creates a writable socket to this index.
        /// </summary>
        /// <returns>The network socket</returns>
        public Socket Attach()
        {
            Receiver receiver = this.Service.GetReceiver();
            return receiver.Attach(this.Name);
        }

        /// <summary>
        /// Creates a writable socket to this index, adding optional arguments.
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns>The network socket</returns>
        public Socket Attach(Args args)
        {
            Receiver receiver = this.Service.GetReceiver();
            return receiver.Attach(this.Name, args);
        }

        /// <summary>
        /// Cleans this index removing all events, with specific timeout value.
        /// -1 Waits forever. 
        /// </summary>
        /// <param name="maxSeconds">The maximum number of seconds to wait 
        /// before returning</param>
        /// <returns>The index</returns>
        public Index Clean(int maxSeconds)
        {
            Args saved = new Args();
            saved.Add("maxTotalDataSizeMB", this.MaxTotalDataSizeMB);
            saved.Add("frozenTimePeriodInSecs", this.FrozenTimePeriodInSecs);

            Args reset = new Args();
            reset.Add("maxTotalDataSizeMB", "1");
            reset.Add("frozenTimePeriodInSecs", "1");
            this.Update(reset);
            this.RollHotBuckets();

            while (maxSeconds != 0)
            {
                Thread.Sleep(1000); // 1000ms (1 second sleep)
                maxSeconds = maxSeconds - 1;
                if (this.TotalEventCount == 0)
                {
                    this.Update(saved);
                    return this;
                }
                this.Refresh();
            }
            throw new SplunkException(
                SplunkException.TIMEOUT, "Index cleaning timed out");
        }

        /// <summary>
        /// Performs rolling hot buckets for this index.
        /// </summary>
        public void RollHotBuckets()
        {
            ResponseMessage response = 
                this.Service.Post(this.Path + "/roll-hot-buckets");
            Debug.Assert(response.Status == 200, "Status not 200!");
        }

        /// <summary>
        /// Submits an event to this index through an HTTP POST request.
        /// </summary>
        /// <param name="data">The event data to submit</param>
        public void Submit(string data)
        {
            Receiver receiver = this.Service.GetReceiver();
            receiver.Submit(this.Name, data);
        }

        /// <summary>
        /// Submits an event to this index through an HTTP POST request.
        /// </summary>
        /// <param name="args">The optional arguments</param>
        /// <param name="data">The event data to submit</param>
        public void Submit(Args args, string data)
        {
            Receiver receiver = this.Service.GetReceiver();
            receiver.Submit(this.Name, args, data);
        }

        /// <summary>
        /// Uploads a file to this index as an event stream. Note: This file 
        /// must be directly accessible by the Splunk server, by the file-path
        /// supplied. 
        /// </summary>
        /// <param name="filepath">The file</param>
        public void Upload(string filepath)
        {
            EntityCollection<Upload> uploads = this.Service.GetUploads();
            Args args = new Args("index", this.Name);
            uploads.Create(filepath, args);
        }
    }
}
