﻿/*
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
    /// The <see cref="ApplicationArgs"/> class extends <see cref="Args"/> for 
    /// <see cref="Application"/> creation setters.
    /// </summary>
    public class ApplicationArgs : Args
    {
        /// <summary>
        /// Sets the author of this application. 
        /// </summary>
        /// <remarks>
        /// For apps you intend to post to Splunkbase, enter the username of 
        /// your splunk.com account. For internal-use-only apps, include your 
        /// full name and/or contact info (for example, email address).
        /// </remarks>
        public string Author
        {
            set
            {
                this["author"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether the application has had its 
        /// custom configuration performed.
        /// </summary>
        public bool Configured
        {
            set
            {
                this["configured"] = value;
            }
        }

        /// <summary>
        /// Sets the description of the application. The application description
        /// is a short explanatory string displayed underneath the app's title 
        /// in Launcher. Typically, descriptions of 200 characters or fewer are 
        /// most effective.
        /// </summary>
        public string Description
        {
            set
            {
                this["description"] = value;
            }
        }

        /// <summary>
        /// Sets the label of the application that is displayed in the Splunk 
        /// GUI and Launcher. The recommended length of the application label is
        /// between 5 and 80 characters, and it must not include "Splunk For".
        /// </summary>
        public string Label
        {
            set
            {
                this["label"] = value;
            }
        }

        /// <summary>
        /// Sets a value that indicates whether the application can be managed 
        /// by the Splunk Manager. Note: This method is deprecated in Splunk 
        /// 5.0.
        /// </summary>
        public bool Manageable
        {
            set
            {
                this["manageable"] = value;
            }
        }

        /// <summary>
        /// Sets the template type used when creating the application. 
        /// The valid values are "barebones" or "sample_app" or a previously
        /// installed custom template.
        /// </summary>
        /// <remarks>
        /// A value of "barebones" indicates the template contains the basic 
        /// framework for an app.<br/>
        /// A value of "sample_app" indicates the template contains example 
        /// views and searches.
        /// </remarks>
        public string Template
        {
            set
            {
                this["template"] = value;
            }
        }

        /// <summary>
        /// Sets a value indicating whether the application is visible and 
        /// accessible from the Splunk UI.
        /// </summary>
        public bool Visible
        {
            set
            {
                this["visible"] = value;
            }
        }
    }
}