using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.Bowtie.Web.Models
{
    public class GraphPropertyViewModel : GraphElementViewModel
    {
        public string GraphPropertyType { get; set; }

        public bool isMultiValued { get; set; }
    }
}