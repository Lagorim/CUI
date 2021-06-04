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
    public class ContrAgentHelper : BaseHelper
    {
        public ContrAgentHelper(ManagerApp manager) : base(manager)
        {
        }

        public ContrAgentHelper CheckCreateTenant(ContrAgentData contrAgent)
        {
            manager.Contract.ClickOnContracts();
            manager.Contract.ButtonAdd();
            AddTenant();
            Contragent();
            DateStartContragentForm();
            SalaryContragent(contrAgent.Salary);


            return this;
        }

        public ContrAgentHelper CheckCreateEmptyTenant(ContrAgentData contrAgent)
        {

            manager.Contract.ClickOnContracts();
            manager.Contract.ButtonAdd();
            AddTenant();

            DateStartContragentForm();
            SalaryContragent(contrAgent.Salary);
            return this;
        }

        public ContrAgentHelper CheckCreateEmptyDate(ContrAgentData contrAgent)
        {
            manager.Contract.ClickOnContracts();
            manager.Contract.ButtonAdd();
            AddTenant();
            Contragent();

            SalaryContragent(contrAgent.Salary);
            return this;
        }

        public ContrAgentHelper CloseFormContragent(ContrAgentData contrAgent)
        {
            manager.Contract.ClickOnContracts();
            manager.Contract.ButtonAdd();
            AddTenant();
            Contragent();
            DateStartContragentForm();
            SalaryAndCloseWindow(contrAgent.Salary);
            return this;
        }

        public ContrAgentHelper AddContragentsummNull(ContrAgentData contrAgent)
        {
            manager.Contract.ClickOnContracts();
            manager.Contract.ButtonAdd();
            AddTenant();
            Contragent();
            DateStartContragentForm();
            SalaryContragent(contrAgent.Salary);

            return this;
        }



        public ContrAgentHelper SalaryAndCloseWindow(string value)
        {
            //Установка суммы и закрытие диалогового окна

            driver.FindElement(By.XPath("(//input[@name='Amount'])[3]")).SendKeys(value);
            driver.FindElement(By.XPath("//div[@data-qtip='Close dialog']")).Click();
            return this;
        }

        public ContrAgentHelper SalaryContragent(string value)
        {
            //Ввод суммы (форма контрагента)

            driver.FindElement(By.XPath("(//input[@name='Amount'])[3]")).SendKeys(value);
            driver.FindElement(By.XPath("(//span[contains(text(),'Сохранить')])[22]")).Click();

            return this;
        }

        public ContrAgentHelper DateStartContragentForm()
        {
            //Выбрать дату начала расчета (на форме выбора контрагента)

            driver.FindElement(By.XPath("(//input[@name='Date'])[4]/ancestor::div[@data-ref='inputWrap']/following-sibling::div")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("(//div[@data-ref='footerEl'])//span[contains(text(),'Сегодня')]")).Click();

            return this;
        }

        public ContrAgentHelper Contragent()
        {
            //Найти контрагента

            driver.FindElement(By.XPath("//span[contains(text(),'Контрагент:')]/following::div[@class='x-form-trigger x-form-trigger-default x-form-search-trigger x-form-search-trigger-default x-trigger-index-1 ']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//tr[@class='  x-grid-row']//td[@role='gridcell']//span[@class='x-grid-checkcolumn'])[1]")).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'Выбрать')]")).Click();

            return this;
        }

        public ContrAgentHelper AddTenant()
        {
            //Нажать кнопку "Добавить" (арендаторы)

            driver.FindElement(By.XPath("//label[contains(text(),'Арендаторы')]/following::span[contains(text(),'Добавить')]")).Click();

            return this;
        }
    }
}
