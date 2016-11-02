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
        public string GraphPropertyTypeID { get; set; }

        public string GraphPropertyTypeName { get; set; }

        public bool isMultiValued { get; set; }
    }
}