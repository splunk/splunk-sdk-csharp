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
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// The <see cref="Receiver"/> class exposes methods to send events to
    /// Splunk via the simple or streaming receiver endpoint.
    /// </summary>
    public class Receiver
    {        
        /// <summary>
        /// A reference to the attached service.
        /// </summary>
        private Service service = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Receiver"/> class.
        /// </summary>
        /// <param name="service">The service</param>
        public Receiver(Service service) 
        {
            this.service = service;
        }

        /// <summary>
        /// Creates a socket to the Splunk server using the default index, and 
        /// default port.
        /// </summary>
        /// <returns>The Stream</returns>
        public Stream Attach() 
        {
            return this.Attach(null, null);
        }

        /// <summary>
        /// Creates a socket to the Splunk server using the named index, and 
        /// default port.
        /// </summary>
        /// <param name="indexName">The index to write to</param>
        /// <returns>The Stream</returns>
        public Stream Attach(string indexName) 
        {
            return this.Attach(indexName, null);
        }

        /// <summary>
        /// Creates a socket to the Splunk server using the default index and 
        /// variable arguments.
        /// </summary>
        /// <param name="args">The variable arguments</param>
        /// <returns>The Socket</returns>
        public Stream Attach(Args args)
        {
            return this.Attach(null, args);
        }

        /// <summary>
        /// Creates a socket to the Splunk server using the named index and 
        /// variable arguments.
        /// </summary>
        /// <param name="indexName">The index name.</param>
        /// <param name="args">The variable arguments.</param>
        /// <returns>The Socket.</returns>
        public Stream Attach(string indexName, Args args) 
        {
            Stream stream;
            if (this.service.Scheme == HttpService.SchemeHttps)
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(this.service.Host, this.service.Port);
                var sslStream = new SslStreamWrapper(tcp);
                sslStream.AuthenticateAsClient(this.service.Host);
                stream = sslStream;
            }
            else
            {
                Socket socket = this.service.Open(this.service.Port);
                stream = new NetworkStream(socket, true);
            }
            
            string postUrl = "POST /services/receivers/stream";
            if (indexName != null) 
            {
                postUrl = postUrl + "?index=" + indexName;
            }

            if (args != null && args.Count > 0) 
            {
                postUrl = postUrl + ((indexName == null) ? "?" : "&");
                postUrl = postUrl + args.Encode();
            }

            string header = string.Format(
                "{0} HTTP/1.1\r\n" +
                "Host: {1}:{2}\r\n" +
                "Accept-Encoding: identity\r\n" +
                "Authorization: {3}\r\n" +
                "X-Splunk-Input-Mode: Streaming\r\n\r\n",
                postUrl,
                this.service.Host, 
                this.service.Port,
                this.service.Token);

            var bytes = Encoding.UTF8.GetBytes(header);
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();

            return stream;
        }

        /// <summary>
        /// Submits the data using HTTP post, to the default index.
        /// </summary>
        /// <param name="data">The data</param>
        public void Submit(string data) 
        {
            this.Submit(null, null, data);
        }

        /// <summary>
        /// Submits the data using HTTP post, to the named index.
        /// </summary>
        /// <param name="indexName">The index name./param>
        /// <param name="data">The data.</param>
        public void Submit(string indexName, string data) 
        {
            this.Submit(indexName, null, data);
        }

        /// <summary>
        /// Submits the data using HTTP post, using variable arguments to the 
        /// default index.
        /// </summary>
        /// <param name="args">The variable arguments.</param>
        /// <param name="data">The data.</param>
        public void Submit(Args args, string data) 
        {
            this.Submit(null, args, data);
        }

        /// <summary>
        /// Submits the data to the named index using variable arguments.
        /// </summary>
        /// <param name="indexName">The named index.</param>
        /// <param name="args">The variable arguments.</param>
        /// <param name="data">The data.</param>
        public void Submit(string indexName, Args args, string data) 
        {
            string sendString = string.Empty;
            RequestMessage request = new RequestMessage("POST");
            request.Content = data;
            if (indexName != null) 
            {
                sendString = string.Format("?index={0}", indexName);
            }

            if (args != null && args.Count > 0) 
            {
                sendString = sendString + ((indexName == null) ? "?" : "&");
                sendString = sendString + args.Encode();
            }
            this.service.Send(
                this.service.SimpleReceiverEndPoint + sendString, request);
        }

        /// <summary>
        /// Alias for submit().
        /// </summary>
        /// <param name="data">The data.</param>
        public void Log(string data) 
        {
            this.Submit(data);
        }

        /// <summary>
        /// Alias for submit()
        /// </summary>
        /// <param name="indexName">The index name</param>
        /// <param name="data">The data</param>
        public void Log(string indexName, string data) 
        {
            this.Submit(indexName, data);
        }

        /// <summary>
        /// Alias for submit().
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="data">The data</param>
        public void Log(Args args, string data) 
        {
            this.Submit(args, data);
        }

        /// <summary>
        /// Alias for submit().
        /// </summary>
        /// <param name="indexName">The index name</param>
        /// <param name="args">The arguments</param>
        /// <param name="data">The data</param>
        public void Log(string indexName, Args args, string data) 
        {
            this.Submit(indexName, args, data);
        }

        /// <summary>
        /// Wrapper class of SslStream for closing TCP connection 
        /// when closing the stream.
        /// </summary>
        private class SslStreamWrapper : SslStream
        {
            /// <summary>
            /// The TcpClient object the SSLStream object is based on.
            /// </summary>
            private TcpClient tcpClient;
            
            /// <summary>
            /// Initializes a new instance of the <see cref="SslStreamWrapper"/> class.
            /// </summary>
            /// <param name="tcpClient">
            /// A TcpClient object the SSLStream object is based on.
            /// </param>
            public SslStreamWrapper(
                TcpClient tcpClient)
                : base(
                    tcpClient.GetStream(),
                    false,
                    ServicePointManager.ServerCertificateValidationCallback)
            {
                this.tcpClient = tcpClient;
            }

            /// <summary>
            /// Release resources including the tcpClient.
            /// </summary>
            /// <param name="disposing">True to release both managed and unmanaged resources; 
            ///     false to release only unmanaged resources. </param>
            protected override void Dispose(bool disposing)
            {
                // Dispose(bool disposing) executes in two distinct scenarios.
                // If disposing equals true, the method has been called directly
                // or indirectly by a user's code. Managed and unmanaged resources
                // can be disposed.
                // If disposing equals false, the method has been called by the
                // runtime from inside the finalizer and you should not reference
                // other objects. Only unmanaged resources can be disposed.
                // If this is called from the finalizer, don't reference tcpClient.
                // It is ok since its manage resources will be cleaned up as part of regular GC and 
                // the runtime will call its Dispose with 'disposing' being false.
                if (disposing)
                {
                    this.tcpClient.Close();
                }
            }
        }
    }
}
