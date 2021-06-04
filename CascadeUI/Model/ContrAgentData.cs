using System;
using System.Collections.Generic;
using System.Text;

namespace CascadeUITest
{
    public class ContrAgentData
    {
        private string nameContrAgent;
        private string dateStartPayment;
        private string salary;

        public ContrAgentData(/*string nameContrAgent, string dateStartPayment,*/ string salary)
        {
            this.nameContrAgent = nameContrAgent;
            this.dateStartPayment = dateStartPayment;
            this.salary = salary;


        }

        public string NameContrAgent
        {
            get
            {
                return nameContrAgent;
            }
            set
            {
                nameContrAgent = value;
            }
        }

        public string DateStartPayment
        {
            get
            {
                return dateStartPayment;
            }
            set
            {
                dateStartPayment = value;
            }
        }

        public string Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
            }
        }
    }
}
