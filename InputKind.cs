/*
 * Copyright 2012 Splunk, Inc.
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

namespace Splunk
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the set of specific Splunk input kinds.
    /// </summary>
    public class InputKind
    {
        /// <summary>
        /// The map of known inputs, aka relative paths.
        /// </summary>
        private static Dictionary<string, InputKind> knownRelativePaths = 
            new Dictionary<string, InputKind>();

        /// <summary>
        /// The kind of input.
        /// </summary>
        private string kind;

        /// <summary>
        /// The relative path from the inputs endpoint.
        /// </summary>
        private string relpath;

        /// <summary>
        /// The input class type.
        /// </summary>
        private Type inputClass;

        /// <summary>
        /// The basic, unknown input type.
        /// </summary>
        public static readonly InputKind Unknown = 
            new InputKind("Unknown", typeof(Input));

        /// <summary>
        /// The Monitor Input
        /// </summary>
        public static readonly InputKind Monitor = 
            new InputKind("monitor", typeof(MonitorInput));

        /// <summary>
        /// The Script Input
        /// </summary>
        public static readonly InputKind Script = 
            new InputKind("script", typeof(ScriptInput));

        /// <summary>
        /// The Raw TCP Input
        /// </summary>
        public static readonly InputKind Tcp = 
            new InputKind("tcp/raw", typeof(TcpInput));

        /// <summary>
        /// The Cooked TCP Input
        /// </summary>
        public static readonly InputKind TcpSplunk = 
            new InputKind("tcp/cooked", typeof(TcpSplunkInput));

        /// <summary>
        /// The UDP Input
        /// </summary>
        public static readonly InputKind Udp = 
            new InputKind("udp", typeof(UdpInput));

        /// <summary>
        /// The Windows Active Directory Input
        /// </summary>
        public static readonly InputKind WindowsActiveDirectory = 
            new InputKind("ad", typeof(WindowsActiveDirectoryInput));

        /// <summary>
        /// The Windows Event Log Input
        /// </summary>
        public static readonly InputKind WindowsEventLog = 
            new InputKind(
                "win-event-log-collections", typeof(WindowsEventLogInput));

        /// <summary>
        /// The Windows Performance Monitor Input
        /// </summary>
        public static readonly InputKind WindowsPerfmon = 
            new InputKind("win-perfmon", typeof(WindowsPerfmonInput));

        /// <summary>
        /// The Windows Registry Input
        /// </summary>
        public static readonly InputKind WindowsRegistry =
            new InputKind("registry", typeof(WindowsRegistryInput));

        /// <summary>
        /// The Windows WMI Input
        /// </summary>
        public static readonly InputKind WindowsWmi = 
            new InputKind("win-wmi-collections", typeof(WindowsWmiInput));

        /// <summary>
        /// Initializes a new instance of the <see cref="InputKind"/> class. 
        /// </summary>
        /// <param name="relpath">The relative input path</param>
        /// <param name="inputClass">The input class</param>
        public InputKind(string relpath, Type inputClass)
        {
            this.kind = relpath;
            this.relpath = relpath;
            this.inputClass = inputClass;

            knownRelativePaths.Add(relpath, this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputKind"/> class. 
        /// </summary>
        /// <param name="relpath">The relative input path</param>
        /// <param name="inputClass">The input class</param>
        /// <param name="kind">The kind</param>
        public InputKind(string relpath, Type inputClass, string kind)
        {
            this.kind = kind;
            this.relpath = relpath;
            this.inputClass = inputClass;

            knownRelativePaths.Add(relpath, this);
        }

        /// <summary>
        /// Gets the IEnumerable list of Inputs
        /// </summary>
        public static IEnumerable<InputKind> Values
        {
            get
            {
                yield return Unknown;
                yield return Monitor;
                yield return Script;
                yield return Tcp;
                yield return TcpSplunk;
                yield return Udp;
                yield return WindowsActiveDirectory;
                yield return WindowsEventLog;
                yield return WindowsPerfmon;
                yield return WindowsRegistry;
                yield return WindowsWmi;
            }
        }

        /// <summary>
        /// Gets the kind of this input
        /// </summary>
        public string Kind
        {
            get
            {
                return this.kind;
            }
        }

        /// <summary>
        /// Gets the relative path of this input
        /// </summary>
        public string RelPath
        {
            get
            {
                return this.relpath;
            }
        }

        /// <summary>
        /// Gets the class type of this input
        /// </summary>
        public Type InputClass
        {
            get
            {
                return this.inputClass;
            }
        }

        /// <summary>
        /// Creates an input.
        /// </summary>
        /// <param name="relPath">The relative path</param>
        /// <returns>The input kind</returns>
        public static InputKind Create(string relPath)
        {
            if (knownRelativePaths.ContainsKey(relPath))
            {
                return knownRelativePaths[relPath];
            }
            else
            {
                return new InputKind(relPath, typeof(Input));
            }
        }
    }
}