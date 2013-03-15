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
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Splunk;

    /// <summary>
    /// This is the user/roles test class
    /// </summary>
    [TestClass]
    public class UserTest : TestHelper
    {
        /// <summary>
        /// The base assert string
        /// </summary>
        private string assertRoot = "User tests: ";

        /// <summary>
        /// Test the creation of users, update of values and removal
        /// </summary>
        [TestMethod]
        public void User()
        {
            Service service = Connect();

            string username = "sdk-user";
            string password = "changeme";

            UserCollection users = service.GetUsers();

            // Cleanup potential prior failed test run.
            users.Remove(username);
            Debug.Assert(!users.ContainsKey(username), this.assertRoot + "#14");

            // Create user using base create method
            Args args = new Args();
            args.Add("password", password);
            args.Add("roles", "power");
            User user = users.Create(username, args);
            Debug.Assert(users.ContainsKey(username), this.assertRoot + "#15");
            Debug.Assert(username.Equals(user.Name), this.assertRoot + "#16");
            Debug.Assert(1 == user.Roles.Length, this.assertRoot + "#17");
            Debug.Assert(this.Contains(user.Roles, "power"), this.assertRoot + "#18");

            users.Remove(username);
            Debug.Assert(!users.ContainsKey(username), this.assertRoot + "#19");

            // Create user using derived create method 
            user = users.Create(username, password, "power");
            Debug.Assert(users.ContainsKey(username), this.assertRoot + "#21");
            Debug.Assert(username.Equals(user.Name), this.assertRoot + "#22");
            Debug.Assert(1 == user.Roles.Length, this.assertRoot + "#23");
            Debug.Assert(this.Contains(user.Roles, "power"), this.assertRoot + "#24");

            users.Remove(username);
            Debug.Assert(!users.ContainsKey(username), this.assertRoot + "#25");

            // Create using derived method with multiple roles
            user = users.Create(
                username, password, new string[] { "power", "user" });
            Debug.Assert(users.ContainsKey(username), this.assertRoot + "#26");
            Debug.Assert(username.Equals(user.Name), this.assertRoot + "#27");
            Debug.Assert(2 == user.Roles.Length, this.assertRoot + "#28");
            Debug.Assert(this.Contains(user.Roles, "power"), this.assertRoot + "#29");
            Debug.Assert(this.Contains(user.Roles, "user"), this.assertRoot + "#30");

            users.Remove(username);
            Debug.Assert(!users.ContainsKey(username), this.assertRoot + "#31");

            // Create using drived method with multiple roles and extra properties
            args = new Args();
            args.Add("realname", "Renzo");
            args.Add("email", "email.me@now.com");
            args.Add("defaultApp", "search");
            user = users.Create(
                username, password, new string[] { "power", "user" }, args);
            Debug.Assert(users.ContainsKey(username), this.assertRoot + "#32");
            Debug.Assert(username.Equals(user.Name), this.assertRoot + "#33");
            Debug.Assert(2 == user.Roles.Length, this.assertRoot + "#34");
            Debug.Assert(this.Contains(user.Roles, "power"), this.assertRoot + "#35");
            Debug.Assert(this.Contains(user.Roles, "user"), this.assertRoot + "#36");
            Debug.Assert("Renzo".Equals(user.RealName), this.assertRoot + "#37");
            Debug.Assert("email.me@now.com".Equals(user.Email), this.assertRoot + "#38");
            Debug.Assert("search".Equals(user.DefaultApp), this.assertRoot + "#39");
            string dummyGet = user.Tz;

            // update user using setters
            user.DefaultApp = "search";
            user.Email = "none@noway.com";
            user.Password = "new-password";
            user.RealName = "SDK-name";
            if (service.VersionCompare("4.3") >= 0)
            {
                user.RestartBackgroundJobs = false;
            }
            user.Roles = new string[] { "power" };
            user.Update();
            user.Refresh();

            Debug.Assert(1 == user.Roles.Length, this.assertRoot + "#40");
            Debug.Assert(this.Contains(user.Roles, "power"), this.assertRoot + "#41");
            Debug.Assert("SDK-name".Equals(user.RealName), this.assertRoot + "#42");
            Debug.Assert("none@noway.com".Equals(user.Email), this.assertRoot + "#43");
            Debug.Assert("search".Equals(user.DefaultApp), this.assertRoot + "#44");

            users.Remove(username);
            Debug.Assert(!users.ContainsKey(username), this.assertRoot + "#45");

            Debug.Assert(!users.ContainsKey("sdk-user"), this.assertRoot + "#46");
            if (users.ContainsKey("SDK-user"))
            {
                users.Remove("SDK-user");
            }
            Debug.Assert(!users.ContainsKey("SDK-user"), this.assertRoot + "#47");

            args = new Args();
            args.Add("password", password);
            args.Add("roles", "power");
            Debug.Assert(username.Equals(user.Name), this.assertRoot + "#48");
            Debug.Assert(!users.ContainsKey("SDK-user"), this.assertRoot + "#49");
            users.Remove(username);
            Debug.Assert(!users.ContainsKey(username), this.assertRoot + "#50");
        }
    }
}
