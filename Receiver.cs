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
    using System.IO;
    using System.Linq;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    /// <summary>
    /// The receiver class. This class exposes methods to send events to splunk
    /// via the simple or streaming receiver endpoint.
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
        /// Creates a SSL stream to the splunk server using the default index, and 
        /// default port.
        /// </summary>
        /// <returns>SSL stream</returns>
        public SslStream Attach() 
        {
            return this.Attach(null, null);
        }

        /// <summary>
        /// Creates a SSL stream to the splunk server using the named index, and 
        /// default port.
        /// </summary>
        /// <param name="indexName">The index to write to</param>
        /// <returns>SSL stream</returns>
        public SslStream Attach(string indexName) 
        {
            return this.Attach(indexName, null);
        }

        /// <summary>
        /// Creates a SSL stream to the splunk server using the default index and 
        /// variable arguments.
        /// </summary>
        /// <param name="args">The variable arguments</param>
        /// <returns>SSL stream</returns>
        public SslStream Attach(Args args)
        {
            return this.Attach(null, args);
        }

        /// <summary>
        /// Creates a SSL stream to the splunk server using the named index and 
        /// variable arguments.
        /// </summary>
        /// <param name="indexName">The index name</param>
        /// <param name="args">The variable arguments</param>
        /// <returns>SSL stream</returns>
        public SslStream Attach(string indexName, Args args) 
        {
            TcpClient tcp = new TcpClient();
            tcp.NoDelay = true;
            tcp.Connect(this.service.Host, this.service.Port);            
            var sslStream = new SslStream(tcp.GetStream(), false, new RemoteCertificateValidationCallback(ValidateSslCertificate), null);
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
                "Authorization: {3}\r\n" +
                "x-splunk-input-mode: streaming\r\n\r\n",
                postUrl,
                this.service.Host,
                this.service.Port,
                this.service.Token);
            sslStream.AuthenticateAsClient(this.service.Host);
            sslStream.Write(Encoding.UTF8.GetBytes(header));
            sslStream.Flush();      
            return sslStream;
        }

        /// <summary>
        /// Validates the SSL (X509) certificate. Always valid.
        /// SECURITY WARNING: Though the SSL certificate is validated, nothing is done if the certificate
        /// produces errors. Example: Mismatch hostname on certificate. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool ValidateSslCertificate(object sender, X509Certificate certificate,
                                                    X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        /// <summary>
        /// Submits the data using HTTP post, to the default index
        /// </summary>
        /// <param name="data">The data</param>
        public void Submit(string data) 
        {
            this.Submit(null, null, data);
        }

        /// <summary>
        /// Submits the data using HTTP post, to the named index
        /// </summary>
        /// <param name="indexName">The index name</param>
        /// <param name="data">The data</param>
        public void Submit(string indexName, string data) 
        {
            this.Submit(indexName, null, data);
        }

        /// <summary>
        /// Submits the data using HTTP post, using variable arguments to the 
        /// default index.
        /// </summary>
        /// <param name="args">The variable arguments</param>
        /// <param name="data">The data</param>
        public void Submit(Args args, string data) 
        {
            this.Submit(null, args, data);
        }

        /// <summary>
        /// Submits the data to the named index using variable arguments.
        /// </summary>
        /// <param name="indexName">The named index</param>
        /// <param name="args">The variable arguments</param>
        /// <param name="data">The data</param>
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
        /// Alias for submit()
        /// </summary>
        /// <param name="data">The data</param>
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
        /// Alias for submit()
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <param name="data">The data</param>
        public void Log(Args args, string data) 
        {
            this.Submit(args, data);
        }

        /// <summary>
        /// Alias for submit()
        /// </summary>
        /// <param name="indexName">The index name</param>
        /// <param name="args">The arguments</param>
        /// <param name="data">The data</param>
        public void Log(string indexName, Args args, string data) 
        {
            this.Submit(indexName, args, data);
        }
    }
}
