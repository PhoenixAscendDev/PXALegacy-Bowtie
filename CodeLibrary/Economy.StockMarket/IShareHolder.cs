using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IShareHolder<TShare,TKey, TStockValue> : IStockHolderable<TKey>
        where TKey: IComparable
        where TShare : IStockShare<TKey, TStockValue>
    {
        TStockValue GetTotalValue();

        TShare GetShares(TKey stockSymbol);

        IEnumerable<TShare> GetShares();

        TStockValue GetFundsAvalable();

        string BankAccountID { get; set; }

        uint GetShareCount(TKey stockSymbol);


    }
}
