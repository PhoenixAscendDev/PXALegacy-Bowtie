using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.Economy
{
    public interface ITreasury : IIDNamePair<string,string>
    {
        ITreasuryNote IssueDeomination(ITreasuryRequest request);

        long GetAmountIssued();

        void Cancel(ITreasuryNote treasuryNote);

        
    }
}
