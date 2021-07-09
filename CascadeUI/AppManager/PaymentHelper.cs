using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace CascadeUITest
{
    public class PaymentHelper : BaseHelper
    {
        private string summ;
        private string day;
        private int summ1;

        public PaymentHelper(ManagerApp manager) : base(manager)
        {           
        }



        public void ClickStatusPayment()
        {
            //переход в обортно-сальдовую ведомость, для договора "нет задолженности"
            driver = manager.Driver;

            Thread.Sleep(5000);
            StatusPaid();
            Thread.Sleep(6000);
            RedactionClickButton();
            ClickPayment();
        }

        public void ClickStatusContratcClaim()
        {
            //переход в обортно-сальдовую ведомость, для договора "требует направления претензии"

            driver = manager.Driver;

            Thread.Sleep(5000);
            StatusContractClaim();
            GetSummContract();
            Thread.Sleep(6000);
            RedactionClickButton();
            ClickPayment();

        }

        public void ClickPayment()
        {
            //клик по кнопке "оборотно-сальдовая ведомость"

            driver.FindElement(By.XPath("//span[contains(text(),'Оборотно-сальдовая ведомость')]")).Click();
        }

        private void RedactionClickButton()
        {
            //клик по кнопке "Редактировать"

            driver.FindElement(By.XPath("//span[contains(text(),'Редактировать')]")).Click();
        }

        private void StatusPaid()
        {
            //найти и кликнуть по договору со статусом "Нет задолженности"

            driver.FindElement(By.XPath("//span[contains(text(),'Нет задолженности')]")).Click();
        }

        public void StatusContractClaim()
        {
            //найти и кликнуть по договору со статусом "Требует направления претензии"

            driver.FindElement(By.XPath("//span[contains(text(),'Требует направления претензии')]")).Click();
        }

        public void GetSummContract()
        {
            summ = driver.FindElement(By.XPath("(//tr[@class='  x-grid-row']//td[@role='gridcell'])[8]")).Text;
        }

        public void ClickIncomigCharges()
        {
            //клик по "Входящее" в графе "Начисления". Изменение положения на "убывание"

            while (!CheckSummNullIncoming())
            {
                driver.FindElement(By.XPath("(//span[contains(text(),'Входящее')])[1]")).Click();
            }
            //driver.FindElement(By.XPath("(//span[contains(text(),'Входящее')])[1]")).Click();

        }

       

        public void ClickRecalculate()
        {
            //клик по кнопке "Пересчитать"

            driver.FindElement(By.XPath("//span[contains(text(),'Пересчитать')]")).Click();
            Thread.Sleep(3000);
        }

        public bool Recalculate()
        {
            //найти кнопку "Пересчитать"

            return IsElementPresent(By.XPath("//span[contains(text(),'Пересчитать')]"));
        }

        public bool ContractClaim()
        {
            //найти договор со статусом "Требует направление претензии"

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement firstResult = wait.Until(e => e.FindElement(By.XPath("//span[contains(text(),'Требует направления претензии')]")));

            return true;
        }




        public bool CheckSummOutgoing()
        {
            //проверка отображения суммы в столбце "Исходящее"

            return IsElementPresent(By.XPath("(//div[@class='x-grid-cell-inner '][normalize-space()='" + summ + "'])[3]"));
        }

        public bool CheckSummOutgoing_2String()
        {
            var str = summ.Length;
            //int total = summ.Sum(value => int.Parse(summ));
            var tmp = Decimal.TryParse(summ, out Decimal total);
            var total1 = total * 2;
            return IsElementPresent(By.XPath("(//div[@class='x-grid-cell-inner '][normalize-space()='" +total1+"'])[1]"));
        }



        public bool CheckSummAccrued()
        {
            //проверка отображения суммы в столбце "Начислено"

            return IsElementPresent(By.XPath("(//div[@class='x-grid-cell-inner '][normalize-space()='"+ summ + "'])[2]"));
        }

        public bool CheckSummAccrues_2String()
        {
            //проверка отображения суммы во второй строчке, столбец "Начислено"

            return IsElementPresent(By.XPath("(//div[@class='x-grid-cell-inner '][normalize-space()='" + summ + "'])[5]"));
        }



        public bool CheckSummNullIncoming()
        {
            //проверка отображения суммы "0,00" в столбце "Входящее"

            return IsElementPresent(By.XPath("(//span[contains(text(),'Начисления')]/following::div[@class='x-grid-cell-inner '])[2][normalize-space()='0,00']")); //
        }

        public bool CheckSummIncoming_2String()
        {
            //проверка отображения суммы во второй строчке, столбец "Входящее"

            return IsElementPresent(By.XPath("(//div[@class='x-grid-cell-inner '][normalize-space()='" + summ + "'])[4]"));
        }


        public bool CheckSummNullPenalties()
        {
            //проверка отображения сумы пени "0,00" в столбце "Входящее" 

            return IsElementPresent(By.XPath("((//div[@role='tabpanel'])[2]//div[@class='x-grid-cell-inner '][normalize-space()='0,00'])[3]"));
        }

        public bool CheckSummPenltiesAccrued()
        {
            //проверка отображения суммы пени в столбце "Начислено"


            var days = DateTime.Now.ToString("dd");
            var tmp1 = Int32.TryParse(days, out Int32 days1);
                     


            var str = summ.Length;
            //int total = summ.Sum(value => int.Parse(summ));
            var tmp = Decimal.TryParse(summ, out Decimal total);
            var total1 = total * 0.1m / 100 * days1;
            

            return IsElementPresent(By.XPath("(//span[contains(text(),'Пени')]/following::div[@class='x-grid-cell-inner '][normalize-space()='"+ total1 + "'])[1]"));
        }

        public bool CheckSummPenaltiesOutgoing()
        {
            var str = summ.Length;
            //int total = summ.Sum(value => int.Parse(summ));
            var tmp = Decimal.TryParse(summ, out Decimal total);
            var total1 = total * 0.1m / 100 * 7;

            return IsElementPresent(By.XPath("(//span[contains(text(),'Пени')]/following::div[@class='x-grid-cell-inner '][normalize-space()='" + total1 + "'])[2]"));
        }

    }
}
