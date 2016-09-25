using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IStockShare<TKey,TValue>
    {
        IStockable<TKey,TValue> Company { get; set; }
        TKey StockExchangeAccountID { get; set; } 
        DateTime DatePurchased { get; set; }
        int Quantity { get; set; }
        TValue PurchaseAmount { get; set; }

        string ExchangeTransactionID { get; set; }
    }
}
