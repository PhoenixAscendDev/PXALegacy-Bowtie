using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface ISystemRepository : JB2.Common.IRepository<JB2.Bowtie.ISystemConfig, string>
    {
        IEnumerable<ISystemConfig<IPointSystem>> GetPointSystems();

        IEnumerable<ISystemConfig<ICurrencySystem>> GetCurrencySystems();

        ISystemConfig<ICurrencySystem> GetCurrencySystemByConfigID(string configID);

        ISystemConfig<IPointSystem> GetPointSystemByConfigID(string configID);


    }
}
