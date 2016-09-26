using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IJbeanStockMarketRepository
    {
        #region Exchange
        JbeanStockExchange RetrieveStockMarket(string exchangeID);
        #endregion Exchange

        #region StockShare

        JbeanStockShare RetrieveShareByTranID(string transationID);

        IEnumerable<JbeanStockShare> RetrieveSharesByHolder(string holderAccountID);

        IEnumerable<JbeanStockShare> RetrieveSharesByHolder(string holderAccountID, string companyID);

        IEnumerable<JbeanStockShare> RetrieveSharesByDateRange(JB2.Common.Range<DateTime> range, string companyID);

        JB2.Common.ServiceResult Save(JbeanStockShare stockshare);
        #endregion StockShare

        #region StockCompany
        JbeanStockCompany RetrieveCompany(string companyID);

        JbeanStockCompany RetrieveCompanyBySymbol(string stockSymbol);

        IEnumerable<JbeanStockCompany> RetrieveAllCompanies();

        JB2.Common.ServiceResult Save(JbeanStockCompany company);
        #endregion StockCompany

        #region StockPrice
        IEnumerable<StockPrice<string, long>> RetrieveStockPriceByCompany(string companyID);
        IEnumerable<StockPrice<string, long>> RetrieveStockPriceByDateRange(JB2.Common.Range<DateTime> range, string companyID);
        JB2.Common.ServiceResult Save(StockPrice<string, long> stockprice);

        #endregion StockPrice

        #region TransationNote

        IEnumerable<TradeTransactionNote<string>> RetrieveTransationsByCompany(string companyID, int recordCount);

        bool Save(TradeTransactionNote<string> note);

        #endregion TransationNote

        #region ShareHolder

        IJbeanStockHolder RetrieveShareHolder(string accountID);

        IEnumerable<IJbeanStockHolder> RetrieveShareHoldersByCompany(string companyID);

        IEnumerable<IJbeanStockHolder> RetrieveShareHoldersAll();

        #endregion ShareHolder

    }
}
