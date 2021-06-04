using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CascadeUITest
{
    public class AuthBaseTest : BaseTest
    {


        [SetUp]
        public void SetupLogin()
        {
            var reader = new ConfigReader();
            var userName = reader.GetValue("Login");
            var passWord = reader.GetValue("Password");
            app.Auth.LoginPage(userName, passWord);
            Thread.Sleep(10000);
        }
    }
}
