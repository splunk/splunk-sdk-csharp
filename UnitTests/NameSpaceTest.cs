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
            Service service = this.Connect();

            Args splunkNameSpace = new Args();

            splunkNameSpace.Add("app", "search");
            Assert.AreEqual("/servicesNS/-/search/", service.Fullpath("", splunkNameSpace),
                            "Expected the path URL to be /servicesNS/-/search/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("owner", "Bob");
            Assert.AreEqual("/servicesNS/Bob/-/", service.Fullpath("", splunkNameSpace),
                            "Expected path URL to be /servicesNS/Bob/-/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("sharing", "app");
            Assert.AreEqual("/servicesNS/nobody/-/", service.Fullpath("", splunkNameSpace),
                            "Expected path URL to be /servicesNS/nobody/-/");

            splunkNameSpace.Clear();
            splunkNameSpace.Add("sharing", "system");
            Assert.AreEqual("/servicesNS/nobody/system/", service.Fullpath("", splunkNameSpace),
                            "Expected path URL to be /servicesNS/nobody/system/");
        }

        [TestMethod]
        public void TestLiveNameSpace()
        {
            Service service = Connect();

            // Establish a naming convention for seperate namespaces
            String search = "search *";

            String username1 = "sdk-user1";
            String username2 = "sdk-user2";
            
            String appname1 = "sdk-app1";
            String appname2 = "sdk-app2";

            Args splunkNameSpace11 = new Args();
            Args splunkNameSpace22 = new Args();
            Args splunkNameSpacex1 = new Args();

            splunkNameSpace11.Add("owner", username1);
            splunkNameSpace11.Add("app", appname1);

            splunkNameSpace22.Add("owner", username2);
            splunkNameSpace22.Add("app", appname2);

            splunkNameSpacex1.Add("owner", "-");
            splunkNameSpacex1.Add("app", appname1);

            // Remove app if it already exists
            EntityCollection<Application> apps = service.GetApplications();
            if (apps.ContainsKey(appname1))
            {
                apps.Remove(appname1);
            }
            if (apps.ContainsKey(appname2))
            {
                apps.Remove(appname2);
            }
            Assert.IsFalse(apps.ContainsKey(appname1), "Expected the app " + appname1 + " to be removed");
            Assert.IsFalse(apps.ContainsKey(appname2), "Expected the app " + appname2 + " to be removed");

            // Remove users if they already exist
            UserCollection users = service.GetUsers();
            if (users.ContainsKey(username1))
            {
                users.Remove(username1);
            }
            if (users.ContainsKey(username2))
            {
                users.Remove(username2);
            }
            Assert.IsFalse(users.ContainsKey(username1), "Expected the username " + username1 + " to be removed");
            Assert.IsFalse(users.ContainsKey(username2), "Expected the username " + username2 + " to be removed");

            // Create users
            users.Create(username1, "abc", "user");
            users.Create(username2, "abc", "user");
            Assert.IsTrue(users.ContainsKey(username1), "Expected users to contain " + username1);
            Assert.IsTrue(users.ContainsKey(username1), "Expected users to contain " + username2);

            // Create app
            apps.Create(appname1);
            apps.Create(appname2);
            Assert.IsTrue(apps.ContainsKey(appname1), "Expected app to contain " + appname1);
            Assert.IsTrue(apps.ContainsKey(appname1), "Expected app to contain " + appname2);

            // Create namespace specific searches
            SavedSearchCollection savedSearches11 = service.GetSavedSearches(splunkNameSpace11);
            SavedSearchCollection savedSearches22 = service.GetSavedSearches(splunkNameSpace22);
            SavedSearchCollection savedSearchesx1 = service.GetSavedSearches(splunkNameSpacex1);

            // Removes test search "sdk-test-search" if it already exists
            if (savedSearches11.ContainsKey("sdk-test-search"))
            {
                savedSearches11.Remove("sdk-test-search");
            }
            if (savedSearches22.ContainsKey("sdk-test-search"))
            {
                savedSearches22.Remove("sdk-test-search");
            }
            Assert.IsFalse(savedSearches11.ContainsKey("sdk-test-search"), "SavedSearches11 already contains sdk-test-search, remove");
            Assert.IsFalse(savedSearches22.ContainsKey("sdk-test-search"), "SavedSearches22 already contains sdk-test-search, remove");

            // Create test search
            savedSearches11.Create("sdk-test-search", search + " | head 1");
            savedSearches22.Create("sdk-test-search", search + " | head 2");
            Assert.IsTrue(savedSearches11.ContainsKey("sdk-test-search"), "Expected savedSearches11 to contain the key sdk-test-search");
            Assert.IsTrue(savedSearches22.ContainsKey("sdk-test-search"), "Expected savedSearches22 to contain the key sdk-test-search");
            Assert.IsTrue(savedSearchesx1.ContainsKey("sdk-test-search", splunkNameSpace11), "Expected savedSearchesx1 to contain sdk-test-search in the namespace splunkNameSpace11");
            Assert.IsTrue(savedSearchesx1.Get("sdk-test-search", splunkNameSpace11) != null, "Expected savedSearchesx1 to have the test sdk-test-search for splunkNameSpace11");

            // Remove a saved search through a specific namespace path
            savedSearchesx1.Remove("sdk-test-search", splunkNameSpace11);
            Assert.IsFalse(savedSearchesx1.ContainsKey("sdk-test-search"), "Expected the saved search sdk-test-search to be removed");

            // Clean up apps and users
            apps.Refresh();
            if (apps.ContainsKey(appname1))
            {
                apps.Remove(appname1);
            }
            if (apps.ContainsKey(appname2))
            {
                apps.Remove(appname2);
            }
            Assert.IsFalse(apps.ContainsKey(appname1), "Expected app " + appname1 + " to be removed");
            Assert.IsFalse(apps.ContainsKey(appname2), "Expected app " + appname2 + " to be removed");

            users = service.GetUsers();
            if (users.ContainsKey(username1))
            {
                users.Remove(username1);
            }
            if (users.ContainsKey(username2))
            {
                users.Remove(username2);
            }
            Assert.IsFalse(users.ContainsKey(username1), "Expected user " + username1 + " to be removed");
            Assert.IsFalse(users.ContainsKey(username2), "Expected user " + username2 + " to be removed");
        }
    }
}