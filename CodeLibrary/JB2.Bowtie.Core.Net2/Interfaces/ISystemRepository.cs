using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface ISystemRepository : JB2.Common.IRepository<JB2.Bowtie.ISystemConfig, string>
    {
        IEnumerable<ISystemConfig<IPointSystem>> GetPointSystems();

        IEnumerable<ISystemConfig<ICurrencySystem>> GetCurrencySystems();

        ISystemConfig<ICurrencySystem> GetCurrencySystemByConfigID(string configID);

        ISystemConfig<IPointSystem> GetPointSystemByConfigID(string configID);

        IEnumerable<ISystemConfig> GetSystemsByCategory(string category);


    }
}
