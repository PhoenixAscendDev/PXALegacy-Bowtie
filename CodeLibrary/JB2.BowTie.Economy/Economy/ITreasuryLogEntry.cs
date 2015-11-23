using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public interface ITreasuryLogEntry<TDenominationType,TCurrencyKey>
    {
        string ApplicationKey { get; set; }
        TDenominationType DenominationType { get; set; }
        DateTime TransactionDate { get; set; }
        TCurrencyKey CurrencyKey { get; set; }
        int Quantity { get; set; }
    }
}
