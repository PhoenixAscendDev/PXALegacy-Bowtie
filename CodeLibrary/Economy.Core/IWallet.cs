using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IWallet<TCurrency,TOwner,TID>
        where TID : IComparable
        where TOwner : JB2.Common.IPerson<TID>
        where TCurrency : ICurrency
    {
        TOwner Owner { get; }
        TID ID { get; }
        double CurrencyTotal(TCurrency currency);
        void AddAmount(TCurrency currency, double quantity);
        void RemoveAmount(TCurrency currency, double quantity);     
    }
}
