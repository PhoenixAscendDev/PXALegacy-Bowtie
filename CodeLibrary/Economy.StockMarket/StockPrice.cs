using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public struct StockPrice<TKey,TStockValue>
    {
        TKey StockExchangeCompanyID { get; set; }
        DateTime PriceDate { get; set; }
        TStockValue Value { get; set; }
    }
}
