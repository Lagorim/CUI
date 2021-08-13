using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.IO.Compression;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Reflection;
using OpenQA.Selenium.Interactions;

namespace CascadeUITest
{
    public class StaffHelper : BaseHelper
    {

        public StaffHelper(ManagerApp manager) : base(manager)
        {
        }

        public void CreateStaff()
        {
            //Создание сотрудника

            driver = manager.Driver;

            SelectStaff();
            ClickAdd();
            FillFields(surname, name, login, password);
            ClickList();
            FilterLogin(login);



        }

        public void RedactionStaff()
        {
            //Редактирование сотрудника

            driver = manager.Driver;

            SelectStaff();
            SelectStaffAllList();
            ClickRedaction();
            ChangeStatus();
            ClickSaveButton();
            ClickList();
        }

        public void DeleteStaff()
        {
            //Удаление сотрудника

            driver = manager.Driver;

            SelectStaff();
            ClickAdd();
            FillFields(surname, name, login, password);
            ClickList();
            SelectStaffAllList();
            ClickDelete();
            
        }

        public void CreateAndAutorization()
        {
            driver = manager.Driver;

            SelectStaff();
            ClickAdd();
            FillFields(surname, name, login, password);
            ClickList();
            AutorizationStaff(login, password);
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.XPath("//span[@data-ref='btnInnerEl']"));
        }

        public bool IsLoggedIn(string login, string password)
        {
            return IsLoggedIn()
                && driver.FindElement(By.XPath("//span[@data-ref='btnInnerEl']")).FindElement(By.XPath("//b[contains(text(),'Иванов Иван')]")).Text == login;
        }

        public void Logout()
        {
            //Выход из системы 

            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//span[@data-ref='btnInnerEl']")).Click();
            }
        }

        public void AutorizationStaff(string login, string password)

        {
            driver = manager.Driver;

            if (IsLoggedIn())
            {
                if (IsLoggedIn(login, password))
                {
                    return;
                }

                Logout();
            }
            Thread.Sleep(5000);
            driver.FindElement(By.Name("login")).SendKeys(login);
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();


        }

        public void Delete()
        {
            driver = manager.Driver;

            SelectStaffAllList();
            ClickDelete();
        }

        public void SelectStaff()
        {
            //клик в меню на "Сотрудники"
            
            driver.FindElement(By.XPath("//span[contains(text(),'Сотрудники')]")).Click();
        }

        public void ClickAdd()
        {
            //клик по кнопке "Добавить" в разделе сотрудники
            
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[contains(text(),'Сотрудники')])[1]/following::span[contains(text(),'Добавить')][1]")).Click();
        }

        public void ClickRedaction()
        {
            //клик по кнопке "Редактировать" в разделе сотрудники

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[contains(text(),'Сотрудники')])[1]/following::span[contains(text(),'Редактировать')][1]")).Click();
        }

        public void ClickDelete()
        {
            //клик по кнопке "Удалить" в разделе сотрудники

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[contains(text(),'Сотрудники')])[1]/following::span[contains(text(),'Удалить')][1]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[contains(text(),'Предупреждение')]/following::span[contains(text(),'Да')]")).Click();
        }

        public void ChangeStatus()
        {
            //изменение статуса

            Thread.Sleep(1000);
            driver.FindElement(By.XPath("(//span[contains(text(),'Статус')])[7]/following::div[@role='presentation'][4]")).Click();
            driver.FindElement(By.XPath("//li[contains(text(),'Уволен')]")).Click();
        }


        public void FillFields(string surname, string name, string login, string password)
        {
            //заполнение обязательных полей для создания сотрудника

            //Данные
            Thread.Sleep(2000);
            driver.FindElement(By.XPath("//div[contains(text(),'Сотрудники / Сотрудник')]/following::input[@name='Surname']")).SendKeys(surname);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[contains(text(),'Сотрудники / Сотрудник')]/following::input[@name='Name']")).SendKeys(name);

            //Системные данные
            driver.FindElement(By.XPath("//input[@name='Login']")).SendKeys(login);
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//input[@name='Password']")).SendKeys(password);
            driver.FindElement(By.XPath("((//span[contains(text(),'Подразделение')])[3]/following::div[@role='presentation'])[4]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//div[contains(text(),'Выбор элемента')]/following::input")).SendKeys("Алексеевского");
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//tr[@class='x-grid-tree-node-leaf  x-grid-row']//td[@role='gridcell']//span[@class='x-grid-checkcolumn']")).Click();
            driver.FindElement(By.XPath("//div[contains(text(),'Выбор элемента')]/following::span[contains(text(),'Выбрать')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("(//span[contains(text(),'Статус')])[7]/following::div[@role='presentation'][4]")).Click();
            driver.FindElement(By.XPath("//li[contains(text(),'Активный')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("(//span[contains(text(),'Должность')])[4]/following::div[@role='presentation'][4]")).Click();
            driver.FindElement(By.XPath("//li[contains(text(),'Сотрудник')]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("(//span[contains(text(),'Роли')])[2]/following::div[@role='presentation'][6]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//span[@class='x-column-header-checkbox']")).Click();
            driver.FindElement(By.XPath("//div[contains(text(),'Выбор элемента')]/following::span[contains(text(),'Выбрать')]")).Click();

            driver.FindElement(By.XPath("//div[contains(text(),'Сотрудники / Сотрудник')]/following::span[contains(text(),'Сохранить')][1]")).Click();
            Thread.Sleep(3000);
        }

        public void ClickList()
        {
            //клик по "К списку"
            driver = manager.Driver;

            driver.FindElement(By.XPath("//div[contains(text(),'Сотрудники / Сотрудник')]/following::span[contains(text(),'К списку')]")).Click();
            Thread.Sleep(3000);
        }

        public void FilterLogin(string login)
        {
            driver = manager.Driver;

            driver.FindElement(By.XPath("//span[contains(text(),'Логин')]/following::input")).SendKeys(login);
        }

        public void SelectStaffAllList()
        {
            //выбрать сотрудника из списка
            driver.Navigate().Refresh();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[@class='x-grid-item-container'])[2]//table")).Click();
            //Thread.Sleep(3000);

        }

        public void ClickSaveButton()
        {
            //клик по кнопке "Сохранить"

            driver.FindElement(By.XPath("//div[contains(text(),'Сотрудники / Сотрудник')]/following::span[contains(text(),'Сохранить')][1]")).Click();
            Thread.Sleep(3000);
        }

        //public bool CheckLogin(string login)
        //{

        //    return IsElementPresent(By.XPath("//div[contains(text(),'" + login + "')]"));
        //}

        public bool IsLoggedInTrue()
        {
            return IsElementPresent(By.XPath("//b[contains(text(),'Автотест Тест')]"));
        }

        public bool CheckFIO()
        {
            return IsElementPresent(By.XPath("//div[contains(text(),'Автотест Тест')]"));
        }

        public bool CheckStatus()
        {

            return IsElementPresent(By.XPath("//div[contains(text(),'Уволен')]"));

        }
    }
}
