using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;


namespace JB2.Bowtie
{

    public interface ICurrency : ICurrency<IDenomination>
    {

    }
    public interface ICurrency<TDenomination> : JB2.Bowtie.ICurrency<string,byte,TDenomination,byte,string>
        where TDenomination : IDenomination<string, byte, string, string>
    {

    }

    public interface ICurrency<TKey, TUnit, TDenomination, TMultiplier,TSerial> : JB2.Common.IIDNamePair<TKey, string>
        where TDenomination : IDenomination<TKey, TMultiplier,TKey,TSerial>
    {

        TUnit BaseUnit { get; set; }

        string OwnerClientID { get; set; }

        CurrencyType CurrencyType { get; set; }

        TDenomination[] Denominations { get; set; }

        float[] SubUnits { get; set; }

        string PluralName { get; set; }

        string SymbolUrl { get; set; }

        



    }
}
