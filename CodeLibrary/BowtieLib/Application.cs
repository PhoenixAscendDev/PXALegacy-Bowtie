using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2.Bowtie
{
    public class Application : BowtieObject,JB2.Common.IIDNamePair<string, string>
    {
        private string _secret;     

        public Application(string publickey, string secretKey ) : base(Enum.BowtieObjectType.bowtie_application,publickey)
        {
            this._secret = secretKey;           
        }

        #region Public Properies
    
        public string Secret
        {
            get
            {
                return _secret;
            }
        }

       #endregion Public Properies
    }
}
