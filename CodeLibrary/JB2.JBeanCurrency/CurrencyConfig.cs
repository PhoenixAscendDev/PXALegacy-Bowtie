using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;



namespace JB2.JBeanCurrency
{
    public class CurrencyConfig : JB2.Common.IDNamePair<string,string>, ICurrencyConfig
    {
        #region Fields
        protected ICurrencyTreasury  _treasury;

        #endregion Fields

        public CurrencyConfig() : base(JB2.Settings.Jbean.Factory.Currencies[0].ID + "config", JB2.Settings.Jbean.Factory.Currencies[0].Name + " Config")
        {
            _treasury = new JB2.JBeanCurrency.BowtieTreasury();

        }

        public string CurrencyID
        {
            get
            {
                return JB2.Settings.Jbean.Factory.Currencies[0].ID;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public ICurrencyTreasury Treasury
        {
            get
            {
                return _treasury;
            }

            set
            {
                _treasury = value;
            }
        }

        
       
    }
}
