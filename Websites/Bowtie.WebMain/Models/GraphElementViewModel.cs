using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

using JB2.Bowtie;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie.Web.Models
{
    public class GraphElementViewModel
    {
        public string ApplicationID { get; set; }
       

        public string ElementType { get; set; }
        

        public string ID { get; set; }
       

        public string Name { get; set; }
        

        public string ParentID { get; set; }
       
    }
}