using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pr14.Pages;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void RegisterTestSuccess()
        {
            var registerPage = new RegisterPage();

            string uniqueLogin = "testuser_" + Guid.NewGuid().ToString().Substring(0, 8);

            bool result = registerPage.Register(login: uniqueLogin, password: "TestPass123!", fullName: "Тестов Тест Тестович", email: "test@example.com", phone: "+79161234567", showMessages: false);   
            Assert.IsTrue(result, "Регистрация должна пройти успешно");
        }

        [TestMethod]
        public void RegisterTestFail()
        {
            var registerPage = new RegisterPage();

            Assert.IsFalse(registerPage.Register("", "", showMessages: false));

            Assert.IsFalse(registerPage.Register("newuser", "", showMessages: false));

            Assert.IsFalse(registerPage.Register("bebreef", "1234", showMessages: false));

            bool resultWithSpaces = registerPage.Register(
                login: "   userWithSpaces   ",
                password: "Pass123",
                fullName: "Пользователь С Пробелами",
                showMessages: false);

            Assert.IsTrue(resultWithSpaces); 
        }
    }
}