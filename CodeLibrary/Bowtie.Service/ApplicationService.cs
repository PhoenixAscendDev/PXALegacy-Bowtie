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

        #region Constructors
        public ApplicationService()
        {
           
        }

        public ApplicationService(IUnitOfWork unitOfWork) : this(unitOfWork.ApplicationRepository)
        {
            _uofw = unitOfWork;
        }

        public ApplicationService(IApplicationRepository repo): base(repo)
        {
            _uofw = JB2.Settings.Bowtie.UnitOfWork;

        }

        #endregion Constructors


        public IApplication RetrieveByAPIKey(JB2.Common.IAPIKeySecretPair apiKey)
        {
            return _repo.GetApplicationByAPIKey(apiKey.APIkey);
        }

        public ApplicationStatePair RetrieveAuthorizeState(string publickey, string secret)
        {
            return _repo.GetApplicationStateByAPIKey(publickey, secret);
        }

        public JB2.Bowtie.Enum.APIAuthorizeState CheckAPIAuthorization(string applicationID)
        {
            IApplication app = _repo.GetById(applicationID);

            if (app.ID == JB2.Settings.Bowtie.CurrentApplication.ID)
                JB2.Settings.Bowtie.LastAPIAuthCheck = DateTime.Now;

            return app == null ? JB2.Bowtie.Enum.APIAuthorizeState.Unknown : app.AuthorizedState;
        }

        public JB2.Common.ServiceResult isAuthorized(string applicationID)
        {
            var state = _repo.GetApplicationStateByID(applicationID);
            return isAuthorized(state);     
        }

        public JB2.Common.ServiceResult isAuthorized(JB2.Common.IAPIKeySecretPair api)
        {
            var app = _repo.GetApplicationStateByAPIKey(api.APIkey, api.Secret);
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
            var state2 = _repo.GetApplicationStateByAPIKey(state.APIKey.APIkey, state.APIKey.Secret);

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

        public JB2.Economy.jBeanAppSettings RetrievejBeanSettings(string applicationID)
        {
            var jbeanRepo = _uofw.JbeanRepository;

            var settings = jbeanRepo.GetApplicationSettingsByID(applicationID);

            return settings;

        }



        



    }
}