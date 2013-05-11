# Splunk SDK for C# Changelog

## Version 1.0.0.0 

### New APIs

* New classes have been added for creating a modular input. They are all under `Splunk.ModularInputs` namespace. An example of creating such a modular input is added.
* More specialized *args* classes have been added to make it easier to pass 
  operation-specific arguments:
    - `JobEventsArgs`
    - `JobExportArgs`
    - `JobResultsArgs`
    - `JobResultsPreviewArgs`
    - `SavedSearchDispatchArgs`
    - `ReceiverSubmitArgs`
        
    These new *args* classes are used with the following methods: 
     - `Service.Export` method
    - `Job.Results` method
    - `Job.ResultsPreview` method
    - `Job.Events` method
    - `SavedSearch.Dispatch` method
    - `Receiver.Submit` method

### New Supported Configuration

* The SDK now supports running with .NET Framework 3.5, in addition to .NET Framework 4.0 and .NET Framework 4.5. The SDK assembly's target framework is now .NET Framework 3.5. The Newtonsoft.Json assembly (used by `ResultsReaderJson` and `MultiResultsReaderJson`) is now Net35 version of Json.NET 5.0 Release 4.

### Breaking changes
* `JobArgs.ExecMode` has been renamed to `JobArgs.ExecutionMode`, and changed from string type to enum type.
* `JobArgs.ExecutionMode` and `JobArgs.SearchMode` have been changed to Enum types.
* `JobArgs.RemoteServerList` has been changed from string type to string array type.
* `SavedSearchArgs` class has been renamed to `SavedSearchDispatchArgs`.
* A few Atom parsing methods were changed from public to internal.

## Version 0.8.0.0 (beta)

### Minor updates

* Upgraded to Json.NET 4.5 (used by `ResultsReaderJson` and `MultiResultsReaderJson`) Release 11

* Removed obsolete methods

* Code cleanup

## Version 0.1.0.0 (preview)

### New APIs

* New classes have been added for reading search results,  `ResultsReaderXml` to read search results streams in XML output format. `MultiResultsReaderXml` and `MultiResultsReaderJson`, 
  to read search results streams with multiple result sets from `Service.Export` methods, and `Event` to support type conversions of event field values.

### Minor updates

* Events are returned from result reader classes though `IEnumerable<Event>`

### Bug fixes

* Fixed `Receiver.Attach` to support submitting events to Splunk through SSL.

## In Private 

Initial Splunk SDK for C# in private testing.
