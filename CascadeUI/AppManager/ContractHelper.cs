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
    public class ContractHelper : BaseHelper
    {
        string currentFile = string.Empty;
        static string name = string.Empty;

        public ContractHelper(ManagerApp manager) : base(manager)
        {
        }


        public void CreateContract(ContractData contract)
        {
            //Создание договора
            driver = manager.Driver;

            ClickOnContracts();
            ButtonAdd();
            NumberContract(contract.NumberContract);
            ChoiceDateEnd_Today();
            ChoiceObjectRent(contract.NumberObject);
            ChoiceTypeConclusion(contract.Conclusion);
            ChoiceDatePay(contract.Value);
            try
            {
                ClickSave();
                driver.FindElement(By.XPath("//div[contains(text(),'Ошибка! Поле Арендаторы обязательное поле для заполнения')]"));
            }
            catch
            {
                AddTenant();
            }
            Contragent();
            DateStartContragentForm(contract.Date);
            SalaryContragent();
            ClickSave();
            Thread.Sleep(2000);

            //return this;
        }

        public void CreateContractTrueStatus(ContractData contract)
        {
            driver = manager.Driver;
            ClickOnContracts();
            ButtonAdd();
            NumberContract(contract.NumberContract);
            ChoicedateEnd_OneYear(contract.Date);
            ChoiceObjectRent(contract.NumberObject);
            ChoiceTypeConclusion(contract.Conclusion);
            ChoiceDatePay(contract.Value);
            try
            {
                ClickSave();
                driver.FindElement(By.XPath("//div[contains(text(),'Ошибка! Поле Арендаторы обязательное поле для заполнения')]"));
            }
            catch
            {
                AddTenant();
            }
            Contragent();
            DateStartContragentForm(contract.Date);
            SalaryContragent();
            ClickSave();
            Thread.Sleep(2000);

            //return this;
        }

        public void RedactionContractClick()
        {
            //переход договора в редактирование

            driver = manager.Driver;

            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(6000);
            RedactionClickButton();

        }

        public void StatusPaidContract()
        {
            //проверка перехода во вкладку "Претензии" у договора со статусом "Нет задолженности"

            driver = manager.Driver;

            Thread.Sleep(5000);
            StatusPaid();
            Thread.Sleep(6000);
            RedactionClickButton();
            ClickClaims();
        }

        public void StatusPaidContract_LawSuit()
        {
            //проверка перехода во вкладку "Претензии" у договора со статусом "Нет задолженности" и переход в Иски

            Thread.Sleep(5000);
            StatusPaid();
            Thread.Sleep(6000);
            RedactionClickButton();
            ClickLawSuit();
        }

        public void AddClaimPaidContract(string dateClaim, string dateStart, string dateEnd)
        {
            //Добавление претензии в статусе договора "Нет задолженности"

            StatusPaidContract();
            ClickButoonAddClaimForm();

            ClaimSigned();
            ClaimTenant();
            DateClaim(dateClaim);
            DateClaimWith(dateStart);
            DateClaimEnd(dateEnd);
            //ClickCreateFile();
            //Thread.Sleep(6000);
        }

        public void AddClaimPaidContract_LawSuit(string dateClaim, string dateStart, string dateEnd)
        {
            StatusPaidContract();
            //ClickButoonAddClaimForm();
            ClickButtonAddClaimForm_LawSuit();

            ClaimSigned();
            ClaimTenant();
            DateClaim(dateClaim);
            DateClaimWith(dateStart);
            DateClaimEnd(dateEnd);
            ClickSaveButtonClaim();
        }

        public void AddLawSuit()
        {
            //Добавление иска в статус договора "Нет задолженности"

            driver = manager.Driver;

            StatusPaidContract_LawSuit();
            ClickButtonAddLawSuit();

            SelectClaim();
            if (CheckSelectClaim())
            {
                if (! CheckSelectClaim())
                {
                    return;
                    //ClickSelectClaim();
                    //AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
                }
                ClickSelectClaim();
            }

            CloseDialogLawSuit();
            CloseFormLawSuit();
            ClickButtonList();
            AddClaimPaidContract_LawSuit("20.05.2021", "10.04.2020", "20.04.2021");
            //SelectTenantInFormLawSuit();
            if (CheckSelectClaim())
            {
                if (!CheckSelectClaim())
                {
                    AddTenantInLawSuit("Тест", "ООО", "г Казань, ул Татарстан", "г Казань, ул Татарстан");
                }
            }

        }

        #region Methods for claim //Методы для составления претензии
        //Методы для составления претензии

        public void ClickClaims()
        {
            //переход по вкладке "Претензии"

            driver.FindElement(By.XPath("//span[contains(text(),'Претензии')]")).Click();
        }

        public void ClickButoonAddClaimForm()
        {
            //клик  по кнопке "Добавить" во вкладке "Претензии"

            driver.FindElement(By.XPath("(//div[contains(text(),'Договоры /')]/following::div[@data-ref='bodyWrap']//a//span[contains(text(),'Добавить')])[3]")).Click();
        }

        public void ClickButtonAddClaimForm_LawSuit()
        {
            driver.FindElement(By.XPath("(//div[contains(text(),'Договоры /')]/following::div[@data-ref='bodyWrap']//a//span[contains(text(),'Добавить')])[4]")).Click();
        }

        public void ClaimSigned()
        {
            //форма "Подписант"

            driver.FindElement(By.XPath("(//span[contains(text(),'Подписант')]/following::div[@role='presentation'])[4]")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[@class='x-grid-checkcolumn']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(text(),'Выбрать')]")).Click();
        }

        public void ClaimTenant()
        {
            //форма "Арендатор"

            driver.FindElement(By.XPath("((//span[contains(text(),'Арендатор:')])[2]/following::div[@role='presentation'])[4]")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[@class='x-grid-checkcolumn']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(text(),'Выбрать')]")).Click();
        }

        public void DateClaim(string dateClaim)
        {
            //дата претензии

            var elem = driver.FindElement(By.XPath("(//input[@name='DocDate'])[2]"));
            elem.Click();
            Thread.Sleep(7000);
            elem.SendKeys(dateClaim);
            Thread.Sleep(3000);


        }

        public void DateClaimWith(string dateStart)
        {
            //дата "С"

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("//input[@name='DateStart']"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(7000);
            elementLocator.SendKeys(dateStart);
            Thread.Sleep(3000);
        }

        public void DateClaimEnd(string dateEnd)
        {
            //дата "По"

            var elem = driver.FindElement(By.XPath("//input[@name='DateEnd']"));
            elem.Click();
            Thread.Sleep(7000);
            elem.SendKeys(dateEnd);
            Thread.Sleep(3000);

        }

        public void ClickCreateFile()
        {
            //клик по кнопке "Сформировать файл"

            driver.FindElement(By.XPath("//span[contains(text(),'Сформировать файл')]")).Click();
            Thread.Sleep(6000);
        }

        public void ClickSaveButtonClaim()
        {
            //клик по кнопке "Сохранить"

            driver.FindElement(By.XPath("//div[@role='dialog']//div[@role='toolbar']//span[contains(text(),'Сохранить')]")).Click();
        }

        public void DeleteClaim()
        {
            //удаление Претензии

            Thread.Sleep(5000);
            driver.FindElement(By.XPath("(//div[@class='x-grid-item-container'])[6]//tr[@class='  x-grid-row']")).Click();
            driver.FindElement(By.XPath("(//div[@class='x-panel-bodyWrap']//span[contains(text(),'Удалить')])[7]")).Click();
            Thread.Sleep(5000);
            CloseWindowClaimDelete();
            Thread.Sleep(4000);
        }

        public bool CheckSummNull()
        {
            //проверка строки "суммы" равной "0,00" 

            return IsElementPresent(By.XPath("(//div[@class='x-grid-item-container'])[6]//div[contains(@class,'x-grid-cell-inner')][normalize-space()='0,00']"));
        }

        public bool CheckSave()
        {
            //проверка кнопки "Сохранить"

            return IsElementPresent(By.XPath("//div[@role='dialog']//div[@role='toolbar']//span[contains(text(),'Сохранить')]"));
        }

        public void RedactionClaim()
        {
            //клик по кнопке "Редактировать" в претензиях

            Thread.Sleep(5000);
            driver.FindElement(By.XPath("(//div[@class='x-grid-item-container'])[6]//tr[@class='  x-grid-row']")).Click();
            driver.FindElement(By.XPath("(//div[@class='x-panel-bodyWrap']//span[contains(text(),'Редактировать')])[2]")).Click();
            Thread.Sleep(5000);

        }

        public void ChangeClaim(string conclusion)
        {

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(text(),'Требование:')]/following::div[@class='x-form-trigger x-form-trigger-default x-form-arrow-trigger x-form-arrow-trigger-default  ']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//li[contains(text(),'" + conclusion + "')]")).Click();
            Thread.Sleep(3000);
        }

        public bool ChangeNameClaim(string conclusion)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[@name='Requirement']")).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'Требование:')]/following::div[@class='x-form-trigger x-form-trigger-default x-form-arrow-trigger x-form-arrow-trigger-default  ']")).Click();
            //Thread.Sleep(3000);
            return IsElementPresent(By.XPath($"//li[contains(text(),'" + conclusion + "')]"));
        }

        public void CheckDownloadFileClaim()
        {

            //driver.Navigate().GoToUrl("/home/selenium/Downloads/");

            string expectedFile = @"/home/selenium/Downloads/download";
            bool exist = false;
            //string Path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
           

            //var reader = new ConfigReader();
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", "/home/selenium/Downloads");

            FileInfo fileinfo = new FileInfo(expectedFile);
            Assert.AreEqual(fileinfo.Name, "download");
            Assert.AreEqual(fileinfo.FullName, @"C:\home\selenium\Downloads\download");
            //string Path = options.AddUserProfilePreference("download.default_directory", @"/home/selenium/Downloads");
            //options.AddUserProfilePreference("download.default_directory", @"/home/selenium/Downloads");

            //WebDriverWait wait = new WebDriverWait(manager.Driver, TimeSpan.FromSeconds(5));
            //wait.Until<bool>(x => exist = File.Exists(expectedFile));

            //string[] filePaths = Directory.GetFiles(options);
            //foreach (string p in filePaths)
            //{
            //    if (p.Contains(filename))
            //    {
            //        FileInfo thisFile = new FileInfo(p);
            //        //Check the file that are downloaded in the last 3 minutes
            //        if (thisFile.LastWriteTime.ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
            //        thisFile.LastWriteTime.AddMinutes(1).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
            //        thisFile.LastWriteTime.AddMinutes(2).ToShortTimeString() == DateTime.Now.ToShortTimeString() ||
            //        thisFile.LastWriteTime.AddMinutes(3).ToShortTimeString() == DateTime.Now.ToShortTimeString())
            //            exist = true;
            //        File.Delete(p);
            //        break;
            //    }
            //}
            //return exist;

        }

        public void CloseWindowClaimDelete()
        {
            //Выбор "ДА" в форме удаления претензии

            driver.FindElement(By.XPath("(//span[contains(text(),'Да')])[37]")).Click();
        }

        #endregion


        #region Methods for LawSuit //Методы для составления исков

        public void AddTenantInLawSuit(string fullName, string orgForm, string jurAddress, string postAddress)
        {
            //добавление "Арендатора" в исках 

            driver.FindElement(By.XPath("(//a[@role='button']//span[contains(text(),'Добавить')])[22]")).Click();
            CreateFullNameTenantLawSuit(fullName);
            CreateOrgFormTenantLawSuit(orgForm);
            CreateJurAddressTenantLawSuit(jurAddress);
            CreatePostAddressTenantLawSuit(postAddress);
        }

        public void CreateFullNameTenantLawSuit(string fullName)
        {
            //добавление в строку "полное наименование"

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("//div[contains(text(),'Новая запись')]/following::div//input[@name='Name']"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(100);
            elementLocator.SendKeys(fullName);
            Thread.Sleep(2000);
        }

        public void CreateOrgFormTenantLawSuit(string orgForm)
        {
            //добавление в строку "организационная форма" 

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("//div[contains(text(),'Новая запись')]/following::div//input[@name='OrgForm']"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(3000);
            elementLocator.SendKeys(orgForm);
            Thread.Sleep(3000);
        }

        public void CreateJurAddressTenantLawSuit(string jurAddress)
        {
            //добавление в строку "юридический адрес"

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("//div[contains(text(),'Новая запись')]/following::div//input[@name='JurAddress']"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(3000);
            elementLocator.SendKeys(jurAddress);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[contains(text(),'Новая запись')]/following::div//input[@name='OrgForm']")).Click();
        }

        public void CreatePostAddressTenantLawSuit(string postAddress)
        {
            //добавление в строку "почтовый адрес"

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("//div[contains(text(),'Новая запись')]/following::div//input[@name='PostAddress']"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(3000);
            elementLocator.SendKeys(postAddress);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[contains(text(),'Новая запись')]/following::div//input[@name='OrgForm']")).Click();

        }

        public void ClickLawSuit()
        {
            //переход во вкладку "Иски"

            driver.FindElement(By.XPath("//span[contains(text(),'Иски')]")).Click();
        }

        public void ClickButtonAddLawSuit()
        {
            //клик по кнопке "добавить" в исках

            driver.FindElement(By.XPath("(//div[contains(text(),'Договоры /')]/following::div[@data-ref='bodyWrap']//a//span[contains(text(),'Добавить')])[3]")).Click();
        }

        public void SelectClaim()
        {
            //клик по кнопке "Претензия" и ее выбор, в форме добавления Исков

            driver.FindElement(By.XPath("(//span[contains(text(),'Претензия:')]/following::div[@role='presentation'])[4]")).Click();
        }

        public void SelectTenantInFormLawSuit()
        {
            //клик по кнопке "Арендатор" и ее выбор, в форме добавления Исков
            driver.FindElement(By.XPath("((//span[@class='x-form-item-label-text'][contains(text(),'Арендатор:')])[3]/following::div[@role='presentation'])[4]")).Click();
        }

        public void ClickSelectClaim()
        {

            driver.FindElement(By.XPath("//span[@class='x-grid-checkcolumn']")).Click();
        }

        public void CloseDialogLawSuit()
        {
            //закрытие формы Претензии (Иски) --> "Выбор элемента"  

            driver.FindElement(By.XPath("//div[@data-qtip='Close dialog']")).Click();
        }

        public void CloseFormLawSuit()
        {
            // закрытие формы "Иски"

            driver.FindElement(By.XPath("//div[@role='dialog']//div[@role='toolbar']//span[contains(text(),'Отменить')]")).Click();
        }

        public bool CheckSelectClaim()
        {
            //проверка наличия чек-бокс в Исках

            return IsElementPresent(By.XPath("//span[@class='x-grid-checkcolumn']"));
        }

        #endregion
        
        public void StatusClaimContract()
        {
            //проверка вкладки "Претензии" у договора со статусом "Требует направление претензии"

            driver = manager.Driver;


        }

        public void StatusPaid()
        {
            //найти договор со статусом "Нет задолженности"

            driver.FindElement(By.XPath("//span[contains(text(),'Нет задолженности')]")).Click();
        }

        

        public void RedactionClickButton()
        {
            //клик по кнопке "Редактировать"

            driver.FindElement(By.XPath("//span[contains(text(),'Редактировать')]")).Click();
        }

        public void ClickButtonList()
        {
            //клик по кнопке "К списку"

            driver.FindElement(By.XPath("//span[contains(text(),'К списку')]")).Click();
        }

        public bool ButtonList()
        {
            //поиск кнопки "К списку"

            return IsElementPresent(By.XPath("//span[contains(text(),'К списку')]"));
        }

        public void DeleteLastContract()
        {
            //driver = manager.Driver;

            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("(//div[@role='toolbar']//span[contains(text(),'Удалить')])[1]")).Click();
            Thread.Sleep(8000);
            CloseWindowAttentionForDelete();
            //return this;
        }

        public void CloseWindowAttentionForDelete()
        {
            
            driver.FindElement(By.XPath("(//span[contains(text(),'Да')])[36]")).Click();
            //return this;
        }

        public void CheckRecContract(string value)
        {
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//input[@data-ref='inputEl'])[3]")).SendKeys(value);
            //return this;
        }

        public bool CheckStatusContract_Correct()
        {
            //Проверка статуса договора (истек срок)

            return IsElementPresent(By.XPath("//div[contains(text(),'Действующий')]"));


        }

        public bool CheckStatusContract_Expired()
        {
            //Проверка статуса договора (истек срок)

            return IsElementPresent(By.XPath("//div[contains(text(),'Истек срок')]"));

             
        }

        public void SalaryContragent()
        {
            //Ввод суммы (форма контрагента)

            driver.FindElement(By.XPath("(//input[@name='Amount'])[3]")).SendKeys("1000");
            driver.FindElement(By.XPath("(//span[contains(text(),'Сохранить')])[22]")).Click();

            //return this;
        }

        public void DateStartContragentForm(string data)
        {
            //Выбрать дату начала расчета (на форме выбора контрагента)

            var elem = driver.FindElement(By.XPath("(//input[@name='Date'])[4]"));
            elem.Click();
            Thread.Sleep(7000);
            //elem.Submit();
            //Thread.Sleep(3000);
            elem.SendKeys(data);
            Thread.Sleep(3000);
            //Thread.Sleep(6000);
            //driver.FindElement(By.XPath("(//div[@data-ref='footerEl'])[2]//span[contains(text(),'Сегодня')]")).Click();

            //return this;
        }

        public void Contragent()
        {
            //Найти контрагента

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(text(),'Контрагент:')]/following::div[@class='x-form-trigger x-form-trigger-default x-form-search-trigger x-form-search-trigger-default x-trigger-index-1 ']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//tr[@class='  x-grid-row']//td[@role='gridcell']//span[@class='x-grid-checkcolumn'])[1]")).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'Выбрать')]")).Click();

            //return this;
        }

        public void AddTenant()
        {
            //Нажать кнопку "Добавить" (арендаторы)

            driver.FindElement(By.XPath("//label[contains(text(),'Арендаторы')]/following::span[contains(text(),'Добавить')]")).Click();

            //return this;
        }

        public void ClickSave()
        {
            //клик по кнопке "Сохранить"

            driver.FindElement(By.XPath("//span[contains(text(),'Сохранить')]")).Click();

            //return this;
        }

        public void ChoiceDatePay(string value)
        {
            //Выбрать "Расчетную дату оплаты"

            driver.FindElement(By.Id("combo-1093-trigger-picker")).Click();
            Thread.Sleep(6000);
            driver.FindElement(By.XPath("(//li[@data-boundview='combo-1093-picker'])['"+ value + "']")).Click();

            //return this;
        }

        public void ChoiceTypeConclusion(string conclusion)
        {
            //Выбрать "Основания заключения"

            driver.FindElement(By.Id("combo-1091-trigger-picker")).Click();
            driver.FindElement(By.XPath($"//li[contains(text(),'"+conclusion+"')]")).Click();

            //return this;
        }

        public void ChoiceObjectRent(string numberObject)
        {
            //Выбрать объект аренды

            driver.FindElement(By.XPath("//div[@id='apptagselectfield-1089-trigger-find']")).Click();
            driver.FindElement(By.XPath("(//span[contains(text(),'Кадастровый номер')])[5]/following::div[@data-ref='inputWrap']//input")).SendKeys(numberObject);
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[@class='x-grid-checkcolumn']")).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'Выбрать')]")).Click();

            //return this;
        }

        public void ChoicedateEnd_OneYear(string data)
        {

            var elem = driver.FindElement(By.XPath("//input[@name='ActionDateEnd']"));
            //Thread.Sleep(3000);
            elem.Click();
            Thread.Sleep(7000);
            //elem.Submit();
            //Thread.Sleep(3000);
            elem.SendKeys(data);
            Thread.Sleep(3000);

            //return this;
        }

        public void ChoiceDateEnd_Today()
        {
            //Выбрать дату окончания действия ("Cегодня")

            driver.FindElement(By.Id("dateextendfield-1078-trigger-picker")).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'Сегодня')]")).Click();

            //return this;
        }

        public void NumberContract(string numberContract)
        {
            //Ввод номера в договоре

            driver.FindElement(By.Name("Number")).SendKeys(numberContract);

            //return this;
        }

        public void ButtonAdd()
        {
            //Кликнуть кнопку "Добавить"

            driver.FindElement(By.XPath("(//span[contains(text(),'Добавить')])[1]")).Click();

            //return this;
        }

        public void ClickOnContracts()
        {
            //Выбрать "договоры" в меню

            driver.FindElement(By.XPath("//div[@id='menuTreePanelItemId']//span[contains(text(),'Договоры')]/i")).Click();

            //return this;
        }
    }
}
