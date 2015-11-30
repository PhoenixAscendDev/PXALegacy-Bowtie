using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class jBeanReceipt : IBankTransactionReceipt
    {

        #region Fields
        private string _message;
        private string _transactionNumber;
        private bool _success;
        #endregion

        public jBeanReceipt(string transNumber, string message,bool isSuccess)
        {
            _transactionNumber = transNumber;
            _message = message;
            _success = isSuccess;
        }


        public string BankID
        {
            get
            {
                return JB2.Settings.Jbean.Factory.CentralBank.GetID();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        public string TransactionNumber
        {
            get
            {
                return _transactionNumber;
            }
        }

        public bool WasSuccess
        {
            get
            {
                return _success;
            }
        }
    }
}
