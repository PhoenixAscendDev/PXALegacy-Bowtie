using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie.Attributes
{
    public class TokenName : System.Attribute
    {
        public string name;

        public TokenName(string name)
        {
            this.name = name;
        }


    }


}
