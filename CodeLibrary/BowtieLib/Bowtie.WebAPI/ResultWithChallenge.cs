using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Http.Results;

namespace Bowtie.WebAPI
{
    public class ResultWithChallenge : IHttpActionResult
    {
        private readonly string authenticationScheme = "amx";
        private readonly IHttpActionResult next;

        public ResultWithChallenge(IHttpActionResult next)
        {
            this.next = next;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await next.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(authenticationScheme));
            }

            return response;
        }
    }
}