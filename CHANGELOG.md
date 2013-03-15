# Splunk SDK for C# Changelog

## Version 0.1.0 (preview)

### New APIs

New classes have been added for reading search results,  `ResultsReaderXml` to read search results streams in XML output format. `MultiResultsReaderXml` and `MultiResultsReaderJson`, 
  to read search results streams with multiple result sets from `Service.Export` methods, and `Event` to support type conversions of event field values.

### Minor updates

* Events are returned from result reader classes though `IEnumerable<Event>`

### Bug fixes

* Fixed `Receiver.Attach` to support submitting events to Splunk through SSL.

## In Private 

Initial Splunk SDK for C# in private testing.
