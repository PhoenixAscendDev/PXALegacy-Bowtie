using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;

namespace JB2.Bowtie.Web.Models
{
    public class GraphActionViewModel : GraphElementViewModel
    {
        public string[] PostedPropertyIDs { get; set; }
        public IEnumerable<SelectListItem> Properties { get; set; }

        public string[] PostedObjectIDs { get; set; }

        public IEnumerable<SelectListItem> Objects { get; set; }




    }
}