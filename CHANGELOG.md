# Splunk SDK for C# Changelog

## Version 0.1.0 (preview)

### New APIs
* `ResultsReaderXml` class
* `MultiResultsReaderXml` class
* `MultiResultsReaderJson` class
* `Event` class

### Minor updates

* Events are returned from result reader classes though `IEnumerable<Event>`

### Bug fixes

* Fixed `Receiver.Attach` to support submitting events to Splunk through SSL.

## In Private 

Initial Splunk SDK for C# in private testing.
