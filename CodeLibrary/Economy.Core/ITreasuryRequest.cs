using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface ITreasuryRequest
    {
        object Requestor { get; set; }
        DateTime RequestDate { get; set; }
        long Amount { get; set; }
        string VerificationKey { get; set; }
    }
}
