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
    [TestFixture]
    public class CheckCreateContract : AuthBaseTest
    {

        [TestCase(TestName = "#01 Проверка создания договора - с истекшей датой")]
        [Order(1)]
        public void AddContract()
        {

            app.Contract.CreateContract(new ContractData("16:05:011601:1006", "Кадастровая стоимость"));
            //app.Contract
            //.ClickOnContracts()
            //.ButtonAdd()
            //.NumberContract(new ContractData("1000-dd"))
            //.ChoiceDateEnd_Today()
            //.ChoiceObjectRent(new ContractData("16:05:011601:1006"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Expired());

            // надо применить в других тестах, проверка избыточна

            //app.Contract.CheckRecContract();
            
        }

        [TestCase(TestName = "#02 Редактирование договора")]
        [Order(2)]
        public void RedactionContract()
        {
            app.Contract.CreateContract(new ContractData("16:05:011601:1006", "Кадастровая стоимость"));

            app.Contract.RedactionContract();

            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#03 Удаление договора")]
        [Order(3)]
        public void DeleteContract()
        {
            app.Contract.CreateContract(new ContractData("16:05:011601:1006", "Аукцион"));

            app.Contract.DeleteLastContract();
            app.Contract.CheckRecContract();

            Assert.IsFalse(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#04 Обновление договоров")]
        [Order(4)]
        public void RefreshContract()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "Аукцион"));

            app.Contract.RefreshButton();
            app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#05 Выгрузить договора")]
        [Order(5)]
        public void UnloadContract()
        {           
            app.Contract.ClickOnContracts();

            app.Contract.ClickUnloadButton();
        }

        [TestCase(TestName = "#06 Клонирование договора")]
        [Order(6)]
        public void CloneContract()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "Кадастровая стоимость"));

            app.Contract.CloneContract();
            app.Contract.ForCloneContract_ObjectRent("16:05:011601:1006");
            app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.FilterFieldCheck() && app.Contract.FilterSecondStringCheck());
        }


        //[TestCase(TestName = "#02 Проверка создания договора - с датой действующей")]
        //[Order(2)]
        //public void AddContractTrueDate()
        //{
        //    app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "Кадастровая стоимость"));

        //    Assert.IsTrue(app.Contract.CheckStatusContract_Correct());
        //    app.Contract.CheckRecContract();
        //    //app.Contract.CheckStatusContract_Correct();

        //    // надо применить в других тестах, проверка избыточна

        //    //app.Contract.DeleteLastContract();
        //}

        //[TestCase(TestName = "#03 Переход договора в редактирование")]
        //[Order(3)]
        //public void RedactionContractClick()
        //{
        //    app.Contract.RedactionContractClick();
        //    Assert.IsTrue(app.Contract.ButtonList());
        //}

        //[TestCase(TestName = "#04 Переход во вкладку Претензи - у договора со статусом Нет претензии")]
        //[Order(4)]
        //public void StatusPaidContract()
        //{
        //    app.Contract.StatusPaidContract();
        //    Assert.IsTrue(app.Contract.ButtonList());
        //}

        //[TestCase(TestName = "#05 Формирование файла претензии, проверка формирования файла")]
        //[Order(5)]
        //public void CheckAddClaimFile()
        //{
        //    app.Contract.AddClaimPaidContract("17.06.2021", "16.09.2020", "21.09.2021");
        //    //Assert.IsTrue(app.Contract.CheckFile());
        //    app.Contract.ClickCreateFile();
        //    app.Contract.CheckDownloadFileClaim();

        //    Assert.IsTrue(app.Contract.CheckSave());
        //}

        //[TestCase(TestName = "#06 Сохранение претензии - с файлом и удаление")]
        //[Order(6)]
        //public void CheckAddClaimSaveAndDelete()
        //{
        //    app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
        //    app.Contract.ClickCreateFile();
        //    app.Contract.CheckDownloadFileClaim();
        //    app.Contract.ClickSaveButtonClaim();

        //    app.Contract.DeleteClaim();
        //    //Assert.IsFalse(app.Contract.CheckSummNull());
        //    while (app.Contract.CheckSummNull() == true)
        //    {
        //        app.Contract.DeleteClaim();
        //    }
        //    //else
        //    //{
        //    //    app.Contract.CheckSummNull();
        //    //}

        //    Assert.IsFalse(app.Contract.CheckSummNull());
        //}

        //[TestCase(TestName = "#07 Сохранение претензии - с файлом ")]
        //[Order(7)]
        //public void CheckAddClaimSave()
        //{
        //    app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
        //    app.Contract.ClickCreateFile();
        //    app.Contract.CheckDownloadFileClaim();
        //    app.Contract.ClickSaveButtonClaim();



        //    Assert.IsTrue(app.Contract.CheckSummNull());

        //    app.Contract.DeleteClaim();
        //    while (app.Contract.CheckSummNull() == true)
        //    {
        //        app.Contract.DeleteClaim();
        //    }
        //}

        //[TestCase(TestName = "#08 Редактирование претензии")]
        //[Order(8)]
        //public void RedactionClaim()
        //{
        //    app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
        //    app.Contract.ClickSaveButtonClaim();
        //    app.Contract.RedactionClaim();

        //    app.Contract.ChangeClaim("Задолженность и неустойка");
        //    app.Contract.ClickSaveButtonClaim();

        //    app.Contract.RedactionClaim();
        //    Assert.IsTrue(app.Contract.ChangeNameClaim("Задолженность и неустойка"));

        //}

        //[TestCase(TestName = "#09 Добавление иска")]
        //[Order(9)]
        //public void AddLawSuit()
        //{
        //    app.Contract.AddLawSuit();
        //}
    }
}
