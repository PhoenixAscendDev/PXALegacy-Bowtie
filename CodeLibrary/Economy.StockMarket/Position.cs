using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public struct Position<TKey,TStockPrice>
        where TStockPrice : IComparable
        where TKey : IComparable
    {
        public Share<TKey,TStockPrice> Share { get; set; }
        public TKey StockExchangeAccountID { get; set; }
        public int Quantity { get; set; }
        public TStockPrice TotalCost { get; set; }

    }
}
