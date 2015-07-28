using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public interface ITreasuryRepository<TLog,TDenominationType,TCurrencyKey>
       where TLog : ITreasuryLogEntry<TDenominationType,TCurrencyKey>
    {
        bool AddLog(TLog log);
    }
}
