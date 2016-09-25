using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IStockBusiness<TShareHolder,TShare,TKey,TStockValue> : IBusiness<TKey>, IStockable<TKey,TStockValue>
        where TKey : IComparable
        where TShareHolder : IShareHolder<TShare,TKey,TStockValue>
        where TShare : IStockShare<TKey, TStockValue>
    {
        int GetShareCount();
        TShareHolder GetShareHolder(string accountID);

        DateTime GetLastTradeDate();
        IEnumerable<TShareHolder> GetShareHolders();

        IEnumerable<StockPrice<TKey, TStockValue>> GetLastStockPrices(int count);

        IEnumerable<StockPrice<TKey, TStockValue>> GetStockPricesRange(DateTime min, DateTime max);


    }
}
