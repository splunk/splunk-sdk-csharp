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

namespace Splunk
{
    /// <summary>
    /// The <see cref="Input"/> class is the base class upon which all specific
    /// inputs are derived from.
    /// </summary>
    public class Input : Entity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> class.
        /// </summary>
        /// <param name="service">The connected service</param>
        /// <param name="path">The path</param>
        public Input(Service service, string path)
            : base(service, path)
        {
        }

        /// <summary>
        /// Gets the unknown input kind. Must be overridden in sub-classes.
        /// </summary>
        /// <returns>The input kind</returns>
        public virtual InputKind GetKind()
        {
            string[] pathComponents = 
                Util.SubstringAfter(this.Path, "/data/inputs/", null).Split('/');
            string kindPath;
            if (pathComponents[0].Equals("tcp"))
            {
                kindPath = "tcp/" + pathComponents[1];
            }
            else
            {
                kindPath = pathComponents[0];
            }

            return InputKind.Create(kindPath);
        }
    }
}
