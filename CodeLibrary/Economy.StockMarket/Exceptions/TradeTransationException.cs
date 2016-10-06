using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class TradeTransationException : Exception
    {
        public TradeTransationException()
            : this("Trade Transation")
        {

        }
        public TradeTransationException(TradeType type)
            : this("Error on " + type.ToString())
        {

        }

        public TradeTransationException(string message) : this(message, null)
        {

        }

        public TradeTransationException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
