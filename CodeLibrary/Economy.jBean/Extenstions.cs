using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Economy;

namespace JB2.Economy
{
    public static class JBeanExtenstions
    {
        public static int JBeanAmount(this IWallet<JB2.Common.IPerson<string>,string> wallet)
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

        public static JBeanBag ToJBean(this float value)
        {
            int number;
            int.TryParse(value.ToString(), out number);
            return number.ToJBean();
        }
        public static JBeanBag ToJBean(this double value)
        {
            int number;
            int.TryParse(value.ToString(), out number);
            return number.ToJBean();
        }

        public static int ToInt(this JBeanBag b)
        {
            return (int)b;
        }
        public static jBeanAccount jBeanAccount(this JB2.Identity.IPlayer player)
        {
            return new jBeanAccount(player.GetjBeanAccountNumber());
        }
    }
}
