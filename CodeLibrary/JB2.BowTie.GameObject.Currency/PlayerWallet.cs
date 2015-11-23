using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
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
            var ca = _amounts[currency.ID];
            ca.IncreaseAmount(quantity);
            
        }

        public long CurrencyTotal(ICurrency currency)
        {
            throw new NotImplementedException();
        }

        public void RemoveAmount(ICurrency currency, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
