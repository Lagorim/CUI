using System;
using System.Collections.Generic;
using System.Text;

namespace CascadeUITest
{
    public class Account
    {
        private string username;
        private string password;
        //private ManagerApp driver;

        //public Account(string username, string password)
        //{
        //    //var reader = new ConfigReader();
        //    //var userName = reader.GetValue("Login");
        //    //var passWord = reader.GetValue("Password");
        //    this.username = username;
        //    this.password = password;
        //}

        //public Account(ManagerApp _driver)
        //{
            //driver = _driver;

            //var reader = new ConfigReader();
            //var userName = reader.GetValue("Login");
            //var passWord = reader.GetValue("Password");
            //this.username = username;
            //this.password = password;
            //Login();
        //}

        //public void Login()
        //{
        //    var page = new LoginHelper(manager);
        //    //page.OpenHomePage(manager);

        //    var reader = new ConfigReader();
        //    var userName = reader.GetValue("Login");
        //    var passWord = reader.GetValue("Password");

        //    page.LoginPage(userName, passWord);

        //}

        public string Username
        {
            get
            {
                //var reader = new ConfigReader();
                //var username = reader.GetValue("Login");
                return username;
            }

            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }
    }
}
