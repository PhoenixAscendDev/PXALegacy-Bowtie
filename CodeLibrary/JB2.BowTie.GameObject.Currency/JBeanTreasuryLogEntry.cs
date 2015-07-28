using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public class JBeanTreasuryLogEntry  : ITreasuryLogEntry<Enum.JBeanTokenType,string>
    {

        public JBeanTreasuryLogEntry(string appKey, JBeanToken token, int quantity)
        {
            this.ApplicationKey = appKey;
            this.CurrencyKey = token.CurrencyID;
            this.DenominationType = token.DenominationType;
            this.Quantity = quantity;
        }

        public string ApplicationKey
        {
            get;
            set;
        }

        public Enum.JBeanTokenType DenominationType
        {
            get;
            set;
        }

        public DateTime TransactionDate
        {
            get;
            set;
        }

        public string CurrencyKey
        {
            get;
            set;
        }


        public int Quantity
        {
            get;
            set;
        }
    }
}
