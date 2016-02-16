using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Dewdrop : JB2.Common.IDNamePair, JB2.Identity.IApplicationable, JB2.Common.IIDNamePair<string, string>
    {
        #region Fields
        protected string _appid;
        protected string _graphID;
        #endregion Fields

        public Dewdrop()
        {

        }

        public Dewdrop(string id, string name, string applicationID, string graphID)
        {
            _id = id;
            _name = name;
            _appid = applicationID;
            _graphID = graphID;
        }

        protected string ApplicationID
        {
            get;set;
        } 

        public string GetApplicationID()
        {
            return _appid;
        }

        public string GetGraphID()
        {
            return _graphID;
        }

        public static Dewdrop NewPlayerDew(string name, string applicationID)
        {
            Dewdrop newDew = new Dewdrop();
            newDew.ID = "dew" + JB2.Common.NewID.ShortGuid();
            newDew.Name = name;
            newDew.ApplicationID = applicationID;
            return newDew;
        }
    }
}
