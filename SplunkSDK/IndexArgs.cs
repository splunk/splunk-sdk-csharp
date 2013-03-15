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

namespace Splunk
{
    /// <summary>
    /// The <see cref="IndexArgs"/> class extends <see cref="Args"/> for Index
    /// creation specific setters.
    /// </summary>
    public class IndexArgs : Args
    {
        /// <summary>
        /// Sets a value that indicates whether or not all data retrieved 
        /// from the index is proper UTF8. Note: when enabled indexing 
        /// performance will degrade. The default value is false.
        /// </summary>
        public bool AssureUTF8
        {
            set
            {
                this["assureUTF8"] = value;
            }
        }

        /// <summary>
        /// Sets a value that controls how many events make up a block for 
        /// block signatures. If set to 0, block signing is disabled for this 
        /// index. A recommended value is 100. The default is 0.
        /// </summary>
        public int BlockSignSize
        {
            set
            {
                this["blockSignSize"] = value;
            }
        }

        /// <summary>
        /// Sets the suggested Splunk bucket rebuild process for the size of 
        /// the time-series (tsidx) file to make.
        /// Caution: This is an advanced parameter. Inappropriate use of this 
        /// parameter causes splunkd to not start if rebuild is required. Do not
        /// set this parameter unless instructed by Splunk Support.
        /// The default is "auto". This was introduced in Splunk 5.0. 
        /// </summary>
        public string BucketRebuildMemoryHint
        {
            set
            {
                this["BucketRebuildMemoryHint"] = value;
            }
        }

        /// <summary>
        /// Sets an absolute filesystem path, local to the server, that contains
        /// the colddbs for the index. The path must be both readable and 
        /// writable. Cold databases are opened as needed when searching. May be
        /// defined in terms of a volume definition (see volume). 
        /// Note: Splunk will not start if an index lacks a valid coldPath. 
        /// </summary>
        public string ColdPath
        {
            set
            {
                this["coldPath"] = value;
            }
        }

        /// <summary>
        /// Sets the destination filesystem path, local to the server, for the 
        /// frozen archive. Use as an alternative to a coldToFrozenScript. 
        /// Splunk automatically puts frozen buckets in this directory.
        /// </summary>
        /// <remarks>
        /// Bucket freezing policy is as follows:
        /// New style buckets (4.2 and on): removes all files but the rawdata
        /// To thaw, run splunk rebuild "bucket dir" on the bucket, then move to 
        /// the thawed directory. Old style buckets (Pre-4.2): gzip all the 
        /// .data and .tsidx files. To thaw, gunzip the zipped files and move 
        /// the bucket into the thawed directory Note: If both coldToFrozenDir 
        /// and coldToFrozenScript are specified, coldToFrozenDir 
        /// takes precedence
        /// </remarks>
        public string ColdToFrozenDir
        {
            set
            {
                this["coldToFrozenDir"] = value;
            }
        }

        /// <summary>
        /// Sets the filesystem path, local to the server, that is the archiving
        /// script. 
        /// </summary>
        /// <remarks>
        /// By default, the example script has two possible behaviors 
        /// when archiving: For buckets created from version 4.2 and on, it 
        /// removes all files except for raw data. To thaw: cd to the frozen 
        /// bucket and type splunk rebuild, then copy the bucket to thawed for 
        /// that index. We recommend using the coldToFrozenDir parameter unless 
        /// you need to perform a more advanced operation upon freezing buckets.
        /// For older-style buckets, we simply gzip all the .tsidx files. To 
        /// thaw: cd to the frozen bucket and unzip the tsidx files, then copy 
        /// the bucket to thawed for that index.
        /// </remarks>
        public string ColdToFrozenScript
        {
            set
            {
                this["coldToFrozenScript"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether asynchronous (online fsck) bucket 
        /// repair is enabled. When enabled, bucket repair runs concurrently 
        /// with splunk. Note: a slight performance degradation may be 
        /// noticeable when enabled. The default value is enabled.
        /// </summary>
        public bool EnableOnlineBucketRepair
        {
            set
            {
                this["enableOnlineBucketRepair"] = value;
            }
        }

        /// <summary>
        /// Sets the number of seconds after which indexed data rolls to frozen. 
        /// The defaults is 188697600 (6 years). Freezing data means it is 
        /// removed from the index. If you need to archive your data, refer to 
        /// coldToFrozenDir and coldToFrozenScript parameter documentation.
        /// </summary>
        public int FrozenTimePeriodInSecs
        {
            set
            {
                this["frozenTimePeriodInSecs"] = value;
            }
        }

        /// <summary>
        /// Sets an absolute path that contains the hot and warm buckets for the
        /// index. The path must be both readable and writable. 
        /// Note: Splunk will not start if an index lacks a valid homePath.
        /// </summary>
        public string HomePath
        {
            set
            {
                this["homePath"] = value;
            }
        }

        /// <summary>
        /// Sets the age of a bucket that inhibits the creation or rebuilding of
        /// its bloomfilter. Specify 0 to never rebuild its bloomfilter. Valid 
        /// values are of the form: Valid values are: Integer[m|s|h|d]. The 
        /// default is 30d.
        /// </summary>
        public string MaxBloomBackfillBucketAge
        {
            set
            {
                this["maxBloomBackfillBucketAge"] = value;
            }
        }

        /// <summary>
        /// Sets the  number of concurrent optimize processes that can run 
        /// against a hot bucket. This number should be increased from the 
        /// default only if instructed by Splunk Support. The default is 3.
        /// </summary>
        public int MaxConcurrentOptimizes
        {
            set
            {
                this["maxConcurrentOptimizes"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum size in MB for a hot DB to reach before a roll to 
        /// warm is triggered. Note that acceptable values are also "auto" or 
        /// "auto_high_volume". The default value is "auto". 
        /// </summary>
        /// <remarks>        
        /// A "high volume index" 
        /// would typically be considered one that gets over 10GB of data per 
        /// day. "auto" sets the size to 750MB. "auto_high_volume" sets the size
        /// to 10GB on 64-bit, and 1GB on 32-bit systems. Although the maximum 
        /// value you can set this is 1048576 MB, which corresponds to 1 TB, a 
        /// reasonable number ranges anywhere from 100 - 50000. Any number 
        /// outside this range should be approved by Splunk Support before 
        /// proceeding. If you specify an invalid number or string, maxDataSize 
        /// will be auto tuned. Note: The precise size of your warm buckets may 
        /// vary from maxDataSize, due to post-processing and timing issues with
        /// the rolling policy.
        /// </remarks>
        public string MaxDataSize
        {
            set
            {
                this["maxDataSize"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of hot buckets for this index. The default 
        /// is 3. When maxHotBuckets is exceeded, Splunk rolls the least 
        /// recently used (LRU) hot bucket to warm. Both normal hot buckets and 
        /// quarantined hot buckets count towards this total. This setting 
        /// operates independently of maxHotIdleSecs, which can also cause
        /// hot buckets to roll.
        /// </summary>
        public int MaxHotBuckets
        {
            set
            {
                this["maxHotBuckets"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum life of a hot bucket, in seconds. If a hot bucket
        /// exceeds maxHotIdleSecs, Splunk rolls it to warm. This setting 
        /// operates independently of maxHotBuckets, which can also cause hot 
        /// buckets to roll. A value of 0 turns off the idle check. The
        /// default is 0.
        /// </summary>
        public int MaxHotIdleSecs
        {
            set
            {
                this["maxHotIdleSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the Upper bound of target maximum timespan of hot/warm buckets 
        /// in seconds. Default is 7776000 (90 days).
        /// Note: If you set this too small, you can get an explosion of 
        /// hot/warm buckets in the filesystem. The system sets a lower bound 
        /// implicitly for this parameter at 3600, but this is an advanced 
        /// parameter that should be set with care and understanding of 
        /// the characteristics of your data.
        /// </summary>
        public int MaxHotSpanSecs
        {
            set
            {
                this["maxHotSpanSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the amount of memory, expressed in MB, to allocate for 
        /// buffering a single tsidx file into memory before flushing to disk. 
        /// Default is 5. The default is recommended for all environments.
        /// IMPORTANT: Calculate this number carefully. Setting this number 
        /// incorrectly may have adverse effects on your systems memory and/or 
        /// splunkd stability/performance.
        /// </summary>
        public int MaxMemMB
        {
            set
            {
                this["maxMemMB"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of unique lines in .data files in a bucket,
        /// which may help to reduce memory consumption. The default value is
        /// 1000000.
        /// </summary>
        /// <remarks>
        /// If set to 0, this setting is ignored (it is treated as
        /// infinite). If exceeded, a hot bucket is rolled to prevent further
        /// increase. If your buckets are rolling due to Strings.data hitting
        /// this limit, the culprit may be the punct field in your data. If you
        /// don't use punct, it may be best to simply disable this (see
        /// props.conf.spec in $SPLUNK_HOME/etc/system/README).
        /// There is a small time delta between when maximum is exceeded and 
        /// bucket is rolled. This means a bucket may end up with epsilon more 
        /// lines than specified, but this is not a major concern unless excess 
        /// is significant.
        /// </remarks>
        public int MaxMetaEntries
        {
            set
            {
                this["maxMetaEntries"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum size of an index, in MB. If an index grows larger 
        /// than the maximum size, the oldest data is frozen. The default value  
        /// is 500000.
        /// </summary>
        public int MaxTotalDataSizeMB
        {
            set 
            {
                this["maxTotalDataSizeMB"] = value;
            }
        }

        /// <summary>
        /// Sets the Upper limit, in seconds, on how long an event can sit in 
        /// raw slice. Applies only if replication is enabled for this index,
        /// otherwise is ignored. 
        /// </summary>
        /// <remarks>
        /// If there are any acknowledged events sharing 
        /// this raw slice, this paramater does not apply. In this case,
        /// maxTimeUnreplicatedWithAcks applies. Highest legal value is 
        /// 2147483647. To disable this parameter, set to 0.
        /// Note: this is an advanced parameter. Understand the consequences 
        /// before changing. This is introduced in Splunk 5.0. 
        /// </remarks>
        public int MaxTimeUnreplicatedNoAcks
        {
            set
            {
                this["MaxTimeUnreplicatedNoAcks"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of warm buckets. If this number is 
        /// exceeded, the warm bucket/s with the lowest value for their latest 
        /// times will be moved to cold. The default value is 300.
        /// </summary>
        public int MaxWarmDBCount
        {
            set
            {
                this["maxWarmDBCount"] = value;
            }
        }

        /// <summary>
        /// Sets how frequently splunkd forces a filesystem sync while 
        /// compressing journal slices. During this interval, uncompressed 
        /// slices are left on disk even after they are compressed. Then splunkd
        /// forces a filesystem sync of the compressed journal and removes the 
        /// accumulated uncompressed files.
        /// </summary>
        /// <remarks>
        /// If 0 is specified, splunkd forces a filesystem sync after every 
        /// slice completes compressing. Specifying "disable" disables syncing 
        /// entirely: uncompressed slices are removed as soon as compression is 
        /// complete. Note: Some filesystems are very inefficient at performing 
        /// sync operations, so only enable this if you are sure it is needed.
        /// The default value is "disable".
        /// </remarks>
        public string MinRawFileSyncSecs
        {
            set
            {
                this["minRawFileSyncSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the metadata synchronization period, in seconds, but only for 
        /// for records where the sync can be done efficiently in-place, without 
        /// requiring a full re-write of the metadata file. Records that require
        /// full re-write are be sync'ed at serviceMetaPeriod. The default value
        /// is 0. Zero means that this feature is turned off and 
        /// serviceMetaPeriod is the only time when metadata sync happens.
        /// Note: If the value of partialServiceMetaPeriod is greater than 
        /// serviceMetaPeriod, this setting has no effect.
        /// </summary>
        public int PartialServiceMetaPeriod
        {
            set
            {
                this["partialServiceMetaPeriod"] = value;
            }
        }

        /// <summary>
        /// Sets the forward quarantined time, in seconds. Events with timestamp
        /// of quarantineFutureSecs newer than "now" are dropped into quarantine
        /// bucket. Default is 2592000 (30 days).
        /// </summary>
        public int QuarantineFutureSecs
        {
            set
            {
                this["quarantineFutureSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the backward quarantined time, in seconds. Events with 
        /// timestamp of quarantineFutureSecs older than "now" are dropped into 
        /// quarantine bucket. Default is 77760000 (900 days).
        /// </summary>
        public int QuarantinePastSecs
        {
            set
            {
                this["quarantinePastSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the uncompressed target size in bytes for individual raw slice
        /// in the rawdata journal of the index. The Default is 131072 (128KB). 
        /// If 0 is specified, rawChunkSizeBytes is set to the systemwide 
        /// default value. Note: rawChunkSizeBytes only specifies a target chunk
        /// size. The actual chunk size may be slightly larger by an amount 
        /// proportional to an individual event size. This value should be 
        /// changed from the default only if instructed by Splunk Support.
        /// </summary>
        public int RawChunkSizeBytes
        {
            set
            {
                this["rawChunkSizeBytes"] = value;
            }
        }

        /// <summary>
        /// Sets the replication factor. Valid Values are either a non-negative 
        /// number or "auto." This parameter only applies to Splunk clustering 
        /// slaves. "auto": Use the value as configured with the master.
        /// "0": Specify zero to turn off replication for this index. This is 
        /// introduced in Splunk 5.0. 
        /// </summary>
        public string ReplicationFactor
        {
            set
            {
                this["repFactor"] = value;
            }
        }

        /// <summary>
        /// Sets the hot bucket creation-check frequency, in seconds. This 
        /// setting also sets the frequency check for warm/cold buckets to 
        /// migrate to rolled/frozen buckets.
        /// </summary>
        public int RotatePeriodInSecs
        {
            set
            {
                this["rotatePeriodInSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the syncing metadata to disk frequency, in seconds. The default
        /// is 25 seconds. You may want to set this to a larger value if the 
        /// sum of your metadata file sizes is larger than many tens of 
        /// megabytes, to avoid the hit on I/O in the indexing fast path.
        /// </summary>
        public int ServiceMetaPeriod
        {
            set
            {
                this["serviceMetaPeriod"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether a sync operation is performed 
        /// before file descriptor is closed on  metadata file updates. This 
        /// functionality improves integrity of metadata files, especially in 
        /// regards to operating system crashes/machine failures. The default is
        /// true. This value should be changed from the default only if 
        /// instructed by Splunk Support.
        /// </summary>
        public bool SyncMeta
        {
            set
            {
                this["syncMeta"] = value;
            }
        }

        /// <summary>
        /// Sets an  absolute filesystem path, local to the server, that 
        /// contains the thawed (resurrected) databases for the index. Note: 
        /// Splunk will not start if an index lacks a valid thawedPath. 
        /// </summary>
        public string ThawedPath
        {
            set
            {
                this["thawedPath"] = value;
            }
        }

        /// <summary>
        /// Sets the index throttling check frequency in seconds. The default 
        /// is 15 seconds. This value should be changed from the  default only 
        /// if instructed by Splunk Support.
        /// </summary>
        public int ThrottleCheckPeriod
        {
            set
            {
                this["throttleCheckPeriod"] = value;
            }
        }
    }
}
