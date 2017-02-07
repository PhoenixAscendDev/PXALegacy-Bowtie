using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface IWalletRepository : JB2.Common.IRepository<IWallet, string>
    {
        IWallet GetByPlayerAndApplication(string playerID, string appID);
    }
}
