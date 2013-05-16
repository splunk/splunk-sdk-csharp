using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splunk.Examples.SharePointWebPart.IndexSummaryWebPart
{
    class IndexSummaryItem
    {
        public string SourceType
        {
            get; 
            set;
        }

        public string Source { get; set; }

        public string Host { get; set; }

        public string EventCount { get; set; }
    }
}
