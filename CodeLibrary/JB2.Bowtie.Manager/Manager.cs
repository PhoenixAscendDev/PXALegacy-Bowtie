using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Common;

namespace JB2.Bowtie
{
    public class Manager
    {

        public static bool Initialize(string publicKey, string secretKey)
        {
            Application app = new Application(publicKey, secretKey);

            BaseSetting s = new BaseSetting()
            {
                ID = Economy.JbeanSettingName.CurrencyID,
                Name = "CurrencyID",
                Value = "jbeanID123456789"
            };
            //JB2.Bowtie.Settings._application = app;
            var jbeanStorage = JB2.Infrastructure.Storage.BowtieAccount;
            JB2.Settings.Jbean.Configure( new BaseSetting[1]{ s }, new JB2.Economy.Data.jBeanRespostory(jbeanStorage));

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
                Value = new JB2.Bowtie.Data.Azure.UnitOfWork()            
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
