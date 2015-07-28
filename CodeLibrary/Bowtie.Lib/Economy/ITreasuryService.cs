using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public interface ITreasuryService<TCurrency,TDenomination,TDenominationType,TCurrencyKey,TSerial>
        where TCurrency : ICurrency<TCurrencyKey,byte,TDenomination,TDenominationType,byte,TSerial>
        where TDenomination : IDenomination<TDenominationType,TCurrencyKey,byte,TCurrencyKey,TSerial>
    {
        bool IssueDenomination(TDenomination type, int quantity);

        ulong TotalAmountIssued { get; set; }

        long DenominationIssuedCount(TDenominationType type);


    }
}
