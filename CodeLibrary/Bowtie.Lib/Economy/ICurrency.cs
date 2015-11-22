using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;


namespace JB2.Bowtie.Economy
{

    public interface ICurrency : ICurrency<IDenomination<string>,string>
    {

    }
    public interface ICurrency<TDenomination,TDenominationType> : ICurrency<string,byte,TDenomination,TDenominationType,byte,string>
        where TDenomination : IDenomination<TDenominationType,string, byte, string, string>
    {

    }

    public interface ICurrency<TKey, TUnit, TDenomination,TDenominationType, TMultiplier,TSerial> : JB2.Common.IIDNamePair<TKey, string>
        where TDenomination : IDenomination<TDenominationType,TKey, TMultiplier,TKey,TSerial>
        where TKey : IComparable
    {

        TUnit BaseUnit { get; set; }

        string OwnerClientID { get; set; }

        CurrencyType CurrencyType { get; set; }

        TDenomination[] Denominations { get; set; }

        float[] SubUnits { get; set; }

        string PluralName { get; set; }

        string SymbolUrl { get; set; }

        TDenomination[] GetDenomination(TDenominationType dType);

        



    }
}
