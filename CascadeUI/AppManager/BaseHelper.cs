using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Linq;
using Allure.Commons;
using System.Text;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace CascadeUITest
{
     public class BaseHelper
    {
        protected ManagerApp manager;
        protected IWebDriver driver;
        

        public BaseHelper(ManagerApp manager)
        {
            this.manager = manager;
            driver = manager.Driver;
        }

        public bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //internal Element _FindAndDoubleClickElement(By by, bool wait = true)
        //{
        //    Element elem = driver.FindElementBy(by, wait);
        //    return elem.DoubleClick();
        //}


    }
}
