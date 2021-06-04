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
        public void AddContract()
        {

            app.Contract.CreateContract(new ContractData("1000-Т", "30042022", "16:05:011601:1006", "Кадастровая стоимость", "2"));
            //app.Contract
            //.ClickOnContracts()
            //.ButtonAdd()
            //.NumberContract(new ContractData("1000-dd"))
            //.ChoiceDateEnd_Today()
            //.ChoiceObjectRent(new ContractData("16:05:011601:1006"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Expired());
            app.Contract.CheckRecContract("1000-Т");
            app.Contract.DeleteLastContract();
        }

        [TestCase(TestName = "#02 Проверка создания договора - с датой действующей")]
        public void AddContractTrueDate()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("4409-Т", "16:05:011601:1006", "30.04.2022", "Кадастровая стоимость", "2"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Correct());
            app.Contract.CheckRecContract("4409-Т");
            //app.Contract.CheckStatusContract_Correct();
            app.Contract.DeleteLastContract();
        }

        [TestCase(TestName = "#03 Переход договора в редактирование")]
        public void RedactionContractClick()
        {
            app.Contract.RedactionContractClick();
            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#04 Переход во вкладку Претензи - у договора со статусом Нет претензии")]
        public void StatusPaidContract()
        {
            app.Contract.StatusPaidContract();
            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#05 Формирование файла претензии, проверка формирования файла")]
        public void CheckAddClaimFile()
        {
            app.Contract.AddClaimPaidContract("17.06.2021", "16.09.2020", "21.09.2021");
            //Assert.IsTrue(app.Contract.CheckFile());
            app.Contract.ClickCreateFile();
            app.Contract.CheckDownloadFileClaim();

            Assert.IsTrue(app.Contract.CheckSave());
        }

        [TestCase(TestName = "#06 Сохранение претензии - с файлом и удаление")]
        public void CheckAddClaimSaveAndDelete()
        {
            app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
            app.Contract.ClickCreateFile();
            app.Contract.CheckDownloadFileClaim();
            app.Contract.ClickSaveButtonClaim();

            app.Contract.DeleteClaim();
            //Assert.IsFalse(app.Contract.CheckSummNull());
            while (app.Contract.CheckSummNull() == true)
            {
                app.Contract.DeleteClaim();
            }
            //else
            //{
            //    app.Contract.CheckSummNull();
            //}

            Assert.IsFalse(app.Contract.CheckSummNull());
        }

        [TestCase(TestName = "#07 Сохранение претензии - с файлом ")]
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

        [TestCase(TestName = "#08 Редактирование претензии")]
        public void RedactionClaim()
        {
            app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
            app.Contract.ClickSaveButtonClaim();
            app.Contract.RedactionClaim();

            app.Contract.ChangeClaim("Задолженность и неустойка");
            app.Contract.ClickSaveButtonClaim();

            app.Contract.RedactionClaim();
            Assert.IsTrue(app.Contract.ChangeNameClaim("Задолженность и неустойка"));

        }

        [TestCase(TestName = "#09 Добавление иска")]
        public void AddLawSuit()
        {
            app.Contract.AddLawSuit();
        }
    }
}
