# The Splunk Software Development Kit for C# 
### Version 1.0.1

The Splunk Software Development Kit (SDK) for C# contains library code and 
examples designed to enable developers to build applications using Splunk.

Splunk is a search engine and analytic environment that uses a distributed
map-reduce architecture to efficiently index, search, and process large 
time-varying data sets.

The Splunk product is popular with system administrators for aggregation and
monitoring of IT machine data, security, compliance and a wide variety of 
other scenarios that share a requirement to efficiently index, search, analyze,
and generate real-time notifications from large volumes of time series data.

The Splunk developer platform enables developers to take advantage of the 
same technology used by the Splunk product to build exciting new applications
that are enabled by Splunk's unique capabilities.

## Getting started with the Splunk SDK for C# 

The Splunk SDK for C# contains library code and examples that show how to 
programmatically interact with Splunk for a variety of scenarios including 
searching, saved searches, data inputs, and many more, along with building 
complete applications. 

The information in this Readme provides steps to get going quickly. In the 
future we plan to roll out more in-depth documentation.

### Requirements

Here's what you need to get going with the Splunk SDK for C#.

#### Splunk

If you haven't already installed Splunk, download it at 
<http://www.splunk.com/download>. For more information about installing and 
running Splunk and system requirements, see the
[Splunk Installation Manual](http://docs.splunk.com/Documentation/Splunk/latest/Installation). 

#### Splunk SDK for C# 

[Get the Splunk SDK for C#](http://dev.splunk.com/view/SP-CAAAEPP). Download 
the ZIP file and extract its contents.

If you are interested in contributing to the Splunk SDK for C#, you can 
[get it from GitHub](https://github.com/splunk/splunk-sdk-csharp) and clone the 
resources to your computer.

#### Visual Studio

The Splunk SDK for C# supports development in Microsoft Visual Studio 2012. The 
minimum supported version of the .NET Framework is version 3.5. Visual Studio 
downloads are available on the 
[Visual Studio Downloads webpage](http://www.microsoft.com/visualstudio/downloads).

### Building the SDK

Before starting to develop custom software, you must first build the SDK. Once 
you've downloaded and extracted—or cloned—the SDK, do the following:

1. At the root level of the **splunk-sdk-csharp** directory, open the 
**SplunkSDK.sln** file in Visual Studio.
2. On the **BUILD** menu, click **Build Solution**.

This will build the SDK, the examples, and the unit tests.

### Examples and unit tests

The Splunk SDK for C# includes several examples and unit tests that are run at 
the command prompt. 

#### Set up the .splunkrc file

To connect to Splunk, many of the SDK examples and unit tests take 
command-prompt arguments that specify values for the host, port, and login 
credentials for Splunk. For convenience during development, you can store these 
arguments as key-value pairs in a text file named **.splunkrc**. Then, when you 
don't specify these arguments at the command prompt, the SDK examples and unit 
tests use the values from the **.splunkrc** file.

**To use a .splunkrc file:**

First, create a new text file and save it as **.splunkrc**.

Windows might display an error when you try to name the file because 
".splunkrc" looks like a nameless file with an extension. You can use the 
console window to create this file by going to the **C:\Users\currentusername** 
directory and entering the following at the command prompt:
    
    Notepad.exe .splunkrc

A dialog box appears, asking whether you want to create a new file. Click 
**Yes**, and then continue creating the file.

In the new file, paste in the following. Be sure to customize any lines that 
apply to your Splunk instance.

	# Splunk host (default: localhost)
	host=localhost
	# Splunk admin port (default: 8089)
	port=8089
	# Splunk username
	username=admin
	# Splunk password
	password=changeme
	# Access scheme (default: https)
	scheme=https

Save the file in the current user's home directory—for instance:

	C:\Users\currentusername\.splunkrc

**Important:** Storing login credentials in the .splunkrc file is only for 
convenience during development&mdash;this file isn't part of the Splunk 
platform and shouldn't be used for storing user credentials for production. 
And, if you're at all concerned about the security of your credentials, just 
enter them at the command line rather than saving them in the .splunkrc file. 

#### Run examples

You can start getting familiar with the Splunk SDK for C# by running the 
examples that came with the SDK. Examples are located in the 
**\splunk-sdk-csharp\examples** directory. When you build the SDK, the examples 
are built as well. You can run the examples at the command prompt. 

This version of the Splunk SDK for C# contains the following examples:

Example         | Description
--------------- | -----------
authenticate    | Authenticates to the server and prints the received token.
list_apps       | Lists the apps installed on the server.
modular_input   | Creates a modular input executable that you can modify.
search          | Runs a normal search using a specified search query.
search_oneshot  | Runs a oneshot search using a specified search query.
search_realtime | Runs a real-time search, gets a number of snapshots, prints them, and then exits.
sharepoint_web_part | Shows you how to create a Splunk Web part for Microsoft SharePoint.
submit          | Submits events into Splunk.

**To run an example from the command line:**

At the command prompt, go to the **Debug** directory of the built example, 
for instance:
	
	\splunk-sdk-csharp\examples\examplename\bin\Debug

Run the example by typing its name, for instance:

	examplename.exe

For the examples that include search functionality, you will need to provide 
search criteria in the following form, where _query_syntax_ is a [valid search 
query](http://docs.splunk.com/Documentation/Splunk/latest/SearchReference/WhatsInThisManual):

	examplename.exe --search="query_syntax"

For instance, in the directory for the **search_oneshot** example, you can 
enter the following at the command prompt:

	search_oneshot.exe --search="search sourcetype="access_combined_wcookie" 10.2.1.44 | head 10"

This runs a oneshot search for sourcetype="access_combined_wcookie" 10.2.1.44 
(the IP address 10.2.1.44 and a sourcetype of access_combined_wcookie) and 
prints the first ten results to the console window.

#### Run unit tests

A great place to look for examples of how to use the Splunk SDK for C# is in 
the unit tests. These are the same tests that we used to validate the core SDK 
library. The unit tests are located in the **\splunk-sdk-csharp\UnitTests** 
directory. When you build the SDK, the unit tests are built as well.

Before you run the unit tests, you must specify the test settings file to use:

1. On the **TEST** menu, point to **Test Settings**, and then click **Select Test
Settings File**
2. In the **Open Settings File** dialog box, navigate to the root folder of the 
Splunk SDK for C#. 
3. Select the file named "Local.runsettings", and then click **Open**.

To run the unit tests in Visual Studio:

* On the **TEST** menu, point to **Run**, and then choose the option you want.

Alternately, you can use the Test Explorer in Visual Studio:

1. Click the **TEST** menu, point to **Windows**, and then click **Test 
Explorer**.
2. In the Test Explorer pane, click **Run All** to run all the tests, or choose 
a different option from the **Run** menu.

For more information about running unit tests, see the topic 
[Running Unit Tests with Test Explorer](http://msdn.microsoft.com/en-us/library/hh270865.aspx)
on the Microsoft Developer Network (MSDN).

## Repository
<table>

<tr>
<td><b>\examples</b></td>
<td>Examples demonstrating various SDK features</td>
</tr>

<tr>
<td><b>\lib</b></td>
<td>Third-party libraries used by the SDK</td>
</tr>

<tr>
<td><b>\SplunkSDK</b></td>
<td>Source for the SDK</td>
</tr>

<tr>
<td><b>\SplunkSDKHelper</b></td>
<td>Utilities shared by examples and unit tests</td>
</tr>

<tr>
<td><b>\UnitTests</b></td>
<td>Source for unit tests</td>
</tr>
</table> 

### Changelog

The **CHANGELOG.md** file in the root of the repository contains a description
of changes for each version of the SDK. You can also find it online at
[https://github.com/splunk/splunk-sdk-csharp/blob/master/CHANGELOG.md](https://github.com/splunk/splunk-sdk-csharp/blob/master/CHANGELOG.md). 

### Branches

The **master** branch always represents a stable and released version of the SDK.
You can read more about our branching model on our Wiki at 
[https://github.com/splunk/splunk-sdk-csharp/wiki/Branching-Model](https://github.com/splunk/splunk-sdk-java/wiki/Branching-Model).

## Documentation and resources

If you need to know more:

* For all things developer with Splunk, your main resource is the [Splunk
  Developer Portal](http://dev.splunk.com).

* For conceptual and how-to documentation, see the [Overview of the Splunk SDK
  for C#](http://dev.splunk.com/view/SP-CAAAEPK).

* For API reference documentation, see the [Splunk SDK for C# 
  Reference](http://docs.splunk.com/Documentation/CshrpSDK)

* For more about the Splunk REST API, see the [REST API 
  Reference](http://docs.splunk.com/Documentation/Splunk/latest/RESTAPI).

* For more about about Splunk in general, see [Splunk>Docs](http://docs.splunk.com/Documentation/Splunk).

* For more about this SDK's repository, see our 
  [GitHub Wiki](https://github.com/splunk/splunk-sdk-csharp/wiki/).
  
## Community

Stay connected with other developers building on Splunk.

<table>

<tr>
<td><em>Email</em></td>
<td><a href="mailto:devinfo@splunk.com">devinfo@splunk.com</a></td>
</tr>

<tr>
<td><em>Issues</em>
<td><a href="https://github.com/splunk/splunk-sdk-csharp/issues/">
https://github.com/splunk/splunk-sdk-csharp/issues</a></td>
</tr>

<tr>
<td><em>Answers</em>
<td><a href="http://splunk-base.splunk.com/tags/csharp/">
http://splunk-base.splunk.com/tags/csharp/</a></td>
</tr>

<tr>
<td><em>Blog</em>
<td><a href="http://blogs.splunk.com/dev/">http://blogs.splunk.com/dev/</a></td>
</tr>

<tr>
<td><em>Twitter</em>
<td><a href="http://twitter.com/splunkdev">@splunkdev</a></td>
</tr>

</table>

### Contributions

If you want to make a code contribution, go to the 
[Open Source](http://dev.splunk.com/view/opensource/SP-CAAAEDM)
page for more information.

### Support

1. You will be granted support if you or your company are already covered 
   under an existing maintenance/support agreement. Visit 
   <http://www.splunk.com/support> and click **Submit a Case** under **Contact
   a Support Engineer**.

2. If you are not covered under an existing maintenance/support agreement, you 
   can find help through the broader community at:
   * [Splunk Answers](http://splunk-base.splunk.com/answers/) (use the **sdk** and 
   **c#** tags to identify your questions)
   * [Splunkdev Google Group](http://groups.google.com/group/splunkdev)

3. Splunk will NOT provide support for SDKs if the core library (the 
   code in the **SplunkSDK** directory) has been modified. If you modify an SDK
   and want support, you can find help through the broader community and Splunk 
   answers (see above). We would also like to know why you modified the core 
   library&mdash;please send feedback to _devinfo@splunk.com_.
   
4. File any issues on [GitHub](https://github.com/splunk/splunk-sdk-csharp/issues).

### Contact Us

You can reach the Dev Platform team at devinfo@splunk.com.

## License

The Splunk SDK for C# is licensed under the Apache License 2.0. Details can be 
found in the LICENSE file.
