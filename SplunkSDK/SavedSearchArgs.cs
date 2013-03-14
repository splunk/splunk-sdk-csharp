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
    /// Extends Args for SavedSearch creation setters
    /// </summary>
    public class SavedSearchArgs : Args
    {
        /// <summary>
        /// Sets the  Wildcard argument for any action. 
        /// </summary>
        public string ActionWildcard
        {
            set
            {
                this["action.*"] = value;
            }
        }

        /// <summary>
        /// Sets the password to use when authenticating with the SMTP server. 
        /// Normally this value will be set when editing the email settings, 
        /// however you can set a clear text password here and it will be 
        /// encrypted on the next Splunk restart. Defaults to empty string.
        /// </summary>
        public string ActionEmailAuthPassword
        {
            set
            {
                this["action.email.auth_password"] = value;
            }
        }

        /// <summary>
        /// Sets the username to use when authenticating with the SMTP server.
        /// If this is empty string, no authentication is attempted. Defaults 
        /// to empty string. NOTE: Some SMTP servers reject unauthenticated 
        /// emails.
        /// </summary>
        public string ActionEmailAuthUsername
        {
            set
            {
                this["action.email.auth_username"] = value;
            }
        }

        /// <summary>
        /// Sets the BCC email address to use if action.email is enabled.
        /// </summary>
        public string ActionEmailBcc
        {
            set
            {
                this["action.email.bcc"] = value;
            }
        }

        /// <summary>
        /// Sets the CC email address to use if action.email is enabled.
        /// </summary>
        public string ActionEmailCc
        {
            set
            {
                this["action.email.cc"] = value;
            }
        }

        /// <summary>
        /// Sets the search command (or pipeline) which is responsible for 
        /// executing the action. Generally the command is a template search 
        /// pipeline which is realized with values from the saved search. To 
        /// reference saved search field values wrap them in $, for example 
        /// to reference the savedsearch name use $name$, to reference the 
        /// search use $search$.
        /// </summary>
        public string ActionEmailCommand
        {
            set
            {
                this["action.email.command"] = value;   
            }
        }

        /// <summary>
        /// Sets the format of text in the email. Valid values are: 
        /// (plain | html | raw | csv). This value also applies to attachments.
        /// </summary>
        public string ActionEmailFormat
        {
            set
            {
                this["action.email.format"] = value;
            }
        }

        /// <summary>
        /// Sets the Email address from which the email action originates.
        /// The default is splunk@$LOCALHOST,  or whatever value is set in 
        /// alert_actions.conf.
        /// </summary>
        public string ActionEmailFrom
        {
            set
            {
                this["action.email.from"] = value;
            }
        }

        /// <summary>
        /// Sets the hostname used in the URL sent in email actions. This value 
        /// has two forms:
        /// <example>
        /// hostname (for example, splunkserver, splunkserver.example.com)
        /// </example>
        /// <example>
        /// protocol://hostname:port (like: http://splunkserver:8000, or
        /// https://splunkserver.example.com:443)
        /// </example>
        /// When set to a simple hostname, the protocol and port which are 
        /// configured within splunk are used to construct the base of the url.
        /// When set to 'http://...', it is used verbatim. NOTE: This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases when the Splunk server is not
        /// aware of how to construct an externally referencable url, such as 
        /// SSO environments, other proxies, or when the Splunk server hostname 
        /// is not generally resolvable. The default is the current hostname 
        /// provided by the operating system, or if that fails "localhost". When
        /// set to empty, default behavior is used.
        /// </summary>
        public string ActionEmailHostname
        {
            set
            {
                this["action.email.hostname"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the search results are contained in 
        /// the body of the email. Results can be either inline or attached to 
        /// an email. See action.email.sendresults.
        /// </summary>
        public bool ActionEmailInline
        {
            set
            {
                this["action.email.inline"] = value;
            }
        }

        /// <summary>
        /// Sets the address of the MTA server to be used to send the emails.
        /// The Default is LOCALHOST, or whatever is set in alert_actions.conf.
        /// </summary>
        public string ActionEmailMailServer
        {
            set
            {
                this["action.email.mailserver"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of search results to send when action.email
        /// is enabled. The defaults is 100.
        /// </summary>
        public string ActionEmailMaxResults
        {
            set
            {
                this["action.email.maxresults"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum amount of time the execution of an email action 
        /// takes before the action is aborted. Valid values are an integer 
        /// followed by m|s|h|d. The default is 5m.
        /// </summary>
        public string ActionEmailMaxTime
        {
            set
            {
                this["action.email.maxtime"] = value;
            }
        }

        /// <summary>
        /// Sets the name of the view to deliver if sendpdf is enabled.
        /// </summary>
        public string ActionEmailPdfView
        {
            set
            {
                this["action.email.pdfview"] = value;
            }
        }

        /// <summary>
        /// Sets the search string to preprocess results before emailing them. 
        /// The default is an empty string which implies no preprocessing
        /// Preprocessing normally consists of filtering out unwanted internal 
        /// fields.
        /// </summary>
        public string ActionEmailPreprocessResults
        {
            set
            {
                this["action.email.preprocess_results"] = value;
            }
        }

        /// <summary>
        /// Sets the paper orientation: portrait or landscape. The defaults is 
        /// portrait.
        /// </summary>
        public string ActionEmailReportPaperOrientation
        {
            set
            {
                this["action.email.reportPaperOrientation"] = value;
            }
        }

        /// <summary>
        /// Sets the paper size for PDFs. the valid values are: 
        /// letter | legal | ledger | a2 | a3 | a4 | a5. The defaults is letter.
        /// </summary>
        public string ActionEmailReportPaperSize
        {
            set
            {
                this["action.email.reportPaperSize"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the PDF server is enabled. The 
        /// default is false.
        /// </summary>
        public bool ActionEmailReportServerEnabled
        {
            set
            {
                this["action.email.reportServerEnabled"] = value;
            }
        }

        /// <summary>
        /// Sets the URL of the PDF report server. For a locally installed 
        /// report server, the default URL is http://localhost:8091/
        /// </summary>
        public string ActionEmailReportServerUrl
        {
            set
            {
                this["action.email.reportServerURL"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to create and send the results as a
        /// PDF. The default is false.
        /// </summary>
        public bool ActionEmailSendPdf
        {
            set
            {
                this["action.email.sendpdf"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to attach the search results in the 
        /// email. Note: Results can be either attached or inline. See 
        /// action.email.inline.
        /// </summary>
        public bool ActionEmailSendResults
        {
            set
            {
                this["action.email.sendresults"] = value;
            }
        }

        /// <summary>
        /// Sets the subject of the email. The default is 
        /// SplunkAlert-(savedsearchname).
        /// </summary>
        public string ActionEmailSubject
        {
            set
            {
                this["action.email.subject"] = value;
            }
        }

        /// <summary>
        /// Sets the emails recipient. This value can be comma or semicolon 
        /// separated list of recipient email addresses. This value is required 
        /// if this search is scheduled and the email alert action is enabled.
        /// </summary>
        public string ActionEmailTo
        {
            set
            {
                this["action.email.to"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the execution of this action 
        /// signifies a trackable alert.
        /// </summary>
        public bool ActionEmailTrackAlert
        {
            set
            {
                this["action.email.track_alert"] = value;
            }
        }

        /// <summary>
        /// Sets the minimum time-to-live, in seconds, of the search artifacts 
        /// if this action is triggered. If a "p" follows the value, the value 
        /// is interpreted as periods, not seconds. The default is 86400
        /// (24 hours). Note: if no actions are triggered, the artifacts have 
        /// their ttl determined by dispatch.ttl in savedsearches.conf.
        /// </summary>
        public string ActionEmailTtl
        {
            set
            {
                this["action.email.ttl"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to use SSL (Secure Socket Layer) 
        /// when communicating with the SMTP server. The default value is false.
        /// </summary>
        public bool ActionEmailUseSsl
        {
            set
            {
                this["action.email.ssl"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to use TLS (Transport Layer 
        /// Security) when communicating with the SMTP server. The default value
        /// is false.
        /// </summary>
        public bool ActionEmailUseTls
        {
            set
            {
                this["action.email.tls"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether columns should be sorted from least
        /// wide to most wide, left to right. Only valid if 
        /// action.email.format=plain.
        /// </summary>
        public bool ActionEmailWidthSortColumns
        {
            set
            {
                this["action.email.width_sort_columns"] = value;
            }
        }

        /// <summary>
        /// Sets the search command (or pipeline) which is responsible for 
        /// executing the action. Generally the command is a template search 
        /// pipeline which is realized with values from the saved search. To 
        /// reference saved search field values wrap them in $, for example 
        /// to reference the savedsearch name use $name$, to reference the 
        /// search use $search$.
        /// </summary>
        public string ActionPopulateLookupCommand
        {
            set
            {
                this["action.populate_lookup.command"] = value;
            }
        }

        /// <summary>
        /// Sets the Lookup name of path of the lookup to populate.
        /// </summary>
        public string ActionPopulateLookupDest
        {
            set
            {
                this["action.populate_lookup.dest"] = value;
            }
        }

        /// <summary>
        /// Sets the hostname used in the URL sent in email actions. This value 
        /// has two forms:
        /// <example>
        /// hostname (for example, splunkserver, splunkserver.example.com)
        /// </example>
        /// <example>
        /// protocol://hostname:port (like: http://splunkserver:8000, or
        /// https://splunkserver.example.com:443)
        /// </example>
        /// When set to a simple hostname, the protocol and port which are 
        /// configured within splunk are used to construct the base of the url.
        /// When set to 'http://...', it is used verbatim. NOTE: This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases when the Splunk server is not
        /// aware of how to construct an externally referencable url, such as 
        /// SSO environments, other proxies, or when the Splunk server hostname 
        /// is not generally resolvable. The default is the current hostname 
        /// provided by the operating system, or if that fails "localhost". When
        /// set to empty, default behavior is used.
        /// </summary>
        public string ActionPopulateLookupHostname
        {
            set
            {
                this["action.populate_lookup.hostname"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of search results to sent via alerts.
        /// The defaults is 100.
        /// </summary>
        public int ActionPopulateLookupMaxResults
        {
            set
            {
                this["action.populate_lookup.maxresults"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum amount of time the execution an action takes before 
        /// the action is aborted. Valid values are an integer followed by 
        /// m|s|h|d. The default is 5m.
        /// </summary>
        public string ActionPopulateLookupMaxTime
        {
            set
            {
                this["action.populate_lookup.maxtime"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the execution of this action 
        /// signifies a trackable alert.
        /// </summary>
        public bool ActionPopulateLookupTrackAlert
        {
            set
            {
                this["action.populate_lookup.track_alert"] = value;
            }
        }

        /// <summary>
        /// Sets the minimum time-to-live, in seconds, of the search artifacts 
        /// if this action is triggered. If a "p" follows the value, the value
        /// is interpreted as periods, not seconds. The default is 10p. Note: 
        /// if no actions are triggered, the artifacts have their ttl determined 
        /// by dispatch.ttl in savedsearches.conf.
        /// </summary>
        public string ActionPopulateLookupTtl
        {
            set
            {
                this["action.populate_lookup.ttl"] = value;
            }
        }

        /// <summary>
        /// Sets the search command (or pipeline) which is responsible for 
        /// executing the action. Generally the command is a template search
        /// pipeline which is realized with values from the saved search. To 
        /// reference saved search field values wrap them in $, for example to 
        /// reference the savedsearch name use $name$, to reference the search 
        /// use $search$.
        /// </summary>
        public string ActionRssCommand
        {
            set
            {
                this["action.rss.command"] = value;
            }
        }

        /// <summary>
        /// Sets the hostname used in the URL sent in email actions. This value 
        /// has two forms:
        /// <example>
        /// hostname (for example, splunkserver, splunkserver.example.com)
        /// </example>
        /// <example>
        /// protocol://hostname:port (like: http://splunkserver:8000, or
        /// https://splunkserver.example.com:443)
        /// </example>
        /// When set to a simple hostname, the protocol and port which are 
        /// configured within splunk are used to construct the base of the url.
        /// When set to 'http://...', it is used verbatim. NOTE: This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases when the Splunk server is not
        /// aware of how to construct an externally referencable url, such as 
        /// SSO environments, other proxies, or when the Splunk server hostname 
        /// is not generally resolvable. The default is the current hostname 
        /// provided by the operating system, or if that fails "localhost". When
        /// set to empty, default behavior is used.
        /// </summary>
        public string ActionRssHostname
        {
            set
            {
                this["action.rss.hostname"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of search results to sent via alerts.
        /// The defaults is 100.
        /// </summary>
        public int ActionRssMaxResults
        {
            set
            {
                this["action.rss.maxresults"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum amount of time the execution an action takes before 
        /// the action is aborted. Valid values are an integer followed by
        /// m|s|h|d. The default is 1m.
        /// </summary>
        public string ActionRssMaxTime
        {
            set
            {
                this["action.rss.maxtime"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the execution of this action 
        /// signifies a trackable alert.
        /// </summary>
        public bool ActionRssTrackAlert
        {
            set
            {
                this["action.rss.track_alert"] = value;
            }
        }

        /// <summary>
        /// Sets the minimum time-to-live, in seconds, of the search artifacts 
        /// if this action is triggered. If a "p" follows the value, the value 
        /// is interpreted as periods, not seconds. The default is 10p. Note: 
        /// if no actions are triggered, the artifacts have their ttl determined
        /// by dispatch.ttl in savedsearches.conf.
        /// </summary>
        public string ActionRssTtl
        {
            set
            {
                this["action.rss.ttl"] = value;
            }
        }

        /// <summary>
        /// Sets the search command (or pipeline) which is responsible for 
        /// executing the action. Generally the command is a template search 
        /// pipeline which is realized with values from the saved search. To 
        /// reference saved search field values wrap them in $, for example to 
        /// reference the savedsearch name use $name$, to reference the search 
        /// use $search$.
        /// </summary>
        public string ActionScriptCommand
        {
            set
            {
                this["action.script.command"] = value;
            }
        }

        /// <summary>
        /// Sets the filename of the script to invoke. Required if script action 
        /// is enabled.
        /// </summary>
        public string ActionScriptFilename
        {
            set
            {
                this["action.script.filename"] = value;
            }
        }

        /// <summary>
        /// Sets the hostname used in the URL sent in email actions. This value 
        /// has two forms:
        /// <example>
        /// hostname (for example, splunkserver, splunkserver.example.com)
        /// </example>
        /// <example>
        /// protocol://hostname:port (like: http://splunkserver:8000, or
        /// https://splunkserver.example.com:443)
        /// </example>
        /// When set to a simple hostname, the protocol and port which are 
        /// configured within splunk are used to construct the base of the url.
        /// When set to 'http://...', it is used verbatim. NOTE: This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases when the Splunk server is not
        /// aware of how to construct an externally referencable url, such as 
        /// SSO environments, other proxies, or when the Splunk server hostname 
        /// is not generally resolvable. The default is the current hostname 
        /// provided by the operating system, or if that fails "localhost". When
        /// set to empty, default behavior is used.
        /// </summary>
        public string ActionScriptHostname
        {
            set
            {
                this["action.script.hostname"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of search results to sent via alerts.
        /// The defaults is 100.
        /// </summary>
        public int ActionScriptMaxResults
        {
            set
            {
                this["action.script.maxresults"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum amount of time the execution an action takes before 
        /// the action is aborted. Valid values are an integer followed by 
        /// m|s|h|d. The default is 5m.
        /// </summary>
        public string ActionScriptMaxTime
        {
            set
            {
                this["action.script.maxtime"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the execution of this action 
        /// signifies a trackable alert.
        /// </summary>
        public bool ActionScriptTrackAlert
        {
            set
            {
                this["action.script.track_alert"] = value;
            }
        }

        /// <summary>
        /// Sets the minimum time-to-live, in seconds, of the search artifacts 
        /// if this action is triggered. If a "p" follows the value, the value
        /// is interpreted as periods, not seconds. The default is 600 
        /// (10 minutes). Note: if no actions are triggered, the artifacts have 
        /// their ttl determined by dispatch.ttl in savedsearches.conf.
        /// </summary>
        public string ActionScriptTtl
        {
            set
            {
                this["action.script.ttl"] = value;
            }
        }

        /// <summary>
        /// Sets the name of the summary index where the results of the 
        /// scheduled search are saved. The defaults is "summary."
        /// </summary>
        public string ActtionSummaryIndexName
        {
            set
            {
                this["action.summary_index._name"] = value;
            }
        }

        /// <summary>
        /// Sets the search command (or pipeline) which is responsible for 
        /// executing the action. Generally the command is a template search 
        /// pipeline which is realized with values from the saved search. To 
        /// reference saved search field values wrap them in $, for example 
        /// to reference the savedsearch name use $name$, to reference the 
        /// search use $search$.
        /// </summary>
        public string ActionSummaryIndexCommand
        {
            set
            {
                this["action.summary_index.command"] = value;
            }
        }

        /// <summary>
        /// Sets the hostname used in the URL sent in email actions. This value 
        /// has two forms:
        /// <example>
        /// hostname (for example, splunkserver, splunkserver.example.com)
        /// </example>
        /// <example>
        /// protocol://hostname:port (like: http://splunkserver:8000, or
        /// https://splunkserver.example.com:443)
        /// </example>
        /// When set to a simple hostname, the protocol and port which are 
        /// configured within splunk are used to construct the base of the url.
        /// When set to 'http://...', it is used verbatim. NOTE: This means the 
        /// correct port must be specified if it is not the default port for 
        /// http or https. This is useful in cases when the Splunk server is not
        /// aware of how to construct an externally referencable url, such as 
        /// SSO environments, other proxies, or when the Splunk server hostname 
        /// is not generally resolvable. The default is the current hostname 
        /// provided by the operating system, or if that fails "localhost". When
        /// set to empty, default behavior is used.
        /// </summary>
        public string ActionSummaryIndexHostname
        {
            set
            {
                this["action.summary_index.hostname"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to execute the summary indexing 
        /// action as part of the scheduled search. NOTE: This option is 
        /// considered only if the summary index action is enabled and is marked
        /// as always executed (if counttype = always). The default is true.
        /// </summary>
        public bool ActionSummaryIndexInline
        {
            set
            {
                this["action.summary_index.inline"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of search results to sent via alerts.
        /// The defaults is 100.
        /// </summary>
        public int ActionSummaryIndexMaxResults
        {
            set
            {
                this["action.summary_index.maxresults"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum amount of time the execution an action takes before 
        /// the action is aborted. Valid values are an integer followed by
        /// m|s|h|d. The default is 5m.
        /// </summary>
        public string ActionSummaryIndexMaxTime
        {
            set
            {
                this["action.summary_index.maxtime"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the execution of this action 
        /// signifies a trackable alert.
        /// </summary>
        public bool ActionSummaryIndexTrackAlert
        {
            set
            {
                this["action.summary_index.track_alert"] = value;
            }
        }

        /// <summary>
        /// Sets the minimum time-to-live, in seconds, of the search artifacts 
        /// if this action is triggered. If a "p" follows the value, the value 
        /// is interpreted as periods, not seconds. The default is 600 
        /// (10 minutes). Note: if no actions are triggered, the artifacts have 
        /// their ttl determined by dispatch.ttl in savedsearches.conf.
        /// </summary>
        public string ActionSummaryIndexTtl
        {
            set
            {
                this["action.summary_index.ttl"] = value;
            }
        }

        /// <summary>
        /// Sets the actions to enable. This is a comma-separated list. For
        /// example "rss,email"
        /// </summary>
        public string Actions
        {
            set
            {
                this["actions"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether plunk applies the alert actions to 
        /// the entire result set or on each individual result. The default is
        /// true.
        /// </summary>
        public bool AlertDigestMode
        {
            set
            {
                this["alert.digest_mode"] = value;
            }
        }

        /// <summary>
        /// Sets the period of time to show the alert in the dashboard, in 
        /// seconds. The default is 24h. 
        /// </summary>
        public string AlertExpires
        {
            set
            {
                this["alert.expires"] = value;
            }
        }

        /// <summary>
        /// Sets the alert severity level. Valid values are numbers 1 through 6:
        /// 1=DEBUG, 2=INFO, 3=WARN, 4=ERROR, 5=SEVERE, 6=FATAL
        /// </summary>
        public int AlertSeverity
        {
            set
            {
                this["alert.severity"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether alert suppression is enabled for 
        /// this scheduled search.
        /// </summary>
        public bool AlertSuppress
        {
            set
            {
                this["alert.suppress"] = value;
            }
        }

        /// <summary>
        /// Sets the fields touse for supression when performing pre result 
        /// alerting. This is a comma-separated list. This setting is required
        /// if supporession is turned on and per result alerting is enabled.
        /// </summary>
        public string AlertSuppressFields
        {
            set
            {
                this["alert.suppress.fields"] = value;
            }
        }

        /// <summary>
        /// Sets the supression period. The valid format is [number][time-unit].
        /// For example "2d". This setting is only valid if alert.suppress is
        /// enabled.
        /// </summary>
        public string AlertSuppressPeriod
        {
            set
            {
                this["alert.suppress.period"] = value;
            }
        }

        /// <summary>
        /// Sets whether to track the actions triggered by this scheduled 
        /// search. Valid values are true, false, auto. These values are 
        /// interpretted as: 
        /// <list type="">
        /// <item>
        /// auto - determine whether to track or not based on the tracking 
        /// setting of each action, do not track scheduled searches that always
        /// trigger actions.
        /// </item>
        /// <item>
        /// true - force alert tracking.
        /// </item>
        /// <item>
        /// false - disable alert tracking for this search.
        /// </item>
        /// </list>
        /// </summary>
        public string AlertTrack
        {
            set
            {
                this["alert.track"] = value;
            }
        }

        /// <summary>
        /// Sets the comparator for alert triggering. valid strings are: 
        /// "greater than", "less than", "equal to", "rises by", "drops by", 
        /// "rises by perc", "drops by perc". Note: use in conjuction with 
        /// alert_threshold to trigger alert actions.
        /// </summary>
        public string AlertComparator
        {
            set
            {
                this["alert_comparator"] = value;
            }
        }

        /// <summary>
        /// Sets the conditional search that is evaluated against the results of
        /// the saved search. The default is an empty string. Alerts are 
        /// triggered if the specified search yields a non-empty search result 
        /// list.
        /// </summary>
        public string AlertCondition
        {
            set
            {
                this["alert_condition"] = value;
            }
        }

        /// <summary>
        /// Sets the value to compare (see alert_comparator) before triggering 
        /// alert actions. Valid form is a number followed by an optional
        /// percent sign. If expressed as a percentage, indicates value to use 
        /// when alert_comparator is set to "rises by perc" or "drops by perc."
        /// </summary>
        public string AlertThreshold
        {
            set
            {
                this["alert_threshold"] = value;
            }
        }

        /// <summary>
        /// Sets the alert trigger type. Valid values are, "always", "custom", 
        /// "number of events", "numberof hosts", number of sources". Note: 
        /// alert_condition overrides this. 
        /// </summary>
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
        /// Sets the cron schedule for this search. Any valid cron string is 
        /// acceptable. For example: */5 * * * * causes the search to execute 
        /// every 5 minutes. Note: It is recommended the cron schedules be 
        /// staggered to reduce system load.
        /// </summary>
        public string CronSchedule
        {
            set
            {
                this["cron_schedule"] = value;
            }
        }

        /// <summary>
        /// Sets the human-readable description of this saved search. Default 
        /// is an empty string
        /// </summary>
        public string Description
        {
            set
            {
                this["description"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the search is disabled. 
        /// </summary>
        public bool Disabled
        {
            set
            {
                this["disabled"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of timeline buckets.
        /// </summary>
        public int DispatchBuckets
        {
            set
            {
                this["dispatch.buckets"] = value;
            }
        }

        /// <summary>
        /// Sets the earliest time for this search. Note: the time string can 
        /// be a relative or absolute time. Also, if using an absolute time, use 
        /// dispatch.time_format to format the value.
        /// </summary>
        public string DipatchEarliestTime
        {
            set
            {
                this["dispatch.earliest_time"] = value;
            }
        }

        /// <summary>
        /// Sets the latest time for this search. Note: the time string can be a
        /// relative or absolute time. Aslo, if using an absolute time, use 
        /// dispatch.time_format to format the value.
        /// </summary>
        public string DipatchLatestTime
        {
            set
            {
                this["dispatch.latest_time"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether lookups are enabled for this search.
        /// </summary>
        public bool DispatchLookups
        {
            set
            {
                this["dispatch.lookups"] = value;
            }
        }

        /// <summary>
        /// Sets the  maximum number of results before finalizing the search. 
        /// The default is 500,000.
        /// </summary>
        public int DispatchMaxCount
        {
            set
            {
                this["dispatch.max_count"] = value;
            }
        }

        /// <summary>
        /// Sets thethe maximum amount of time, in seconds,  before finalizing 
        /// the search. The default is 500,000.
        /// </summary>
        public string DispatchMaxTime
        {
            set
            {
                this["dispatch.max_time"] = value;
            }
        }

        /// <summary>
        /// Sets how frequently Splunk runs the MapReduce reduce phase on 
        /// accumulated map values, in seconds. The default is 10.
        /// </summary>
        public int DispatchReduceFrequency
        {
            set
            {
                this["dispatch.reduce_freq"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to back fill the real time window
        /// for this search. This parameter valid only if this is a real time 
        /// search.
        /// </summary>
        public bool DispatchRealTimeBackfill
        {
            set
            {
                this["dispatch.rt_backfill"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether Splunk spawns a new search process 
        /// when this saved search is executed. Note: Searches against indexes 
        /// must run in a separate process.
        /// </summary>
        public bool DispatchSpawnSubprocess
        {
            set
            {
                this["dispatch.spawn_process"] = value;
            }
        }

        /// <summary>
        /// Sets the time format that Splunk uses to specify the earliest and
        /// latest time. The default is %FT%T.%Q%:z
        /// </summary>
        public string DispatchTimeFormat
        {
            set
            {
                this["dispatch.time_format"] = value;
            }
        }

        /// <summary>
        /// Sets the minimum time-to-live, in seconds, of the search artifacts 
        /// if this action is triggered. If a "p" follows the value, the value
        /// is interpreted as periods, not seconds. The default is 2p. Note: if 
        /// no actions are triggered, the artifacts have their ttl determined by 
        /// dispatch.ttl in savedsearches.conf.
        /// </summary>
        public string DispatchTtl
        {
            set
            {
                this["dispatch.ttl"] = value;
            }
        }

        /// <summary>
        /// Sets the default UI view name (not label) in which to load the 
        /// results. Accessibility is subject to the user having sufficient 
        /// permissions.
        /// </summary>
        public string DisplayView
        {
            set
            {
                this["displayview"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether this search is to be run on a 
        /// schedule.
        /// </summary>
        public bool IsScheduled
        {
            set
            {
                this["is_scheduled"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether this search should be listed in the 
        /// visible saved search list.
        /// </summary>
        public bool IsVisible
        {
            set
            {
                this["is_visible"] = value;
            }
        }

        /// <summary>
        /// Sets the maximum number of concurrent instances of this search the 
        /// scheduler is allowed to run. The default is 1.
        /// </summary>
        public int MaxConcurrent
        {
            set
            {
                this["max_concurrent"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the scheduler starts this search 
        /// relative to current time or last search execution time. If this 
        /// value is set to 1, the scheduler bases its determination of the next
        /// scheduled search execution time on the current time. If this value 
        /// is set to 0, the scheduler bases its determination of the next 
        /// scheduled search on the last search execution time. This is called 
        /// continuous scheduling. If set to 0, the scheduler never skips 
        /// scheduled execution periods. However, the execution of the saved 
        /// search might fall behind depending on the scheduler's load. Use 
        /// continuous scheduling whenever you enable the summary index option.
        /// If set to 1, the scheduler might skip some execution periods to make
        /// sure that the scheduler is executing the searches running over the 
        /// most recent time range. The scheduler tries to execute searches that
        /// have realtime_schedule set to 1 before it executes searches that 
        /// have continuous scheduling (realtime_schedule = 0).
        /// The default is 1. 
        /// </summary>
        public int RealtimeSchedule
        {
            set
            {
                this["realtime_schedule"] = value;
            }
        }

        /// <summary>
        /// Sets the field used by Splunk UI to denote the app this search 
        /// should be dispatched in.
        /// </summary>
        public string RequestUIDispatchApp
        {
            set
            {
                this["request.ui_dispatch_app"] = value;
            }
        }

        /// <summary>
        /// Sets the field used by Splunk UI to denote the view this search 
        /// should be displayed in.
        /// </summary>
        public string RequestUIDispatchView
        {
            set
            {
                this["request.ui_dispatch_view"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether to restart a real-time search 
        /// managed by the scheduler when a search peer becomes available for 
        /// this saved search. NOTE: The peer can be a newly added peer or a 
        /// peer that has been down and has become available.
        /// </summary>
        public bool RestartOnSearchPeerAdd
        {
            set
            {
                this["restart_on_searchpeer_add"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether this search runs when Splunk starts.
        /// If it does not run on startup, it runs at the next scheduled time.
        /// Splunk recommends that you set run_on_startup to true for scheduled 
        /// searches that populate lookup tables. The default is false.
        /// </summary>
        public bool RunOnStartup
        {
            set
            {
                this["run_on_startup"] = value;
            }
        }

        /// <summary>
        /// Sets  the viewstate id associated with the UI view listed in 
        /// 'displayview'. This must match up to a stanza in viewstates.conf.
        /// </summary>
        public string VSID
        {
            set
            {
                this["vsid"] = value;
            }
        }
    }
}