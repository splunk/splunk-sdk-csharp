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
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Reflection;
    using System.Text;
    using System.Web;

    /// <summary>
    /// The <see cref="HttpService"/> class represents a generic HTTP/S layer
    /// that facilitates HTTP/S access to Splunk.
    /// </summary>
    public class HttpService
    {
        /// <summary>
        /// Gets or sets the host name of the service.
        /// </summary>
        private string host;

        /// <summary>
        /// Gets or sets the port number of the service.
        /// </summary>
        private int port;

        /// <summary>
        /// Gets or sets the prefix.
        /// </summary>
        private string prefix;

        /// <summary>
        /// Gets or sets the scheme used to access the service.
        /// </summary>
        private string scheme;

        /// <summary>
        /// Cached user agent string
        /// </summary>
        private static string userAgent;

        /// <summary>
        /// Constant for http scheme.
        /// </summary>
        public const string SchemeHttp = "http";

        /// <summary>
        /// Constant for https scheme.
        /// </summary>
        public const string SchemeHttps = "https";

        /// <summary>
        /// Constant for default scheme.
        /// </summary>
        public const string DefaultScheme = SchemeHttps;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class.
        /// </summary>
        public HttpService() 
        {
            this.InitProperties();
            this.SetTrustPolicy();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class,
        /// adding the host.
        /// </summary>
        /// <param name="host">The host name.</param>
        public HttpService(string host) 
        {
            this.InitProperties();
            this.Host = host;
            this.SetTrustPolicy();
        }
  
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class,
        /// adding the host and the port. 
        /// </summary>
        /// <param name="host">The hostname.</param>
        /// <param name="port">The port.</param>
        public HttpService(string host, int port) 
        {
            this.InitProperties();
            this.Host = host;
            this.Port = port;
            this.SetTrustPolicy();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpService"/> class,
        /// adding the host, port and scheme.
        /// </summary>
        /// <param name="host">The hostname.</param>
        /// <param name="port">The port.</param>
        /// <param name="scheme">The scheme, either http, or https.</param>
        public HttpService(string host, int port, string scheme) 
        {
            this.InitProperties();
            this.Host = host;
            this.Port = port;

            scheme = scheme.ToLower();

            if (scheme != SchemeHttp && scheme != SchemeHttps)
            {
                throw new Exception("Scheme not supported");
            }
            
            this.Scheme = scheme;
            
            this.SetTrustPolicy();
        }

        /// <summary>
        /// Gets or sets the hostname of this service.
        /// </summary>
        /// <returns>The hostname.</returns>
        public string Host
        {
            get
            {
                return this.host;
            }

            set
            {
                this.host = value;
            }
        }

        /// <summary>
        /// Gets or sets the port of this service.
        /// </summary>
        /// <returns>The port.</returns>
        public int Port
        {
            get
            {
                return this.port;
            }

            set
            {
                this.port = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL prefix of this service, consisting of
        /// scheme://host:port.
        /// </summary>
        /// <returns>The URL prefix.</returns>
        public string Prefix
        {
            get
            {
                if (this.prefix == null)
                {
                    this.prefix = string.Format(
                        "{0}://{1}:{2}", this.Scheme, this.Host, this.Port);
                }
                return this.prefix;
            }

            set
            {
                this.prefix = value;
            }
        }

        /// <summary>
        /// Gets or sets the scheme used by this service.
        /// </summary>
        /// <returns>The scheme.</returns>
        public string Scheme
        {
            get
            {
                return this.scheme;
            }

            set
            {
                this.scheme = value;
            }
        }

        /// <summary>
        /// Initialize the properties.
        /// </summary>
        private void InitProperties() 
        {
            this.Host = "localhost";
            this.Port = 8089;
            this.Prefix = null;
            this.Scheme = "https";
        }

        /// <summary>
        /// Returns the count of arguments in the given dictionary.
        /// </summary>
        /// <param name="args">The dictionary.</param>
        /// <returns>The number of elements in the dictionary.</returns>
        private static int Count(Dictionary<string, object> args) 
        {
            if (args == null) 
            {
                return 0;
            }
            return args.Count;
        }

        /// <summary>
        /// Issues an HTTP GET request against the service using a given path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The responseMessage.</returns>
        public ResponseMessage Get(string path) 
        {
            return this.Send(path, new RequestMessage("GET"));
        }

        /// <summary>
        /// Issues an HTTP GET request against the service using a given path
        /// and arguments.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The ResponseMessage.</returns>
        public ResponseMessage Get(string path, Dictionary<string, object> args) 
        {
            if (Count(args) > 0) 
            {
                path = path + "?" + Args.Encode(args);
            }
            RequestMessage request = new RequestMessage("GET");
            return this.Send(path, request);
        }

        /// <summary>
        /// Constructs a fully qualified URL for this service using a given
        /// path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The fully qualified URL.</returns>
        public Uri GetUrl(string path) 
        {
            // Taken from http://stackoverflow.com/questions/781205/getting-a-url-with-an-url-encoded-slash
            // WebRequest can munge an encoded URL, and we don't want it to. 
            // This technique forces WebRequest to leave the URL alone. There is
            // no simple mechanism to ask .Net to do this, once there is, this 
            // code can be changed. This code also may break in the future, as 
            // it reaches into the class's non-public fields and whacks them 
            // with a hammer. 
            //
            // An alternative is to use 
            // genericUriParserOptions="DontUnescapePathDotsAndSlashes"
            // in app.config. However, that is a global setting, which is owned
            // by the application using the Splunk SDK. There's no way to 
            // configure the setting just of the Splunk SDK assembly.
            Uri uri = new Uri(this.Prefix + path);
            string paq = uri.PathAndQuery; // need to access PathAndQuery
            FieldInfo flagsFieldInfo =
                typeof(Uri).GetField(
                    "m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
            ulong flags = (ulong)flagsFieldInfo.GetValue(uri);
            // Flags.PathNotCanonical|Flags.QueryNotCanonical = 0x30
            flags &= ~((ulong)0x30);
            flagsFieldInfo.SetValue(uri, flags);
            return uri;
        }

        /// <summary>
        /// Issues a POST request against the service using a given path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The <see cref="responseMessage"/>.</returns>
        public ResponseMessage Post(string path) 
        {
            return this.Post(path, null);
        }

        /// <summary>
        /// Issues a POST request against the service using a given path and 
        /// arguments.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The <see cref="responseMessage"/>.</returns>
        public ResponseMessage 
            Post(string path, Dictionary<string, object> args) 
        {
            RequestMessage request = new RequestMessage("POST");
            if (Count(args) > 0) 
            {
                request.Content = Args.Encode(args);
            }
            return this.Send(path, request);
        }

        /// <summary>
        /// Issues a DELETE request against the service using a given path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The <see cref="responseMessage"/>.</returns>
        public ResponseMessage Delete(string path) 
        {
            RequestMessage request = new RequestMessage("DELETE");
            return this.Send(path, request);
        }

        /// <summary>
        /// Issues a DELETE request against the service using a given path and 
        /// arguments.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>The <see cref="responseMessage"/>.</returns>
        public ResponseMessage 
            Delete(string path, Dictionary<string, object> args) 
        {
            if (Count(args) > 0) 
            {
                path = path + "?" + Args.Encode(args);
            }
            RequestMessage request = new RequestMessage("DELETE");
            return this.Send(path, request);
        }

        /// <summary>
        /// The main HTTP send method. Sends any of the supported HTTP/S 
        /// methods to the service.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="request">The requestMessage.</param>
        /// <returns>The responseMessage.</returns>
        public virtual ResponseMessage Send(string path, RequestMessage request) 
        {
            // Construct a full URL to the resource
            Uri url = this.GetUrl(path);
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            // build web request.
            webRequest.Method = request.Method;
            webRequest.AllowAutoRedirect = true;

            // Add headers from request message
            Dictionary<string, string> header = request.Header;
            foreach (KeyValuePair<string, string> entry in header) 
            {
                webRequest.Headers.Add(entry.Key, entry.Value);
            }

            // Reflection can be expensive, thus cache userAgent value.
            if (userAgent == null)
            {
                var assembly = Assembly.GetExecutingAssembly();
                // Use file version since it is common for it to change without
                // an assembly version change.
                var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                var version = fvi.FileVersion;
                userAgent = "splunk-sdk-csharp/" + version;
            }
            webRequest.UserAgent = userAgent;

            webRequest.Accept = "*/*";
            if (request.Method.Equals("POST")) 
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
            }

            // Write out request content, if any
            object content = request.Content;
            if (content != null)
            {
                // Get the bytes for proper encoded length when going over the 
                // wire.
                byte[] bytes = Encoding.UTF8.GetBytes((string)content);
                try
                {
                    webRequest.ContentLength = bytes.GetLength(0);
                    Stream stream = webRequest.GetRequestStream();
                    StreamWriter streamWriter = new StreamWriter(stream);
                    streamWriter.Write(content);
                    streamWriter.Flush();
                    stream.Close();
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Excetion: " + e.Message);
                }
            }

            int status;
            HttpWebResponse response = null;

            try
            {
                response = (HttpWebResponse)webRequest.GetResponse();
                status = response.StatusCode.GetHashCode();
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && 
                    ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    status = resp.StatusCode.GetHashCode();
                }
                throw;
            }

            Stream input;
            input = status >= 400
                ? null
                : response != null ? response.GetResponseStream() : null;

            // If there is no input, then we can closeout this response
            // straight away.
            if (input == null)
            {
                response.Close();
                response = null;
            }

            ResponseMessage returnResponse = 
                new ResponseMessage(status, input, response);

            return returnResponse;
        }

        /// <summary>
        /// Sets the trust policy for communication to the service. The default 
        /// is trust all servers.
        /// </summary>
        private void SetTrustPolicy() 
        {
            if (ServicePointManager.ServerCertificateValidationCallback == null)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) => true;
            }

            ServicePointManager.DefaultConnectionLimit = 100;
        }
    }
}