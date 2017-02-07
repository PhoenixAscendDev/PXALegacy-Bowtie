using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public interface ICurrencyTreasury
    {

        decimal RequestAmount(IApplication application, string currencyID, decimal anmout);

        TreasuryRequestKey RegisterApplication(IApplication application);


    }
}
