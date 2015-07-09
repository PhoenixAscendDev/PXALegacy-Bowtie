using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class BowtieConfig : JB2.Common.JB2AppSettings<BowtieConfig>
        {
            public string SecretKey = "secret";
            public string AppKey = "appkey";
            public string Signature = "";
            public JB2.Bowtie.BowtieAPI API = new JB2.Bowtie.BowtieAPI();

        }
}
