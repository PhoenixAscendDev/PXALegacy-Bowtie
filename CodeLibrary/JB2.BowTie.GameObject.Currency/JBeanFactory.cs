using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie.Economy
{
    //private static JB2.Bowtie.Economy.EconomicFactory;
    public class JBeanFactory : Economy.EconomicFactory
    {

        public ISetting GetSetting(string settingName)
        {
            if (Settings == null)
                return null;
            else
                return Settings[settingName];
        }

        public  string GetTokenImageFront(Enum.JBeanTokenType type)
        {
            return string.Empty;
        }

        public  string GetTokenImageBack(Enum.JBeanTokenType type)
        {
            return string.Empty;
        }

        public int GetTokenValue(Enum.JBeanTokenType type)
        {
            return 10;
        }

        public static JBeanFactory Configure(IEnumerable<ISetting> settings)
        {
            JBeanFactory factory = new JBeanFactory();

            JB2.Common.SettingCollection<string> sc = new SettingCollection<string>();
            foreach (ISetting s in settings)
            {
                sc.Add(s);
            }
            factory.Settings = sc;
            factory.Currencies = new ICurrency[1] { new JBean() };

            factory.Denominations = new IDenomination[3] {  new JBeanDenomination(Enum.JBeanTokenType.Kidney),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Navy),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Pinto)
                                                          };
            factory.Treasury = new JbeanTreasury();

            return factory;
        }
    }
}
