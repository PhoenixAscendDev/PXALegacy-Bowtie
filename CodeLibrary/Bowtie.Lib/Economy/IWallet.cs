using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public interface IWallet<TOwner,TDenomination> : IWallet<TOwner,ulong,TDenomination,string,string>
        where TDenomination : IDenomination<string,string, byte, string, string>
    {

    }
    public interface IWallet<TOwner,TAmount,TDenomination, TKey,TDenominationType> : JB2.Common.IIDNamePair<TKey, string>
        where TDenomination : IDenomination<TDenominationType, TKey, byte, TKey, string>
        where TKey : IComparable
    {
        TOwner Owner { get; set; }
        TDenomination[] Denomination { get;}
        TAmount Amount { get; }

        bool AddDenomination(TDenomination denomination,int quantity);
        bool RemoveDenomination(TDenomination denomination, int quantity);
    }

}
