/*
 * Copyright 2013 Splunk, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License"): you may
 * not use this file except in compliance with the License. You may obtain
 * a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 */

namespace UnitTests
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// Tests Inputs
    /// </summary>
    [TestClass]
    public class InputTest : TestHelper
    {
        /// <summary>
        /// Assert root string
        /// </summary>
        private static string assertRoot = "Input assert: ";

        /// <summary>
        /// Touch all the properties of all any input
        /// </summary>
        /// <param name="input">The Input</param>
        private void TouchSpecificInput(Input input)
        {
            InputKind inputKind = input.GetKind();
            TcpConnections tcpConnections = null;
            UdpConnections udpConnections = null;
            string[] dummyStrings;
            string dummyString;
            bool dummyBoolean;
            DateTime dummyDate;
            int dummyInt;

            if (inputKind.Equals(InputKind.Monitor))
            {
                MonitorInput monitorInput = (MonitorInput)input;
                dummyString = monitorInput.Blacklist;
                dummyString = monitorInput.CrcSalt;
                dummyInt = monitorInput.FileCount;
                dummyBoolean = monitorInput.FollowTail;
                dummyString = monitorInput.Host;
                dummyString = monitorInput.HostRegex;
                dummyString = monitorInput.IgnoreOlderThan;
                dummyString = monitorInput.Index;
                dummyString = monitorInput.Queue;
                dummyBoolean = monitorInput.IsRecursive;
                dummyString = monitorInput.Source;
                dummyString = monitorInput.SourceType;
                dummyInt = monitorInput.TimeBeforeClose;
                dummyString = monitorInput.Whitelist;
            }
            else if (inputKind.Equals(InputKind.Script))
            {
                ScriptInput scriptInput = (ScriptInput)input;
                dummyDate = scriptInput.EndTime;
                dummyString = scriptInput.Group;
                dummyString = scriptInput.Host;
                dummyString = scriptInput.Index;
                dummyString = scriptInput.Interval;
                dummyDate = scriptInput.StartTime;
            }
            else if (inputKind.Equals(InputKind.Tcp))
            {
                TcpInput tcpInput = (TcpInput)input;
                dummyString = tcpInput.ConnectionHost;
                dummyString = tcpInput.Group;
                dummyString = tcpInput.Host;
                dummyString = tcpInput.Index;
                dummyString = tcpInput.Queue;
                dummyString = tcpInput.RestrictToHost;
                dummyString = tcpInput.Source;
                dummyString = tcpInput.SourceType;
                dummyBoolean = tcpInput.SSL;
                tcpConnections = tcpInput.Connections();
                dummyString = tcpConnections.Connection;
                dummyString = tcpConnections.Servername;
            }
            else if (inputKind.Equals(InputKind.TcpSplunk))
            {
                TcpSplunkInput tcpSplunkInput = (TcpSplunkInput)input;
                dummyString = tcpSplunkInput.ConnectionHost;
                dummyString = tcpSplunkInput.Group;
                dummyString = tcpSplunkInput.Host;
                dummyString = tcpSplunkInput.Index;
                dummyString = tcpSplunkInput.Queue;
                dummyString = tcpSplunkInput.Source;
                dummyString = tcpSplunkInput.SourceType;
                dummyBoolean = tcpSplunkInput.SSL;
                tcpConnections = tcpSplunkInput.Connections();
                dummyString = tcpConnections.Connection;
                dummyString = tcpConnections.Servername;
            }
            else if (inputKind.Equals(InputKind.Udp))
            {
                UdpInput udpInput = (UdpInput)input;
                dummyString = udpInput.ConnectionHost;
                dummyString = udpInput.Group;
                dummyString = udpInput.Host;
                dummyString = udpInput.Index;
                dummyString = udpInput.Queue;
                dummyString = udpInput.Source;
                dummyString = udpInput.SourceType;
                dummyBoolean = udpInput.NoAppendingTimeStamp;
                dummyBoolean = udpInput.NoPriorityStripping;
                udpConnections = udpInput.Connections();
                dummyString = udpConnections.Group;
            }
            else if (inputKind.Equals(InputKind.WindowsActiveDirectory))
            {
                WindowsActiveDirectoryInput windowsActiveDirectoryInput = (WindowsActiveDirectoryInput)input;
                dummyString = windowsActiveDirectoryInput.Index;
                dummyBoolean = windowsActiveDirectoryInput.MonitorSubtree;
                dummyString = windowsActiveDirectoryInput.StartingNode;
                dummyString = windowsActiveDirectoryInput.TargetDc;
            }
            else if (inputKind.Equals(InputKind.WindowsEventLog))
            {
                WindowsEventLogInput windowsEventLogInput = (WindowsEventLogInput)input;
                dummyString = windowsEventLogInput.Hosts;
                dummyString = windowsEventLogInput.Index;
                dummyString = windowsEventLogInput.LocalName;
                dummyStrings = windowsEventLogInput.Logs;
                dummyString = windowsEventLogInput.LookupHost;
            }
            else if (inputKind.Equals(InputKind.WindowsPerfmon))
            {
                WindowsPerfmonInput windowsPerfmonInput = (WindowsPerfmonInput)input;
                dummyStrings = windowsPerfmonInput.Counters;
                dummyString = windowsPerfmonInput.Index;
                dummyStrings = windowsPerfmonInput.Instances;
                dummyInt = windowsPerfmonInput.Interval;
                dummyString = windowsPerfmonInput.Object;
            }
            else if (inputKind.Equals(InputKind.WindowsRegistry))
            {
                WindowsRegistryInput windowsRegistryInput = (WindowsRegistryInput)input;
                dummyBoolean = windowsRegistryInput.Baseline;
                dummyString = windowsRegistryInput.Hive;
                dummyString = windowsRegistryInput.Index;
                dummyBoolean = windowsRegistryInput.MonitorSubnodes;
                dummyString = windowsRegistryInput.Proc;
                dummyStrings = windowsRegistryInput.Type;
            }
            else if (inputKind.Equals(InputKind.WindowsWmi))
            {
                WindowsWmiInput windowsWmiInput = (WindowsWmiInput)input;
                dummyString = windowsWmiInput.Classes;
                dummyStrings = windowsWmiInput.Fields;
                dummyString = windowsWmiInput.Index;
                dummyStrings = windowsWmiInput.Instances;
                dummyInt = windowsWmiInput.Interval;
                dummyString = windowsWmiInput.LocalName;
                dummyString = windowsWmiInput.LookupHost;
                dummyString = windowsWmiInput.Servers;
                dummyString = windowsWmiInput.Wql;
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Touch test all the inputs in the system.
        /// </summary>
        [TestMethod]
        public void Inputs()
        {
            Service service = Connect();

            InputCollection inputs = service.GetInputs();
            string dummyString;
            InputKind dummyInputKind;

            // Iterate inputs and make sure we can read them.
            foreach (Input input in inputs.Values)
            {
                dummyString = input.Name;
                dummyString = input.Title;
                dummyString = input.Path;
                dummyInputKind = input.GetKind();
                this.TouchSpecificInput(input);
            }
        }

        /// <summary>
        /// Tests the Monitor Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void MonitorInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            ServiceInfo info = service.GetInfo();

            // CRUD Monitor input
            string filename;
            if (info.OsName.Equals("Windows"))
            {
                filename = "C:\\Windows\\WindowsUpdate.log"; // normally here
            }
            else if (info.OsName.Equals("Linux"))
            {
                filename = "/var/log/syslog";
            }
            else if (info.OsName.Equals("Darwin"))
            {
                filename = "/var/log/system.log";
            }
            else
            {
                throw new Exception("OS: " + info.OsName + " not supported");
            }

            if (inputCollection.ContainsKey(filename))
            {
                inputCollection.Remove(filename);
            }

            inputCollection.Create(filename, InputKind.Monitor);
            Assert.IsTrue(inputCollection.ContainsKey(filename));
            MonitorInput monitorInput = (MonitorInput)inputCollection.Get(filename);

            monitorInput.Blacklist = "phonyregex*1";
            monitorInput.CheckIndex = true;
            monitorInput.CheckPath = true;
            if (service.VersionCompare("4.2.1") >= 0)
            {
                monitorInput.CrcSalt = "ThisIsSalt";
                monitorInput.IgnoreOlderThan = "1d";
                monitorInput.TimeBeforeClose = 120;
            }

            monitorInput.FollowTail = false;
            monitorInput.Host = "three.four.com";
            monitorInput.HostRegex = "host*regex*";
            monitorInput.HostSegment = string.Empty;
            monitorInput.Index = "main";
            monitorInput.IsRecursive = false;
            monitorInput.RenameSource = "renamedSource";
            monitorInput.SourceType = "monitor";
            monitorInput.Whitelist = "phonyregex*2";
            String index = monitorInput.Index;
            monitorInput.Update();

            //monitorInput.Disable();
            // some attributes are write only; check what we can.
            Assert.AreEqual("phonyregex*1", monitorInput.Blacklist, assertRoot + "#1");
            Assert.IsFalse(monitorInput.FollowTail, assertRoot + "#2");
            Assert.AreEqual("three.four.com", monitorInput.Host, assertRoot + "#3");
            Assert.AreEqual("host*regex*", monitorInput.HostRegex, assertRoot + "#4");
            if (service.VersionCompare("4.2.1") >= 0)
            {
                Assert.AreEqual("1d", monitorInput.IgnoreOlderThan, assertRoot + "#4");
                Assert.AreEqual(120, monitorInput.TimeBeforeClose, assertRoot + "#5");
            }
            Assert.AreEqual("main", index, assertRoot + "#6");
            Assert.IsFalse(monitorInput.IsRecursive, assertRoot + "#7");
            Assert.AreEqual("renamedSource", monitorInput.Source, assertRoot + "#8");
            Assert.AreEqual("monitor", monitorInput.SourceType, assertRoot + "#9");
            Assert.AreEqual("phonyregex*2", monitorInput.Whitelist, assertRoot + "#10");

            monitorInput.Disable();

            monitorInput.Remove();
            inputCollection.Refresh();
            inputCollection.Refresh();
            Assert.IsFalse(inputCollection.ContainsKey(filename), assertRoot + "#10");
        }

        /// <summary>
        /// Tests the Script Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void ScriptInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            ServiceInfo info = service.GetInfo();

            // CRUD Script input
            string filename;
            if (info.OsName.Equals("Windows"))
            {
                filename = "echo.bat";
            }
            else
            {
                filename = "echo.sh";
            }

            Args args = new Args();
            args.Clear();
            args.Add("interval", "60");
            if (inputCollection.Get(filename) != null)
            {
                inputCollection.Remove(filename);
            }

            inputCollection.Create(filename, InputKind.Script, args);
            Assert.IsTrue(inputCollection.ContainsKey(filename), assertRoot + "#11");
            ScriptInput scriptInput = (ScriptInput)inputCollection.Get(filename);

            scriptInput.Host = "three.four.com";
            scriptInput.Index = "main";
            scriptInput.Interval = "120";
            if (service.VersionCompare("4.2.4") >= 0)
            {
                scriptInput.PassAuth = "admin";
            }

            scriptInput.RenameSource = "renamedSource";
            scriptInput.Source = "source";
            scriptInput.SourceType = "script";
            scriptInput.Update();

            Assert.AreEqual("three.four.com", scriptInput.Host, assertRoot + "#12");
            Assert.AreEqual("main", scriptInput.Index, assertRoot + "#13");
            Assert.AreEqual("120", scriptInput.Interval, assertRoot + "#14");
            if (service.VersionCompare("4.2.4") >= 0)
            {
                Assert.AreEqual("admin", scriptInput.PassAuth, assertRoot + "#15");
            }

            if (service.VersionCompare("5.0") >= 0)
            {
                Assert.AreEqual("source", scriptInput.Source, assertRoot + "#16");
            }
            else
            {
                Assert.AreEqual("renamedSource", scriptInput.Source, assertRoot + "#16");
            }

            Assert.AreEqual("script", scriptInput.SourceType, assertRoot + "#17");

            scriptInput.Remove();
            inputCollection.Refresh();
            Assert.IsFalse(inputCollection.ContainsKey(filename), assertRoot + "#18");
        }

        /// <summary>
        /// Tests the Windows Tcp (raw) Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void TcpInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            string port = "9999"; // test port

            // CRUD TCP (raw) input
            if (inputCollection.ContainsKey(port))
            {
                inputCollection.Remove(port);
                inputCollection.Refresh();
            }

            Assert.IsFalse(inputCollection.ContainsKey(port), assertRoot + "#19");

            inputCollection.Create(port, InputKind.Tcp);
            Assert.IsTrue(inputCollection.ContainsKey(port), assertRoot + "#20");
            TcpInput tcpInput = (TcpInput)inputCollection.Get(port);

            tcpInput.ConnectionHost = "dns";
            tcpInput.Host = "myhost";
            tcpInput.Index = "main";
            tcpInput.Queue = "indexQueue";
            if (service.VersionCompare("4.3") >= 0)
            {
                // Behavioral difference between 4.3 and earlier versions
                tcpInput.RawTcpDoneTimeout = 120;
            }
            tcpInput.Source = "tcp";
            tcpInput.SourceType = "sdk-tests";
            tcpInput.SSL = false;
            tcpInput.Update();

            Assert.AreEqual("dns", tcpInput.ConnectionHost, assertRoot + "#21");
            Assert.AreEqual("myhost", tcpInput.Host, assertRoot + "#22");
            Assert.AreEqual("main", tcpInput.Index, assertRoot + "#23");
            Assert.AreEqual("indexQueue", tcpInput.Queue, assertRoot + "#24");
            Assert.AreEqual("tcp", tcpInput.Source, assertRoot + "#25");
            Assert.AreEqual("sdk-tests", tcpInput.SourceType, assertRoot + "#26");
            Assert.IsFalse(tcpInput.SSL, assertRoot + "#27");

            tcpInput.Remove();
            inputCollection.Refresh();
            Assert.IsFalse(inputCollection.ContainsKey(port), assertRoot + "#28");
        }

        /// <summary>
        /// Tests the Windows Tcp (cooked) Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void TcpSplunkInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            string port = "9998"; // test port

            // CRUD TCP (cooked) input
            if (inputCollection.ContainsKey(port))
            {
                inputCollection.Remove(port);
                inputCollection.Refresh();
            }

            Assert.IsFalse(inputCollection.ContainsKey(port), assertRoot + "#29");

            inputCollection.Create(port, InputKind.TcpSplunk);
            Assert.IsTrue(inputCollection.ContainsKey(port), assertRoot + "#30");
            TcpSplunkInput tcpSplunkInput = (TcpSplunkInput)inputCollection.Get(port);

            tcpSplunkInput.ConnectionHost = "dns";
            tcpSplunkInput.Host = "myhost";
            tcpSplunkInput.SSL = false;
            bool getSSL = tcpSplunkInput.SSL;
            String getGroup = tcpSplunkInput.Group;
            tcpSplunkInput.Update();

            Assert.AreEqual("dns", tcpSplunkInput.ConnectionHost, assertRoot + "#31");
            Assert.AreEqual("myhost", tcpSplunkInput.Host, assertRoot + "#32");
            Assert.IsFalse(getSSL, assertRoot + "#33");
            Assert.AreEqual("listenerports", getGroup, "Expected the group of the TCP input to be listenerports");
            Assert.IsNull(tcpSplunkInput.Queue, "Expected the queue to initially be null");

            tcpSplunkInput.Remove();
            inputCollection.Refresh();
            Assert.IsFalse(inputCollection.ContainsKey(port), assertRoot + "#34");
        }

        /// <summary>
        /// Tests the Windows UDP Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void UdpInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            string port = "9997"; // test port

            // CRUD UDP input
            if (inputCollection.ContainsKey(port))
            {
                inputCollection.Remove(port);
                inputCollection.Refresh();
            }

            Assert.IsFalse(inputCollection.ContainsKey(port), assertRoot + "#34");

            inputCollection.Create(port, InputKind.Udp);
            Assert.IsTrue(inputCollection.ContainsKey(port), assertRoot + "#35");
            UdpInput udpInput = (UdpInput)inputCollection.Get(port);

            udpInput.ConnectionHost = "ip";
            udpInput.Host = "myhost";
            udpInput.Index = "main";
            udpInput.NoAppendingTimeStamp = true;
            udpInput.NoPriorityStripping = true;
            udpInput.Queue = "indexQueue";
            udpInput.Source = "mysource";
            udpInput.SourceType = "mysourcetype";
            udpInput.Update();

            Assert.AreEqual("ip", udpInput.ConnectionHost, assertRoot + "#36");
            Assert.AreEqual("myhost", udpInput.Host, assertRoot + "#37");
            Assert.AreEqual("main", udpInput.Index, assertRoot + "#38");
            Assert.IsTrue(udpInput.NoAppendingTimeStamp, assertRoot + "#39");
            Assert.IsTrue(udpInput.NoPriorityStripping, assertRoot + "#40");
            Assert.AreEqual("indexQueue", udpInput.Queue, assertRoot + "#41");
            Assert.AreEqual("mysource", udpInput.Source, assertRoot + "#42");
            Assert.AreEqual("mysourcetype", udpInput.SourceType, assertRoot + "#43");

            udpInput.Remove();
            inputCollection.Refresh();
            Assert.IsFalse(inputCollection.ContainsKey(port), assertRoot + "#44");
        }

        /// <summary>
        /// Tests the Windows Active Directory Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void WindowsActiveDirectoryInputCRUD()
        {
            // Need an Active Directory Domain Controller
        }

        /// <summary>
        /// Tests the Windows EventLog Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void WindowsEventLogInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            ServiceInfo info = service.GetInfo();

            if (info.OsName.Equals("Windows"))
            {
                string name = "sdk-input-wel";
                Args args = new Args();

                if (inputCollection.ContainsKey(name))
                {
                    inputCollection.Remove(name);
                    inputCollection.Refresh();
                }
                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#52");

                // CRUD Windows Event Log Input
                args.Add("lookup_host", service.Host);
                inputCollection.Create(name, InputKind.WindowsEventLog, args);
                Assert.IsTrue(inputCollection.ContainsKey(name), assertRoot + "#53");
                WindowsEventLogInput windowsEventLogInput = (WindowsEventLogInput)inputCollection.Get(name);

                windowsEventLogInput.Index = "main";
                windowsEventLogInput.LookupHost = service.Host;
                windowsEventLogInput.Hosts = "one.two.three,four.five.six";
                windowsEventLogInput.Update();

                Assert.AreEqual(service.Host, windowsEventLogInput.LookupHost, assertRoot + "#54");
                Assert.AreEqual("one.two.three,four.five.six", windowsEventLogInput.Hosts, assertRoot + "#55");
                Assert.AreEqual("main", windowsEventLogInput.Index, assertRoot + "#55");

                windowsEventLogInput.Remove();
                inputCollection.Refresh();
                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#56");
            }
        }

        /// <summary>
        /// Tests the Windows Perfmon Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void WindowsPerfmonInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            ServiceInfo info = service.GetInfo();

            if (info.OsName.Equals("Windows"))
            {
                string name = "sdk-input-wp";
                Args args = new Args();

                if (inputCollection.ContainsKey(name))
                {
                    inputCollection.Remove(name);
                    inputCollection.Refresh();
                }
                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#57");

                // CRUD Windows Perfmon Input
                args.Add("interval", 600);
                args.Add("object", "Server");
                inputCollection.Create(name, InputKind.WindowsPerfmon, args);
                Assert.IsTrue(inputCollection.ContainsKey(name), assertRoot + "#58");
                WindowsPerfmonInput windowsPerfmonInput = (WindowsPerfmonInput)inputCollection.Get(name);

                windowsPerfmonInput.Index = "main";
                windowsPerfmonInput.Counters = new string[] { "% Privileged Time" };
                windowsPerfmonInput.Instances = new string[] { "wininit" };
                windowsPerfmonInput.Object = "Process";
                windowsPerfmonInput.Interval = 1200;
                windowsPerfmonInput.Update();

                Assert.AreEqual(1, windowsPerfmonInput.Counters.Length, assertRoot + "#59");
                Assert.IsTrue(this.Contains(windowsPerfmonInput.Counters, "% Privileged Time"), assertRoot + "#60");
                Assert.AreEqual(windowsPerfmonInput.Index, "main", assertRoot + "#60.1");
                Assert.IsTrue(this.Contains(windowsPerfmonInput.Instances, "wininit"), assertRoot + "#61");
                Assert.AreEqual(1200, windowsPerfmonInput.Interval, assertRoot + "#62");
                Assert.AreEqual("Process", windowsPerfmonInput.Object, assertRoot + "#63");

                // set multi-series values and update.
                windowsPerfmonInput.Counters = new string[] { "% Privileged Time", "% User Time" };
                windowsPerfmonInput.Instances = new string[] { "smss", "csrss" };
                windowsPerfmonInput.Update();

                Assert.AreEqual(2, windowsPerfmonInput.Counters.Length, assertRoot + "#64");
                Assert.IsTrue(this.Contains(windowsPerfmonInput.Counters, "% Privileged Time"), assertRoot + "#65");
                Assert.IsTrue(this.Contains(windowsPerfmonInput.Counters, "% User Time"), assertRoot + "#66");

                Assert.AreEqual(2, windowsPerfmonInput.Instances.Length, assertRoot + "#67");
                Assert.IsTrue(this.Contains(windowsPerfmonInput.Instances, "smss"), assertRoot + "#68");
                Assert.IsTrue(this.Contains(windowsPerfmonInput.Instances, "csrss"), assertRoot + "#69");

                windowsPerfmonInput.Remove();
                inputCollection.Refresh();
                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#70");
            }
        }

        /// <summary>
        /// Tests the Windows Registry Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void WindowsRegistryInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            ServiceInfo info = service.GetInfo();

            if (info.OsName.Equals("Windows"))
            {
                string name = "sdk-input-wr";
                Args args = new Args();

                if (service.VersionCompare("4.3") < 0)
                {
                    return;
                }

                if (inputCollection.ContainsKey(name))
                {
                    inputCollection.Remove(name);
                    inputCollection.Refresh();
                }
                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#71");

                // CRUD Windows Registry Input
                args.Add("disabled", true);
                args.Add("baseline", false);
                args.Add("hive", "HKEY_USERS");
                args.Add("proc", "*");
                args.Add("type", "*");
                inputCollection.Create(name, InputKind.WindowsRegistry, args);
                Assert.IsTrue(inputCollection.ContainsKey(name), assertRoot + "#72");
                WindowsRegistryInput windowsRegistryInput =
                        (WindowsRegistryInput)inputCollection.Get(name);

                windowsRegistryInput.Index = "main";
                windowsRegistryInput.MonitorSubnodes = true;
                windowsRegistryInput.Update();

                Assert.IsFalse(windowsRegistryInput.Baseline, assertRoot + "#73");
                Assert.AreEqual("main", windowsRegistryInput.Index, assertRoot + "#74");

                // adjust a few of the arguments
                windowsRegistryInput.Type = new string[] { "create", "delete" };
                // touch the new Type (testing get after set)
                string[] foo = windowsRegistryInput.Type;
                windowsRegistryInput.Baseline = false;
                windowsRegistryInput.Update();

                Assert.AreEqual("*", windowsRegistryInput.Proc, assertRoot + "#75");
                Assert.IsTrue(windowsRegistryInput.Type.Contains("create"), assertRoot + "#76");
                Assert.IsTrue(windowsRegistryInput.Type.Contains("delete"), assertRoot + "#77");
                Assert.IsFalse(windowsRegistryInput.Baseline, assertRoot + "#78");

                windowsRegistryInput.Remove();
                inputCollection.Refresh();
                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#79");
            }
        }

        /// <summary>
        /// Tests the Windows WMI Create, Read, Update and Delete
        /// </summary>
        [TestMethod]
        public void WindowsWMIInputCRUD()
        {
            Service service = Connect();
            InputCollection inputCollection = service.GetInputs();
            ServiceInfo info = service.GetInfo();

            if (info.OsName.Equals("Windows"))
            {
                string name = "sdk-input-wmi";
                Args args = new Args();

                if (inputCollection.ContainsKey(name))
                {
                    inputCollection.Remove(name);
                    inputCollection.Refresh();
                }

                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#80");

                // CRUD Windows Wmi Input
                args.Add("classes", "PerfOS_Processor");
                args.Add("interval", 600);
                args.Add("lookup_host", service.Host);
                inputCollection.Create(name, InputKind.WindowsWmi, args);
                Assert.IsTrue(inputCollection.ContainsKey(name), assertRoot + "#81");
                WindowsWmiInput windowsWmiInput = (WindowsWmiInput)inputCollection.Get(name);

                Assert.AreEqual("Win32_PerfFormattedData_PerfOS_Processor", windowsWmiInput.Classes, assertRoot + "#82");
                Assert.AreEqual(600, windowsWmiInput.Interval, assertRoot + "#83");
                Assert.AreEqual(windowsWmiInput.LookupHost, service.Host, assertRoot + "#84");

                windowsWmiInput.Classes = "PerfDisk_LogicalDisk";
                windowsWmiInput.Fields = new string[] { "Caption" };
                windowsWmiInput.Index = "main";
                windowsWmiInput.Interval = 1200;
                windowsWmiInput.Instances = new string[] { "_Total" };
                windowsWmiInput.Servers = "host1.splunk.com,host2.splunk.com";
                windowsWmiInput.Update();

                Assert.AreEqual("Win32_PerfFormattedData_PerfDisk_LogicalDisk", windowsWmiInput.Classes, assertRoot + "#85");
                Assert.AreEqual(1, windowsWmiInput.Fields.Length, assertRoot + "#86");
                Assert.IsTrue(this.Contains(windowsWmiInput.Fields, "Caption"), assertRoot + "#87");
                Assert.AreEqual("main", windowsWmiInput.Index, assertRoot + "#88");
                Assert.AreEqual(1200, windowsWmiInput.Interval, assertRoot + "#89");
                Assert.AreEqual(1, windowsWmiInput.Instances.Length, assertRoot + "#90");
                Assert.IsTrue(this.Contains(windowsWmiInput.Instances, "_Total"), assertRoot + "#91");
                Assert.AreEqual("host1.splunk.com,host2.splunk.com", windowsWmiInput.Servers, assertRoot + "#92");

                // set list fields
                windowsWmiInput.Fields = new string[] { "Caption", "Description" };
                windowsWmiInput.Update();

                Assert.AreEqual(2, windowsWmiInput.Fields.Length, assertRoot + "#93");
                Assert.IsTrue(this.Contains(windowsWmiInput.Fields, "Caption"), assertRoot + "#94");
                Assert.IsTrue(this.Contains(windowsWmiInput.Fields, "Description"), assertRoot + "#95");

                windowsWmiInput.Remove();
                inputCollection.Refresh();
                Assert.IsFalse(inputCollection.ContainsKey(name), assertRoot + "#96");
            }
        }
    }
}
