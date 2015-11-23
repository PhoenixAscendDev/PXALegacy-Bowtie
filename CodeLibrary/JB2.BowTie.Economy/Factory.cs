using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public class EconomicFactory
    {
        public ITreasury Treasury { get; set; }
        public ICurrency[] Currencies { get; set; }
        public IDenomination[] Denominations { get; set; }
        public JB2.Common.SettingCollection<string> Settings {get;set;}
        public IBank CentralBank { get; set; }
    }
}
