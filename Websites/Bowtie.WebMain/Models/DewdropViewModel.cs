using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace JB2.Bowtie.Web.Models
{
    public class DewdropViewModel : BaseViewModel
    {
        [Display(Name = "Application")]
        public string ApplicationID { get; set; }
        [Display(Name = "Action ")]
        public string GraphID { get; set; }
        public string Description { get; set; }
        public int jBeanCost { get; set; }  
    }
}