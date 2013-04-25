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

namespace Splunk
{
    /// <summary>
    ///     Class to host extension method to get enum custom string.
    /// </summary>
    internal static class CustomStringExtenstion
    {
        /// <summary>
        ///     Get the custom string for the Ennm value
        /// </summary>
        /// <param name="value">The enum value</param>
        /// <returns>The custom string</returns>
        public static string GetCustomString(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());

            // Get the stringvalue attributes
            var attribs = fieldInfo.GetCustomAttributes(
                typeof(CustomString), false) as CustomString[];

            // Return the first if there was a match.
            return attribs.Length > 0 ? attribs[0].Value : null;
        }
    }
}