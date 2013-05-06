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
    using System.Net.Sockets;
    using System.Xml;
    
    /// <summary>
    /// The <see cref="Service"/> class represents a Splunk service instance 
    /// at a given address (<i>host</i>:<i>port</i>) accessed using the http or
 	/// https protocol scheme.
    /// </summary>
	/// <remarks>
    /// <para>
    /// A <see cref="Service"/> instance also captures an optional namespace 
    /// contact that consists of an optional owner name (or "-" wildcard) and 
    /// optional app name (or "-" wildcard).
    /// </para>
    /// <para>
    /// To access the <see cref="Service"/> members, the <see cref="Service"/>
    /// instance must be authenticated by presenting credentials using the 
    /// <see cref="Login"/> method, or by constructing the
    /// <see cref="Service"/> instance using the <see cref="Connect"/> method.
    /// </para>	/// </remarks>
    public class Service : BaseService 
    {
        /// <summary>
        /// The default host name, which is used when a host name is not 
        /// provided.
        /// </summary>
        private static string defaultHost = "localhost";

        /// <summary>
        /// The default port number, which is used when a port number is not
        /// provided.
        /// </summary>
        private static int defaultPort = 8089;

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class
        /// </summary>
        /// <param name="host">The hostname.</param>
        public Service(string host) 
            : base(host) 
        {
            this.InitProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class,
        /// with host and port.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port.</param>
        public Service(string host, int port) 
            : base(host, port) 
        {
            this.InitProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class,
        /// with host, port, and scheme.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port</param>
        /// <param name="scheme">The scheme, http or https.</param>
        public Service(string host, int port, string scheme)
            : base(host, port, scheme) 
        {
            this.InitProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class,
        /// with a <see cref="ServiceArgs"/> argument list.
        /// </summary>
        /// <param name="args">The service arguments.</param>
        public Service(ServiceArgs args)
            : base() 
        {
            this.InitProperties();
            this.App = args.App;
            this.Host = args.Host == null ? defaultHost : args.Host;
            this.Owner = args.Owner;
            this.Port = args.Port == 0 ? defaultPort : args.Port;
            this.Scheme = args.Scheme == null ? HttpService.DefaultScheme : args.Scheme;
            this.Token = args.Token;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class
        /// with a general argument list.
        /// </summary>
        /// <param name="args">The service arguments.</param>
        public Service(Dictionary<string, object> args) 
            : base() 
        {
            this.InitProperties();
            this.App = Args.Get(args, "app", null);
            this.Host = Args.Get(args, "host", defaultHost);
            this.Owner = Args.Get(args, "owner", null);
            this.Port = args.ContainsKey("port") 
                ? Convert.ToInt32(args["port"]) 
                : defaultPort;
            this.Scheme = Args.Get(args, "scheme", HttpService.DefaultScheme);
            this.Token = Args.Get(args, "token", null);
        }

        /// <summary>
        /// Gets or sets the current app context.
        /// </summary>
        private string App 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current owner context. 
        /// </summary>
		/// <remarks>
		/// A value of "nobody" means that all users have access to the 
		/// resource.
		/// </remarks>
        private string Owner 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the password, which is used to authenticate the
 		/// Splunk instance.
        /// </summary>
        private string Password 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the default password endpoint, which can change over
 		/// Splunk versions.
        /// </summary>
        internal string PasswordEndPoint 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the default simple receiver endpoint.
        /// </summary>
        internal string SimpleReceiverEndPoint 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current session (authorization) token.
        /// </summary>
        public string Token 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Splunk account username, which is used to 
        /// authenticate the Splunk instance.
        /// </summary>
        private string Username 
        {
            get; set;
        }

        /// <summary>
        /// Gets the version of this Splunk instance, once logged in.
        /// </summary>
        public string Version 
        {
            get; private set;
        }

        /// <summary>
        /// Initializes all properties to their default values.
        /// </summary>
        private void InitProperties() 
        {
            this.SimpleReceiverEndPoint = "receivers/simple";
            this.PasswordEndPoint = "admin/passwords";
            this.App = null;
            this.Owner = null;
            this.Password = null;
            this.Token = null;
            this.Username = null;
            this.Version = null;
        }

        /// <summary>
        /// Establishes a connection to a Splunk service using a map of 
        /// arguments. 
        /// </summary>
        /// <param name="args">The connection arguments.</param>
        /// <returns>A new <see cref="Service"/> instance.</returns>
		/// <remarks>
		/// This method creates a new <see cref="Service"/> instance and 
        /// authenticates the session using credentials passed in from the args
        /// dictionary.
		/// </remarks>
        public static Service Connect(Dictionary<string, object> args) 
        {
            Service service = new Service(args);
            if (args.ContainsKey("username")) 
            {
                string username = Args.Get(args, "username", null);
                string password = Args.Get(args, "password", null);
                service.Login(username, password);
            }
            return service;
        }

        /// <summary>
        /// Returns the array of the system capabilities.
        /// </summary>
        /// <returns>An array of system capabilities.</returns>
        public string[] Capabilities()
        {
            Entity caps = new Entity(this, "authorization/capabilities");
            return caps.GetStringArray("capabilities");
        }

        /// <summary>
        /// Runs a search using the search/jobs/export endpoint, which streams
        /// results back via a <see cref="Stream"/>.
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <returns>A results <see cref="Stream"/>.</returns>
        public Stream Export(string search) 
        {
            return this.Export(search, null);
        }

        /// <summary>
        /// Runs a search using the search/jobs/export endpoint, which streams
        /// results back via a <see cref="Stream"/>. 
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <param name="args">The search arguments.</param>
        /// <returns>A results <see cref="Stream"/>.</returns>
        public Stream Export(string search, Args args) 
        {
            args = Args.Create(args).AlternateAdd("search", search);
            ResponseMessage response = Get("search/jobs/export", args);
            return new ExportResultsStream(response.Content);
        }

        /// <summary>
        /// Runs a search using the search/jobs/export endpoint which streams
        /// results back via a Stream. 
        /// </summary>
        /// <param name="search">The search query string.</param>
        /// <param name="args">The search arguments.</param>
        /// <returns>The results stream.</returns>
        public Stream Export(string search, JobExportArgs args)
        {
            return Export(search, (Args) args);
        }

        /// <summary>
        /// Ensures that the given path is fully qualified, prepending a path
        /// prefix if necessary. 
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>A fully qualified URL.</returns>
		/// <remarks>
		/// The path prefix is constructed using the current owner and app
		/// context when available.
		/// </remarks>
        public string Fullpath(string path) 
        {
            return this.Fullpath(path, null);
        }

        /// <summary>
        /// Ensures that the given path is fully qualified, prepending a path
        /// prefix if necessary. 
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="splunkNamespace">The Splunk namespace.</param>
        /// <returns>A fully qualified URL.</returns>
		/// <remarks>
		/// The path prefix is constructed using the current owner and app
		/// context when available.
		/// </remarks>
        public string Fullpath(string path, Args splunkNamespace) 
        {
            // If already fully qualified (i.e. root begins with /), then return
            // the already qualified path.
            if (path.StartsWith("/")) 
            {
                return path;
            }

            // If no namespace at all and no service instance of app and no
            // sharing, return base service endpoint + path.
            if (splunkNamespace == null && this.App == null) 
            {
                return "/services/" + path;
            }

            // Base namespace values.
            string localApp = this.App;
            string localOwner = this.Owner;
            string localSharing = string.Empty;

            // Override with invocation namespace if set.
            if (splunkNamespace != null) 
            {
                if (splunkNamespace.ContainsKey("app")) 
                {
                    localApp = (string)splunkNamespace["app"];
                }
                if (splunkNamespace.ContainsKey("owner")) 
                {
                    localOwner = (string)splunkNamespace["owner"];
                }
                if (splunkNamespace.ContainsKey("sharing")) 
                {
                    localSharing = (string)splunkNamespace["sharing"];
                }
            }

            // Sharing, if set calls for special mapping, override here.
            // "user"    --> {user}/{app}
            // "app"     --> nobody/{app}
            // "global"  --> nobody/{app}
            // "system"  --> nobody/system
            if (localSharing.Equals("app") || localSharing.Equals("global")) 
            {
                localOwner = "nobody";
            }
            else if (localSharing.Equals("system")) 
            {
                localApp = "system";
                localOwner = "nobody";
            }

            return string.Format(
                "/servicesNS/{0}/{1}/{2}",
                localOwner == null ? "-" : localOwner,
                localApp   == null ? "-" : localApp,
                path);
        }

        /// <summary>
        /// Returns the collection of applications.
        /// </summary>
        /// <returns>An <see cref="EntityCollection"/> of 
		/// <see cref="Application">Applications</see>.</returns>
        public EntityCollection<Application> GetApplications() 
        {
            return new EntityCollection<Application>(
                this, "/services/apps/local", typeof(Application));
        }

        /// <summary>
        /// Returns the collection of configurations.
        /// </summary>
        /// <returns>A <see cref="ConfCollection"/>.</returns>
        public ConfCollection GetConfs() 
        {
            return new ConfCollection(this);
        }

        /// <summary>
        /// Returns the collection of applications.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="ConfCollection"/>.</returns>
        public ConfCollection GetConfs(Args args) 
        {
            return new ConfCollection(this, args);
        }

        /// <summary>
        /// Returns a collection of saved event types.
        /// </summary>
        /// <returns>A <see cref="EventTypeCollection"/>.</returns>
        public EventTypeCollection GetEventTypes()
        {
            return new EventTypeCollection(this);
        }

        /// <summary>
        /// Returns a collection of saved event types.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="EventTypeCollection"/>.</returns>
        public EventTypeCollection GetEventTypes(Args args)
        {
            return new EventTypeCollection(this, args);
        }

        /// <summary>
        /// Returns a collection of alert groups that have been 
        /// fired by the service.
        /// </summary>
        /// <returns>A <see cref="FiredAlertsGroupCollection"/>.</returns>
        public FiredAlertsGroupCollection GetFiredAlertGroups()
        {
            return new FiredAlertsGroupCollection(this);
        }

        /// <summary>
        /// Returns a collection of alert groups that have been 
        /// fired by the service.
        /// </summary>
        /// <param name="args">Optional arguments, such as "count" and 
        /// "offset" for pagination.</param>
        /// <returns>A <see cref="FiredAlertsGroupCollection"/>.</returns>
        public FiredAlertsGroupCollection GetFiredAlerts(Args args)
        {
            return new FiredAlertsGroupCollection(this, args);
        }

        /// <summary>
        /// Returns a collection of Splunk indexes.
        /// </summary>
        /// <returns>An <see cref="Index"/> collection.</returns>
        public EntityCollection<Index> GetIndexes() 
        {
            return new 
                EntityCollection<Index>(this, "data/indexes", typeof(Index));
        }

        /// <summary>
        /// Returns a collection of Splunk indexes.
        /// </summary>
        /// <param name="args">The optional arguments.</param>
        /// <returns>An <see cref="Index"/> collection.</returns>
        public EntityCollection<Index> GetIndexes(Args args) 
        {
            return new EntityCollection<Index>(
                this, "data/indexes", args, typeof(Index));
        }

        /// <summary>
        /// Returns information about the Splunk service.
        /// </summary>
        /// <returns><see cref="ServiceInfo"/> about the Splunk
 		/// service.</returns>
        public ServiceInfo GetInfo() 
        {
            return new ServiceInfo(this);
        }

        /// <summary>
        /// Returns a collection of configured inputs.
        /// </summary>
        /// <returns>An <see cref="InputCollection"/>.</returns>
        public InputCollection GetInputs() 
        {
            return new InputCollection(this);
        }

        /// <summary>
        /// Returns a collection of configured inputs.
        /// </summary>
        /// <param name="args">Optional arguments.</param>
        /// <returns>An <see cref="InputCollection"/>.</returns>
        public InputCollection GetInputs(Args args) 
        {
            return new InputCollection(this, args);
        }

        /// <summary>
        /// Returns a collection of current search jobs.
        /// </summary>
        /// <returns>A <see cref="JobCollection"/>.</returns>
        public JobCollection GetJobs() 
        {
            return new JobCollection(this);
        }

        /// <summary>
        /// Returns a collection of current search jobs.
        /// </summary>
        /// <param name="args">The variable arguments.</param>
        /// <returns>A <see cref="JobCollection"/>.</returns>
        public JobCollection GetJobs(Args args) 
        {
            return new JobCollection(this, args);
        }

        /// <summary>
        /// Returns a collection of service logging categories and their 
		/// status.
        /// </summary>
        /// <returns>A collection of 
		/// <see cref="Logger">Loggers</see>.</returns>
        public EntityCollection<Logger> GetLoggers()
        {
            return new EntityCollection<Logger>(
                this, "server/logger", typeof(Logger));
        }

        /// <summary>
        /// Returns a collection of service logging categories and their status.
        /// </summary>
        /// <param name="args">Optional arguments, such as "count" and "offset"
        /// for pagination.</param>
        /// <returns>A collection of 
		/// <see cref="Logger">Loggers</see>.</returns>
        public EntityCollection<Logger> GetLoggers(Args args) 
        {
            return new EntityCollection<Logger>(
                this, "server/logger", args, typeof(Logger));
        }

        /// <summary>
        /// Returns the collection of messages.
        /// </summary>
        /// <returns>A <see cref="MessageCollection"/>.</returns>
        public MessageCollection GetMessages() 
        {
            return new MessageCollection(this);
        }

        /// <summary>
        /// Returns the collection of messages.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="MessageCollection"/>.</returns>
        public MessageCollection GetMessages(Args args) 
        {
            return new MessageCollection(this, args);
        }

        /// <summary>
        /// Returns a collection of credentials. 
        /// </summary>
        /// <returns>A <see cref="CredentialCollection"/>.</returns>
		/// <remarks>
		/// This collection is used for managing secure credentials.
		/// </remarks>
        public CredentialCollection GetCredentials() 
        {
            return new CredentialCollection(this);
        }

        /// <summary>
        /// Returns a collection of credentials.
        /// </summary>
        /// <param name="args">Optional arguments, such as "count" and "offset"
        /// for pagination.</param>
        /// <returns>A <see cref="CredentialCollection"/>.</returns>
		/// <remarks>
		/// This collection is used for managing secure credentials.
		/// </remarks>
        public CredentialCollection GetPasswords(Args args) 
        {
            return new CredentialCollection(this, args);
        }

        /// <summary>
        /// Returns the receiver object for this Splunk service.
        /// </summary>
        /// <returns>A <see cref="Receiver"/>.</returns>
        public Receiver GetReceiver() 
        {
            return new Receiver(this);
        }

        /// <summary>
        /// Returns a collection of Splunk user roles.
        /// </summary>
        /// <returns>A collection of Splunk 
		/// <see cref="Role">Roles</see>.</returns>
        public EntityCollection<Role> GetRoles() 
        {
            return new EntityCollection<Role>(
                this, "authentication/roles", typeof(Role));
        }

        /// <summary>
        /// Returns a collection of Splunk user roles.
        /// </summary>
        /// <param name="args">Optional parameters.</param>
        /// <returns>A collection of Splunk 
		/// <see cref="Role">Roles</see>.</returns>
        public EntityCollection<Role> GetRoles(Args args) 
        {
            return new EntityCollection<Role>(
                this, "authentication/roles", args, typeof(Role));
        }

        /// <summary>
        /// Returns a collection of saved searches.
        /// </summary>
        /// <returns>A <see cref="SavedSearchCollection"/>.</returns>
        public SavedSearchCollection GetSavedSearches() 
        {
            return new SavedSearchCollection(this);
        }

        /// <summary>
        /// Returns a collection of saved searches.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="SavedSearchCollection"/>.</returns>
        public SavedSearchCollection GetSavedSearches(Args args) 
        {
            return new SavedSearchCollection(this, args);
        }

        /// <summary>
        /// Returns service configuration information for an instance of 
        /// Splunk.
        /// </summary>
        /// <returns>This Splunk instance's <see cref="Settings"/>.</returns>
        public Settings GetSettings()
        {
            return new Settings(this);
        }

        /// <summary>
        /// Returns a collection of in-progress oneshot uploads.
        /// </summary>
        /// <returns>A collection of oneshot 
		/// <see cref="Upload">Uploads</see>.</returns>
        public EntityCollection<Upload> GetUploads()
        {
            return new EntityCollection<Upload>(
                this, "data/inputs/oneshot", typeof(Upload));
        }

        /// <summary>
        /// Returns a collection of in-progress oneshot uploads.
        /// </summary>
        /// <param name="splunkNamespace">The specific namespace.</param>
        /// <returns>A collection of oneshot 
		/// <see cref="Upload">Uploads</see>.</returns>
        public EntityCollection<Upload> GetUploads(Args splunkNamespace) 
        {
            return new EntityCollection<Upload>(
                this, "data/inputs/oneshot", splunkNamespace, typeof(Upload));
        }

        /// <summary>
        /// Returns a collection of Splunk users.
        /// </summary>
        /// <returns>A <see cref="UserCollection"/>.</returns>
        public UserCollection GetUsers() 
        {
           return new UserCollection(this);
        }

        /// <summary>
        /// Returns a collection of Splunk users.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>A <see cref="UserCollection"/>.</returns>
        public UserCollection GetUsers(Args args) 
        {
            return new UserCollection(this, args);
        }

        /// <summary>
        /// Authenticates the <see cref="Service"/> instance with a username 
		/// and password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The <see cref="Service"/>.</returns>
        public Service Login(string username, string password) 
        {
            this.Username = username;
            this.Password = password;

            Args args = new Args();
            args.AlternateAdd("username", username);
            args.AlternateAdd("password", password);

            ResponseMessage response = Post("/services/auth/login", args);
            StreamReader streamReader = new StreamReader(response.Content);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(streamReader.ReadToEnd());
            string sessionKey = doc
                .SelectSingleNode("/response/sessionKey")
                .InnerText;
            this.Token = "Splunk " + sessionKey;

            this.Version = this.GetInfo().Version;
            if (!this.Version.Contains("."))
            {
                // internal build
                this.Version = "6.0";
            }
            if (this.VersionCompare("4.3") >= 0)
            {
                this.PasswordEndPoint = "storage/passwords";
            }

            return this;
        }

        /// <summary>
        /// Logs out of the service. 
        /// </summary>
        /// <returns>The <see cref="Service"/>.</returns>
        public Service Logout() 
        {
            this.Token = null;
            return this;
        }

        /// <summary>
        /// Creates a oneshot synchronous search.
        /// </summary>
        /// <param name="query">The search string.</param>
        /// <returns>An I/O <see cref="Stream"/>.</returns>
        public Stream Oneshot(string query) 
        {
           return this.Oneshot(query, null);
        }

        /// <summary>
        /// Creates a oneshot synchronous search using search arguments.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="inputArgs">The input arguments.</param>
        /// <returns>An I/O <see cref="Stream"/>.</returns>
        public Stream Oneshot(string query, Args inputArgs) 
        {
            inputArgs = (inputArgs == null) ? Args.Create() : inputArgs; 
            inputArgs = Args.Create(inputArgs);
            inputArgs.Add("search", query);
            inputArgs.Add("exec_mode", "oneshot");
            ResponseMessage response = this.Post("search/jobs", inputArgs);
            return response.Content;
        }

        /// <summary>
        /// Opens a socket to the host and specified port.
        /// </summary>
        /// <param name="port">The port on the server to which to 
        /// connect.</param>
        /// <returns>A connected <see cref="Socket"/>.</returns>
        public Socket Open(int port) 
        {
            Socket socket = new Socket(
               AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(this.Host, port);
            return socket;
        }

        /// <summary>
        /// Parses a search query and returns a semantic map for the search in 
        /// JSON format.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A parse <see cref="ResponseMessage"/>.</returns>
        public ResponseMessage Parse(string query) 
        {
            return this.Parse(query, null);
        }

        /// <summary>
        /// Parses a search query with additional arguments and returns a 
        /// semantic map for the search in JSON format.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <param name="args">The arguments.</param>
        /// <returns>A parse <see cref="ResponseMessage"/>.</returns>
        public ResponseMessage Parse(string query, Args args) 
        {
            args = Args.Create(args);
            args.Add("q", query);
            return this.Get("search/parser", args);
        }

        /// <summary>
        /// Issues a restart request to the service. 
        /// </summary>
        /// <returns>A <see cref="ResponseMessage"/>.</returns>
        public ResponseMessage Restart() 
        {
            return this.Get("server/control/restart");
        }

        /// <summary>
        /// Creates a simplified synchronous search using search arguments. 
        /// </summary>
        /// <param name="query">The search string.</param>
        /// <returns>The <see cref="Stream"/> handle of the search.</returns>
		/// <remarks>
		/// Use this method for simple searches. For output control arguments, 
        /// use the <see cref="Search(string,Args,Args)"/> method.
		/// </remarks>
        public Stream Search(string query) 
        {
           return this.Search(query, null, null);
        }

        /// <summary>
        /// Creates a simplified synchronous search using search arguments. 
        /// </summary>
        /// <param name="query">The search string.</param>
        /// <param name="inputArgs">The variable arguments.</param>
        /// <returns>The <see cref="Stream"/> handle of the search.</returns>
		/// <remarks>
		/// Use this method for simple searches. For output control arguments, 
        /// use the <see cref="Search(string,Args,Args)"/> method.
		/// </remarks>
        public Stream Search(string query, Args inputArgs) 
        {
            return this.Search(query, inputArgs, null);
        }

        /// <summary>
        /// Creates a simplified synchronous search using search arguments. 
        /// </summary>
        /// <param name="query">The search string.</param>
        /// <param name="inputArgs">The variable arguments.</param>
        /// <param name="outputArgs">The output arguments.</param>
        /// <returns>The <see cref="Stream"/> handle of the search.</returns>
		/// <remarks>
		/// Use this method for simple searches. 
		/// </remarks>
        public Stream Search(string query, Args inputArgs, Args outputArgs) 
        {
            inputArgs = Args.Create(inputArgs);
            inputArgs.AlternateAdd("search", query);
            // always block until results are ready.
            inputArgs.AlternateAdd("exec_mode", "blocking");
            Job job = this.GetJobs().Create(query, inputArgs);
            return job.Results(outputArgs);
        }

        /// <summary>
        /// Overloaded. Issues an HTTP request against the service using a 
        /// request path and message. 
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="request">The request message.</param>
        /// <returns>A <see cref="ResponseMessage"/>.</returns>
		/// <remarks>
        /// This method overrides the base 
		/// <see cref="HttpService"/>.<see cref="HttpService.Send"/> 
		/// method and applies the Splunk authorization header, which is 
		/// required for authenticated interactions with the Splunk service.
		/// </remarks>
        public override 
            ResponseMessage Send(string path, RequestMessage request) 
        {
            if (this.Token != null) 
            {
                request.Header.Add("Authorization", this.Token);
            }
            return base.Send(this.Fullpath(path), request);
        }

        /// <summary>
        /// Compares the current version versus a desired version. 
        /// </summary>
        /// <param name="right">The desired version.</param>
        /// <returns><para>One of the following values:
 		/// <list>
		/// <item>"-1" indicates the current version is less than the desired
		/// version.</item>
		/// <item>"0" indicates the versions are equal.</item>
		/// <item>"1" indicates the current version is greater than the 
		/// desired version.</item>
		/// </list></para></returns>
        public int VersionCompare(string right) 
        {
            // Short cut for equality.
            if (this.Version.Equals(right)) 
            {
                return 0;
            }

            // If not the same, break down into individual digits for 
            // comparison.
            string[] leftDigits = this.Version.Split('.');
            string[] rightDigits = right.Split('.');

            for (int i = 0; i < leftDigits.Length; i++) 
            {
                // No more right side, left side is bigger.
                if (i == rightDigits.Length) 
                {
                    return 1;
                }
                // Left side smaller?
                if (Convert.ToInt32(leftDigits[i]) < 
                    Convert.ToInt32(rightDigits[i])) 
                {
                    return -1;
                }
                // Left side bigger?
                if (Convert.ToInt32(leftDigits[i]) > 
                    Convert.ToInt32(rightDigits[i])) 
                {
                    return 1;
                }
            }

            // We got to the end of the left side, and not equal, right side
            // must be larger by having more digits.
            return -1;
        }
    }
}
