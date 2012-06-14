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

    /// <summary>
    /// Represents a class to convert values easily
    /// </summary>
    public class Value
    {
        /// <summary>
        /// Converts a string of either 0/1 or true/false to a bool.
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The bool value</returns>
        public static bool ToBoolean(string value) 
        {
            if (value == null) 
            {
                return false;
            }
            if (value.Equals("0")) 
            {
                return false;
            }
            if (value.Equals("1")) 
            {
                return true;
            }
            if (value.ToLower().Equals("false")) 
            {
                return false;
            }
            if (value.ToLower().Equals("true")) 
            {
                return true;
            }
            throw new Exception(string.Format("Value error: '{0}'", value));
        }

        /// <summary>
        /// Converts a string to numeriv byte count. The input can be a number
        /// or a number followed by KB, MB, GB.
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The long value</returns>
        public static long ToByteCount(string value) 
        {
            long multiplier = 1;
            if (value.EndsWith("B")) 
            {
                if (value.EndsWith("KB")) 
                {
                    multiplier = 1024;
                }
                else if (value.EndsWith("MB")) 
                {
                    multiplier = 1024 * 1024;
                }
                else if (value.EndsWith("GB")) 
                {
                    multiplier = 1024 * 1024 * 1024;
                }
                else 
                {
                    throw new Exception(string.Format("Value error: '{0}'", value));
                }
                value = value.Substring(0, value.Length - 2);
            }
            return Convert.ToInt64(value) * multiplier;
        }

        /*
        private static SimpleDateFormat[] dateFormat = null;
        private static Pattern datePattern = null;

         * Converts a {@code string} to a {@code Date} value.
         *
         * @param value Value to convert.
         * @return Date value.

        "Mon May 07 12:09:17 2012""
        static Date toDate(string value) {
        if (dateFormat == null) {
            dateFormat = new SimpleDateFormat[4];
            dateFormat[0] = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ssZ");
            dateFormat[0].setLenient(true);
            dateFormat[1] = new SimpleDateFormat("E MMM d HH:mm:ss z y");
            dateFormat[1].setLenient(true);
            dateFormat[2] = new SimpleDateFormat("EEE MMM dd HH:mm:ss y");
            dateFormat[2].setLenient(true);
            dateFormat[3] = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss Z");
            dateFormat[3].setLenient(true);
        }
        if (datePattern == null) {
            string pattern = "(.*)\\.\\d+([\\-+]\\d+):(\\d+)";
            datePattern = Pattern.compile(pattern);
        }

        for (SimpleDateFormat simpleDateFormat: dateFormat)  {
             Must first remove the colon (':') from the timezone
             field, or SimpleDataFormat will not parse correctly.
             Eg: 2010-01-01T12:00:00+01:00 => 2010-01-01T12:00:00+0100
            try {

                Matcher matcher = datePattern.matcher(value);
                value = matcher.replaceAll("$1$2$3");
                return simpleDateFormat.parse(value);
            }
            catch (ParseException e) {}
        }
        try {
            return new Date(Long.parseLong(value)*1000);
        } catch (Exception e) {
            throw new RuntimeException(e.getMessage());
        }
        */

        /// <summary>
        /// Converts a string to a double value.
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The double value</returns>
        public static double ToFloat(string value) 
        {
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// Converts a string to an int value.
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The int value</returns>
        public static int ToInteger(string value) 
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts a string to a long value.
        /// </summary>
        /// <param name="value">The string value</param>
        /// <returns>The long value</returns>
        public static long ToLong(string value) 
        {
            return Convert.ToInt64(value);
        }
    }
}
