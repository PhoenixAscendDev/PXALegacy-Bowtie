using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;

namespace JB2.Bowtie
{
    public interface IDenomination : IDenomination<string, byte, string, string>
    {

    }

    public interface IDenomination<TCurrencyKey,TMultiplier,TKey,TSerial> : JB2.Common.IIDNamePair<TKey,string>
    {
        TCurrencyKey CurrencyID { get; set; }
        TMultiplier UnitMultiplier { get; set; }
        bool isSubUnit {get;set;}
        TSerial SerialNumber { get; set; }
        string ImageFrontUrl { get; set; }
        string ImageBackUrl { get; set; }
    }
}
