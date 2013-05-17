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
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Web;

    /// <summary>
    /// The <see cref="Args"/> class is a helper class for working with Splunk
    /// REST API arguments. This extension is used mainly for encoding
    /// arguments for UTF-8 transmission to a Splunk instance in a key/value
    /// pairing for a string, or key=value1&amp;key=value2 (and so on) for an 
    /// array of strings.
    /// </summary>
    public class Args : Dictionary<string, object>, ISerializable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Args"/> class.
        /// </summary>
        public Args() 
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Args"/> class,
        /// with a single key/value pair.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public Args(string key, object value) 
        {
            base[key] = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Args"/> class, 
        /// with an existing dictionary. 
        /// </summary>
        /// <param name="values">The existing dictionary.</param>
        public Args(Dictionary<string, object> values) 
        {
            foreach (KeyValuePair<string, object> entry in values) 
            {
                base[entry.Key] = entry.Value;
            }
        }

        /// <summary>
        /// Adds a key/value pair to an <see cref="Args"/> object,
        /// or overwrites the value if the key exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="Args"/> object.</returns>
        public Args Set(string key, object value) 
        {
            base[key] = value;
            return this;
        }

        /// <summary>
        /// Creates a new, empty <see cref="Args"/> object.
        /// </summary>
        /// <returns>The new, empty <see cref="Args"/> object.</returns>
        public static Args Create() 
        {
            return new Args();
        }

        /// <summary>
        /// Creates a new <see cref="Args"/> instance and initializes it with 
        /// a single key/value pair.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The new initialized <see cref="Args"/> object.</returns>
        public static Args Create(string key, object value) 
        {
            return new Args(key, value);
        }
        
        /// <summary>
        /// Creates a new <see cref="Args"/> instance and initializes it with a
        /// pre-existing dictionary.
        /// </summary>
        /// <param name="values">The existing dictionary.</param>
        /// <returns>The new initialized <see cref="Args"/> object.</returns>
        public static Args Create(Dictionary<string, object> values) 
        {
            return values == null ? new Args() : new Args(values);
        }

        /// <summary>
        /// Encodes a single string with UTF-8 encoding.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns>The UTF-8 encoded string.</returns>
        public static string Encode(string value) 
        {
            if (value == null) 
            {
                return string.Empty;
            }
            return HttpUtility.UrlEncode(value);
        }

        /// <summary>
        /// Encodes a dictionary of strings or string arrays into a single 
        /// UTF-8-encoded string.
        /// </summary>
        /// <param name="args">The string or string array.</param>
        /// <returns>The UTF-8-encoded string.</returns>
        public static string Encode(Dictionary<string, object> args) 
        {
            return Args.Create(args).Encode();
        }

        /// <summary>
        /// Encodes an argument with a list-valued argument.
        /// </summary>
        /// <param name="builder">The string builder.</param>
        /// <param name="key">The key.</param>
        /// <param name="values">The string array.</param>
        private void 
            EncodeValues(StringBuilder builder, string key, string[] values) 
        {
            key = Encode(key);
            foreach (string value in values) 
            {
                if ((builder.Length > 0) && 
                    (builder[builder.Length - 1] != '&')) 
                {
                    builder.Append('&');
                }
                builder.Append(key);
                builder.Append('=');
                builder.Append(Encode(value));
            }
        }

        /// <summary>
        /// Encodes an <see cref="Args"/> instance into a UTF-8 encoded string.
        /// </summary>
        /// <returns>The UTF-8 encoded string.</returns>
        public string Encode() 
        {
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, object> entry in this) 
            {
                if (builder.Length > 0) 
                {
                    builder.Append('&');
                }
                string key = entry.Key;
                object value = entry.Value;

                if (value is string[]) 
                {
                    this.EncodeValues(builder, key, (string[])value);
                }
                else 
                {
                    builder.Append(Encode(key));
                    builder.Append('=');
                    builder.Append(Encode(value.ToString()));
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Returns the dictionary value of a specific key, or the default 
        /// value if the key is not found.
        /// </summary>
        /// <param name="args">The dictionary.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The key's value in the dictionary, or the default value if
        /// not found.</returns>
        public static string 
           Get(Dictionary<string, object> args, string key, string defaultValue) 
        {
            if (!args.ContainsKey(key)) 
            {
                return defaultValue;
            }
            return (string)args[key];
        }
    }
}
