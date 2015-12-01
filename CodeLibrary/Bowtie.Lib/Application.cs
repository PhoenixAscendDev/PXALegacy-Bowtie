using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Application : BowtieObject,IApplication
    {

        #region Fields
        private string _secret;       
        private Enum.APIAuthorizeState _APIstate;

        
        #endregion Fields

        #region Constructors
        public Application(string publickey, string secretKey ) : base(Enum.BowtieObjectType.bowtie_application,publickey)
        {
            this._secret = secretKey;           
        }
        #endregion Constructors

        #region Public Properies

        public string Secret
        {
            get
            {
                return _secret;
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


        public bool canIssueJBeans
        {
            get;set;
            
        }

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
    }
}
