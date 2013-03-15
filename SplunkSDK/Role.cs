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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The <see cref="Role"/> class represents a Splunk role, which is a 
    /// collection of permissions and capabilities. The user's role determines 
    /// what the user can see and interact with in Splunk.
    /// </summary>
    public class Role : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        /// <param name="path">The endpoint path</param>
        public Role(Service service, string path) 
            : base(service, path) 
        {
        }

        /// <summary>
        /// Gets or sets the list of capabilities assigned to this role.
        /// </summary>
        public string[] Capabilities
        {
            get
            {
                return this.GetStringArray("capabilities");
            }

            set
            {
                this.SetCacheValue("capabilities", value);
            }
        }

        /// <summary>
        /// Gets or sets the default app for this role.
        /// </summary>
        public string DefaultApp
        {
            get
            {
                return this.GetString("defaultApp", null);
            }

            set
            {
                this.SetCacheValue("defaultApp", value);
            }
        }

        /// <summary>
        /// Gets or sets an array of roles used to import attributes from, such 
        /// as capabilities and allowed indexes to search.
        /// </summary>
        public string[] ImportedRoles
        {
            get
            {
                return this.GetStringArray("imported_roles", null);
            }

            set
            {
                this.SetCacheValue("imported_roles", value);
            }
        }

        /// <summary>
        /// Gets the maximum number of concurrent real-time search jobs a user
        /// with this role is allowed to run.
        /// </summary>
        public int ImportedRtSearchJobsQuota
        {
            get
            {
                return this.GetInteger("imported_rtSrchJobsQuota");
            }
        }

        /// <summary>
        /// Gets the maximum disk space that can be used for search jobs by a 
        /// user with this role.
        /// </summary>
        public int ImportedSearchDiskQuota
        {
            get
            {
                return this.GetInteger("imported_srchDiskQuota");
            }
        }

        /// <summary>
        /// Gets a search string that restricts the scope of searches run by 
        /// this role. Only those events that also match this search string are
        /// shown to the user. If a user has multiple roles with different 
        /// search filters, they are combined with an "OR".
        /// </summary>
        public string ImportedSearchFilter
        {
            get
            {
                return this.GetString("imported_srchFilter", null);
            }
        }

        /// <summary>
        /// Gets an array of indexes that a user with this role has permissions 
        /// to search.
        /// </summary>
        public string[] ImportedIndexesAllowed
        {
            get
            {
                return this.GetStringArray("imported_srchIndexesAllowed", null);
            }
        }

        /// <summary>
        /// Gets an array of indexes to search by default when no index is
        /// specified for a user with this role.
        /// </summary>
        public string[] ImportedIndexesDefault
        {
            get
            {
                return this.GetStringArray("imported_srchIndexesDefault", null);
            }
        }

        /// <summary>
        /// Gets the maximum number of concurrent searches a user with this role 
        /// is allowed to run.
        /// </summary>
        public int ImportedSearchJobsQuota
        {
            get
            {
                return this.GetInteger("imported_srchJobsQuota");
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of concurrent real-time search jobs 
        /// a use with this role is allowed to run.
        /// </summary>
        public int RtSearchJobsQuota
        {
            get
            {
                return this.GetInteger("rtSrchJobsQuota");
            }

            set
            {
                this.SetCacheValue("rtSrchJobsQuota", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum disk space that can be used for search 
        /// jobs by a user with this role.
        /// </summary>
        public int SearchDiskQuota
        {
            get
            {
                return this.GetInteger("srchDiskQuota");
            }

            set
            {
                this.SetCacheValue("srchDiskQuota", value);
            }
        }

        /// <summary>
        /// Gets or sets a search string that restricts the scope of searches 
        /// run by this role. Only those events that also match this search 
        /// string are shown to the user. If a user has multiple roles with 
        /// different search filters, they are combined with an "OR".
        /// </summary>
        public string SearchFilter
        {
            get
            {
                return this.GetString("srchFilter", null);
            }

            set
            {
                this.SetCacheValue("srchFilter", value);
            }
        }

        /// <summary>
        /// Gets or sets an array of indexes that a user with this role has 
        /// permissions to search.
        /// </summary>
        public string[] SearchIndexesAllowed
        {
            get
            {
                return this.GetStringArray("srchIndexesAllowed", null);
            }

            set
            {
                this.SetCacheValue("srchIndexesAllowed", value);
            }
        }

        /// <summary>
        /// Gets or sets an array of indexes to search by default when no index
        /// is specified for a user with this role.
        /// </summary>
        public string[] SearchIndexesDefault
        {
            get
            {
                return this.GetStringArray("srchIndexesDefault", null);
            }

            set
            {
                this.SetCacheValue("srchIndexesDefault", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of concurrent searches a user with 
        /// this role is allowed to run.
        /// </summary>
        public int SearchJobsQuota
        {
            get
            {
                return this.GetInteger("srchJobsQuota");
            }

            set
            {
                this.SetCacheValue("srchJobsQuota", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum time span of a search that is allowed for 
        /// users in this role.
        /// </summary>
        public int SearchTimeWin
        {
            get
            {
                return this.GetInteger("srchTimeWin");
            }

            set
            {
                this.SetCacheValue("srchTimeWin", value);
            }
        }
    }
}
