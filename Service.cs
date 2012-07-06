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
    using System.Net.Sockets;
    using System.Xml;
    
    /// <summary>
    /// The Service class represent a Splunk service instance at a given
    /// address (host:port) accessed using http or https protocol scheme.
    /// <para>
    /// A Service instance also captures an optional namespace contect 
    /// consisting of an opriont owner name (or "-" wildcard) and optional app
    /// name (or "-" wildcard).
    /// </para>
    /// To access the Service members, the Service instance must be authenticated
    /// by presenting credentials using the the login method, or by contructing
    /// the Service instance using the connect method.
    /// </summary>
    public class Service : HttpService 
    {
        /// <summary>
        /// The default host name, which is used when a host name is not provided.
        /// </summary>
        private static string defaultHost = "localhost";

        /// <summary>
        /// The default port number, which is used when a port number is not
        /// provided.
        /// </summary>
        private static int defaultPort = 8089;

        /// <summary>
        /// The default scheme, which is used when a scheme is not provided.
        /// </summary>
        private static string defaultScheme = "https";

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class
        /// </summary>
        /// <param name="host">The hostname</param>
        public Service(string host) 
            : base(host) 
        {
            this.InitProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class,
        /// with host and port.
        /// </summary>
        /// <param name="host">The hostname</param>
        /// <param name="port">The port</param>
        public Service(string host, int port) 
            : base(host, port) 
        {
            this.InitProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class,
        /// with host, port and scheme.
        /// </summary>
        /// <param name="host">The hostname</param>
        /// <param name="port">The port</param>
        /// <param name="scheme">The scheme, http or https</param>
        public Service(string host, int port, string scheme)
            : base(host, port, scheme) 
        {
            this.InitProperties();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class,
        /// with a ServiceArgs argument list.
        /// </summary>
        /// <param name="args">The service arguments</param>
        public Service(ServiceArgs args)
            : base() 
        {
            this.InitProperties();
            this.App = args.App;
            this.Host = args.Host == null ? defaultHost : args.Host;
            this.Owner = args.Owner;
            this.Port = args.Port == 0 ? defaultPort : args.Port;
            this.Scheme = args.Scheme == null ? defaultScheme : args.Scheme;
            this.Token = args.Token;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class,
        /// with a general argument list.
        /// </summary>
        /// <param name="args">The service args</param>
        public Service(Dictionary<string, object> args) 
            : base() 
        {
            this.InitProperties();
            this.App = Args.Get(args, "app", null);
            this.Host = Args.Get(args, "host", defaultHost);
            this.Owner = Args.Get(args, "owner", null);
            this.Port = args.ContainsKey("port") ? Convert.ToInt32(args["port"]) : defaultPort;
            this.Scheme = Args.Get(args, "scheme", defaultScheme);
            this.Token = Args.Get(args, "token", null);
        }

        /// <summary>
        /// Gets or sets the current app context
        /// </summary>
        private string App 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current owner context. A of "nobody" means that all users
        /// have access to the resource.
        /// </summary>
        private string Owner 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the password, which is used to authenticate the Splunk instance.
        /// </summary>
        private string Password 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the default password endpoint, can change over Splunk versions.
        /// </summary>
        public string PasswordEndPoint 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the default simple receiver endpoint.
        /// </summary>
        public string SimpleReceiverEndPoint 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the current session (authorization) token
        /// </summary>
        public string Token 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the Splunk account username, which is used to authenticate the Splunk
        /// instance.
        /// </summary>
        private string Username 
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the version of this Splunk instance, once logged in.
        /// </summary>
        public string Version 
        {
            get; set;
        }

        /// <summary>
        /// Initialize all properties to their default values
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
        /// Establishes a connection to a Splunk service using a map of arguments. 
        /// This member creates a new Service instance and authenticates 
        /// the session using credentials passed in from the args dictionary.
        /// </summary>
        /// <param name="args">The connection arguments</param>
        /// <returns>The serfvice instance</returns>
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
        /// Runs a search using the search/jobs/export endpoint which streams
        /// results back via a Stream.
        /// </summary>
        /// <param name="search">The search query string</param>
        /// <returns>The results stream</returns>
        public Stream Export(string search) 
        {
            return this.Export(search, null);
        }

        /// <summary>
        /// Runs a search using the search/jobs/export endpoint which streams
        /// results back via a Stream. 
        /// </summary>
        /// <param name="search">The search query string</param>
        /// <param name="args">The search arguments</param>
        /// <returns>The results stream</returns>
        public Stream Export(string search, Args args) 
        {
            args = Args.Create(args).AlternateAdd("search", search);
            ResponseMessage response = Get("search/jobs/export", args);
            return response.Content;
        }

        /// <summary>
        /// Ensures that the given path is fully qualified, prepending a path
        /// prefix if necessary. The path prefix is constructed using the current 
        /// owner and app context when available.
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The fully qualified URL</returns>
        public string Fullpath(string path) 
        {
            return this.Fullpath(path, null);
        }

        /// <summary>
        /// Ensures that the given path is fully qualified, prepending a path
        /// prefix if necessary. The path prefix is constructed using the current 
        /// owner and app context when available.
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="splunkNamespace">The Splunk namespace</param>
        /// <returns>The fully qualified URL</returns>
        public string Fullpath(string path, Args splunkNamespace) 
        {
            // if already fully qualified (i.e. root begins with /) then return
            // the already qualified path.
            if (path.StartsWith("/")) 
            {
                return path;
            }

            // if no namespace at all, and no service instance of app, and no
            // sharing, return base service endpoint + path.
            if (splunkNamespace == null && this.App == null) 
            {
                return "/services/" + path;
            }

            // base namespace values
            string localApp = this.App;
            string localOwner = this.Owner;
            string localSharing = string.Empty;

            // override with invocation namespace if set.
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

            // sharing, if set calls for special mapping, override here.
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

        /**
         * Returns the collection of applications.
         *
         * @return The application collection.
        public EntityCollection<Application> GetApplications() {
            return new EntityCollection<Application>(
                this, "/services/apps/local", Application.class);
        }
         */

        ///**
        // * Returns the collection of configurations.
        // *
        // * @return The configurations collection.
        // */
        //public ConfCollection GetConfs() {
        //    return new ConfCollection(this);
        //}

        ///**
        // * Returns the collection of configurations.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return The configurations collection.
        // */
        //public ConfCollection GetConfs(Args args) {
        //    return new ConfCollection(this, args);
        //}

        ///**
        // * Returns an array of system capabilities.
        // *
        // * @return An array of capabilities.
        // */
        //public string[] GetCapabilities() {
        //    Entity caps = new Entity(this, "authorization/capabilities");
        //    return caps.getStringArray("capabilities");
        //}

        ///**
        // * Returns the configuration and status of a deployment client.
        // *
        // * @return The configuration and status.
        // */
        //public DeploymentClient GetDeploymentClient() {
        //    return new DeploymentClient(this);
        //}

        ///**
        // * Returns the configuration of all deployment servers.
        // *
        // * @return The configuration of deployment servers.
        // */
        //public EntityCollection<DeploymentServer> GetDeploymentServers() {
        //    return new EntityCollection<DeploymentServer>(
        //        this, "deployment/server", DeploymentServer.class);
        //}

        ///**
        // * Returns the configuration of all deployment servers.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return The configuration of deployment servers.
        // */
        //public EntityCollection<DeploymentServer> GetDeploymentServers(Args args) {
        //    return new EntityCollection<DeploymentServer>(
        //        this, "deployment/server", DeploymentServer.class, args);
        //}

        ///**
        // * Returns a collection of class configurations for a deployment server.
        // *
        // * @return A collection of class configurations.
        // */
        //public EntityCollection<DeploymentServerClass> GetDeploymentServerClasses(){
        //    return new EntityCollection<DeploymentServerClass>(
        //        this, "deployment/serverclass", DeploymentServerClass.class);
        //}

        ///**
        // * Returns a collection of class configurations for a deployment server.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of server class configurations.
        // */
        //public EntityCollection<DeploymentServerClass> GetDeploymentServerClasses(
        //        Args args){
        //    return new EntityCollection<DeploymentServerClass>(
        //        this, "deployment/serverclass", DeploymentServerClass.class, args);
        //}

        ///**
        // * Returns a collection of multi-tenant configurations.
        // *
        // * @return A collection of multi-tenant configurations.
        // */
        //public EntityCollection<DeploymentTenant> GetDeploymentTenants() {
        //    return new EntityCollection<DeploymentTenant>(
        //        this, "deployment/tenants", DeploymentTenant.class);
        //}

        ///**
        // * Returns a collection of multi-tenant configurations.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of multi-tenant configurations.
        // */
        //public EntityCollection<DeploymentTenant> GetDeploymentTenants(Args args) {
        //    return new EntityCollection<DeploymentTenant>(
        //        this, "deployment/tenants", DeploymentTenant.class, args);
        //}

        ///**
        // * Returns information about distributed search options.
        // *
        // * @return Distributed search information.
        // */
        //public DistributedConfiguration GetDistributedConfiguration() {
        //    return new DistributedConfiguration(this);
        //}

        ///**
        // * Returns a collection of distributed search peers. A <i>search peer</i>
        // * is a Splunk server to which another Splunk server distributes searches.
        // * The Splunk server where the search originates is referred to as the
        // * <i>search head</i>.
        // *
        // * @return A collection of search peers.
        // */
        //public EntityCollection<DistributedPeer> GetDistributedPeers() {
        //    return new EntityCollection<DistributedPeer>(
        //        this, "search/distributed/peers", DistributedPeer.class);
        //}

        ///**
        // * Returns a collection of distributed search peers. A <i>search peer</i>
        // * is a Splunk server to which another Splunk server distributes searches.
        // * The Splunk server where the search originates is referred to as the
        // * <i>search head</i>.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of search peers.
        // */
        //public EntityCollection<DistributedPeer> GetDistributedPeers(Args args) {
        //    return new EntityCollection<DistributedPeer>(
        //        this, "search/distributed/peers", DistributedPeer.class, args);
        //}

        ///**
        // * Returns a collection of saved event types.
        // *
        // * @return A collection of saved event types.
        // */
        //public EventTypeCollection GetEventTypes() {
        //    return new EventTypeCollection(this);
        //}

        ///**
        // * Returns a collection of saved event types.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of saved event types.
        // */
        //public EventTypeCollection GetEventTypes(Args args) {
        //    return new EventTypeCollection(this, args);
        //}

        ///**
        // * Returns a collection of alerts that have been fired by the service.
        // *
        // * @return A collection of fired alerts.
        // */
        //public FiredAlertGroupCollection GetFiredAlertGroups() {
        //    return new FiredAlertGroupCollection(this);
        //}

        ///**
        // * Returns a collection of alerts that have been fired by the service.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of fired alerts.
        // */
        //public FiredAlertGroupCollection GetFiredAlerts(Args args) {
        //    return new FiredAlertGroupCollection(this, args);
        //}

        ///**
        // * Returns a collection of Splunk indexes.
        // *
        // * @return A collection of indexes.
        // */
        //public EntityCollection<Index> GetIndexes() {
        //    return new EntityCollection<Index>(this, "data/indexes", Index.class);
        //}

        ///**
        // * Returns a collection of Splunk indexes.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of indexes.
        // */
        //public EntityCollection<Index> GetIndexes(Args args) {
        //    return new EntityCollection<Index>(
        //        this, "data/indexes", Index.class, args);
        //}

        /// <summary>
        /// Returns information about the Splunk service.
        /// </summary>
        /// <returns>The information about the splunk service.</returns>
        public ServiceInfo GetInfo() 
        {
            return new ServiceInfo(this);
        }

        ///**
        // * Returns a collection of configured inputs.
        // *
        // * @return A collection of inputs.
        // */
        //public InputCollection GetInputs() {
        //    return new InputCollection(this);
        //}

        ///**
        // * Returns a collection of configured inputs.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of inputs.
        // */
        //public InputCollection GetInputs(Args args) {
        //    return new InputCollection(this, args);
        //}

        /// <summary>
        /// Returns a collection of current search jobs.
        /// </summary>
        /// <returns>The JobCollection</returns>
        public JobCollection GetJobs() 
        {
            return new JobCollection(this);
        }

        /// <summary>
        /// Returns a collection of current search jobs.
        /// </summary>
        /// <param name="args">The variable arguments</param>
        /// <returns>The JobCollection</returns>
        public JobCollection GetJobs(Args args) 
        {
            return new JobCollection(this, args);
        }

        ///**
        // * Returns a collection of license group configurations.
        // *
        // * @return A collection of license group configurations.
        // */
        //public EntityCollection<LicenseGroup> GetLicenseGroups() {
        //    return new EntityCollection<LicenseGroup>(
        //        this, "licenser/groups", LicenseGroup.class);
        //}

        ///**
        // * Returns a collection of license group configurations.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of license group configurations.
        // */
        //public EntityCollection<LicenseGroup> GetLicenseGroups(Args args) {
        //    return new EntityCollection<LicenseGroup>(
        //        this, "licenser/groups", LicenseGroup.class, args);
        //}

        ///**
        // * Returns a collection of messages from the licenser.
        // *
        // * @return A collection of licenser messages.
        // */
        //public EntityCollection<LicenseMessage> GetLicenseMessages() {
        //    return new EntityCollection<LicenseMessage>(
        //        this, "licenser/messages", LicenseMessage.class);
        //}

        ///**
        // * Returns a collection of messages from the licenser.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of licenser messages.
        // */
        //public EntityCollection<LicenseMessage> GetLicenseMessages(Args args) {
        //    return new EntityCollection<LicenseMessage>(
        //        this, "licenser/messages", LicenseMessage.class, args);
        //}

        ///**
        // * Returns the current owner context for this {@code Service} instance. 
        // * A value of {@code "-"} indicates a wildcard, and a {@code null} value 
        // * indicates no owner context.
        // *
        // * @return The current owner context.
        // */
        //public string GetOwner() {
        //    return this.owner;
        //}

        ///**
        // * Returns a collection of licenser pool configurations.
        // *
        // * @return A collection of licenser pool configurations.
        // */
        //public LicensePoolCollection GetLicensePools() {
        //    return new LicensePoolCollection(this);
        //}

        ///**
        // * Returns a collection of licenser pool configurations.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of licenser pool configurations.
        // */
        //public LicensePoolCollection GetLicensePools(Args args) {
        //    return new LicensePoolCollection(this, args);
        //}

        ///**
        // * Returns a collection of slaves reporting to this license master.
        // *
        // * @return A collection of licenser slaves.
        // */
        //public EntityCollection<LicenseSlave> GetLicenseSlaves() {
        //    return new EntityCollection<LicenseSlave>(
        //        this, "licenser/slaves", LicenseSlave.class);
        //}

        ///**
        // * Returns a collection of slaves reporting to this license master.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of licenser slaves.
        // */
        //public EntityCollection<LicenseSlave> GetLicenseSlaves(Args args) {
        //    return new EntityCollection<LicenseSlave>(
        //        this, "licenser/slaves", LicenseSlave.class, args);
        //}

        ///**
        // * Returns a collection of license stack configurations.
        // *
        // * @return A collection of license stack configurations.
        // */
        //public EntityCollection<LicenseStack> GetLicenseStacks() {
        //    return new EntityCollection<LicenseStack>(
        //        this, "licenser/stacks", LicenseStack.class);
        //}

        ///**
        // * Returns a collection of license stack configurations.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of license stack configurations.
        // */
        //public EntityCollection<LicenseStack> GetLicenseStacks(Args args) {
        //    return new EntityCollection<LicenseStack>(
        //        this, "licenser/stacks", LicenseStack.class, args);
        //}

        ///**
        // * Returns a collection of licenses for this service.
        // *
        // * @return A collection of licenses.
        // */
        //public EntityCollection<License> GetLicenses() {
        //    return new EntityCollection<License>(
        //        this, "licenser/licenses", License.class);
        //}

        ///**
        // * Returns a collection of licenses for this service.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of licenses.
        // */
        //public EntityCollection<License> GetLicenses(Args args) {
        //    return new EntityCollection<License>(
        //        this, "licenser/licenses", License.class, args);
        //}

        ///**
        // * Returns a collection of service logging categories and their status.
        // *
        // * @return A collection of logging categories.
        // */
        //public EntityCollection<Logger> GetLoggers() {
        //    return new EntityCollection<Logger>(
        //        this, "server/logger", Logger.class);
        //}

        ///**
        // * Returns a collection of service logging categories and their status.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of logging categories.
        // */
        //public EntityCollection<Logger> GetLoggers(Args args) {
        //    return new EntityCollection<Logger>(
        //        this, "server/logger", Logger.class, args);
        //}

        /// <summary>
        /// Returns the collection of messages.
        /// </summary>
        /// <returns>The collection of messages</returns>
        public MessageCollection GetMessages() 
        {
            return new MessageCollection(this);
        }

        ///**
        // * Returns a collection of system messages.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of system messages.
        // */
        //public MessageCollection GetMessages(Args args) {
        //    return new MessageCollection(this, args);
        //}

        ///**
        // * Returns global TCP output properties.
        // *
        // * @return Global TCP output properties.
        // */
        //public OutputDefault GetOutputDefault() {
        //    return new OutputDefault(this);
        //}

        ///**
        // * Returns a collection of output group configurations.
        // *
        // * @return A collection of output group configurations.
        // */
        //public EntityCollection<OutputGroup> GetOutputGroups() {
        //    return new EntityCollection<OutputGroup>(
        //        this, "data/outputs/tcp/group", OutputGroup.class);
        //}

        ///**
        // * Returns a collection of output group configurations.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of output group configurations.
        // */
        //public EntityCollection<OutputGroup> GetOutputGroups(Args args) {
        //    return new EntityCollection<OutputGroup>(
        //        this, "data/outputs/tcp/group", OutputGroup.class, args);
        //}

        ///**
        // * Returns a collection of data-forwarding configurations.
        // *
        // * @return A collection of data-forwarding configurations.
        // */
        //public EntityCollection<OutputServer> GetOutputServers() {
        //    return new EntityCollection<OutputServer>(
        //        this, "data/outputs/tcp/server", OutputServer.class);
        //}

        ///**
        // * Returns a collection of data-forwarding configurations.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of data-forwarding configurations.
        // */
        //public EntityCollection<OutputServer> GetOutputServers(Args args) {
        //    return new EntityCollection<OutputServer>(
        //        this, "data/outputs/tcp/server", OutputServer.class, args);
        //}

        ///**
        // * Returns a collection of configurations for forwarding data in standard
        // * syslog format.
        // *
        // * @return A collection of syslog forwarders.
        // */
        //public EntityCollection<OutputSyslog> GetOutputSyslogs() {
        //    return new EntityCollection<OutputSyslog>(
        //        this, "data/outputs/tcp/syslog", OutputSyslog.class);
        //}

        ///**
        // * Returns a collection of configurations for forwarding data in standard
        // * syslog format.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of syslog forwarders.
        // */
        //public EntityCollection<OutputSyslog> GetOutputSyslogs(Args args) {
        //    return new EntityCollection<OutputSyslog>(
        //        this, "data/outputs/tcp/syslog", OutputSyslog.class, args);
        //}

        ///**
        // * Returns the current password that was used to authenticate the session.
        // *
        // * @return The current password.
        // */
        //public string GetPassword() {
        //    return this.password;
        //}

        ///**
        // * Returns a collection of passwords. This collection is used for managing
        // * secure credentials.
        // *
        // * @return A collection of passwords.
        // */
        //public PasswordCollection GetPasswords() {
        //    return new PasswordCollection(this);
        //}

        ///**
        // * Returns a collection of passwords. This collection is used for managing
        // * secure credentials.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of passwords.
        // */
        //public PasswordCollection GetPasswords(Args args) {
        //    return new PasswordCollection(this, args);
        //}

        /// <summary>
        /// Returns the receiver object for this Splunk service
        /// </summary>
        /// <returns>The receiver object</returns>
        public Receiver GetReceiver() 
        {
            return new Receiver(this);
        }

        /// <summary>
        /// Returns a collection of Splunk user roles.
        /// </summary>
        /// <returns>A collecgtion of Splunk Roles</returns>
        public EntityCollection<Role> GetRoles() 
        {
            return new EntityCollection<Role>(
                this, "authentication/roles", typeof(Role));
        }

        /// <summary>
        /// Returns a collection of Splunk user roles.
        /// </summary>
        /// <param name="args">Optional parameters</param>
        /// <returns>A collecgtion of Splunk Roles</returns>
        public EntityCollection<Role> GetRoles(Args args) 
        {
            return new EntityCollection<Role>(
                this, "authentication/roles", args, typeof(Role));
        }

        ///**
        // * Returns a collection of saved searches.
        // *
        // * @return A collection of saved searches.
        // */
        //public SavedSearchCollection GetSavedSearches() {
        //    return new SavedSearchCollection(this);
        //}

        ///**
        // * Returns a collection of saved searches.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of saved searches.
        // */
        //public SavedSearchCollection GetSavedSearches(Args args) {
        //    return new SavedSearchCollection(this, args);
        //}

        /// <summary>
        ///  Returns service configuration information for an instance of Splunk.
        /// </summary>
        /// <returns>This Splunk instances settings</returns>
        public Settings GetSettings()
        {
            return new Settings(this);
        }

        ///**
        // * Returns the current session token. Session tokens can be shared across
        // * multiple {@code Service} instances.
        // *
        // * @return The session token.
        // */
        //public string GetToken() {
        //    return this.token;
        //}

        ///**
        // * Returns a collection of in-progress oneshot uploads.
        // *
        // * @return A collection of in-progress oneshot uploads
        // */
        //public EntityCollection<Upload> GetUploads() {
        //    return new EntityCollection<Upload>(
        //        this, "data/inputs/oneshot", Upload.class);
        //}

        ///**
        // * Returns a collection of in-progress oneshot uploads.
        // *
        // * @param namespace This collection's namespace; there are no other
        // * optional arguments for this endpoint.
        // * @return A collection of in-progress oneshot uploads
        // */
        //public EntityCollection<Upload>
        //getUploads(Args splunkNamespace) {
        //    return new EntityCollection<Upload>(
        //        this, "data/inputs/oneshot", Upload.class, splunkNamespace);
        //}

        ///**
        // * Returns the Splunk account username that was used to authenticate the
        // * current session.
        // *
        // * @return The current username.
        // */
        //public string GetUsername() {
        //    return this.username;
        //}

        ///**
        // * Returns a collection of Splunk users.
        // *
        // * @return A collection of users.
        // */

        /// <summary>
        ///Returns a collection of Splunk users.
        /// </summary>
        /// <returns>The collection of Splunk users</returns>
        public UserCollection GetUsers() 
        {
           return new UserCollection(this);
        }

        ///**
        // * Returns a collection of Splunk users.
        // *
        // * @param args Optional arguments, such as "count" and "offset" for 
        // * pagination.
        // * @return A collection of users.
        // */
        //public UserCollection GetUsers(Args args) {
        //    return new UserCollection(this, args);
        //}

        /// <summary>
        /// Authenticates the Service instance with a username and password.
        /// </summary>
        /// <param name="username">The username</param>
        /// <param name="password">The password</param>
        /// <returns>The service</returns>
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
            string sessionKey = doc.SelectSingleNode("/response/sessionKey").InnerText;
            this.Token = "Splunk " + sessionKey;

            this.Version = this.GetInfo().Version;
            if (this.VersionCompare("4.3") >= 0)
            {
                this.PasswordEndPoint = "storage/passwords";
            }

            return this;
        }

        /// <summary>
        /// Logout of the service. 
        /// </summary>
        /// <returns>The service</returns>
        public Service Logout() 
        {
            this.Token = null;
            return this;
        }

        ///**
        // * Creates a oneshot synchronous search.
        // *
        // * @param query The search query.
        // * @return The search results.
        // */
        //public InputStream oneshot(string query) {
        //   return oneshot(query, null, null);
        //}

        ///**
        // * Creates a oneshot synchronous search using search arguments.
        // *
        // * @param query The search query.
        // * @param inputArgs The search arguments.
        // * @return The search results.
        // */
        //public InputStream oneshot(string query, Map inputArgs) {
        //    return oneshot(query, inputArgs, null);
        //}

        ///**
        // * Creates a oneshot synchronous search using search arguments.
        // *
        // * @param query The search query.
        // * @param inputArgs The search arguments.
        // * @param outputArgs The output qualifier arguments.
        // * @return The search results.
        // */
        //public InputStream oneshot(string query, Map inputArgs, Map outputArgs) {
        //    inputArgs = Args.create(inputArgs);
        //    inputArgs.put("search", query);
        //    inputArgs.put("exec_mode", "oneshot");
        //    ResponseMessage response = post("search/jobs", inputArgs);
        //    return response.getContent();
        //}

        /// <summary>
        /// Opens a socket to the host and specified port.
        /// </summary>
        /// <param name="port">The port on the server to connect to</param>
        /// <returns>The connected socket</returns>
        public Socket Open(int port) 
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(this.Host, port);
            return socket;
        }

        ///**
        // * Parses a search query and returns a semantic map for the search in JSON 
        // * format.
        // *
        // * @param query The search query.
        // * @return The parse response message.
        // */
        //public ResponseMessage parse(string query) {
        //    return parse(query, null);
        //}

        ///**
        // * Parses a search query with additional arguments and returns a semantic
        // * map for the search in JSON format.
        // *
        // * @param query The search query.
        // * @param args Additional parse arguments.
        // * @return The parse response message.
        // */
        //public ResponseMessage parse(string query, Map args) {
        //    args = Args.create(args).add("q", query);
        //    return Get("search/parser", args);
        //}

        /// <summary>
        /// Issues a restart request to the service. 
        /// </summary>
        /// <returns>The repsonse message</returns>
        public ResponseMessage Restart() 
        {
            return this.Get("server/control/restart");
        }

        /// <summary>
        /// Creates a simplified synchronous search using search arguments. Use this
        /// method for simple searches. For output control arguments, use overload
        /// with outputArgs.
        /// </summary>
        /// <param name="query">The search string</param>
        /// <returns>The Stream handle of the search</returns>
        public Stream Search(string query) 
        {
           return this.Search(query, null, null);
        }

        /// <summary>
        /// Creates a simplified synchronous search using search arguments. Use this
        /// method for simple searches. For output control arguments, use overload
        /// with outputArgs.
        /// </summary>
        /// <param name="query">The search string</param>
        /// <param name="inputArgs">The variable arguments</param>
        /// <returns>The Stream handle of the search</returns>
        public Stream Search(string query, Args inputArgs) 
        {
            return this.Search(query, inputArgs, null);
        }

        /// <summary>
        /// Creates a simplified synchronous search using search arguments. Use this
        /// method for simple searches.
        /// </summary>
        /// <param name="query">The search string</param>
        /// <param name="inputArgs">The variable arguments</param>
        /// <param name="outputArgs">The output arguments</param>
        /// <returns>The Stream handle of the search</returns>
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
        /// Issues an HTTP request against the service using a request path and 
        /// message. 
        /// This method overrides the base HttpService.send() method
        /// and applies the Splunk authorization header, which is required for 
        /// authenticated interactions with the Splunk service.
        /// </summary>
        /// <param name="path">The path</param>
        /// <param name="request">The request message</param>
        /// <returns>The ResponseMessage</returns>
        public override ResponseMessage Send(string path, RequestMessage request) 
        {
            if (this.Token != null) 
            {
                request.Header.Add("Authorization", this.Token);
            }
            return base.Send(this.Fullpath(path), request);
        }

        /// <summary>
        /// Compares the current version versus a desired version. If the current version is
        /// less than the desired -1 is returned. If they are equal 0 is returned. If the 
        /// current version is greater than the desired version 1 is returned.
        /// </summary>
        /// <param name="right">The desired version</param>
        /// <returns>A avlue of -1, 0, or 1.</returns>
        public int VersionCompare(string right) 
        {
            // short cut for equality.
            if (this.Version.Equals(right)) 
            {
                return 0;
            }

            // if not the same, break down into individual digits for comparison.
            string[] leftDigits = this.Version.Split('.');
            string[] rightDigits = right.Split('.');

            for (int i = 0; i < leftDigits.Length; i++) 
            {
                // No more right side, left side is bigger
                if (i == rightDigits.Length) 
                {
                    return 1;
                }
                // left side smaller>?
                if (Convert.ToInt32(leftDigits[i]) < Convert.ToInt32(leftDigits[1])) 
                {
                    return -1;
                }
                // left side bigger?
                if (Convert.ToInt32(leftDigits[i]) > Convert.ToInt32(leftDigits[1])) 
                {
                    return 1;
                }
            }
            // we got to the end of the left side, and not equal, right side
            // most be larger by having more digits.
            return -1;
        }
    }
}
