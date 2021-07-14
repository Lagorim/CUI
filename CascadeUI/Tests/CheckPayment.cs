using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CascadeUITest
{
    [TestFixture]
    public class CheckPayment : AuthBaseTest
    {
        [TestCase(TestName = "#01 Проверка открытия оборотно-сальдовой ведомости")]
        [Order(1)]
        public void CheckOpenPayment()
        {
            app.Contract.CreateContractTrueStatus(new ContractData("16:05:011601:1006", "Кадастровая стоимость"));
            //app.Contract.CheckRecContract();

            app.Payment.ClickStatusPayment();

            Assert.IsTrue(app.Payment.Recalculate());

        }

        [TestCase(TestName = "#02 Проверка изменения статуса договора с 'Нет Задолженности' на 'Требует направление претензии' ")]
        [Order(2)]
        public void CheckChangeStatus()
        {
            //app.Contract.CheckRecContract();
            app.Payment.ClickStatusPayment();

            app.Payment.ClickRecalculate();
            app.Contract.ClickButtonList();
            //app.Contract.CheckRecContract();

            Assert.IsTrue(app.Payment.ContractClaim());

            //app.Contract.DeleteLastContract();
        }

        [TestCase(TestName = "#03 Проверка отображения сумм в обортной-сальдовой ведомости(входящее = 0,00-начислено=сумма=исходящее)")]
        [Order(3)]
        public void CheckSummPay()
        {
            //app.Contract.CreateContractTrueStatus(new ContractData("01.06.2020", "01.06.2020", "16:05:011601:1006", "01.06.2022", "Кадастровая стоимость", "1", "01.06.2020"));
            
            //app.Contract.CheckRecContract();
            app.Payment.ClickStatusContratcClaim();

            app.Payment.ClickRecalculate();
            //app.Payment.ClickStatusContratcClaim();

            app.Payment.ClickIncomigCharges();

            Assert.IsTrue(app.Payment.CheckSummNullIncoming() && app.Payment.CheckSummAccrued() && app.Payment.CheckSummOutgoing());
            Assert.IsTrue(app.Payment.CheckSummIncoming_2String()&& app.Payment.CheckSummAccrues_2String() && app.Payment.CheckSummOutgoing_2String());
        }

        [TestCase(TestName = "#04 Проверка отображения пени в оборотно-садльдовой ведомости")]
        [Order(4)]
        public void CheckSummPenalties()
        {
            //app.Contract.CheckRecContract();
            app.Payment.ClickStatusContratcClaim();

            app.Payment.ClickRecalculate();
            app.Payment.ClickIncomigCharges();

            Assert.IsTrue(app.Payment.CheckSummNullPenalties() && app.Payment.CheckSummPenltiesAccrued() && app.Payment.CheckSummPenaltiesOutgoing());
        }
    }
}
