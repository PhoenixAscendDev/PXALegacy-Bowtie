using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;


namespace JB2.Economy
{
    public interface IJbeanStockMarketRepository : IStockMarketRepository<JbeanStockCompany, JbeanStockExchange, IJbeanStockHolder, string, long>
    {

        IEnumerable<ISetting> GetDefaultSettings(string exchangeID);

    }
}
