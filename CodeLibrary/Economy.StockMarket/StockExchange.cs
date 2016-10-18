using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public abstract class StockExchange<TCompany, TShareHolder, TKey, TStockValue> : JB2Class, IStockExchange<TCompany, TShareHolder, TKey, TStockValue>
        where TKey : IComparable
        where TStockValue : IComparable
        where TShareHolder : IShareHolder<TKey, TStockValue>
        where TCompany : IStockBusiness<TShareHolder, TKey, TStockValue>
    {

        public StockExchange() : base()
        {
            ShareHolderCreated += Log_ShareHolderCreated;
            ShareTraded += Log_ShareTraded;
            StockPriceChanged += Log_StockPriceChange;
            CompanyAdded += Log_CompanyAdded;
            CompanyRemoved += Log_CompanyRemoved;

        }


        #region Events
        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TShareHolder> ShareHolderCreated;
        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TradeTransactionNote<TKey>, TShareHolder, TCompany> ShareTraded;
        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TradeTransactionNote<TKey>, TShareHolder, TCompany> ShareBought;
        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TradeTransactionNote<TKey>, TShareHolder, TCompany> ShareSold;

        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TCompany, StockPrice<TKey, TStockValue>, TStockValue> StockPriceChanged;
        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TCompany, StockPrice<TKey, TStockValue>, TStockValue> StockPriceIncrease;
        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TCompany, StockPrice<TKey, TStockValue>, TStockValue> StockPriceDecrease;

        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TCompany> CompanyAdded;
        public event Action<IStockExchange<TCompany, TShareHolder, TKey, TStockValue>, TCompany> CompanyRemoved;

        protected virtual void OnShareHolderCreated(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TShareHolder newholder)
        {
            if (ShareHolderCreated != null)
                ShareHolderCreated(this, (TShareHolder)newholder);
        }


        protected virtual void OnShareTraded(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TradeTransactionNote<TKey> tradeTran, TShareHolder shareHolder, TCompany company)
        {
            if (ShareTraded != null)
                ShareTraded(exchange, tradeTran, shareHolder, company);
        }
        protected virtual void OnShareBought(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TradeTransactionNote<TKey> tradeTran, TShareHolder shareHolder, TCompany company)
        {
            if (ShareTraded != null)
                ShareTraded(exchange, tradeTran, shareHolder, company);
        }

        protected virtual void OnShareSold(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TradeTransactionNote<TKey> tradeTran, TShareHolder shareHolder, TCompany company)
        {
            if (ShareTraded != null)
                ShareTraded(exchange, tradeTran, shareHolder, company);
        }

        protected virtual void OnStockPriceChanged(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company, StockPrice<TKey, TStockValue> newprice, TStockValue lastprice)
        {
            if (StockPriceChanged != null)
                StockPriceChanged(exchange, company, newprice, lastprice);
        }

        protected virtual void OnStockPriceDecrease(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company, StockPrice<TKey, TStockValue> newprice, TStockValue lastprice)
        {
            if (StockPriceDecrease != null)
                StockPriceDecrease(exchange, company, newprice, lastprice);
        }
        protected virtual void OnStockPriceIncrease(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company, StockPrice<TKey, TStockValue> newprice, TStockValue lastprice)
        {
            if (StockPriceIncrease != null)
                StockPriceIncrease(exchange, company, newprice, lastprice);
        }

        protected virtual void OnCompanyRemoved(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company)
        {
            if (CompanyRemoved != null)
                CompanyRemoved(exchange, company);
        }

        protected virtual void OnCompanyAdded(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company)
        {
            if (CompanyAdded != null)
                CompanyAdded(exchange, company);
        }

        #endregion Events;

        public virtual TKey ID
        {
            get
            {
                return this.GetProperity<TKey>("ID");
            }

            set
            {
                this.SetProperty<TKey>("ID", value);
            }
        }

        public virtual string Name
        {
            get
            {
                return this.GetProperity<string>("NAME");
            }

            set
            {
                this.SetProperty<string>("NAME", value);
            }
        }

        public virtual IBusiness<TKey> Owner
        {
            get
            {
                return this.GetProperity<IBusiness<TKey>>("OWNER");
            }

            set
            {
                this.SetProperty<IBusiness<TKey>>("OWNER", value);
            }
        }

        public abstract bool IsOpen
        {
            get;
        }

        public abstract TCompany GetCompany(string stockSymbol);


        public virtual TKey GetID()
        {
            return ID;
        }

        public virtual string GetName()
        {
            return Name;
        }

        public abstract TShareHolder GetShareHolder(TKey accountID);

        public abstract Position<TKey, TStockValue> GetShare(string transactionID);

        public abstract TradeTransactionNote<TKey> Trade(string holderAccountID, string stockSymbol, int quantity, TradeType tradeType, long? askPrice);

        public abstract DateTime GetLastOpenDate();

        public abstract DateTime GetNextCloseTime();

        public abstract DateTime GetLastTradeTime();

        public abstract ServiceResult ValidateTrade(TradeTransactionNote<TKey> note);

        public abstract TShareHolder OpenNewAccount(string bankAccountID);

        public abstract StockPrice<TKey, TStockValue> UpdateStockPrice(TKey companyID, TStockValue value);

        public abstract ILogger GetLogger();


        public abstract TCompany AddCompany(TCompany company, TStockValue initialValue);

        public abstract ServiceResult RemoveCompany(TCompany company);




        #region Log Events

        protected virtual void Log_ShareHolderCreated(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TShareHolder holder)
        {
            ILogEntry newEntry = null;

            newEntry = new Common.Log.LogEntry("Info-ShareHolderCreated" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
                              "Share Holder Created " + "\r\n"
                              + "Exchange=" + exchange.GetID() + "\r\n"
                              + "AccountID=" + holder.StockExchangeAccountID, null, DateTime.Now);
            var logger = this.GetLogger();

            if (logger != null)
                logger.Log(newEntry);

            //return newEntry;
        }

        protected virtual void Log_ShareTraded(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TradeTransactionNote<TKey> note, TShareHolder holder, TCompany company)
        {
            ILogEntry newEntry = null;
            newEntry = new Common.Log.LogEntry("Info-ShareTraded" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
                  "Share Traded: " + note.TradeType.ToString() + " " + note.ShareCount.ToString() + " Shares of " + company.StockSymbol + " " + "\r\n"
                  + "Exchange=" + exchange.GetID() + "\r\n"
                  + "AccountID=" + holder.StockExchangeAccountID + "\r\n"
                  + "StockSymbol=" + company.StockSymbol + "\r\n"
                  + "TradeType=" + note.TradeType.ToString() + "\r\n"
                  + "Quanity=" + note.ShareCount + "\r\n"
                  + "Price=" + note.SharePrice + "\r\n"
                  , null, DateTime.Now);


            var logger = this.GetLogger();

            if (logger != null)
                logger.Log(newEntry);

            //return newEntry;
        }

        protected virtual void Log_StockPriceChange(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company, StockPrice<TKey, TStockValue> oldPrice, TStockValue newPrice)
        {
            ILogEntry newEntry = null;
            string sign = (oldPrice.Value.CompareTo(newPrice) >= 0) ? "+" : "-";
            string updown = (oldPrice.Value.CompareTo(newPrice) >= 0) ? "Up" : "Down";
            int change = (oldPrice.Value.CompareTo(newPrice));
            newEntry = new Common.Log.LogEntry("Info-ShareTraded" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
              "Stock Price Changed: " + company.StockSymbol + " " + sign + change.ToString() + " \r\n"
              + "Exchange=" + exchange.GetID() + "\r\n"
              + "Stock Symbol=" + company.StockSymbol + "\r\n"
              + "Up/Down=" + updown + "\r\n"
              + "OldPrice=" + oldPrice.Value.ToString() + "\r\n"
              + "NewPrice=" + newPrice.ToString() + "\r\n"
      , null, DateTime.Now);


            var logger = this.GetLogger();

            if (logger != null)
                logger.Log(newEntry);
            //return newEntry;
        }

        protected virtual void Log_CompanyAdded(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company)
        {
            ILogEntry newEntry = null;
            
            newEntry = new Common.Log.LogEntry("Info-CompanyAdded" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
              "Company Added: " + company.Name + " (" + company.StockSymbol  + ") \r\n"
              + "Exchange=" + exchange.GetID() + "\r\n"
              + "Stock Symbol=" + company.StockSymbol + "\r\n"
              + "Stock Price=" + company.GetCurrentStockValue(), null, DateTime.Now);

            Logit(newEntry);


        }

        protected virtual void Log_CompanyRemoved(IStockExchange<TCompany, TShareHolder, TKey, TStockValue> exchange, TCompany company)
        {
            ILogEntry newEntry = null;

            newEntry = new Common.Log.LogEntry("Info-CompanyRemoved" + JB2.Common.NewID.ShortGuid(), Common.Enum.LogServerityType.Informational,
              "Company Removed: " + company.Name + " (" + company.StockSymbol + ") \r\n"
              + "Exchange=" + exchange.GetID() + "\r\n"
              + "Stock Symbol=" + company.StockSymbol + "\r\n"
              + "Stock Price=" + company.GetCurrentStockValue(), null, DateTime.Now);

            Logit(newEntry);
        }

        protected virtual void Logit(ILogEntry entry)
        {
            var logger = this.GetLogger();

            if (logger != null)
                logger.Log(entry);

        }


        #endregion Log Events








    }
}
