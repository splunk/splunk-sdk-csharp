using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Splunk;

namespace UnitTests
{
    [TestClass]
    public class NameSpaceTest : TestHelper
    {
        /// <summary>
        /// Tests the syntax of the path given a name space
        /// </summary>
        [TestMethod]
        public void TestStaticNameSpace()
        {
            Service service = Connect();

            Args splunkNameSpace = new Args();

            splunkNameSpace.Add("app", "search");
            Assert.AreEqual("/servicesNS/-/search/", service.Fullpath("", splunkNameSpace), "Expected the path URL to be /servicesNS/-/search/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("owner", "Bob");
            Assert.AreEqual("/servicesNS/Bob/-/", service.Fullpath("", splunkNameSpace), "Expected path URL to be /servicesNS/Bob/-/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("sharing", "app");
            Assert.AreEqual("/servicesNS/nobody/-/", service.Fullpath("", splunkNameSpace), "Expected path URL to be /servicesNS/nobody/-/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("sharing", "system");
            Assert.AreEqual("/servicesNS/nobody/system/", service.Fullpath("", splunkNameSpace), "Expected path URL to be /servicesNS/nobody/system/");
        }

        [TestMethod]
        public void TestLiveNamespace1()
        {
            Service service = Connect();

            String username = "sdk-user";
            String password = "changeme";
            String savedSearch = "sdk-test1";
            String searchString = "search index=main * | 10";

            // Setup a namespace
            Args splunkNameSpace = new Args();
            splunkNameSpace.Add("owner", username);
            splunkNameSpace.Add("app", "search");

            // Get all users, scrub and make our test user
            UserCollection users = service.GetUsers();
            if (users.ContainsKey(username))
            {
                users.Remove(username);
            }

            Assert.IsFalse(users.ContainsKey(username), "Expected users to not contain: " + username);
            users.Create(username, password, "user");
            Assert.IsTrue(users.ContainsKey(username), "Expected users to contain: " + username);

            // Get saved searches for our new namespace, scrub and make our test saved searches
            SavedSearchCollection savedSearches = service.GetSavedSearches(splunkNameSpace);
            if (savedSearches.ContainsKey(savedSearch))
            {
                savedSearches.Remove(savedSearch);
            }

            Assert.IsFalse(savedSearches.ContainsKey(savedSearch), "Expected the saved search to not contain " + savedSearch);

        }

        [TestMethod]
        public void TestLiveNameSpace()
        {
            Service service = Connect();

            String search = "search *";

            // Establish naming convention for separate namespaces
            String searchName = "sdk-test-search";
            String username = "sdk-user";
            String appname = "sdk-app";

            Args splunkNameSpace1 = new Args();
            Args splunkNameSpace2 = new Args();

            splunkNameSpace1.Add("owner", username);
            splunkNameSpace1.Add("app", appname);

            splunkNameSpace2.Add("owner", "-");
            splunkNameSpace2.Add("app", appname);

            // Scrub to ensure apps doesn't already exist and then creates a new app with give appname
            this.CreateApp(appname);
            
            // Scrub to ensure users doesn't already exist
            UserCollection users = service.GetUsers();
            if (users.ContainsKey(username))
            {
                users.Remove(username);
            }
            Assert.IsFalse(users.ContainsKey(username), "Expected users to not contain the username: " + username);

            // Create users
            users.Create(username, "abc", "user");

            // Create namespace specific UNIQUE searches
            SavedSearchCollection savedSearches1 = service.GetSavedSearches(splunkNameSpace1);
            SavedSearchCollection savedSearches2 = service.GetSavedSearches(splunkNameSpace2);



            // FIXME: figure out why create doesn't work.
            //apps.Create("sdk-user", splunkNameSpace);
        }
    }
}

