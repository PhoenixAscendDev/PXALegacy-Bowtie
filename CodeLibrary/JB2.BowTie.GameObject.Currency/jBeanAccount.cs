using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class jBeanAccount : IBankAccount<JB2.Common.IIDProp<string>,JB2.Economy.Enum.jBeanAccountStatus>
    {
        #region Fields
        private string _accountNumber;
        private string _name;
        private string _routingNumber;

        #endregion Fields

        #region Constructors

        public jBeanAccount(string accountNumber)
        {
            _accountNumber = accountNumber;
            _name = "Jbean Account";
            _routingNumber = "0";
        }

        #endregion Constructors


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

        public JB2.Common.IIDProp<string> AccountHolder { get; set; }

        public JB2.Economy.Enum.jBeanAccountStatus Status { get; set; }

        public static jBeanAccount FromAccountNumber(string accountNumber)
        {
            return new jBeanAccount(accountNumber);
        }
    }
}
