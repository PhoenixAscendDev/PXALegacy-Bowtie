using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public abstract class StockExchange<TCompany,TShareHolder,TShare,TKey, TStockValue> : JB2Class, IStockExchange<TCompany, TShareHolder,TShare, TKey, TStockValue>
        where TKey : IComparable
        where TShareHolder : IShareHolder<TShare,TKey,TStockValue>
        where TCompany : IStockBusiness<TShareHolder,TShare,TKey,TStockValue>
        where TShare : IStockShare<TKey, TStockValue>
    {


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

        public abstract TShare GetShare(string transactionID);

        public abstract TradeTransactionNote<TKey> Trade(string holderAccountID, string stockSymbol, int quantity, TradeType tradeType);

        public abstract DateTime GetLastOpenDate();

        public abstract DateTime GetNextCloseTime();

        public abstract DateTime GetLastTradeTime();
        
    }
}
