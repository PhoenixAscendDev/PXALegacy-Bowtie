using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;
using JB2.Economy;

using JB2.Common.Data;

using Microsoft.WindowsAzure.Storage.Table;

namespace JB2.Economy.Data
{
    public class JBeanStockRepository : IJbeanStockMarketRepository
    {
        #region Fields
        private JB2.Common.Data.AzureTableRepository _marketTable;
        private JB2.Common.Data.AzureTableRepository _tranlogTable;
        private JB2.Common.Data.AzureTableRepository _accountTable;
        private JB2.Common.Data.AzureTableRepository _priceTable;
        private string _exchangeID;
        #endregion Fields


        #region Constructor
        public JBeanStockRepository(JB2.Common.Data.StorageAccount storageAccount) :
            this(storageAccount.GetTable("stockmarket"),
                storageAccount.GetTable("stockmaketTran"),
                storageAccount.GetTable("stockmarketAccount"),
                storageAccount.GetTable("stockmarketPrice")
            )
        {

        }

        public JBeanStockRepository(JB2.Common.Data.AzureTableRepository marketTable,
                                       JB2.Common.Data.AzureTableRepository tranLogTable,
                                       JB2.Common.Data.AzureTableRepository accountTable,
                                       JB2.Common.Data.AzureTableRepository priceTable)
        {
            _marketTable = marketTable;
            _tranlogTable = tranLogTable;
            _accountTable = accountTable;
            _priceTable = priceTable;
            _exchangeID = JB2.Configuration.GetjBeanStockMarketID();
        }
        #endregion Constructor



        #region Exchange
        public JbeanStockExchange GetExchange()
        {
            var id = JB2.Configuration.GetjBeanStockMarketID();
            var entity = _marketTable.GetEntity<DynamicTableEntity>("exchange", "id:" + id);

            return convertToObject(entity);

        }

        private JbeanStockExchange convertToObject(DynamicTableEntity e)
        {
            string id = e.GetPropertyValue<string>("ID", string.Empty);

            JbeanStockExchange exchange = new JbeanStockExchange(id);

            exchange.Name = e.PropertyStringValue("Name");
            exchange.Owner = JB2.Info.HQ;
            exchange.SetOpen(e.GetPropertyValue<bool>("Open", false));

            return exchange;
        }


        public ServiceResult Save(JbeanStockExchange exchange)
        {
            DynamicTableEntity e = new DynamicTableEntity();

            e.SetProperty<string>("ID", exchange.ID);
            e.SetProperty<string>("Name", exchange.Name);

            e.PartitionKey = "exchange";
            e.RowKey = "id:" + exchange.ID;

            try
            {
                _marketTable.Insert<DynamicTableEntity>(e, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }

            return true;

        }

        #endregion Exchange


        #region StockCompany

        public JbeanStockCompany GetCompanyByID(string key)
        {
            var e = _marketTable.GetEntity<DynamicTableEntity>("exchange:" + _exchangeID + ":company", "id:" + key);

            return convertToCompany(e);
        }

        public JbeanStockCompany GetCompanyByStockSymbol(string symbol)
        {
            var pkey = "exchange:" + _exchangeID + ":company";
            var rkey = "stocksymbol:" + symbol;
            var e = _marketTable.GetEntity<DynamicTableEntity>(pkey,rkey);

            return convertToCompany(e);
        }

        public IEnumerable<JbeanStockCompany> GetAllCompanies()
        {
            var e = _marketTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":company");

            return convertToCompany(e);
        }

        public ServiceResult Save(JbeanStockCompany company)
        {
            DynamicTableEntity e = new DynamicTableEntity();



            e.SetProperty<string>("ID", company.StockExchangeCompanyID);
            e.SetProperty<string>("Name", company.Name);
            e.SetProperty<string>("StockSymbol", company.StockSymbol);
            e.SetProperty<string>("ExchangeID", company.ExchangeID);



            e.SetProperty<string>("MailingAddress_Line1", company.MailingAddress.GetAddressLine1());
            e.SetProperty<string>("MailingAddress_Line2", company.MailingAddress.GetAddressLine2());
            e.SetProperty<string>("MailingAddress_City", company.MailingAddress.GetCity());

            if(company.MailingAddress.GetStateProvince() == null)
                e.SetProperty<string>("MailingAddress_State", string.Empty);
            else
                e.SetProperty<string>("MailingAddress_State", company.MailingAddress.GetStateProvince().ToString());
            e.SetProperty<string>("MailingAddress_PostalCode", company.MailingAddress.GetPostalCode());


            //e.SetProperty<string>("POC_FirstName", company.POC.Name.First);
            //e.SetProperty<string>("POC_LastName", company.POC.Name.Middle);
            //e.SetProperty<string>("POC_MiddleName", company.POC.Name.Last);
            //e.SetProperty<string>("POC_DisplayName", company.POC.DisplayName);
            //e.SetProperty<string>("POC_ID", company.POC.ID);

            try
            {
                e.PartitionKey = "exchange:" + _exchangeID + ":company";
                e.RowKey = "id:" + company.StockExchangeCompanyID;
                _marketTable.Insert<DynamicTableEntity>(e, true);

                e.PartitionKey = "exchange:" + _exchangeID + ":company";
                e.RowKey = "stocksymbol:" + company.StockSymbol;
                _marketTable.Insert<DynamicTableEntity>(e, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }

            return true;
        }

        private JbeanStockCompany convertToCompany(DynamicTableEntity e)
        {

            var id = e.PropertyStringValue("ID", string.Empty);

            var result = new JbeanStockCompany(id);

            result.ID = id;
            result.ExchangeID = _exchangeID;


            JB2.Common.Map.StandardAddress address = new Common.Map.StandardAddress();

            address.AddressLine1 = e.PropertyStringValue("MailingAddress_Line1", string.Empty);
            address.AddressLine2 = e.PropertyStringValue("MailingAddress_Line2", string.Empty);
            address.City = e.PropertyStringValue("MailingAddress_City", string.Empty);
            address.StateProvince = new JB2.Common.StateProvince(string.Empty, e.PropertyStringValue("MailingAddress_State", string.Empty));
            address.PostalCode = e.PropertyStringValue("MailingAddress_PostalCode", string.Empty);


            result.MailingAddress = address;
            result.Name = e.PropertyStringValue("Name");



            result.POC = null;
            result.StockExchangeCompanyID = id;
            result.StockSymbol = e.PropertyStringValue("StockSymbol");

            return result;

        }

        private IEnumerable<JbeanStockCompany> convertToCompany(IEnumerable<DynamicTableEntity> elist)
        {
            List<JbeanStockCompany> list = new List<JbeanStockCompany>();

            foreach (var e in elist)
            {
                list.Add(convertToCompany(e));
            }

            return list;
        }

        #endregion StockCompany

        #region StockHolder

        public IJbeanStockHolder GetShareholderByID(string key)
        {
            var e = _accountTable.GetEntity<DynamicTableEntity>("exchange:" + _exchangeID + ":account", "accountID:" + key);

            return convertToStockHolder(e);
        }

        public IEnumerable<IJbeanStockHolder> GetAllShareholders()
        {
            var e = _accountTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":account");

            return convertToStockHolder(e);
        }

        public ServiceResult Save(IJbeanStockHolder shareHolder)
        {
            DynamicTableEntity e = new DynamicTableEntity();
            e.SetProperty<string>("ExchangeAccountID", string.Empty);
            e.SetProperty<string>("BankAccountID", string.Empty);

            try
            {
                _accountTable.Insert<DynamicTableEntity>(e, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }
            return true;

        }

        private IEnumerable<IJbeanStockHolder> convertToStockHolder(IEnumerable<DynamicTableEntity> elist)
        {
            List<IJbeanStockHolder> list = new List<IJbeanStockHolder>();

            foreach (var e in elist)
            {
                list.Add(convertToStockHolder(e));
            }

            return list;
        }

        private IJbeanStockHolder convertToStockHolder(DynamicTableEntity e)
        {
            string accountID = e.PropertyStringValue("ExchangeAccountID");

            var holder = new JbeanStockHolder(accountID);
            holder.BankAccountID = e.PropertyStringValue("BankAccountID");
            return holder;



        }

        #endregion StockHolder


        #region StockShare

        //public JbeanStockShare GetStockShareByID(string id)
        //{
        //    var e = _marketTable.GetEntity<DynamicTableEntity>("exchange:" + _exchangeID + "stockshare", "id:" + id);

        //    return convertToStockShare(e);
        //}

        //public JbeanStockShare GetStockShareByTranID(string transactionID)
        //{
        //    var e = _marketTable.GetEntity<DynamicTableEntity>("exchange:" + _exchangeID + ":stockshare", "id:" + transactionID);

        //    return convertToStockShare(e);
        //}

        //public IEnumerable<JbeanStockShare> GetStockSharesByCompanyID(string companyID)
        //{
        //    var e = _marketTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":company:" + companyID + ":stockshare");

        //    return convertToStockShare(e);
        //}

        //public IEnumerable<JbeanStockShare> GetAllStockShares()
        //{
        //    var e = _marketTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":stockshare");

        //    return convertToStockShare(e);
        //}

        //public IEnumerable<JbeanStockShare> GetStockSharesByAccountID(string accountID)
        //{
        //    var e = _marketTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":account" + accountID + ":stockshare");

        //    return convertToStockShare(e);
        //}


        //public ServiceResult Save(JbeanStockShare share)
        //{
        //    DynamicTableEntity e = new DynamicTableEntity();

        //    e.SetProperty<string>("CompanyID", share.Company.StockExchangeCompanyID);
        //    e.SetProperty<DateTime>("DatePurchased", share.DatePurchased);
        //    e.SetProperty<string>("TransactionID", share.ExchangeTransactionID);
        //    e.SetProperty<long>("PurchaseAmount", share.PurchaseAmount);
        //    e.SetProperty<int>("Quantity", share.Quantity);
        //    e.SetProperty<string>("AccountID", share.StockExchangeAccountID);
        //    e.SetProperty<string>("ExchangeID", _exchangeID);

        //    try
        //    {

        //        e.PartitionKey = "exchange:" + _exchangeID + ":stockshare";
        //        e.RowKey = "id:" + share.ExchangeTransactionID;
        //        _marketTable.Insert<DynamicTableEntity>(e, true);

        //        e.PartitionKey = "exchange:" + _exchangeID + ":company:" + share.Company.StockExchangeCompanyID + ":stockshare";
        //        e.RowKey = "id:" + share.ExchangeTransactionID;
        //        _marketTable.Insert<DynamicTableEntity>(e, true);


        //        e.PartitionKey = "exchange:" + _exchangeID + ":account" + share.StockExchangeAccountID + ":stockshare";
        //        e.RowKey = "id:" + share.ExchangeTransactionID;
        //        _marketTable.Insert<DynamicTableEntity>(e, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ServiceResult(ex);
        //    }

        //    return true;

        //}


        //private IEnumerable<JbeanStockShare> convertToStockShare(IEnumerable<DynamicTableEntity> elist)
        //{
        //    List<JbeanStockShare> list = new List<JbeanStockShare>();

        //    foreach (var e in elist)
        //    {
        //        list.Add(convertToStockShare(e));
        //    }

        //    return list;
        //}

        //private JbeanStockShare convertToStockShare(DynamicTableEntity e)
        //{
        //    JbeanStockShare share = new JbeanStockShare();

        //    string companyID = e.PropertyStringValue("CompanyID");
        //    share.Company = this.GetCompanyByID(companyID);
        //    share.DatePurchased = e.GetPropertyValue<DateTime>("DatePurchased", DateTime.Now);
        //    share.ExchangeTransactionID = e.GetPropertyValue<string>("TransactionID", string.Empty);
        //    share.PurchaseAmount = e.GetPropertyValue<long>("PurchaseAmount", 0);
        //    share.Quantity = e.GetPropertyValue<int>("Quantity", 0);
        //    share.StockExchangeAccountID = e.GetPropertyValue<string>("AccountID", string.Empty);

        //    return share;
        //}

        #endregion StockShare


        #region StockPositions
        public Position<string, long> GetPositionByTranID(string transactionID)
        {
           TradeTransactionNote<string> t  = GetTradeTranByID(transactionID);
            var p = new Position<string, long>();
            if ((TradeTransactionNote<string>)t != null)
            {        
                p.Quantity = t.ShareCount;
                var s = new Share<string, long>();

                var c = GetCompanyByID(t.CompanyID);
                s.CurrentPrice = c.GetCurrentStockValue();
                s.Symbol = c.StockSymbol;
                s.ID = string.Empty;

                p.Share = s;
                p.StockExchangeAccountID = t.AccountID;
                p.TotalCost = t.SharePrice * t.ShareCount;
            }

            return p;
        }

        public IEnumerable<Position<string, long>> GetStockPositionsByCompanyID(string companyID)
        {
            var list = _marketTable.GetByPartitionKey<DynamicTableEntity>("position" + ":company:" + companyID + ":exchange:" + _exchangeID);

            return convertToPosition(list);
        }

        public IEnumerable<Position<string, long>> GetAllStockPositions()
        {
            var list = _marketTable.GetByPartitionKey<DynamicTableEntity>("position" +  ":exchange:" + _exchangeID);

            return convertToPosition(list);
        }

        public IEnumerable<Position<string, long>> GetStockPositionByAccountID(string accountID)
        {
            var list = _marketTable.GetByPartitionKey<DynamicTableEntity>("position" + ":account:" + accountID + ":exchange:" + _exchangeID);

            return convertToPosition(list);
        }

        public ServiceResult Save(Position<string,long> p)
        {
            DynamicTableEntity e = new DynamicTableEntity();

            e.SetProperty<long>("Quantity", p.Quantity);
            e.SetProperty<string>("AccountID", p.StockExchangeAccountID);
            e.SetProperty<long>("TotalCost", p.TotalCost);
            e.SetProperty<long>("Share_CurrentPrice", p.Share.CurrentPrice);
            e.SetProperty<string>("Share_CompanySymbol", p.Share.Symbol);
            try
            {
                
                var c = GetCompanyByStockSymbol(p.Share.Symbol);

                //base
                e.PartitionKey = "position" + ":exchange:" + _exchangeID;
                e.RowKey = "accountID:" + p.StockExchangeAccountID + ":company:" + c.StockExchangeCompanyID;
                _marketTable.Insert<DynamicTableEntity>(e, true);

                //by company
                e.PartitionKey = "position" + ":company:" + c.StockExchangeCompanyID + ":exchange:" + _exchangeID;
                e.RowKey = "accountID:" + p.StockExchangeAccountID + ":company:" + c.StockExchangeCompanyID;
                _marketTable.Insert<DynamicTableEntity>(e, true);

                //by account
                e.PartitionKey = "position" + ":account:" + p.StockExchangeAccountID + ":exchange:" + _exchangeID;
                e.RowKey = "accountID:" + p.StockExchangeAccountID + ":company:" + c.StockExchangeCompanyID;
                _marketTable.Insert<DynamicTableEntity>(e, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }

            return true;
        }

        private IEnumerable<Position<string,long>> convertToPosition(IEnumerable<DynamicTableEntity> elist)
        {
            List<Position<string, long>> r = new List<Position<string, long>>();
            foreach(var e in elist)
            {
                r.Add(convertToPosition(e));
            }

            return r;
        }
        private Position<string,long> convertToPosition(DynamicTableEntity e)
        {
            var p = new Position<string, long>();
            p.Quantity = e.GetPropertyValue<int>("Quantity", 0);
            p.StockExchangeAccountID = e.GetPropertyValue<string>("AccountID", string.Empty);
            p.TotalCost = e.GetPropertyValue<long>("TotalCost", 0);

            var s = new Share<string, long>();
            s.CurrentPrice = e.GetPropertyValue<long>("Share_CurrentPrice", 0);
            s.ID = string.Empty;
            s.Symbol = e.GetPropertyValue<string>("Share_CompanySymbol", string.Empty);

            p.Share = s;

            return p;
        }



        #endregion



        #region TradeTransations


        public TradeTransactionNote<string> GetTradeTranByID(string key)
        {
            var e = _tranlogTable.GetEntity<DynamicTableEntity>("exchange:" + _exchangeID + ":transaction", "id:" + key);

            return convertToTradeTran(e);

        }

        public IEnumerable<TradeTransactionNote<string>> GetTradeTransByAccountID(string id)
        {
            var e = _tranlogTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":account:" + id + ":transaction");

            return convertToTradeTran(e);
        }

        public IEnumerable<TradeTransactionNote<string>> GetTradeTransByCompanyID(string companyid, int? count)
        {
            var e = _tranlogTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":company:" + companyid + ":transaction");

            if (count == null)
                return convertToTradeTran(e);
            else
                return convertToTradeTran(e).OrderBy(t => t.TransactionDate).Take((int)count);


        }

        public IEnumerable<TradeTransactionNote<string>> GetTradeTransByDate(DateTime date)
        {
            var e = _tranlogTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":day:" + date.ToJB2DateKey() + ":transaction");

            return convertToTradeTran(e);
        }

        public IEnumerable<TradeTransactionNote<string>> GetTradeTransByCompanyDate(string companyID, DateTime date)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TradeTransactionNote<string>> GetAllTradeTrans()
        {
            var e = _tranlogTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":transaction");

            return convertToTradeTran(e);
        }

        public ServiceResult Save(TradeTransactionNote<string> note)
        {
            DynamicTableEntity e = new DynamicTableEntity();

            e.SetProperty<string>("AccountID", note.AccountID);
            e.SetProperty<string>("CheckSum", note.CheckSum);
            e.SetProperty<string>("CompanyID", note.CompanyID);
            e.SetProperty<string>("ExchangeID", _exchangeID);
            e.SetProperty<int>("ShareCount", note.ShareCount);
            e.SetProperty<int>("SharePrice", (int)note.SharePrice.Amount);
            e.SetProperty<string>("TradeType", note.TradeType.ToString());
            e.SetProperty<string>("TransactionID", note.TransactionID);
            e.SetProperty<DateTime>("TransactionDate", note.TransactionDate);
            e.SetProperty<bool>("IsComplete", note.IsComplete);
            e.SetProperty<string>("Message", note.Message);


            try
            {
                e.PartitionKey = "exchange:" + _exchangeID + ":transaction";
                e.RowKey = "id:" + note.TransactionID;
                _tranlogTable.Insert<DynamicTableEntity>(e, true);

                e.PartitionKey = "exchange:" + _exchangeID + ":day:" + note.TransactionDate.ToJB2DateKey() + ":transaction";
                e.RowKey = "id:" + note.TransactionID;
                _tranlogTable.Insert<DynamicTableEntity>(e, true);

                e.PartitionKey = "exchange:" + _exchangeID + ":company:" + note.CompanyID + ":transaction";
                e.RowKey = "id:" + note.TransactionID;
                _tranlogTable.Insert<DynamicTableEntity>(e, true);

                e.PartitionKey = "exchange:" + _exchangeID + ":account:" + note.AccountID + ":transaction";
                e.RowKey = "id:" + note.TransactionID;
                _tranlogTable.Insert<DynamicTableEntity>(e, true);
            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }

            return true;


        }

        private IEnumerable<TradeTransactionNote<string>> convertToTradeTran(IEnumerable<DynamicTableEntity> elist)
        {
            List<TradeTransactionNote<string>> list = new List<TradeTransactionNote<string>>(elist.Count());

            foreach (var e in elist)
            {
                list.Add(convertToTradeTran(e));
            }

            return list;

        }

        private TradeTransactionNote<string> convertToTradeTran(DynamicTableEntity e)
        {
            TradeTransactionNote<string> note = new TradeTransactionNote<string>();

            note.AccountID = e.PropertyStringValue("AccountID");
            note.CheckSum = e.PropertyStringValue("CheckSum");
            note.CompanyID = e.PropertyStringValue("CompanyID");
            note.ExchangeID = _exchangeID;
            note.ShareCount = e.GetPropertyValue<int>("ShareCount", 0);
            note.SharePrice = new CurrencyAmountPair(JB2.Settings.JbeanStockMarket.DefaultCurrency, e.GetPropertyValue<double>("SharePrice", 0.00));
            note.TradeType = (TradeType)System.Enum.Parse(typeof(TradeType), e.PropertyStringValue("TradeType"));
            note.TransactionID = e.PropertyStringValue("TransactionID");
            note.TransactionDate = e.GetPropertyValue<DateTime>("TransactionDate", System.DateTime.Now);
            note.IsComplete = e.GetPropertyValue<bool>("IsComplete", false);
            note.Message = e.GetPropertyValue<string>("Message", string.Empty);

            return note;
        }


        #endregion TradeTrans

        #region StockPrice

        public StockPrice<string, long> GetStockPriceByCompanyDatetime(string companyID, DateTime date)
        {
            var e = _priceTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":company:" + companyID + ":stockPrice");

            if (e == null)
                return new StockPrice<string, long>();

            var prices = convertToStockPrice(e);

            var filters = prices.Where(p => p.PriceDate <= date).OrderBy(p => p.PriceDate);

            if (filters.Count() > 0)
                return filters.ToList()[0];
            else
                return new StockPrice<string, long>();

        }

        public IEnumerable<StockPrice<string, long>> GetStockPriceByDatetime(DateTime date)
        {
            var e = _priceTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":day:" + date.ToJB2DateKey() + ":stockPrice");

            var prices = convertToStockPrice(e);

            var filters = prices.Where(p => p.PriceDate <= date).OrderBy(p => p.StockExchangeCompanyID);


            List<StockPrice<string, long>> result = new List<StockPrice<string, long>>();


            //get only the latest prices that price during the date
            string companyID = filters.ToList()[0].StockExchangeCompanyID;
            foreach( var s in filters)
            {
                if (companyID != s.StockExchangeCompanyID)
                    result.Add(filters.Where(p => p.StockExchangeCompanyID == companyID).OrderBy(p => p.PriceDate).ToList()[0]);
                companyID = s.StockExchangeCompanyID;
            }

            return result;

        }

        public IEnumerable<StockPrice<string, long>> GetStockPricesByCompany(string companyID)
        {
            var e = _priceTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":company:" + companyID + ":stockPrice");

            var prices = convertToStockPrice(e);

            return prices;

        }

        public IEnumerable<StockPrice<string, long>> GetStockPricesByCompany(string companyID, int count)
        {
            var e = _priceTable.GetByPartitionKey<DynamicTableEntity>("exchange:" + _exchangeID + ":company:" + companyID + ":stockPrice");

            var prices = convertToStockPrice(e);

            return prices.OrderBy(p => p.PriceDate).Take(count);
        }

        public ServiceResult Save(StockPrice<string, long> stockPrice)
        {
            DynamicTableEntity e = new DynamicTableEntity();

            e.SetProperty<string>("CompanyID", stockPrice.StockExchangeCompanyID);
            e.SetProperty<DateTime>("PriceDate", stockPrice.PriceDate);
            e.SetProperty<long>("Value", stockPrice.Value);

            try
            {

                e.PartitionKey = "exchange:" + _exchangeID + ":company:" + stockPrice.StockExchangeCompanyID + ":stockPrice";
                e.RowKey = "datetime:" + stockPrice.PriceDate.ToJB2DateKey() + "_" + stockPrice.PriceDate.ToString("HHmmss");
                _priceTable.Insert<DynamicTableEntity>(e, true);

                e.PartitionKey = "exchange:" + _exchangeID + ":day:" + stockPrice.PriceDate.ToJB2DateKey() + ":stockPrice";
                e.RowKey = "datetime:" + stockPrice.PriceDate.ToJB2DateKey() + "_" + stockPrice.PriceDate.ToString("HHmmss") + ":company:" + stockPrice.StockExchangeCompanyID;
                _priceTable.Insert<DynamicTableEntity>(e, true);


            }
            catch (Exception ex)
            {
                return new ServiceResult(ex);
            }

            return true;
        }

        private IEnumerable<StockPrice<string, long>> convertToStockPrice(IEnumerable<DynamicTableEntity> elist)
        {
            List<StockPrice<string, long>> list = new List<StockPrice<string, long>>();

            foreach (var e in elist)
            {
                list.Add(convertToStockPrice(e));
            }

            return list;
        }

        private StockPrice<string,long> convertToStockPrice(DynamicTableEntity e)
        {
            StockPrice<string, long> newPrice = new StockPrice<string, long>();

            newPrice.PriceDate = e.GetPropertyValue<DateTime>("PriceDate", System.DateTime.Now);
            newPrice.StockExchangeCompanyID = e.GetPropertyValue<string>("CompanyID", string.Empty);
            newPrice.Value = e.GetPropertyValue<long>("Value", 0);

            return newPrice;

        }

        


        #endregion StockPrice




    }
}
