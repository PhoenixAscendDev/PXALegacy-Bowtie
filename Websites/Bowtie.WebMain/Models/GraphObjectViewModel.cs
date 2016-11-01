using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace JB2.Bowtie.Web.Models
{
    public class GraphObjectViewModel : GraphElementViewModel
    {

        public string Determiner {get;set;}

        public IEnumerable<SelectListItem> Properties { get; set; }

        public GraphObject obj { get; set; }

        public string Singular { get; set; }
        public string Plural { get; set; }

        public string[] PostedPropertyIDs { get; set; }


    }
}