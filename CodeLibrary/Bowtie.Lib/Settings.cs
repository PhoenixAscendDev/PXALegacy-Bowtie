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

        internal static int _authCheckInterval = 5;

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

        public static Enum.APIMode Mode
        {
            get 
            {
                   Enum.APIMode result = Enum.APIMode.Debug;
                    #if DEBUG
                        result =  Enum.APIMode.Debug;    
                    #endif
                    #if NODB
                        result = Enum.APIMode.UnitTest;
                    #endif

                    
                return result;
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

        public static DateTime LastAPIAuthCheck
        {
            get
            {
                return _lastAPIAuthCheck;
            }
            set
            {
                _lastAPIAuthCheck = value;
            }
        }

        public static int AuthCheckInterval
        {
            get
            {
                if (_authCheckInterval > 60)
                    return 60;
                if (_authCheckInterval < 5)
                    return 5;
                else return _authCheckInterval;
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
            _authCheckInterval = config.AuthCheckInterval;
        }  
    }
}
