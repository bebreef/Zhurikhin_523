using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pr14.Pages;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AuthTest()
        {
            var page = new LoginPage();
            Assert.IsTrue(page.Auth("test", "test1"));
            Assert.IsFalse(page.Auth("user1", "12345"));
            Assert.IsFalse(page.Auth("", ""));
            Assert.IsFalse(page.Auth(" ", " "));
        }
    }
}
