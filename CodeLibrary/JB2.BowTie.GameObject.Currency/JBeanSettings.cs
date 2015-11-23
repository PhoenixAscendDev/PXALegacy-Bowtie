using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public static class Jbean
    {
        #region Fields

        private static JB2.Bowtie.Economy.JBeanFactory _factory;
        //private static SettingCollection<string> _settings;
        #endregion Fields


        public static void Configure(IEnumerable<ISetting> settings)
        {
            _factory = JB2.Bowtie.Economy.JBeanFactory.Configure(settings);
        }

        public static ISetting GetSetting(string settingName)
        {
            return _factory.GetSetting(settingName);
            //if (_settings == null)
            //    return null;
            //else
            //    return _settings[settingName];
        }

        public static string GetTokenImageFront(Enum.JBeanTokenType type)
        {
            return _factory.GetTokenImageFront(type);
        }

        public static string GetTokenImageBack(Enum.JBeanTokenType type)
        {
            return _factory.GetTokenImageBack(type);
        }

        public static int GetTokenValue(Enum.JBeanTokenType type)
        {
            return _factory.GetTokenValue(type);
        }

        public static JB2.Bowtie.Economy.JBeanFactory Factory
        {
            get
            {
                return _factory;
            }
            
        }


        

    }
}
