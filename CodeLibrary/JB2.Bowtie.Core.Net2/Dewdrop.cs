using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JB2.Bowtie
{
    [GraphElement("o67C3D61D")]
    [GraphPropertyMap("id", "ID")]
    [GraphPropertyMap("title", "Name")]
    [GraphPropertyMap("appID", "GetApplicationID", Enum.ClassMemberType.Method)]
    [GraphPropertyMap("storyID", "GetGraphID", Enum.ClassMemberType.Method)]
    //[GraphPropertyMap("currencyCost", "GetjBeanCost", Enum.ClassMemberType.Method)]
    [GraphPropertyMap("description", "GetDescription", Enum.ClassMemberType.Method)]
    public class Dewdrop : JB2.Common.IDNamePair, IDewdrop,  JB2.Common.IIDNamePair<string, string>
    {
        #region Fields
        protected string _appid;
        protected string _graphID;
        protected string _description;
        protected Dictionary<string, int> _currencyCost;
        protected int _jbeanCost;

        protected JB2.Common.MetaDataCollection _prop;

        #endregion Fields

        #region Constructors
        public Dewdrop()
        {
            _prop = new JB2.Common.MetaDataCollection();

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

        #endregion Constructors

        #region Properties
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

        #endregion Properties

        #region Methods
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
        //public int GetjBeanCost()
        //{
        //    return _jbeanCost;
        //}

        public int GetCurrencyCost(string currencyID)
        {
            if (_currencyCost.ContainsKey(currencyID))
                return _currencyCost[currencyID];
            else
                return 0;
        }

        #endregion Methods

        #region IClass
        public T GetProperity<T>(string index, T defaultValue)
        {
            return _prop.GetProperty<T>(index, defaultValue);
        }

        public void SetProperty<T>(string index, T newValue, bool changeLastUpdate)
        {
            _prop.SetProperty<T>(index, newValue, changeLastUpdate);
        }

        public DateTime GetLastUpdate()
        {
            return System.DateTime.Now;
        }

        #endregion IClass

        #region Static Methods
        public static Dewdrop NewDewdrop(string name, string description, string applicationID, string graphID, int jbeanCost = 0)
        {
            Dewdrop newDew = new Dewdrop();
            var idvalue = //JB2.Common.NewID.ShortGuid();// JB2.Infrastructure.Counter.GetNext("dewdrop", defaultStart: 1000000);
            newDew.ID = "dew_" + JB2.Common.NewID.ShortGuid(); //JB2.Common.NewID.Base62("dew_{0}", (long)idvalue);
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

        #endregion Static Methods

    }
}
