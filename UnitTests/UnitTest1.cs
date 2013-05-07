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

            // syntactic tests
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
        public void TestLiveNameSpace()
        {
            Service service = Connect();
            EntityCollection<Application> apps = service.GetApplications();
            apps = service.GetApplications();

            Args splunkNameSpace = new Args();
            splunkNameSpace.Add("owner", "sdk-user");

            apps.Create("sdk-user", splunkNameSpace);

            Application apps2 = apps.Get("sdk-user", splunkNameSpace);
            // doesn't work
        }
    }
}

