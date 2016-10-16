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
        private static JbeanStockExchange _exchange;

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
            _settings = new SettingCollection<string>(settings);
            _isConfigured = true;
            _exchange = new JbeanStockExchange((string)GetSetting(JbeanStockMarketSettingName.StockExchangeID).Value);

            if(!validExchange())
            {
                _isConfigured = false;
                throw new Exception("Stock Exchange is not a valid jBean Stock Exchange");               
            }
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

                return _exchange;
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

        public static string JBeanTreasuryVerificationKey
        {
            get
            {
                var setting = GetSetting(JbeanStockMarketSettingName.TreasuryValidationKey);
                return (string)setting.Value;
            }
        }

        private static bool validExchange()
        {
            try
            {
                var exchangeID = (string)GetSetting(JbeanStockMarketSettingName.StockExchangeID).Value;
                var exchange = _repo.GetExchange();

                return (exchange.GetID() == exchangeID);
            }
            catch
            {
                return false;
            }
        }

    }
}
