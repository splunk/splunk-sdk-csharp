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

    /// <summary>
    /// The <see cref="Value"/> class provides methods for converting values.
    /// </summary>
    public class Value
    {
        /// <summary>
        /// The parseable date formats.
        /// </summary>
        private static string[] dateFormats = 
        {
            "yyyy-MM-dd'T'HH:mm:sszzz",
            "yyyy-MM-dd'T'HH:mm:ss.FFFzzz",
            "ddd MMM d HH:mm:ss yyyy",
            "yyyy-MM-dd HH:mm:ss zzz"
        };

        /// <summary>
        /// Converts a string of "0", "1", true, or false to a Boolean value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The Boolean value.</returns>
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
        /// Converts a string to numeric byte count. The input can be a number
        /// or a number followed by KB, MB, GB.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The long value.</returns>
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
                    throw new 
                        Exception(string.Format("Value error: '{0}'", value));
                }
                value = value.Substring(0, value.Length - 2);
            }
            return Convert.ToInt64(value) * multiplier;
        }

        /// <summary>
        /// Converts a date string to a DateTime structure.
        /// </summary>
        /// <param name="value">The date string.</param>
        /// <returns>The DateTime structure.</returns>
        public static DateTime ToDate(string value)
        {
            try
            {
                return DateTime.Parse(value);
            }
            catch (Exception)
            {
                // Try to replace a textual time zone
                // format with a UTC time zone. 
                value = ReplaceTimezone.ReplaceTimeZone(value);
                try
                {
                    return DateTime.Parse(value);
                }
                catch (Exception)
                {
                }
            }

            // If all else fails, try epoch time.
            try
            {
                return new DateTime(Convert.ToInt64(value) * 1000);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Converts a string to a double value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The double value.</returns>
        public static double ToFloat(string value) 
        {
            return Convert.ToDouble(value);
        }

        /// <summary>
        /// Converts a string to an integer value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The integer value.</returns>
        public static int ToInteger(string value) 
        {
            return Convert.ToInt32(value);
        }

        /// <summary>
        /// Converts a string to a long value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>The long value.</returns>
        public static long ToLong(string value) 
        {
            return Convert.ToInt64(value);
        }
    }
}
