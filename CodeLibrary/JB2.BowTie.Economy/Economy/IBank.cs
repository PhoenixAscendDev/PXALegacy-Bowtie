using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    
    public interface IBank : IBank<ICurrency, long,string, byte, IDenomination<string>, byte, string>
    {

    }

    public interface IBank<TCurrency,TCapital> :  IBank<TCurrency,TCapital,string,byte,IDenomination<string,string,byte,string,string>,byte,string>
        where TCurrency : ICurrency<IDenomination<string,string, byte, string, string>,string>
    {

    }
    public interface IBank<TCurrency,TCapital,TKey,TUnit,TDenomination,TMultiplier,TSerial>
        where TCurrency : ICurrency<TKey,TUnit,TDenomination,string,TMultiplier,TSerial>
        where TDenomination : IDenomination<string,TKey,TMultiplier,TKey,TSerial>
        where TKey : IComparable
    {
        TCapital TotalCapital { get; set; }
        float InterestRate { get; set; }

       

        int MaxAmountPerIssue { get; set; }

    }
}
