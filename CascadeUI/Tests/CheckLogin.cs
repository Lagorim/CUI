using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace CascadeUITest
{
    [TestFixture]
    public class CheckCreateLogin : AuthBaseTest
    {

        [TestCase(TestName = "#01 Проверка валидных данных при логине"), Order(1)]
        public void CheckLoginTest_Admin()
        {
            app.Auth.Logout();

            app.Auth.LoginPage("admin", "00admin1234");
            Assert.IsTrue(app.Auth.IsLoggedIn());
        }

        [TestCase(TestName = "#02 Проверка не валидных данных при логине"), Order(2)]
        public void CheckLoginNotValid()
        {
            app.Auth.Logout();

            app.Auth.LoginPage("admin", "test123");
            Assert.IsFalse(app.Auth.IsLoggedIn());

        }
    }
}
