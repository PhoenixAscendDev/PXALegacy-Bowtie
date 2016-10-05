using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public struct StockPrice<TKey,TStockValue>
    {
        public TKey StockExchangeCompanyID { get; set; }
        public DateTime PriceDate { get; set; }
        public TStockValue Value { get; set; }
    }
}
