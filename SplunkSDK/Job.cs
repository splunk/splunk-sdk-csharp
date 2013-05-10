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
    using System;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// The <see cref="Job" /> class represents a search Job.
    /// </summary>
    public class Job : Entity
    {
        /// <summary>
        /// Whether or not the job status can be queried.
        /// </summary>
        private bool isReady = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Job" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="path">The endpoint path.</param>
        public Job(Service service, string path) 
            : base(service, path) 
        {
        }

        /// <summary>
        /// Gets this job's name (its SID). 
        /// </summary>
		/// <remarks>
		/// Be aware that this getting this property may cause a refresh from 
		/// the server if the local copy is dirty.
		/// </remarks>
        public override string Name
        {
            get
            {
                this.CheckReady();
                return this.Sid;
            }
        }

        /// <summary>
        /// Gets or sets this job's priority. 
        /// </summary>
		/// <remarks>
		/// Be aware that this property has the side effect of setting the 
		/// priority immediately, and may if the data is dirty cause a 
		/// refreshing of the data when getting the priority.
		/// </remarks>
        public int Priority
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("priority");
            }

            set
            {
                this.Control("setpriority", new Args("priority", value));
            }
        }

        /// <summary>
        /// Gets this job's search title, which is the same as the 
        /// search string.
        /// </summary>
        public string Search
        {
            get
            {
                return this.Title;
            }
        }

        /// <summary>
        /// Gets the earliest time from which no events are later scanned. 
        /// </summary>
		/// <remarks>
		/// Use this property as a progress indicator.
		/// </remarks>
        public DateTime CursorTime
        {
            get
            {
                return this.GetDate("cursorTime");
            }
        }

        /// <summary>
        /// Gets the delegate for this job.
        /// </summary>
        public string Delegate 
        {
            get
            {
                this.CheckReady();
                return this.GetString("delegate", null);
            }
        }

        /// <summary>
        /// Gets the disk usage for this job. 
        /// </summary>
        public int DiskUsage
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("diskUsage");
            }
        }

        /// <summary>
        /// Gets the dispatch state for this job.
        /// </summary>
		/// <remarks>
		/// <para>
        /// Valid values are: QUEUED, PARSING, RUNNING, PAUSED, FINALIZING, FAILED, DONE
		/// </para>
        /// </remarks>
        public string DispatchState
        {
            get
            {
                this.CheckReady();
                return this.GetString("dispatchState");
            }
        }

        /// <summary>
        /// Gets the approximate progress of the job, in the range of 0.0 to 
        /// 1.0. 
        /// </summary>
		/// <remarks>
		/// <para>
		/// The <see cref="DoneProgress"/> property is calculated as follows:
		/// </para>
        /// <para>
        /// doneProgress = (latestTime - cursorTime) / 
		/// (latestTime - earliestTime)
        /// </para>
		/// </remarks>
        /// <seealso cref="LatestTime" />
        /// <seealso cref="CursorTime" />
        /// <seealso cref="EarliestTime" />
        public double DoneProgress
        {
            get
            {
                this.CheckReady();
                return this.GetFloat("doneProgress");
            }
        }

        /// <summary>
        /// Gets the number of possible events that were dropped due to the
        /// <c>rt_queue_size</c> (the number of events that the indexer should use
        /// for this search). Applicable for real-time searches only.
        /// </summary>
        public int DropCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("dropCount");
            }
        }

        /// <summary>
        /// Gets the earliest time a search job is configured to start.
        /// </summary>
        public DateTime EarliestTime 
        {
            get
            {
                return this.GetDate("earliestTime");
            }
        }

        /// <summary>
        /// Gets the count of events stored by search that are available to be
        /// retrieved from the events endpoint. 
        /// </summary>
        public int EventAvailableCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("eventAvailableCount");
            }
        }

        /// <summary>
        /// Gets the count of pre-transformed events generated by this search 
        /// job. 
        /// </summary>
        public int EventCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("eventCount");
            }
        }

        /// <summary>
        /// Gets the count of event fields.
        /// </summary>
        public int EventFieldCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("eventFieldCount");
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the events from this job are 
        /// available by streaming or not. 
        /// </summary>
        public bool EventIsStreaming 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("eventIsStreaming");
            }
        }

        /// <summary>
        /// Gets a value that indicates whether any events from this job have
        /// not been stored. 
        /// </summary>
        public bool EventIsTruncated 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("eventIsTruncated");
            }
        }

        /// <summary>
        /// Gets the subset of the entire search that is before any transforming 
        /// commands. 
        /// </summary>
		/// <remarks>
		/// The original search should be the <see cref="EventSearch"/> + 
		/// <see cref="ReportSearch"/>.
		/// </remarks>
        public string EventSearch 
        {
            get
            {
                this.CheckReady();
                return this.GetString("eventSearch", null);
            }
        }

        /// <summary>
        /// Gets a value that indicates how events are sorted. 
        /// </summary>
		/// <remarks>
		/// <para>
		/// Valid values are:
		/// </para>
		/// <para>
        /// <b>asc</b>: oldest first
		/// </para>
		/// <para>
 		/// <b>desc</b>: latest first
		/// </para>
		/// <para>
		/// <b>none</b>: not sorted
		/// </para>	
		/// </remarks>
        public string EventSorting 
        {
            get
            {
                this.CheckReady();
                return this.GetString("eventSorting");
            }
        }

        /// <summary>
        /// Gets the earliest (inclusive), respectively, time bounds for the 
        /// search, based on the index time bounds. 
        /// </summary>
		/// <remarks>
		/// <para>
		/// The time string can be either a UTC time (with fractional seconds), 
		/// a relative time specifier (to now) or a formatted time string. 
		/// </para>
		/// <para>
		/// This property was introduced in Splunk 5.0.
		/// </para>
		/// </remarks>
        public string IndexEarliest
        {
            get
            {
                this.CheckReady();
                return this.GetString("index_earliest", null);
            }
        }

        /// <summary>
        /// Gets the latest (exclusive), respectively, time bounds for the 
        /// search, based on the index time bounds. 
        /// </summary>
		/// <remarks>
		/// <para>
		/// The time string can be either a UTC time (with fractional seconds), 
		/// a relative time specifier (to now) or a formatted time string. 
		/// </para>
		/// <para>
		/// This property was introduced in Splunk 5.0.
		/// </para>
		/// </remarks>
        public string IndexLatest
        {
            get
            {
                this.CheckReady();
                return this.GetString("index_latest", null);
            }
        }

        /// <summary>
        /// Gets all positive keywords used by this job. 
        /// </summary>
		/// <remarks>
		/// A positive keyword is a keyword that is not in a NOT clause.
		/// </remarks
        public string Keywords
        {
            get
            {
                this.CheckReady();
                return this.GetString("keywords", null);
            }
        }

        /// <summary>
        /// Gets this job's label.
        /// </summary>
        public string Label 
        {
            get
            {
                this.CheckReady();
                return this.GetString("label", null);
            }
        }

        /// <summary>
        /// Gets the latest time a search job is configured to start.
        /// </summary>
        public DateTime LatestTime
        {
            get
            {
                return this.GetDate("latestTime");
            }
        }

        /// <summary>
        /// Gets the number of previews that have been generated so far for this
        /// job.
        /// </summary>
        public int NumPreviews 
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("numPreviews");
            }
        }

        /// <summary>
        /// Gets the search string that is sent to every search peer for this 
        /// job.  
        /// </summary>
        public string RemoteSearch
        {
            get
            {
                this.CheckReady();
                return this.GetString("remoteSearch", null);
            }
        }

        /// <summary>
        /// Gets the reporting subset of this search. 
        /// <seealso cref="EventSearch" />
        /// </summary>
		/// <remarks>
		/// <para>
		/// This is the streaming part of the search that is sent to remote
		/// providers if reporting commands are used. The original search 
		/// should be the <see cref="EventSearch"/> + 
		/// <see cref="reportSearch"/>.
		/// </para>
		/// </remarks>
        public string ReportSearch
        {
            get
            {
                this.CheckReady();
                return this.GetString("reportSearch", null);
            }
        }

        /// <summary>
        /// Gets the total count of results returned for this search job. 
        /// </summary>
        /// <remarks>
        /// This is the subset of scanned events that actually matches the 
        /// search terms. 
		/// </remarks>
        public int ResultCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("resultCount");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the job's result is 
		/// available by streaming.  
        /// </summary>
        public bool ResultIsStreaming 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("resultIsStreaming");
            }
        }

        /// <summary>
        /// Gets the number of result rows in the latest preview results for 
        /// this job. 
        /// </summary>
        public int ResultPreviewCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("resultPreviewCount");
            }
        }

        /// <summary>
        /// Gets the time that the search job took to complete.  
        /// </summary>
        public double RunDuration
        {
            get
            {
                this.CheckReady();
                return this.GetFloat("runDuration");
            }
        }

        /// <summary>
        /// Gets the number of events that are scanned or read off disk. 
        /// </summary>
        public int ScanCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("scanCount");
            }
        }

        /// <summary>
        /// Gets the earliest time a search job is configured to start. 
        /// <seealso cref="SearchLatestTime"/>
        /// <seealso cref="CursorTime"/>
        /// <seealso cref="DoneProgress"/>
        /// </summary>
        public string SearchEarliestTime 
        {
            get
            {
                this.CheckReady();
                return this.GetString("searchEarliestTime", null);
            }
        }

        /// <summary>
        /// Gets the latest time a search job is configured to start. 
        /// <seealso cref="SearchEarliestTime"/>
        /// <seealso cref="CursorTime"/>
        /// <seealso cref="DoneProgress"/>
        /// </summary>
        public string SearchLatestTime
        {
            get
            {
                this.CheckReady();
                return this.GetString("searchLatestTime", null);
            }
        }

        /// <summary>
        /// Gets the list of all the search peers that were contacted.  
        /// </summary>
        public string[] SearchProviders
        {
            get
            {
                this.CheckReady();
                return this.GetStringArray("searchProviders", null);
            }
        }

        /// <summary>
        /// Gets the unique search identifier (SID) for this job.
        /// </summary>
        public string Sid
        {
            get
            {
                return this.GetString("sid");
            }
        }

        /// <summary>
        /// Gets the maximum number of timeline buckets for this job.  
        /// </summary>
		/// <remarks>
		/// Be aware that if the Splunk instance is "dirty," getting this 
		/// property has the side effect of retrieving refreshed data from the
		/// server.
		/// </remarks>
        public int StatusBuckets 
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("statusBuckets");
            }
        }

        /// <summary>
        /// Gets this job's time to live (that is, the time before the search
	 	/// job expires and is still available) in seconds.
        /// </summary>
		/// <remarks>
		/// If this property's value is "0", it means the job has expired.  
		/// </remarks>
        public int Ttl
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("ttl");
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the job is done. 
        /// </summary>
		/// <remarks>
		/// This property implicitly calls the <see cref="Refresh"/> method to 
		/// get current data.
		/// </remarks>
        public bool IsDone
        {
            get
            {
                this.Refresh();
                if (!this.IsReady)
                {
                    return false;
                }

                return this.GetBoolean("isDone");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the job failed. 
        /// </summary>
        public bool IsFailed
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isFailed");
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the job is finalized (that is,
	 	/// it was forced to finish). 
        /// </summary>
        public bool IsFinalized 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isFinalized");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the job is paused.
        /// </summary>
		/// <remarks>
		/// Be aware that if the Splunk instance is "dirty," getting this 
		/// property has the side effect of retrieving refreshed data from the
		/// server.
		/// </remarks>
        public bool IsPaused 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isPaused");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether preview for the job 
		/// is enabled. 
        /// </summary>
        public bool IsPreviewEnabled
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isPreviewEnabled");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether this job is ready to
        /// be queried. 
        /// </summary>
		/// <remarks>
		/// This property implicitly calls the <see cref="Refresh"/> method to 
		/// get current data.
		/// </remarks>
        public bool IsReady
        {
            get
            {
                this.Refresh();
                return this.isReady;
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the job is a real-time
 		/// search. 
        /// </summary>
        public bool IsRealTimeSearch
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isRealTimeSearch");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the job has a remote 
		/// timeline component. 
        /// </summary>
        /// <returns>Whether the job has a remote timeline.</returns>
        public bool IsRemoteTimeline
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isRemoteTimeline");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the job is to be saved
        /// indefinitely.
        /// </summary>
        public bool IsSaved
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isSaved");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether this job was run as a
 		/// saved search (via scheduler).  
        /// </summary>
        public bool IsSavedSearch
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isSavedSearch");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the process running 
		/// the search is dead but with the search not finished. 
        /// </summary>
        public bool IsZombie
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isZombie");
            }
        }

        /// <summary>
        /// Returns the action path for the requested action. 
        /// </summary>
        /// <param name="action">The requested action.</param>
        /// <returns>The action path endpoint.</returns>
		/// <remarks>
		/// This class adds the control endpoint plus includes all the base
		/// class actions.
		/// </remarks>
        protected override string ActionPath(string action) 
        {
            if (action.Equals("control")) 
            {
                return this.Path + "/control";
            }
            return base.ActionPath(action);
        }

        /// <summary>
        /// Performs the requested control action on this job.
        /// </summary>
        /// <param name="action">The action requested.</param>
        /// <returns>The <see cref="Job"/>.</returns>
        public Job Control(string action)
        {
            return this.Control(action, null);
        }

        /// <summary>
        /// Performs the requested action on this job.
        /// </summary>
        /// <param name="action">The requested action.</param>
        /// <param name="args">The variable arguments.</param>
        /// <returns>The <see cref="Job"/>.</returns>
        public Job Control(string action, Args args) 
        {
            args = Args.Create(args).AlternateAdd("action", action);
            this.Service.Post(this.ActionPath("control"), args);
            this.Invalidate();
            return this;
        }

        /// <summary>
        /// Stops the current search and deletes the result cache on the
 		/// server.
        /// </summary>
        /// <returns>The <see cref="Job"/>.</returns>
        public Job Cancel() 
        {
            return this.Control("cancel");
        }

        /// <summary>
        /// Checks whether the job is ready to be queried; if it is not, throws
 		/// an exception.
        /// </summary>
        private void CheckReady()
        {
            if (!this.isReady)
            {
                throw new SplunkException(
                    SplunkException.JOBNOTREADY, 
                    "Job not yet scheduled by server");
            }
        }

        /// <summary>
        /// Disables preview for this job.
        /// </summary>
        /// <returns>The <see cref="Job"/>.</returns>
        public Job DisablePreview() 
        {
            return this.Control("disablepreview");
        }

        /// <summary>
        /// Enables preview for this job.
        /// </summary>
		/// <remarks>
		/// This method might slow search considerably.
		/// </remarks>
        /// <returns>The <see cref="Job"/>.</returns>
        public Job EnablePreview() 
        {
            return this.Control("enablepreview");
        }

        /// <summary>
        /// Returns the stream I/O handle for this job's events.
        /// </summary>
        /// <returns>The event <see cref="Stream"/> I/O handle.</returns>
        public Stream Events() 
        {
            return this.Events(null);
        }

        /// <summary>
        /// Returns the stream I/O handle for this job's events.
        /// </summary>
        /// <param name="args">The variable arguments sent to the .../events 
        /// endpoint.</param>
        /// <returns>The event <see cref="Stream"/> I/O handle.</returns>
        public Stream Events(Args args)
        {
            args = SetSegmentationDefault(args);
            ResponseMessage response = Service.Get(Path + "/events", args);
            return response.Content;
        }

        /// <summary>
        /// Sets the default value for the 'segmentation' property 
        /// in the specified Args, returning the modified original.
        /// </summary>
        /// <param name="args">Arguments input</param>
        /// <returns>Arguments with default set</returns>
        private static Args SetSegmentationDefault(Args args)
        {
            if (args == null)
            {
                args = new Args();
            }
            args.SetSegmentationDefault();
            return args;
        }

        /// <summary>
        /// Stops the job and provides intermediate results available for 
        /// retrieval.
        /// </summary>
        /// <returns>The <see cref="Job"/>.</returns>
        public Job Finish() 
        {
            return this.Control("finalize");
        }

        /// <summary>
        /// Suspends the execution of the current search.
        /// </summary>
        /// <returns>The <see cref="Job"/>.</returns>
        public Job Pause() 
        {
            return this.Control("pause");
        }

        /// <summary>
        /// Returns the stream I/O handle for the results from this job.
        /// </summary>
        /// <returns>The <see cref="Stream"/> I/O handle.</returns>
        public Stream Results() 
        {
            return this.Results(null);
        }

        /// <summary>
        /// Returns the input stream I/O handle for the results from this job,
 		/// adding optional parameters.
        /// </summary>
        /// <param name="args">The variable arguments.</param>
        /// <returns>The results input <see cref="Stream"/> I/O handle.
		/// </returns>
        public Stream Results(Args args) 
        {
            args = SetSegmentationDefault(args);
            ResponseMessage response = Service.Get(Path + "/results", args);
            return response.Content;
        }

        /// <summary>
        /// Returns the stream I/O handle for the preview results from this
 		/// job.
        /// </summary>
        /// <returns>The <see cref="Stream"/> I/O handle.</returns>
        public Stream ResultsPreview() 
        {
            return this.ResultsPreview(null);
        }

        /// <summary>
        /// Returns the input stream I/O handle for the preview results from
 		/// this job, adding optional parameters.
        /// </summary>
        /// <param name="args">The optional parameters.</param>
        /// <returns>The preview results input <see cref="Stream"/> I/O
 		/// handle.</returns>
        public Stream ResultsPreview(Args args) 
        {
            args = SetSegmentationDefault(args);
            ResponseMessage response = 
                Service.Get(Path + "/results_preview", args);
            return response.Content;
        }

        /// <summary>
        /// Returns the stream I/O handle to the search log for this job.
        /// </summary>
        /// <returns>The <see cref="Stream"/> I/O handle.</returns>
        public Stream SearchLog() 
        {
            return this.SearchLog(null);
        }

        /// <summary>
        /// Returns the stream I/O handle to the search log for this job,
        /// adding optional parameters.
        /// </summary>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The <see cref="Stream"/> handle.</returns>
        public Stream SearchLog(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/search.log", args);
            return response.Content;
        }

        /// <summary>
        /// Returns this job's SID from within a response message, as opposed
 		/// to directly from the <see cref="Job"/> object.
        /// </summary>
        /// <param name="response">Response message.</param>
        /// <returns>The job's SID.</returns>
        public static string SidExtraction(ResponseMessage response) 
        {
            StreamReader streamReader = new StreamReader(response.Content);
            XmlDocument doc = new XmlDocument();
            string foo = streamReader.ReadToEnd();
            doc.LoadXml(foo);
            //doc.LoadXml(streamReader.ReadToEnd());
            return doc.SelectSingleNode("/response/sid").InnerText;
        }

        /// <summary>
        /// Returns the stream I/O handle for the summary for this job.
        /// </summary>
        /// <returns>The <see cref="Stream"/> I/O handle.</returns>
        public Stream Summary() 
        {
            return this.Summary(null);
        }

        /// <summary>
        /// Returns the stream I/O handle for the summary for this job, adding
 		/// optional arguments.
        /// </summary>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The <see cref="Stream"/> handle.</returns>
        public Stream Summary(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/summary", args);
            return response.Content;
        }

        /// <summary>
        /// Returns the stream I/O handle for the timeline for this job.
        /// </summary>
        /// <returns>The <see cref="Stream"/> I/O handle.</returns>
        public Stream Timeline() 
        {
            return this.Timeline(null);
        }

        /// <summary>
        /// Returns the stream I/O handle for the timeline for this job,
        /// adding optional arguments.
        /// </summary>
        /// <param name="args">The optional arguments.</param>
        /// <returns>The <see cref="Stream"/> handle.</returns>
        public Stream Timeline(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/timeline", args);
            return response.Content;
        }

        // Job "entities" don't return an AtomFeed, only an AtomEntry.

        /// <summary>
        /// Refreshes this job.
        /// </summary>
        /// <returns>The extended resource, the <see cref="Job"/>.</returns>
        public override Resource Refresh() 
        {
            this.Update();
            ResponseMessage response = Service.Get(Path);
            if (response.Status == 204) 
            {
                this.isReady = false;
                return null;
            }

            this.isReady = true;
            AtomEntry entry = AtomEntry.Parse(response.Content);
            this.Load(entry);
            return this;
        }

        /// <summary>
        /// Not supported. Removes this job. 
        /// </summary>
		/// <remarks>
		/// This method is unsupported and will throw an exception.
		/// </remarks>
        public new void Remove() 
        {
            throw new Exception("Job removal not supported");
        }
    }
}
