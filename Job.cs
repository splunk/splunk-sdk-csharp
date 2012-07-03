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
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Represents the Job class.
    /// </summary>
    public class Job : Entity
    {
        /// <summary>
        /// Represents whether or not the job status can be queried
        /// </summary>
        private bool isReady = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Job" /> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The endpoint path</param>
        public Job(Service service, string path) 
            : base(service, path) 
        {
        }

        /// <summary>
        /// Gets this job's name (its SID). Note that this getting this property
        /// may cause a refresh from the server if the local copy is dirty.
        /// </summary>
        public override string Name
        {
            get
            {
                this.CheckReady();
                return this.Sid;
            }
        }

        /// <summary>
        /// Gets or sets this job's priority. Note that this property has the side
        /// effect of setting the priority immediately, and may if the data is dirty
        /// cause a refreshing of the data when getting the priority.
        /// </summary>
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
        /// Gets the delegate for this job.
        /// </summary>
        /// <returns>The delegate</returns>
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
        /// <returns>The disk usage</returns>
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
        /// <para>
        /// Valid values are: QUEUED, PARSING, RUNNING, PAUSED, FINALIZING, FAILED,
        /// or DONE. 
        /// </para>
        /// </summary>
        /// <returns>The dispatch state.</returns>
        public string DispatchState
        {
            get
            {
                this.CheckReady();
                return this.GetString("dispatchState");
            }
        }

        /// <summary>
        /// Gets the approximate progress of the job, in the range of 0.0 to 1.0. 
        /// <para>
        /// doneProgress = (latestTime-cursorTime) / (latestTime-earliestTime)
        /// </para>
        /// <seealso cref="LatestTime" />
        /// <seealso cref="CursorTime" />
        /// <seealso cref="EarliestTime" />
        /// </summary>
        /// <returns>The job progress</returns>
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
        /// rt_queue_size (the number of events that the indexer should use
        /// for this search). Applicable for real-time searches only.
        /// </summary>
        /// <returns>The drop count</returns>
        public int DropCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("dropCount");
            }
        }

        /**
         * The earliest time a search job is configured to start.
         * @see #getLatestTime
         * @see #getCursorTime
         * @see #getDoneProgress
         *
         * @return The earliest time, in UTC format.
        public Date GetEarliestTime() {
            return GetDate("earliestTime");
        }
         */

        /// <summary>
        /// Gets the count of events stored by search that are available to be
        /// retrieved from the events endpoint. 
        /// </summary>
        /// <returns>The available event count</returns>
        public int EventAvailableCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("eventAvailableCount");
            }
        }

        /// <summary>
        /// Gets the count of pre-transformed events generated by this search job. 
        /// data from the server.
        /// </summary>
        /// <returns>The event count</returns>
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
        /// <returns>The event field count</returns>
        public int EventFieldCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("eventFieldCount");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the events from this job are available 
        /// by streaming or not. 
        /// </summary>
        /// <returns>Whether or not the events are streamable.</returns>
        public bool EventIsStreaming 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("eventIsStreaming");
            }
        }

        /// <summary>
        /// Gets a value indicating whether any events from this job have not been stored. 
        /// </summary>
        /// <returns>Whether or not the events are truncated</returns>
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
        /// commands. The original search should be the eventSearch + reportSearch.
        /// </summary>
        /// <returns>The event search.</returns>
        public string EventSearch 
        {
            get
            {
                this.CheckReady();
                return this.GetString("eventSearch", null);
            }
        }

        /// <summary>
        /// Gets value that indicates how events are sorted. Valid values are
        /// asc -> oldest first, desc -> latest first, none -> not sorted. 
        /// </summary>
        /// <returns>The type of sorting performed on the events.</returns>
        public string EventSorting 
        {
            get
            {
                this.CheckReady();
                return this.GetString("eventSorting");
            }
        }

        /// <summary>
        /// Gets all positive keywords used by this job. A positive keyword is 
        /// a keyword that is not in a NOT clause. 
        /// </summary>
        /// <returns>The positive keywords of the search.</returns>
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
        /// <returns>The job's label.</returns>
        public string Label 
        {
            get
            {
                this.CheckReady();
                return this.GetString("label", null);
            }
        }

        /**
         * Returns the latest time a search job is configured to start. Note
        /// that if dirty, this has the side effect of retrieving refreshed
        /// data from the server.
         * @see #getCursorTime
         * @see #getEarliestTime
         * @see #getDoneProgress
         *
         * @return The latest time, in UTC format.
        public Date GetLatestTime() {
            return GetDate("latestTime");
        }
         */

        /// <summary>
        /// Gets the number of previews that have been generated so far for this job.
        /// </summary>
        /// <returns>The number of oreviews.</returns>
        public int NumPreviews 
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("numPreviews");
            }
        }

        /// <summary>
        /// Gets the search string that is sent to every search peer for this job.  
        /// </summary>
        /// <returns>The remote search string.</returns>
        public string RemoteSearch
        {
            get
            {
                this.CheckReady();
                return this.GetString("remoteSearch", null);
            }
        }

        /// <summary>
        /// Gets the reporting subset of this search. This is the streaming part
        /// of the search that is sent to remote providers if reporting commands are
        /// used. The original search should be the eventSearch + reportSearch. 
        /// <seealso cref="eventSearch" />
        /// </summary>
        /// <returns>The report search.</returns>
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
        /// This is the subset of scanned events that actually matches the search
        /// terms. 
        /// </summary>
        /// <returns>The event count.</returns>
        public int ResultCount
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("resultCount");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the job's result is available by streaming.  
        /// </summary>
        /// <returns>If the results are available for streaming.</returns>
        public bool ResultIsStreaming 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("resultIsStreaming");
            }
        }

        /// <summary>
        /// Gets the number of result rows in the latest preview results for this job. 
        /// </summary>
        /// <returns>The results preview count.</returns>
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
        /// <returns>The duration of the run</returns>
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
        /// <returns>The event scan count</returns>
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
        /// <returns>The earliest search time.</returns>
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
        /// <returns>The latest search time.</returns>
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
        /// <returns>The list of search peers involved in the search.</returns>
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
        /// <returns>The SID</returns>
        public string Sid
        {
            get
            {
                this.CheckReady();
                return this.GetString("sid");
            }
        }

        /// <summary>
        /// Gets the maximum number of timeline buckets for this job.  Note
        /// that if dirty, this has the side effect of retrieving refreshed
        /// data from the server. 
        /// </summary>
        /// <returns>The number of status buckets</returns>
        public int StatusBuckets 
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("statusBuckets");
            }
        }

        /// <summary>
        /// Gets this job's time to live in seconds --that is, the time
        /// before the search job expires and is still available.
        /// If this value is 0, it means the job has expired.  
        /// </summary>
        /// <returns>The time-to-live</returns>
        public int Ttl
        {
            get
            {
                this.CheckReady();
                return this.GetInteger("ttl");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the job is done. Implicitly 
        /// call the refresh method to get current data.
        /// </summary>
        /// <returns>If the job is complete or not</returns>
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
        /// Gets a value indicating whether the job failed. 
        /// </summary>
        /// <returns>Whether or not the job has failed.</returns>
        public bool IsFailed
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isFailed");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the job is finalized (forced to finish). 
        /// </summary>
        /// <returns>Whether or not the job is finalized.</returns>
        public bool IsFinalized 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isFinalized");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the jobs is paused.  Note
        /// that if dirty, this has the side effect of retrieving refreshed
        /// data from the server. 
        /// </summary>
        /// <returns>Whether or not the job is paused.</returns>
        public bool IsPaused 
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isPaused");
            }
        }

        /// <summary>
        /// Gets a value indicating whether preview for the job is enabled. 
        /// </summary>
        /// <returns>Whether or not preview is enabled.</returns>
        public bool IsPreviewEnabled
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isPreviewEnabled");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this job is ready to
        /// be queried. Implicitly calls the refresh method to get 
        /// current data.
        /// </summary>
        public bool IsReady
        {
            get
            {
                this.Refresh();
                return this.isReady;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the job is a real-time search. 
        /// </summary>
        /// <returns>If the job is a real-time search.</returns>
        public bool IsRealTimeSearch
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isRealTimeSearch");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the job has a remote timeline component. 
        /// </summary>
        /// <returns>If the job has a remote timeline</returns>
        public bool IsRemoteTimeline
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isRemoteTimeline");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the job is to be saved indefinitely.
        /// </summary>
        /// <returns>If the job is saved indefinitely.</returns>
        public bool IsSaved
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isSaved");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this job was run as a saved search (via scheduler).  
        /// </summary>
        /// <returns>If this is a saved search.</returns>
        public bool IsSavedSearch
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isSavedSearch");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the process running the search is dead but with the
        /// search not finished. 
        /// </summary>
        /// <returns>If the job is a zombie.</returns>
        public bool IsZombie
        {
            get
            {
                this.CheckReady();
                return this.GetBoolean("isZombie");
            }
        }

        /// <summary>
        /// Returns the action path for the requested action. This class adds
        /// the control endpoint plus includes all the base class actions.
        /// </summary>
        /// <param name="action">The requested action</param>
        /// <returns>The action path endpoint</returns>
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
        /// <param name="action">The action requested</param>
        /// <returns>The Job</returns>
        public Job Control(string action)
        {
            return this.Control(action, null);
        }

        /// <summary>
        /// Performs the requested action on this job.
        /// </summary>
        /// <param name="action">The requested action</param>
        /// <param name="args">The variable arguments</param>
        /// <returns>The job</returns>
        public Job Control(string action, Args args) 
        {
            args = Args.Create(args).AlternateAdd("action", action);
            this.Service.Post(this.ActionPath("control"), args);
            this.Invalidate();
            return this;
        }

        /// <summary>
        /// Stops the current search and deletes the result cache
        /// on the server.
        /// </summary>
        /// <returns>The job</returns>
        public Job Cancel() 
        {
            return this.Control("cancel");
        }

        /// <summary>
        /// Checks if the job is ready to be queried, if not, throws an exception.
        /// </summary>
        private void CheckReady()
        {
            if (!this.isReady)
            {
                throw new SplunkException(SplunkException.JOBNOTREADY, "Job not yet scheduled by server");
            }
        }

        /// <summary>
        /// Disables preview for this job.
        /// </summary>
        /// <returns>The job</returns>
        public Job DisablePreview() 
        {
            return this.Control("disablepreview");
        }

        /// <summary>
        /// Enables preview for this job (although it might slow search considerably).
        /// </summary>
        /// <returns>The job</returns>
        public Job EnablePreview() 
        {
            return this.Control("enablepreview");
        }

        /// <summary>
        /// Returns the Stream IO handle for this job's events.
        /// </summary>
        /// <returns>The event Stream IO handle.</returns>
        public Stream Events() 
        {
            return this.Events(null);
        }

        /// <summary>
        /// The Stream IO handle for this job's events.
        /// </summary>
        /// <param name="args">The variable arguments sent to the .../events endpoint</param>
        /// <returns>The Stream IO handle</returns>
        public Stream Events(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/events", args);
            return response.Content;
        }

        /// <summary>
        /// Stops the job and provides intermediate results available for retrieval.
        /// </summary>
        /// <returns>The Job</returns>
        public Job Finish() 
        {
            return this.Control("finalize");
        }

        /// <summary>
        /// Suspends the execution of the current search.
        /// </summary>
        /// <returns>The job</returns>
        public Job Pause() 
        {
            return this.Control("pause");
        }

        /**
         * Returns the earliest time from which we are sure no events later than
         * this time will be scanned later. (Use this as a progress indicator.)
         * @see #getLatestTime
         * @see #getEarliestTime
         * @see #getDoneProgress
         *
         * @return The earliest time.
        public Date GetCursorTime() {
            return GetDate("cursorTime");
        }
         */

        /// <summary>
        /// The Stream IO handle for the results from this job.
        /// </summary>
        /// <returns>The Stream IO handle</returns>
        public Stream Results() 
        {
            return this.Results(null);
        }

        /// <summary>
        /// Returns the InputStream IO handle for the results from this job, 
        /// adding optional parameters.
        /// </summary>
        /// <param name="args">The variable arguments</param>
        /// <returns>The results InputStream IO handle.</returns>
        public Stream Results(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/results", args);
            return response.Content;
        }

        /// <summary>
        /// Returns the Stream IO handle for the preview results from this job.
        /// </summary>
        /// <returns>The Stream IO handle</returns>
        public Stream ResultsPreview() 
        {
            return this.ResultsPreview(null);
        }

        /// <summary>
        /// Returns the InputStream IO handle for the preview results from this job,
        /// adding optional parameters.
        /// </summary>
        /// <param name="args">The optional parameters</param>
        /// <returns>The preview results InputStream IO handle.</returns>
        public Stream ResultsPreview(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/results_preview", args);
            return response.Content;
        }

        /// <summary>
        /// Returns the Stream IO handle to the search log for this job.
        /// </summary>
        /// <returns>The Stream IO handle</returns>
        public Stream SearchLog() 
        {
            return this.SearchLog(null);
        }

        /// <summary>
        /// Returns the Stream IO handle to the search log for this job,
        /// adding optional parameters.
        /// </summary>
        /// <param name="args">The optional arguments</param>
        /// <returns>The Stream handle</returns>
        public Stream SearchLog(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/search.log", args);
            return response.Content;
        }

        /// <summary>
        /// Returns this job's SID from within a response message, as
        /// opposed to directly from the Job object.
        /// </summary>
        /// <param name="response">Response message</param>
        /// <returns>The SID</returns>
        public static string SidExtraction(ResponseMessage response) 
        {
            StreamReader streamReader = new StreamReader(response.Content);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(streamReader.ReadToEnd());
            return doc.SelectSingleNode("/sid").InnerText;
        }

        /// <summary>
        /// Returns the Stream IO handle for the summary for this job.
        /// </summary>
        /// <returns>The Stream IO handle</returns>
        public Stream Summary() 
        {
            return this.Summary(null);
        }

        /// <summary>
        ///  Returns the Stream IO handle for the summary for this job,
        ///  adding optional arguments.
        /// </summary>
        /// <param name="args">The optional arguments</param>
        /// <returns>The Stream handle.</returns>
        public Stream Summary(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/summary", args);
            return response.Content;
        }

        /// <summary>
        /// Returns the Stream IO handle for the timeline for this job.
        /// </summary>
        /// <returns>The Stream IO handle</returns>
        public Stream Timeline() 
        {
            return this.Timeline(null);
        }

        /// <summary>
        /// Returns the InputStream IO handle for the timeline for this job,
        /// adding optional arguments
        /// </summary>
        /// <param name="args">The optional arguments</param>
        /// <returns>The Stream handle</returns>
        public Stream Timeline(Args args) 
        {
            ResponseMessage response = Service.Get(Path + "/timeline", args);
            return response.Content;
        }

        // Job "entities" don't return an AtomFeed, only an AtomEntry.

        /// <summary>
        /// Refreshes this job.
        /// </summary>
        /// <returns>The extended resource, the Job</returns>
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
        /// Unsupported. Removes this job. This method is unsupported and will throw
        /// an exception.
        /// </summary>
        public new void Remove() 
        {
            throw new Exception("Job removal not supported with this operation");
        }
    }
}
