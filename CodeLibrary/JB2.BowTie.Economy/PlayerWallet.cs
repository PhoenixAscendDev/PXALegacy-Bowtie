using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class PlayerWallet : IWallet
    {

        #region Fields
        private string _id;
        private string _playerId;
        private Dictionary<string, CurrencyAmountPair> _amounts;

        #endregion
        public string ID
        {
            get
            {
                return _id;
            }
        }

        public string PlayerID
        {
            get
            {
                return _playerId;
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

        
    }
}
