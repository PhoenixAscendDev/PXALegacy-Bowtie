using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie.Exceptions;

namespace JB2.Bowtie.Web
{
    public class Manager
    {
        private static JB2.Common.Scheduler.IScheduler<string,object> _schedule;
        public static bool Initialize(string publicKey, string secretKey)
        {
            var pair = new JB2.Common.ApiKeySecretPair() { APIkey = publicKey, Secret = secretKey };
            return Initialize(pair);
        }
        public static bool Initialize(JB2.Common.IAPIKeySecretPair apiKey)
        {

            //track config
            JB2.Settings.Bowtie.ConfiguredSuccess += OnConfigSuccess;


            // First we have to validate the application
            IUnitOfWork uofw = new JB2.Bowtie.Data.Azure.UnitOfWork();
            var appService = new JB2.Bowtie.Service.ApplicationService(uofw);

            ApplicationStatePair appState = appService.RetrieveAuthorizeState(apiKey.APIkey, apiKey.Secret);

            ServiceResult isAuth = appService.isAuthorized(appState);
            if (!isAuth)
                throw new ApplicationNotInitialized(isAuth.ToString());

            var app = appService.RetrieveByAPIKey(apiKey);
            var authorizeKey = appService.GenerateAuthorizeKey(appState);
            //configure jBean
            BaseSetting s = new BaseSetting()
            {
                ID = Economy.JbeanSettingName.CurrencyID,
                Name = Economy.JbeanSettingName.CurrencyID,
                Value = JB2.Configuration.GetjBeanCurrencyID()
            };
            var jbeanStorage = JB2.Infrastructure.Storage.EconomyAccount;
            JB2.Settings.Jbean.Configure(new BaseSetting[1] { s }, new JB2.Economy.Data.jBeanRespostory(jbeanStorage));

            //configure jBean StockMarket
            BaseSetting s1 = new BaseSetting()
            {
                ID = Economy.JbeanStockMarketSettingName.StockExchangeID,
                Name = Economy.JbeanStockMarketSettingName.StockExchangeID,
                Value = JB2.Configuration.GetjBeanStockMarketID()
            };
            BaseSetting s2 = new BaseSetting()
            {
                ID = Economy.JbeanStockMarketSettingName.Currency,
                Name = Economy.JbeanStockMarketSettingName.Currency,
                Value = JB2.Settings.Jbean.Factory.Currencies[0]
            };
            var stockMarketAccount = JB2.Infrastructure.Storage.EconomyAccount;
            JB2.Settings.JbeanStockMarket.Configure(new BaseSetting[2] { s1,s2}, new JB2.Economy.Data.JBeanStockRepository(stockMarketAccount));



            //general bowtie settings
            List<ISetting> bowtieSettings = new List<ISetting>();
            bowtieSettings.Add(new BaseSetting()
            {
                ID = "CURRENTAPPLICATION",
                Name = "CurrentApplication",
                Value = app
            });

            bowtieSettings.Add(new BaseSetting()
            {
                ID = "REPOSITORYTYPE",
                Name = "Repository Type",
                Value = "azurestorage"
            });

            bowtieSettings.Add(new BaseSetting()
            {
                ID = "UNITOFWORK",
                Name = "Unit Of Work",
                Value = uofw
            });
            bowtieSettings.Add(new BaseSetting()
            {
                ID = BowtieSettingName.AuthorizeKey,
                Name = BowtieSettingName.AuthorizeKey,
                Value = authorizeKey
            });
            bowtieSettings.Add(new BaseSetting()
            {
                ID = BowtieSettingName.Logger,
                Name = BowtieSettingName.Logger,
                Value = new JB2.Infrastructure.ProjectLogger(uofw.LogRepository)
            });


            // assigning delegates
            JB2.Settings.Bowtie.CheckAuthorizeMethod = CheckApplicationAuthorization;
            JB2.Settings.Bowtie.RNGMethod = NewRNG;

            JB2.Settings.Bowtie.Configure(bowtieSettings);

            return true;
        }

        public static bool Initialize(BowtieConfig config)
        {
            //JB2.Bowtie.Settings.LoadByConfig(config);
            return true;
        }

        public static bool Initialize(string bowtieConfigFile)
        {
            //JB2.Bowtie.Settings.SettingsFilename = bowtieConfigFile;

            return true;
        }


        public static int NewRNG()
        {
            return JB2.Global.RNG;
        }

        public static ServiceResult CheckApplicationAuthorization(IApplication application, string authorizeKey)
        {
            var app = application;

            var appService = new JB2.Bowtie.Service.ApplicationService(JB2.Settings.Bowtie.UnitOfWork);

            var isValid = appService.VerifyAuthorizeKey(JB2.Settings.Bowtie.AuthorizeKey, app.ID);

            return isValid;

        }


        public static JB2.Common.Scheduler.IScheduler<string, object> ManagerSchedule
        {
            get
            {
                if (_schedule == null)
                {
                    _schedule = new JB2.Common.Scheduler.SimpleScheduler(new AuthorizeCheckJob(), JB2.Settings.Bowtie.Logger);

                }
                return _schedule;
            }
        }


        public static void OnConfigSuccess(IEnumerable<ISetting> settings)
        {
            ManagerSchedule.StartJobs();

            JB2.Events.Bowtie.OnApplicationInitilized(JB2.Settings.Bowtie.CurrentApplication, JB2.Settings.Bowtie.AuthorizeKey);
        }
    }
}
