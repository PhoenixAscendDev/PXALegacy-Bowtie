using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie.Enum;

namespace JB2.Economy
{

    public interface IDenomination : JB2.Common.IIDNamePair<string,string>
    {
         
        int UnitMultiplier { get; set; }
        bool isSubUnit { get; set; }      
        string ImageFrontUri { get; set; }
        string ImageBackUri { get; set; }

    }
    //public interface IDenomination<TType> : IDenomination<TType,string, byte, string, string>
    //{

    //}

    //public interface IDenomination<TType,TCurrencyKey,TMultiplier,TKey,TSerial> : JB2.Common.IIDNamePair<TKey,string>
    //    where TKey : IComparable
    //{
    //    TType DenominationType { get; set; }
    //    TCurrencyKey CurrencyID { get; set; }
    //    TMultiplier UnitMultiplier { get; set; }
    //    bool isSubUnit {get;set;}
    //    TSerial SerialNumber { get; set; }
    //    string ImageFrontUrl { get; set; }
    //    string ImageBackUrl { get; set; }
        
    //}
}
