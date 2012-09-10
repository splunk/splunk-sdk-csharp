# The Splunk Software Development Kit for C#
### Version 0.1.0 (Private Release)

This SDK contains library code and examples designed to enable developers to
build applications using Splunk.

Splunk is a search engine and analytic environment that uses a distributed
map-reduce architecture to efficiently index, search and process large 
time-varying data sets.

The Splunk product is popular with system administrators for aggregation and
monitoring of IT machine data, security, compliance and a wide variety of 
other scenarios that share a requirement to efficiently index, search, analyze
and generate real-time notifications from large volumes of time series data.

The Splunk developer platform enables developers to take advantage of the 
same technology used by the Splunk product to build exciting new applications
that are enabled by Splunk's unique capabilities.

## License

The Splunk C# SDK is licensed under the Apache License 2.0. Details can be 
found in the LICENSE file.

1.  The Apache License only applies to the Splunk C# SDK and no other Software
    provided by Splunk.

2.  Splunk, in using the Apache License, does not provide any warranties or 
    indemnification, and does not accept any liabilities with the release of 
    the SDK.

3.  We are accepting contributions from individuals and companies to our 
    Splunk open source projects. See the 
    [Open Source](http://dev.splunk.com/view/opensource/SP-CAAAEDM) page for 
    more information.

## Getting started with the Splunk C# SDK

The Splunk C# SDK contains library code and examples that show how to 
programmatically interact with Splunk for a variety of scenarios including 
searching, saved searches, data inputs, and many more, along with building 
complete applications. 

The information in this Readme provides steps to get going quickly. In the 
future we will roll out more in depth documentation.

### Requirements

Here's what you need to get going with the Splunk C# SDK.

#### Splunk

If you haven't already installed Splunk, download it here: 
http://www.splunk.com/download. For more about installing and running Splunk 
and system requirements, see Installing & Running Splunk 
(http://dev.splunk.com/view/SP-CAAADRV). 

#### Splunk C# SDK

Get the Splunk C# from GitHub (https://github.com/) and clone the 
resources to your computer. For example, use the following command: 

>  git clone https://github.com/splunk/splunk-sdk-csharp.git

#### Visual Studio

Visual Studio 2010 and Visual Studio 2012 are both supported. The minimum
framework version supported is 4.0. Visual Studio downloads are available 
here: http://www.microsoft.com/visualstudio/11/en-us/downloads

### Building the SDK

Before starting to develop custom software, you must first build the SDK.

### Examples and unit tests

The Splunk C# SDK includes several examples and unit tests that are run at 
the command line. 


#### Set up the .splunkrc file

To connect to Splunk, many of the SDK examples and unit tests take command-
line arguments that specify values for the host, port, and login credentials 
for Splunk. For convenience during development, you can store these arguments 
as key-value pairs in a text file named .splunkrc. Then, when you don't 
specify these arguments at the command line, the SDK examples and unit tests 
use the values from the .splunkrc file. 

To use a .splunkrc file, create a text file with the following format:

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
    scheme=https
    # Application context (OPTIONAL)
    app=MyApp
    # Owner context (OPTIONAL)
    owner=User1

Save the file as .splunkrc in the current user's home directory.

*   On Windows, save the file as: 

    >  C:\Users\currentusername\\.splunkrc

    You might get errors in Windows when you try to name the file because
    ".splunkrc" looks like a nameless file with an extension. You can use
    the command line to create this file--go to the 
    C:\Users\currentusername directory and enter the following command: 

    >  Notepad.exe .splunkrc

    Click Yes, then continue creating the file.

NOTE: Storing login credentials in the .splunkrc file is only for convenience 
during development—this file isn't part of the Splunk platform and 
shouldn't be used for storing user credentials for production. And, if you're 
at all concerned about the security of your credentials, just enter them at 
the command line rather than saving them in the .splunkrc file. 


#### Run unit tests

[TBD] 

## Repository

[TBD] 

### Changelog

[TBD] 

### Branches

[TBD] 

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

1. You will be granted support if you or your company are already covered 
   under an existing maintenance/support agreement. Send an email to 
   support@splunk.com and please include the SDK you are referring to in the 
   subject. 

2. If you are not covered under an existing maintenance/support agreement you 
   can find help through the broader community at:

   <ul>
<li><a href='http://splunk-base.splunk.com/answers/'>Splunk Answers</a> - tags SDK, java, python, javascript are available to identify your questions</li>
<li><a href='http://groups.google.com/group/splunkdev'>Splunkdev Google Group</a></li>
</ul>

3. Splunk will NOT provide support for SDKs if the core library (this is the 
   code in the `splunk` directory) has been modified. If you modify an SDK and 
   want support, you can find help through the broader community and Splunk 
   answers (see above). We also want to know about why you modified the core 
   library. You can send feedback to: devinfo@splunk.com

### Contact Us

You can reach the Dev Platform team at devinfo@splunk.com.

