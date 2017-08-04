using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface ICurrencySystem : JB2.Common.IIDNamePair<string,string>, ISystem
    {
        string CurrencyID { get; set; }

        ICurrencyTreasury Treasury { get; set; }
    }
}
