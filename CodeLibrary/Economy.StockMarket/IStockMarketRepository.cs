using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    interface IStockMarketRepository<TCompany, TExchange, TShareholder, TShare, TKey, TStockValue>
        where TExchange : IStockExchange<TCompany, TShareholder, TShare, TKey, TStockValue>
        where TCompany : IStockBusiness<TShareholder, TShare, TKey, TStockValue>
        where TShareholder : IShareHolder<TShare, TKey, TStockValue>
        where TShare : IStockShare<TKey, TStockValue>
        where TKey : IComparable
        where TStockValue : IComparable

    {

        #region Exchange
        TExchange GetExchangeByID(TKey key);
        ServiceResult Save(TExchange exchange);
        #endregion Exchange

        #region Company

        TCompany GetCompanyByID(TKey key);

        TCompany GetCompanyByStockSymbol(string symbol);

        IEnumerable<TCompany> GetAllCompanies();

        ServiceResult Save(TCompany company);

        #endregion Company

        #region Shareholder

        TShareholder GetShareholderByID(TKey key);

        IEnumerable<TShareholder> GetAllShareholders();

        ServiceResult Save(TShareholder shareHolder);


        #endregion Shareholder

        #region Share

        TShare GetStockShareByID(TKey id);

        TShare GetStockShareByTranID(TKey transactionID);

        IEnumerable<TShare> GetStockSharesByCompanyID(TKey companyID);

        IEnumerable<TShare> GetAllStockShares();

        IEnumerable<TShare> GetStockSharesByAccountID(TKey accountID);

        #endregion Share

        #region TradeTransactionNote

        TradeTransactionNote<TKey> GetTradeTranByID(TKey key);

        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByAccountID(TKey id);
        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByCompanyID(TKey companyid);

        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByDate(DateTime date);

        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByCompanyDate(TKey companyID, DateTime date);


        IEnumerable<TradeTransactionNote<TKey>> GetAllTradeTrans();

        ServiceResult Save(TradeTransactionNote<TKey> note);

        #endregion TradeTransactionNote


        #region StockPrice

        StockPrice<TKey, TStockValue> GetStockPriceByCompanyDatetime(TKey companyID, DateTime date);

        IEnumerable<StockPrice<TKey, TStockValue>> GetStockPriceByDatetime(DateTime date);

        ServiceResult Save(StockPrice<TKey, TStockValue> stockPrice);

        #endregion StockPrice

    }
}
