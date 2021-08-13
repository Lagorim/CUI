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

        //private string numberContract = "Тест-" + new Random().Next(10, 10000).ToString();
        //private string salary = new Random().Next(10, 3000000).ToString();

        public void CreateContract(ContractData contract)
        {
            //Создание договора
            driver = manager.Driver;

            driver.Navigate().Refresh();
            ClickOnContracts();
            ButtonAdd();
            NumberContract(numberContract);
            ChoiceDateEnd_Today();
            ChoiceObjectRent(contract.NumberObject);
            ChoiceTypeConclusion(contract.Conclusion);
            ChoiceDatePay(value);
            //try
            //{
            //    ClickSave();
            //    driver.FindElement(By.XPath("//div[contains(text(),'Ошибка! Поле Арендаторы обязательное поле для заполнения')]"));
            //}
            //catch
            //{
            AddTenant();
            //}
            Contragent();
            
            DateStartContragentForm();
            SalaryContragent(salary);
            ClickSave();
            Thread.Sleep(2000);

            //return this;
        }

        public void CreateContractTrueStatus(ContractData contract)
        {
            driver = manager.Driver;
            ClickOnContracts();
            ButtonAdd();
            NumberContract(numberContract);
            ChoiceDate_Date(dateConlusion);
            ChoiceDateStart_ActionDateStart(dateConlusion);
            ChoicedateEnd_OneYear(dateEndContract);
            ChoiceObjectRent(contract.NumberObject);
            ChoiceTypeConclusion(contract.Conclusion);
            ChoiceDatePay(value);
            //try
            //{
            //    ClickSave();
            //    driver.FindElement(By.XPath("//div[contains(text(),'Ошибка!')]"));
            //}
            //catch
            //{
                AddTenant();
            //}
            Contragent();
            DateStartContragentForm();
            SalaryContragent(salary);
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
            driver = manager.Driver;

            Thread.Sleep(5000);
            StatusPaid();
            Thread.Sleep(6000);
            RedactionClickButton();
            ClickLawSuit();
            Thread.Sleep(6000);
        }

        public void AddClaimPaidContract(string dateClaim, string dateStart, string dateEnd)
        {
            //Добавление претензии в статусе договора "Нет задолженности"
            driver = manager.Driver;

            driver.Navigate().Refresh();
            StatusPaidContract();
            ClickButoonAddClaimForm();

            ClaimSigned();
            ClaimTenant();
            DateClaim(dateClaim);
            DateClaimWith(dateStart);
            DateClaimEnd(dateEnd);
            OutComingDate();
            //ClickCreateFile();
            //Thread.Sleep(6000);
        }

        public void AddClaimPaidContract_LawSuit(string dateClaim, string dateStart, string dateEnd)
        {
            driver = manager.Driver;

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
            if (!CheckSelectClaim())
            {               
                CloseDialogLawSuit();
                CloseFormLawSuit();
                ClickButtonList();
                AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
                ClickSaveButtonClaim();
                driver.Navigate().Refresh();
                ClickLawSuit();
                ClickButtonAddLawSuit();
                SelectClaim();

                if (CheckSelectClaim())
                {
                    ClickSelectClaim();
                    SelectChoiceLawSuit();
                    SelectDateRefferalToCourt();
                    SelectSaveLawSuit();
                    Thread.Sleep(3000);
                }
            }
            Thread.Sleep(3000);
            ClickSelectClaim();
            SelectChoiceLawSuit();
            SelectDateRefferalToCourt();
            SelectSaveLawSuit();
            Thread.Sleep(3000);
        }

        public void RedactionLawSuit()
        {
            driver = manager.Driver;

            StatusPaidContract_LawSuit();

            if (!CheckLawSuit())
            {
                ClickButtonList();
                AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
                ClickSaveButtonClaim();
                driver.Navigate().Refresh();
                ClickLawSuit();
                ClickButtonAddLawSuit();
                SelectClaim();
                ClickSelectClaim();
                SelectChoiceLawSuit();
                SelectDateRefferalToCourt();
                SelectSaveLawSuit();
            }

            if (CheckLawSuit())
            {
                Thread.Sleep(3000);
                SelectLawSuit();
                ClickRedactionButtonLawSuit();
                ChangeViewRequest();
                SelectSaveLawSuit();
                
            }
            Thread.Sleep(3000);
            return;
        }

        public void DeleteLawSuit()
        {
            driver = manager.Driver;

            StatusPaidContract_LawSuit();

            //driver.Navigate().Refresh();
            
            //ClickLawSuit();
            //Thread.Sleep(3000);
            if (!CheckSummNull())
            {
                ClickButtonList();
                AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
                ClickSaveButtonClaim();
                driver.Navigate().Refresh();
                ClickLawSuit();
                ClickButtonAddLawSuit();
                SelectClaim();
                ClickSelectClaim();
                SelectChoiceLawSuit();
                SelectDateRefferalToCourt();
                SelectSaveLawSuit();

                Thread.Sleep(3000);
                while (CheckSummNull() == true)
                {
                    //driver.Navigate().Refresh();
                    //ClickLawSuit();
                    SelectAnyLawSuit();
                    ClickDeleteButtonLawSuit();
                    CloseWindowAttentionForDelete();
                    Thread.Sleep(3000);
                }
            }

            if (CheckSummNull() || CheckLawSuit())
            {
                while (CheckSummNull() == true)
                {
                    //driver.Navigate().Refresh();
                    //ClickLawSuit();
                    SelectAnyLawSuit();
                    ClickDeleteButtonLawSuit();
                    CloseWindowAttentionForDelete();
                    Thread.Sleep(3000);

                }

            }
            Thread.Sleep(3000);
            return;
        }

        public void ClickButtonNextPage()
        {
            //клик по кнопке навигационной панели >
            
            driver = manager.Driver;

            driver.FindElement(By.XPath("//a[@data-qtip='Следующая страница']")).Click();
            Thread.Sleep(2000);
        }

        public void ClickButtonLastPage()
        {
            //клик по кнопке навигационной панели >>
            driver = manager.Driver;

            driver.FindElement(By.XPath("//a[@data-qtip='Последняя страница']")).Click();
            Thread.Sleep(2000);
        }

        public void ClickButtonFirstPage()
        {
            //клик по кнопке навигационной панели <<
            driver = manager.Driver;

            driver.FindElement(By.XPath("//a[@data-qtip='Первая страница']")).Click();
            Thread.Sleep(2000);
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
            driver = manager.Driver;

            driver.FindElement(By.XPath("//span[contains(text(),'Сформировать файл')]")).Click();
            Thread.Sleep(6000);
        }

        public void ClickSaveButtonClaim()
        {
            //клик по кнопке "Сохранить"

            driver.FindElement(By.XPath("//div[@role='dialog']//div[@role='toolbar']//span[contains(text(),'Сохранить')]")).Click();
            Thread.Sleep(2000);
        }

        public void ClickCancelButtonClaim()
        {
            //клик по кнопке "Отменить"
            driver = manager.Driver;

            driver.FindElement(By.XPath("//div[@role='dialog']//div[@role='toolbar']//span[contains(text(),'Отменить')]")).Click();
        }

        public void DeleteClaim()
        {
            //удаление Претензии
            driver = manager.Driver;

            Thread.Sleep(5000);
            driver.FindElement(By.XPath("(//div[@class='x-grid-item-container'])[6]//tr[@class='  x-grid-row']")).Click();
            driver.FindElement(By.XPath("(//div[@class='x-panel-bodyWrap']//span[contains(text(),'Удалить')])[7]")).Click();
            Thread.Sleep(5000);
            CloseWindowClaimDelete();
            Thread.Sleep(4000);
        }

        public void OutComingDate()
        {
            //Проставление даты "исход. письма"

            var dateOutcoming = DateTime.Now.AddDays(-30).ToString("ddMMyyyy");

            var elem = driver.FindElement(By.XPath("(//input[@name='OutcomingDate'])"));
            elem.Click();
            Thread.Sleep(7000);
            elem.SendKeys(dateOutcoming);
            Thread.Sleep(3000);
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

            driver = manager.Driver;

            Thread.Sleep(5000);
            driver.FindElement(By.XPath("(//div[@class='x-grid-item-container'])[6]//tr[@class='  x-grid-row']")).Click();
            driver.FindElement(By.XPath("(//div[@class='x-panel-bodyWrap']//span[contains(text(),'Редактировать')])[2]")).Click();
            Thread.Sleep(5000);

        }

        public void ChangeClaim(string conclusion)
        {
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(text(),'Требование:')]/following::div[@class='x-form-trigger x-form-trigger-default x-form-arrow-trigger x-form-arrow-trigger-default  ']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath($"//li[contains(text(),'" + conclusion + "')]")).Click();
            Thread.Sleep(3000);
        }

        public bool ChangeNameClaim(string conclusion)
        {
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//input[@name='Requirement']")).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'Требование:')]/following::div[@class='x-form-trigger x-form-trigger-default x-form-arrow-trigger x-form-arrow-trigger-default  ']")).Click();
            //Thread.Sleep(3000);
            return IsElementPresent(By.XPath($"//li[contains(text(),'" + conclusion + "')]"));
        }

        public void CheckDownloadFileClaim()
        {
            driver = manager.Driver;
            //driver.Navigate().GoToUrl("/home/selenium/Downloads/");

            string expectedFile = @"/home/selenium/Downloads/download";
            bool exist = false;
            //string Path = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";
           

            //var reader = new ConfigReader();
            ChromeOptions options = new ChromeOptions();
            options.AddUserProfilePreference("download.default_directory", "/home/selenium/Downloads");

            FileInfo fileinfo = new FileInfo(expectedFile);
            Assert.AreEqual(fileinfo.Name, "download");
            //Assert.AreEqual(fileinfo.FullName, @"C:\home\selenium\Downloads\download");
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

            driver.FindElement(By.XPath("//div[contains(text(),'Предупреждение')]/following::span[contains(text(),'Да')]")).Click();
        }

        #endregion


        #region Methods for LawSuit //Методы для составления исков

        public void AddTenantInLawSuit(string fullName, string orgForm, string jurAddress, string postAddress)
        {
            //добавление "Арендатора" в исках 
            driver = manager.Driver;

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

            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//span[contains(text(),'Иски')]")).Click();
        }

        public void ClickButtonAddLawSuit()
        {
            //клик по кнопке "добавить" в исках

            driver.FindElement(By.XPath("(//div[contains(text(),'Договоры /')]/following::div[@data-ref='bodyWrap']//a//span[contains(text(),'Добавить')])[3]")).Click();
            Thread.Sleep(3000);
        }

        public void SelectClaim()
        {
            //клик по кнопке "Претензия" и ее выбор, в форме добавления Исков

            driver.FindElement(By.XPath("(//span[contains(text(),'Претензия:')]/following::div[@role='presentation'])[4]")).Click();
            Thread.Sleep(3000);
        }

        public void SelectTenantInFormLawSuit()
        {
            //клик по кнопке "Арендатор" и ее выбор, в форме добавления Исков
            driver = manager.Driver;

            driver.FindElement(By.XPath("((//span[@class='x-form-item-label-text'][contains(text(),'Арендатор:')])[3]/following::div[@role='presentation'])[4]")).Click();
        }

        public void ClickSelectClaim()
        {
            //клик в окошечке для чек-бокса (выбор доступных претензий)
            
            driver = manager.Driver;

            Thread.Sleep(3000);
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

        public void SelectChoiceLawSuit()
        {
            //клик по кнопке "Выбрать" на форме "Выбор элемента" (Иски)

            driver.FindElement(By.XPath("//div[@role='dialog']/following::span[contains(text(),'Выбрать')]")).Click();
        }

        public void SelectSaveLawSuit()
        {
            //клик по кнопке "Сохранить" в форме составления иска

            driver.FindElement(By.XPath("//div[@role='dialog']//span[contains(text(),'Сохранить')]")).Click();
        }

        public void SelectDateRefferalToCourt()
        {
            //выбор даты "направления в суд" (Иски)

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("//input[@name='LawsuitSendDocDate1']"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(3000);
            elementLocator.SendKeys(DateTime.Today.ToString());
            Thread.Sleep(3000);

            //driver.FindElement(By.XPath("")).Click();
        }

        public void SelectLawSuit()
        {
            //клик по строчке с задолженностью

            driver.FindElement(By.XPath("//div[contains(text(),'Задолженность')]")).Click();
        }

        public void SelectAnyLawSuit()
        {
            //выбрать любую строчку в спсике доступныз исков

            driver.FindElement(By.XPath("//span[contains(text(),'Вид требования')]/following::tr[@class='  x-grid-row']")).Click();
            
        }

        public void ClickRedactionButtonLawSuit()
        {
            //клик по кнопке "Редактировать" в исках

            driver.FindElement(By.XPath("(//div[@data-ref='innerCt']/following::span[contains(text(),'Редактировать')])[2]")).Click();
        }

        public void ClickDeleteButtonLawSuit()
        { 
            //клик по кнопке "Удалить" в исках

            driver.FindElement(By.XPath("(//div[@data-ref='innerCt']/following::span[contains(text(),'Удалить')])[7]")).Click();
            Thread.Sleep(2000);
        }

        public void ChangeViewRequest()
        {
            //изменение "типа требования" в форме иска

            driver.FindElement(By.XPath("//input[@name='RequirementKind']")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//li[contains(text(),'Заявление требований кредитора (банкротство)')]")).Click();
        }

        public bool CheckSelectClaim()
        {
            //проверка наличия чек-бокс в Исках

            return IsElementPresent(By.XPath("//span[@class='x-grid-checkcolumn']"));
        }

        public bool CheckLawSuit()
        {
            //проверка наличия строчки с задолженностью

            return IsElementPresent(By.XPath("//div[contains(text(),'Задолженность')]"));
        }

        public bool CheckViewRequest()
        {
            //проверка наличия строки с видом требования "Заявление требований кредитора (банкротство)"

            return IsElementPresent(By.XPath("//div[contains(text(),'Заявление требований кредитора (банкротство)')]"));
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

        public void StatusContractClaim()
        {
            //найти договор со статусом "Требует направление претензии"
            driver = manager.Driver;

            driver.FindElement(By.XPath("//span[contains(text(),'Требует направление претензии')]")).Click();
        }

        public void RedactionContract()
        {
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(3000);

            driver.FindElement(By.XPath("//span[contains(text(),'Редактировать')]")).Click();
        }

        public void RedactionClickButton()
        {
            //клик по кнопке "Редактировать"

            driver.FindElement(By.XPath("//span[contains(text(),'Редактировать')]")).Click();
        }

        public void ClickButtonList()
        {
            //клик по кнопке "К списку"
            driver = manager.Driver;

            driver.FindElement(By.XPath("//span[contains(text(),'К списку')]")).Click();
            Thread.Sleep(3000);
        }

        public bool ButtonList()
        {
            //поиск кнопки "К списку"

            return IsElementPresent(By.XPath("//span[contains(text(),'К списку')]"));
        }

        public void DeleteLastContract()
        {
            //удаление последнего договора
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(3000);

            //Actions actions = new Actions(driver);
            //IWebElement elementLocator = driver.FindElement(By.XPath("(//a[@role='button']//span[contains(text(),'Удалить')])[1]"));
            //actions.DoubleClick(elementLocator);
            
            driver.FindElement(By.XPath("(//div[@role='toolbar']//span[contains(text(),'Удалить')])[1]")).Click();
            Thread.Sleep(8000);
            CloseWindowAttentionForDelete();
        }

        public void CloseWindowAttentionForDelete()
        {
            //подтверждение удаления - кнопка "Да"

            driver.FindElement(By.XPath("//div[contains(text(),'Предупреждение')]/following::span[contains(text(),'Да')]")).Click();
            //return this;
        }

        public void CheckRecContract(/*string numberContract*/) // стринг и драйвер после отладки убрать
        {
            //внесение "номер договора" в поле фильтра "номер договора" 
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//input[@data-ref='inputEl'])[3]")).SendKeys(numberContract);
            Thread.Sleep(3000);
            //return this;
        }

        public bool FilterFieldCheck()
        {
            //проверка значения фильтра
            Thread.Sleep(1000);
            return IsElementPresent(By.XPath("(//div[@class='x-grid-cell-inner '])[3]"));
        }

        public bool FilterSecondStringCheck()
        {
            //проверка значения поля фильтра "номер договора" для второй строчки. 
            Thread.Sleep(1000);
            return IsElementPresent(By.XPath("((//table[@role='presentation'])[2]//div[@class='x-grid-cell-inner '])[3]"));
        }

        public bool CheckStatusContract_Correct()
        {
            //Проверка статуса договора (Действующий)
            Thread.Sleep(1000);
            return IsElementPresent(By.XPath("//div[contains(text(),'Действующий')]"));
        }

        public bool CheckStatusContract_Expired()
        {
            //Проверка статуса договора (истек срок)
            Thread.Sleep(1000);
            return IsElementPresent(By.XPath("//div[contains(text(),'Истек срок')]"));             
        }

        public bool CheckStatusTypeContract()
        {
            //Проверка статуса тип договора (Доп.соглашение)
            Thread.Sleep(1000);
            return IsElementPresent(By.XPath("//div[contains(text(),'Доп. соглашение')]"));
        }

        public void SalaryContragent(string salary)
        {
            //Ввод суммы (форма контрагента)

            driver.FindElement(By.XPath("(//input[@name='Amount'])[3]")).SendKeys(salary);
            driver.FindElement(By.XPath("//div[contains(text(),'Арендатор')]/following::span[contains(text(),'Сохранить')]")).Click();

            //return this;
        }

        public bool StatusTypeContractCheck()
        {
            //проверка статуса типа договора

            return IsElementPresent(By.XPath("//div[contains(text(),'На проверке')]"));
        }

        public bool CheckWorkNextPage()
        {
            //проверка отбражения следующей страницы пр нажатии на кнопку > (навигац. панель)

            return IsElementPresent(By.XPath("//div[contains(text(),'Данные на отображение 31 - 60')]"));
        }

        public bool CheckWorkFirstPage()
        {
            //проверка отбражения страницы пр нажатии на кнопку << (навигац. панель)

            return IsElementPresent(By.XPath("//div[contains(text(),'Данные на отображение 1 - 30')]"));
        }

        //public bool CheckWorkLastPage()
        //{


        //    return IsElementPresent(By.XPath("//a[@data-qtip='Последняя страница']"));
        //}

        public void DateStartContragentForm()
        {
            //Выбрать дату начала расчета (на форме выбора контрагента)

            var date = DateTime.Today.ToString("ddMMyyyy");

            var elem = driver.FindElement(By.XPath("(//input[@name='Date'])[4]"));
            elem.Click();
            Thread.Sleep(7000);
            //elem.Submit();
            //Thread.Sleep(3000);
            elem.SendKeys(date);
            Thread.Sleep(3000);
            //Thread.Sleep(6000);
            //driver.FindElement(By.XPath("(//div[@data-ref='footerEl'])[2]//span[contains(text(),'Сегодня')]")).Click();

            //return this;
        }

        public void Contragent()
        {
            //Найти контрагента

            //Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div//span[contains(text(),'Контрагент:')]/following::div[@role='presentation'])[4]")).Click();
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

        //public void ClickSaveButtonTenant()
        //{
        //    //клик по копке "Сохранить" в форме "Арендатор"


        //    //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //    //IWebElement result = wait.Until(e => e.FindElement(By.XPath("//div[contains(text(),'Арендатор')]/following::span[contains(text(),'Сохранить')]")));

        //    //result.Click();

        //    driver.FindElement(By.XPath("//div[contains(text(),'Арендатор')]/following::span[contains(text(),'Сохранить')]")).Click();
        //}

        public void ClickSave()
        {
            //клик по кнопке "Сохранить"

            driver.FindElement(By.XPath("//span[contains(text(),'Сохранить')]")).Click();

            //return this;
        }

        public void ChoiceDatePay(string value)
        {
            //Выбрать "Расчетную дату оплаты"

            driver.FindElement(By.XPath("(//label//span[contains(text(),'Расчетная дата оплаты')]/following::div//div[@role='presentation'])[3]")).Click();
            Thread.Sleep(6000);
            //driver.FindElement(By.XPath("(//div[@data-ref='listWrap']//ul[@class='x-list-plain']//li[@role='option'])['"+value+"']")).Click();
            driver.FindElement(By.XPath("//li[normalize-space()='"+value+"']")).Click();

            //return this;
        }

        public void ChoiceTypeConclusion(string conclusion)
        {
            //Выбрать "Основания заключения"

            driver.FindElement(By.XPath("(//label//span[contains(text(),'Основание заключения')]/following::div//div[@role='presentation'])[3]")).Click();
            driver.FindElement(By.XPath($"//li[contains(text(),'"+conclusion+"')]")).Click();

            //return this;
        }

        public void ChoiceObjectRent(string numberObject)
        {
            //Выбрать объект аренды

            driver.FindElement(By.XPath("(//label//span[contains(text(),'Объекты аренды')]/following::div[@data-ref='triggerWrap']//div[@role='presentation'])[4]")).Click();
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

        public void ChoiceDateStart_ActionDateStart(string dateStartAction)
        {
            //Дата начала дествия

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("//span[contains(text(),'Дата начала действия:')]/following::input[@name='ActionDateStart']"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(3000);
            elementLocator.SendKeys(dateStartAction);
            Thread.Sleep(3000);
        }

        public void ChoiceDate_Date(string dateConclusion)
        {
            //Дата заключения

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("(//span[contains(text(),'Дата заключения:')]/following::input[@name='Date'])[1]"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(3000);
            elementLocator.SendKeys(dateConclusion);
            Thread.Sleep(3000);
        }

        public void ChoiceDateEnd_Today()
        {
            //Выбрать дату окончания действия ("Cегодня")

            var actionDateEnd = DateTime.Today.ToString("ddMMyyyy");

            Actions actions = new Actions(driver);
            IWebElement elementLocator = driver.FindElement(By.XPath("(//span[contains(text(),'Дата окончания действия:')]/following::input[@name='ActionDateEnd'])[1]"));
            actions.DoubleClick(elementLocator).Perform();
            Thread.Sleep(3000);
            elementLocator.SendKeys(actionDateEnd);
            Thread.Sleep(3000);



            //driver.FindElement(By.Id("dateextendfield-1078-trigger-picker")).Click();
            //driver.FindElement(By.XPath("//span[contains(text(),'Сегодня')]")).Click();

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
            driver = manager.Driver;

            driver.Navigate().Refresh();
            driver.FindElement(By.XPath("//div[@id='menuTreePanelItemId']//span[contains(text(),'Договоры')]/i")).Click();

            //driver.FindElement(By.XPath("//div[@class='thumb-wrap']//span[contains(text(),'Договоры')]")).Click();

            //return this;
        }

        public void RefreshButton()
        {
            //кнопка "Обновить" в шапке хэдера

            driver = manager.Driver;
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[@role='toolbar']//span[contains(text(),'Обновить')])[1]")).Click();
        }

        public void ClickUnloadButton()
        {
            //кнопка "Выгрузить" в шапке хэдера

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//div[@role='toolbar']//span[contains(text(),'Выгрузить')]")).Click();
        }

        public  void CloneContract()
        {
            //driver = manager.Driver;

            //клонирование договора (функц.кнопка)

            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[contains(@class,'x-grid-cell-inner x-grid-cell-inner-action-col')])[1]")).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'Клонировать договор')]")).Click();    

        }

        public void AddMoreContract()
        {
            //добавление доп.соглашения (функ.кнопка)
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[contains(@class,'x-grid-cell-inner x-grid-cell-inner-action-col')])[1]")).Click();
            driver.FindElement(By.XPath("//span[contains(text(),'Добавить доп. соглашение')]")).Click();
        }

        public void RedactionFuncButton()
        {
            //Редактирвоание (функц.кнопка)
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[contains(@class,'x-grid-cell-inner x-grid-cell-inner-action-col')])[1]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//a[@data-ref='itemEl']//span[contains(text(),'Редактировать')]")).Click();
            Thread.Sleep(3000);
        }

        public void DeleteFuncButton()
        {
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.Navigate().Refresh();
            Thread.Sleep(5000);
            driver.FindElement(By.XPath("//tr[@class='  x-grid-row']")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.XPath("(//div[contains(@class,'x-grid-cell-inner x-grid-cell-inner-action-col')])[1]")).Click();
            driver.FindElement(By.XPath("//a[@data-ref='itemEl']//span[contains(text(),'Удалить')]")).Click();
            Thread.Sleep(3000);
            CloseWindowAttentionForDelete();
        }

        public void ChangeStatusContract()
        {
            driver = manager.Driver;

            Thread.Sleep(3000);
            driver.FindElement(By.XPath("//span[contains(text(),'Статус договора:')]/following::input[1]")).Click();
            Thread.Sleep(1000);
            driver.FindElement(By.XPath("//li[contains(text(),'На проверке')]")).Click();
            Thread.Sleep(1000);
            ClickSave();
            Thread.Sleep(4000);
        }

        public void ForCloneContract_ObjectRent(string numberObject)
        {
            driver = manager.Driver;

            Thread.Sleep(3000);
            ChoiceObjectRent(numberObject);
            ClickSave();
        }

        //public void t()
        //{
        //   var e = driver.FindElement(By.XPath("(//tr[contains(@aria-selected,'true')]//div[contains(@class,'x-grid-cell-inner')])[3]"));
        //    e.Click();
            
        //}
    }
}
