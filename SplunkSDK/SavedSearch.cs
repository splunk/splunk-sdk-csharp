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
    using System.Linq;
    using System.Text;

    /// <summary>
    /// The <see cref="SavedSearch"/> class represents a saved search.
    /// </summary>
    public class SavedSearch : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavedSearch"/> class.
        /// </summary>
        /// <param name="service">The connected service.</param>
        /// <param name="path">The path.</param>
        public SavedSearch(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the password to use when authenticating with the 
		/// SMTP server. 
        /// </summary>
        /// <remarks>
		/// This property's default value is the empty string.
        /// </remarks>
        public string ActionEmailAuthPassword
        {
            get
            {
                return this.GetString("action.email.auth_password", null);
            }

            set
            {
                this.SetCacheValue("action.email.auth_password", value);
            }
        }

        /// <summary>
        /// Gets or sets the username to use when authenticating with the SMTP 
		/// server.
        /// </summary>
        /// <remarks>
		/// This property's default value is the empty string.
        /// </remarks>
        public string ActionEmailAuthUsername
        {
            get
            {
                return this.GetString("action.email.auth_username", null);
            }

            set
            {
                this.SetCacheValue("action.email.auth_username", value);
            }
        }

        /// <summary>
        /// Gets or sets the blind carbon copy (BCC) email address 
		/// receiving alerts.
        /// </summary>
        public string ActionEmailBcc
        {
            get
            {
                return this.GetString("action.email.bcc", null);
            }

            set
            {
                this.SetCacheValue("action.email.bcc", value);
            }
        }

        /// <summary>
        /// Gets or sets the carbon copy (CC) email address receiving
 		/// alerts.
        /// </summary>
        public string ActionEmailCc
        {
            get
            {
                return this.GetString("action.email.cc", null);
            }

            set
            {
                this.SetCacheValue("action.email.cc", value);
            }
        }

        /// <summary>
        /// Gets or sets the search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
        /// Generally, the search command is a template search pipeline that is 
        /// realized with values from the saved search. To reference saved 
        /// search field values, wrap them in a '$' symbol. For example, use 
        /// $name$ to reference the saved search name, or use $search$ to 
        /// reference the search query.
        /// </remarks>
        public string ActionEmailCommand
        {
            get
            {
                return this.GetString("action.email.command", null);
            }

            set
            {
                this.SetCacheValue("action.email.command", value);
            }
        }

        /// <summary>
        /// Gets or sets the format of text in the email. 
        /// </summary>
        /// <remarks>
        /// This value also applies to any attachments formats. Valid values
        /// are: "plain", "html", "raw", and "csv".
        /// </remarks>
        public string ActionEmailFormat
        {
            get
            {
                return this.GetString("action.email.format", null);
            }

            set
            {
                this.SetCacheValue("action.email.format", value);
            }
        }

        /// <summary>
        /// Gets or sets the email address from which the email action originates.
        /// </summary>
        /// <remarks>
        /// The default is splunk@$LOCALHOST, or whatever value is set in 
        /// alert_actions.conf.
        /// </remarks>
        public string ActionEmailFrom
        {
            get
            {
                return this.GetString("action.email.from", null);
            }

            set
            {
                this.SetCacheValue("action.email.from", value);
            }
        }

        /// <summary>
        /// Gets or sets the host name used in the web link (URL) that is sent 
        /// in email alerts.
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
        /// </remarks>
        public string ActionEmailHostname
        {
            get
            {
                return this.GetString("action.email.hostname", null);
            }

            set
            {
                this.SetCacheValue("action.email.hostname", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether the search 
		/// results are contained in the body of the email.
        /// </summary>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ActionEmailInline
        {
            get
            {
                return this.GetBoolean("action.email.inline", false);
            }

            set
            {
                this.SetCacheValue("action.email.inline", value);
            }
        }

        /// <summary>
        /// Gets or sets the address of the SMTP server that is used to send
 		/// the emails, in the form &lt;<i>host</i>&gt;[:&lt;<i>port</i>&gt;]. 
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
        /// </remarks>
        public string ActionEmailMailServer
        {
            get
            {
                return this.GetString("action.email.mailserver", null);
            }

            set
            {
                this.SetCacheValue("action.email.mailserver", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of search results to send in email
 		/// alerts.
        /// </summary>
        /// <remarks>
        /// This property's default value is "100".
        /// </remarks>
        public int ActionEmailMaxResults
        {
            get
            {
                return this.GetInteger("action.email.maxresults", -1);
            }

            set
            {
                this.SetCacheValue("action.email.maxresults", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum amount of time an email action takes 
		/// before the action is canceled. 
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>This property's default value is "5m".</para>
        /// </remarks>
        public string ActionEmailMaxTime
        {
            get
            {
                return this.GetString("action.email.maxtime", null);
            }

            set
            {
                this.SetCacheValue("action.email.maxtime", value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the PDF view to deliver.
        /// </summary>
		/// <remarks>
		/// This property is only valid if the <see cref="ActionEmailSendPdf"/> 
        /// property is set to true.
		/// </remarks>
        public string ActionEmailPdfView
        {
            get
            {
                return this.GetString("action.email.pdfview", null);
            }

            set
            {
                this.SetCacheValue("action.email.pdfview", value);
            }
        }

        /// <summary>
        /// Gets or sets a search string for preprocessing results before
 		/// emailing them.
        /// </summary>
        /// <remarks>
        /// <para>Preprocessing usually involves filtering out unwanted
 		/// internal fields.</para>
        /// <para>This property's default value is an empty string, which
        /// indicates no preprocessing.</para>
        /// </remarks>
        public string ActionEmailPreProcessResults
        {
            get
            {
                return this.GetString("action.email.preprocess_results", null);
            }

            set
            {
                this.SetCacheValue("action.email.preprocess_results", value);
            }
        }

        /// <summary>
        /// Gets or sets a space-delimited list of character-identified
 		/// (CID) fonts for handling some Asian languages in integrated PDF
 		/// rendering.
        /// </summary>
        /// <remarks>
		/// <para>
		/// The fonts that can be listed include the following:
		/// <list>
		/// <item><b>gb</b>: Simplified Chinese</item>
		/// <item><b>cns</b>: Traditional Chinese</item>
		/// <item><b>jp</b>: Japanese</item>
		/// <item><b>kor</b>: Korean</item>
		/// </list>
		/// </para>
        /// <para>If multiple fonts provide a glyph for a given character code, the
        /// glyph from the first font specified in the list is used.</para> 
        /// <para>To skip loading any CID fonts, specify an empty string.</para>
        /// <para>This property's default value is "gb cns jp kor".</para>
        /// <para>This property was introduced in Splunk 5.0.</para>
        /// </remarks>
        public string ActionEmailReportCIDFontList
        {
            get
            {
                return this.GetString("action.email.reportCIDFontList", null);
            }

            set
            {
                this.SetCacheValue("action.email.reportCIDFontList", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether to include the
 		/// Splunk logo with the report. 
        /// </summary>
        /// <remarks>
        /// This property was introduced in Splunk 5.0. 
        /// </remarks>
        public bool ActionEmailReportIncludeSplunkLogo
        {
            get
            {
                return this.GetBoolean(
                    "action.email.reportIncludeSplunkLogo", false);
            }

            set
            {
                this.SetCacheValue(
                    "action.email.reportIncludeSplunkLogo", value);
            }
        }

        /// <summary>
        /// Gets or sets the paper orientation.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Valid values for this property are "portrait" and "landscape".
        /// </para>
        /// <para>
        /// This property's default value is "portrait".
        /// </para>
        /// </remarks>
        public string ActionEmailReportPaperOrientation
        {
            get
            {
                return this.GetString(
                    "action.email.reportPaperOrientation", null);
            }

            set
            {
                this.SetCacheValue(
                    "action.email.reportPaperOrientation", value);
            }
        }

        /// <summary>
        /// Gets or sets the paper size for PDFs.
        /// </summary>
        /// <remarks>
        /// <para>Valid values for this property are "letter", "legal",
        /// "ledger", "a2", "a3", "a4", and "a5".</para>
        /// <para>This property's default value is "letter".</para>
        /// </remarks>
        public string ActionEmailReportPaperSize
        {
            get
            {
                return this.GetString("action.email.reportPaperSize", null);
            }

            set
            {
                this.SetCacheValue("action.email.reportPaperSize", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether the PDF server
 		/// is enabled.
        /// </summary>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ActionEmailReportServerEnabled
        {
            get
            {
                return this.GetBoolean(
                    "action.email.reportServerEnabled", false);
            }

            set
            {
                this.SetCacheValue("action.email.reportServerEnabled", value);
            }
        }

        /// <summary>
        /// Gets or sets the URL of the PDF report server, if one is set up and
 		/// available on the network.
        /// </summary>
        /// <remarks>
        /// This property's default value for a locally installed report server 
	  	/// is "http://localhost:8091/".
        /// </remarks>
        public string ActionEmailReportServerUrl
        {
            get
            {
                return this.GetString("action.email.reportServerURL", null);
            }

            set
            {
                this.SetCacheValue("action.email.reportServerURL", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether to create and
 		/// send the results in PDF format.
        /// </summary>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ActionEmailSendPdf
        {
            get
            {
                return this.GetBoolean("action.email.sendpdf", false);
            }

            set
            {
                this.SetCacheValue("action.email.sendpdf", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether search results
 		/// are attached to an email.
        /// </summary>
		/// <para>
		/// Results can be either attached or inline. For more information,
		/// see <see cref="ActionEmailInline"/>.
		/// </para>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ActionEmailSendResults
        {
            get
            {
                return this.GetBoolean("action.email.sendresults", false);
            }

            set
            {
                this.SetCacheValue("action.email.sendresults", value);
            }
        }

        /// <summary>
        /// Gets or sets the subject line of the email.
        /// </summary>
        /// <remarks>
        /// This property's default value is 
		/// "SplunkAlert-&lt;<i>savedsearchname</i>&gt;".
        /// </remarks>
        public string ActionEmailSubject
        {
            get
            {
                return this.GetString("action.email.subject", null);
            }

            set
            {
                this.SetCacheValue("action.email.subject", value);
            }
        }

        /// <summary>
        /// Gets or sets a comma- or semicolon-delimited list of email 
		/// recipients receiving alerts.
        /// </summary>
        public string ActionEmailTo
        {
            get
            {
                return this.GetString("action.email.to", null);
            }

            set
            {
                this.SetCacheValue("action.email.to", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether running this
 		/// email action results in a trackable alert.
        /// </summary>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ActionEmailTrackAlert
        {
            get
            {
                return this.GetBoolean("action.email.track_alert", false);
            }

            set
            {
                this.SetCacheValue("action.email.track_alert", value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum time-to-live (TTL), in seconds, of search
 		/// artifacts if this email action is triggered. 
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
        /// </remarks>
        public string ActionEmailTtl
        {
            get
            {
                return this.GetString("action.email.ttl", null);
            }

            set
            {
                this.SetCacheValue("action.email.ttl", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether to use secure
 		/// socket layer (SSL) when communicating with the SMTP server.
        /// </summary>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ActionEmailUseSsl
        {
            get
            {
                return this.GetBoolean("action.email.use_ssl", false);
            }

            set
            {
                this.SetCacheValue("action.email.use_ssl", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether to use
 		/// transport layer security (TLS) when communicating with the SMTP
 		/// server.
        /// </summary>
        /// <remarks>
        /// This property's default value is false.
        /// </remarks>
        public bool ActionEmailUseTls
        {
            get
            {
                return this.GetBoolean("action.email.use_tls", false);
            }

            set
            {
                this.SetCacheValue("action.email.use_tls", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether columns should
 		/// be sorted from least wide to most wide, left to right.
        /// </summary>
        /// <remarks>
        /// <para>
		/// This property is only used when the <see cref="ActionEmailFormat"/>
        /// property is set to "plain".
		/// </para>
        /// <para>This property's default value is "true".</para>
        /// </remarks>
        public bool ActionEmailWidthSortColumns
        {
            get
            {
                return this.GetBoolean(
                    "action.email.width_sort_columns", false);
            }

            set
            {
                this.SetCacheValue("action.email.width_sort_columns", value);
            }
        }

        /// <summary>
        /// Gets or sets the search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
        /// </remarks>
        public string ActionPopulateLookupCommand
        {
            get
            {
                return this.GetString("action.populate_lookup.command", null);
            }

            set
            {
                this.SetCacheValue("action.populate_lookup.command", value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the lookup table or lookup path to
 		/// populate.
        /// </summary>
        public string ActionPopulateLookupDest
        {
            get
            {
                return this.GetString("action.populate_lookup.dest", null);
            }

            set
            {
                this.SetCacheValue("action.populate_lookup.dest", value);
            }
        }

        /// <summary>
        /// Gets or sets the host name used in the web link (URL) that is sent 
        /// in populate-lookup alerts.
        /// </summary>
        /// <remarks>
        /// This property's value can be in either of two forms:
        /// <list type="bullet">
        ///   <item><i>hostname</i> (for example, "splunkserver", "splunkserver.example.com")</item>
		///   <item><i>protocol://hostname:port</i> (for example, "http://splunkserver:8000", "https://splunkserver.example.com:443")</item>
		/// </list>
        /// </remarks>
        public string ActionPopulateLookupHostname
        {
            get
            {
                return this.GetString("action.populate_lookup.hostname", null);
            }

            set
            {
                this.SetCacheValue("action.populate_lookup.hostname", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of search results to send in
 		/// populate-lookup alerts.
        /// </summary>
        /// <remarks>
        /// This property's default value is "100".
        /// </remarks>
        public int ActionPopulateLookupMaxResults
        {
            get
            {
                return this.GetInteger("action.populate_lookup.maxresults", -1);
            }

            set
            {
                this.SetCacheValue("action.populate_lookup.maxresults", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum amount of time an alert action takes 
		/// before the action is canceled. 
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>
        /// This property's default value is "5m".
        /// </para>
		/// </remarks>
        public string ActionPopulateLookupMaxTime
        {
            get
            {
                return this.GetString("action.populate_lookup.maxtime", null);
            }

            set
            {
                this.SetCacheValue("action.populate_lookup.maxtime", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether running this
 		/// populate-lookup action results in a trackable alert.
        /// </summary>
        public bool ActionPopulateLookupTrackAlert
        {
            get
            {
                return this.GetBoolean(
                    "action.populate_lookup.track_alert", false);
            }

            set
            {
                this.SetCacheValue("action.populate_lookup.track_alert", value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum time-to-live (TTL), in seconds, of search
 		/// artifacts if this populate-lookup action is triggered. 
        /// </summary>
        /// <remarks>
        /// <para>If the value is a number followed by "p", it is the number of
        /// scheduled search periods.</para>
        /// <para>This property's default value is "10p".</para>
        /// <para>If no actions are triggered, the artifacts will have their TTL
		/// determined by the "dispatch.ttl" attribute in savedsearches.conf.
		/// </para>
        /// </remarks>
        public string ActionPopulateLookupTtl
        {
            get
            {
                return this.GetString("action.populate_lookup.ttl", null);
            }

            set
            {
                this.SetCacheValue("action.populate_lookup.ttl", value);
            }
        }

        /// <summary>
        /// Gets or sets the search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
		/// <para>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
		/// </para>
		/// </remarks>
        public string ActionRssCommand
        {
            get
            {
                return this.GetString("action.rss.command", null);
            }

            set
            {
                this.SetCacheValue("action.rss.command", value);
            }
        }

        /// <summary>
        /// Gets or sets the host name used in the web link (URL) that is sent 
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
        /// </remarks>
        public string ActionRssHostname
        {
            get
            {
                return this.GetString("action.rss.hostname", null);
            }

            set
            {
                this.SetCacheValue("action.rss.hostname", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of search results to send in 
		/// RSS alerts.
        /// </summary>
        /// <remarks>
        /// This property's default value is "100".
        /// </remarks>
        public int ActionRssMaxResults
        {
            get
            {
                return this.GetInteger("action.rss.maxresults", -1);
            }

            set
            {
                this.SetCacheValue("action.rss.maxresults", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum amount of time an RSS alert action takes 
        /// before the action is canceled.
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>This property's default value is "1m".</para>
		/// </remarks>
        public string ActionRssMaxTime
        {
            get
            {
                return this.GetString("action.rss.maxtime", null);
            }

            set
            {
                this.SetCacheValue("action.rss.maxtime", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether running this 
		/// RSS action results in a trackable alert.
        /// </summary>
        public bool ActionRssTrackAlert
        {
            get
            {
                return this.GetBoolean("action.rss.track_alert", false);
            }

            set
            {
                this.SetCacheValue("action.rss.track_alert", value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum time-to-live (TTL) of search artifacts if
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
        /// </remarks>
        public string ActionRssTtl
        {
            get
            {
                return this.GetString("action.rss.ttl", null);
            }

            set
            {
                this.SetCacheValue("action.rss.ttl", value);
            }
        }

        /// <summary>
        /// Gets or sets the search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
		/// </remarks>
        public string ActionScriptCommand
        {
            get
            {
                return this.GetString("action.script.command", null);
            }

            set
            {
                this.SetCacheValue("action.script.command", value);
            }
        }

        /// <summary>
        /// Gets or sets the file name of the script to call.
        /// </summary>
        /// <remarks>
        /// This value is required if script action is enabled 
        /// <see cref="IsActionScript"/> is set to true).
        /// </remarks>
        public string ActionScriptFilename
        {
            get
            {
                return this.GetString("action.script.filename", null);
            }

            set
            {
                this.SetCacheValue("action.script.filename", value);
            }
        }

        /// <summary>
        /// Gets or sets the host name used in the web link (URL) that is sent 
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
        /// </remarks>
        public string ActionScriptHostname
        {
            get
            {
                return this.GetString("action.script.hostname", null);
            }

            set
            {
                this.SetCacheValue("action.script.hostname", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of search results to send in 
		/// script alerts.
        /// </summary>
        /// <remarks>
        /// This property's default value is "100".
        /// </remarks>
        public int ActionScriptMaxResults
        {
            get
            {
                return this.GetInteger("action.script.maxresults", -1);
            }

            set
            {
                this.SetCacheValue("action.script.maxresults", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum amount of time a script action takes 
        /// before the action is canceled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>This property's default value is "5m".</para>
		/// </remarks>
        public string ActionScriptMaxTime
        {
            get
            {
                return this.GetString("action.script.maxtime", null);
            }

            set
            {
                this.SetCacheValue("action.script.maxtime", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether running this 
		/// script action results in a trackable alert.
        /// </summary>
        public bool ActionScriptTrackAlert
        {
            get
            {
                return this.GetBoolean("action.script.track_alert", false);
            }

            set
            {
                this.SetCacheValue("action.script.track_alert", value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum time-to-live (TTL) of search artifacts if
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
        /// </remarks>
        public string ActionScriptTtl
        {
            get
            {
                return this.GetString("action.script.ttl", null);
            }

            set
            {
                this.SetCacheValue("action.script.ttl", value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the summary index where the results of the 
        /// scheduled search are saved.
        /// </summary>
        /// <remarks>
        /// <para>This property's default value is "summary".</para>
        /// </remarks>
        public string ActionSummaryIndexName
        {
            get
            {
                return this.GetString("action.summary_index._name", null);
            }

            set
            {
                this.SetCacheValue("action.summary_index._name", value);
            }
        }

        /// <summary>
        /// Gets or sets the search command (or pipeline) that runs the action.
        /// </summary>
        /// <remarks>
        /// Generally the command is a template search pipeline that is
        /// realized with values from the saved search. To reference saved
        /// search field values, wrap them in the '$' symbol. For example, to
        /// reference the saved search <i>name</i> use $name$; to reference
        /// <i>search</i> use $search$.
		/// </remarks>
        public string ActionSummaryIndexCommand
        {
            get
            {
                return this.GetString("action.summary_index.command", null);
            }

            set
            {
                this.SetCacheValue("action.summary_index.command", value);
            }
        }

        /// <summary>
        /// Gets or sets the host name used in the web link (URL) that is sent 
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
        /// </remarks>
        public string ActionSummaryIndexHostname
        {
            get
            {
                return this.GetString("action.summary_index.hostname", null);
            }

            set
            {
                this.SetCacheValue("action.summary_index.hostname", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether to run the 
		/// summary indexing action as part of the scheduled search.
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
        /// </remarks>
        public bool ActionSummaryIndexInline
        {
            get
            {
                return this.GetBoolean("action.summary_index.inline", false);
            }

            set
            {
                this.SetCacheValue("action.summary_index.inline", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of search results to send in 
		/// summary-index alerts.
        /// </summary>
        /// <remarks>
        /// This property's default value is "100".
        /// </remarks>
        public int ActionSummaryIndexMaxResults
        {
            get
            {
                return this.GetInteger("action.summary_index.maxresults", -1);
            }

            set
            {
                this.SetCacheValue("action.summary_index.maxresults", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum amount of time a summary-index action 
		/// takes before the action is canceled. 
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// <para>This property's default value is "5m".</para>
        /// </remarks>
        public string ActionSummaryIndexMaxTime
        {
            get
            {
                return this.GetString("action.summary_index.maxtime", null);
            }

            set
            {
                this.SetCacheValue("action.summary_index.maxtime", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether running this 
        /// summary-index action results in a trackable alert.
        /// </summary>
        public bool ActionSummaryIndexTrackAlert
        {
            get
            {
                return this.GetBoolean(
                    "action.summary_index.track_alert", false);
            }

            set
            {
                this.SetCacheValue("action.summary_index.track_alert", value);
            }
        }

        /// <summary>
        /// Gets or sets the minimum time-to-live (TTL) of search artifacts if
        /// this summary-index action is triggered. 
        /// </summary>
        /// <remarks>
        /// <para>If the value is a number followed by "p", it is the number of
        /// scheduled search periods.</para>
        /// <para>This property's default value is "10p".</para>
        /// <para>If no actions are triggered, the artifacts will have their TTL
		/// determined by the "dispatch.ttl" attribute in savedsearches.conf.
		/// </para>
        /// </remarks>
        public string ActionSummaryIndexTtl
        {
            get
            {
                return this.GetString("action.summary_index.ttl", null);
            }

            set
            {
                this.SetCacheValue("action.summary_index.ttl", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether Splunk applies 
		/// the alert actions to the entire result set (digest) or to each 
		/// individual search result (per result).
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// </remarks>
        public bool AlertDigestMode
        {
            get
            {
                return this.GetBoolean("alert.digest_mode", false);
            }

            set
            {
                this.SetCacheValue("alert.digest_mode", value);
            }
        }

        /// <summary>
        /// Gets or sets the amount of time to show the alert in the dashboard.
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
        /// </remarks>
        public string AlertExpires
        {
            get
            {
                return this.GetString("alert.expires");
            }

            set
            {
                this.SetCacheValue("alert.expires", value);
            }
        }

        /// <summary>
        /// Gets or sets the alert severity level.
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
        /// </remarks>
        public int AlertSeverity
        {
            get
            {
                return this.GetInteger("alert.severity");
            }

            set
            {
                this.SetCacheValue("alert.severity", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether alert 
		/// suppression is enabled for this search.
        /// </summary>
        public bool AlertSuppress
        {
            get
            {
                return this.GetBoolean("alert.suppress", false);
            }

            set
            {
                this.SetCacheValue("alert.suppress", value);
            }
        }

        /// <summary>
        /// Gets or sets a comma-delimited list of fields to use for alert 
        /// suppression when doing per-result alerting.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is required if suppression is turned on and
        /// per-result alerting is enabled.
        /// </para>
        /// </remarks>
        public string AlertSuppressFields
        {
            get
            {
                return this.GetString("alert.suppress.fields", null);
            }

            set
            {
                this.SetCacheValue("alert.suppress.fields", value);
            }
        }

        /// <summary>
        /// Gets or sets the suppression period, which is only valid if
        /// <see cref="AlertSuppress"/> is enabled.
        /// </summary>
        /// <remarks>
        /// <para>The property value's valid format is an <i>integer</i>
        /// followed by a time unit ("s" for seconds, "m" for minutes, "h" for
        /// hours, or "d" for days). For instance, "2s" means 2 seconds.
        /// </para>
        /// </remarks>
        public string AlertSuppressPeriod
        {
            get
            {
                return this.GetString("alert.suppress.period", null);
            }

            set
            {
                this.SetCacheValue("alert.suppress.period", value);
            }
        }

        /// <summary>
        /// Gets or sets a keyword value that indicates how to track the 
		/// actions triggered by this saved search. 
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
        /// </remarks>
        public string AlertTrack
        {
            get
            {
                return this.GetString("alert.track");
            }

            set
            {
                this.SetCacheValue("alert.track", value);
            }
        }

        /// <summary>
        /// Gets or sets the alert comparator for alert triggering. 
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
        /// </remarks>
        public string AlertComparator
        {
            get
            {
                return this.GetString("alert_comparator", null);
            }

            set
            {
                this.SetCacheValue("alert_comparator", value);
            }
        }

        /// <summary>
        /// Gets or sets a conditional search that is evaluated against the 
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
        /// </remarks>
        public string AlertCondition
        {
            get
            {
                return this.GetString("alert_condition", null);
            }

            set
            {
                this.SetCacheValue("alert_condition", value);
            }
        }

        /// <summary>
        /// Gets or sets the value to compare to before triggering the alert 
		/// action. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// If this value is expressed as a percentage, it indicates the
        /// value to use when <see cref="AlertComparator"/> is set to "rises by 
        /// perc" or "drops by perc."
        /// </para>
        /// </remarks>
        public string AlertThreshold
        {
            get
            {
                return this.GetString("alert_threshold", null);
            }

            set
            {
                this.SetCacheValue("alert_threshold", value);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates what to base the alert on. 
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
        /// </remarks>
        public string AlertType
        {
            get
            {
                return this.GetString("alert_type");
            }

            set
            {
                this.SetCacheValue("alert_type", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum ratio of summary_size/bucket_size, which 
        /// specifies when to stop summarization and deem it unhelpful for a 
        /// bucket. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// The test is only performed if the summary size is larger than 
        /// 5 megabytes (MB). 
        /// </para>
        /// <para>
        /// This property's default value is "0.1".
        /// </para>
        /// <para>
        /// This property is available in Splunk 5.0 and later. 
        /// </para>
        /// </remarks>
        public double AutoSummarizeMaxSummaryRatio
        {
            get
            {
                return this.GetFloat("auto_summarize.max_summary_ratio", -1.0);
            }

            set
            {
                this.SetCacheValue("auto_summarize.max_summary_ratio", value);
            }
        }

        /// <summary>
        /// Gets or sets the cron-style schedule for running this saved search.
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
        /// </remarks>
        public string CronSchedule
        {
            get
            {
                return this.GetString("cron_schedule", null);
            }

            set
            {
                this.SetCacheValue("cron_schedule", value);
            }
        }

        /// <summary>
        /// Gets or sets a human-readable description of this saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is an empty string.
        /// </para>
        /// </remarks>
        public string Description
        {
            get
            {
                return this.GetString("description", null);
            }

            set
            {
                this.SetCacheValue("description", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of timeline buckets.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "0".
        /// </para>
        /// </remarks>
        public int DispatchBuckets
        {
            get
            {
                return this.GetInteger("dispatch.buckets");
            }

            set
            {
                this.SetCacheValue("dispatch.buckets", value);
            }
        }

        /// <summary>
        /// Gets or sets a time string that specifies the earliest time for 
		/// this search. 
        /// </summary>
        /// <remarks>
        /// <para>This value can be a relative or absolute time as formatted 
        /// by <see cref="DispatchTimeFormat"/>.
        /// </para>
        /// </remarks>
        public string DispatchEarliestTime
        {
            get
            {
                return this.GetString("dispatch.earliest_time", null);
            }

            set
            {
                this.SetCacheValue("dispatch.earliest_time", value);
            }
        }

        /// <summary>
        /// Gets or sets a time string that specifies the latest time for this 
		/// search. 
        /// </summary>
        /// <remarks>
        /// <para>This property's value can be a relative or absolute time 
        /// as formatted by <see cref="DispatchTimeFormat"/>.
        /// </para>
        /// </remarks>
        public string DispatchLatestTime
        {
            get
            {
                return this.GetString("dispatch.latest_time", null);
            }

            set
            {
                this.SetCacheValue("dispatch.latest_time", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether lookups are 
		/// enabled for this search.
        /// </summary>
        /// <remarks>
        /// <para>This property's default value is true.
        /// </para>
        /// </remarks>
        public bool DispatchLookups
        {
            get
            {
                return this.GetBoolean("dispatch.lookups");
            }

            set
            {
                this.SetCacheValue("dispatch.lookups", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of results before finalizing 
		/// the search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "500000".
        /// </para>
        /// </remarks>
        public int DispatchMaxCount
        {
            get
            {
                return this.GetInteger("dispatch.max_count");
            }

            set
            {
                this.SetCacheValue("dispatch.max_count", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum amount of time in seconds before 
		/// finalizing the search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "0".
        /// </para>
        /// </remarks>
        public string DispatchMaxTime
        {
            get
            {
                return this.GetString("dispatch.max_time");
            }

            set
            {
                this.SetCacheValue("dispatch.max_time", value);
            }
        }

        /// <summary>
        /// Gets or sets an integer value that specifies how frequently Splunk 
		/// runs the MapReduce reduce phase on accumulated map values.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "10".
        /// </para>
        /// </remarks>
        public int DispatchReduceFreq
        {
            get
            {
                return this.GetInteger("dispatch.reduce_freq");
            }

            set
            {
                this.SetCacheValue("dispatch.reduce_freq", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether to backfill the 
        /// real-time window for this search. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is only valid for real-time searches.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool DispatchRtBackfill
        {
            get
            {
                return this.GetBoolean("dispatch.rt_backfill", false);
            }

            set
            {
                this.SetCacheValue("dispatch.rt_backfill", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether Splunk spawns 
		/// a new search process when running this saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Searches against indexes must run in a separate process.
        /// </para>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// </remarks>
        public bool DispatchSpawnProcess
        {
            get
            {
                return this.GetBoolean("dispatch.spawn_process");
            }

            set
            {
                this.SetCacheValue("dispatch.spawn_process", value);
            }
        }

        /// <summary>
        /// Gets or sets a time format string that defines the time format 
		/// used to specify the earliest and latest times for this search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "%FT%T.%Q%:z".
        /// </para>
        /// </remarks>
        public string DispatchTimeFormat
        {
            get
            {
                return this.GetString("dispatch.time_format");
            }

            set
            {
                this.SetCacheValue("dispatch.time_format", value);
            }
        }

        /// <summary>
        /// Gets or sets the time to live (TTL) for the artifacts of the 
		/// scheduled search (the time before the search job expires and 
		/// artifacts are still available), if no actions are triggered.
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
        /// </remarks>
        public string DispatchTtl
        {
            get
            {
                return this.GetString("dispatch.ttl");
            }

            set
            {
                this.SetCacheValue("dispatch.ttl", value);
            }
        }

        /// <summary>
        /// Gets or sets the default UI view name (not label) in which to 
		/// load the results.
        /// </summary>
        /// <remarks>
        /// Access is dependent on the user having sufficient permissions.
        /// </remarks>
        public string DisplayView
        {
            get
            {
                return this.GetString("displayview", null);
            }

            set
            {
                this.SetCacheValue("displayview", value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum number of concurrent instances of this 
        /// search the scheduler is allowed to run.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is "1".
        /// </para>
        /// </remarks>
        public int MaxConcurrent
        {
            get
            {
                return this.GetInteger("max_concurrent");
            }

            set
            {
                this.SetCacheValue("max_concurrent", value);
            }
        }

        /// <summary>
        /// Gets or sets the time at which the scheduler will run this search
 		/// again.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is read-only.
        /// </para>
        /// </remarks>
        public DateTime NextScheduledTime
        {
            get
            {
                return this.GetDate("next_scheduled_time", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Gets or sets the exact search string that the scheduler will run.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property is read-only.
        /// </para>
        /// </remarks>
        public string QualifiedSearch
        {
            get
            {
                return this.GetString("qualfiedSearch", null);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the scheduler computes the 
		/// next run time of a scheduled search.
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
        /// </remarks>
        public int RealtimeSchedule
        {
            get
            {
                return this.GetInteger("realtime_schedule");
            }

            set
            {
                this.SetCacheValue("realtime_schedule", value);
            }
        }

        /// <summary>
        /// Gets or sets a string value that specifies the app in which Splunk 
		/// Web dispatches this search.
        /// </summary>
        public string RequestUiDispatchApp
        {
            get
            {
                return this.GetString("request.ui_dispatch_app", null);
            }

            set
            {
                this.SetCacheValue("request.ui_dispatch_app", value);
            }
        }

        /// <summary>
        /// Gets or sets a string value that specifies the view in which Splunk
 		/// Web displays this search.
        /// </summary>
        public string RequestUiDispatchView
        {
            get
            {
                return this.GetString("request.ui_dispatch_view", null);
            }

            set
            {
                this.SetCacheValue("request.ui_dispatch_view", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether a real-time 
		/// search managed by the scheduler is restarted when a search peer 
		/// becomes available for this saved search.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The peer can be a newly added peer or a peer that has been down
        /// and has become available.
        /// </para>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// </remarks>
        public bool RestartOnSearchPeerAdd
        {
            get
            {
                return this.GetBoolean("restart_on_searchpeer_add");
            }

            set
            {
                this.SetCacheValue("restart_on_searchpeer_add", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether this search is 
		/// run when Splunk starts.
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
        /// </remarks>
        public bool RunOnStartup
        {
            get
            {
                return this.GetBoolean("run_on_startup");
            }

            set
            {
                this.SetCacheValue("run_on_startup", value);
            }
        }

        /// <summary>
        /// Gets or sets the search expression for this saved search.
        /// </summary>
        public string Search
        {
            get
            {
                return this.GetString("search");
            }

            set
            {
                this.SetCacheValue("search", value);
            }
        }

        /// <summary>
        /// Gets or sets the view state ID that is associated with the view 
        /// specified in the <see cref="DisplayView"/> property.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This ID corresponds to a stanza in the viewstates.conf 
		/// configuration file.
        /// </para>
        /// </remarks>
        public string Vsid
        {
            get
            {
                return this.GetString("vsid", null);
            }

            set
            {
                this.SetCacheValue("vsid", value);
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the email action is
 		/// enabled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool IsActionEmail
        {
            get
            {
                return this.GetBoolean("action.email");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the populate-lookup
 		/// action is enabled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool IsActionPopulateLookup
        {
            get
            {
                return this.GetBoolean("action.populate_lookup");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether RSS action is enabled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool IsActionRss
        {
            get
            {
                return this.GetBoolean("action.rss");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the script action is
 		/// enabled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool IsActionScript
        {
            get
            {
                return this.GetBoolean("action.script");
            }
        }

        /// <summary>
        /// Gets a Boolean value that indicates whether the summary-index
 		/// action is enabled.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool IsActionSummaryIndex
        {
            get
            {
                return this.GetBoolean("action.summary_index");
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether this search is 
		/// run on a schedule.
        /// </summary>
        public bool IsScheduled
        {
            get
            {
                return this.GetBoolean("is_scheduled");
            }

            set
            {
                this.SetCacheValue("is_scheduled", value);
            }
        }

        /// <summary>
        /// Gets or sets a Boolean value that indicates whether the search 
		/// should be visible in the saved search list.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's default value is true.
        /// </para>
        /// </remarks>
        public bool IsVisible
        {
            get
            {
                return this.GetBoolean("is_visible");
            }

            set
            {
                this.SetCacheValue("is_visible", value);
            }
        }

        /// <summary>
        /// Sets a comma-separated list of actions to enable.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This property's value can include any of the following strings,
		/// separated by commas:
        /// <list type="bullet">
        /// <item>"email"</item>
        /// <item>"populate_lookup"</item> 
        /// <item>"rss"</item>
        /// <item>"script"</item>
        /// <item>"summary_index"</item>
        /// </list>
        /// </para>
        /// </remarks>
        public string TriggerActions
        {
            set
            {
                this.SetCacheValue("actions", value);
            }
        }

        /// <summary>
        /// Sets a wildcard argument that accepts any action.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Use this property to specify specific action arguments. For 
		/// example, to specify the email recipients for the 
        /// <see cref="ActionEmailTo"/> property.
        /// </para>
        /// </remarks>
        public string ActionWildcard
        {
            set
            {
                this.SetCacheValue("action.*", value);
            }
        }

        /// <summary>
        /// Sets a wildcard argument that accepts any saved search template
        /// argument.
        /// </summary>
        /// <remarks>
        /// <para>
        /// An example saved search template is "args.username=foobar" when 
        /// the search is "search $username$".
        /// </para>
        /// </remarks>
        public string ArgsWildcard
        {
            set
            {
                this.SetCacheValue("args.*", value);
            }
        }

        /// <summary>
        /// Sets a Boolean value that indicates whether the saved search is 
		/// disabled. 
        /// </summary>
        /// <remarks>
        /// <para>
        /// Disabled searches are not visible in Splunk Web.
        /// </para>
        /// <para>
        /// This property's default value is false.
        /// </para>
        /// </remarks>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Sets a wildcard argument that accepts any dispatch-related 
        /// argument.
        /// </summary>
        public string DispatchWildcard
        {
            set
            {
                this.SetCacheValue("dispatch.*", value);
            }
        }

        /// <summary>
        /// Acknowledges the suppression of alerts from this saved search
        /// and resumes alerting.
        /// </summary>
        public void Acknowledge()
        {
            this.Service.Post(this.ActionPath("acknowledge"));
            this.Invalidate();
        }

        /// <summary>
        /// Returns the path that corresponds to the requested action
        /// </summary>
        /// <param name="action">The requested action.</param>
        /// <returns>The path to the action.</returns>
        protected override string ActionPath(string action)
        {
            if (action.Equals("acknowledge"))
            {
                return this.Path + "/acknowledge";
            }
            else if (action.Equals("dispatch"))
            {
                return this.Path + "/dispatch";
            }
            else if (action.Equals("history"))
            {
                return this.Path + "/history";
            }
            else
            {
                return base.ActionPath(action);
            }
        }

        /// <summary>
        /// Runs the saved search.
        /// </summary>
        /// <returns>The job.</returns>
        public Job Dispatch()
        {
            return this.Dispatch(null);
        }

        /// <summary>
        /// Runs the saved search using dispatch arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The job.</returns>
        public Job Dispatch(Args args)
        {
            ResponseMessage response = 
                this.Service.Post(this.ActionPath("dispatch"), args);
            this.Invalidate();
            string sid = Job.SidExtraction(response);

            Job job;
            JobCollection jobs = this.Service.GetJobs();
            job = jobs.Get(sid);

            // If job not yet scheduled, create an empty job object
            if (job == null)
            {
                job = new Job(this.Service, "search/jobs/" + sid);
            }

            return job;
        }

        /// <summary>
        /// Returns an array of search jobs created from this saved search.
        /// </summary>
        /// <returns>An array of jobs.</returns>
        public Job[] History()
        {
            ResponseMessage response = 
                this.Service.Get(this.ActionPath("history"));
            AtomFeed feed;

            feed = AtomFeed.Parse(response.Content);

            int count = feed.Entries.Count;
            Job[] result = new Job[count];
            for (int i = 0; i < count; ++i)
            {
                string sid = feed.Entries[i].Title;
                result[i] = new Job(this.Service, "search/jobs/" + sid);
            }
            return result;
        }

        /// <summary>
        /// Updates the entity with the values you previously set using the 
        /// corresponding properties, and any additional specified arguments.
        /// </summary>
        /// <param name="args">The key/value pairs to update.</param>
        /// <remarks>
        /// The specified arguments take precedence over the values that were
        /// previously set using properties.
        /// </remarks>
        public override void Update(Dictionary<string, object> args)
        {
            // Add required arguments if not already present
            if (!args.ContainsKey("search"))
            {
                args = Args.Create(args);
                args.Add("search", this.Search);
            }
            base.Update(args);
        }

        /// <summary>
        /// Updates the entity with the accumulated arguments, established by 
        /// the corresponding properties for each specific entity class.
        /// </summary>
        public override void Update()
        {
            // If not present in the update keys, add required attribute as long
            // as one pre-existing update pair exists.
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("search"))
            {
                this.SetCacheValue("search", this.Search);
            }
            base.Update();
        }
    }
}
