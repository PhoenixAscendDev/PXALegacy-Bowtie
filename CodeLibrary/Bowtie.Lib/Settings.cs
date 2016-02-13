using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JB2.Bowtie;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Settings
{
    public static class Bowtie
    {
        internal static Application _application = null;

        internal static DateTime _lastAPIAuthCheck;

        internal static string _sigFormat = "{0}>*<{1}{2}{3}{4}";

        internal const string _headerDelimiter = ":";

        internal static int _authCheckInterval = 5;
        private static JB2.Common.SettingCollection<string> _settings;
        private static bool _isConfigured = false;
        private static IUnitOfWork _unitofWork;
        
        public static APIMode Mode
        {
            get 
            {
                APIMode result = APIMode.Debug;
                    #if DEBUG
                        result =  APIMode.Debug;    
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
                checkIfConfigured();
                return _application;
            }
        }

        public static BowtieAPI APIInfo
        {
            get
            {
                return null; //getSettingsFromFile().API;
            }
        }

        public static IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitofWork;
            }
        }

        public static string SignatureFormat
        {
            get
            {
                checkIfConfigured();
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
                checkIfConfigured();
                return _headerDelimiter;
            }
        }

        public static DateTime LastAPIAuthCheck
        {
            get
            {
                checkIfConfigured();
                return _lastAPIAuthCheck;
            }
            set
            {
                checkIfConfigured();
                _lastAPIAuthCheck = value;
            }
        }

        public static int AuthCheckInterval
        {
            get
            {
                checkIfConfigured();
                if (_authCheckInterval > 60)
                    return 60;
                if (_authCheckInterval < 5)
                    return 5;
                else return _authCheckInterval;
            }


        }

        public static void Configure( IEnumerable<ISetting> settings)
        {
           
            _settings = new SettingCollection<string>(settings);
            _isConfigured = true;

            try
            {
                var application = (Application)_settings["CURRENTAPPLICATION"].Value;

                if (isApplicationLegit(application))
                    _application = application;
                else
                    throw new Exception("Application is not valid");

                var unitofWork = (IUnitOfWork)_settings["UNITOFWORK"].Value;
                if (unitofWork == null)
                    throw new Exception("Unit of work is not set");
                _unitofWork = unitofWork;

            }
            catch(Exception ex)
            {
                _isConfigured = false;
                ///Do something with the exceptions
            }
            

        }

        public static ISetting GetSetting(string settingName)
        {
            return _settings[settingName];
        }

        private static void checkIfConfigured()
        {
            if (!_isConfigured)
                throw new JB2.Common.Exceptions.NotConfiguredException();                             
        }

        private static ServiceResult isApplicationLegit(Application app)
        {
            return true;
        }

    }
}
