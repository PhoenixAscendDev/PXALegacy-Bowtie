using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Economy.Enum;

namespace JB2.Economy
{
    public interface IDenomination : JB2.Common.IIDNamePair<string,string>
    {
         
        int UnitMultiplier { get; set; }
        bool isSubUnit { get; set; }      
        string ImageFrontUri { get; set; }
        string ImageBackUri { get; set; }

    }
    
}
