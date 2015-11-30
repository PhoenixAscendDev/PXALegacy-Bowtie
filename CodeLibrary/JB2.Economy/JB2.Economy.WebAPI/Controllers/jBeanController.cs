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
        public IHttpActionResult BankAccount()
        {
            if (isPermitted("bowtie"))
            {
                return Json(getAccountFromClaims);
            }
            else
                return Json(string.Empty);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult Balance()
        {
            var player = getPlayerFromClaims();
            var account = player.jBeanAccount();
            return Json(getCentralBank().CheckBalance(getAccountFromClaims));
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Withdraw() 

        protected jBeanCentralBank getCentralBank()
        {
            return JB2.Settings.Jbean.Factory.CentralBank as jBeanCentralBank;
        }

        protected JbeanTreasury getTreasury()
        {
            return JB2.Settings.Jbean.Factory.Treasury as JbeanTreasury;
        }
        protected jBeanAccount getAccountFromClaims()
        {
            var player = getPlayerFromClaims();
            var account = player.jBeanAccount();
            return account;
        }
        protected IPlayer getPlayerFromClaims()
        {
            var user = User as ClaimsPrincipal;
            var claims = user.Claims.ToList();

            var playerid = claims.Find(x => x.Type == ClaimType.PlayerID);
            var clientid = claims.Find(x => x.Type == ClaimType.ClientId);

            return JB2.Identity.PlayerStore.GetPlayerByAppPlayerID(playerid.Value, clientid.Value);
        }

        protected bool isPermitted(string scopeName)
        {

            var user = User as ClaimsPrincipal;
            var claims = user.Claims.ToList();

            var claim = claims.Find(x => x.Type == "scope:" + scopeName);

            return claim != null;


        }

        protected JB2.Identity.IApplication getApplicationFromClaims()
        {
            var user = User as ClaimsPrincipal;
            var claims = user.Claims.ToList();
            var clientid = claims.Find(x => x.Type == ClaimType.ClientId);

            //JB2.Identity.
        }





    }
}
