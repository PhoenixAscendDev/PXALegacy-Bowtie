using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Common

{
    public class Business : JB2.Common.IBusiness
    {
        public IPerson<string> POC { get; set; }
        public IAddress MailingAddress { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }

        public string GetID()
        {
            return ID;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
