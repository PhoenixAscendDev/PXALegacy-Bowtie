using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Interface
{
    public interface IWallet<TOwner,TDenomination> : IWallet<TOwner,ulong,TDenomination,string>
        where TDenomination : IDenomination<string,string, byte, string, string>
    {

    }
    public interface IWallet<TOwner,TAmount,TDenomination, TKey> : JB2.Common.IIDNamePair<TKey, string>
        where TDenomination : IDenomination<string,TKey,byte,TKey,string>
    {
        TOwner Owner { get; set; }
        TDenomination[] Denomination { get; set; }
        TAmount Amount { get; }

        bool AddDenomination(TDenomination denomination,int quantity);
        bool RemoveDenomination(TDenomination denomination, int quantity);






    }

}
