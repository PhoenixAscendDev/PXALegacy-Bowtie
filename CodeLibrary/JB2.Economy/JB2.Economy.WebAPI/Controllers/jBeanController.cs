using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

using JB2.Common.WebAPI;
using JB2.Identity;

namespace JB2.Economy.WebAPI.Controllers
{
    public class jBeanController : BaseApiController
    {
        [HttpGet]
        [Authorize]
        public HttpResponseMessage BankAccount()
        {
            var player = getPlayerFromClaims();

            jBeanAccount account = new jBeanAccount(player.GetjBeanAccountNumber());
            return Request.CreateResponse(HttpStatusCode.OK, account);
        }

        protected IPlayer getPlayerFromClaims()
        {
            var user = User as ClaimsPrincipal;
            var claims = user.Claims.ToList();


            var playerid = claims.Find(x => x.Type == ClaimType.PlayerID);
            var clientid = claims.Find(x => x.Type == ClaimType.ClientId);

            return JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(playerid.Value, clientid.Value);

        }
    }
}
