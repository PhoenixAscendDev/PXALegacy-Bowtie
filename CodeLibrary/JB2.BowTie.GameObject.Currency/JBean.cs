using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Economy
{
    public class JBean : ICurrency<JBeanToken,Enum.JBeanTokenType>
    {
        Dictionary<Enum.JBeanTokenType, JBeanToken> _tokens;

        #region Public Properties

        public byte BaseUnit
        {
            get
            {
                return 1;             
            }
            set {}
        }

        public string OwnerClientID
        {
            get;set;
            
        }

        public Enum.CurrencyType CurrencyType
        {
            get { return Enum.CurrencyType.NonDecimal; }
            set { }
        }

        public JBeanToken[] Denominations
        {
            get
            {
               return _tokens.Values.ToArray();
            }
            set
            {
                Dictionary<JBeanToken,JBean> result = new Dictionary<JBeanToken,JBean>();
                foreach(JBeanToken t in value)
                {
                    _tokens.Add(t.DenominationType,t);
                }         
            }
        }

        public float[] SubUnits
        {
            get;set;
        }

        public string PluralName
        {
            get;set;
        }

        public string SymbolUrl
        {
            get;set;
        }

        public string ID
        {
            get;set;
        }

        public string Name
        {
            get;set;
        }

        #endregion Public Properties

        #region Public Methods

        public JBeanToken[] GetDenomination(Enum.JBeanTokenType dType)
        {

            return new JBeanToken[] {_tokens[dType]};
        }

        #endregion Public Methods
    }
}
