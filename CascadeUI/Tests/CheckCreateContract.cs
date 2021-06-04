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

        [TestCase(TestName = "#01 �������� �������� �������� - � �������� �����")]
        public void AddContract()
        {

            app.Contract.CreateContract(new ContractData("1000-�", "30042022", "16:05:011601:1006", "����������� ���������", "2"));
            //app.Contract
            //.ClickOnContracts()
            //.ButtonAdd()
            //.NumberContract(new ContractData("1000-dd"))
            //.ChoiceDateEnd_Today()
            //.ChoiceObjectRent(new ContractData("16:05:011601:1006"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Expired());
            app.Contract.CheckRecContract("1000-�");
            app.Contract.DeleteLastContract();
        }

        [TestCase(TestName = "#02 �������� �������� �������� - � ����� �����������")]
        public void AddContractTrueDate()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("4409-�", "16:05:011601:1006", "30.04.2022", "����������� ���������", "2"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Correct());
            app.Contract.CheckRecContract("4409-�");
            //app.Contract.CheckStatusContract_Correct();
            app.Contract.DeleteLastContract();
        }

        [TestCase(TestName = "#03 ������� �������� � ��������������")]
        public void RedactionContractClick()
        {
            app.Contract.RedactionContractClick();
            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#04 ������� �� ������� �������� - � �������� �� �������� ��� ���������")]
        public void StatusPaidContract()
        {
            app.Contract.StatusPaidContract();
            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#05 ������������ ����� ���������, �������� ������������ �����")]
        public void CheckAddClaimFile()
        {
            app.Contract.AddClaimPaidContract("17.06.2021", "16.09.2020", "21.09.2021");
            //Assert.IsTrue(app.Contract.CheckFile());
            app.Contract.ClickCreateFile();
            app.Contract.CheckDownloadFileClaim();

            Assert.IsTrue(app.Contract.CheckSave());
        }

        [TestCase(TestName = "#06 ���������� ��������� - � ������ � ��������")]
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

        [TestCase(TestName = "#07 ���������� ��������� - � ������ ")]
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

        [TestCase(TestName = "#08 �������������� ���������")]
        public void RedactionClaim()
        {
            app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
            app.Contract.ClickSaveButtonClaim();
            app.Contract.RedactionClaim();

            app.Contract.ChangeClaim("������������� � ���������");
            app.Contract.ClickSaveButtonClaim();

            app.Contract.RedactionClaim();
            Assert.IsTrue(app.Contract.ChangeNameClaim("������������� � ���������"));

        }

        [TestCase(TestName = "#09 ���������� ����")]
        public void AddLawSuit()
        {
            app.Contract.AddLawSuit();
        }
    }
}
