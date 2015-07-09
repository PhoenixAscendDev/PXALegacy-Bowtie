using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JB2.Bowtie;
using JB2.Bowtie.Enum;

namespace Bowtie.WebAPI.Service
{
    public class ApplicationService : GenericService<IApplication>
    {

        public ApplicationService()
        {

        }

        public ApplicationService(IApplicationRepository repo): base( repo)
        {
        }

        public JB2.Bowtie.Enum.APIAuthorizeState CheckAPIAuthorization(string applicationID)
        {
            IApplication app = _repo.GetById(applicationID);

            if (app.ID == JB2.Bowtie.Settings.CurrentApplication.ID)
                JB2.Bowtie.Settings.LastAPIAuthCheck = DateTime.Now;

            return app == null ? JB2.Bowtie.Enum.APIAuthorizeState.Unknown : app.AuthorizedState;

        }

        public bool isApplicationAuthorized(string applicationID)
        {
            APIAuthorizeState state = CheckAPIAuthorization(applicationID);

            return state == APIAuthorizeState.Authorized; 
           
        }



    }
}