using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{

    public interface ITreasuryRequest : ITreasuryRequest<IRequestor>
    {

    }

    public interface ITreasuryRequest<TRequestor> : ITreasuryRequest<string,TRequestor>
        where TRequestor : IRequestor<string>
    {

    }
    public interface ITreasuryRequest<T, TRequestor>
        where T : IComparable
        where TRequestor : IRequestor<T>

    {
        TRequestor Requestor { get; set; }
        DateTime RequestDate { get; set; }
        long Amount { get; set; }
        string VerificationKey { get; set; }
        string GetTreasuryID();
    }
}
