using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Economy
{
    public interface IApplicationWallet<TCurrency,TOwner,TID> : IWallet<TCurrency, TOwner, TID>
        where TID : IComparable
        where TOwner : JB2.Common.IPerson<TID>
        where TCurrency : ICurrency
    {
        TID ApplicationID { get; set; }
        TID GetApplicationID();
    }
}
