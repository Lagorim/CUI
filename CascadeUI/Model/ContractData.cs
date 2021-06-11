using System;
using System.Collections.Generic;
using System.Text;

namespace CascadeUITest
{
    public class ContractData
    {
        

        //private string numberContract;
        private string numberObject;
        private string conclusion;
        //private string date;
        //private string value;
        private string dateEnd;
        private string dateStart;
        private string dateClaim;
        //private string salary;
        //private string dataStartPay;
        //private string dateConclusion;
        //private string dateStartAction;

        public ContractData(/*string numberContract,*/ /*string dateConclusion, string dateStartAction,*/ string numberObject/*, string date*/,string conclusion/*, string value, string dataStartPay, string salary*/)
        {
            //this.numberContract = numberContract;
            this.numberObject = numberObject;
            this.conclusion = conclusion;
            //this.date = date;
            //this.value = value;
            //this.salary = salary;
            //this.dataStartPay = dataStartPay;
            //this.dateConclusion = dateConclusion;
            //this.dateStartAction = dateStartAction;
        }

        public void Claim(string dateClaim)
        {
            this.dateClaim = dateClaim;
        }

        public void ClaimDateStart(string dateStart)
        {
            this.dateStart = dateStart;
        }

        public void ClaimDateEnd (string dateEnd)
        {
            this.dateEnd = dateEnd;
        }

        //public void CheckRecContract(string numberContract)
        //{ 
        //    this.n
        //}

        //public ContractData(string numberObject)
        //{
        //    this.numberObject = numberObject;
        //    //    //return new ContractData(numberObject);
        //}

        //public string NumberContract
        //{
        //    get
        //    {
        //        return numberContract;
        //    }

        //    set
        //    {
        //        numberContract = value;
        //    }
        //}

        public string NumberObject
        {
            get
            {
                return numberObject;
            }

            set
            {
                numberObject = value;
            }
        }

        public string Conclusion
        {
            get
            {
                return conclusion;
            }

            set
            {
                conclusion = value;
            }
        }

        //public string Value
        //{
        //    get
        //    {
        //        return value;
        //    }

        //    set
        //    {
        //        value = value;
        //    }
        //}

        //public string Date 
        //{
        //    get
        //    {
        //        return date;
        //    }
        //    set
        //    {
        //        date = value;
        //    }
        //}

        public string DateClaim
        {
            get
            {
                return dateClaim;
            }
            set
            {
                dateClaim = value;
            }
        }

        public string DateEnd
        {
            get
            {
                return dateEnd;
            }
            set
            {
                dateEnd = value;
            }
        }

        public string DateStart
        {
            get
            {
                return dateStart;
            }
            set
            {
                dateStart = value;
            }
        }

        //public string Salary
        //{
        //    get
        //    {
        //        return salary;
        //    }
        //    set
        //    {
        //        salary = value;
        //    }
        //}

        //public string DataStartPay
        //{
        //    get
        //    {
        //        return dataStartPay;
        //    }
        //    set
        //    {
        //        dataStartPay = value;
        //    }
        //}

        //public string DateConclusion
        //{
        //    get
        //    {
        //        return dateConclusion;
        //    }
        //    set
        //    {
        //        dateConclusion = value;
        //    }
        //}

        //public string DateStartAction
        //{
        //    get
        //    {
        //        return dateStartAction;
        //    }
        //    set
        //    {
        //        dateStartAction = value;
        //    }
        //}
    }
}
