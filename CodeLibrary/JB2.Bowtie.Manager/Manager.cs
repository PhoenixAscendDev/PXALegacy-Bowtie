using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;
using JB2.Bowtie.Exceptions;

namespace JB2.Bowtie
{
    public class Manager
    {
        public static bool Initialize(string publicKey, string secretKey)
        {
            var pair = new JB2.Common.ApiKeySecretPair() { APIkey = publicKey, Secret = secretKey };

            return Initialize(pair);
        }
        public static bool Initialize(JB2.Common.IAPIKeySecretPair apiKey)
        {

            IUnitOfWork uofw = new JB2.Bowtie.Data.Azure.UnitOfWork();

            var appService = new JB2.Bowtie.Service.ApplicationService(uofw);


            IApplication app = appService.RetrieveByAPIKey(apiKey);

            if (app == null)
                throw new ApplicationNotInitialized("Application could not be Initialized");
            if (app.Secret != apiKey.Secret)
                throw new ApplicationNotInitialized("Application API Key and Secret are invalid");

            BaseSetting s = new BaseSetting()
            {
                ID = Economy.JbeanSettingName.CurrencyID,
                Name = "CurrencyID",
                Value = JB2.Configuration.GetjBeanCurrencyID()
            };
            //JB2.Bowtie.Settings._application = app;
            var jbeanStorage = JB2.Infrastructure.Storage.BowtieAccount;
            JB2.Settings.Jbean.Configure(new BaseSetting[1] { s }, new JB2.Economy.Data.jBeanRespostory(jbeanStorage));

            //genera bowtie settings
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
    }
}
