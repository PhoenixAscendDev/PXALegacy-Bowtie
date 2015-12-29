using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Economy.Enum;

namespace JB2.Economy
{
    //private static JB2.Economy.EconomicFactory;
    public class JBeanFactory : Economy.EconomicFactory<IIDProp<string>,jBeanAccountStatus>
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
            switch(type)
            {
                case Enum.JBeanTokenType.Kidney:
                    return 1;
                case Enum.JBeanTokenType.Navy:
                    return 100;
                case Enum.JBeanTokenType.Pinto:
                    return 1000;
                default:
                    return 0;
            }
            
        }

        public static JBeanFactory Configure(IEnumerable<ISetting> settings, IJBeanRepository repo)
        {
            JBeanFactory factory = new JBeanFactory();

            JB2.Common.SettingCollection<string> sc = new SettingCollection<string>(settings);

            factory.Settings = sc;
            factory.Denominations = new IDenomination[3] {  new JBeanDenomination(Enum.JBeanTokenType.Kidney),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Navy),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Pinto)
                                                          };
            ICurrency jBeanCurrency = new JBean()
            {
                ID = sc[JbeanSettingName.CurrencyID].ToString(),
                Denominations = factory.Denominations,
                Name = "jBean",
                PluralName = "jBeans"
            };

            
            factory.Currencies = new ICurrency[1] { jBeanCurrency };

            factory.Denominations = new IDenomination[3] {  new JBeanDenomination(Enum.JBeanTokenType.Kidney),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Navy),
                                                            new JBeanDenomination(Enum.JBeanTokenType.Pinto)
                                                          };
            factory.Treasury = new JbeanTreasury(repo);

            factory.CentralBank = new jBeanCentralBank(factory.Treasury, repo);

            return factory;
        }
    }
}
