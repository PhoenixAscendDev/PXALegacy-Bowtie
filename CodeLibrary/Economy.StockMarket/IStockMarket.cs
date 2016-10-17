using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IStockExchange<TCompany, TShareHolder,TKey, TStockValue> : IClass, IIDNamePair<TKey, string>
        where TKey : IComparable
        where TStockValue : IComparable
        where TShareHolder : IShareHolder<TKey, TStockValue>
        where TCompany : IStockBusiness<TShareHolder, TKey, TStockValue>
       
    {


        #region Events
        event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TShareHolder> ShareHolderCreated;
        event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>,TradeTransactionNote<TKey>, TShareHolder,TCompany> ShareTraded;
        event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TradeTransactionNote<TKey>, TShareHolder,TCompany> ShareBought;
        event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TradeTransactionNote<TKey>, TShareHolder, TCompany> ShareSold;


        event Action<IStockExchange<TCompany, TShareHolder,  TKey, TStockValue>, TCompany, StockPrice<TKey, TStockValue>, TStockValue> StockPriceChanged;
        event Action<IStockExchange<TCompany, TShareHolder,  TKey, TStockValue>, TCompany, StockPrice<TKey, TStockValue>, TStockValue> StockPriceIncrease;
        event Action<IStockExchange<TCompany, TShareHolder,  TKey, TStockValue>, TCompany, StockPrice<TKey, TStockValue>, TStockValue> StockPriceDecrease;
        #endregion Events;

        JB2.Common.IBusiness<TKey> Owner { get; set; }
        TCompany GetCompany(string stockSymbol);

        TShareHolder GetShareHolder(TKey accountID);

        Position<TKey,TStockValue> GetShare(string transactionID);

        TradeTransactionNote<TKey> Trade(string holderAccountID, string stockSymbol, int quantity, TradeType tradeType, long? askPrice);

        ServiceResult ValidateTrade(TradeTransactionNote<TKey> note);

        bool IsOpen { get; }

        DateTime GetLastOpenDate();

        DateTime GetNextCloseTime();

        DateTime GetLastTradeTime();

        TShareHolder OpenNewAccount(string bankAccountID);

        ILogger GetLogger();
        
    }
}
