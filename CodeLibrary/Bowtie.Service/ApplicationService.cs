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

        public IApplication RetrieveByAPIKey(JB2.Common.IAPIKeySecretPair apiKey)
        {
            return _repo.GetApplicationByAPIKey(apiKey.APIkey);
        }

        public JB2.Bowtie.Enum.APIAuthorizeState CheckAPIAuthorization(string applicationID)
        {
            IApplication app = _repo.GetById(applicationID);

            if (app.ID == JB2.Settings.Bowtie.CurrentApplication.ID)
                JB2.Settings.Bowtie.LastAPIAuthCheck = DateTime.Now;

            return app == null ? JB2.Bowtie.Enum.APIAuthorizeState.Unknown : app.AuthorizedState;
        }

        public bool isAuthorized(string applicationID)
        {
            return isAuthorized(_repo.GetById(applicationID));     
        }

        public bool isAuthorized(JB2.Common.IAPIKeySecretPair api)
        {
            var app = _repo.GetApplicationByAPIKey(api.APIkey);
            return isAuthorized(app);
        }

        public JB2.Economy.jBeanAppSettings RetrievejBeanSettings(string applicationID)
        {
            var jbeanRepo = _uofw.JbeanRepository;

            var settings = jbeanRepo.GetApplicationSettingsByID(applicationID);

            return settings;

        }

        private bool isAuthorized(IApplication app)
        {
            if (app == null)
                return false;

            return app.isAuthorized;
        } 

        



    }
}