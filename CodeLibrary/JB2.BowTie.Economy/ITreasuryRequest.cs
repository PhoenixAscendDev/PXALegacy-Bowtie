using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public interface ITreasuryRequest
    {
        string Requestor { get; set; }
        DateTime RequestDate { get; set; }
        long Amount { get; set; }
        string VerificationKey { get; set; }
    }
}
