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
    public class CheckCreateContragent : BaseTest
    {
        [TestCase(TestName = "#01 Проверка создания контрагента")]
        public void AddContragent()
        {

            //app.ContrAgent
            //    .ClickOnContracts()
            //    .ButtonAdd()
            //    .AddTenant()
            //    .Contragent()
            //    .DateStartContragentForm()
            //    .SalaryContragent("1000");

            app.ContrAgent.CheckCreateTenant(new ContrAgentData("1000"));

        }

        [TestCase(TestName = "#02 Проверка создания контрагента - не заполнена поле контрагент")]
        public void AddContragentEmpty()
        {
            app.ContrAgent.CheckCreateEmptyTenant(new ContrAgentData("1500"));
            
        }

        [TestCase(TestName = "#03 Проверка создания контрагента - не заполнена дата")]
        public void AddContrAgentEmptyDate()
        {
            app.ContrAgent.CheckCreateEmptyDate(new ContrAgentData("1000000000"));

        }

        [TestCase(TestName = "#04 Отказ от заполнения формы контрагента")]
        public void CancelAddFormContragent()
        {
            app.ContrAgent.CloseFormContragent(new ContrAgentData("23809"));
        }

        [TestCase(TestName = "#05 Проверка создания арендатора - с суммой 0 ")]
        public void AddContragentSalaryNull()
        {
            app.ContrAgent.AddContragentsummNull(new ContrAgentData("0"));
        }
    }
}