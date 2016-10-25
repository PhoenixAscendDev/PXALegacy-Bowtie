using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public JB2.Common.ServiceResult isAuthorized(string applicationID)
        {
            var state = _authRepo.GetApplicationStateByID(applicationID);
            return isAuthorized(state);     
        }




        public JB2.Common.ServiceResult isAuthorized(JB2.Common.IAPIKeySecretPair api)
        {
            var app = _authRepo.GetApplicationStateByAPIKey(api.APIkey, api.Secret);
            return isAuthorized(app);
        }

        private bool isAuthorized(IApplication app)
        {
            var state = _repo.GetApplicationStateByAPIKey(app.APIkey, app.Secret);

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