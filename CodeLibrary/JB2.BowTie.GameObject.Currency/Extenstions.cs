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
            int result = 0;
            var c = wallet.CurrencyTotal(new JBean());
            Int32.TryParse(c.ToString(), out result);
            return result;            
        }
        public static JBeanBag ToJBean(this int value)
        {
            return (JBeanBag)value;
        }

        public static int ToInt(this JBeanBag b)
        {
            return (int)b;
        }
    }
}
