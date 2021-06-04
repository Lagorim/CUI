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
    public class LoginHelper : BaseHelper
    {

        public LoginHelper(ManagerApp manager) : base(manager)
        {    
        }

        public void LoginPage(string username, string password)
        {

            driver = manager.Driver;

            if (IsLoggedIn())
            {
                if (IsLoggedIn(username, password))
                {
                    return;
                }

                Logout();
            }

            driver.FindElement(By.Name("login")).SendKeys(username);
            driver.FindElement(By.Name("password")).Clear();
            driver.FindElement(By.Name("password")).SendKeys(password);
            driver.FindElement(By.XPath("//button[@type='submit']")).Click();

            
            //return this;
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.XPath("//span[@data-ref='btnInnerEl']"));
        }

        public bool IsLoggedIn(string username, string password)
        {
            return IsLoggedIn()
                && driver.FindElement(By.XPath("//span[@data-ref='btnInnerEl']")).FindElement(By.XPath("//b[contains(text(),'Иванов Иван')]")).Text == username;
        }

        //public bool GetLoggetUserName()
        //{
        //    return IsLoggedIn() 
        //        && driver.FindElement(By.XPath("//span[@data-ref='btnInnerEl']")).FindElement(By.TagName("b")).Text == "Иванов Иван";
        //    //return text.Substring(1, text.Length - 2);
        //}

        public void Logout()
        {
            //Выход из системы 

            if (IsLoggedIn())
            {
                driver.FindElement(By.XPath("//span[@data-ref='btnInnerEl']")).Click();
            }
        }
    }
}
