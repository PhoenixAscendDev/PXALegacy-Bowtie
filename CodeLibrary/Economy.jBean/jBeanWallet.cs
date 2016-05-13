using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;


namespace JB2.Economy
{
    public class JBeanWallet : JB2.Common.IDValue<string>,IApplicationWallet<JB2.Common.IPerson<string>,string>
    {
        private JBeanBag _tokens;
        private JB2.Common.IPerson<string> _player;
        protected string _appID;

        public JBeanWallet()
        {
            _tokens = new JBeanBag();

        }

        public JB2.Common.IPerson<string> Owner
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
            }
        }



        public long Amount
        {
            get 
            {
                long result = (long)_tokens;

               
                return result;
            }
        }
        public string GetApplicationID()
        {
            return _appID;
        }

        public double CurrencyTotal(ICurrency currency)
        {
            return (double)_tokens;
        }

        public void AddAmount(ICurrency currency, double quantity)
        {
            int intQ = (int)quantity;
            _tokens = (int)_tokens + intQ;
        }

        public void RemoveAmount(ICurrency currency, double quantity)
        {
            int intQ = (int)quantity;
            _tokens = (int)_tokens - intQ;
        }

        public override string GetID()
        {
            return base.ID;
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string ApplicationID
        {
            get
            {
                return _appID;
            }

            set
            {
                _appID = value;
            }
        }
    }
}
