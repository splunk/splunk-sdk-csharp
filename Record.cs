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
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the basic data representation, extending the basic 
    /// Dictionary with some basic get methods.
    /// </summary>
    public class Record : Dictionary<string, object>
    {
        /// <summary>
        /// Returns the boolean value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public bool GetBoolean(string key) 
        {
            return Value.ToBoolean(this.GetString(key));
        }

        /// <summary>
        /// Returns the boolean value associated with the given key, adding
        /// a default value if the key is not present in the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value</returns>
        public bool GetBoolean(string key, bool defaultValue) 
        {
            if (this.ContainsKey(key)) 
            {
                return defaultValue;
            }
            return Value.ToBoolean(this.GetString(key));
        }

        /// <summary>
        /// Returns the long byte count value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public long GetByteCount(string key) 
        {
            return Value.ToByteCount(this.GetString(key));
        }

        /// <summary>
        /// Returns the long byte count value associated with the given key, adding
        /// a default value if the key is not present in the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value</returns>
        public long GetByteCount(string key, long defaultValue) 
        {
            if (!this.ContainsKey(key)) 
            {
                return defaultValue;
            }
            return Value.ToByteCount(this.GetString(key));
        }

        /// <summary>
        /// Returns the DateTime value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The DateTime structure</returns>
        public DateTime GetDate(string key) 
        {
            return Value.ToDate(this.GetString(key));
        }

        /// <summary>
        /// Returns the DateTime value associated with the given key. adding
        /// a default value if the key is not present in the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value</returns>
        public DateTime GetDate(string key, DateTime defaultValue)
        {
            if (!this.ContainsKey(key))
            {
                return defaultValue;
            }

            return Value.ToDate(this.GetString(key));
        }

        /// <summary>
        /// Returns the double value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public double GetFloat(string key) 
        {
            return Value.ToFloat(this.GetString(key));
        }

        /// <summary>
        /// Returns the int value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public int GetInteger(string key) 
        {
            return Value.ToInteger(this.GetString(key));
        }

        /// <summary>
        /// Returns the int value associated with the given key, adding
        /// a default value if the key is not present in the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value</returns>
        public int GetInteger(string key, int defaultValue) 
        {
            if (!this.ContainsKey(key)) 
            {
                return defaultValue;
            }
            return Value.ToInteger(this.GetString(key));
        }

        /// <summary>
        /// Returns the long value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public long GetLong(string key) 
        {
            return Value.ToLong(this.GetString(key));
        }

        /// <summary>
        /// Returns the int value associated with the given key, adding
        /// a default value if the key is not present in the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value</returns>
        public long GetLong(string key, int defaultValue) 
        {
            if (!this.ContainsKey(key)) 
            {
                return defaultValue;
            }
            return Value.ToLong(this.GetString(key));
        }

        /// <summary>
        /// Returns the string value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public string GetString(string key) 
        {
            object returnValue = null;
            this.TryGetValue(key, out returnValue);
            return (string)returnValue;
        }

        /// <summary>
        /// Returns the string value associated with the given key, adding
        /// a default value if the key is not present in the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default</param>
        /// <returns>The value</returns>
        public string GetString(string key, string defaultValue) 
        {
            if (!this.ContainsKey(key)) 
            {
                return defaultValue;
            }
            object returnValue = null;
            this.TryGetValue(key, out returnValue);
            return (string)returnValue;
        }

        /// <summary>
        /// Returns the string[] value associated with the given key.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The value</returns>
        public string[] GetStringArray(string key) 
        {
            if (this.ContainsKey(key))
            {
                return ((List<object>)this[key]).Select(i => i.ToString()).ToList().ToArray();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the string[] value associated with the given key, adding
        /// a default value if the key is not present in the dictionary.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="defaultValue">The default value</param>
        /// <returns>The value</returns>
        public string[] GetStringArray(string key, string[] defaultValue) 
        {
            if (!this.ContainsKey(key)) 
            {
                return defaultValue;
            }
            return this.GetStringArray(key);
        }

        /**
         * Returns the value associated with the given key, cast to the given type
         * parameter.
         *
         * @param key The key of the value being retrieved.
         * @param <T> The type to cast the return value to.
         * @return The value associated with the given key, cast to the given type.
        <T> T GetValue(string key) {
            return (T)get(key);
        }
         */

        /**
         * Returns the value associated with the given key, or {@code defaultValue}
         * if the key does not exist, cast to the given type parameter.
         *
         * @param key The key of the value being retrieved.
         * @param defaultValue The value to return if the key does not exist.
         * @param <T> The type to cast the return value to.
         * @return The value associated with the given key, or {@code defautlValue}
         *         if the key does not exist.
        <T> T GetValue(string key, T defaultValue) {
            if (!containsKey(key)) return defaultValue;
            return (T)get(key);
        }
         */
    }
}
