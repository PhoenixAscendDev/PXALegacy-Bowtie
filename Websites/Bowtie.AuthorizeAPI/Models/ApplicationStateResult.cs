using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using JB2.Common;
using JB2.Common.WebAPI;

namespace Bowtie.AuthorizeAPI.Models
{
    public class ApplicationStateView : APIObject
    {

        public string ApplicationID { get; set; }
        public string State { get; set; }

        public string Message { get; set; }

        public bool isAuthorized { get; set; }

        public string AuthorizeKey { get; set; }
    }
}