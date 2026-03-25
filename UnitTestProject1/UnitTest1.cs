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
            var loginPage = new LoginPage();
            bool result = loginPage.Auth("wronglogin", "wrongpass");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AuthTestSuccess()
        {
            var loginPage = new LoginPage();

            Assert.IsTrue(loginPage.Auth("bebreef", "1234"));      
            Assert.IsTrue(loginPage.Auth("nebebreef", "1"));
            Assert.IsTrue(loginPage.Auth("am", "Fvbnyf2912"));
            Assert.IsTrue(loginPage.Auth("a", "a"));

        }
        [TestMethod]
        public void AuthTestFail()
        {
            var page = new LoginPage();

            Assert.IsFalse(page.Auth("", "any"));

            Assert.IsFalse(page.Auth("admin", ""));

            Assert.IsFalse(page.Auth("nonexistentuser", "12345"));

            Assert.IsFalse(page.Auth("admin", "wrong1"));
            Assert.IsFalse(page.Auth("admin", "wrong2"));
            Assert.IsFalse(page.Auth("admin", "wrong3")); 
            Assert.IsFalse(page.Auth("admin", "admin123")); // тест для капчи, заранее
        }
    }
}
