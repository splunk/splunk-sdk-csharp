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
    using System.Collections;
    using System.Collections.Generic;
    using System.Net;

    /// <summary>
    /// Represents the Input Collection class. 
    /// </summary>
    /// <typeparam name="T">The Input class and its derived classes</typeparam>
    public class InputCollection : EntityCollection<Input> 
    {
        protected List<InputKind> inputKinds = new List<InputKind>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        public InputCollection(Service service)
            : base(service, "data/inputs")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InputCollection"/> 
        /// class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="args">The parameters</param>
        public InputCollection(Service service, Args args)
            : base(service, "data/inputs", args)
        {
        }

        private List<InputKind> AssembleInputKindList(ArrayList subPath)
        {
            List<InputKind> kinds = new List<InputKind>();
            ResponseMessage response =
                this.Service.Get(this.Path + "/" + Util.Join("/", subPath));
            AtomFeed feed = AtomFeed.Parse(response.Content);
            foreach (AtomEntry entry in feed.Entries)
            {
                string itemKeyName = ItemKey(entry);
                bool hasCreateLink = entry.Links.ContainsKey("create");

                ArrayList thisSubPath = new ArrayList(subPath);
                thisSubPath.Add(itemKeyName);

                string relPath = Util.Join("/", thisSubPath);

                if (relPath.Equals("all") || relPath.Equals("tcp/ssl"))
                {
                    continue;
                }
                else if (hasCreateLink)
                {
                    InputKind newKind = InputKind.Create(relPath);
                    kinds.Add(newKind);
                }
                else
                {
                    List<InputKind> subKinds =
                        this.AssembleInputKindList(thisSubPath);
                    kinds.AddRange(subKinds);
                }
            }

            return kinds;
        }

        /// <summary>
        /// Returns a value whether or not this key in in the current list of 
        /// Inputs.
        /// </summary>
        /// <param name="key">The name</param>
        /// <returns>True or false</returns>
        public override bool ContainsKey(object key)
        {
            Input input = this.RetrieveInput((string)key);
            return input != null;
        }
        
        /// <summary>
        /// A stub for create -- which cannot be done this way.
        /// </summary>
        /// <param name="name">The name</param>
        /// <returns>Will not return, but throw an exception</returns>
        public override Input Create(string name)
        {
            throw new Exception("can't create an input here");
        }

        /// <summary>
        /// A stub for create -- which cannot be done this way.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="args">The args</param>
        /// <returns>Will not return, but throw an exception</returns>
        public override Input Create(string name, Args args)
        {
            throw new Exception("can't create an input here");
        }

        /// <summary>
        /// Creates a specific kind of Input.
        /// </summary>
        /// <param name="name">The Input name</param>
        /// <param name="kind">The kind of Input to create</param>
        /// <returns>The new Input object</returns>
        public Input Create(string name, InputKind kind)
        {
            return this.Create(name, kind, (Dictionary<string, object>)null);
        }

        /// <summary>
        /// Creates a specific kind of Input.
        /// </summary>
        /// <param name="name">The Input name</param>
        /// <param name="kind">The kind of Input to create</param>
        /// <param name="args">The new Input object</param>
        /// <returns>The input</returns>
        public Input 
            Create(string name, InputKind kind, Dictionary<string, object> args)
        {
            args = Args.Create(args);
            args.Add("name", name);
            string path = this.Path + "/" + kind.RelPath;
            this.Service.Post(path, args);
            this.Invalidate();
            return this.Get(name);
        }

        /// <summary>
        /// Creates an Input resource item.
        /// </summary>
        /// <param name="entry">The Atom data representing the object</param>
        /// <returns>The Input object</returns>
        protected override Input CreateItem(AtomEntry entry)
        {
            string path = this.ItemPath(entry);
            InputKind kind = this.ItemKind(path);
            Type inputClass = kind.InputClass;
            return this.CreateItem(inputClass, path, null);
        }

        /// <summary>
        /// Returns an Input from the current list of inputs.
        /// </summary>
        /// <param name="key">The key</param>
        /// <returns>The Input</returns>
        public override Input Get(object key)
        {
            return this.RetrieveInput((string)key);
        }

        /// <summary>
        /// Returns a scoped, namespace-constrained Input from the current list
        /// of inputs.
        /// </summary>
        /// <param name="key">The name</param>
        /// <param name="splunkNamespace">The namespace</param>
        /// <returns>The input</returns>
        public override Input Get(object key, Args splunkNamespace)
        {
            Util.EnsureNamespaceIsExact(splunkNamespace);
            return this.RetrieveInput((string)key, splunkNamespace);
        }

        /// <summary>
        /// Returns the path's InputKind value.
        /// </summary>
        /// <param name="path">The path</param>
        /// <returns>The input kind</returns>
        protected InputKind ItemKind(string path)
        {
            string relpathWithInputName = 
                Util.SubstringAfter(path, "/data/inputs/", null);
            foreach (InputKind kind in inputKinds)
            {
                if (relpathWithInputName.StartsWith(kind.RelPath)) 
                {
                    return kind;
                }
            }
            return InputKind.Unknown; // Didn't recognize the input kind
        }

        private static bool 
            MatchesInputName(InputKind kind, string searchFor, string searchIn)
        {
            if (kind == InputKind.Script)
            {
                return searchIn.EndsWith("/" + searchFor) ||
                       searchIn.EndsWith("\\" + searchFor);
            }

            return searchFor.Equals(searchIn);
        }

        /// <summary>
        /// Refreshes this input collection
        /// </summary>
        /// <returns>The input collection</returns>
        public override Resource Refresh()
        {
            this.RefreshInputKinds();
            this.Items.Clear();

            // Iterate over all input kinds and collect all instances.
            foreach (InputKind kind in inputKinds)
            {
                string relpath = kind.RelPath;
                string inputs = 
                    string.Format("{0}/{1}?count=-1", this.Path, relpath);
                ResponseMessage response = null;
                try
                {
                    response = this.Service.Get(inputs);
                }
                catch (WebException ex)
                {
                    if (ex.Status == WebExceptionStatus.ProtocolError && 
                        ex.Response != null)
                    {
                        var resp = (HttpWebResponse)ex.Response;
                        if (resp.StatusCode == HttpStatusCode.NotFound)
                        {
                            continue; // silently ignore 
                        }
                    }
                    else
                    {
                        throw;
                    }
                }
                AtomFeed feed = AtomFeed.Parse(response.Content);
                base.Load(feed);
            }

            return this;
        }

        private void RefreshInputKinds()
        {
            List<InputKind> kinds = 
                this.AssembleInputKindList(new ArrayList());

            this.inputKinds.Clear();
            foreach (InputKind kind in kinds) {
                this.inputKinds.Add(kind);
            }
        }
        /// <summary>
        /// Removes an Input from the collection
        /// </summary>
        /// <param name="key">The name of the input</param>
        /// <returns>The input that was removed</returns>
        public override Input Remove(string key)
        {
            Input input = this.RetrieveInput(key);
            if (input != null)
            {
                input.Remove();
            }
            return input;
        }

        /// <summary>
        /// Removes a scoped, namespace-constrained Input from the collection
        /// </summary>
        /// <param name="key">The name of the input</param>
        /// <param name="splunkNamespace">The namespace</param>
        /// <returns>The input</returns>
        public override Input Remove(string key, Args splunkNamespace)
        {
            Util.EnsureNamespaceIsExact(splunkNamespace);
            Input input = this.RetrieveInput(key, splunkNamespace);
            if (input != null)
            {
                input.Remove();
            }
            return input;
        }

        /// <summary>
        /// Retrieves the named input from the list, or null if it
        /// does not exist.
        /// </summary>
        /// <param name="key">The name of the input</param>
        /// <returns>The input</returns>
        private Input RetrieveInput(string key)
        {
            this.Validate();

            foreach (KeyValuePair<string, List<Input>> entry in this.Items)
            {
                string entryKey = entry.Key;
                List<Input> entryValue = entry.Value;
                InputKind kind = entryValue[0].GetKind();

                if (InputCollection.MatchesInputName(kind, key, entryKey))
                {
                    if (entryValue.Count > 1)
                    {
                        throw new SplunkException(
                            SplunkException.AMBIGUOUS,
                            "Key has multiple values, specify a namespace");
                    }

                    return entryValue[0];
                }
            }
            return null;
        }

        /// <summary>
        /// Retrieves the named, scoped and namespace-constrained input from the
        /// list, or null if it does not exist.
        /// </summary>
        /// <param name="key">The name of the input</param>
        /// <param name="splunkNamespace">The namespace</param>
        /// <returns>The input</returns>
        private Input RetrieveInput(string key, Args splunkNamespace)
        {
            Util.EnsureNamespaceIsExact(splunkNamespace);
            this.Validate();

            string pathMatcher = 
                this.Service.Fullpath(string.Empty, splunkNamespace);
            foreach (KeyValuePair<string, List<Input>> entry in this.Items)
            {
                string entryKey = entry.Key;
                List<Input> entryValue = entry.Value;
                InputKind kind = entryValue[0].GetKind();

                if (InputCollection.MatchesInputName(kind, key, entryKey))
                {

                    List<Input> entities = this.Items[key];
                    if (entities.Count == 0)
                    {
                        continue;
                    }
                    foreach (Input entity in entities)
                    {
                        if (entity.Path.StartsWith(pathMatcher))
                        {
                            return entity;
                        }
                    }
                }
            }
            return null;
        }
    }
}
