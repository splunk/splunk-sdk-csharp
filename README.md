# The Splunk Software Development Kit for C# 
### Version 0.1.0 (Public Preview)

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
running Splunk and system requirements, see 
[Installing & Running Splunk](http://dev.splunk.com/view/SP-CAAADRV). 

#### Splunk SDK for C# 

Get the Splunk SDK for C# from [GitHub](http://www.github.com/) and clone the 
resources to your computer. For example, use the following command: 

	git clone https://github.com/splunk/splunk-sdk-csharp.git
	
You can also download the SDK as a ZIP file.

#### Visual Studio

The Splunk SDK for C# supports development in both Microsoft Visual Studio 2010 
and Visual Studio 2012. The minimum supported version of the .NET Framework
is version 4.0. Visual Studio downloads are available on the 
[Visual Studio Downloads webpage](http://www.microsoft.com/visualstudio/downloads).

### Building the SDK

Before starting to develop custom software, you must first build the SDK. 

1. At the root level of the **splunk-sdk-csharp** directory, open the **SplunkSDK.sln** file in Visual Studio.
2. On the **BUILD** menu, click **Build Solution**, or press F7.

This will build the SDK, the examples, and the unit tests.

### Examples and unit tests

The Splunk SDK for C# includes several examples and unit tests that are run at 
the command line. 

#### Set up the .splunkrc file

To connect to Splunk, many of the SDK examples and unit tests take command-
line arguments that specify values for the host, port, and login credentials 
for Splunk. For convenience during development, you can store these arguments 
as key-value pairs in a text file named **.splunkrc**. Then, when you don't 
specify these arguments at the command line, the SDK examples and unit tests 
use the values from the **.splunkrc** file. 

To use a **.splunkrc** file, create a text file with the following format. Be sure to 
uncomment and customize any lines that apply to your Splunk instance.

    # Host at which Splunk is reachable (OPTIONAL)
    host=localhost
    # Port at which Splunk is reachable (OPTIONAL)
    # Use the admin port, which is 8089 by default.
    port=8089
    # Splunk username
    username=admin
    # Splunk password
    password=changeme
    # Access scheme (OPTIONAL)
    # scheme=https
    # Application context (OPTIONAL)
    # app=MyApp
    # Owner context (OPTIONAL)
    # owner=User1

Save the file as **.splunkrc** in the current user's home directory&mdash;for instance:

    C:\Users\currentusername\.splunkrc

You might get errors in Windows when you try to name the file because ".splunkrc" looks 
like a nameless file with an extension. You can use the command line to create this 
file by going to the **C:\Users\currentusername** directory and entering the following 
command: 
    
    Notepad.exe .splunkrc

A dialog box appears, asking whether you want to create a new file. Click **Yes**, 
and then continue creating the file.

**Note**: Storing login credentials in the .splunkrc file is only for convenience 
during development—this file isn't part of the Splunk platform and 
shouldn't be used for storing user credentials for production. And, if you're 
at all concerned about the security of your credentials, just enter them at 
the command line rather than saving them in the .splunkrc file. 


#### Run unit tests

First, build the whole solution. 
To run all tests, follow the [instructions on MSDN](http://msdn.microsoft.com/en-us/library/hh270865.aspx#BKMK_Running_tests).
To run a single test case or all tests within a test class, right click with the cursor on the test method or the test class,
and invoke the 'Run Tests' context menu item.


## Repository
<table>

<tr>
<td><b>/SplunkSDKHelper</b></td>
<td>Utilities shared by examples and unit tests</td>
</tr>

<tr>
<td><b>/examples</b></td>
<td>Examples demonstrating various SDK features</td>
</tr>

<tr>
<td><b>/lib</b></td>
<td>Third-party libraries used by the SDK</td>
</tr>

<tr>
<td><b>/SplunkSDK</b></td>
<td>Source for the SDK</td>
</tr>

<tr>
<td><b>/UnitTests</b></td>
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
[https://github.com/splunk/splunk-sdk-java/wiki/Branching-Model](https://github.com/splunk/splunk-sdk-java/wiki/Branching-Model).

## Resources

You can find anything having to do with developing on Splunk at the Splunk
developer portal:

* http://dev.splunk.com

You can also find reference documentation for the REST API:

* http://docs.splunk.com/Documentation/Splunk/latest/RESTAPI

For an introduction to the Splunk product and some of its capabilities:

* http://docs.splunk.com/Documentation/Splunk/latest/User/SplunkOverview

## Community

Stay connected with other developers building on Splunk.

<table>

<tr>
<td><em>Email</em></td>
<td>devinfo@splunk.com</td>
</tr>

<tr>
<td><em>Blog</em>
<td><span>http://blogs.splunk.com/dev/</span></td>
</tr>

<tr>
<td><em>Twitter</em>
<td>@splunkdev</td>
</tr>

</table>

### Contributions

If you want to make a code contribution, go to the 
[Open Source](http://dev.splunk.com/view/opensource/SP-CAAAEDM)
page for more information.

### Support

* SDKs in Preview are not Splunk supported. Once the Splunk SDK for C# becomes an Open Beta we will provide more detail on support.

* Issues should be filed here: [https://github.com/splunk/splunk-sdk-php/issues](https://github.com/splunk/splunk-sdk-php/issues)

### Contact Us

You can reach the Dev Platform team at devinfo@splunk.com.

## License

The Splunk SDK for C# is licensed under the Apache License 2.0. Details can be 
found in the LICENSE file.

1.  The Apache License only applies to the Splunk SDK for C# and no other 
    Software provided by Splunk.

2.  Splunk, in using the Apache License, does not provide any warranties or 
    indemnification, and does not accept any liabilities with the release of 
    the SDK.

3.  We are accepting contributions from individuals and companies to our 
    Splunk open source projects. See the 
    [Open Source](http://dev.splunk.com/view/opensource/SP-CAAAEDM) page for 
    more information.
