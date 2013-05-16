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
    /// The <see cref="IndexArgs"/> class extends <see cref="Args"/> for <see
    /// cref="Index"/> creation-specific properties.
    /// </summary>
    public class IndexArgs : Args
    {
        /// <summary>
        /// Sets a value indicating whether all data retrieved from the index
        /// is proper UTF8. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// <b>Note:</b> When enabled indexing, performance will degrade. 
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// <remarks>
        public bool AssureUTF8
        {
            set
            {
                this["assureUTF8"] = value;
            }
        }

        /// <summary>
        /// Sets the block sign size for this index. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This value defines the number of events that make up a block for 
        /// block signatures. 
        /// <para>
        /// If this property is set to "0", block signing is disabled for this 
        /// index. 
        /// </para>
        /// <para>
        /// This property's recommended value is "100". 
        /// </para>
        /// <para>
        /// This property's default value is "0".
        /// </para>
        /// </remarks>
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
        /// </summary>
        /// <remarks>
        /// <para>
        /// <b>Caution:</b> This is an advanced parameter. Inappropriate use 
        /// of this parameter causes splunkd to not start if rebuild is 
        /// required. Do not set this parameter unless instructed by Splunk 
        /// Support.
        /// </para>
        /// <para>
        /// This property's default value is "auto". 
        /// </para>
        /// <para>
        /// This property is available starting in Splunk 5.0. 
        /// </para>
        /// </remarks>
        public string BucketRebuildMemoryHint
        {
            set
            {
                this["BucketRebuildMemoryHint"] = value;
            }
        }

        /// <summary>
        /// Sets an absolute filesystem path, local to the server, to
        /// the cold database for the index. The path must be both readable and 
        /// writable. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Cold databases are opened as needed when searching. May be
        /// defined in terms of a volume definition (see volume). 
        /// </para>
        /// <para>
        /// This property's value may contain shell expansion terms.
        /// </para>
        /// <para>
        /// Be aware that Splunk will not start if an index lacks a valid 
        /// <see cref="ColdPath"/>. 
        /// </para>
        /// </remarks>
        public string ColdPath
        {
            set
            {
                this["coldPath"] = value;
            }
        }

        /// <summary>
        /// Sets the destination filesystem path, local to the server, for the 
        /// frozen archive. Splunk automatically puts frozen buckets in this 
        /// directory.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use as an alternative to a <see cref="ColdToFrozenScript"/>.
        /// </para>
        /// <para>
        /// Bucket freezing policy is as follows:
        /// </para>
        /// <list type="bullet">
        /// <item> 
        /// New style buckets (4.2 and on): removes all files but the rawdata. 
        /// To thaw, run splunk rebuild "bucket dir" on the bucket, then move to 
        /// the thawed directory. 
        /// </item>
        /// <item>
        /// Old style buckets (Pre-4.2): gzip all the 
        /// .data and .tsidx files. To thaw, gunzip the zipped files and move 
        /// the bucket into the thawed directory for that index. 
        /// </item>
        /// </list>
        /// <para>
        /// <b>Note:</b> If both the <see cref="ColdToFrozenDir"/> 
        /// and <see cref="ColdToFrozenScript"/> properties are specified, the
        /// <see cref="ColdToFrozenDir"/> property takes precedence.
        /// </para>
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
        /// <para>
        /// Bucket freezing policy is as follows:
        /// </para>
        /// <list type="bullet">
        /// <item> 
        /// New style buckets (4.2 and on): removes all files but the rawdata. 
        /// To thaw, run splunk rebuild "bucket dir" on the bucket, then move to 
        /// the thawed directory. Use the <see cref="ColdToFrozenDir"/> unless 
        /// you need to perform a more advanced operation upon freezing buckets.
        /// </item>
        /// <item>
        /// Old style buckets (Pre-4.2): gzip all the 
        /// .data and .tsidx files. To thaw, gunzip the zipped files and move 
        /// the bucket into the thawed directory for that index.
        /// </item>
        /// </list>
        /// </remarks>
        public string ColdToFrozenScript
        {
            set
            {
                this["coldToFrozenScript"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether asynchronous (online fsck) bucket 
        /// repair is enabled. When enabled, bucket repair runs concurrently 
        /// with Splunk. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// When this feature is enabled, you don't have to wait for buckets
        /// to be repaired before starting Splunk, but you might notice a 
        /// slight degradation in performance as a result.
        /// </para>
        /// <para>
        /// This property's default value is "enabled".
        /// </para>
        /// </remarks>
        public bool EnableOnlineBucketRepair
        {
            set
            {
                this["enableOnlineBucketRepair"] = value;
            }
        }

        /// <summary>
        /// Sets the number of seconds after which indexed data rolls to frozen. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Freezing data means it is removed from the index. 
        /// If archiving is necessary for frozen data, see the ColdToFrozen 
        /// attributes, <see cref="ColdToFrozenDir"/> and 
        /// <see cref="ColdToFrozenScript"/>.
        /// </para>
        /// <para>
        /// This property's default value is "188697600" (6 years).
        /// </para>
        /// </remarks>
        /// <seealso cref="ColdToFrozenDir"/>
        /// <seealso cref="ColdToFrozenScript"/>
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
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value may contain shell expansion terms.
        /// </para>
        /// <para>
        /// <b>Note:</b> Splunk will not start if an index lacks a valid 
        /// <see cref="HomePath"/>.
        /// </para>
        /// </remarks>
        public string HomePath
        {
            set
            {
                this["homePath"] = value;
            }
        }

        /// <summary>
        /// Sets a time that indicates a bucket age. When a warm or
        /// cold bucket is older than this, Splunk does not create or rebuild
        /// its bloomfilter. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// The valid format is <i>number</i> followed by a time unit ("s", 
        /// "m", "h", or "d"). For example, "30d" indicates 30 days.
        /// </para>
        /// <para>
        /// When this property is set to "0", Splunk never rebuilds
        /// bloom filters.
        /// </para>
        /// <para>
        /// This property's default value is "30d".
        /// </para>
        /// </remarks>
        public string MaxBloomBackfillBucketAge
        {
            set
            {
                this["maxBloomBackfillBucketAge"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of concurrent optimize processes 
        /// that can run against a hot bucket for this index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This number should be increased from the 
        /// default only if instructed by Splunk Support.
        /// </para>
        /// <para>
        /// This property's default value is "3".
        /// </para>
        /// </remarks>
        public int MaxConcurrentOptimizes
        {
            set
            {
                this["maxConcurrentOptimizes"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum size in megabytes (MB) for a hot database to reach
        /// before triggering a roll from hot to warm buckets for this index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Acceptable values are also "auto" or "auto_high_volume". 
        /// </para>
        /// <list type="bullet">
        /// <item>"auto" sets the size to 750MB</item>
        /// <item>"auto_high_volume" sets the size to 10 gigabytes (GB) on
        /// 64-bit, and 1GB on 32-bit systems</item> 
        /// </list>
        /// <para>
        /// A "high volume index" would typically be considered one that gets 
        /// over 10GB of data per day. 
        /// </para>
        /// <para>
        /// Although the maximum value to which you can set this property is
        /// 1048576MB, which corresponds to 1 terabyte (TB), a reasonable
        /// number ranges anywhere from 100 to 50000. Any number outside this
        /// range should be approved by Splunk Support before proceeding. 
        /// </para>
        /// <para>
        /// If you specify an invalid number or string, <see
        /// cref="MaxDataSize"/> will be auto tuned. 
        /// </para>
        /// <para>
        /// <b>Note:</b> The precise size of your warm buckets may 
        /// vary from <see cref="MaxDataSize"/>, due to post-processing and 
        /// timing issues with the rolling policy.
        /// </para>
        /// <para>
        /// This property's default value is "auto".
        /// </para>
        /// </remarks>
        public string MaxDataSize
        {
            set
            {
                this["maxDataSize"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of hot buckets for this index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When <see cref="MaxHotBuckets"/> is exceeded, Splunk rolls the 
        /// least recently used (LRU) hot bucket to warm. Both normal hot 
        /// buckets and quarantined hot buckets count towards this total. 
        /// This setting operates independently of <see 
        /// cref="MaxHotIdleSecs"/>, which can also cause hot buckets to roll.
        /// </para>
        /// <para>
        /// This property's default value is "3". 
        /// </para>
        /// </remarks>
        public int MaxHotBuckets
        {
            set
            {
                this["maxHotBuckets"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum life of a hot bucket, in seconds. 
        /// </summary>
        /// <remarks>
        /// If a hot bucket exceeds this property's value, Splunk rolls
        /// the bucket to warm. This setting operates independently of <see 
        /// cref="MaxHotBuckets"/>, which can also cause hot buckets to roll.
        /// <para>
        /// A value of "0" turns off the idle check, and indicates an
        /// infinite lifetime.
        /// </para>
        /// <para>
        /// This property's default value is "0". 
        /// </para>
        /// </remarks>
        public int MaxHotIdleSecs
        {
            set
            {
                this["maxHotIdleSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the upper bound of target maximum timespan of hot and warm
        /// buckets for this index, in seconds.
        /// </summary>
        /// <remarks>
        /// <para>
        /// <b>Note:</b> If you set this property too small, you can get an 
        /// explosion of hot/warm buckets in the filesystem. The system sets 
        /// a lower bound implicitly for this parameter at 3600, but this is an 
        /// advanced parameter that should be set with care and understanding 
        /// of the characteristics of your data.
        /// </para>
        /// <para>
        /// This property's default value is "7776000" (90 days). 
        /// </para>
        /// </remarks>
        public int MaxHotSpanSecs
        {
            set
            {
                this["maxHotSpanSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the amount of memory, expressed in megabytes (MB), to 
        /// allocate for buffering a single tsidx file into memory before 
        /// flushing to disk. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value ("5") is recommended for all 
        /// environments.
        /// </para>
        /// <para>
        /// <b>Important:</b> Calculate this number carefully. Setting this 
        /// number incorrectly may have adverse effects on your system's memory 
        /// and splunkd stability and performance.
        /// </para>
        /// <para>
        /// This property's default value is "5". 
        /// </para>
        /// </remarks>
        public int MaxMemMB
        {
            set
            {
                this["maxMemMB"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of unique lines in .data files in a bucket,
        /// which may help to reduce memory consumption. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If set to "0", this setting is ignored (it is treated as
        /// infinite). 
        /// </para>
        /// <para>
        /// If this property's value is exceeded, a hot bucket is rolled to 
        /// prevent further increase. If your buckets are rolling due to 
        /// Strings.data hitting this limit, the culprit may be the punct field 
        /// in your data. If you don't use punct, it may be best to simply 
        /// disable this (see props.conf.spec in 
        /// $SPLUNK_HOME/etc/system/README). There is a small time delta 
        /// between when maximum is exceeded and bucket is rolled. This means
        /// a bucket may end up with epsilon more lines than specified, but
        /// this is not a major concern unless the excess is significant.
        /// </para>
        /// <para>
        /// This property's default value is "1000000". 
        /// </para>
        /// </remarks>
        public int MaxMetaEntries
        {
            set
            {
                this["maxMetaEntries"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum size of an index, in megabytes (MB).
        /// </summary>
        /// <remarks>
        /// <para>
        /// If an index grows larger than the maximum size, the oldest data is
        /// frozen.
        /// </para>
        /// <para>
        /// This property's default value is "500000".
        /// </para>
        /// </remarks>
        public int MaxTotalDataSizeMB
        {
            set 
            {
                this["maxTotalDataSizeMB"] = value;
            }
        }

        /// <summary>
        /// Sets the upper limit, in seconds, on how long an event can sit in 
        /// raw slice. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property applies only if replication is enabled for this 
        /// index; otherwise it is ignored. 
        /// </para>
        /// <para>
        /// If there are any acknowledged events sharing this raw slice, this
        /// property does not apply. In this case,
        /// <see cref="MaxTimeUnreplicatedWithAcks"/> applies. 
        /// </para>
        /// <para>
        /// This property's highest legal value is "2147483647".
        /// </para>
        /// <para>
        /// To disable this property, set it to "0".
        /// </para>
        /// <para>
        /// <b>Note:</b> This is an advanced property. Understand the 
        /// consequences before changing it. 
        /// </para>
        /// <para>
        /// This property is available starting in Splunk 5.0. 
        /// </para>
        /// </remarks>
        public int MaxTimeUnreplicatedNoAcks
        {
            set
            {
                this["MaxTimeUnreplicatedNoAcks"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of warm buckets for this index. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this property's value is exceeded, the warm buckets with the 
        /// lowest value for their latest times are moved to cold.
        /// </para>
        /// <para>
        /// This property's default value is "300".
        /// </para>
        /// </remarks>
        public int MaxWarmDBCount
        {
            set
            {
                this["maxWarmDBCount"] = value;
            }
        }

        /// <summary>
        /// Sets how frequently splunkd forces a filesystem sync while 
        /// compressing journal slices. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// During the interval specified by this property, uncompressed 
        /// slices are left on disk even after they are compressed. Then 
        /// splunkd forces a filesystem sync of the compressed journal and
        /// removes the accumulated uncompressed files.
        /// </para>
        /// <para>
        /// If this property is set to "0", splunkd forces a filesystem sync 
        /// after every slice completes compressing. 
        /// </para>
        /// <para>
        /// If this property is set to "disable", syncing is disabled entirely;
        /// uncompressed slices are removed as soon as compression is complete.
        /// </para>
        /// <para>
        /// <b>Note:</b> Some filesystems are very inefficient at performing
        /// sync operations, so only enable this if you are sure it is needed.
        /// </para>
        /// <para>
        /// This property's default value is "disable".
        /// </para>
        /// </remarks>
        public string MinRawFileSyncSecs
        {
            set
            {
                this["minRawFileSyncSecs"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the frequency, in seconds, at which metadata is  
        /// partially synced (synced in-place) for this index. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If set, this property's value indicates the frequency at which 
        /// metadata is synchronized, but only for records where the sync can
        /// be done efficiently in place, without requiring a full rewrite of
        /// the metadata file. Records that require a full rewrite are 
        /// synchronized at an interval defined by the <see 
        /// cref="ServiceMetaPeriod"/> property.
        /// </para>
        /// <para>
        /// When this property is set to a value of "0", partial syncing is
        /// disabled, so metadata is only synced on an interval defined by the
        /// <see cref="ServiceMetaPeriod"/> property.
        /// </para>
        /// <para>
        /// If the value of this property is greater than that of the 
        /// <see cref="ServiceMetaPeriod"/> property, then this property has no
        /// effect.
        /// </para>
        /// <para>
        /// This property's default value is "0".
        /// </para>
        /// </remarks>
        public int PartialServiceMetaPeriod
        {
            set
            {
                this["partialServiceMetaPeriod"] = value;
            }
        }

        /// <summary>
        /// Sets the future event-time quarantine for this index. 
        /// Events that are newer than now plus this value are quarantined.
        /// </summary>
        /// <remarks>
        /// This property's default value is "2592000" (30 days).
        /// </remarks>
        public int QuarantineFutureSecs
        {
            set
            {
                this["quarantineFutureSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the past event-time quarantine for this index. 
        /// Events that are older than now minus this value are quarantined.
        /// </summary>
        /// <remarks>
        /// This property's default value is "77760000" (900 days).
        /// </remarks>
        public int QuarantinePastSecs
        {
            set
            {
                this["quarantinePastSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the target uncompressed size of individual raw slices 
        /// in the raw data journal for this index. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// <b>Warning:</b> This is an advanced property. Only change it if 
        /// you are instructed to do so by Splunk Support.
        /// </para>
        /// <para>
        /// This property only specifies a target chunk size. The actual chunk
        /// size may be slightly larger by an amount proportional to an
        /// individual event size.
        /// </para>
        /// <para>
        /// This property's default value is 131072 (128 kilobytes (KB)).
        /// </para>
        /// </remarks>
        public int RawChunkSizeBytes
        {
            set
            {
                this["rawChunkSizeBytes"] = value;
            }
        }

        /// <summary>
        /// Sets the replication factor. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Valid values for this property are either a non-negative number
        /// or the "auto" keyword. 
        /// </para>
        /// <para>
        /// A value of "auto" instructs Splunk to use the value as configured
        /// with the master. 
        /// </para>
        /// <para>
        /// A value of "0" turns off replication for this index. 
        /// </para>
        /// <para>
        /// This parameter only applies to Splunk clustering slaves. 
        /// </para>
        /// <para>
        /// This property is available starting in Splunk 5.0.
        /// </para>
        /// </remarks>
        public string ReplicationFactor
        {
            set
            {
                this["repFactor"] = value;
            }
        }

        /// <summary>
        /// Sets or sets the frequency to check for the need to create a new 
        /// hot bucket and the need to roll or freeze any warm or cold buckets 
        /// for this index.
        /// </summary>
        public int RotatePeriodInSecs
        {
            set
            {
                this["rotatePeriodInSecs"] = value;
            }
        }

        /// <summary>
        /// Sets the frequency, in seconds, at which metadata is synced to 
        /// disk for this index.
        /// </summary>
        /// <remarks>
        /// <para>
        /// You may want to set this to a larger value if the sum of your
        /// metadata file sizes is larger than many tens of megabytes, to
        /// avoid the hit on I/O in the indexing fast path.
        /// </para>
        /// <para>
        /// This property's default value is "25".
        /// </para>
        /// </remarks>
        public int ServiceMetaPeriod
        {
            set
            {
                this["serviceMetaPeriod"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the sync 
        /// operation is invoked before the file descriptor is closed on 
        /// metadata updates. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This functionality improves integrity of metadata files, especially
        /// in regards to operating system crashes or machine failures.
        /// </para>
        /// <para>
        /// <b>Warning:</b> This is an advanced parameter. Only change it if
        /// you are instructed to do so by Splunk Support.
        /// </para>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// </remarks>
        public bool SyncMeta
        {
            set
            {
                this["syncMeta"] = value;
            }
        }

        /// <summary>
        /// Sets an absolute filesystem path, local to the server, that 
        /// contains the thawed (resurrected) databases for the index. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Be aware that Splunk will not start if an index lacks a 
        /// valid <see cref="ThawedPath"/>. 
        /// </para>
        /// <para>
        /// This property's value may contain shell expansion terms.
        /// </para>
        /// </remarks>
        public string ThawedPath
        {
            set
            {
                this["thawedPath"] = value;
            }
        }

        /// <summary>
        /// Sets the frequency at which Splunk checks for an index 
        /// throttling condition. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value should be changed from the default only 
        /// if instructed by Splunk Support.
        /// </para>
        /// <para>
        /// This property's default value is "15".
        /// </para>
        /// </remarks>
        public int ThrottleCheckPeriod
        {
            set
            {
                this["throttleCheckPeriod"] = value;
            }
        }
    }
}
