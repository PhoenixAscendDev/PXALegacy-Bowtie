using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Economy.Settings
{
    public static class JBean
    {
        private static readonly string  CURRENCYID = "111000";

        public static byte GetMultiplier(Enum.JBeanTokenType type)
        {
            switch(type)
            {
                case Enum.JBeanTokenType.Kidney:
                    return 1;
                case Enum.JBeanTokenType.Navy:
                    return 50;
                case Enum.JBeanTokenType.Pinto:
                    return 200;
                default:
                    return 1;
            }
        }

        public static string GetFrontImage(Enum.JBeanTokenType type)
        {
            return "#";
        }

        public static string GetBackImage(Enum.JBeanTokenType type)
        {
            return "#";
        }

        public static string CurrencyID
        {
            get
            {
                return CURRENCYID;
            }
        }
    }
}
