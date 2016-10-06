using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IStockExchange<TCompany, TShareHolder, TShare, TKey, TStockValue> : IClass, IIDNamePair<TKey, string>
        where TKey : IComparable
        where TShareHolder : IShareHolder<TShare, TKey, TStockValue>
        where TCompany : IStockBusiness<TShareHolder, TShare, TKey, TStockValue>
        where TShare : IStockShare<TKey, TStockValue>
    {
        JB2.Common.IBusiness<TKey> Owner { get; set; }
        TCompany GetCompany(string stockSymbol);

        TShareHolder GetShareHolder(TKey accountID);

        TShare GetShare(string transactionID);

        TradeTransactionNote<TKey> Trade(string holderAccountID, string stockSymbol, int quantity, TradeType tradeType, long? askPrice);

        ServiceResult ValidateTrade(TradeTransactionNote<TKey> note);

        bool IsOpen { get; }

        DateTime GetLastOpenDate();

        DateTime GetNextCloseTime();

        DateTime GetLastTradeTime();

        TShareHolder CreateNewShareHolder(string bankAccountID);

    }
}
