using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace CascadeUITest
{
    public class BaseTest : IDisposable
    {


        public ManagerApp app;

        public ManagerApp Driver { get; set; }

        //public BaseTest()
        //{
        //    Driver = ManagerApp.GetInstance();
        //}

        public void Dispose()
        {
            Thread.Sleep(5000);
            app.Driver.Quit();
            app.Driver.Dispose();
        }

        [SetUp]
        public void SetupManagerApp()
        {
            app = ManagerApp.GetInstance();
        }
        
    }
}
