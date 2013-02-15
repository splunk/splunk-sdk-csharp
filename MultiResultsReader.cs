using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Splunk
{
    public class MultiResultsReader<T> : IEnumerable<ISearchResults>, IDisposable
        where T : ResultsReader<T>, IDisposable, new()
    {
        private T current;

        public MultiResultsReader(Stream inputStream) 
        {
            this.current = new T();
            this.current.Initialize(inputStream);
        }

        /// <summary>
        /// Returns an enumerator over the events in the event stream.
        /// </summary>
        /// <returns>An enumerator of events</returns>
        public IEnumerator<ISearchResults> GetEnumerator()
        {
            while (current.HasResults)
            {
                yield return current;

                var next = new T();
                next.TakeOver(current);
                current = next;
            } 
        }

        /// <summary>
        /// Returns an enumerator over the events in the event stream.
        /// </summary>
        /// <returns>An enumerator of events</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        void IDisposable.Dispose()
        {
            current.Dispose();
        }
    }
}