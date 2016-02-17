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
        protected string _description;
        #endregion Fields

        public Dewdrop()
        {

        }

        public Dewdrop(string id, string name, string description, string applicationID, string graphID)
        {
            _id = id;
            _name = name;
            _appid = applicationID;
            _graphID = graphID;
            _description = description;
        }

        protected string ApplicationID
        {
            get
            {
                return _appid;
            }
            set
            {
                _appid = value;
            }
        } 

        public string GetApplicationID()
        {
            return _appid;
        }

        public string GetGraphID()
        {
            return _graphID;
        }

        public string GetDescription()
        {
            return _description;
        }

        public static Dewdrop NewDewdrop(string name, string description, string applicationID,string graphID)
        {
            Dewdrop newDew = new Dewdrop();
            newDew.ID = "dew_" + JB2.Common.NewID.Base62();
            newDew.Name = name;
            newDew.ApplicationID = applicationID;
            newDew._graphID = graphID;
            newDew._description = description;
            return newDew;
        }

        public static Dewdrop Empty()
        {
            Dewdrop newDew = new Dewdrop();
            newDew.ID = string.Empty;
            newDew.Name = string.Empty;
            newDew.ApplicationID = string.Empty;
            newDew._graphID = string.Empty;
            newDew._description = string.Empty;

            return newDew;
        }
    }
}
