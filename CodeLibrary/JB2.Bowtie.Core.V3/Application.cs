using JB2.Bowtie.Enum;
using JB2.Common;

namespace JB2.Bowtie
{
    public class BasicApplication : IApplication
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


        public static BasicApplication New
        {
            get
            {
                var a = new BasicApplication();
                a.ID = JB2.Helper.Bowtie.GenerateID<BasicApplication>();

                return a;
            }
        }
        
    }
}
