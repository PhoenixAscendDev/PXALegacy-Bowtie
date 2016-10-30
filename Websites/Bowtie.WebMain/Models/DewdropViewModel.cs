using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JB2.Bowtie.Web.Models
{
    public class DewdropViewModel : JB2.Common.IDNamePair<string,string>
    {
        public string ApplicationID { get; set; }
        public string GraphID { get; set; }
        public string Description { get; set; }
        public int jBeanCost { get; set; }  
    }
}