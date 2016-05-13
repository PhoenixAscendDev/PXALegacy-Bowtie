using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IWallet<TOwner,TID> : JB2.Common.IIDProp<TID>
        where TID : IComparable
        where TOwner : JB2.Common.IPerson<TID>
    {
        TOwner Owner { get; }
        TID ID { get; }
        double CurrencyTotal(ICurrency currency);
        void AddAmount(ICurrency currency, double quantity);
        void RemoveAmount(ICurrency currency, double quantity);     
    }
}
