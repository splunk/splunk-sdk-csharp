using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using SplunkSDKHelper;

namespace Splunk.Examples.SharePointWebPart.IndexSummaryWebPart
{
    [ToolboxItemAttribute(false)]
    public partial class IndexSummaryWebPart : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public IndexSummaryWebPart()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get info on how 
            var cli = Command.Splunk();

            cli.AddRule("search", typeof(string), "search string");
            if (!cli.Opts.ContainsKey("search"))
            {
                System.Console.WriteLine(
                    "Search query string required, use --search=\"query\"");
                Environment.Exit(1);
            }

            var service = Service.Connect(cli.Opts);

            var outArgs = new JobResultsArgs
            {
                OutputMode = JobResultsArgs.OutputModeEnum.Xml,

                // Return all entries.
                Count = 0,
            };

            using (var stream = service.Oneshot(
                (string)cli.Opts["search"], outArgs))
            {
                using (var rr = new ResultsReaderXml(stream))
                {
                    foreach (var @event in rr)
                    {
                        System.Console.WriteLine("EVENT:");
                        foreach (string key in @event.Keys)
                        {
                            System.Console.WriteLine(
                                "   " + key + " -> " + @event[key]);
                        }
                    }
                }
            }
            this.IndexSummaryGridView.DataSource
               = new List<IndexSummaryItem>
                    {
                        new IndexSummaryItem
                            {
                                SourceType = "test",
                            }
                    };
            this.IndexSummaryGridView.DataBind();
        }
    }
}
