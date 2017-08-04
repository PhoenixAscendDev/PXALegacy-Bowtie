using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class CurrencyConfig :  ICurrencySystem
    {
        #region Constructor

        public CurrencyConfig(string currencyID)
        {

        }


        #endregion Constructor

        #region ICurrencyConfig
        public string CurrencyID { get; set; }

        public string ID { get; set; }
        

        public string Name { get; set; }

        public ICurrencyTreasury Treasury { get; set; }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }


        #endregion ICurrencyConfig
    }
}
