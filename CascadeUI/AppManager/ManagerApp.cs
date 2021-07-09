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
     public class ManagerApp 
    {
        protected LoginHelper loginHelper;
        protected NavigationHelper navigationHelper;
        protected ContractHelper contractHelper;
        protected ContrAgentHelper contrAgentHelper;
        protected PaymentHelper paymentHelper;

        protected IWebDriver driver;
        //private StringBuilder verificationErrors;
        protected string baseURL; //закоменнтировал для нового
        private bool acceptNextAlert = true;

        //новое
        public string NameSession = null;
        public readonly string DownloadPath;
        public readonly string RemoteWdUri;
        //protected string baseURL;
        public string SessionId => ((RemoteWebDriver)driver).SessionId.ToString();
        private static ThreadLocal<ManagerApp> app = new ThreadLocal<ManagerApp>();
        //

        private ManagerApp(string name = null)
        {
            //старое

            //driver = new ChromeDriver();//(@"C:\wedriver");
            //driver = new RemoteWebDriver();
            baseURL = GetBaseUrl();
            //driver.Manage().Window.Maximize();

            loginHelper = new LoginHelper(this);
            navigationHelper = new NavigationHelper(this/*, baseURL*/);
            contractHelper = new ContractHelper(this);
            contrAgentHelper = new ContrAgentHelper(this);
            paymentHelper = new PaymentHelper(this);

            //новое
            NameSession = name;
            var platform = Environment.OSVersion.Platform;
            if (platform == PlatformID.Win32NT)
                DownloadPath = Environment.CurrentDirectory + "\\downloads";
            else
                DownloadPath = Environment.CurrentDirectory + "/downloads";
            RemoteWdUri = new ConfigReader().GetValue("RemoteWdUri");
            driver = WebDriver;

        }

        public static ManagerApp GetInstance()
        {
            if (! app.IsValueCreated)
            {
                ManagerApp newInstance = new ManagerApp();
                newInstance.Navigation.OpenHomePage(newInstance);
                app.Value = newInstance;
            }
            return app.Value;

        }

        public void Dispose()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        //~ManagerApp()
        //{

        //    try
        //    {
        //        Driver.Quit();
        //    }
        //    catch (Exception)
        //    {
        //        // Ignore errors if unable to close the browser
        //    }
        //}

        //новое


        private RemoteWebDriver WebDriver => _webDriver ?? StartWebDriver();

        private RemoteWebDriver _webDriver;

        private RemoteWebDriver StartWebDriver()
        {
            if (_webDriver != null)
                return _webDriver;

            var attemptsLimit = 6;
            for (int i = 0; i < attemptsLimit; i++)
            {
                try
                {
                    _webDriver = StartBrowser();
                    return _webDriver;
                }
                catch (Exception e)
                {
                    if (i == attemptsLimit - 1)
                        throw new Exception($"Попытка #{i + 1}; Не удалось запустить браузер; {e.Message}");
                    TestContext.Progress.WriteLine($"Попытка #{i + 1}; Не удалось запустить браузер; {e.Message}");
                    Thread.Sleep(5 * 1000);
                }
            }
            return _webDriver;
        }

        private RemoteWebDriver StartBrowser()
        {
            var reader = new ConfigReader();
            ChromeOptions options;
            if (reader.GetValue("Browser").ToLower() == "yandex")
            {
                options = new YandexOptions();
                options.AddArgument("--no-sandbox");
                options.BinaryLocation = reader.GetValue("BinaryLocation");
                options.AddAdditionalCapability("version", "19.4.2.698-1", true);

                if (reader.GetValue("NeedGOSTencryption") == "1" || reader.GetValue("NeedGOSTencryption").ToLower() == "true")
                {
                    //включить гостовое шифрование (для Яндекс-браузера)
                    options.AddArgument("force-fieldtrials=gst/1/");
                    //игнорировать ошибки сертификатов
                    options.AddArgument("ignore-certificate-errors");
                }
            }
            else
            {
                options = new ChromeOptions();
                options.AddArgument("--no-sandbox");
                options.AddAdditionalCapability("version", "90.0", true);
            }
            try
            {
                //создаём директорию для скачивания файлов
                if (!Directory.Exists(DownloadPath)) Directory.CreateDirectory(DownloadPath);
                //options.AddUserProfilePreference("download.default_directory", downloadPath);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            options.AddUserProfilePreference("download.default_directory", "/home/selenium/Downloads");

            options.AddArgument("start-maximized");
            options.AddArgument("no-sandbox");

            //options.AddArgument("no-proxy-server");

            options.AddAdditionalCapability("platform", "Any", true);
            options.AddAdditionalCapability("enableVNC", true, true);
            if (NameSession != null)
                options.AddAdditionalCapability("name", NameSession, true);

            //*****************
            var driver = new RemoteWebDriver(new Uri(RemoteWdUri + "wd/hub"), options.ToCapabilities(), TimeSpan.FromMinutes(9));
            //driver.Manage().Timeouts().PageLoad=TimeSpan.FromSeconds(30);
            //driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);

            return driver;
        }


        public void GoToUrl(string url)
        {
            driver.Url = baseURL + url;
            Thread.Sleep(5000);
        }

        public string GetUrl()
        {
            return driver.Url;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }



        public void CloseDialogWindow()
        {
            try
            {
                Thread.Sleep(1000);
                IAlert alert = Driver.SwitchTo().Alert();
                alert.Accept();
                Thread.Sleep(2000);
            }
            catch (NoAlertPresentException)
            {
                
            }
        }

        public LoginHelper Auth
        {
            get
            {
                return loginHelper;
            }
        }

        public NavigationHelper Navigation
        {
            get
            {
                return navigationHelper;
            }
        }

        public ContractHelper Contract
        {
            get
            {
                return contractHelper;
            }
        }

        public ContrAgentHelper ContrAgent
        {
            get
            {
                return contrAgentHelper;
            }
        }

        public PaymentHelper Payment
        {
            get
            {
                return paymentHelper;
            }
        }


        //новое

        //#region Private
        

        public string GetBaseUrl()
        {
            if (baseURL != null)
                return baseURL;
            var reader = new ConfigReader();
            return reader.GetValue("AppURL");
        }

        

        
        //#endregion

    }
}
