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
using Allure.Commons;
using NUnit.Allure.Core;
using NUnit.Allure.Attributes;

namespace CascadeUITest
{
    [TestFixture]
    [AllureNUnit]
    [AllureParentSuite("�������")]
    [AllureSuite("�������")]
    [AllureFeature("�������")]
    public class CheckCreateContract : AuthBaseTest
    {

        [TestCase(TestName = "#01 �������� �������� �������� - � �������� �����")]
        [Order(1)]
        public void AddContract()
        {

            app.Contract.CreateContract(new ContractData("16:05:011601:1006", "����������� ���������"));
            //app.Contract
            //.ClickOnContracts()
            //.ButtonAdd()
            //.NumberContract(new ContractData("1000-dd"))
            //.ChoiceDateEnd_Today()
            //.ChoiceObjectRent(new ContractData("16:05:011601:1006"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Expired());
            //app.Contract.DeleteLastContract();
            // ���� ��������� � ������ ������, �������� ���������

            //app.Contract.CheckRecContract();

        }

        [TestCase(TestName = "#02 �������������� ��������")]
        [Order(2)]
        public void RedactionContract()
        {
            //app.Contract.CreateContract(new ContractData("16:05:011601:1006", "����������� ���������"));

            app.Contract.RedactionContract();

            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#03 �������� ��������")]
        [Order(3)]
        public void DeleteContract()
        {
            //app.Contract.DeleteLastContract();
            //app.Contract.CreateContract(new ContractData("16:05:011601:1006", "�������"));

            app.Contract.DeleteLastContract();
            app.Contract.CheckRecContract();

            Assert.IsFalse(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#04 �������� �������� �������� - � ����������� ����� ")]
        [Order(4)]
        public void AddContractTrueDate()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "�������"));

            Assert.IsTrue(app.Contract.CheckStatusContract_Correct());
            //app.Contract.CheckRecContract();
            //app.Contract.CheckStatusContract_Correct();

            // ���� ��������� � ������ ������, �������� ���������

            //app.Contract.DeleteLastContract();
        }

        [TestCase(TestName = "#05 ���������� ���������")]
        [Order(5)]
        public void RefreshContract()
        {
            //app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "�������"));

            app.Contract.RefreshButton();
            app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#06 ��������� ��������")]
        [Order(6)]
        public void UnloadContract()
        {
            app.Contract.ClickOnContracts();

            app.Contract.ClickUnloadButton();
        }

        [TestCase(TestName = "#07 �������� �������������� (�������������� ������)")]
        [Order(7)]
        public void RedactionContractFuncButton()
        {
            //app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "�������"));

            app.Contract.RedactionFuncButton();
            app.Contract.ChangeStatusContract();
            app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.StatusTypeContractCheck());
        }

        [TestCase(TestName = "#08 �������� �������� (�������������� ������)")]
        [Order(8)]
        public void DeleteContractFuncButton()
        {
            //app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "�������"));

            app.Contract.DeleteFuncButton();
            app.Contract.CheckRecContract();

            Assert.IsFalse(app.Contract.FilterFieldCheck());
        }

        [TestCase(TestName = "#09 ������������ �������� (������������ ������)")]
        [Order(9)]
        public void CloneContract()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "����������� ���������"));

            app.Contract.CloneContract();
            app.Contract.ForCloneContract_ObjectRent("16:05:011601:1006");
            app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.FilterFieldCheck() && app.Contract.FilterSecondStringCheck());
        }

        [TestCase(TestName = "#10 ���������� ��������������� ���������� (�������������� ������)")]
        [Order(10)]
        public void AddMoreContract()
        {
            //app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "����������� ���������"));

            app.Contract.AddMoreContract();
            app.Contract.ForCloneContract_ObjectRent("16:05:011601:1006");
            app.Contract.CheckRecContract();

            Assert.IsTrue(app.Contract.CheckStatusTypeContract());
        }

        [TestCase(TestName = "#11 ������� �� ������� ��������� - � �������� �� �������� ��� �������������")]
        [Order(11)]
        public void StatusPaidContract()
        {
            app.Contract.StatusPaidContract();
            Assert.IsTrue(app.Contract.ButtonList());
        }

        [TestCase(TestName = "#12 ������������ ����� ���������, �������� ������������ �����")]
        [Order(12)]
        public void CheckAddClaimFile()
        {
            app.Contract.AddClaimPaidContract("17.06.2021", "16.09.2020", "21.09.2021");
            //Assert.IsTrue(app.Contract.CheckFile());
            app.Contract.ClickCreateFile();
            app.Contract.CheckDownloadFileClaim();

            Assert.IsTrue(app.Contract.CheckSave());

            app.Contract.ClickCancelButtonClaim();
        }

        [TestCase(TestName = "#13 ���������� ��������� - � ������ � ��������")]
        [Order(13)]
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

        [TestCase(TestName = "#14 ���������� ��������� - � ������ ")]
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

        [TestCase(TestName = "#15 �������������� ���������")]
        [Order(15)]
        public void RedactionClaim()
        {
            app.Contract.AddClaimPaidContract("20.05.2021", "10.04.2020", "20.04.2021");
            app.Contract.ClickSaveButtonClaim();
            app.Contract.RedactionClaim();

            app.Contract.ChangeClaim("������������� � ���������");
            app.Contract.ClickSaveButtonClaim();

            app.Contract.RedactionClaim();
            Assert.IsTrue(app.Contract.ChangeNameClaim("������������� � ���������"));

            app.Contract.ClickCancelButtonClaim();
        }

        [TestCase(TestName = "#16 ���������� ����")]
        [Order(16)]
        public void AddLawSuit()
        {
            app.Contract.AddLawSuit();

            Assert.IsTrue(app.Contract.CheckSummNull());
        }

        [TestCase(TestName = "#17 �������������� ����")]
        [Order(17)]
        public void ReadactionLawSuit()
        {
            app.Contract.RedactionLawSuit();

            Assert.IsTrue(app.Contract.CheckViewRequest());
        }

        [TestCase(TestName = "#18 �������� ����")]
        [Order(18)]
        public void DeleteLawSuit()
        {
            app.Contract.DeleteLawSuit();

            Assert.IsFalse(app.Contract.CheckSummNull());
        }

        [TestCase(TestName = "#19 �������� ������ ������������� ������ >")]
        [Order(19)]
        public void NavigationButtonNextPage()
        {
            app.Contract.ClickButtonNextPage();

            Assert.IsTrue(app.Contract.CheckWorkNextPage());
        }

        [TestCase(TestName = "#20 �������� ������ ������������� ������ >> � <<")]
        [Order(20)]
        public void NavigationButtonLastPage()
        {
            app.Contract.ClickButtonLastPage();
            app.Contract.ClickButtonFirstPage();


            Assert.IsTrue(app.Contract.CheckWorkFirstPage());
        }
    }
}
