using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Economy
{
    public abstract class StockShare<TKey, TStockValue> : JB2Class, IStockShare<TKey, TStockValue>
    {
        public virtual IStockable<TKey,TStockValue> Company
        {
            get
            {
                return this.GetProperity<IStockable<TKey, TStockValue>>("COMPANY");
            }

            set
            {
                this.SetProperty<IStockable<TKey, TStockValue>>("COMPANY", value);
            }
        }

        public virtual DateTime DatePurchased
        {
            get
            {
                return this.GetProperity<DateTime>("DATE");
            }

            set
            {
                this.SetProperty<DateTime>("DATE", value);
            }
        }

        public virtual string ExchangeTransactionID
        {
            get
            {
                return this.GetProperity<string>("TRANSID");
            }

            set
            {
                this.SetProperty<string>("TRANSID", value);
            }
        }

        public virtual TStockValue PurchaseAmount
        {
            get
            {
                return this.GetProperity<TStockValue>("AMOUNT");
            }

            set
            {
                this.SetProperty<TStockValue>("AMOUNT", value);
            }
        }

        public virtual int Quantity
        {
            get
            {
                return this.GetProperity<int>("QUANTITY");
            }

            set
            {
                this.SetProperty<int>("QUANTITY", value);
            }
        }

        public virtual TKey StockExchangeAccountID
        {
            get
            {
                return this.GetProperity<TKey>("HOLDER");
            }

            set
            {
                this.SetProperty<TKey>("HOLDER", value);
            }
        }
    }
}
