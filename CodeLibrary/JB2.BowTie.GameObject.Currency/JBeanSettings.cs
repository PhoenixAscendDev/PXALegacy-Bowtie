using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

using JB2.Economy.Enum;

namespace JB2.Settings
{
    public static class Jbean
    {
        #region Fields

        private static JB2.Economy.JBeanFactory _factory;
        private static bool _isConfigured = false;
        //private static SettingCollection<string> _settings;
        #endregion Fields


        public static void Configure(IEnumerable<ISetting> settings,JB2.Economy.IJBeanRepository repo)
        {          
            _factory = JB2.Economy.JBeanFactory.Configure(settings,repo);
            _isConfigured = true;
        }

        public static ISetting GetSetting(string settingName)
        {
            checkIfConfigured();
            return _factory.GetSetting(settingName);
        }

        public static string GetTokenImageFront(JBeanTokenType type)
        {
            checkIfConfigured();
            return _factory.GetTokenImageFront(type);
        }

        public static string GetTokenImageBack(JBeanTokenType type)
        {
            checkIfConfigured();
            return _factory.GetTokenImageBack(type);
        }

        public static int GetTokenValue(JBeanTokenType type)
        {
            checkIfConfigured();
            return _factory.GetTokenValue(type);
        }

        public static JB2.Economy.JBeanFactory Factory
        {
            get
            {
                checkIfConfigured();
                return _factory;
            }
            
        }

        private static void checkIfConfigured()
        {
            if (!_isConfigured)
                throw new JB2.Common.Exceptions.NotConfiguredException();
        }




    }
}
