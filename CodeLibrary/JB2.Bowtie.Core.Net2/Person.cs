using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JB2.Common;

namespace JB2.Bowtie
{
    public class Person : JB2.Common.IPerson<string>
    {
        public string DisplayName
        {
            get;set;
           
        }

        public string ID
        {
            get;set;
        }

        public Name Name
        {
            get;set;
          
        }

        public string GetID()
        {
            return ID;
        }

        public Name GetName()
        {
            return Name;
        }
    }
}
