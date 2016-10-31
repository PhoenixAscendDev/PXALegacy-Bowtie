using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Dewdrop : JB2.Common.IDNamePair, IDewdrop, JB2.Identity.IApplicationable, JB2.Common.IIDNamePair<string, string>
    {
        #region Fields
        protected string _appid;
        protected string _graphID;
        protected string _description;
        protected int _jbeanCost;
        #endregion Fields

        public Dewdrop()
        {

        }

        public Dewdrop(string id, string name, string description, string applicationID, string graphID, int jbeanCost = 0)
        {
            _id = id;
            _name = name;
            _appid = applicationID;
            _graphID = graphID;
            _description = description;
            _jbeanCost = jbeanCost;
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

        protected int jBeanCost
        {
            get
            {
                return _jbeanCost;
            }
            set
            {
                _jbeanCost = value;
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

        public static Dewdrop NewDewdrop(string name, string description, string applicationID, string graphID, int jbeanCost = 0)
        {
            Dewdrop newDew = new Dewdrop();
            var idvalue = JB2.Infrastructure.Counter.GetNext("dewdrop", defaultStart: 1000000);
            newDew.ID = JB2.Common.NewID.Base62("dew_{0}", (long)idvalue);
            newDew.Name = name;
            newDew.ApplicationID = applicationID;
            newDew._graphID = graphID;
            newDew._description = description;
            newDew._jbeanCost = jbeanCost;
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

        public int GetjBeanCost()
        {
            return _jbeanCost;
        }
    }
}
