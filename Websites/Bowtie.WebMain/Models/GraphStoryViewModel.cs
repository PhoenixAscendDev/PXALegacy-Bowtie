using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace JB2.Bowtie.Web.Models
{
    public class GraphStoryViewModel : GraphElementViewModel
    {
        [Display(Name = "Action")]
        public string ActionID { get; set; }
        [Display(Name = "Object")]
        public string ObjectID { get; set; }

        public string Past { get; set; }
        public string PluralPast { get; set; }
        public string Present { get; set; }
        public string PluralPresent { get; set; }
        public string ImperativeTense { get; set; }


    }
}