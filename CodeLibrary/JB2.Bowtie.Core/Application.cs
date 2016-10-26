using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common;

namespace JB2.Bowtie
{
    public class Application : BowtieObject,IApplication, JB2.Common.IAPIKeySecretPair
    {

        #region Fields
        protected string _secret;       
        protected Enum.APIAuthorizeState _APIstate;
        protected string _apiKey;
        protected IDictionary<string, IModule> _modules;
        protected IDictionary<string, ApplicationModulePermission> _modulePermission;
        protected BaseCollection<TreasuryRequestKey> _treasuryKeys;
        #endregion Fields

        #region Constructors

        public Application(string publickey, string secretKey) : this (publickey,secretKey,Enum.APIAuthorizeState.Unknown)
        {

        }
        public Application(string publickey, string secretKey, Enum.APIAuthorizeState state ) : this(publickey,secretKey,Enum.APIAuthorizeState.Unknown, new List<IModule>())
        {
        
        }

        public Application(string publickey, string secretKey, Enum.APIAuthorizeState state,IEnumerable<IModule> modules) : base(Enum.BowtieObjectType.bowtie_application, publickey) 
        {
            this._secret = secretKey;
            this._apiKey = publickey;

            _APIstate = state;

            _modules = new Dictionary<string, IModule>();
            foreach(var m in modules)
            {
                _modules.Add(m.GetID(), m);
            }
        }

        #endregion Constructors

        #region Public Properies

        public IEnumerable<TreasuryRequestKey> TreasuryKeys
        {
            get
            {
                return _treasuryKeys;
            }
            set
            {
                _treasuryKeys = new BaseCollection<TreasuryRequestKey>(value);
            }
        }

        public bool isAuthorized
        {
            get
            {
                return _APIstate == Enum.APIAuthorizeState.Authorized || _APIstate == Enum.APIAuthorizeState.Unknown;
            }
        }

        public Enum.APIAuthorizeState AuthorizedState
        {
            get
            {
                return _APIstate;
            }

        }

        public string ClientID
        {
            get;
            set;
        }

        public string CompanyID
        {
            get
            {
                return _props.GetProperty<string>("CompanyID", string.Empty);
            }
            set
            {
                _props.SetProperty<string>("CompanyID", value);
            }
        }

        #region IAPIKeySecretPair

        public string Secret
        {
            get
            {
                return _secret;
            }
            set
            {
                _secret = value;
            }
        }


        public string APIkey
        {
            get
            {
                return _apiKey;
            }
            set
            {
                _apiKey = value;
            }
        }

        public string Website
        {
            get
            {
                return _props.GetProperty<string>("Website", string.Empty);
            }

            set
            {
                _props.SetProperty<string>("Website", value);
            }
        }

        #endregion IAPIKeySecretPair

        #endregion Public Properies

        #region Public Methods

        #region IApplication
        public IEnumerable<JB2.Identity.IPlayer> GetAdmins()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<JB2.Identity.IApplicationVersion>  GetVersions()
        {
            throw new NotImplementedException();
        }

        public JB2.Identity.IAuthClient GetAuthClient()
        {
            throw new NotImplementedException();
        }

        public JB2.Common.IMetaData GetMetaData(string propertyName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModule> GetModules()
        {
            return _modules.Values;
        }

        public ApplicationModulePermission GetModulePermission(string moduleID)
        {
            if (_modulePermission.Keys.Contains(moduleID))
                return _modulePermission[moduleID];
            else
            {
                var e = ApplicationModulePermission.Empty;
                e.ApplicationID = this.GetID();
                e.ModuleID = moduleID;
                return e;
            }
        }

        #endregion IApplication

        #endregion Public Methods

        #region Idenitity IApplication
        public string GetjBeanSecret()
        {
            var jBeanID = JB2.Settings.Jbean.Factory.Treasury.GetID();
            var key = this.GetTreasuryRequestKey(jBeanID);
            return key.Key;
        }

        public JB2Image GetIcon()
        {
            throw new NotImplementedException();
        }

        public string GetCounterName()
        {
            return "bowtie_application";
        }

        public int GetCounterIndex()
        {
            throw new NotImplementedException();
        }

        public TreasuryRequestKey GetTreasuryRequestKey(string treasuryID)
        {
            var key = _treasuryKeys.Find(x => x.TreasuryID == treasuryID);

            return key;
        }

        #endregion Idenitity IApplication

    }
}
