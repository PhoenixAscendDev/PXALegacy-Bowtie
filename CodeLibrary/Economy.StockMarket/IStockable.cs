using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IStockable<TKey,TStockValue>
    {
        TKey StockExchangeCompanyID { get; set; }
        string StockSymbol { get; set; }

        TStockValue GetCurrentStockValue();
        TKey ExchangeID { get; set; }
        
    }
}
