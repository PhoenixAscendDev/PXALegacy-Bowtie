using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public static class Settings
    {
        internal static Application _application = null;

        internal static DateTime _lastAPIAuthCheck;

        internal static string _sigFormat = "{0}>*<{1}{2}{3}{4}";

        internal const string _headerDelimiter = ":";

        internal static string _configFile;

        public static string SettingsFilename
        {
            get
            {
                return _configFile;
            }
            set
            {
                _configFile = value;
            }
        }

        public static Application CurrentApplication
        {
            get
            {
                if(_application == null)
                {
                    BowtieConfig settings = getSettingsFromFile();
                    _application = new Application(settings.AppKey, settings.SecretKey);
                }

                return _application;
            }
        }

        public static BowtieAPI APIInfo
        {
            get
            {
                return getSettingsFromFile().API;
            }
        }

        public static string SignatureFormat
        {
            get
            {
                //StringBuilder result = new StringBuilder(JB2.Bowtie.Settings.CurrentApplication.ID);
               //result.Append(">*<");
                StringBuilder result = new StringBuilder(_sigFormat);
                //result.Append(_sigFormat);
                return result.ToString();
            }
        }

        public static string HeaderDelimiter
        {
            get
            {
                return _headerDelimiter;
            }
        }

        private static BowtieConfig getSettingsFromFile()
        {
            BowtieConfig settings = BowtieConfig.Load(_configFile);
            LoadByConfig(settings);
            return settings;
        }

        public static void LoadByConfig(BowtieConfig config)
        {
            _application = new Application(config.AppKey, config.SecretKey);
            _sigFormat = config.Signature;

        }



        

        
    }
}
