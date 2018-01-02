using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie.Attributes
{
    public class TokenName : System.Attribute
    {
        public string Name;

        public TokenName(string name)
        {
            this.Name = name;
        }


    }


}
