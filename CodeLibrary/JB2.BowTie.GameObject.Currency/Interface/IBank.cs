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
    public interface IBank<TCurrency,TCapital,TKey,TUnit,TDenomination,TMultiplier,TSerial>
        where TCurrency : ICurrency<TKey,TUnit,TDenomination,TMultiplier,TSerial>
        where TDenomination : JB2.Bowtie.IDenomination<TKey,TMultiplier,TKey,TSerial>
    {
        TCapital TotalCapital { get; set; }
        float InterestRate { get; set; }

    }
}
