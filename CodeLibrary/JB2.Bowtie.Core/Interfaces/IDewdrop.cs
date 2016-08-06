using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IDewdrop : JB2.Identity.IApplicationable, JB2.Common.IIDNamePair<string, string>
    {
        string GetGraphID();
        string GetDescription();

        int GetjBeanCost();
    }
}
