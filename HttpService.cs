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
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Web;

    /// <summary>
    /// A genric HTTP/S layer that facilitates HTTP/S access to Splunk 
    /// </summary>
    public class HttpService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class.
        /// </summary>
        public HttpService() {
            this.InitProperties();
            this.SetTrustPolicy();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class,
        /// adding the host.
        /// </summary>
        /// <param name="host">The host name</param>
        public HttpService(string host) {
            this.InitProperties();
            this.Host = host;
            this.SetTrustPolicy();
        }
  
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class,
        /// adding the host and the port. 
        /// </summary>
        /// <param name="host">The hostname</param>
        /// <param name="port">The port</param>
        public HttpService(string host, int port) {
            this.InitProperties();
            this.Host = host;
            this.Port = port;
            this.SetTrustPolicy();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class,
        /// adding the host, port and scheme.
        /// </summary>
        /// <param name="host">The hostname</param>
        /// <param name="port">The port</param>
        /// <param name="scheme">The scheme, either http, or https</param>
        public HttpService(string host, int port, string scheme) {
            this.InitProperties();
            this.Host = host;
            this.Port = port;
            this.Scheme = scheme;
            this.SetTrustPolicy();
        }

        /// <summary>
        /// Gets or sets the host name of the service
        /// </summary>
        protected string Host {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the port number of the service
        /// </summary>
        protected int Port {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        protected string Prefix {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the scheme used to access the service
        /// </summary>
        protected string Scheme {
            get;
            set;
        }

        /// <summary>
        /// Initialize the properties.
        /// </summary>
        private void InitProperties() {
            this.Host = "localhost";
            this.Port = 8089;
            this.Prefix = string.Empty;
            this.Scheme = "https";
        }

        /// <summary>
        /// Returns the count of arguments in the given dictionary
        /// </summary>
        /// <param name="args">The dictionary</param>
        /// <returns>The number of elements in the dictionary</returns>
        private static int Count(Dictionary<string, object> args) {
            if (args == null) {
                return 0;
            }
            return args.Count;
        }

        /// <summary>
        /// Issues an HTTP GET request against the service using a given path.
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>the responseMessage</returns>
        public ResponseMessage Get(string path) {
            return this.Send(path, new RequestMessage("GET"));
        }

        /// <summary>
        /// Issues an HTTP GET request against the service using a given path
        /// and arguments
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="args">The arguments</param>
        /// <returns>the ResponseMessage</returns>
        public ResponseMessage Get(string path, Dictionary<string, object> args) {
            if (Count(args) > 0) {
                path = path + "?" + Args.Encode(args);
            }
            RequestMessage request = new RequestMessage("GET");
            return this.Send(path, request);
        }

        /// <summary>
        /// Returns the hostname of this service
        /// </summary>
        /// <returns>The hostname</returns>
        public string GetHost() {
            return this.Host;
        }

        /// <summary>
        /// Returns the port of this service
        /// </summary>
        /// <returns>The port</returns>
        public int GetPort() {
            return this.Port;
        }
        
        /// <summary>
        /// Returns the URL prefix of this service, consisting of
        /// scheme://host:port
        /// </summary>
        /// <returns>The URL prefix</returns>
        public string GetPrefix() {
            if (this.Prefix == null) {
                this.Prefix = string.Format("{0}://{1}:{2}", this.Scheme, this.Host, this.Port);
            }
            return this.Prefix;
        }

        /// <summary>
        /// Returns the scheme used by this service.
        /// </summary>
        /// <returns>The scheme</returns>
        public string GetScheme() {
            return this.Scheme;
        }

        /// <summary>
        /// Constructs a fully qualified URL for this service using a given path.
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The fully qualified URL</returns>
        public string GetUrl(string path) {
            return this.GetPrefix() + path;
        }

        /// <summary>
        /// Issues a POST request against the service using a given path.
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The <see cref="repsonseMessage"/></returns>
        public ResponseMessage Post(string path) {
            return this.Post(path, null);
        }

        /// <summary>
        /// Issues a POST request against the service using a given path and arguments.
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="args">The arguments</param>
        /// <returns>The <see cref="repsonseMessage"/></returns>
        public ResponseMessage Post(string path, Dictionary<string, object> args) {
            RequestMessage request = new RequestMessage("POST");
            if (Count(args) > 0) {
                request.SetContent(Args.Encode(args));
            }
            return this.Send(path, request);
        }

        /// <summary>
        /// Issues a DELETE request against the service using a given path.
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The <see cref="repsonseMessage"/></returns>
        public ResponseMessage Delete(string path) {
            RequestMessage request = new RequestMessage("DELETE");
            return this.Send(path, request);
        }

        /// <summary>
        /// Issues a DELETE request against the service using a given path and arguments.
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="args">The arguments</param>
        /// <returns>The <see cref="repsonseMessage"/></returns>
        public ResponseMessage Delete(string path, Dictionary<string, object> args) {
            if (Count(args) > 0) {
                path = path + "?" + Args.Encode(args);
            }
            RequestMessage request = new RequestMessage("DELETE");
            return this.Send(path, request);
        }

        /// <summary>
        /// Open a TcpClient connected to the service
        /// </summary>
        /// <returns>The TcpClient object</returns>
        public TcpClient Open() {
            this.SetTrustPolicy();
            TcpClient sock = new TcpClient();
            sock.Connect(this.Host, this.Port);
            return sock.Connected ? sock : null;
        }

        /// <summary>
        /// The main HTTP send method. Sends any of the supported HTTP/S 
        /// methods to the service.
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="request">The requestMessage</param>
        /// <returns>The responseMessage</returns>
        public virtual ResponseMessage Send(string path, RequestMessage request) {
            // Construct a full URL to the resource
            string url = this.GetUrl(path);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            // build web request.
            webRequest.Method = request.GetMethod();
            webRequest.AllowAutoRedirect = true;

            // Add headers from request message
            Dictionary<string, string> header = request.GetHeader();
            foreach (KeyValuePair<string, string> entry in header) {
                webRequest.Headers.Add(entry.Key, entry.Value);
            }

            webRequest.UserAgent = "splunk-sdk-csharp/0.1";
            webRequest.Accept = "*/*";
            if (request.GetMethod().Equals("POST")) {
                webRequest.ContentType = "application/x-www-form-urlencoded";
            }

            // Write out request content, if any
            object content = request.GetContent();
            if (content != null) {
                webRequest.ContentLength = ((string)content).Length;
                Stream stream = webRequest.GetRequestStream();
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write((string)content);
                streamWriter.Close();
                stream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            int status = response.StatusCode.GetHashCode();

            Stream input;
            input = status >= 400
                ? null
                : response.GetResponseStream();

            ResponseMessage returnResponse = new ResponseMessage(status, input);

            if (status >= 400) {
                throw HttpException.CreateFromLastError("HTTP error");
            }

            return returnResponse;
        }

        /// <summary>
        /// Sets the trust policy for communication to the service. The default is trust all servers.
        /// </summary>
        private void SetTrustPolicy() {
            ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
        }
    }
}