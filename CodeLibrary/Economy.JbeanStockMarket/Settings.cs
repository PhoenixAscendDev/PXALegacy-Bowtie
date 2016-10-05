using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Economy.Enum;



using JB2.Economy;

namespace JB2.Settings
{
    public static class JbeanStockMarket
    {
        #region Fields

        private static bool _isConfigured = false;
        private static SettingCollection<string> _settings;
        private static IJbeanStockMarketRepository _repo;

        //private static SettingCollection<string> _settings;
        #endregion Fields


        public static bool IsConfigured
        {
            get
            {
                return _isConfigured;
            }
        }

        public static void Configure(IEnumerable<ISetting> settings, JB2.Economy.IJbeanStockMarketRepository repo)
        {
            _repo = repo;
            _isConfigured = true;
        }

        public static ISetting GetSetting(string settingName)
        {
            checkIfConfigured();
            return _settings[settingName];
        }



        public static JbeanStockExchange StockExchange
        {
            get
            {
                var setting = GetSetting(JbeanStockMarketSettingName.StockExchange);
                return (JbeanStockExchange)setting.Value;
            }
        }

        public static IJbeanStockMarketRepository Repository
        {

            get
            {
                checkIfConfigured();
                return _repo;
            }
        }

        public static ICurrency DefaultCurrency
        {
            get
            {
                var setting = GetSetting(JbeanStockMarketSettingName.Currency);
                return (ICurrency)setting.Value;
            }
        }

        public static IRequestor JBeanTreasuryRequestor
        {
            get
            {
                var setting = GetSetting(JbeanStockMarketSettingName.TreasuryRequestor);
                return (IRequestor)setting.Value;
            }
        }

        private static void checkIfConfigured()
        {
            if (!_isConfigured)
                throw new JB2.Common.Exceptions.NotConfiguredException();
        }


    }
}
