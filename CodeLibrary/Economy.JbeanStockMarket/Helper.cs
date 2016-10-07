using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using JB2.Economy;

namespace JB2.Helper
{
    public static class JbeanStockMarket
    {

        public static string GenerateID<T>()
            where T: class
        {
            var result = JB2.Common.NewID.UriHash( new Uri("http://jbean.stockmarket.com/?exchangeID=" + JB2.Configuration.GetjBeanStockMarketID())) + "." + JB2.Common.NewID.ShortGuid();
            Type type = typeof(T);

            if (type is IJbeanStockHolder)
                result = "h." + result;

            if (type == typeof(JbeanStockCompany))
                result = "c." + result;

            return result;
        }
    }
}
