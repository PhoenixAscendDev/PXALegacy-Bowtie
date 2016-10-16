using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class EconomicFactory<TBankAccountHolder, TBankAccountStatus, TRequest, TRequestor, TID, TServerity, TLogEntry>
        where TID : IComparable
        where TRequestor : IRequestor<TID>
        where TRequest : ITreasuryRequest<TID, TRequestor>
        where TLogEntry : JB2.Common.ILogEntry<TID, TServerity>
        where TServerity : IComparable
    {
        


        public ITreasury Treasury { get; set; }
        public ICurrency[] Currencies { get; set; }
        public IDenomination[] Denominations { get; set; }
        public JB2.Common.SettingCollection<string> Settings { get; set; }
        public IBank<TBankAccountHolder, TBankAccountStatus, TRequest, TRequestor, TID> CentralBank { get; set; }
        public Common.ILogger<TServerity, TID, TLogEntry> Logger { get; set; }

        
        

    }
}
