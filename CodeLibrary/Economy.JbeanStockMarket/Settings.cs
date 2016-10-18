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

            IEnumerable<ISetting> defaultsettings = new List<ISetting>();
            //get default settings
            try
            {
                foreach (var s in _settings)
                {
                    if (s.ID == JB2.Economy.JbeanStockMarketSettingName.StockExchangeID)
                        defaultsettings = repo.GetDefaultSettings(s.Value.ToString());
                }

                foreach (var ds in defaultsettings)
                {
                    if (settings.ToList().Find(x => x.ID == ds.ID) == null)
                        _settings.Add(ds);
                }

            }
            catch (Exception ex)
            {
                _settings = new SettingCollection<string>(settings);
            }


            var logger = new JB2.Infrastructure.ProjectLogger((JB2.Common.Log.ILogRepo)_settings[JB2.Economy.JbeanStockMarketSettingName.LogRepo].Value);
            

            _isConfigured = true;
            _exchange = _repo.GetExchange(); // new JbeanStockExchange((string)GetSetting(JbeanStockMarketSettingName.StockExchangeID).Value);
            _exchange.Logger = logger;



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
                var setting = GetSetting(JbeanStockMarketSettingName.TreasuryRequestorID);
                IRequestor result =(IRequestor)new JB2.Common.IDNamePair<string, string>(setting.Value.ToString(), string.Empty);
                return result;
            }
        }

        private static void checkIfConfigured()
        {
            if (!_isConfigured)
                throw new JB2.Common.NotConfiguredException();
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
