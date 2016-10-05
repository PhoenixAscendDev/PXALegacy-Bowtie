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
            var result = JB2.Configuration.GetjBeanStockMarketID() + "-" + JB2.Common.NewID.Guid();


            Type type = typeof(T);

            if (type is IJbeanStockHolder)
                result = "holder-" + result;

            return result;
        }
    }
}
