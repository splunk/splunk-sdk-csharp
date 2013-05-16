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
    /// The <see cref="ReplaceTimezone"/> class parses time zone strings.
    /// </summary>
    public static class ReplaceTimezone
    {
        /// <summary>
        /// A list of time zone information, including nickname, UTC, and full
        /// text. This list is distilled from various places on the web.
        /// Military time zones are not represented.
        /// </summary>
        private static string[][] timeZones = new string[][] 
        {
            // not order is important, if the nickname is part of a longer nickname
            new string[] { "ACDT", "+1030", "Australian Central Daylight Time" },
            new string[] { "ACST", "+0930", "Australian Central Standard Time" },
            new string[] { "ADT", "-0300", "Atlantic Daylight Time" },
            new string[] { "AEDT", "+1100", "Australian East Daylight Time" },
            new string[] { "AEST", "+1000", "Australian East Standard Time" },
            new string[] { "AHDT", "-0900", "Alaska-Hawaii Daylight Time" },
            new string[] { "AHST", "-1000", "Alaska-Hawaii Standard Time" },
            new string[] { "AST", "-0400", "Atlantic Standard Time" },
            new string[] { "AT", "-0200", "Azores" },
            new string[] { "AWDT", "+0900", "Australian West Daylight Time" },
            new string[] { "AWST", "+0800", "Australian West Standard Time" },
            new string[] { "BAT", "+0300", "Bhagdad" },
            new string[] { "BDST", "+0200", "British Double Summer" },
            new string[] { "BET", "-1100", "Bering Standard Time" },
            new string[] { "BST", "-0300", "Brazil Standard Time" },
            new string[] { "BT", "+0300", "Baghdad" },
            new string[] { "BZT2", "-0300", "Brazil Zone 2" },
            new string[] { "CADT", "+1030", "Central Australian Daylight Time" },
            new string[] { "CAST", "+0930", "Central Australian Standard Time" },
            new string[] { "CAT", "-1000", "Central Alaska" },
            new string[] { "CCT", "+0800", "China Coast" },
            new string[] { "CDT", "-0500", "Central Daylight Time" },
            new string[] { "CED", "+0200", "Central European Daylight Time" },
            new string[] { "CET", "+0100", "Central European" },
            new string[] { "CST", "-0600", "Central Standard Time" },
            new string[] { "CENTRAL", "-0600", "Central Standard Time" },
            new string[] { "EAST", "+1000", "Eastern Australian Standard Time" },
            new string[] { "EDT", "-0400", "Eastern Daylight Time" },
            new string[] { "EED", "+0300", "Eastern European Daylight Time" },
            new string[] { "EET", "+0200", "Eastern Europe" },
            new string[] { "EEST", "+0300", "Eastern Europe Summer" },
            new string[] { "EST", "-0500", "Eastern Standard Time" },
            new string[] { "EASTERN", "-0500", "Eastern Standard Time" },
            new string[] { "FST", "+0200", "French Summer" },
            new string[] { "FWT", "+0100", "French Winter" },
            new string[] { "GMT", "-0000", "Greenwich Mean" },
            new string[] { "GST", "+1000", "Guam Standard Time" },
            new string[] { "HDT", "-0900", "Hawaii Daylight Time" },
            new string[] { "HST", "-1000", "Hawaii Standard Time" },
            new string[] { "IDLE", "+1200", "Internation Date Line East" },
            new string[] { "IDLW", "-1200", "Internation Date Line West" },
            new string[] { "IST", "+0530", "Indian Standard Time" },
            new string[] { "IT", "+0330", "Iran" },
            new string[] { "JST", "+0900", "Japan Standard Time" },
            new string[] { "JT", "+0700", "Java" },
            new string[] { "MDT", "-0600", "Mountain Daylight Time" },
            new string[] { "MED", "+0200", "Middle European Daylight Time" },
            new string[] { "MET", "+0100", "Middle European" },
            new string[] { "MEST", "+0200", "Middle European Summer" },
            new string[] { "MEWT", "+0100", "Middle European Winter" },
            new string[] { "MST", "-0700", "Mountain Standard Time" },
            new string[] { "MOUNTAIN", "-0700", "Mountain Standard Time" },
            new string[] { "MT", "+0800", "Moluccas" },
            new string[] { "NDT", "-0230", "Newfoundland Daylight Time" },
            new string[] { "NFT", "-0330", "Newfoundland" },
            new string[] { "NT", "-1100", "Nome" },
            new string[] { "NST", "+0630", "North Sumatra" },
            new string[] { "NZST", "+1200", "New Zealand Standard Time" },
            new string[] { "NZDT", "+1300", "New Zealand Daylight Time " },
            new string[] { "NZT", "+1200", "New Zealand" },
            new string[] { "NZ", "+1100", "New Zealand " },
            new string[] { "PDT", "-0700", "Pacific Daylight Time" },
            new string[] { "PST", "-0800", "Pacific Standard Time" },
            new string[] { "PACIFIC", "-0800", "Pacific Standard Time" },
            new string[] { "ROK", "+0900", "Republic of Korea" },
            new string[] { "SAD", "+1000", "South Australia Daylight Time" },
            new string[] { "SAST", "+0900", "South Australia Standard Time" },
            new string[] { "SAT", "+0900", "South Australia Standard Time" },
            new string[] { "SDT", "+1000", "South Australia Daylight Time" },
            new string[] { "SST", "+0200", "Swedish Summer" },
            new string[] { "SWT", "+0100", "Swedish Winter" },
            new string[] { "USZ3", "+0400", "USSR Zone 3" },
            new string[] { "USZ4", "+0500", "USSR Zone 4" },
            new string[] { "USZ5", "+0600", "USSR Zone 5" },
            new string[] { "USZ6", "+0700", "USSR Zone 6" },
            new string[] { "UTC", "-0000", "Universal Coordinated" },
            new string[] { "UT", "-0000", "Universal Coordinated" },
            new string[] { "UZ10", "+1100", "USSR Zone 10" },
            new string[] { "WAT", "-0100", "West Africa" },
            new string[] { "WET", "-0000", "West European" },
            new string[] { "WST", "+0800", "West Australian Standard Time" },
            new string[] { "YDT", "-0800", "Yukon Daylight Time" },
            new string[] { "YST", "-0900", "Yukon Standard Time" },
            new string[] { "ZP4", "+0400", "USSR Zone 3" },
            new string[] { "ZP5", "+0500", "USSR Zone 4" },
            new string[] { "ZP6", "+0600", "USSR Zone 5" }
        };

        /// <summary>
        /// Returns the date with the time zone text replaced with 
        /// the UTC equivalent, if found.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>The date with timezone string.</returns>
        public static string ReplaceTimeZone(string date)
        {
            foreach (string[] timezone in timeZones)
            {
                // look for pretty text first
                if (date.Contains(timezone[2]))
                {
                    date = date.Replace(timezone[2], timezone[1]);
                    break;
                } 
                else if (date.Contains(timezone[0]))
                {
                    date = date.Replace(timezone[0], timezone[1]);
                    break;
                }
            }

            return date;
        }
    }
}