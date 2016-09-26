using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Economy;

namespace JB2.Settings
{
    public static class JbeanStockMarket
    {
        public static JbeanStockExchange StockExchange { get; }

        public static IJbeanStockMarketRepository Repository { get; }

        public static ICurrency DefaultCurrency { get; }

        public static IRequestor JBeanTreasuryRequestor { get; }

    }
}
