using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;



namespace JB2.JBeanCurrency
{
    public class CurrencySystem : JB2.Bowtie.CurrencySystem, ICurrencySystem
    {
        #region Fields
        protected ICurrencyTreasury  _treasury;

        #endregion Fields

        public CurrencySystem()
        {
            _id = JB2.Settings.Jbean.Factory.Currencies[0].ID + "config";
            _name = JB2.Settings.Jbean.Factory.Currencies[0].Name + " Config";
            _treasury = new JB2.JBeanCurrency.BowtieTreasury();
        }

        public override string CurrencyID
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

        public override ICurrencyTreasury Treasury
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
