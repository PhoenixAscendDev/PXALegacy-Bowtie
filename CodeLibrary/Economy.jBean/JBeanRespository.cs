using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Economy;

namespace JB2.Bowtie.Data.Linq
{
    public class JBeanRespository : JB2.Bowtie.Economy.ITreasuryRepository<JBeanTreasuryLogEntry,Enum.JBeanTokenType,string>
    {
        public bool AddLog(JBeanTreasuryLogEntry log)
        {
            using(JBeanDataContext dc = new JBeanDataContext())
            {
                jb2bt_TreasuryLog_AddResult result = dc.jb2bt_TreasuryLog_Add(log.ApplicationKey, log.DenominationType.ToString(), log.Quantity, log.CurrencyKey).FirstOrDefault();
            }
            return true;
        }
    }
}
