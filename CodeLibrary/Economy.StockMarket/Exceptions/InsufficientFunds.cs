using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class InsufficientStockSharesException : Exception
    {
        public InsufficientStockSharesException()
            : this("Insufficient Shares")
        {

        }
        public InsufficientStockSharesException(int amount)
            : this("Insufficient Funds: Funds needed " + amount.ToString())
        {

        }

        public InsufficientStockSharesException(string message) : this(message, null)
        {

        }

        public InsufficientStockSharesException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
