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
            JB2.Settings.Jbean.Configure( new BaseSetting[1]{ s });

            //genera bowtie settings
            List<ISetting> bowtieSettings = new List<ISetting>();
            bowtieSettings.Add(new BaseSetting()
            {
                ID = "CurrentApplication".ToUpper(),
                Name = "CurrentApplication".ToUpper(),
                Value = app
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
