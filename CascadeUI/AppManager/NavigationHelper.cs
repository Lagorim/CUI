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
    public class NavigationHelper : BaseHelper
    {

        //private string url;
        //private string baseURL;

        public NavigationHelper(ManagerApp manager/*, string baseURL*/) : base(manager)
        {
        }

        public void OpenHomePage(ManagerApp manager)
        {
            //Открытие главной страницы

            manager.GoToUrl("");



            
            
            //return this;
        }
    }
}
