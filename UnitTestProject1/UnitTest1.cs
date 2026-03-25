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
            bool result = loginPage.Auth("wronglogin", "wrongpass", showMessages: false);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void AuthTestSuccess()
        {
            var loginPage = new LoginPage();

            loginPage.ResetFailedAttempts();

            Assert.IsTrue(loginPage.Auth("bebreef", "1234", showMessages: false));
            Assert.IsTrue(loginPage.Auth("nebebreef", "1", showMessages: false));
            Assert.IsTrue(loginPage.Auth("am", "Fvbnyf2912", showMessages: false));
            Assert.IsTrue(loginPage.Auth("a", "a", showMessages: false));
        }
        [TestMethod]
        public void AuthTestFail()
        {
            var page = new LoginPage();

            Assert.IsFalse(page.Auth("", "any", showMessages: false));

            Assert.IsFalse(page.Auth("admin", "", showMessages: false));

            Assert.IsFalse(page.Auth("nonexistentuser", "12345", showMessages: false));

            Assert.IsFalse(page.Auth("admin", "wrong1", showMessages: false));
            Assert.IsFalse(page.Auth("admin", "wrong2", showMessages: false));
            Assert.IsFalse(page.Auth("admin", "wrong3", showMessages: false)); 
            Assert.IsFalse(page.Auth("admin", "admin123", showMessages: false)); 
        }
        [TestMethod]
        public void AuthTestCaptcha()
        {
            var page = new LoginPage();

            page.Auth("admin", "wrong1", showMessages: false);
            page.Auth("admin", "wrong2", showMessages: false);
            page.Auth("admin", "wrong3", showMessages: false);

            Assert.IsFalse(page.Auth("bebreef", "1234", showMessages: false));

            string correctCaptcha = page.GetCurrentCaptcha(); 
            Assert.IsTrue(page.Auth("bebreef", "1234", correctCaptcha, showMessages: false));
        }
    }
}