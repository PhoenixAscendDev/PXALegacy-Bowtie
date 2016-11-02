using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace JB2.Bowtie.Web.Models
{
    public class GraphPropertyViewModel : GraphElementViewModel
    {
        [Display(Name = "Data Type")]
        public string GraphPropertyType { get; set; }

        public bool isMultiValued { get; set; }
    }
}