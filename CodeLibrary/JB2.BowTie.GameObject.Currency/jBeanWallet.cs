using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Economy
{
    public class JBeanWallet : IWallet<IPlayer,long,JBeanToken,string,Enum.JBeanTokenType>
    {
        private Dictionary<JBeanToken,int> _tokens;
        private IPlayer _player;



        public JBeanWallet()
        {
            _tokens = new Dictionary<JBeanToken, int>();

        }

        public IPlayer Owner
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

        public JBeanToken[] Denomination
        {
            get
            {
                return _tokens.Keys.ToArray();
            }        
        }

        public long Amount
        {
            get 
            {
                long result = 0;

                foreach(var item in _tokens)
                {
                    result = result + (long)(item.Value * item.Key.UnitMultiplier);
                }
                return result;
            }
        }

        public bool AddDenomination(JBeanToken denomination, int quantity)
        {
            throw new NotImplementedException();
        }

        public bool RemoveDenomination(JBeanToken denomination, int quantity)
        {
            throw new NotImplementedException();
        }

        public string ID
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
    }
}
