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
    /// TODO: Update summary.
    /// </summary>
    public class SavedSearch : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SavedSearch"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public SavedSearch(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets or sets the email password.
        /// </summary>
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
        /// Gets or sets the email username.
        /// </summary>
        /// <returns></returns>
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
        /// Gets or sets the blind carbon copy (BCC) email address.
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
        /// Gets or sets the carbon copy (CC) email address.
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
        /// Generally, this command is a template search pipeline that is 
        /// realized with values from the saved search. To reference saved 
        /// search field values, wrap them in $. For example, use $name$ to 
        /// reference the saved search name, or use $search$ to reference the 
        /// search query.
        /// </summary>
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
        /// Gets or sets the format of text in the email. This value also 
        /// applies to any attachments formats. Valid values are: "plain", 
        /// "html", "raw", and "csv".
        /// </summary>
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
        /// Gets or sets the email sender's name.
        /// </summary>
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
        /// Gets or sets the the host name used in the web link (URL) that is 
        /// sent in email alerts.
        /// </summary>
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
        /// Gets or sets a value indicating whether the search results are 
        /// contained in the body of the email.
        /// </summary>
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
        /// Gets or sets the address of the MTA server that is used to send the 
        /// emails. If this attribute is not set, this value defaults to the 
        /// setting in the alert_actions.conf file.
        /// </summary>
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
        /// Gets or sets the maximum amount of time an email action takes before
        /// the action is canceled. The valid format is <i>number</i> followed 
        /// by a time unit ("s", "m", "h", or "d").
        /// </summary>
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
        /// Gets or sets the name of the view to deliver if ActionEmailSendPdf
        /// is enabled.
        /// </summary>
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
        /// Gets or sets the search string for pre-processing results before 
        /// emailing them. Usually preprocessing consists of filtering out 
        /// unwanted internal fields.
        /// </summary>
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
        /// Gets or sets the paper orientation. Valid values are "portrait" and
        /// "landscape".
        /// </summary>
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
        /// Gets or sets the paper size for PDFs. Valid values are:
        /// "letter", "legal", "ledger", "a2", "a3", "a4", and "a5".
        /// </summary>
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
        /// Gets or sets a value indicating whether the PDF server is enabled.
        /// </summary>
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
        /// Gets or sets a value indicating whether to create and send the 
        /// results in PDF format.
        /// </summary>
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
        /// Gets or sets a value indicating whether search results are attached 
        /// to an email.
        /// </summary>
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
        /// Gets or sets a comma or semicolon delimited list of email 
        /// recipients.
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
        /// Gets or sets a value indicating whether running this email action 
        /// results in a trackable alert.
        /// </summary>
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
        /// Gets or sets the minimum time-to-live (ttl) of search artifacts if
        /// this email action is triggered. If the value is a number followed 
        /// by "p", it is the number of scheduled search periods.
        /// </summary>
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
        /// Gets or sets a value indicating whether to use secure socket layer 
        /// (SSL) when communicating with the SMTP server.
        /// </summary>
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
        /// Gets or sets a value indicating whether to use transport layer 
        /// security (TLS) when communicating with the SMTP server.
        /// </summary>
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
        /// Gets or sets a value indicating whether columns should be sorted 
        /// from least wide to most wide, left to right.
        /// This value is only used when ActionEmailFormat is "plain".
        /// </summary>
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
        /// Valid forms are "hostname" and "protocol://hostname:port".
        /// </summary>
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
        /// The valid format is a number followed by a time unit ("s", "m", "h",
        /// or "d").
        /// </summary>
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
        /// Gets or sets a value indicating whether running this populate-lookup 
        /// action results in a trackable alert.
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
        /// Gets or sets the minimum time-to-live (ttl) of search artifacts if
        /// this populate-lookup action is triggered. If the value is a number
        /// followed by "p", it is the number of scheduled search periods.
        /// </summary>
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
        /// Gets or sets the maximum number of search results to send in RSS 
        /// alerts.
        /// </summary>
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
        /// The valid format is a number followed by a time unit ("s", "m", "h",
        /// or "d").
        /// </summary>
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
        /// Gets or sets a value indicating whether running this RSS action 
        /// results in a trackable alert.
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
        /// Gets or sets the minimum time-to-live (ttl) of search artifacts if
        /// this RSS action is triggered. If the value is a number followed by 
        /// "p", it is the number of scheduled search periods.
        /// </summary>
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
        /// Valid forms are "hostname" and "protocol://hostname:port".
        /// </summary>
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
        /// Gets or sets the maximum number of search results to send in script 
        /// alerts.
        /// </summary>
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
        /// Gets or sets the maximum amount of time a script action takes before
        /// the action is canceled.
        /// The valid format is a number followed by a time unit ("s", "m", "h",
        /// or "d").
        /// </summary>
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
        /// Gets or sets a value indicating whether running this script action 
        /// results in a trackable alerts.
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
        /// Gets or sets the minimum time-to-live (ttl) of search artifacts if
        /// this script action is triggered. If the value is a number followed 
        /// by "p", it is the number of scheduled search periods.
        /// </summary>
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
        /// Generally, this command is a template search pipeline that is 
        /// realized with values from the saved search. To reference saved 
        /// search field values, wrap them in $. For example, use $name$ to 
        /// reference the saved search name, or use $search$ to reference the 
        /// search query.
        /// </summary>
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
        /// Valid forms are "hostname" and "protocol://hostname:port".
        /// </summary>
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
        /// Gets or sets a value indicating whether to run the summary indexing
        /// action as part of the scheduled search.
        /// </summary>
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
        /// Gets or sets the maximum amount of time a summary action takes 
        /// before the action is canceled.
        /// The valid format is a number followed by a time unit ("s", "m", "h",
        /// or "d").
        /// </summary>
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
        /// Gets or sets a value indicating whether running this summary-index 
        /// action results in a trackable alert.
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
        /// Gets or sets the minimum time-to-live (ttl) of search artifacts if
        /// a summary-index action is triggered. If the value is a number 
        /// followed by "p", it is the number of scheduled search periods.
        /// </summary>
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
        /// Gets or sets a value indicating whether Splunk applies the alert 
        /// actions to the entire result set (digest) or to each individual 
        /// search result (per result).
        /// </summary>
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
        /// Gets or sets the alert severity level. Valid values are:
        /// 1=DEBUG, 2=INFO, 3=WARN, 4=ERROR, 5=SEVERE, 6=FATAL.
        /// </summary>
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
        /// Gets or sets a value indicating whether alert suppression is enabled
        /// for this search.
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
        /// Gets or sets a comma-delimeted list of fields to use for alert 
        /// suppression.
        /// </summary>
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
        /// Gets or sets the  suppression period, which is only valid if
        /// AlertSuppress is enabled.
        /// The valid format is a number followed by a time unit ("s", "m", "h",
        /// or "d").
        /// </summary>
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
        /// Gets or sets a value that indicates how to track the actions
        /// triggered by this saved search. Valid values are: "true" (enabled), 
        /// "false" (disabled), and "auto" (tracking is based on the setting of
        /// each action).
        /// </summary>
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
        /// Gets or sets the alert comparator. Valid values are: "greater than", 
        /// "less than", "equal to", "rises by", "drops by", "rises by perc",
        /// and "drops by perc".
        /// </summary>
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
        /// action. If this value is expressed as a percentage, it indicates the
        /// value to use when AlertComparator is set to "rises by perc" or 
        /// "drops by perc."
        /// </summary>
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
        /// Gets or sets a value that indicates what to base the alert on. Valid
        /// values are: "always", "custom", "number of events", "number of 
        /// hosts", and "number of sources". This value is overridden by 
        /// AlertCondition if specified.
        /// </summary>
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
        /// Gets or sets the cron-style schedule for running this saved search.
        /// </summary>
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
        /// Gets or sets a description of this saved search.
        /// </summary>
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
        /// Gets or sets the earliest time for this search. This value can be a 
        /// relative or absolute time as formatted by DispatchTimeFormat.
        /// </summary>
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
        /// Gets or sets latest time for this search. This value can be a
        /// relative or absolute time as formatted by DispatchTimeFormat.
        /// </summary>
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
        /// Gets or sets a value indicating whether lookups are enabled for 
        /// this search.
        /// </summary>
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
        /// Gets or sets the maximum number of results before finalizing the 
        /// search.
        /// </summary>
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
        /// Gets or sets the maximum amount of time before finalizing the 
        /// search.
        /// </summary>
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
        /// Gets or sets how frequently Splunk runs the MapReduce reduce phase
        /// on accumulated map values.
        /// </summary>
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
        /// Gets or sets a value indicating whether to back fill the real-time 
        /// window for this search. This attribute is only valid for real-time 
        /// searches.
        /// </summary>
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
        /// Gets or sets a value indicating whether Splunk spawns a new search 
        /// process when running this saved search.
        /// </summary>
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
        /// Gets or sets the time format used to specify the earliest and 
        /// latest times for this search.
        /// </summary>
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
        /// Gets or sets the time to live (ttl) for artifacts of the scheduled 
        /// search (the time before the search job expires and artifacts are 
        /// still available), if no alerts are triggered. If the value is a 
        /// number followed by "p", it is the number of scheduled search 
        /// periods.
        /// </summary>
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
        /// Gets or sets the default view in which to load results.
        /// </summary>
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
        /// Gets the next scheduled time.
        /// </summary>
        public DateTime NextScheduledTime
        {
            get
            {
                return this.GetDate("next_scheduled_time", DateTime.MaxValue);
            }
        }

        /// <summary>
        /// Gets the qualified search.
        /// </summary>
        public string QualifiedSearch
        {
            get
            {
                return this.GetString("qualfiedSearch", null);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the scheduler computes the 
        /// next run time of a scheduled search based on the current time or on
        /// the last search run time (for continuous scheduling). Note, 
        /// although the REST API specifies this as a boolean, and integer with
        /// values of 0 or 1 makes more sense from a documentation point of 
        /// view.
        /// </summary>
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
        /// Gets or sets the app in which Splunk Web dispatches this search.
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
        /// Gets or sets the view in which Splunk Web displays this search.
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
        /// Gets or sets a value indicating whether a real-time search managed
        /// by the scheduler is restarted when a search peer becomes available 
        /// for this saved search.
        /// </summary>
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
        /// Gets or sets a value indicating whether this search is run when 
        /// Splunk starts. If the search is not run on startup, it runs at the 
        /// next scheduled time.
        /// </summary>
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
        /// specified in the DisplayView attribute. This ID corresponds to a 
        /// stanza in the viewstates.conf configuration file.
        /// </summary>
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
        /// Gets a value indicating whether the email action is enabled.
        /// </summary>
        public bool IsActionEmail
        {
            get
            {
                return this.GetBoolean("action.email");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the populate-lookup action is 
        /// enabled.
        /// </summary>
        public bool IsActionPopulateLookup
        {
            get
            {
                return this.GetBoolean("action.populate_lookup");
            }
        }

        /// <summary>
        /// Gets a value indicating whether  RSS action is enabled.
        /// </summary>
        public bool IsActionRss
        {
            get
            {
                return this.GetBoolean("action.rss");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the script action is enabled.
        /// </summary>
        public bool IsActionScript
        {
            get
            {
                return this.GetBoolean("action.script");
            }
        }

        /// <summary>
        /// Gets a value indicating whether the summary-index action is enabled.
        /// </summary>
        public bool IsActionSummaryIndex
        {
            get
            {
                return this.GetBoolean("action.summary_index");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this search is run on a 
        /// schedule.
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
        /// Gets or sets a value indicating whether the search should be visible
        /// in the saved search list.
        /// </summary>
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
        /// Sets whichs actions to enable. Valid actions are: "email",
        /// "populate_lookup", "rss", "script", and "summary_index".
        /// </summary>
        public string TriggerActions
        {
            set
            {
                this.SetCacheValue("actions", value);
            }
        }

        /// <summary>
        /// Sets the wildcard argument that accepts any action.
        /// </summary>
        public string ActionWildcard
        {
            set
            {
                this.SetCacheValue("action.*", value);
            }
        }

        /// <summary>
        /// Sets the wildcard argument that accepts any saved search template
        /// argument, such as "args.username=foobar" when the search is "search
        /// $username$"
        /// </summary>
        public string ArgsWildcard
        {
            set
            {
                this.SetCacheValue("args.*", value);
            }
        }

        /// <summary>
        /// Sets a value indicating whether the saved search is disabled. 
        /// Disabled searches are not visible in Splunk Web.
        /// </summary>
        public bool Disabled
        {
            set
            {
                this.SetCacheValue("disabled", value);
            }
        }

        /// <summary>
        /// Sets the wildcard argument that accepts any dispatch-related 
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
        /// <param name="action">The requested action</param>
        /// <returns>The path to the action</returns>
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
        /// Runs the saved search
        /// </summary>
        /// <returns>The job</returns>
        public Job Dispatch()
        {
            return this.Dispatch(null);
        }

        /// <summary>
        /// Runs the saved search using dispatch arguments.
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns>The job</returns>
        public Job Dispatch(Args args)
        {
            ResponseMessage response = 
                this.Service.Post(this.ActionPath("dispatch"), args);
            this.Invalidate();
            string sid = Job.SidExtraction(response);

            Job job;
            JobCollection jobs = this.Service.GetJobs();
            job = jobs.Get(sid);

            // if job not yet scheduled, create an empty job object
            if (job == null)
            {
                job = new Job(this.Service, "search/jobs/" + sid);
            }

            return job;
        }

        /// <summary>
        /// Returns an array of search jobs created from this saved search.
        /// </summary>
        /// <returns>An array of jobs</returns>
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
        /// setter methods, and any additional specified arguments. The 
        /// specified arguments take precedent over the values that were set
        /// using the setter methods.
        /// </summary>
        /// <param name="args">The key/value pairs to update</param>
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
        /// the individual setter methods for each specific entity class.
        /// </summary>
        public override void Update()
        {
            // If not present in the update keys, add required attribute as long
            // as one pre-existing update pair exists
            if (toUpdate.Count > 0 && !toUpdate.ContainsKey("search"))
            {
                this.SetCacheValue("search", this.Search);
            }
            base.Update();
        }
    }
}
