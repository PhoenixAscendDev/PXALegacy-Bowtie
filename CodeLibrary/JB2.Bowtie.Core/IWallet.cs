using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public interface IWallet : JB2.Economy.IApplicationWallet<JB2.Common.IPerson<string>, string>
    {
        JB2.Economy.JBeanBag JBeanTotal { get; }
    }
}
