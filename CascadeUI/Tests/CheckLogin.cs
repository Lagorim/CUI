using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Reflection;

namespace CascadeUITest
{
    [TestFixture]
    [AllureNUnit]
    [AllureParentSuite("Логин")]
    [AllureSuite("Логин")]
    [AllureFeature("Логин")]
    public class CheckCreateLogin : AuthBaseTest
    {
        //ManagerApp driver;

        //[OneTimeSetUp]
        //public void OneTimeSetUp()
        //{

        //    Driver = ManagerApp.GetInstance();
        //    //Driver =  new ManagerApp("Договора");
        //    var open = new NavigationHelper(Driver);
        //    open.OpenHomePage(Driver);
        //    var login = new LoginHelper(Driver);
        //    login.LoginPage("admin", "00admin1234");
        //}

        //[OneTimeTearDown]
        //public void OneTimeTearDown()
        //{
        //    Driver.Dispose();
        //}

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
