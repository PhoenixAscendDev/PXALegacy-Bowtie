using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class PlayerWallet : JB2.Common.IDValue<string>, IWallet<JB2.Common.IPerson<string>,string>
    {

        #region Fields
        private JB2.Common.IPerson<string> _player;
        private Dictionary<string, CurrencyAmountPair> _amounts;

        #endregion
        

        public JB2.Common.IPerson<string> Owner
        {
            get
            {
                return _player;
            }
        }

        public void AddAmount(ICurrency currency, double quantity)
        {
            if (_amounts.ContainsKey(currency.ID))
                _amounts[currency.ID].DecreaseAmount(quantity);
            else
                _amounts.Add(currency.ID, new CurrencyAmountPair(currency, quantity));

        }

        public void RemoveAmount(ICurrency currency, double quantity)
        {
            if (_amounts.ContainsKey(currency.ID))
                _amounts[currency.ID].DecreaseAmount(quantity);           
        }

        public double CurrencyTotal(ICurrency currency)
        {
            if (_amounts.ContainsKey(currency.ID))
                return (double)_amounts[currency.ID];
            else
                return 0;
        }

        public override string GetID()
        {
            return base.ID;
        }
    }
}
