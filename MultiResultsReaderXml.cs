using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splunk
{
    class MultiResultsReaderXml : MultiResultsReader<ResultsReaderXml>
    {
        public MultiResultsReaderXml(Stream stream) : base(stream)
        {
        }
    }
}
