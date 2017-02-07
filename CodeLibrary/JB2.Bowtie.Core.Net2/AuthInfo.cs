using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    public struct AuthInfo
    {
        public string ProviderID { get; set; }
        public string UserID { get; set; }

        public object AccessToken { get; set; }
    }
}
