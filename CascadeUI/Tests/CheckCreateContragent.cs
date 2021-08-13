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
    [AllureParentSuite("Контрагент")]
    [AllureSuite("Контрагент")]
    [AllureFeature("Контрагент")]
    public class CheckCreateContragent : AuthBaseTest
    {
        [TestCase(TestName = "#01 Проверка создания контрагента")]
        [Order(1)]
        public void AddContragent()
        {
            app.ContrAgent.CheckCreateTenant(new ContrAgentData("1000"));

            Assert.IsTrue(app.ContrAgent.CheckSummContragent("1 000,00 руб."));

        }

        [TestCase(TestName = "#02 Проверка создания контрагента - не заполнена поле контрагент")]
        [Order(2)]
        public void AddContragentEmpty()
        {
            app.ContrAgent.CheckCreateEmptyTenant(new ContrAgentData("1500"));

            Assert.IsFalse(app.ContrAgent.CheckSummContragent("1 500,00 руб."));
            
        }

        [TestCase(TestName = "#03 Проверка создания контрагента - не заполнена дата")]
        [Order(3)]
        public void AddContrAgentEmptyDate()
        {
            app.ContrAgent.CheckCreateEmptyDate(new ContrAgentData("1000000000"));

            Assert.IsFalse(app.ContrAgent.CheckSummContragent("1 000 000 000,00 руб."));
        }

        [TestCase(TestName = "#04 Отказ от заполнения формы контрагента")]
        [Order(4)]
        public void CancelAddFormContragent()
        {
            app.ContrAgent.CloseFormContragent(new ContrAgentData("23809"));

            Assert.IsFalse(app.ContrAgent.CheckSummContragent("23 809,00 руб."));
        }

        [TestCase(TestName = "#05 Проверка создания арендатора - с суммой 0 ")]
        [Order(5)]
        public void AddContragentSalaryNull()
        {
            app.ContrAgent.AddContragentsummNull(new ContrAgentData("0"));

            Assert.IsTrue(app.ContrAgent.CheckSummContragent("0,00 руб."));
        }
    }
}