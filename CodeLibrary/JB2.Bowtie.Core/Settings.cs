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
    
        public delegate ServiceResult CheckAuthorize(IApplication application, string authorizeKey);

        public static event Action<IEnumerable<ISetting>> ConfiguredSuccess;

        public static event Action<IEnumerable<ISetting>, ServiceResult> ConfiguredFailed;

        public static event Action<IApplication> ApplicationNotAuthorized;

        public static event Action<IApplication,string, DateTime> ApplicationAuthCheck;

        public static CheckAuthorize CheckAuthorizeMethod;

        //public delegate void MoveDelegate(object o);
        //public static MoveDelegate MoveMethod;

        internal static IApplication _application = null;

        internal static DateTime _lastAPIAuthCheck;

        internal static string _sigFormat = "{0}>*<{1}{2}{3}{4}";

        internal const string _headerDelimiter = ":";

        internal static IEnumerable<Dewdrop> _dewdrops;

        internal static ILogger _logger;

        internal static int _authCheckInterval = 5;
        private static JB2.Common.SettingCollection<string> _settings;
        private static bool _isConfigured = false;
        private static IUnitOfWork _unitofWork;
        private static string _authorizekey;


        public static ILogger Logger
        {
            get
            {
                return _logger;
            }
        }
        
        public static APIMode Mode
        {
            get
            {
                APIMode result = APIMode.Debug;
#if DEBUG
                result = APIMode.Debug;
#endif
#if NODB
                        result = Enum.APIMode.UnitTest;
#endif


                return result;
            }
        }

        public static IEnumerable<Dewdrop> Dewdrops
        {
            get
            {
                return _dewdrops;
            }
            set
            {
                _dewdrops = value;
            }

        }
        public static IApplication CurrentApplication
        {
            get
            {
                checkIfConfigured();
                return _application;
            }
        }

        public static string AuthorizeKey
        {
            get
            {
                checkIfConfigured();
                return _authorizekey;
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
                var unitofWork = (IUnitOfWork)_settings[BowtieSettingName.UnitofWork].Value;
                if (unitofWork == null)
                    throw new Exception("Unit of work is not set");
                _unitofWork = unitofWork;

                var application = (IApplication)_settings[BowtieSettingName.CurrentApplication].Value;

                var authorizekey = (string)_settings[BowtieSettingName.AuthorizeKey].Value;

                if (application.isAuthorized)
                    _application = application;
                else
                    throw new Exception("Application is not valid");

                var logger = (ILogger)_settings[BowtieSettingName.Logger].Value;

                if (logger == null)
                    throw new Exception("Logger was not configured");
                else
                    _logger = logger;

                if (authorizekey == null)
                    throw new Exception("Authorize key was not configured");
                else
                    _authorizekey = authorizekey;


                //setup events
                _logger.EntryLogged += OnLogged;
            }
            catch(Exception ex)
            {
                _isConfigured = false;
                if (ConfiguredFailed != null)
                    ConfiguredFailed(settings, new ServiceResult(ex));
                ///Do something with the exceptions
            }

            if(_isConfigured)
            {
                if (ConfiguredSuccess != null)
                    ConfiguredSuccess(settings);
            }
        }

        public static ISetting GetSetting(string settingName)
        {
            return _settings[settingName];
        }

        private static void checkIfConfigured()
        {
            if (!_isConfigured)
                throw new JB2.Common.NotConfiguredException();                             
        }

        public static void OnLogged(ILogger<JB2.Common.Enum.LogServerityType, string, ILogEntry> logger, JB2.Common.Enum.LogServerityType type,ILogEntry logEntry)
        {
            JB2.Events.Bowtie.OnLogEntryLogged(logger, logEntry);
        }

        public static ServiceResult isAuthorized()
        {
            ServiceResult result = false;
            if(CheckAuthorizeMethod != null)
            {
                result = CheckAuthorizeMethod(_application, _authorizekey);
                _lastAPIAuthCheck = System.DateTime.Now;
                return result;
            }
            return result;

            
        }

    }
}
