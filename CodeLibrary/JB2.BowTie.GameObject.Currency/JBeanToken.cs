using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class JBeanToken : IDenomination<Enum.JBeanTokenType>
    {
        public string ImageFrontUrl
        {
            get;set;
        }

        public string ImageBackUrl
        {
            get;set;
        }

        public string CurrencyID
        {
            get;set;
        }

        public byte UnitMultiplier
        {
            get;set;
        }

        public bool isSubUnit
        {
            get;set;
        }

        public string SerialNumber
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

        public Enum.JBeanTokenType DenominationType
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
