using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class jBeanAccount : IBankAccount
    {
        #region Fields
        private string _accountNumber;
        private string _name;
        private string _routingNumber;

        #endregion Fields

        public jBeanAccount(string accountNumber)
        {
            _accountNumber = accountNumber;
            _name = "Jbean Account";
            _routingNumber = "0";
        }

        public string AccountNumber
        {
            get
            {
                return _accountNumber;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string RoutingNumber
        {
            get
            {
                return _routingNumber;
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
