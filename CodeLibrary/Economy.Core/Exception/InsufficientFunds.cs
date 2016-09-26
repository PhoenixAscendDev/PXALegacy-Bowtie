using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class InsufficientFundsException : Exception
    {
        public InsufficientFundsException()
            : this("Insufficient Funds")
        {

        }
        public InsufficientFundsException(double amount)
            : this("Insufficient Funds: Funds needed " + amount.ToString())
        {

        }
        public InsufficientFundsException(long amount)
            : this("Insufficient Funds: Funds needed " + amount.ToString())
        {

        }
        public InsufficientFundsException(int amount)
            : this("Insufficient Funds: Funds needed " + amount.ToString())
        {

        }

        public InsufficientFundsException(string message) : this(message, null)
        {

        }

        public InsufficientFundsException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
