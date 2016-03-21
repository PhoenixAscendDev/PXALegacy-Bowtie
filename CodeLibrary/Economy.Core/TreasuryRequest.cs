using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class TreasuryRequest : ITreasuryRequest
    {
        public object Requestor { get; set; }
        public DateTime RequestDate { get; set; }
        public long Amount { get; set; }
        public string VerificationKey { get; set; }
    }
}
