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

        public bool IssueDenomination(JBeanToken type, int quantity)
        {
            throw new NotImplementedException();
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
