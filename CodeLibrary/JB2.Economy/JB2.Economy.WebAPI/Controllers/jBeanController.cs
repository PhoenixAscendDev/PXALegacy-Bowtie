using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Security.Claims;

using JB2.Common.WebAPI;
using JB2.Identity;

using JB2.Common;

namespace JB2.Economy.WebAPI.Controllers
{
    public class jBeanController : BaseApiController
    {
        [HttpGet]
        public IHttpActionResult NewBankAccount(string id)
        {
            var account = getCentralBank().OpenNewBankAccount(new JB2.Common.IDNamePair(id, string.Empty));
            return Json(account);
        }

        [HttpGet]
        public IHttpActionResult BankAccount(string id)
        {
            var account = getCentralBank().GetBankAccount(new JB2.Common.IDNamePair<string, string>(id, string.Empty));

            return Json(account);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult BankAccount()
        {
            if (isPermitted("bowtie"))
            {
                return Json(getAccountFromClaims());
            }
            else
                return Json(string.Empty);
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult AccountBalance()
        {
            var player = getPlayerFromClaims();
            var account = player.jBeanAccount();
            return Json(getCentralBank().CheckBalance(getAccountFromClaims()));
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult WithdrawFromAccount(ITreasuryRequest request)
        {
            var bank = getCentralBank();
            var account = getAccountFromClaims();
            IBankTransactionReceipt receipt = null;

            if (request.Requestor.GetType() != typeof(Identity.IApplication))
            {
                var requestApp = JB2.Identity.ApplicatonStore.GetApplicationByID(request.Requestor.ToString());
                request.Requestor = requestApp;
            }
            receipt = bank.Withdrawn(account, request);
            return Json(receipt);              
        }

        [HttpGet]
        public IHttpActionResult  DepositTreasuryNote([FromUri] string note, [FromUri] string accountnumber)
        {
            var n = new JbeanTreasuryNote(note, 100,new JB2.Common.IDNamePair<string,string>("apptest",string.Empty));
            var account = getCentralBank().GetBankAccount(new JB2.Common.IDNamePair<string, string>("jbtest566", string.Empty));

            var result = getCentralBank().Deposit(account, n);

            return Json(result);
        }

        [HttpGet]
        public IHttpActionResult WithdrawTokens([FromUri] int amount, [FromUri] string accountNumber)
        {
            var account = getCentralBank().GetBankAccount(new JB2.Common.IDNamePair<string, string>("jbtest566", string.Empty));
            var request = new TreasuryRequest();
            request.Amount = 50;
            request.RequestDate = DateTime.Now;
            request.Requestor = new JB2.Common.IDNamePair<string, string>("apptest", string.Empty);
            request.VerificationKey = "not being used";

            var result = getCentralBank().Withdrawn(account, request);

            return Json(result);
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult DepositToAccount(ITreasuryNote note)
        {
            IBankTransactionReceipt receipt = null;
            var account = getAccountFromClaims();
            var bank = getCentralBank();

            receipt = bank.Deposit(account, note);

            return Json(receipt);
        }
        
        [HttpGet]
        public IHttpActionResult RequestTokens([FromUri] string amount, [FromUri] string appid)
        {
            TreasuryRequest request = new TreasuryRequest();
            long a = 0;
            long.TryParse(amount, out a);
            request.Amount = a;
            request.RequestDate = DateTime.Now;
            request.Requestor = new JB2.Common.IDNamePair<string, string>(appid,string.Empty);

            return RequestAmountFromTreasury(request);

        }

        public IHttpActionResult RequestAmountFromTreasury(ITreasuryRequest request)
        {
            var treasury = getTreasury();

            //if (request.Requestor.GetType() != typeof(Identity.IApplication))
            //{
            //    var requestApp = JB2.Identity.ApplicatonStore.GetApplicationByID(request.Requestor.ToString());
            //    request.Requestor = requestApp;
            //}

            var note = treasury.IssueNote(request);

            return Json(note);
        }

        #region Helpers

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

            return JB2.Identity.ApplicatonStore.GetApplicationByClientID(clientid.Value);
            //JB2.Identity.
        }

        #endregion Helpers





    }
}
