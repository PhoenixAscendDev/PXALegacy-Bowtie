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

        public ApplicationService(IApplicationRepository repo): base( repo)
        {

        }

        public JB2.Bowtie.Enum.APIAuthorizeState CheckAPIAuthorization(string applicationID)
        {
            IApplication app = _repo.GetById(applicationID);

            if (app.ID == JB2.Settings.Bowtie.CurrentApplication.ID)
                JB2.Settings.Bowtie.LastAPIAuthCheck = DateTime.Now;

            return app == null ? JB2.Bowtie.Enum.APIAuthorizeState.Unknown : app.AuthorizedState;
        }

        public bool isApplicationAuthorized(string applicationID)
        {
            APIAuthorizeState state = CheckAPIAuthorization(applicationID);

            return state == APIAuthorizeState.Authorized; 
           
        }



    }
}