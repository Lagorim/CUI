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
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace CascadeUITest
{
    [TestFixture]
    [AllureNUnit]
    [AllureParentSuite("Сотрудники")]
    [AllureSuite("Сотрудники")]
    [AllureFeature("Сотрудники")]

    public class CheckStaff : AuthBaseTest
    {

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Driver.Dispose();
        }

        //[TearDown]
        //public void TearDown()
        //{
        //    app.Staff.Logout();
        //    app.Auth.IsLoggedIn();
        //    app.Staff.SelectStaff();
            
        //    while (app.Staff.CheckFIO() == true)
        //    {
        //        app.Staff.SelectStaffAllList();
        //        app.Staff.ClickDelete();
        //    }
        //}

        [TestCase(TestName = "#01 Создание сотрудника")]
        [Order(1)]
        public void CheckCreateStaff()
        {
            app.Staff.CreateStaff();
            
            Assert.IsTrue(app.Staff.CheckFIO());
        }

        [TestCase(TestName = "#02 Редактирование сотрудника")]
        [Order(2)]
        public void RedactiobStaff()
        {
            app.Staff.RedactionStaff();

            Assert.IsTrue(app.Staff.CheckStatus());
        }

        [TestCase(TestName = "#03 Удаление сотрудника")]
        [Order(3)]
        public void DeleteStaff()
        {
            app.Staff.DeleteStaff();
        }

        [TestCase(TestName = "#04 Создание пациента и последующая авторизация под ним")]
        [Order(4)]
        public void CreateStaffAndAutorization()
        {
            app.Staff.CreateAndAutorization();

            Assert.IsTrue(app.Staff.IsLoggedInTrue());

        }
    }
}
