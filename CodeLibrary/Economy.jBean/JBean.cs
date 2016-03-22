using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class JBean : TokenCurrency
    {

        //Dictionary<Enum.JBeanTokenType, JBeanToken> _tokens;

        #region Constructors

        public JBean(string currencyID)
        {
            
            var demoinations = new JB2.Economy.IDenomination[3] {  new JB2.Economy.JBeanDenomination(JB2.Economy.Enum.JBeanTokenType.Kidney),
                                                            new JB2.Economy.JBeanDenomination(JB2.Economy.Enum.JBeanTokenType.Navy),
                                                            new JB2.Economy.JBeanDenomination(JB2.Economy.Enum.JBeanTokenType.Pinto)
                                                          };
            ID = currencyID;
            Denominations = demoinations;
            Name = "jBean";
            PluralName = "jBeans";
        }

        public JBean()
        {

        }


        #endregion Constructors




        #region Properties

        #endregion Properties


        #region Methods

        // public JBeanToken[] GetDenomination(Enum.JBeanTokenType dType)
        //{

        //    return new JBeanToken[] {_tokens[dType]};
        //}

        #endregion Methods
    }
}
