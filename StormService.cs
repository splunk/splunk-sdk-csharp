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
    using System.Text;

    /// <summary>
    /// The StormService class represents a Splunk service instance at a
    /// given address (host:port), accessed using the http or https
    /// protocol scheme.
    /// Using the StormService class, you can get a Receiver object
    /// and log events to it.  
    /// </summary>
    public class StormService : Service
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StormService"/> class.
        /// </summary>
        public StormService() : base("api.splunkstorm.com", 443, HttpService.SchemeHttps)
        {
            this.SimpleReceiverEndPoint = "/1/inputs/http";
        }

        /// <summary>
        /// Encodes an ASCII string to Base64.
        /// </summary>
        /// <param name="toEncode">The ASCII string to encode</param>
        /// <returns>The base 64 encoded string</returns>
        private static string Encode64(string toEncode)
        {
            byte[] toEncodeAsBytes = 
                System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            return System.Convert.ToBase64String(toEncodeAsBytes);
        }

        /// <summary>
        /// Establishes a connection to a Splunk Storm service using a 
        /// dictionary of arguments. This member creates a new StormService 
        /// instance and authenticates the session using credentials passed in 
        /// from the Dictionary of args.
        /// </summary>
        /// <param name="args">The credentials </param>
        /// <returns>The Storm Service object</returns>
        public static new StormService Connect(Dictionary<string, object> args)
        {
            StormService service = new StormService();
            if (args.ContainsKey("StormToken"))
            {
                string username = (string)args["StormToken"];
                string preToken = username + ":x";
                service.Token = "Basic " + Encode64(preToken);
            }

            return service;
        }
    }
}
