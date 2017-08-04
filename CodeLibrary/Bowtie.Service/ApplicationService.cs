using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using JB2.Common.Log;
using JB2.Common;
using JB2.Bowtie;
using JB2.Bowtie.Enum;

namespace JB2.Bowtie.Service
{
    public class ApplicationService : GenericService<IApplication,IApplicationRepository>
    {

        #region Fields

        protected IAuthorizeRepository _authRepo;

        #endregion Fields

        #region Constructors
        public ApplicationService() : this(JB2.Settings.Bowtie.UnitOfWork)
        {
           
        }

        public ApplicationService(IUnitOfWork unitOfWork) : this(unitOfWork.ApplicationRepository)
        {
            _uofw = unitOfWork;
            _authRepo = _uofw.AuthorizeRepository;
        }

        public ApplicationService(IApplicationRepository repo): base(repo)
        {
            _uofw = JB2.Settings.Bowtie.UnitOfWork;
        }

        #endregion Constructors


        public IApplication RetrieveByAPIKey(JB2.Common.IAPIKeySecretPair apiKey)
        {
            var authState = _authRepo.GetApplicationStateByAPIKey(apiKey.APIkey, apiKey.Secret);

            return _repo.GetById(authState.ApplicationID);
        }

        public ApplicationStatePair RetrieveAuthorizeState(string publickey, string secret)
        {
            var authState = _authRepo.GetApplicationStateByAPIKey(publickey,secret);

            return authState;
        }

        public JB2.Bowtie.Enum.APIAuthorizeState CheckAPIAuthorization(string applicationID)
        {
            var authState = _authRepo.GetApplicationStateByID(applicationID);
            return authState.AuthorizeState;
        }

        public IApplication GenerateNewApplication()
        {
            
            try
            {

                var currencySystems = _uofw.SystemRepository.GetCurrencySystems();
                var pointSystems = _uofw.SystemRepository.GetPointSystems();
                var apikey = new JB2.Common.ApiKeySecretPair();
                apikey.APIkey = "BT-" + JB2.Common.NewID.UriHash(new Uri("http://bowtie.io?ticks=" + JB2.Common.NewID.TickHash())).ToUpper();
                apikey.Secret = JB2.Common.NewID.Guid();

                //set modules
                var modules = JB2.Settings.Bowtie.UnitOfWork.ModuleRepository.GetAll();
                var app = new Application(apikey.APIkey, apikey.Secret, APIAuthorizeState.Authorized, modules);

                //setup TreasuryKeys
                
                foreach (var s in currencySystems)
                {
                    var system = s.Construct();
                    var requestKey = system.Treasury.RegisterApplication(app);
                    if( !string.IsNullOrEmpty(requestKey.Key))
                        app.TreasuryKeys.ToList().Add(requestKey);
                }
                ////jBean
                //var jbeanRequestor = JB2.Settings.Jbean.Factory.Treasury.RegisterNewRequestor(app.ID);
                //var jBean = new TreasuryRequestKey("jBean", jbeanRequestor.RequestValidationKey);
                //app.TreasuryKeys = new TreasuryRequestKey[1] { jBean };

                //setup CurrencySystems
                var c = app.AllowedCurrencySystems.ToList();
                
                foreach (var s in currencySystems)
                    c.Add( (IIDNamePair<string,string>)s);
                app.AllowedCurrencySystems = c;


                //setup Point Systems
                var p = app.AllowedCurrencySystems.ToList();
                foreach (var s in pointSystems)
                    p.Add((IIDNamePair<string, string>)s);
                app.AllowedPointSystems = p;


                //save app to repo
                _repo.Insert(app);

                //setup the authorize state
                ApplicationStatePair state = new ApplicationStatePair();
                state.ApplicationID = app.ID;
                state.APIKey = apikey;
                state.AuthorizeState = APIAuthorizeState.Authorized;
                _authRepo.Insert(state);
                
                _authRepo.UpdateAuthorizeState(APIAuthorizeState.Authorized, app.ID);

                var logentry = JB2.Common.Log.LogEntry.NewLogEntry(Common.Enum.LogServerityType.Informational, "Application Created: \r\n " + "ID: " + app.ID, "BT-1-001");
                JB2.Settings.Bowtie.Logger.Log(logentry);

                JB2.Events.Bowtie.OnApplicationCreated(app, DateTime.Now);
                return app;
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return null;
            }




            
        }
        
        public JB2.Common.ServiceResult isAuthorized(string applicationID)
        {
            var state = _authRepo.GetApplicationStateByID(applicationID);
            return isAuthorized(state);     
        }

        public override bool Save(IApplication entity)
        {

            return base.Save(entity);
        }

        public JB2.Common.ServiceResult isAuthorized(JB2.Common.IAPIKeySecretPair api)
        {
            var app = _authRepo.GetApplicationStateByAPIKey(api.APIkey, api.Secret);
            return isAuthorized(app);
        }

        private bool isAuthorized(IApplication app)
        {
            var state = _authRepo.GetApplicationStateByAPIKey(app.APIkey, app.Secret);

            return isAuthorized(state);
        }

        public JB2.Common.ServiceResult isAuthorized(ApplicationStatePair state)
        {
            //double check the secret is correct
            var state2 = _authRepo.GetApplicationStateByAPIKey(state.APIKey.APIkey, state.APIKey.Secret);

            if (state2.APIKey.Secret != state.APIKey.Secret)
                return new Common.ServiceResult(new Exception("Not Authorized: API Key is invalid"));

            if (state2.ApplicationID != state.ApplicationID)
                return new Common.ServiceResult(new Exception("Not Authorized: Application can not be found"));

            switch (state2.AuthorizeState)
            {
                case APIAuthorizeState.Authorized:
                    return true;
                case APIAuthorizeState.LifelongBan:
                case APIAuthorizeState.TemporaryBlocked:
                case APIAuthorizeState.Unknown:
                default:
                    return new JB2.Common.ServiceResult(new Exception("Not Authorized: Current Authorize State is " + state.AuthorizeState.ToString()));
            }
        }

        public JB2.Common.ServiceResult VerifyAuthorizeKey(string authorizeKey, string applicationId)
        {
            JB2.Common.ServiceResult result = true;
            //Get the ticks
            try
            {
                var ticks = _authRepo.GetAuthorizeKeyTicks(authorizeKey);

                if (ticks == 0)
                    throw new Exception("Authorize Key not found");
                var dateGenerated = new DateTime(ticks);

                var state = _authRepo.GetApplicationStateByID(applicationId);

                var isAuth = isAuthorized(state);
                if (!isAuth)
                    return isAuth;

                var keyverify = createAuthorizeKey(state, dateGenerated);

                if (keyverify != authorizeKey)
                    throw new Exception("Authorize key does not match");
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return new Common.ServiceResult(ex);
            }

            return result;

            


        }

        protected string createAuthorizeKey(ApplicationStatePair state, DateTime timestamp)
        {
            string keyurlformat = JB2.Configuration.GetAppSetting("JB2:UrlHash:BowtieAuthorizeKey");
            
            string url = string.Format(keyurlformat, state.APIKey.APIkey, state.APIKey.Secret, timestamp.Ticks.ToString());

            string key = JB2.Common.NewID.UriHash(new Uri(url));

            return key;

        }

        public string GenerateAuthorizeKey(ApplicationStatePair state)
        {
            try
            {
                var isauth = this.isAuthorized(state);

                if (isauth)
                {
                    var dateGenerated = DateTime.Now;
                    string key = createAuthorizeKey(state, dateGenerated);

                    _authRepo.InsertAuthorizeKey(key, state.ApplicationID, dateGenerated);

                    return key;
                }
                return string.Empty;
            }
            catch(Exception ex)
            {
                ex.BowtieLog();
                return string.Empty;
            }
        }

        public JB2.Economy.jBeanAppSettings RetrievejBeanSettings(string applicationID)
        {
            var jbeanRepo = _uofw.JbeanRepository;

            var settings = jbeanRepo.GetApplicationSettingsByID(applicationID);

            return settings;

        }
    }
}