using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IDewdrop : JB2.Common.IIDNamePair<string, string>, JB2.Common.IClass, JB2.Common.IIDProp<string>
    {
        string GetGraphID();
        string GetDescription();

        int GetCurrencyCost(string currencyID);
        //int GetjBeanCost();

        string GetApplicationID();
    }
}
