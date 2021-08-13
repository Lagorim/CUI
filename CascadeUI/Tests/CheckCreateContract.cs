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
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace CascadeUITest
{
    [TestFixture]
    [AllureNUnit]
    [AllureParentSuite("Договор")]
    [AllureSuite("Договор")]
    [AllureFeature("Договор")]
    public class CheckCreateContract : AuthBaseTest
    {
        
        //[OneTimeTearDown]
        //public void OneTimeTearDown()
        //{
        //    Driver.Dispose();
        //}

        [TestCase(TestName = "#01 Создание договора - с истекшей датой")]
        [Order(1)]
        public void AddContract()
        {
            
            app.Contract.CreateContract(new ContractData("16:05:011601:1006", "Кадастровая стоимость"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Expired());
        }

        [TestCase(TestName = "#02 Редактирование договора")]
        [Order(2)]
        public void RedactionContract()
        {
            app.Contract.RedactionContract();

            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#03 Удаление договора")]
        [Order(3)]
        public void DeleteContract()
        {
            app.Contract.DeleteLastContract();
            app.Contract.CheckRecContract();

            Assert.IsFalse(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#04 Проверка создания договора - с действующей датой")]
        [Order(4)]
        public void AddContractTrueDate()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "Аукцион"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Correct());
        }

        [TestCase(TestName = "#05 Обновление договоров")]
        [Order(5)]
        public void RefreshContract()
        {
            app.Contract.RefreshButton();
            //app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#06 Выгрузить договора")]
        [Order(6)]
        public void UnloadContract()
        {
            app.Contract.ClickOnContracts();

            app.Contract.ClickUnloadButton();
        }

        [TestCase(TestName = "#07 Проверка редактирования (функциональные кнопки)")]
        [Order(7)]
        public void RedactionContractFuncButton()
        {
            app.Contract.RedactionFuncButton();
            app.Contract.ChangeStatusContract();
            //app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.StatusTypeContractCheck());
        }

        [TestCase(TestName = "#08 Проверка удаления (функциональные кнопки)")]
        [Order(8)]
        public void DeleteContractFuncButton()
        {
            app.Contract.DeleteFuncButton();
            app.Contract.CheckRecContract();

            Assert.IsFalse(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#09 Клонирование договора (функциольные кнопки)")]
        [Order(9)]
        public void CloneContract()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "Кадастровая стоимость"));

            app.Contract.CloneContract();
            app.Contract.ForCloneContract_ObjectRent("16:05:011601:1006");
            app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.FilterFieldCheck() && app.Contract.FilterSecondStringCheck());
        }

        [TestCase(TestName = "#10 Добавление дополнительного соглашения (функциональные кнопки)")]
        [Order(10)]
        public void AddMoreContract()
        {
            app.Contract.AddMoreContract();
            app.Contract.ForCloneContract_ObjectRent("16:05:011601:1006");
            //app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.CheckStatusTypeContract());
        }

        [TestCase(TestName = "#11 Переход во вкладку Претензии - у договора со статусом Нет задолженности")]
        [Order(11)]
        public void StatusPaidContract()
        {
            app.Contract.StatusPaidContract();

            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#12 Формирование файла претензии, проверка формирования файла")]
        [Order(12)]
        public void CheckAddClaimFile()
        {
            app.Contract.AddClaimPaidContract("17.06.2021", "16.09.2020", "21.09.2021");
            app.Contract.ClickCreateFile();
            app.Contract.CheckDownloadFileClaim();

            Assert.IsTrue(app.Contract.CheckSave());

            app.Contract.ClickCancelButtonClaim();
        }

        [TestCase(TestName = "#13 Сохранение претензии - с файлом и удаление")]
        [Order(13)]
        public void CheckAddClaimSaveAndDelete()
        {
            app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
            app.Contract.ClickCreateFile();
            app.Contract.CheckDownloadFileClaim();
            app.Contract.ClickSaveButtonClaim();

            app.Contract.DeleteClaim();

            while (app.Contract.CheckSummNull() == true)
            {
                app.Contract.DeleteClaim();
            }

            Assert.IsFalse(app.Contract.CheckSummNull());
        }

        [TestCase(TestName = "#14 Сохранение претензии - с файлом")]
        [Order(14)]
        public void CheckAddClaimSave()
        {
            app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
            app.Contract.ClickCreateFile();
            app.Contract.CheckDownloadFileClaim();
            app.Contract.ClickSaveButtonClaim();

            Assert.IsTrue(app.Contract.CheckSummNull());

            app.Contract.DeleteClaim();
            while (app.Contract.CheckSummNull() == true)
            {
                app.Contract.DeleteClaim();
            }
        }

        [TestCase(TestName = "#15 Редактирование претензии")]
        [Order(15)]
        public void RedactionClaim()
        {
            app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
            app.Contract.ClickSaveButtonClaim();
            app.Contract.RedactionClaim();

            app.Contract.ChangeClaim("Задолженность и неустойка");
            app.Contract.ClickSaveButtonClaim();

            app.Contract.RedactionClaim();
            Assert.IsTrue(app.Contract.ChangeNameClaim("Задолженность и неустойка"));

            app.Contract.ClickCancelButtonClaim();
        }

        [TestCase(TestName = "#16 Добавление иска")]
        [Order(16)]
        public void AddLawSuit()
       {
            app.Contract.AddLawSuit();

            Assert.IsTrue(app.Contract.CheckSummNull());
        }

        [TestCase(TestName = "#17 Редактирование иска")]
        [Order(17)]
        public void ReadactionLawSuit()
        {
            app.Contract.RedactionLawSuit();

            Assert.IsTrue(app.Contract.CheckViewRequest());
        }

        [TestCase(TestName = "#18 Удаление иска")]
        [Order(18)]
        public void DeleteLawSuit()
        {
            app.Contract.DeleteLawSuit();

            Assert.IsFalse(app.Contract.CheckSummNull());
        }

        [TestCase(TestName = "#19 Проверка кнопок навигационной панели >")]
        [Order(19)]
        public void NavigationButtonNextPage()
        {
            app.Contract.ClickButtonNextPage();

            Assert.IsTrue(app.Contract.CheckWorkNextPage());
        }

        [TestCase(TestName = "#20 Проверка кнопок навигационной панели >> и <<")]
        [Order(20)]
        public void NavigationButtonLastPage()
        {
            app.Contract.ClickButtonLastPage();
            app.Contract.ClickButtonFirstPage();

            Assert.IsTrue(app.Contract.CheckWorkFirstPage());
        }
    }
}
