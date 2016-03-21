using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IWallet
    {
        string PlayerID { get; }
        string ID { get; }

        double CurrencyTotal(ICurrency currency);
        void AddAmount(ICurrency currency, double quantity);
        void RemoveAmount(ICurrency currency, double quantity);     
    }
}
