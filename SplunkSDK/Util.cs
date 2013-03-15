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
    using System.Collections;

    /// <summary>
    /// The <see cref="Util"/> class represents a collection of utility 
    /// routines.
    /// </summary>
    public class Util
    {
        /// <summary>
        /// Returns the substring after a found element.
        /// </summary>
        /// <param name="template">The original string.</param>
        /// <param name="toFind">What to find.</param>
        /// <param name="defaultTo">Default if not found.</param>
        /// <returns>The substring after 'toFind'.</returns>
        public static string 
            SubstringAfter(string template, string toFind, string defaultTo)
        {
            int toFindLength = toFind.Length;
            int toFindOffset = template.IndexOf(toFind);
            int substringOffset = toFindOffset + toFindLength;

            if (toFindOffset == -1)
            {
                return defaultTo;
            }
            else
            {
                return template.Substring(substringOffset);
            }
        }

        /// <summary>
        /// Joins a new string with the elements of the array.
        /// </summary>
        /// <param name="joiner">The joiner.</param>
        /// <param name="joinees">The joinees.</param>
        /// <returns>The combined string.</returns>
        public static string Join(string joiner, ArrayList joinees)
        {
            if (joinees.Count == 0)
            {
                return string.Empty;
            }

            string joined = (string)joinees[0];
            foreach (string str in joinees.GetRange(1, joinees.Count - 1))
            {
                joined = joined + joiner + str;
            }

            return joined;
        }

        /// <summary>
        /// Validates an exact namespace.
        /// </summary>
        /// <param name="nameSpace">The namespace.</param>
        public static void EnsureNamespaceIsExact(Args nameSpace)
        {
            string app = (string)nameSpace["app"];
            string owner = (string)nameSpace["owner"];

            bool wilcardedApp = (app == null) || app.Equals("-");
            bool wildcardedOwner = (owner == null) || owner.Equals("-");
            bool isExact = !wilcardedApp && !wildcardedOwner;

            if (!isExact)
            {
                throw new SplunkException(
                    SplunkException.AMBIGUOUS,
                    "An exact namespace must be provided");
            }
        }
    }
}
