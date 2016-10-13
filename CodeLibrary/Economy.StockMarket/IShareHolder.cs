using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IShareHolder<TKey, TStockValue> : IStockHolderable<TKey>
        where TKey: IComparable
        where TStockValue: IComparable
    {
        TStockValue GetTotalValue();

        Position<TKey,TStockValue> GetShares(TKey stockSymbol);

        IEnumerable<Position<TKey, TStockValue>> GetShares();

        TStockValue GetFundsAvalable();

        string BankAccountID { get; set; }

        uint GetShareCount(TKey stockSymbol);


    }
}
