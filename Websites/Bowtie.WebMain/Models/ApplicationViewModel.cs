using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.Bowtie.Web.Models
{
    public class ApplicationViewModel
    {
        public string Name { get; set; }
        public string APIkey { get; set; }

        public string ID { get; set; }

        public string ClientID { get; set; }

        public Enum.APIAuthorizeState AuthorizedState { get; set; }
    }
}