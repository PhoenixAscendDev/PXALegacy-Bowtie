using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public abstract class CurrencySystem : GenericSystem , ICurrencySystem, ISystem
    {
        public CurrencySystem() : base(Enum.SystemType.Currency)
        {
            
        }

        public CurrencySystem(string currencyID, ICurrencyTreasury treasury) : this()
        {
            CurrencyID = currencyID;
            Treasury = treasury;
        }

        public abstract string CurrencyID { get; set; }

        public abstract ICurrencyTreasury Treasury { get; set; }
        
    }
}
