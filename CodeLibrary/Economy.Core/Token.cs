using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public class CurrencyToken : IDenomination<JB2.Common.IIDNamePair<string,string>,string,short,string,string>
    {
        public Common.IIDNamePair<string, string> DenominationType
        {
            get;set;
            
        }

        public string CurrencyID
        {
            get;
            set;
        }

        public short UnitMultiplier
        {
            get;
            set;
        }

        public bool isSubUnit
        {
            get;
            set;
        }

        public string SerialNumber
        {
            get;
            set;
        }

        public string ImageFrontUrl
        {
            get;
            set;
        }

        public string ImageBackUrl
        {
            get;
            set;
        }

        public string ID
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }
}
