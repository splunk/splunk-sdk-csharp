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
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// The <see cref="JobCollection"/> class represents a collection of jobs.
    /// A job is an individual instance of a running or completed search or
    /// report, along with its related output.
    /// </summary>
    public class JobCollection : EntityCollection<Job>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JobCollection"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        public JobCollection(Service service)
            : base(service, "search/jobs", typeof(Job)) 
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobCollection"/> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="args">The variable arguments.</param>
        public JobCollection(Service service, Args args) 
            : base(service, "search/jobs", args, typeof(Job)) 
        {
        }

        /// <summary>
        /// Creates a search with a UTF-8 pre-encoded search request.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>The job.</returns>
        /// <remarks>
        /// A "oneshot" request is invalid. To create a oneshot search,
        /// use the <see cref="Service.Oneshot"/> method instead. 
        /// </remarks>
        public new Job Create(string query) 
        {
            return this.Create(query, (Args)null);
        }

        /// <summary>
        /// Creates a search with a UTF-8 pre-encoded search request.
        /// </summary>
        /// <remarks>
        /// A "oneshot" request is invalid. To create a oneshot search,
        /// use the <see cref="Service.Oneshot"/> method instead. 
        /// </remarks>
        /// <param name="query">The search query.</param>
        /// <param name="args">Additional arguments for this job.</param>
        /// <returns>The job.</returns>
        public new Job Create(string query, Args args) 
        {
            if (args != null && args.ContainsKey("exec_mode")) 
            {
                if (args["exec_mode"].Equals("oneshot")) 
                {
                    throw new Exception(
                      "Oneshot not allowed, use service oneshot search method");
                }
            }
            args = Args.Create(args).Set("search", query);
            ResponseMessage response = Service.Post(Path, args);
            /* assert(response.getStatus() == 201); */
            StreamReader streamReader = new StreamReader(response.Content);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(streamReader.ReadToEnd());
            string sid = doc.SelectSingleNode("/response/sid").InnerText;
            this.Invalidate();
            Job job = (Job)Get(sid);

            // if job not yet scheduled, create an empty job object
            if (job == null) 
            {
                job = new Job(Service, "search/jobs/" + sid);
            }

            return job;
        }

        /// <summary>
        /// Creates a search with a UTF-8 pre-encoded search request.
        /// </summary>
        /// <remarks>
        /// A "oneshot" request is invalid. To create a oneshot search,
        /// use the <see cref="Service.Oneshot"/> method instead. 
        /// </remarks>
        /// <param name="query">The search query.</param>
        /// <param name="args">Additional arguments for this job.</param>
        /// <returns>The job.</returns>
        public Job Create(string query, JobArgs args)
        {
            return this.Create(query, (Args) args);
        }

        /// <summary>
        /// Returns the list of jobs, as a <see cref="ResponseMessage"/> object.
        /// </summary>
        /// <returns>The list of jobs.</returns>
        public override ResponseMessage List() 
        {
            return Service.Get(this.Path + "?count=0");
        }

        /// <summary>
        /// Returns the job's unique search identifier (SID), which is used as 
        /// this item's key.
        /// </summary>
        /// <param name="entry">The Atom entry.</param>
        /// <returns>The SID.</returns>
        protected override string ItemKey(AtomEntry entry) 
        {
            return (string)entry.Content["sid"];
        }
    }
}
