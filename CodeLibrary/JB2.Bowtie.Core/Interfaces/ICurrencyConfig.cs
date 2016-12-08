using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface ICurrencyConfig : JB2.Common.IIDNamePair<string,string>
    {
        string CurrencyID { get; set; }

        ICurrencyTreasury Treasury { get; set; }
    }
}
