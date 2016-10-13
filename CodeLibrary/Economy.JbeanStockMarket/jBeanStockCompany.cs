using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public class JbeanStockCompany : JB2Class, IStockBusiness<IJbeanStockHolder, string, long>
    {

        #region Constructor

        public JbeanStockCompany(string id)
        {
            
            this._props = new MetaDataCollection();          
            this._defaultchangeLastUpdate = true;
            this._lastupdate = System.DateTime.Now;
            this.ID = id;
        }

        #endregion Constructor

        public string ExchangeID
        {
            get
            {
                return this.GetProperity<string>("EXCHANGEID");
            }

            set
            {
                this.SetProperty<string>("EXCHANGEID", value);
            }
        }

        public string ID
        {
            get
            {
                return this.GetProperity<string>("ID");
            }

            set
            {
                this.SetProperty<string>("ID", value);
            }
        }

        public IAddress MailingAddress
        {
            get
            {
                IAddress address = this.GetProperity<IAddress>("ADDRESS");
                if (address == null)
                    return new JB2.Common.Map.EmptyAddress();
                return address;
            }

            set
            {
                this.SetProperty<IAddress>("ADDRESS", value);
            }
        }

        public string Name
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

        public IPerson<string> POC
        {
            get
            {
                return this.GetProperity<IPerson<string>>("POC");
            }

            set
            {
                this.SetProperty<IPerson<string>>("POC", value);
            }
        }

        public string StockExchangeCompanyID
        {
            get
            {
                string exchangeCompanyID = this.GetProperity<string>("EXCHANGECOMPANYID");
                if (string.IsNullOrEmpty(exchangeCompanyID))
                    return this.ID;
                else
                    return exchangeCompanyID;
            }

            set
            {
                this.SetProperty<string>("EXCHANGECOMPANYID", value);
            }
        }

        public string StockSymbol
        {
            get
            {
                return this.GetProperity<string>("STOCKSYMBOL");
            }

            set
            {
                this.SetProperty<string>("STOCKSYMBOL", value);
            }
        }

        public long GetCurrentStockValue()
        {
            try
            {
                var price = JB2.Settings.JbeanStockMarket.Repository.GetStockPricesByCompany(this.StockExchangeCompanyID, 1);
                if (price != null)
                    return price.FirstOrDefault().Value;
                else
                    throw new NullReferenceException();
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public string GetID()
        {
            return this.ID;
        }

        public IEnumerable<StockPrice<string, long>> GetLastStockPrices(int count)
        {
            try
            {
                var price = JB2.Settings.JbeanStockMarket.Repository.GetStockPricesByCompany(this.StockExchangeCompanyID, count);
                if (price != null)
                    return price;
                else
                    throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                return new List<StockPrice<string, long>>();
            }
        }

        public DateTime GetLastTradeDate()
        {
            try
            {
                var trades = JB2.Settings.JbeanStockMarket.Repository.GetTradeTransByCompanyID(this.StockExchangeCompanyID, 1);
                 if (trades != null)
                    return trades.FirstOrDefault().TransactionDate;
                else
                    throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                return DateTime.MinValue;
            }
        }

        public string GetName()
        {
            return this.Name;
        }

        public int GetShareCount()
        {
            try
            {
                var price = JB2.Settings.JbeanStockMarket.Repository.GetStockPositionsByCompanyID(this.StockExchangeCompanyID);


                if (price != null)
                {
                    int total = 0;
                    foreach(var s in price)
                    {
                        total = total + s.Quantity;
                    }
                    return total;
                }             
                else
                    throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public IJbeanStockHolder GetShareHolder(string accountID)
        {
            return JB2.Settings.JbeanStockMarket.StockExchange.GetShareHolder(accountID);
        }

        public IEnumerable<IJbeanStockHolder> GetShareHolders()
        {
            var shares = JB2.Settings.JbeanStockMarket.Repository.GetStockPositionsByCompanyID(this.StockExchangeCompanyID);

            Dictionary<string, IJbeanStockHolder> result = new Dictionary<string, IJbeanStockHolder>();

            foreach(var s in shares)
            {
                var holder = JB2.Settings.JbeanStockMarket.StockExchange.GetShareHolder(s.StockExchangeAccountID);
                if (!result.ContainsKey(holder.StockExchangeAccountID))
                    result.Add(holder.StockExchangeAccountID, holder);
            }

            return result.Values.ToList();
        }

        public IEnumerable<StockPrice<string, long>> GetStockPricesRange(DateTime min, DateTime max)
        {
            try
            {
                var price = JB2.Settings.JbeanStockMarket.Repository.GetStockPricesByCompany(this.StockExchangeCompanyID);
                if (price != null)
                    return price.Where(p => p.PriceDate >= min && p.PriceDate <= max).OrderBy(p => p.PriceDate);
                else
                    throw new NullReferenceException();
            }
            catch (Exception ex)
            {
                return new List<StockPrice<string, long>>();
            }
        }

        public static JbeanStockCompany New
        {
            get
            {
                var id = JB2.Helper.JbeanStockMarket.GenerateID<JbeanStockCompany>();
                return new JbeanStockCompany(id);
            }
        }
    }
}
