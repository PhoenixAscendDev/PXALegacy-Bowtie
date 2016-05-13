using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public class TreasuryRequest : TreasuryRequest<IRequestor,string>, ITreasuryRequest
    {

    }
    public class TreasuryRequest<TRequestor,TID> : ITreasuryRequest<TID,TRequestor>
         where TID : IComparable
        where TRequestor : IRequestor<TID>
    {
        public TRequestor Requestor { get; set; }
        public DateTime RequestDate { get; set; }
        public long Amount { get; set; }
        public string VerificationKey { get; set; }
    }
}
