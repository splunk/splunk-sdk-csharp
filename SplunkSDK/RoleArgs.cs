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
    /// <summary>
    /// Extends Args for Role creation setters
    /// </summary>
    public class RoleArgs : Args
    {
        /// <summary>
        /// Sets the capabilities of this role, as a list.
        /// </summary>
        public string[] Capabilities
        {
            set
            {
                this["capabilities"] = value;
            }
        }

        /// <summary>
        /// Sets the name of the app to use as the default app for 
        /// the role.A user-specific default app will override this.
        /// The name you specify is the name of the folder containing the app.
        /// </summary>
        public string DefaultApp
        {
            set
            {
                this["defaultApp"] = value;
            }
        }

        /// <summary>
        /// Sets a role to import attributes from, as a list.
        /// The default is a role imports no other roles.
        /// </summary>
        public string[] ImportedRoles
        {
            set
            {
                this["imported_roles"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of concurrent real time search jobs for this
        /// role. This count is ford not effect the normal search jobs limit.
        /// </summary>
        public int RtSrchJobsQuota
        {
            set 
            {
                this["rtSrchJobsQuota"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum disk space, in MB, that can be used by a user's 
        /// search jobs. For example, 100 limits this role to 100 MB total.
        /// </summary>
        public int SrchDiskQuota
        {
            set
            {
                this["srchDiskQuota"] = value;
            }
        }

        /// <summary>
        /// Sets a search string that restricts the scope of searches run by 
        /// this role. Search results for this role only show events that also 
        /// match the search string you specify. In the case that a user has 
        /// multiple roles with different search filters, they are combined with
        /// an OR. The search string can include source, host, index, eventtype,
        /// sourcetype, search fields, *, OR and, AND.
        /// Example: 
        /// <example>"host=web* OR source=/var/log/*"</example>
        /// Note: You can also use the srchIndexesAllowed and srchIndexesDefault 
        /// parameters to limit the search on indexes.
        /// </summary>
        public string SrchFilter
        {
            set
            {
                this["srchFilter"] = value;
            }
        }

        /// <summary>
        /// Sets the index(es) this role has permissions to search.
        /// </summary>
        public string[] SrchIndexesAllowed
        {
            set
            {
                this["srchIndexesAllowed"] = value;
            }
        }

        /// <summary>
        /// Sets the indexes to search when no index is specified. These indexes
        /// can be wildcarded, with the exception that '*' does not match 
        /// internal indexes. To match internal indexes, start with '_'. All 
        /// internal indexes are represented by '_*'.
        /// </summary>
        public string[] SrchIndexesDefault
        {
            set
            {
                this["srchIndexesDefault"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of concurrent searches a user with this role
        /// is allowed to run. In the event of many roles per user, the maximum
        /// of these quotas is applied.
        /// </summary>
        public int SrchJobsQuota
        {
            set
            {
                this["srchJobsQuota"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum time span of a search, in seconds.
        /// By default, searches are not limited to any specific time window. 
        /// To override any search time windows from imported roles, set 
        /// srchTimeWin to '0', as the 'admin' role does.
        /// </summary>
        public int SrchTimeWin
        {
            set
            {
                this["srchTimeWin"] = value;
            }
        }
    }
}