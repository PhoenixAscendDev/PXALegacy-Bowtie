using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    
    public interface IBank : IBank<ICurrency, ulong,string, byte, IDenomination, byte, string>
    {

    }

    public interface IBank<TCurrency,TCapital> :  JB2.Bowtie.IBank<TCurrency,TCapital,string,byte,IDenomination<string,byte,string,string>,byte,string>
        where TCurrency : ICurrency<IDenomination<string, byte, string, string>>
    {

    }
    public interface IBank<TCurrency,TCapital,TKey,TUnit,TDenomination,TMultiplier,TSerial>
        where TCurrency : ICurrency<TKey,TUnit,TDenomination,TMultiplier,TSerial>
        where TDenomination : JB2.Bowtie.IDenomination<TKey,TMultiplier,TKey,TSerial>
    {
        TCapital TotalCapital { get; set; }
        float InterestRate { get; set; }

        bool IssueDenomination(TDenomination type, int quantity);

        int MaxAmountPerIssue { get; set; }

    }
}
