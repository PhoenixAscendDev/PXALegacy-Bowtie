using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface ICurrencyTreasury
    {

        decimal RequestAmount(IApplication application, string currencyID, decimal anmout);


    }
}
