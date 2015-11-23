using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy
{
    public static class JBeanExtenstions
    {
        public static int JBeanAmount(this IWallet wallet)
        {
            var c = wallet.CurrencyTotal(new JBean());
        }
    }
}
