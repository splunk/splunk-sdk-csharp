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
    /// <summary>
    /// The <see cref="SavedSearchArgs"/> class extends <see cref="Args"/> for 
    /// <see cref="SavedSearch"/> creation setters.
    /// </summary>
    public class SavedSearchArgs : Args
    {
        /// <summary>
        /// The wildcard argument for any action. 
        /// </summary>
		/// <remarks>
		/// This property is write-only.
		/// </remarks>
        public string ActionWildcard
        {
            set
            {
                this["action.*"] = value;
            }
        }

        /// <summary>
        /// The password to use when authenticating with the SMTP server. 
        /// </summary>
        /// <remarks>
        /// <para>
		/// Normally this value will be set when editing the email settings.
		/// However, you can set a clear text password here and it will be
		/// encrypted on the next Splunk restart. 
		/// <para>
		/// This property is write-only.
		/// </para>
		/// <para>
		/// This property's default value is the empty string.
		/// </para>
        /// </remarks>
        public string ActionEmailAuthPassword
        {
            set
            {
                this["action.email.auth_password"] = value;
            }
        }

        /// <summary>
        /// The username to use when authenticating with the SMTP server.
        /// </summary>
		/// <remarks>
		/// <para>
        /// If this is empty string, no authentication is attempted. 
		/// Be aware that some SMTP servers reject unauthenticated emails.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// <para>
		/// This property's default value is the empty string.
		/// </para>
        /// </remarks>
        public string ActionEmailAuthUsername
        {
            set
            {
                this["action.email.auth_username"] = value;
            }
        }

        /// <summary>
        /// The blind carbon copy (BCC) email address to use.
        /// </summary>
		/// <remarks>
		/// This property is write-only.
		/// </remarks>
        public string ActionEmailBcc
        {
            set
            {
                this["action.email.bcc"] = value;
            }
        }

        /// <summary>
        /// The carbon copy (CC) email address to use.
        /// </summary>
		/// <remarks>
		/// This property is write-only.
		/// </remarks>
        public string ActionEmailCc
        {
            set
            {
                this["action.email.cc"] = value;
            }
        }

        /// <summary>
        /// The search command (or pipeline) that runs the action. 
        /// </summary>
        /// <remarks>
		/// <para>
        /// Generally, the search command is a template search pipeline that is 
        /// realized with values from the saved search. To reference saved 
        /// search field values, wrap them in a '$' symbol. For example, use 
        /// $name$ to reference the saved search name, or use $search$ to 
        /// reference the search query.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailCommand
        {
            set
            {
                this["action.email.command"] = value;   
            }
        }

        /// <summary>
        /// The format of text in the email. 
        /// </summary>
        /// <remarks>
		/// <para>
        /// This value also applies to any attachments formats. Valid values
        /// are: "plain", "html", "raw", and "csv".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailFormat
        {
            set
            {
                this["action.email.format"] = value;
            }
        }

        /// <summary>
        /// The email address from which the email action originates.
        /// </summary>
        /// <remarks>
		/// <para>
        /// The default is splunk@$LOCALHOST, or whatever value is set in 
        /// alert_actions.conf.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailFrom
        {
            set
            {
                this["action.email.from"] = value;
            }
        }

        /// <summary>
        /// The hostname used in the web link (URL) that is sent in email
 		/// actions. 
        /// </summary>
        /// <remarks>
        /// This property's value can be in either of two forms:
        /// <list type="bullet">
        ///   <item><i>hostname</i> (for example, "splunkserver", "splunkserver.example.com")</item>
		///   <item><i>protocol://hostname:port</i> (for example, "http://splunkserver:8000", "https://splunkserver.example.com:443")</item>
		/// </list>
		/// <para>
        /// When set to a simple hostname, the protocol and port that are 
        /// configured within Splunk are used to construct the base of the URL.
        /// When set to 'http://...', it is used verbatim. This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases in which the Splunk server
 		/// is not aware of how to construct a URL that can be externally
 		/// referenced, such as single sign on (SSO) environments, other
 		/// proxies, or when the Splunk server hostname is not generally
 		/// resolvable. 
		/// </para>
		/// <para>This property's default value is the current hostname
		/// provided by the operating system, or "localhost". 
		/// </para>
		/// <para>
		/// When this property is set to an empty string, the default
		/// behavior is used.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailHostname
        {
            set
            {
                this["action.email.hostname"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether the search results are 
        /// contained in the body of the email.
        /// </summary>
 		/// <remarks>
		/// Results can be either inline or attached to an email. For more 
		/// information, see <see cref="ActionEmailSendResults"/>.
        /// </remarks>
        public bool ActionEmailInline
        {
            set
            {
                this["action.email.inline"] = value;
            }
        }

        /// <summary>
        /// The address of the SMTP server that is used to send the emails, in
        /// the form &lt;<i>host</i>&gt;[:&lt;<i>port</i>&gt;]. 
        /// </summary>
        /// <remarks>
        /// <para>&lt;<i>host</i>&gt; can be either the hostname or the IP
        /// address.</para>
        /// <para>&lt;<i>port</i>&gt; is optional, and specifies the SMTP port
        /// that Splunk should connect to.</para>
        /// <para>If this property is not set, it defaults to the setting 
        /// defined in the
        /// <see href="http://docs.splunk.com/Documentation/Splunk/latest/Admin/Alertactionsconf">alert_actions.conf</see>
        /// file, or, if not set, to "$LOCALHOST:25".</para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailMailServer
        {
            set
            {
                this["action.email.mailserver"] = value;
            }
        }

        /// <summary>
        /// The maximum number of search results to send in email alerts.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is "100".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailMaxResults
        {
            set
            {
                this["action.email.maxresults"] = value;
            }
        }

        /// <summary>
        /// The maximum amount of time an email action takes before the action
        /// is canceled. 
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>This property's default value is "5m".</para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailMaxTime
        {
            set
            {
                this["action.email.maxtime"] = value;
            }
        }

        /// <summary>
        /// The name of the view to deliver.
        /// </summary>
        /// <remarks>
		/// <para>
		/// This property is only valid if the
		/// <see cref="ActionEmailSendPdf"/> property is enabled.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailPdfView
        {
            set
            {
                this["action.email.pdfview"] = value;
            }
        }

        /// <summary>
        /// A search string for preprocessing results before emailing them.
        /// </summary>
        /// <remarks>
        /// <para>Preprocessing usually involves filtering out unwanted
 		/// internal fields.</para>
        /// <para>
		/// This property's default value is an empty string, which indicates
		/// no preprocessing.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailPreprocessResults
        {
            set
            {
                this["action.email.preprocess_results"] = value;
            }
        }

        /// <summary>
        /// The paper orientation.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Valid values for this property are "portrait" and "landscape".
        /// </para>
        /// <para>
        /// This property's default value is "portrait".
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailReportPaperOrientation
        {
            set
            {
                this["action.email.reportPaperOrientation"] = value;
            }
        }

        /// <summary>
        /// The paper size for PDFs.
        /// </summary>
        /// <remarks>
        /// <para>Valid values for this property are "letter", "legal",
        /// "ledger", "a2", "a3", "a4", and "a5".</para>
        /// <para>
		/// This property's default value is "letter".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailReportPaperSize
        {
            set
            {
                this["action.email.reportPaperSize"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether the PDF server is 
        /// enabled.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is false.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionEmailReportServerEnabled
        {
            set
            {
                this["action.email.reportServerEnabled"] = value;
            }
        }

        /// <summary>
        /// The URL of the PDF report server, if one is set up and available on
        /// the network.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value for a locally installed report server 
	  	/// is "http://localhost:8091/".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailReportServerUrl
        {
            set
            {
                this["action.email.reportServerURL"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether to create and send the 
        /// results in PDF format.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is false.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionEmailSendPdf
        {
            set
            {
                this["action.email.sendpdf"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether search results are 
        /// attached to an email.
        /// </summary>
        /// <remarks>
		/// <para>
		/// Results can be either attached or inline. For more information,
		/// see <see cref="ActionEmailInline"/>.
		/// </para>
		/// <para>
        /// This property's default value is false.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionEmailSendResults
        {
            set
            {
                this["action.email.sendresults"] = value;
            }
        }

        /// <summary>
        /// The subject line of the email.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is "SplunkAlert-&lt;<i>savedsearchname</i>&gt;".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailSubject
        {
            set
            {
                this["action.email.subject"] = value;
            }
        }

        /// <summary>
        /// A comma- or semicolon-delimited list of email recipients receiving
        /// alerts.
        /// </summary>
        /// <remarks>
		/// <para>
		/// This value is required if this search is scheduled and the email
		/// alert action is enabled.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </summary>
        public string ActionEmailTo
        {
            set
            {
                this["action.email.to"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether running this email 
        /// action results in a trackable alert.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is false.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionEmailTrackAlert
        {
            set
            {
                this["action.email.track_alert"] = value;
            }
        }

        /// <summary>
        /// The minimum time-to-live (TTL), in seconds, of search artifacts if
        /// this email action is triggered. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the value is a number followed by "p", it is the number of
        /// scheduled search periods.
        /// </para>
        /// <para>
        /// This property's default value is "86400" (equal to 24 hours).
        /// </para>
        /// <para>
        /// If no actions are triggered, the artifacts will have their TTL
		/// determined by the "dispatch.ttl" attribute in savedsearches.conf.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionEmailTtl
        {
            set
            {
                this["action.email.ttl"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether to use secure socket layer
        /// (SSL) when communicating with the SMTP server.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is false.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionEmailUseSsl
        {
            set
            {
                this["action.email.ssl"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether to use transport layer 
        /// security (TLS) when communicating with the SMTP server.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is false.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionEmailUseTls
        {
            set
            {
                this["action.email.tls"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether columns should be sorted 
        /// from least wide to most wide, left to right.
        /// </summary>
        /// <remarks>
        /// <para>This property is only used when the
 		/// <see cref="ActionEmailFormat"/> property is set to "plain".</para>
        /// <para>
		/// This property's default value is true.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionEmailWidthSortColumns
        {
            set
            {
                this["action.email.width_sort_columns"] = value;
            }
        }

        /// <summary>
        /// The search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
		/// <para>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionPopulateLookupCommand
        {
            set
            {
                this["action.populate_lookup.command"] = value;
            }
        }

        /// <summary>
        /// The name of the lookup table or lookup path to populate.
        /// </summary>
        /// <remarks>
		/// This property is write-only.
        /// </remarks>
        public string ActionPopulateLookupDest
        {
            set
            {
                this["action.populate_lookup.dest"] = value;
            }
        }

        /// <summary>
        /// The host name used in the web link (URL) that is sent 
        /// in populate-lookup alerts.
        /// </summary>
        /// <remarks>
        /// This property's value can be in either of two forms:
        /// <list type="bullet">
        ///   <item><i>hostname</i> (for example, "splunkserver", "splunkserver.example.com")</item>
		///   <item><i>protocol://hostname:port</i> (for example, "http://splunkserver:8000", "https://splunkserver.example.com:443")</item>
		/// </list>
		/// <para>
        /// When set to a simple hostname, the protocol and port that are 
        /// configured within Splunk are used to construct the base of the URL.
        /// When set to 'http://...', it is used verbatim. This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases in which the Splunk server
 		/// is not aware of how to construct a URL that can be externally
 		/// referenced, such as single sign on (SSO) environments, other
 		/// proxies, or when the Splunk server hostname is not generally
 		/// resolvable. 
		/// </para>
		/// <para>This property's default value is the current hostname
		/// provided by the operating system, or "localhost". 
		/// </para>
		/// <para>
		/// When this property is set to an empty string, the default
		/// behavior is used.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionPopulateLookupHostname
        {
            set
            {
                this["action.populate_lookup.hostname"] = value;
            }
        }

        /// <summary>
        /// The maximum number of search results to send in populate-lookup
        /// alerts.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is "100".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public int ActionPopulateLookupMaxResults
        {
            set
            {
                this["action.populate_lookup.maxresults"] = value;
            }
        }

        /// <summary>
        /// The maximum amount of time an alert action takes before the action
        /// is canceled. 
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>
        /// This property's default value is "5m".
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// </remarks>
        public string ActionPopulateLookupMaxTime
        {
            set
            {
                this["action.populate_lookup.maxtime"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether running this populate-lookup
        /// action results in a trackable alert.
        /// </summary>
		/// <remarks>
		/// This property is write-only.
		/// </remarks>
        public bool ActionPopulateLookupTrackAlert
        {
            set
            {
                this["action.populate_lookup.track_alert"] = value;
            }
        }

        /// <summary>
        /// The minimum time-to-live (TTL), in seconds, of search artifacts if
        /// this populate-lookup action is triggered. 
        /// </summary>
        /// <remarks>
        /// <para>If the value is a number followed by "p", it is the number of
        /// scheduled search periods.</para>
        /// <para>This property's default value is "10p".</para>
        /// <para>If no actions are triggered, the artifacts will have their TTL
		/// determined by the "dispatch.ttl" attribute in savedsearches.conf.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionPopulateLookupTtl
        {
            set
            {
                this["action.populate_lookup.ttl"] = value;
            }
        }

        /// <summary>
        /// The search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
		/// <para>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// </remarks>
        public string ActionRssCommand
        {
            set
            {
                this["action.rss.command"] = value;
            }
        }

        /// <summary>
        /// The host name used in the web link (URL) that is sent 
        /// in RSS alerts.
        /// </summary>
        /// <remarks>
        /// This property's value can be in either of two forms:
        /// <list type="bullet">
        ///   <item><i>hostname</i> (for example, "splunkserver", 
        ///     "splunkserver.example.com")</item>
		///   <item><i>protocol://hostname:port</i> (for example, 
        ///     "http://splunkserver:8000", 
        ///     "https://splunkserver.example.com:443")</item>
		/// </list>
		/// <para>
        /// When set to a simple hostname, the protocol and port that are 
        /// configured within Splunk are used to construct the base of the URL.
        /// When set to 'http://...', it is used verbatim. This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases in which the Splunk server
 		/// is not aware of how to construct a URL that can be externally
 		/// referenced, such as single sign on (SSO) environments, other
 		/// proxies, or when the Splunk server hostname is not generally
 		/// resolvable. 
		/// </para>
		/// <para>This property's default value is the current hostname
		/// provided by the operating system, or "localhost". 
		/// </para>
		/// <para>
		/// When this property is set to an empty string, the default
		/// behavior is used.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionRssHostname
        {
            set
            {
                this["action.rss.hostname"] = value;
            }
        }

        /// <summary>
        /// The maximum number of search results to send in RSS alerts.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is "100".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// </remarks>
        public int ActionRssMaxResults
        {
            set
            {
                this["action.rss.maxresults"] = value;
            }
        }

        /// <summary>
        /// The maximum amount of time an RSS alert action takes 
        /// before the action is canceled.
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>
		/// This property's default value is "1m".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// </remarks>
        public string ActionRssMaxTime
        {
            set
            {
                this["action.rss.maxtime"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether running this RSS action 
        /// results in a trackable alert.
        /// </summary>
		/// <remarks>
		/// This property is write-only.
		/// </remarks>
        public bool ActionRssTrackAlert
        {
            set
            {
                this["action.rss.track_alert"] = value;
            }
        }

        /// <summary>
        /// The minimum time-to-live (TTL) of search artifacts if
        /// this RSS action is triggered. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the value is a number followed by "p", it is the number of
        /// scheduled search periods.
        /// </para>
        /// <para>
        /// If no actions are triggered, the artifacts will have their TTL
		/// determined by the "dispatch.ttl" attribute in savedsearches.conf.
		/// </para>
        /// <para>
        /// This property's default value is "86400" (equal to 24 hours).
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionRssTtl
        {
            set
            {
                this["action.rss.ttl"] = value;
            }
        }

        /// <summary>
        /// The search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
		/// <para>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// </remarks>
        public string ActionScriptCommand
        {
            set
            {
                this["action.script.command"] = value;
            }
        }

        /// <summary>
        /// The file name of the script to call.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This value is required if script action is enabled 
        /// <see cref="IsActionScript"/> is set to true).
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionScriptFilename
        {
            set
            {
                this["action.script.filename"] = value;
            }
        }

        /// <summary>
        /// The host name used in the web link (URL) that is sent 
        /// in script alerts.
        /// </summary>
        /// <remarks>
        /// This property's value can be in either of two forms:
        /// <list type="bullet">
        ///   <item><i>hostname</i> (for example, "splunkserver", 
        ///     "splunkserver.example.com")</item>
		///   <item><i>protocol://hostname:port</i> (for example, 
        ///     "http://splunkserver:8000", 
        ///     "https://splunkserver.example.com:443")</item>
		/// </list>
		/// <para>
        /// When set to a simple hostname, the protocol and port that are 
        /// configured within Splunk are used to construct the base of the URL.
        /// When set to 'http://...', it is used verbatim. This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases in which the Splunk server
 		/// is not aware of how to construct a URL that can be externally
 		/// referenced, such as single sign on (SSO) environments, other
 		/// proxies, or when the Splunk server hostname is not generally
 		/// resolvable. 
		/// </para>
		/// <para>This property's default value is the current hostname
		/// provided by the operating system, or "localhost". 
		/// </para>
		/// <para>
		/// When this property is set to an empty string, the default
		/// behavior is used.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionScriptHostname
        {
            set
            {
                this["action.script.hostname"] = value;
            }
        }

        /// <summary>
        /// The maximum number of search results to send in script alerts.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is "100".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public int ActionScriptMaxResults
        {
            set
            {
                this["action.script.maxresults"] = value;
            }
        }

        /// <summary>
        /// The maximum amount of time a script action takes before
        /// the action is canceled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>This property's default value is "5m".</para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// </remarks>
        public string ActionScriptMaxTime
        {
            set
            {
                this["action.script.maxtime"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether running this script 
        /// action results in a trackable alert.
        /// </summary>
		/// <remarks>
		/// This property is write-only.
		/// </remarks>
        public bool ActionScriptTrackAlert
        {
            set
            {
                this["action.script.track_alert"] = value;
            }
        }

        /// <summary>
        /// The minimum time-to-live (TTL) of search artifacts if
        /// this script action is triggered. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the value is a number followed by "p", it is the number of
        /// scheduled search periods.
        /// </para>
        /// <para>
        /// If no actions are triggered, the artifacts will have their TTL
		/// determined by the "dispatch.ttl" attribute in savedsearches.conf.
		/// </para>
        /// <para>
        /// This property's default value is "600" (equal to 10 minutes).
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionScriptTtl
        {
            set
            {
                this["action.script.ttl"] = value;
            }
        }

        /// <summary>
        /// The name of the summary index where the results of the 
        /// scheduled search are saved.
        /// </summary>
        /// <remarks>
        /// <para>This property's default value is "summary".</para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActtionSummaryIndexName
        {
            set
            {
                this["action.summary_index._name"] = value;
            }
        }

        /// <summary>
        /// The search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
		/// <para>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
		/// </remarks>
        public string ActionSummaryIndexCommand
        {
            set
            {
                this["action.summary_index.command"] = value;
            }
        }

        /// <summary>
        /// The host name used in the web link (URL) that is sent 
        /// in summary-index alerts.
        /// </summary>
        /// <remarks>
        /// This property's value can be in either of two forms:
        /// <list type="bullet">
        ///   <item><i>hostname</i> (for example, "splunkserver", 
        ///     "splunkserver.example.com")</item>
		///   <item><i>protocol://hostname:port</i> (for example, 
        ///     "http://splunkserver:8000", 
        ///     "https://splunkserver.example.com:443")</item>
		/// </list>
		/// <para>
        /// When set to a simple hostname, the protocol and port that are 
        /// configured within Splunk are used to construct the base of the URL.
        /// When set to 'http://...', it is used verbatim. This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases in which the Splunk server
 		/// is not aware of how to construct a URL that can be externally
 		/// referenced, such as single sign on (SSO) environments, other
 		/// proxies, or when the Splunk server hostname is not generally
 		/// resolvable. 
		/// </para>
		/// <para>This property's default value is the current hostname
		/// provided by the operating system, or "localhost". 
		/// </para>
		/// <para>
		/// When this property is set to an empty string, the default
		/// behavior is used.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionSummaryIndexHostname
        {
            set
            {
                this["action.summary_index.hostname"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether to run the summary 
        /// indexing action as part of the scheduled search.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property is only considered if the summary-index action is
        /// enabled and is always executed—that is, if <b>counttype =
        /// always</b>.
		/// </para>
		/// <para>
		/// This property's default value is true.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool ActionSummaryIndexInline
        {
            set
            {
                this["action.summary_index.inline"] = value;
            }
        }

        /// <summary>
        /// The maximum number of search results to send in summary-index
        /// alerts.
        /// </summary>
        /// <remarks>
		/// <para>
        /// This property's default value is "100".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public int ActionSummaryIndexMaxResults
        {
            set
            {
                this["action.summary_index.maxresults"] = value;
            }
        }

        /// <summary>
        /// The maximum amount of time a summary-index action takes before 
        /// the action is canceled. 
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>This property's default value is "5m".</para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionSummaryIndexMaxTime
        {
            set
            {
                this["action.summary_index.maxtime"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether running this 
        /// summary-index action results in a trackable alert.
        /// </summary>
		/// <remarks>
		/// This property is write-only.
        /// </remarks>
        public bool ActionSummaryIndexTrackAlert
        {
            set
            {
                this["action.summary_index.track_alert"] = value;
            }
        }

        /// <summary>
        /// The minimum time-to-live (TTL) of search artifacts if
        /// this summary-index action is triggered. 
        /// </summary>
        /// <remarks>
        /// <para>If the value is a number followed by "p", it is the number of
        /// scheduled search periods.</para>
        /// <para>This property's default value is "10p".</para>
        /// <para>If no actions are triggered, the artifacts will have their TTL
		/// determined by the "dispatch.ttl" attribute in savedsearches.conf.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string ActionSummaryIndexTtl
        {
            set
            {
                this["action.summary_index.ttl"] = value;
            }
        }

        /// <summary>
        /// A comma-separated list of actions to enable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value can be one of the following strings:
        /// <list type="bullet">
        /// <item>"email"</item>
        /// <item>"populate_lookup"</item> 
        /// <item>"rss"</item>
        /// <item>"script"</item>
        /// <item>"summary_index"</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string Actions
        {
            set
            {
                this["actions"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether Splunk applies the alert 
        /// actions to the entire result set (digest) or to each individual 
        /// search result (per result).
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public bool AlertDigestMode
        {
            set
            {
                this["alert.digest_mode"] = value;
            }
        }

        /// <summary>
        /// The amount of time to show the alert in the dashboard.
        /// The valid format is a number followed by a time unit ("s", "m", "h",
        /// or "d").
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>
        /// This property's default value is "24h".
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertExpires
        {
            set
            {
                this["alert.expires"] = value;
            }
        }

        /// <summary>
        /// The alert severity level.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The property's value can be one of the following integers:
        /// <list type="bullet">
        /// <item>"1" indicates DEBUG</item>
        /// <item>"2" indicates INFO</item>
        /// <item>"3" indicates WARN</item>
        /// <item>"4" indicates ERROR</item>
        /// <item>"5" indicates SEVERE</item>
        /// <item>"6" indicates FATAL</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property's default value is "3".
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public int AlertSeverity
        {
            set
            {
                this["alert.severity"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether alert suppression is 
        /// enabled for this search.
        /// </summary>
        /// <remarks>
        /// This property is write-only.
        /// </remarks>
        public bool AlertSuppress
        {
            set
            {
                this["alert.suppress"] = value;
            }
        }

        /// <summary>
        /// A comma-delimeted list of fields to use for alert 
        /// suppression when doing per-result alerting.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is required if suppression is turned on and
        /// per-result alerting is enabled.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertSuppressFields
        {
            set
            {
                this["alert.suppress.fields"] = value;
            }
        }

        /// <summary>
        /// The suppression period, which is only valid if
        /// <see cref="AlertSuppress"/> is enabled.
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertSuppressPeriod
        {
            set
            {
                this["alert.suppress.period"] = value;
            }
        }

        /// <summary>
        /// A keyword value that indicates how to track the actions
        /// triggered by this saved search. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value can be one of the following keywords:
        /// <list type="bullet">
        /// <item>"true" indicates enabled</item>
        /// <item>"false" indicates disabled</item>
        /// <item>"auto" indicates that tracking is based on the setting of
        /// each action</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertTrack
        {
            set
            {
                this["alert.track"] = value;
            }
        }

        /// <summary>
        /// The alert comparator for alert triggering. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value can be one of the following strings:
        /// <list type="bullet">
        /// <item>"greater than"</item>
        /// <item>"less than"</item> 
        /// <item>"equal to"</item> 
        /// <item>"rises by"</item>
        /// <item>"drops by"</item> 
        /// <item>"rises by perc"</item>
        /// <item>"drops by perc"</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property is used with <see cref="AlertThreshold"/> to 
        /// trigger alert actions.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertComparator
        {
            set
            {
                this["alert_comparator"] = value;
            }
        }

        /// <summary>
        /// A conditional search that is evaluated against the 
        /// results of the saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Alerts are triggered if the specified search yields a non-empty 
        /// search result list.
        /// </para>
        /// <para>
        /// This property's default value is an empty string.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertCondition
        {
            set
            {
                this["alert_condition"] = value;
            }
        }

        /// <summary>
        /// The value to compare to before triggering the alert action. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this value is expressed as a percentage, it indicates the
        /// value to use when <see cref="AlertComparator"/> is set to "rises by 
        /// perc" or "drops by perc."
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertThreshold
        {
            set
            {
                this["alert_threshold"] = value;
            }
        }

        /// <summary>
        /// A value that indicates what to base the alert on. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value can be one of the following strings:
        /// <list type="bullet">
        /// <item>"always"</item>
        /// <item>"custom"</item> 
        /// <item>"number of events"</item>
        /// <item>"number of hosts"</item>
        /// <item>"number of sources"</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property is overridden by the <see cref="AlertCondition"/> 
        /// property if specified.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string AlertType
        {
            set
            {
                this["alert_type"] = value;
            }
        }

        /*
         * args.* String Wildcard argument that accepts any saved search template argument, such as args.username=foobar when the search is search $username$.
         *   dispatch.* String Wildcard argument that accepts any dispatch related argument.
         *
         * wkcfix: not sure how to model with a setter.
         */

        /// <summary>
        /// The cron-style schedule for running this saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use standard cron notation to define your scheduled search 
        /// interval. The cron format can accept this type of notation: 
        /// "00,20,40 * * * *", which runs the search every hour at hh:00, 
        /// hh:20, and hh:40. Along the same lines, a cron of 03,23,43 * * * * 
        /// runs the search every hour at hh:03, hh:23, hh:43. 
        /// </para>
        /// <para>Splunk recommends that you schedule your searches so that
        /// they are staggered over time. This reduces system load. Running 
        /// all of them every 20 minutes (*/20) means they would all launch at 
        /// hh:00 (20, 40) and might slow your system every 20 minutes.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string CronSchedule
        {
            set
            {
                this["cron_schedule"] = value;
            }
        }

        /// <summary>
        /// A human-readable description of this saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is an empty string.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string Description
        {
            set
            {
                this["description"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether the saved search is disabled. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Disabled searches are not visible in Splunk Web.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// The maximum number of timeline buckets.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "0".
        /// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public int DispatchBuckets
        {
            set
            {
                this["dispatch.buckets"] = value;
            }
        }

        /// <summary>
        /// A time string that specifies the earliest time for this search.
        /// </summary>
        /// <remarks>
		/// <para>
 		/// This property's value can be a relative or absolute time. If it
 		/// is an absolute time, use the <see cref="DispatchTimeFormat"/> to
 		/// format the value.
		/// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string DipatchEarliestTime
        {
            set
            {
                this["dispatch.earliest_time"] = value;
            }
        }

        /// <summary>
        /// A time string that specifies the latest time for this search.
        /// </summary>
        /// <remarks>
		/// <para>
 		/// This property's value can be a relative or absolute time. If it
 		/// is an absolute time, use the <see cref="DispatchTimeFormat"/> to
 		/// format the value.
		/// </para>
        /// <para>
        /// This property is write-only.
        /// </para>
        /// </remarks>
        public string DipatchLatestTime
        {
            set
            {
                this["dispatch.latest_time"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether lookups are enabled for this 
        /// search.
        /// </summary>
        /// <remarks>
        /// <para>
		/// This property's default value is true.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool DispatchLookups
        {
            set
            {
                this["dispatch.lookups"] = value;
            }
        }

        /// <summary>
        /// The maximum number of results before finalizing the search. 
        /// </summary>
        /// <remarks>
        /// <para>
		/// This property's default value is "500000".
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public int DispatchMaxCount
        {
            set
            {
                this["dispatch.max_count"] = value;
            }
        }

        /// <summary>
        /// The maximum amount of time in seconds before finalizing the search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "0".
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string DispatchMaxTime
        {
            set
            {
                this["dispatch.max_time"] = value;
            }
        }

        /// <summary>
        /// An integer value that specifies how frequently Splunk runs the
        /// MapReduce reduce phase on accumulated map values.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "10".
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public int DispatchReduceFrequency
        {
            set
            {
                this["dispatch.reduce_freq"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether to backfill the 
        /// real-time window for this search. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is only valid for real-time searches.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool DispatchRealTimeBackfill
        {
            set
            {
                this["dispatch.rt_backfill"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether Splunk spawns a new 
        /// search process when running this saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Searches against indexes must run in a separate process.
        /// </para>
        /// <para>
        /// This property's default value is true.
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public bool DispatchSpawnSubprocess
        {
            set
            {
                this["dispatch.spawn_process"] = value;
            }
        }

        /// <summary>
        /// A time format string that defines the time format used to
        /// specify the earliest and latest times for this search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "%FT%T.%Q%:z".
        /// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string DispatchTimeFormat
        {
            set
            {
                this["dispatch.time_format"] = value;
            }
        }

        /// <summary>
        /// The time to live (TTL) for the artifacts of the scheduled 
        /// search (the time before the search job expires and artifacts are 
        /// still available), if no actions are triggered.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If an action is triggered, Splunk changes the TTL to that
        /// action's TTL. If multiple actions are triggered, Splunk applies
        /// the maximum TTL to the artifacts.
        /// </para>
        /// <para>If the value is a number followed by "p", it is the number of
        /// scheduled search periods.</para>
        /// <para>This property's default value is "2p".</para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string DispatchTtl
        {
            set
            {
                this["dispatch.ttl"] = value;
            }
        }

        /// <summary>
        /// The default UI view name (not label) in which to load the results.
        /// </summary>
        /// <remarks>
		/// <para>
        /// Access is dependent on the user having sufficient permissions.
		/// </para>
		/// <para>
		/// This property is write-only.
		/// </para>
        /// </remarks>
        public string DisplayView
        {
            set
            {
                this["displayview"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether this search is run on a 
        /// schedule.
        /// </summary>
        /// <remarks>
		/// This property is write-only.
        /// </remarks>
        public bool IsScheduled
        {
            set
            {
                this["is_scheduled"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether the search should be 
        /// visible in the saved search list.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// <para>
		/// This property is write-only.
        /// </para>
        /// </remarks>
        public bool IsVisible
        {
            set
            {
                this["is_visible"] = value;
            }
        }

        /// <summary>
        /// The maximum number of concurrent instances of this 
        /// search the scheduler is allowed to run.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "1".
        /// </para>
        /// <para>
		/// This property is write-only.
        /// </para>
        /// </remarks>
        public int MaxConcurrent
        {
            set
            {
                this["max_concurrent"] = value;
            }
        }

        /// <summary>
        /// A value that indicates how the scheduler computes the next run
        /// time of a scheduled search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's possible values are the following:
        /// <list type="bullet">
        /// <item>"0" indicates the scheduler bases its determination of the
        /// next scheduled search on the last search execution time. This is
        /// called continuous scheduling. The scheduler will never skip
        /// scheduled execution periods. However, the execution of the saved
        /// search might fall behind depending on the scheduler's load. Use
        /// continuous scheduling whenever you enable the summary index
        /// option. </item>
        /// <item>"1" indicates the scheduler is executing the searches running
        /// over the most recent time range, and therefore might skip some
        /// execution periods to keep up.</item>
        /// </list>
        /// </para>
        /// <para>
        /// This property's default value is "1".
        /// </para>
        /// <para>
		/// This property is write-only.
        /// </para>
        /// </remarks>
        public int RealtimeSchedule
        {
            set
            {
                this["realtime_schedule"] = value;
            }
        }

        /// <summary>
        /// A string value that specifies the app in which Splunk Web
        /// dispatches this search.
        /// </summary>
        /// <remarks>
		/// This property is write-only.
        /// </remarks>
        public string RequestUIDispatchApp
        {
            set
            {
                this["request.ui_dispatch_app"] = value;
            }
        }

        /// <summary>
        /// A string value that specifies the view in which Splunk Web
        /// displays this search.
        /// </summary>
        /// <remarks>
		/// This property is write-only.
        /// </remarks>
        public string RequestUIDispatchView
        {
            set
            {
                this["request.ui_dispatch_view"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether a real-time search 
        /// managed by the scheduler is restarted when a search peer becomes 
        /// available for this saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The peer can be a newly added peer or a peer that has been down
        /// and has become available.
        /// </para>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// <para>
		/// This property is write-only.
        /// </para>
        /// </remarks>
        public bool RestartOnSearchPeerAdd
        {
            set
            {
                this["restart_on_searchpeer_add"] = value;
            }
        }

        /// <summary>
        /// A Boolean value that indicates whether this search is run when 
        /// Splunk starts.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the search is not run on startup, it runs at the next scheduled time.
        /// </para>
        /// <para>
        /// Splunk recommends that you set this property to true for scheduled
        /// searches that populate lookup tables.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// <para>
		/// This property is write-only.
        /// </para>
        /// </remarks>
        public bool RunOnStartup
        {
            set
            {
                this["run_on_startup"] = value;
            }
        }

        /// <summary>
        /// The view state ID that is associated with the view 
        /// specified in the <see cref="DisplayView"/> property.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This ID corresponds to a stanza in the viewstates.conf 
		/// configuration file.
        /// </para>
        /// <para>
		/// This property is write-only.
        /// </para>
        /// </remarks>
        public string VSID
        {
            set
            {
                this["vsid"] = value;
            }
        }
    }
}