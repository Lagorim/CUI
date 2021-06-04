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


        protected ManagerApp app;

        public ManagerApp Driver { get; }

        public BaseTest()
        {
            Driver = ManagerApp.GetInstance();
        }

        public void Dispose()
        {
            Driver.Driver.Quit();
            Driver.Driver.Dispose();
        }

        [SetUp]
        public void SetupManagerApp()
        {
            app = ManagerApp.GetInstance();
        }
    }
}
