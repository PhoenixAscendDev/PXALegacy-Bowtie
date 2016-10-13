using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public struct Share<TKey,TStockPrice>
        where TStockPrice : IComparable
        where TKey : IComparable
    {
        public TKey ID { get; set; }
        public TStockPrice CurrentPrice { get; set; }
        public string Symbol { get; set; }
    }
}
