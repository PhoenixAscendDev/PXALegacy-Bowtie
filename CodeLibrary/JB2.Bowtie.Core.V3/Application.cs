using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie
{
    public class Application : IApplication
    {
        public string Website { get; set; }

        public bool IsAuthorized => true;

        public APIAuthorizeState AuthorizedState => APIAuthorizeState.Authorized;

        public IBusiness Company { get; set; }
        public string APIkey { get; set; }
        public string Secret { get; set; }
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
