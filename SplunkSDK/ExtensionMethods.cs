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

using System;
using System.Globalization;
using System.Text;

namespace Splunk
{
    /// <summary>
    /// The <see cref="ExtensionMethods"/> class hosts an extension method 
    /// that retrieves an enum value's custom string.
    /// </summary>
    internal static class ExtensionMethods
    {      
        /// <summary>
        /// Retrieves the custom string for the enum value.
        /// </summary>
        /// <param name="value">The enum value.</param>
        /// <returns>The custom string.</returns>
        internal static string GetCustomString(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            var attribs = fieldInfo.GetCustomAttributes(
                typeof(CustomString), false) as CustomString[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Value : null;
        }

        /// <summary>
        /// Retrieves comma-separated elements in an array.
        /// </summary>
        /// <param name="value">The array.</param>
        /// <returns>
        /// A string of comma-separated values.
        /// </returns>
        internal static string GetCsv(this string[] value)
        {
            var csv = new StringBuilder();
            for (int i = 0, n = value.Length; i < n; i++)
            {
                if (i != 0)
                {
                    csv.Append(",");
                }
                csv.Append(value[i]);
            }
            return csv.ToString();
        }
    }
}
