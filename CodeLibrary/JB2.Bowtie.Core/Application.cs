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
        private string _secret;       
        private Enum.APIAuthorizeState _APIstate;
        private string _apiKey;


        #endregion Fields

        #region Constructors

        public Application(string publickey, string secretKey) : this (publickey,secretKey,Enum.APIAuthorizeState.Unknown)
        {

        }
        public Application(string publickey, string secretKey, Enum.APIAuthorizeState state ) : base(Enum.BowtieObjectType.bowtie_application,publickey)
        {
            this._secret = secretKey;
            this._apiKey = publickey;

            _APIstate = state;          
        }

        #endregion Constructors

        #region Public Properies

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



        #endregion Public Properies

        #region IAPIKeySecretPair

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


        #endregion IAPIKeySecretPair

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

        #endregion IApplication

        #region Idenitity IApplication
        public string GetjBeanSecret()
        {
            throw new NotImplementedException();
        }

        public JB2Image GetIcon()
        {
            throw new NotImplementedException();
        }

        #endregion Idenitity IApplication

    }
}
