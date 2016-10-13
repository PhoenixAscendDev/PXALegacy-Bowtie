using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public interface IStockMarketRepository<TCompany, TExchange, TShareholder,TKey, TStockValue>
        where TExchange : IStockExchange<TCompany, TShareholder, TKey, TStockValue>
        where TCompany : IStockBusiness<TShareholder, TKey, TStockValue>
        where TShareholder : IShareHolder<TKey, TStockValue>
        where TShare : IStockShare<TKey, TStockValue>
        where TKey : IComparable
        where TStockValue : IComparable

    {

        #region Exchange
        TExchange GetExchange();
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

       // TShare GetStockShareByID(TKey id);

        Position<TKey,TStockValue> GetPositionByTranID(TKey transactionID);

        IEnumerable<Position<TKey, TStockValue>> GetStockPositionsByCompanyID(TKey companyID);

        IEnumerable<Position<TKey, TStockValue>> GetAllStockPositions();

        IEnumerable<Position<TKey, TStockValue>> GetStockPositionByAccountID(TKey accountID);

        //ServiceResult Save(TShare share);

        #endregion Share

        #region TradeTransactionNote

        TradeTransactionNote<TKey> GetTradeTranByID(TKey key);

        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByAccountID(TKey id);
        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByCompanyID(TKey companyid, int? count);

        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByDate(DateTime date);

        IEnumerable<TradeTransactionNote<TKey>> GetTradeTransByCompanyDate(TKey companyID, DateTime date);


        IEnumerable<TradeTransactionNote<TKey>> GetAllTradeTrans();

        ServiceResult Save(TradeTransactionNote<TKey> note);

        #endregion TradeTransactionNote


        #region StockPrice

        StockPrice<TKey, TStockValue> GetStockPriceByCompanyDatetime(TKey companyID, DateTime date);

        IEnumerable<StockPrice<TKey, TStockValue>> GetStockPriceByDatetime(DateTime date);

        IEnumerable<StockPrice<TKey, TStockValue>> GetStockPricesByCompany(TKey companyID);

        IEnumerable<StockPrice<TKey, TStockValue>> GetStockPricesByCompany(TKey companyID, int count);


        ServiceResult Save(StockPrice<TKey, TStockValue> stockPrice);

        #endregion StockPrice

    }
}
