using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Bowtie.AuthorizeAPI.Models;
using JB2.Common;
using JB2.Bowtie;

namespace Bowtie.AuthorizeAPI.Controllers
{
    [RoutePrefix("api/Application")]
    public class AppAuthorizeController : JB2.Common.WebAPI.BaseApiController
    {

        public AppAuthorizeController() : base()
        {

        }


        [HttpPost]
        public HttpResponseMessage CheckAuthorize(string publicKey, string secret)
        {
            IUnitOfWork uofw = new JB2.Bowtie.Data.Azure.UnitOfWork();
            var appService = new JB2.Bowtie.Service.ApplicationService(uofw);

            ApplicationStatePair appState = appService.RetrieveAuthorizeState(publicKey, secret);

            ServiceResult isAuth = appService.isAuthorized(appState);

            ApplicationStateView result = new ApplicationStateView();
            var authkeyURL = "http://bowtieAuth.io?key=" + publicKey + "&secret=" + secret;


            result.ApplicationID = appState.ApplicationID;
            result.AuthorizeKey = JB2.Common.NewID.UriHash(new Uri(authkeyURL));
            result.isAuthorized = isAuth;
            result.Message = isAuth.ToString();
            result.State = appState.ToString();

            result.SetSerializableProperties(null);

            return this.createResponse<ApplicationStateView>(result, "all", HttpStatusCode.OK);

        }
    }
}
