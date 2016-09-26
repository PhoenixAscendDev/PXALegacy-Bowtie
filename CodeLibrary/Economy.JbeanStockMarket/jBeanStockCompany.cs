using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public class JbeanStockCompany : JB2Class, IStockBusiness<IJbeanStockHolder, JbeanStockShare, string, long>
    {

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
                return this.GetProperity<IAddress>("ADDRESS");
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
                return this.GetProperity<string>("EXCHANGECOMPANYID");
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
            throw new NotImplementedException();
        }

        public string GetID()
        {
            return this.ID;
        }

        public IEnumerable<StockPrice<string, long>> GetLastStockPrices(int count)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastTradeDate()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            return this.Name;
        }

        public int GetShareCount()
        {
            throw new NotImplementedException();
        }

        public IJbeanStockHolder GetShareHolder(string accountID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IJbeanStockHolder> GetShareHolders()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<StockPrice<string, long>> GetStockPricesRange(DateTime min, DateTime max)
        {
            throw new NotImplementedException();
        }
    }
}
