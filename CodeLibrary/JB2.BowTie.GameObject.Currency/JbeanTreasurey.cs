using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;

namespace JB2.Bowtie.Economy
{
    public class JBeanTreasury : ITreasuryService<JBean,JBeanToken,Enum.JBeanTokenType,string,string>
    {

        public JBeanToken[] IssueDenomination(Enum.JBeanTokenType type, int quantity)
        {
            List<JBeanToken> result = new List<JBeanToken>(quantity);

            for(int i = 1; i <=quantity;i++)
            {
                JBeanToken t = new JBeanToken(type);
                result.Add(t);
            }
            return result.ToArray();
        }

        public ulong TotalAmountIssued
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

        public long DenominationIssuedCount(Enum.JBeanTokenType type)
        {
            throw new NotImplementedException();
        }
    }
}
