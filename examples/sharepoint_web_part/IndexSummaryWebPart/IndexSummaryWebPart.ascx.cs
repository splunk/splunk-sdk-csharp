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

namespace Splunk.Examples.SharePointWebPart.IndexSummaryWebPart
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Web.UI.WebControls.WebParts;
    using SplunkSDKHelper;

    /// <summary>
    /// Partial class that contains custom code for 
    /// getting index summary data from Splunk.
    /// </summary>
    [ToolboxItemAttribute(false)]
    public partial class IndexSummaryWebPart : WebPart
    {
        /// <summary>
        /// There's no change to this method after being generated
        /// by Visual Studio.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        /// <summary>
        /// Binds search results to GridView.
        /// </summary>
        /// <param name="sender">A sender</param>
        /// <param name="e">Event arguments</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Load connection info for Splunk server in .splunkrc file,
            var cli = Command.Splunk();

            var service = Service.Connect(cli.Opts);

            const string Search =
                "search index=\"oidemo\" sourcetype=\"business_event\" networkProviderName=* planType=\"*\" orderType=\"*\"  | bin _time span=1m  | stats count by _time, marketCity, orderType, networkProviderName, phoneName, phoneType, planDescription, planPrice, planType | sort  limit=10 -planPrice*count";

            var outArgs = new JobResultsArgs
                {
                    OutputMode = JobResultsArgs.OutputModeEnum.Xml,
                };

            using (var stream = service.Oneshot(
                Search, 
                outArgs))
            {
                using (var results = new ResultsReaderXml(stream))
                {
                    var summary = from @event in results
                                    let s = @event.ToDictionary(
                                        r => r.Key,
                                        // Convert event field values to string
                                        // type so that GridView can generate
                                        // columns for them.
                                        r => (string) r.Value)
                                    select new
                                        {
                                            Time = s["_time"],
                                            MarketCity = s["marketCity"],
                                            OrderType = s["orderType"],
                                            NetworkProviderName = s["networkProviderName"],
                                            PhoneName = s["phoneName"],
                                            PhoneType = s["phoneType"],
                                            PlanDescription = s["planDescription"],
                                            PlanPrice = s["planPrice"],
                                            PlanType = s["planType"],
                                        };
                    this.IndexSummaryGridView.DataSource = summary;
                    this.IndexSummaryGridView.DataBind();
                }
            }
        }
    }
}
