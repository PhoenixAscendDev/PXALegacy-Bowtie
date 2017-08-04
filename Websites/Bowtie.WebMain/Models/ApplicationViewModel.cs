using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

using JB2.Bowtie;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.Web.Models
{
    public class ApplicationViewModel : BaseViewModel
    {

        [Display(Name = "Bowtie Key")]
        public string APIkey { get; set; }

        public string Secret { get; set; }

        public string ClientID { get; set; }

        public string Website { get; set; }

        public string CompanyID { get; set; }
        public string CompanyName { get; set; }

        public string jBeanKey { get; set; }

        public string CurrencySystem { get; set; }

        public string PointSystem { get; set; }


        public Enum.APIAuthorizeState AuthorizedState { get; set; }

        }

}